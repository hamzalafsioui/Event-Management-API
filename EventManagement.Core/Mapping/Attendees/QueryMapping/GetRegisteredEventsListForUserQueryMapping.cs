using EventManagement.Core.Features.Attendees.Queries.Responses;
using EventManagement.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement.Core.Mapping.Attendees
{
	public partial class AttendeeProfile
	{
		private void GetRegisteredEventsListForUserQueryMapping()
		{
			CreateMap<Attendee, GetRegisteredEventsListForUserResponse>()
				.ForMember(x => x.Id, opt => opt.MapFrom(x => x.EventId))
				.ForMember(x => x.Name, opt => opt.MapFrom(x => x.Event.Title))
				.ForMember(x => x.StartTime, opt => opt.MapFrom(x => x.Event.StartTime))
				.ForMember(x => x.EndTime, opt => opt.MapFrom(x => x.Event.EndTime));
				
		}
	}
}
