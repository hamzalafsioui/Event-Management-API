using EventManagement.Core.Features.Attendees.Command.Models;
using EventManagement.Data.Entities;

namespace EventManagement.Core.Mapping.Attendees
{
	public partial class AttendeeProfile
	{
		private void AddAttendeeCommandMapping()
		{
			CreateMap<AddAttendeeCommand, Attendee>()
				.ForMember(dest => dest.Status, opt => opt.MapFrom(src => ParseStatus(src.Status)));


		}
		private RSVPStatus ParseStatus(string status)
		{
			return Enum.TryParse<RSVPStatus>(status, true, out var parsedStatus) ? parsedStatus : RSVPStatus.Pending;
		}
	}
}
