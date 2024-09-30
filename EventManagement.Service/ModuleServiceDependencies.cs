using EventManagement.Service.Abstracts;
using EventManagement.Service.Implementations;
using Microsoft.Extensions.DependencyInjection;

namespace EventManagement.Service
{
	public static class ModuleServiceDependencies
	{
		public static IServiceCollection AddServiceDependencies(this IServiceCollection services)
		{
			services.AddTransient<IUserService, UserService>();
			return services;
		}
	}
}
