using EventManagement.Data.Entities.Identity;
using EventManagement.Data.Helper.Authentication;
using EventManagement.Data.Helper.Models;
using EventManagement.Data.Responses;
using EventManagement.Infrustructure.Abstracts;
using EventManagement.Service.Abstracts;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
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
		private readonly IRefreshTokenRepository _refreshTokenRepository;
		private readonly UserManager<User> _userManager;
		#endregion
		#region Constructors
		public AuthenticationService(JwtSettings jwtSettings,
			IRefreshTokenRepository refreshTokenRepository,
			UserManager<User> userManager)
		{
			_jwtSettings = jwtSettings;
			_refreshTokenRepository = refreshTokenRepository;
			_userManager = userManager;
		}
		#endregion
		#region Handle Functions
		public async Task<JwtAuthResponse> GetJWTTokenAsync(User user)
		{
			// retrieve roles
			var roles = await _userManager.GetRolesAsync(user);
			// define the claims including in the token
			var claims = GetClaims(user, roles.ToList()); 
			// create the signing key from appsettings.json
			if (string.IsNullOrEmpty(_jwtSettings.SigningKey))
			{
				throw new ArgumentNullException(nameof(_jwtSettings.SigningKey), "JWT signing key cannot be null or empty.");
			}
			var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SigningKey));
			var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

			// create the token descriptor with claims,exipry, signing credentials
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

			var userRefreshToken = new UserRefreshToken
			{
				UserId = user.Id,
				//CreatedAt = DateTime.Now, //Option: will be automatically filled by saveChangesAsync because Inherite IHasCreateAt
				ExpiredAt = DateTime.Now.AddDays(_jwtSettings.RefreshTokenExpireDate),
				IsRevoked = false,
				IsUsed = true,
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

		private List<Claim> GetClaims(User user,List<string> roles)
		{
			var claims =  new List<Claim>()
			{
				new Claim(nameof(UserClaimModel.Id),user.Id.ToString()),
				new Claim(ClaimTypes.Name,user.UserName!),
				new Claim(ClaimTypes.Email,user.Email!)
			};
			foreach(var role in roles)
			{
				claims.Add(new Claim(ClaimTypes.Role,role.ToString()));
			}
			return claims;
		}
		private (JwtSecurityToken, string) GenerateJWTToken(User user)
		{
			// define the claims including in the token
			var claims = new List<Claim>()
			{
				new Claim(nameof(UserClaimModel.Id),user.Id.ToString()),
				new Claim(nameof(UserClaimModel.UserName),user.UserName!),
				new Claim(nameof(UserClaimModel.Email),user.Email!)
			};

			// create the signing key from appsettings.json
			if (string.IsNullOrEmpty(_jwtSettings.SigningKey))
			{
				throw new ArgumentNullException(nameof(_jwtSettings.SigningKey), "JWT signing key cannot be null or empty.");
			}

			var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SigningKey));
			var jwtToken = new JwtSecurityToken(
				_jwtSettings.Issuer,
				_jwtSettings.Audience,
				claims,
				expires: DateTime.UtcNow.AddMinutes(Convert.ToDouble(_jwtSettings.LifeTimeInMinutes)),
				signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
				);
			var accessToken = new JwtSecurityTokenHandler().WriteToken(jwtToken);
			return (jwtToken, accessToken);
		}
		public JwtAuthResponse GetRefreshToken(User user, JwtSecurityToken jwtToken, DateTime? expiredDate, string refreshToken)
		{

			// Generate Refresh Token
			var (jwtSecurityToken, newToken) = GenerateJWTToken(user);
			var response = new JwtAuthResponse();
			response.AccessToken = newToken;
			var RefreshTokenResult = new RefreshToken(user.UserName!, refreshToken, expiredDate);
			response.RefreshToken = RefreshTokenResult;
			return response;
		}
		public JwtSecurityToken ReadJwtToken(string accessToken)
		{
			if (string.IsNullOrEmpty(accessToken))
				throw new ArgumentNullException(nameof(accessToken));

			var handler = new JwtSecurityTokenHandler();
			var response = handler.ReadJwtToken(accessToken);
			return response;
		}
		private string GenerateRefreshToken()
		{
			var randomNumber = new byte[32];
			var randomNumberGenerator = RandomNumberGenerator.Create();
			randomNumberGenerator.GetBytes(randomNumber);
			return Convert.ToBase64String(randomNumber);
		}

		public string ValidateToken(string accessToken)
		{
			var handler = new JwtSecurityTokenHandler();
			var parameters = new TokenValidationParameters
			{
				ValidateIssuer = true,
				ValidateAudience = true,
				ValidateLifetime = true,
				ValidateIssuerSigningKey = true,
				ValidIssuer = _jwtSettings.Issuer,
				ValidAudience = _jwtSettings.Audience,
				IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SigningKey)),
				ClockSkew = TimeSpan.Zero // Eliminate clock skew for token expiration

			};
			try
			{
				var validator = handler.ValidateToken(accessToken, parameters, out SecurityToken validatedToken);
				if (validator == null)
					return "InvalidToken";
				return "NotExpired";
			}
			catch (Exception ex)
			{

				return ex.Message;
			}
		}

		public async Task<(string userId, DateTime? expiredDate)> ValidateDetailsAsync(JwtSecurityToken jwtToken, string accessToken, string refreshToken)
		{
			if (jwtToken == null || !jwtToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256))
				return ("AlgorithmIsWrong", null);

			if (jwtToken.ValidTo > DateTime.UtcNow)
				return ("TokenIsNotExpired", null);

			// Get User
			var userId = jwtToken.Claims.FirstOrDefault(x => x.Type == nameof(UserClaimModel.Id))?.Value;
			var userRefreshToken = await _refreshTokenRepository.GetTableNoTracking()
				.FirstOrDefaultAsync(x => x.Token == accessToken && x.RefreshToken == refreshToken && x.UserId == int.Parse(userId!));

			if (userRefreshToken == null)
				return ("RefreshTokenIsNotFound", null);

			// validation Token & RefreshToken
			if (userRefreshToken.ExpiredAt < DateTime.UtcNow)
			{
				userRefreshToken.IsRevoked = true;
				userRefreshToken.IsUsed = false;
				await _refreshTokenRepository.UpdateAsync(userRefreshToken);
				return ("RefreshTokenIsExpired", null);
			}
			var expiredDate = userRefreshToken.ExpiredAt;
			return (userId!, expiredDate);
		}
		#endregion

	}
}
