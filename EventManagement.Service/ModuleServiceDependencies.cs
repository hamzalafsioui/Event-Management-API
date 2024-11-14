using EventManagement.Service.Abstracts;
using EventManagement.Service.AuthService.Implementations;
using EventManagement.Service.AuthService.Interfaces;
using EventManagement.Service.Implementations;
using Microsoft.Extensions.DependencyInjection;

namespace EventManagement.Service
{
	public static class ModuleServiceDependencies
	{
		public static IServiceCollection AddServiceDependencies(this IServiceCollection services)
		{
			services.AddTransient<IUserService, UserService>();
			services.AddTransient<IEventService, EventService>();
			services.AddTransient<IAttendeeService, AttendeeService>();
			services.AddTransient<ICommentService, CommentService>();
			services.AddTransient<ICategoryService, CategoryService>();
			services.AddTransient<IAuthenticationService, AuthenticationService>();
			services.AddTransient<IAuthorizationService, AuthorizationService>();
			services.AddTransient<IEmailService, EmailService>();
			services.AddTransient<ICurrentUserService, CurrentUserService>();
			services.AddTransient<IFileService, FileService>();
			return services;
		}
	}
}
