using EventManagement.Core.Bases;
using MediatR;

namespace EventManagement.Core.Features.Authentication.Commands.Models
{
	public record ResetPasswordCommand(string Email, string Password, string ConfirmPassword) : IRequest<Response<string>>;



}
