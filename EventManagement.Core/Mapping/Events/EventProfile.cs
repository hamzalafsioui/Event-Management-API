using AutoMapper;

namespace EventManagement.Core.Mapping.Events
{
	public partial class EventProfile : Profile
	{
		public EventProfile()
		{
			GetEventByIdMapping();
			AddEventCommandMapping();
			GetEventListMapping();
		}
	}
}
