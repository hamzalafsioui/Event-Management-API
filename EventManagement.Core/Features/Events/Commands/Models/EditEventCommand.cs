using EventManagement.Core.Bases;
using MediatR;

namespace EventManagement.Core.Features.Events.Commands.Models
{
	public record EditEventCommand(int Id, string Title, string? Description, string Location,
		DateTime StartTime, DateTime EndTime, int CategoryId, int CreatorId, int Capacity) : IRequest<Response<string>>;

}
