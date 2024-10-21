using EventManagement.Data.Entities.Identity;
using EventManagement.Infrustructure.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace EventManagement.Infrustructure
{
	public static class ServiceRegistration
	{
		public static IServiceCollection AddServiceRegistration(this IServiceCollection services)
		{
			services.AddIdentity<User, IdentityRole<int>>(opt =>
			{
				// Password settings.
				opt.Password.RequireDigit = false;
				opt.Password.RequireLowercase = false;
				opt.Password.RequireNonAlphanumeric = false;
				opt.Password.RequireUppercase = false;
				opt.Password.RequiredLength = 3;
				opt.Password.RequiredUniqueChars = 1;

				// Lockout settings.
				opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
				opt.Lockout.MaxFailedAccessAttempts = 5;
				opt.Lockout.AllowedForNewUsers = true;

				//  settings.
				opt.User.AllowedUserNameCharacters =
				"abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
				opt.User.RequireUniqueEmail = true;
				opt.SignIn.RequireConfirmedEmail = false;
			}).AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();
			return services;
		}
	}
}
