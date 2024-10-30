using EventManagement.Core.Bases;
using MediatR;

namespace EventManagement.Core.Features.Attendees.Command.Models
{
	public record MarkAttendanceCommand(int eventId, int userId) : IRequest<Response<string>>;

}
