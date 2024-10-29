using EventManagement.Core.Bases;
using MediatR;

namespace EventManagement.Core.Features.Events.Commands.Models
{
	public record CancelEventCommand(int EventId) : IRequest<Response<string>>;

}
