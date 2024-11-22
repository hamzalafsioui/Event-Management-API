using EventManagement.Core.Features.Events.Queries.Response;
using EventManagement.Data.Entities;

namespace EventManagement.Core.Mapping.Events
{
	public partial class EventProfile
	{
		private void GetEventListMapping()
		{
			CreateMap<Event, GetEventListResponse>()
				.ForMember(x => x.SpeakersList, opt => opt.MapFrom(src => src.SpeakerEvents))
				.ForMember(x => x.CategoryName, opt => opt.MapFrom(src => src.Category.Name))
				.ForMember(x => x.CreatedBy, opt => opt.MapFrom(src => src.Creator.UserName));

			CreateMap<SpeakerEvent, SpeakerResponse>()
				.ForMember(dest => dest.SpeakerName, opt => opt.MapFrom(src => $"{src.Speaker.User.FirstName} {src.Speaker.User.LastName}"))
				.ForMember(dest => dest.Bio, opt => opt.MapFrom(src => src.Speaker.Bio));

		}
	}
}
