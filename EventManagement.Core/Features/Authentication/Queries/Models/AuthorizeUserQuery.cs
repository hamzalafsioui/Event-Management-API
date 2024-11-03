using EventManagement.Core.Bases;
using MediatR;

namespace EventManagement.Core.Features.Authentication.Queries.Models
{
	public record AuthorizeUserQuery(string AccessToken):IRequest<Response<string>>;
	
}
