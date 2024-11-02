using EventManagement.Data.Entities.Identity;
using EventManagement.Data.Helper.Enums;
using EventManagement.Infrustructure.Repositories;
using EventManagement.Service.Abstracts;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Linq.Expressions;

namespace EventManagement.Service.Implementations
{
	public class UserService : UserManager<User>, IUserService
	{
		#region Fields
		private IUserRepository _userRepository;
		#endregion

		#region Constructors
		public UserService(IUserRepository userRepository,
				   IUserStore<User> store,
				   IOptions<IdentityOptions> optionsAccessor,
				   IPasswordHasher<User> passwordHasher,
				   IEnumerable<IUserValidator<User>> userValidators,
				   IEnumerable<IPasswordValidator<User>> passwordValidators,
				   ILookupNormalizer keyNormalizer,
				   IdentityErrorDescriber errors,
				   IServiceProvider services,
				   ILogger<UserManager<User>> logger)
	: base(store, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors, services, logger)
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
			return await _userRepository.GetTableNoTracking()
										.Include(x=>x.Role)
									   .Where(x => x.Id == id)
									   .FirstOrDefaultAsync()?? throw new Exception("User not found.");
			;  
		}

		public async Task<bool> AddAsync(User user)
		{
			var result = await IsUserNameExist(user.UserName!);
			if (!result)
			{
				await _userRepository.AddAsync(user);
				return true;
			}
			return false;

		}

		public async Task<bool> IsUserNameExist(string name) => await _userRepository.GetTableNoTracking().AnyAsync(x => x.UserName!.Equals(name));

		public async Task<bool> IsUserNameExistExcludeSelf(string username, int id) => await _userRepository.GetTableNoTracking().AnyAsync(x => x.UserName!.Equals(username) & !(x.Id.Equals(id))); // !& x.UserId.Equals(id) => && x.UserId != id


		public async Task<bool> EditAsync(User userMapper)
		{
			await _userRepository.UpdateAsync(userMapper);
			return true;
		}

		public async Task<bool> DeleteAsync(User user)
		{
			var transaction = await _userRepository.BeginTransactionAsync();
			try
			{
				await _userRepository.DeleteAsync(user);
				await transaction.CommitAsync();
				return true;
			}
			catch (Exception ex)
			{
				await transaction.RollbackAsync();
				return false;
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
				queryable = queryable.Where(x => x.UserName!.Contains(search) || x.Email!.Contains(search));

			}
			Expression<Func<User, object>> orderExpression = orderingEnum switch
			{
				UserOrderingEnum.UserId => x => x.Id,
				UserOrderingEnum.Username => x => x.UserName!,
				UserOrderingEnum.FirstName => x => x.FirstName,
				UserOrderingEnum.LastName => x => x.LastName,
				UserOrderingEnum.Email => x => x.Email!,
				UserOrderingEnum.Role => x => x.Role,
				UserOrderingEnum.CreatedAt => x => x.CreatedAt,
				_ => x => x.Id
			};
			return queryable.OrderBy(orderExpression);


		}
		#endregion
	}
}
