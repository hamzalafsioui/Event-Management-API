using EventManagement.Data.Entities.Identity;
using EventManagement.Data.Helper.Authentication;

namespace EventManagement.Service.Abstracts
{
	public interface IAuthenticationService
	{
		public Task<JwtAuthResponse> GetJWTTokenAsync(User user);
	}
}
