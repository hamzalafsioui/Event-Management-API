using EventManagement.Core.Features.Events.Queries.Response;
using EventManagement.Data.Entities;

namespace EventManagement.Core.Mapping.Events
{
	public partial class EventProfile
	{
		private void GetEventsListByCategoryIdQueryMapping()
		{
			CreateMap<Event, GetEventsListByCategoryIdResponse>()
				.ForMember(dest => dest.Speakers, opt => opt.MapFrom(src => src.SpeakerEvents))
				.ForMember(x => x.Id, opt => opt.MapFrom(x => x.EventId))
				.ForMember(x => x.Creator, opt => opt.MapFrom(x => x.Creator.UserName))
				.ForMember(x => x.StartTime, opt => opt.MapFrom(x => x.StartTime))
				.ForMember(x => x.EndTime, opt => opt.MapFrom(x => x.EndTime))
				.ForMember(x => x.Name, opt => opt.MapFrom(x => x.Title));

			CreateMap<SpeakerEvent, SpeakerResponse>()
				.ForMember(dest => dest.SpeakerName, opt => opt.MapFrom(src => $"{src.Speaker.User.FirstName} {src.Speaker.User.LastName}"))
				.ForMember(dest => dest.Bio, opt => opt.MapFrom(src => src.Speaker.Bio));

		}
	}
}
