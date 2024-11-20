using EventManagement.Core.Bases;
using MediatR;

namespace EventManagement.Core.Features.Speakers.Commands.Models
{
	public record DeleteSpeakerCommand(int SpeakerId):IRequest<Response<string>>;
	
}
