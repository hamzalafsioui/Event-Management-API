using EventManagement.Core.Features.Events.Queries.Response;
using EventManagement.Data.Entities;

namespace EventManagement.Core.Mapping.Events
{
	public partial class EventProfile
	{
		private void GetUpcomingEventListQueryMapping()
		{
			CreateMap<Event, GetUpcomingEventsListResponse>()
				.ForMember(x => x.Id, opt => opt.MapFrom(x => x.EventId))
				.ForMember(x => x.Creator, opt => opt.MapFrom(x => x.Creator.UserName))
				.ForMember(x => x.StartTime, opt => opt.MapFrom(x => x.StartTime))
				.ForMember(x => x.EndTime, opt => opt.MapFrom(x => x.EndTime))
				.ForMember(x => x.Name, opt => opt.MapFrom(x => x.Title));
		}
	}
}
