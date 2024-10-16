using EventManagement.Infrustructure.Abstracts;
using EventManagement.Infrustructure.InfrustructureBase;
using EventManagement.Infrustructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace EventManagement.Infrustructure
{
	public static class ModuleInfrustructureDependencies
	{
		public static IServiceCollection AddInfrustructureDependencies(this IServiceCollection services)
		{
			services.AddTransient<IUserRepository, UserRepository>();
			services.AddTransient(typeof(IGenericRepositoryAsync<>), typeof(GenericRepositoryAsync<>));

			services.AddTransient<IEventRepository, EventRepository>();
			services.AddTransient<IAttendeeRepository, AttendeeRepository>();
			services.AddTransient<ICommentRepository, CommentRepository>();
			services.AddTransient<ICategoryRepository, CategoryRepository>();

			return services;
		}
	}
}
