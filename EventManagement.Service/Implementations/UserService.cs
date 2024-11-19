using EventManagement.Data.Entities.Identity;
using EventManagement.Data.Entities.SPs;
using EventManagement.Data.Entities.Views;
using EventManagement.Data.Helper.Enums;
using EventManagement.Infrustructure.Abstracts.IViewRepository;
using EventManagement.Infrustructure.Abstracts.SPs;
using EventManagement.Infrustructure.Repositories;
using EventManagement.Service.Abstracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Serilog;
using System.Linq.Expressions;

namespace EventManagement.Service.Implementations
{
	public class UserService : UserManager<User>, IUserService
	{
		#region Fields
		private IUserRepository _userRepository;
		private readonly UserManager<User> _userManager;
		private readonly IHttpContextAccessor _httpContextAccessor;
		private readonly IEmailService _emailService;
		private readonly IUrlHelper _urlHelper;
		private readonly IViewRepository<ViewUserEventEngagementSummary> _viewUserEventEngagementSummaryRepository;
		private readonly ISP_GetUserEventEngagementDetailsRepository _sP_GetUserEventEngagementDetailsRepository;
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
				   ILogger<UserManager<User>> logger, UserManager<User> userManager,
				   IHttpContextAccessor httpContextAccessor,
				   IEmailService emailService,
				   IUrlHelper urlHelper,
				   IViewRepository<ViewUserEventEngagementSummary> viewRepository,
				   ISP_GetUserEventEngagementDetailsRepository sP_GetUserEventEngagementDetailsRepository)
	: base(store, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors, services, logger)
		{
			_userRepository = userRepository;
			_userManager = userManager;
			_httpContextAccessor = httpContextAccessor;
			_emailService = emailService;
			_urlHelper = urlHelper;
			_viewUserEventEngagementSummaryRepository = viewRepository;
			_sP_GetUserEventEngagementDetailsRepository = sP_GetUserEventEngagementDetailsRepository;
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
										.Include(x => x.Role)
									   .Where(x => x.Id == id)
									   .FirstOrDefaultAsync() ?? throw new Exception("User not found.");
			;
		}

		public async Task<string> AddAsync(User user, string password)
		{
			using (var transaction = await _userRepository.BeginTransactionAsync())
			{
				try
				{
					// create
					var CreateResult = await _userManager.CreateAsync(user, password);
					// Failed
					if (!CreateResult.Succeeded)
						return "ErrorInCreateUser";

					// Assign the role to the user
					var addToRoleResult = await _userManager.AddToRoleAsync(user, "User");
					if (!addToRoleResult.Succeeded)
						return "ErrorInAddRole";

					// confirm Emal
					var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
					var requestAccessor = _httpContextAccessor.HttpContext?.Request;
					//var url = requestAccessor!.Scheme + "://" + requestAccessor.Host + $"/Api/V1/Authentication/ConfirmEmail?userId={user.Id}&code={code}";
					var url = requestAccessor!.Scheme + "://" + requestAccessor.Host + _urlHelper.Action("ConfirmEmail", "Authentication", new { userId = user.Id, code = code });
					// message body
					var message = $"<p>To Confirm Your Email Please Click This Link: </p> <a href = '{url}'>Click Here</a>";
					var emailResult = await _emailService.SendEmailAsync(user.Email!, url, "Confirm Email");
					// success
					if (emailResult != "Success")
						return "FailedWhenSendEmail";
					await transaction.CommitAsync();
					return "Success";
				}
				catch (Exception ex)
				{
					await transaction.RollbackAsync();
					Log.Error($"Error In Add User: {ex.Message}");
					return ex.Message.ToString();
				}
			}


		}

		public async Task<bool> IsUserNameExist(string name) => await _userRepository.GetTableNoTracking().AnyAsync(x => x.UserName!.Equals(name));

		public async Task<bool> IsUserNameExistExcludeSelf(string username, int id) => await _userRepository.GetTableNoTracking().AnyAsync(x => x.UserName!.Equals(username) & !(x.Id.Equals(id))); // !& x.UserId.Equals(id) => && x.UserId != id


		public async Task<User> EditAsync(User userMapper) => await _userRepository.UpdateAsync(userMapper);



		public async Task<bool> CustomDeleteAsync(User user)
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
				Log.Error($"Error In Custom Delete User Async: {ex.Message}");
				return false;
			}

		}

		public async Task<User?> GetByIdAsync(int id) => await _userRepository.GetByIdAsync(id);



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


		public async Task<List<ViewUserEventEngagementSummary>> GetViewUserEventEngagementSummaryAsync()
		{
			var viewUserEventEngagementSummaries = await _viewUserEventEngagementSummaryRepository.GetTableNoTracking()
			.ToListAsync();
			return viewUserEventEngagementSummaries;
		}

		public async Task<SP_GetUserEventEngagementDetails> GetUserEventEngagementDetailsAsync(SP_GetUserEventEngagementDetailsParameters parameters)
		{
			return await _sP_GetUserEventEngagementDetailsRepository.GetUserEventEngagementDetailsAsync(parameters);
		}
		#endregion
	}
}
