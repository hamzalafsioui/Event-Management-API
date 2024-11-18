using EventManagement.Core.Bases;
using MediatR;

namespace EventManagement.Core.Features.Authentication.Commands.Models
{
	public record SendConfirmEmailCommand(string email) : IRequest<Response<string>>;

}
