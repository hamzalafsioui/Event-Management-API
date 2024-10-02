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

		public async Task<User> GetUserByIdAsync(int id)
		{
			//var user = await _userRepository.GetByIdAsync(id);
			var user = _userRepository.GetTableNoTracking()
									   .Where(x=>x.UserId == id)
									   .FirstOrDefault();  // we can use Include Here
			return user!;
		}
		#endregion
	}
}
