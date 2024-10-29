using EventManagement.Core.Features.Events.Queries.Response;
using EventManagement.Data.Entities;

namespace EventManagement.Core.Mapping.Events
{
	public partial class EventProfile
	{
		private void GetEventAttendeesListMapping()
		{
			CreateMap<Event, GetEventAttendeesResponse>()
				.ForMember(x => x.Id, opt => opt.MapFrom(x => x.Creator.Id))
				.ForMember(x => x.UserName, opt => opt.MapFrom(x => x.Creator.UserName));

		}
	}
}
