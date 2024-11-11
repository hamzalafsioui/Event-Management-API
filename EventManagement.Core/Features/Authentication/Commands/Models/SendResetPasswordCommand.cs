using EventManagement.Core.Bases;
using MediatR;

namespace EventManagement.Core.Features.Authentication.Commands.Models
{
	public record SendResetPasswordCommand(string Email) : IRequest<Response<string>>;

}
