using EventManagement.Data.Entities;
using EventManagement.Infrustructure.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace EventManagement.Infrustructure
{
	public static class ServiceRegistration
	{
		public static IServiceCollection AddServiceRegistration(this IServiceCollection services)
		{
			services.AddIdentity<User, IdentityRole<int>>(opt =>
			{
				// Password settings.
				opt.Password.RequireDigit = true;
				opt.Password.RequireLowercase = true;
				opt.Password.RequireNonAlphanumeric = true;
				opt.Password.RequireUppercase = true;
				opt.Password.RequiredLength = 6;
				opt.Password.RequiredUniqueChars = 1;

				// Lockout settings.
				opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
				opt.Lockout.MaxFailedAccessAttempts = 5;
				opt.Lockout.AllowedForNewUsers = true;

				//  settings.
				opt.User.AllowedUserNameCharacters =
				"abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
				opt.User.RequireUniqueEmail = false;
			}).AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();
			return services;
		}
	}
}
