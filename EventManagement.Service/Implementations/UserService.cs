using EventManagement.Data.Entities;
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
									   .Where(x => x.UserId == id)
									   .FirstOrDefault();  // we can use Include Here
			return user!;
		}

		public async Task<string> AddAsync(User user)
		{
			var result = await IsUserNameExist(user.Username);
			if (!result)
			{
				await _userRepository.AddAsync(user);
				return "Success";
			}
			return "Failed";

		}

		public async Task<bool> IsUserNameExist(string name)
		{
			// check is username exist or no
			var result = _userRepository.GetTableNoTracking().Where(x => x.Username.Equals(name)).FirstOrDefault();
			if (result != null)
				return true;
			return false;
		}

		public async Task<bool> IsUserNameNameExistExcludeSelf(string username, int id)
		{
			// check if the username is exist or not with other id
			var result = _userRepository.GetTableNoTracking().Where(x => x.Username.Equals(username) & !(x.UserId.Equals(id))).FirstOrDefault(); // !& x.UserId.Equals(id) => && x.UserId != id
			if (result == null)
				return false;
			return true;
		}

		public async Task<string> EditAsync(User userMapper)
		{
			await _userRepository.UpdateAsync(userMapper);
			return "Success";
		}
		#endregion
	}
}
