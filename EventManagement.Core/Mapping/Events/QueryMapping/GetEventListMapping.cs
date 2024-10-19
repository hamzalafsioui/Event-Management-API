using EventManagement.Core.Features.Events.Queries.Response;
using EventManagement.Data.Entities;

namespace EventManagement.Core.Mapping.Events
{
	public partial class EventProfile
	{
		private void GetEventListMapping()
		{
			CreateMap<Event, GetEventListResponse>()
				.ForMember(x => x.CategoryName, opt => opt.MapFrom(src => src.Category.Name))
				.ForMember(x => x.CreatedBy, opt => opt.MapFrom(src => src.Creator.Username));

		}
	}
}
