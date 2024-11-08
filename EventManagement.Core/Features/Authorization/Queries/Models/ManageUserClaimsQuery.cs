using EventManagement.Core.Bases;
using EventManagement.Data.Responses;
using MediatR;

namespace EventManagement.Core.Features.Authorization.Queries.Models
{
	public record ManageUserClaimsQuery(int UserId):IRequest<Response<ManageUserClaimsResponse>>;
	
}
