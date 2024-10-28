using EventManagement.Core.Bases;
using MediatR;

namespace EventManagement.Core.Features.Attendees.Command.Models
{
	public record EditAttendeeCommand(int UserId, int EventId, string Status,bool HasAttended) : IRequest<Response<string>>;


}
