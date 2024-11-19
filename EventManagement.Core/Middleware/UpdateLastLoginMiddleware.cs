using EventManagement.Data.Entities.Identity;
using EventManagement.Data.Helper.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Serilog;
using System.Security.Claims;

namespace EventManagement.Core.Middleware
{
	public class UpdateLastLoginMiddleware
	{
		#region Fields
		private readonly RequestDelegate _next;
		#endregion
		#region Constructors
		public UpdateLastLoginMiddleware(RequestDelegate next)
		{
			_next = next;
		}
		#endregion
		#region Action
		public async Task InvokeAsync(HttpContext context,UserManager<User> userManager)
		{
			if(context.User.Identity?.IsAuthenticated == true)
			{
				try
				{
					var userId = context.User.FindFirst(x => x.Type == nameof(UserClaimModel.Id))?.Value;
					if (int.TryParse(userId, out var id))
					{
						var user = await userManager.FindByIdAsync(id.ToString());
						if (user != null)
						{
							user.LastLoginDate = DateTime.UtcNow;
							await userManager.UpdateAsync(user);
						}
					}
				}catch(Exception ex)
				{
					Log.Error($"Error In UpdateLastLoginMiddleware: {ex.Message}");
				}
				
			}
			await _next(context);

		}

		#endregion
	}
}
