using EventManagement.Data.Entities.Identity;

namespace EventManagement.Service.Abstracts
{
	public interface IAuthenticationService
	{
		public Task<string> GetJWTToken(User user);
	}
}
