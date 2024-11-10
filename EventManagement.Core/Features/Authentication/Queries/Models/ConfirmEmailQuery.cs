using EventManagement.Core.Bases;
using MediatR;

namespace EventManagement.Core.Features.Authentication.Queries.Models
{
	public record ConfirmEmailQuery(int UserId,string Code):IRequest<Response<string>>;
	
}
