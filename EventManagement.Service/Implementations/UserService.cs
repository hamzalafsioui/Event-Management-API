using EventManagement.Data.Entities;
using EventManagement.Data.Helper;
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

		public async Task<User> GetByIdWithIncludeAsync(int id)
		{
			//var user = await _userRepository.GetByIdAsync(id);
			var user = _userRepository.GetTableNoTracking()
									   .Where(x => x.Id == id)
									   .FirstOrDefault();  // we can use Include Here
			return user!;
		}

		public async Task<string> AddAsync(User user)
		{
			var result = await IsUserNameExist(user.UserName);
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
			var result = _userRepository.GetTableNoTracking().Where(x => x.UserName.Equals(name)).FirstOrDefault();
			if (result != null)
				return true;
			return false;
		}

		public async Task<bool> IsUserNameExistExcludeSelf(string username, int id)
		{
			// check if the username is exist or not with other id
			var result = _userRepository.GetTableNoTracking().Where(x => x.UserName.Equals(username) & !(x.Id.Equals(id))).FirstOrDefault(); // !& x.UserId.Equals(id) => && x.UserId != id
			if (result == null)
				return false;
			return true;
		}

		public async Task<string> EditAsync(User userMapper)
		{
			await _userRepository.UpdateAsync(userMapper);
			return "Success";
		}

		public async Task<string> DeleteAsync(User user)
		{
			var transaction = _userRepository.BeginTransaction();
			try
			{
				await _userRepository.DeleteAsync(user);
				await transaction.CommitAsync();
				return "Success";
			}
			catch (Exception ex)
			{
				await transaction.RollbackAsync();
				return "Failed";
			}

		}

		public async Task<User> GetByIdAsync(int id)
		{
			var user = await _userRepository.GetByIdAsync(id);
			return user;
		}

		public IQueryable<User> GetUsersListQueryable()
		{
			return _userRepository.GetTableNoTracking().AsQueryable();
		}

		public IQueryable<User> FilterUserPaginatedQueryable(UserOrderingEnum orderingEnum, string search)
		{
			var queryable = _userRepository.GetTableNoTracking().AsQueryable();
			if (!string.IsNullOrEmpty(search))
			{
				queryable = queryable.Where(x => x.UserName.Contains(search) || x.Email.Contains(search));

			}
			switch (orderingEnum)
			{
				case UserOrderingEnum.UserId:
					queryable = queryable.OrderBy(x => x.Id);
					break;
				case UserOrderingEnum.Username:
					queryable = queryable.OrderBy(x => x.UserName);
					break;
				case UserOrderingEnum.FirstName:
					queryable = queryable.OrderBy(x => x.FirstName);
					break;
				case UserOrderingEnum.LastName:
					queryable = queryable.OrderBy(x => x.LastName);
					break;
				case UserOrderingEnum.Email:
					queryable = queryable.OrderBy(x => x.Email);
					break;
				case UserOrderingEnum.Role:
					queryable = queryable.OrderBy(x => x.Role);
					break;
				case UserOrderingEnum.CreatedAt:
					queryable = queryable.OrderBy(x => x.CreatedAt);
					break;
				default:
					queryable = queryable.OrderBy(x => x.Id);
					break;
			}
			return queryable;

		}
		#endregion
	}
}
