using EventManagement.Data.Entities.SPs;
using EventManagement.Infrustructure.Abstracts.SPs;
using EventManagement.Infrustructure.Context;
using StoredProcedureEFCore;

namespace EventManagement.Infrustructure.Repositories.SPs
{
	public class SP_GetUserEventEngagementDetailsRepository : ISP_GetUserEventEngagementDetailsRepository
	{
		#region Fields
		private readonly AppDbContext _appDbContext;
		#endregion
		#region Constructors
		public SP_GetUserEventEngagementDetailsRepository(AppDbContext appDbContext)
		{
			_appDbContext = appDbContext;
		}
		#endregion
		#region Handle Functions

		#endregion
		public async Task<SP_GetUserEventEngagementDetails> GetUserEventEngagementDetailsAsync(SP_GetUserEventEngagementDetailsParameters parameters)
		{
			var result = new SP_GetUserEventEngagementDetails();
			await _appDbContext.LoadStoredProc(nameof(SP_GetUserEventEngagementDetails))
			   .AddParam(nameof(SP_GetUserEventEngagementDetails.UserId), parameters.UserId)
			   .ExecAsync(async reader=>
			   {
				   result = await reader.FirstOrDefaultAsync<SP_GetUserEventEngagementDetails>();
			} );
			return result;
		}
	}
}
