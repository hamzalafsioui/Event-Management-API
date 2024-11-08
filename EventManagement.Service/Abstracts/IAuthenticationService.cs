using EventManagement.Data.Entities.Identity;
using EventManagement.Data.Responses;
using System.IdentityModel.Tokens.Jwt;

namespace EventManagement.Service.Abstracts
{
    public interface IAuthenticationService
	{
		public Task<JwtAuthResponse> GetJWTTokenAsync(User user);
		public JwtAuthResponse GetRefreshToken(User user, JwtSecurityToken jwtToken, DateTime? expiredDate, string refreshToken);
		public string ValidateToken(string accessToken);
		public JwtSecurityToken ReadJwtToken(string accessToken);
		public Task<(string userId,DateTime? expiredDate)> ValidateDetailsAsync(JwtSecurityToken jwtToken, string accessToken, string refreshToken);
	}
}
