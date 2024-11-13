using EventManagement.Data.Entities.Views;
using EventManagement.Infrustructure.Abstracts;
using EventManagement.Infrustructure.Abstracts.IViewRepository;
using EventManagement.Infrustructure.InfrustructureBase;
using EventManagement.Infrustructure.Repositories;
using EventManagement.Infrustructure.Repositories.Views;
using Microsoft.Extensions.DependencyInjection;

namespace EventManagement.Infrustructure
{
	public static class ModuleInfrustructureDependencies
	{
		public static IServiceCollection AddInfrustructureDependencies(this IServiceCollection services)
		{
			services.AddTransient(typeof(IGenericRepositoryAsync<>), typeof(GenericRepositoryAsync<>));

			services.AddTransient<IUserRepository, UserRepository>();
			services.AddTransient<IEventRepository, EventRepository>();
			services.AddTransient<IAttendeeRepository, AttendeeRepository>();
			services.AddTransient<ICommentRepository, CommentRepository>();
			services.AddTransient<ICategoryRepository, CategoryRepository>();
			services.AddTransient<IRefreshTokenRepository, RefreshTokenRepository>();
			services.AddTransient<IViewRepository<ViewUserEventEngagementSummary>, ViewUserEventEngagementSummaryRepository>();

			return services;
		}
	}
}
