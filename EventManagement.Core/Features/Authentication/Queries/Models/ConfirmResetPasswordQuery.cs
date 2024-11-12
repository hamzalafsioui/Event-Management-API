using EventManagement.Core.Bases;
using MediatR;

namespace EventManagement.Core.Features.Authentication.Queries.Models
{
	public record ConfirmResetPasswordQuery(string Email, string Code) : IRequest<Response<string>>;

}
