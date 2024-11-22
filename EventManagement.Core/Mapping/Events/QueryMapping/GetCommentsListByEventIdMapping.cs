using EventManagement.Core.Features.Events.Queries.Responses;
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
		private void GetCommentsListByEventIdMapping()
		{
			CreateMap<Comment, GetCommentsListByEventIdResponse>()
				.ForMember(x => x.Creator, opt => opt.MapFrom(x => x.User.UserName));
		}
	}
}
