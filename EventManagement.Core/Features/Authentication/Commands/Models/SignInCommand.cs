using EventManagement.Core.Bases;
using MediatR;

namespace EventManagement.Core.Features.Authentication.Commands.Models
{
	public record SignInCommand(string UserName, string Password):IRequest<Response<string>>;
	
}
