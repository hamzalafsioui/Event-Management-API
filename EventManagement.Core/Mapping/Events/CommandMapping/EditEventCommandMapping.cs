using EventManagement.Core.Features.Events.Commands.Models;
using EventManagement.Data.Entities;

namespace EventManagement.Core.Mapping.Events
{
	public partial class EventProfile
	{
		private void EditEventCommandMapping()
		{
			CreateMap<EditEventCommand, Event>()
				//.ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
				.ForMember(dest => dest.EventId, opt => opt.Ignore());

		}

	}
}
