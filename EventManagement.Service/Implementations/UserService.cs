using EventManagement.Data.Entities;
using EventManagement.Infrustructure.Abstracts;
using EventManagement.Infrustructure.Context;
using EventManagement.Infrustructure.Repositories;
using EventManagement.Service.Abstracts;

namespace EventManagement.Service.Implementations
{
	public class UserService : IUserService
	{
		#region Fields
		private IUserRepository _userRepository;
		#endregion

		#region Constructors
		public UserService(IUserRepository userRepository)
		{
			_userRepository = userRepository;
		}
		#endregion

		#region Handl Functions
		public async Task<List<User>> GetUsersListAsync()
		{
			return await _userRepository.GetUsersListAsync();
		}
		#endregion
	}
}
