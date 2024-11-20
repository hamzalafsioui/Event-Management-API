using EventManagement.Core.Bases;
using MediatR;

namespace EventManagement.Core.Features.Speakers.Commands.Models
{
	public record AddSpeakerCommand(int userId, string Bio) : IRequest<Response<string>>;

}
