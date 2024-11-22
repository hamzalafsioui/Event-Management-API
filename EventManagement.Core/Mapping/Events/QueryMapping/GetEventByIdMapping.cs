using AutoMapper;
using EventManagement.Core.Features.Events.Queries.Response;
using EventManagement.Data.Entities;

namespace EventManagement.Core.Mapping.Events
{
	public partial class EventProfile : Profile
	{

		private void GetEventByIdMapping()
		{

			CreateMap<Event, GetEventByIdResponse>()
				.ForMember(dest => dest.SpeakersList, opt => opt.MapFrom(src => src.SpeakerEvents))
				.ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name))
				.ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.Creator != null ? src.Creator.UserName : "Unknown"));


			CreateMap<SpeakerEvent, SpeakerResponse>()
				.ForMember(dest => dest.SpeakerName, opt => opt.MapFrom(src => $"{src.Speaker.User.FirstName} {src.Speaker.User.LastName}"))
				.ForMember(dest => dest.Bio, opt => opt.MapFrom(src => src.Speaker.Bio));

			//	.ForMember(dest => dest.AttendeesList, opt => opt.MapFrom(src => src.Attendees))
			//.ForMember(dest => dest.CommentsList, opt => opt.MapFrom(src => src.Comments));

			//CreateMap<Attendee, AttendeeResponse>()
			//	.ForMember(dest => dest.AttendeeId, opt => opt.MapFrom(src => src.AttendeeId))
			//	.ForMember(dest => dest.AttendeeName, opt => opt.MapFrom(src => src.User.Username));

			//CreateMap<Comment, CommentResponse>()
			//	.ForMember(dest => dest.CommentId, opt => opt.MapFrom(src => src.CommentId))
			//	.ForMember(dest => dest.WriteBy, opt => opt.MapFrom(src => src.User != null ? src.User.Username : "Anonymous"))
			//	.ForMember(dest => dest.Content, opt => opt.MapFrom(src => src.Content));
		}
	}
}
