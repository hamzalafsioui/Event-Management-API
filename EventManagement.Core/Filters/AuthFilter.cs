using EventManagement.Data.Entities.Identity;
using EventManagement.Service.AuthService.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace EventManagement.Core.Filters
{
	public class AuthFilter : IAsyncActionFilter
	{
		private readonly ICurrentUserService _currentUserService;
		private readonly UserManager<User> _userManager;
        public AuthFilter(ICurrentUserService currentUserService,UserManager<User> userManager)
        {
			_currentUserService = currentUserService;
			_userManager = userManager;
		}
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
		{
			if(context.HttpContext?.User?.Identity?.IsAuthenticated == true)
			{
				var roles = await _currentUserService.GetCurrentUserRolesAsync();
                if (!roles.Any(x=>x == "User"))
                {
					context.Result = new ObjectResult("Forbidden")
					{
						StatusCode = StatusCodes.Status403Forbidden,
					};
                }
				else
				{
					await next() ;
				}
			}
			
		}

		// override methods
	}
}
