using EventManagement.Core.Features.Attendees.Command.Models;
using EventManagement.Data.Entities;

namespace EventManagement.Core.Mapping.Attendees
{
	public partial class AttendeeProfile
	{
		private void EditAttendeeCommandMapping()
		{
			CreateMap<EditAttendeeCommand, Attendee>()
				.ForMember(dest => dest.Status, opt => opt.MapFrom(src => ParseStatus(src.Status)));
		}
	}
}
