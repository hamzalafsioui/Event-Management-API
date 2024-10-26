using EventManagement.Data.Entities.Identity;
using EventManagement.Data.Helper.Authentication;

namespace EventManagement.Service.Abstracts
{
	public interface IAuthenticationService
	{
		public JwtAuthResponse GetJWTToken(User user);
	}
}
