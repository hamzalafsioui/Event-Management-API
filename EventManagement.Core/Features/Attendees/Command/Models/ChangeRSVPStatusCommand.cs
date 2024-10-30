using EventManagement.Core.Bases;
using MediatR;

namespace EventManagement.Core.Features.Attendees.Command.Models
{
	public record ChangeRSVPStatusCommand(int eventId, int userId, string status) : IRequest<Response<string>>;

}
