using EventManagement.Data.Entities.Identity;
using EventManagement.Data.Helper.Models;
using EventManagement.Service.AuthService.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace EventManagement.Service.AuthService.Implementations
{
	public class CurrentUserService : ICurrentUserService
	{
		#region Fields
		private readonly IHttpContextAccessor _httpContextAccessor;
		private readonly UserManager<User> _userManager;
        #endregion
        #region Constructors
        public CurrentUserService(IHttpContextAccessor httpContextAccessor,UserManager<User> userManager)
        {
			_httpContextAccessor = httpContextAccessor;
			_userManager = userManager;
		}
		#endregion
		#region Actions
		public int GetUserId()
		{
			var userId = _httpContextAccessor.HttpContext!.User.Claims.SingleOrDefault(claim=>claim.Type == nameof(UserClaimModel.Id));
			if(userId == null)
			{
				throw new UnauthorizedAccessException();
			}
			return int.Parse(userId.Value);
		}
		public async Task<User> GetCurrentUserAsync()
		{
			var userId = GetUserId();
			var user = await _userManager.FindByIdAsync(userId.ToString());
			if(user == null)
				throw new UnauthorizedAccessException();
			return user;
		}

		public async Task<List<string>> GetCurrentUserRolesAsync()
		{
			var user = await GetCurrentUserAsync();
			var roles = await _userManager.GetRolesAsync(user);
			return roles.ToList();
		}


		#endregion

	}
}
