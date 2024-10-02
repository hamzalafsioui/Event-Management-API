using EventManagement.Core.Bases;
using EventManagement.Core.Features.Users.Queries.Results;
using MediatR;

namespace EventManagement.Core.Features.Users.Queries.Models
{
	public class GetUserByIdQuery : IRequest<Response<GetSingleUserResponse>>
	{
		public int Id { get; set; }
        public GetUserByIdQuery(int id)
        {
            Id = id;
        }

    }
}
