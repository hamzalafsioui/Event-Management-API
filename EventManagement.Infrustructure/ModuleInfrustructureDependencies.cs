using EventManagement.Infrustructure.Abstracts;
using EventManagement.Infrustructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace EventManagement.Infrustructure
{
	public static class ModuleInfrustructureDependencies
	{
		public static IServiceCollection AddInfrustructureDependencies(this IServiceCollection service)
		{
			 service.AddTransient<IUserRepository, UserRepository>();
			return service;
		}
	}
}
