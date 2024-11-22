using EventManagement.Core.Features.Events.Queries.Responses;
using EventManagement.Data.Entities;

namespace EventManagement.Core.Mapping.Events
{
	public partial class EventProfile
	{
		private void GetEventAttendeesListMapping()
		{
			CreateMap<Attendee, GetEventAttendeesResponse>()
				.ForMember(x => x.Id, opt => opt.MapFrom(x => x.User.Id))
				.ForMember(x => x.UserName, opt => opt.MapFrom(x => x.User.UserName));

		}
	}
}
