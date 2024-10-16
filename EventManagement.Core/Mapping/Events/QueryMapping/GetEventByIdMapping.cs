using EventManagement.Core.Features.Events.Queries.Response;
using EventManagement.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement.Core.Mapping.Events
{
	public partial class EventProfile
	{
		public void GetEventByIdMapping()
		{
			CreateMap<Event, GetEventByIdResponse>()
				.ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name))
				.ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.Creator.Username))
				.ForMember(dest => dest.AttendeesList, opt => opt.MapFrom(src => src.Attendees))
				.ForMember(dest => dest.CommentsList, opt => opt.MapFrom(src => src.Comments));

			CreateMap<Attendee, AttendeeResponse>()
				.ForMember(dest => dest.AttendeeId, opt => opt.MapFrom(src => src.AttendeeId))
				.ForMember(dest => dest.AttendeeName, opt => opt.MapFrom(src => src.User.Username));
			CreateMap<Comment, CommentResponse>()
				.ForMember(dest => dest.CommentId, opt => opt.MapFrom(src => src.CommentId))
				.ForMember(dest => dest.WriteBy, opt => opt.MapFrom(src => src.User.Username))
				.ForMember(dest => dest.Content, opt => opt.MapFrom(src => src.Content));



		}
	}
}
 