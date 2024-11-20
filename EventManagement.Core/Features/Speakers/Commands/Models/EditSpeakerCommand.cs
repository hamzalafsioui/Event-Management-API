using EventManagement.Core.Bases;
using MediatR;

namespace EventManagement.Core.Features.Speakers.Commands.Models
{
	public record EditSpeakerCommand(int SpeakerId, string Bio) : IRequest<Response<string>>;


}
