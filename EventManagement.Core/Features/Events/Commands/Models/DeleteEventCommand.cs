using EventManagement.Core.Bases;
using MediatR;

namespace EventManagement.Core.Features.Events.Commands.Models
{
	public record DeleteEventCommand(int EventId) : IRequest<Response<string>>;

}
