using EventManagement.Data.Entities.Views;
using EventManagement.Infrustructure.Abstracts;
using EventManagement.Infrustructure.Abstracts.IViewRepository;
using EventManagement.Infrustructure.Abstracts.SPs;
using EventManagement.Infrustructure.InfrustructureBase;
using EventManagement.Infrustructure.Repositories;
using EventManagement.Infrustructure.Repositories.SPs;
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
			services.AddTransient<ISpeakerRepository, SpeakerRepository>();
			services.AddTransient<IRefreshTokenRepository, RefreshTokenRepository>();

			// views
			services.AddTransient<IViewRepository<ViewUserEventEngagementSummary>, ViewUserEventEngagementSummaryRepository>();

			// SPs
			services.AddTransient<ISP_GetUserEventEngagementDetailsRepository, SP_GetUserEventEngagementDetailsRepository>();
			return services;
		}
	}
}
