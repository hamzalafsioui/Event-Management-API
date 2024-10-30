using EventManagement.Core.Bases;
using MediatR;

namespace EventManagement.Core.Features.Attendees.Command.Models
{
	public record LeaveEventCommand(int EventId,int UserId):IRequest<Response<string>>;
	
}
