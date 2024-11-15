using EventManagement.Data.Entities.Views;
using EventManagement.Infrustructure.Abstracts.IViewRepository;
using EventManagement.Infrustructure.Context;
using EventManagement.Infrustructure.InfrustructureBase;
using Microsoft.EntityFrameworkCore;

namespace EventManagement.Infrustructure.Repositories.Views
{
	public class ViewUserEventEngagementSummaryRepository : GenericRepositoryAsync<ViewUserEventEngagementSummary>,IViewRepository<ViewUserEventEngagementSummary>
	{
        #region Fields
        private readonly DbSet<ViewUserEventEngagementSummary> _viewUserEventEngagementSummary;
        #endregion
        #region Constructors
        public ViewUserEventEngagementSummaryRepository(AppDbContext appDbContext):base(appDbContext) 
        {
            _viewUserEventEngagementSummary = appDbContext.Set<ViewUserEventEngagementSummary>();
        }
       
        #endregion
        #region Handle Functions

        #endregion
    }
}
