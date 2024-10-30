using AutoMapper;

namespace EventManagement.Core.Mapping.Attendees
{
	public partial class AttendeeProfile : Profile
	{
		public AttendeeProfile()
		{
			AddAttendeeCommandMapping();
			EditAttendeeCommandMapping();
			GetRegisteredEventsListForUserQueryMapping();
		}
	}
}
