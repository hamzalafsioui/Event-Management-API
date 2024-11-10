using EventManagement.Core.Bases;
using MediatR;

namespace EventManagement.Core.Features.Emails.Commands.Models
{
	public record SendEmailCommand(string Email,string Message):IRequest<Response<string>>;
	
}
