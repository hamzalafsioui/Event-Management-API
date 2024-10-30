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
		private void GetEventsListByCategoryIdQueryMapping()
		{
			CreateMap<Event, GetEventsListByCategoryIdResponse>()
				.ForMember(x => x.Id, opt => opt.MapFrom(x => x.EventId))
				.ForMember(x => x.Creator, opt => opt.MapFrom(x => x.Creator.UserName))
				.ForMember(x => x.StartTime, opt => opt.MapFrom(x => x.StartTime))
				.ForMember(x => x.EndTime, opt => opt.MapFrom(x => x.EndTime))
				.ForMember(x => x.Name, opt => opt.MapFrom(x => x.Title));
		}
	}
}
