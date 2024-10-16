using EventManagement.Core.Bases;
using EventManagement.Core.Features.Events.Queries.Response;
using MediatR;

namespace EventManagement.Core.Features.Events.Queries.Models
{
	public class GetEventByIdQuery:IRequest<Response<GetEventByIdResponse>>
	{
		public int Id { get; set; }
        public GetEventByIdQuery(int id)
        {
			Id = id;
		}
    }
}
