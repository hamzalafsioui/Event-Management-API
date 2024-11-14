using EventManagement.Core.Features.Users.Queries.Models;
using EventManagement.Core.Features.Users.Queries.Results;
using EventManagement.Data.Entities.SPs;

namespace EventManagement.Core.Mapping.Users
{
	public partial class UserProfile
	{
		private void GetUserEventEngagementDetailsQueryMapping()
		{
			CreateMap<GetUserEventEngagementDetailsQuery, SP_GetUserEventEngagementDetailsParameters>()
				;
			CreateMap<SP_GetUserEventEngagementDetails, GetUserEventEngagementDetailsResponse>()
				 .ForMember(destination => destination.LastCategoryAttended, option => option.MapFrom(source => source.LastCategoryAttended ?? "N/A"))
			   .ForMember(destination => destination.LastEventAttendedDate, option => option.MapFrom(source => source.LastEventAttendedDate ?? DateTime.MinValue));
			;
		}
	}
}
