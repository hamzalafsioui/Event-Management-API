using AutoMapper;
using EventManagement.Core.Features.Events.Commands.Models;
using EventManagement.Data.Entities;

namespace EventManagement.Core.Mapping.Events
{
	public partial class EventProfile : Profile
	{
		private void AddEventCommandMapping()
		{
			CreateMap<AddEventCommand, Event>();
		}
	}
}
