using EventManagement.Core.Bases;
using EventManagement.Data.Entities;
using MediatR;

namespace EventManagement.Core.Features.Attendees.Command.Models
{
	public record AddAttendeeCommand(int UserId, int EventId, string Status) : IRequest<Response<string>>;


}
