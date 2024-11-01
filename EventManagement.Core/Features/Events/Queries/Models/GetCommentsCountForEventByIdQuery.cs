using EventManagement.Core.Bases;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement.Core.Features.Events.Queries.Models
{
	public record GetCommentsCountForEventByIdQuery(int eventId):IRequest<Response<string>>;
	
}
