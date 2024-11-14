using EventManagement.Data.Entities.SPs;

namespace EventManagement.Infrustructure.Abstracts.SPs
{
	public interface ISP_GetUserEventEngagementDetailsRepository
	{
		public Task<SP_GetUserEventEngagementDetails> GetUserEventEngagementDetailsAsync(SP_GetUserEventEngagementDetailsParameters parameters);
	}
}
