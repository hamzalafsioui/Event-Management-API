using EventManagement.Core.Bases;
using EventManagement.Data.Helper.Authentication;
using MediatR;

namespace EventManagement.Core.Features.Authentication.Commands.Models
{
	public record SignInCommand(string UserName, string Password) : IRequest<Response<JwtAuthResponse>>;

}
