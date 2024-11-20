using EventManagement.Core.Features.Speakers.Commands.Models;
using EventManagement.Data.Entities;

namespace EventManagement.Core.Mapping.Speakers
{
	public partial class SpeakerProfile
	{
		private void AddSpeakerCommandMapping()
		{
			CreateMap<AddSpeakerCommand, Speaker>();
		}
	}
}
