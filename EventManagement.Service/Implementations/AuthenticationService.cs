using EventManagement.Data.Entities.Identity;
using EventManagement.Data.Helper.Authentication;
using EventManagement.Service.Abstracts;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EventManagement.Service.Implementations
{
	public class AuthenticationService : IAuthenticationService
	{
		#region Fields
		private readonly JwtSettings _jwtSettings;
		#endregion
		#region Constructors
		public AuthenticationService(JwtSettings jwtSettings)
		{
			_jwtSettings = jwtSettings;
		}
		#endregion
		#region Handle Functions
		public Task<string> GetJWTToken(User user)
		{
			// define the claims including in the token
			var claims = new List<Claim>()
			{
				new Claim(JwtRegisteredClaimNames.Sub,user.Id.ToString()),
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

			// return the serialized token as a string
			return Task.FromResult(accessToken);
		}
		#endregion

	}
}
