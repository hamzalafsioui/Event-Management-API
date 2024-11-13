using EventManagement.Core.Features.Users.Queries.Results;
using EventManagement.Data.Entities.Views;

namespace EventManagement.Core.Mapping.Users
{
	public partial class UserProfile
	{
		private void GetUserEventEngagementSummaryMapping()
		{
			CreateMap<ViewUserEventEngagementSummary, GetUserEventEngagementSummaryResponse>()
			   .ForMember(destination => destination.LastCategoryAttended, option => option.MapFrom(source => source.LastCategoryAttended ?? "N/A"))
			   .ForMember(destination => destination.LastEventAttendedDate, option => option.MapFrom(source => source.LastEventAttendedDate ?? DateTime.MinValue));
		}
	}
}
