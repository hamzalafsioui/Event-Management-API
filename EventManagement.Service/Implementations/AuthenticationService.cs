using EventManagement.Data.Entities.Identity;
using EventManagement.Data.Helper.Authentication;
using EventManagement.Infrustructure.Abstracts;
using EventManagement.Service.Abstracts;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Concurrent;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace EventManagement.Service.Implementations
{
	public class AuthenticationService : IAuthenticationService
	{
		#region Fields
		private readonly JwtSettings _jwtSettings;
		private readonly ConcurrentDictionary<string, RefreshToken> _userRefreshToken;
		private readonly IRefreshTokenRepository _refreshTokenRepository;
		#endregion
		#region Constructors
		public AuthenticationService(JwtSettings jwtSettings, IRefreshTokenRepository refreshTokenRepository)
		{
			_jwtSettings = jwtSettings;
			_userRefreshToken = new ConcurrentDictionary<string, RefreshToken>();
			_refreshTokenRepository = refreshTokenRepository;
		}
		#endregion
		#region Handle Functions
		public async Task<JwtAuthResponse> GetJWTTokenAsync(User user)
		{
			// define the claims including in the token
			var claims = new List<Claim>()
			{
				new Claim(JwtRegisteredClaimNames.NameId,user.Id.ToString()),
				new Claim(JwtRegisteredClaimNames.Email,user.Email!),
				new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
				new Claim(JwtRegisteredClaimNames.Name,user.UserName!)
			};

			// create the signing key from appsettings.json
			if (string.IsNullOrEmpty(_jwtSettings.SigningKey))
			{
				throw new ArgumentNullException(nameof(_jwtSettings.SigningKey), "JWT signing key cannot be null or empty.");
			}
			var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SigningKey));
			var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

			// create the token descriptor with clims,exipry, signing credentials
			var tokenDescriptor = new SecurityTokenDescriptor
			{
				Subject = new ClaimsIdentity(claims),
				Expires = DateTime.Now.AddMinutes(Convert.ToDouble(_jwtSettings.LifeTimeInMinutes)),
				SigningCredentials = creds,
				Issuer = _jwtSettings.Issuer,
				Audience = _jwtSettings.Audience
			};

			// use JwtSecurityTokenHandler to create and write the token
			var tokenHandler = new JwtSecurityTokenHandler();
			var token = tokenHandler.CreateToken(tokenDescriptor);
			var accessToken = tokenHandler.WriteToken(token);

			var refreshToken = new RefreshToken(user.UserName!, GenerateRefreshToken(), DateTime.Now.AddDays(_jwtSettings.RefreshTokenExpireDate));

			_userRefreshToken.AddOrUpdate(refreshToken.TokenString, refreshToken, (s, t) => refreshToken);

			var userRefreshToken = new UserRefreshToken
			{
				UserId = user.Id,
				//CreatedAt = DateTime.Now, //Option: will be automatically filled by saveChangesAsync because Inherite IHasCreateAt
				ExpiredAt = DateTime.Now.AddDays(_jwtSettings.RefreshTokenExpireDate),
				IsRevoked = false,
				IsUsed = false,
				JwtId = token.Id,
				RefreshToken = refreshToken.TokenString,
				Token = accessToken,
			};
			await _refreshTokenRepository.AddAsync(userRefreshToken);
			// return response
			var response = new JwtAuthResponse();
			response.AccessToken = accessToken;
			response.RefreshToken = refreshToken;
			return response;
		}

		private string GenerateRefreshToken()
		{
			var randomNumber = new byte[32];
			var randomNumberGenerator = RandomNumberGenerator.Create();
			randomNumberGenerator.GetBytes(randomNumber);
			return Convert.ToBase64String(randomNumber);
		}
		#endregion

	}
}
