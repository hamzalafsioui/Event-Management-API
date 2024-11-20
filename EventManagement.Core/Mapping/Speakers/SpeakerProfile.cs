using AutoMapper;

namespace EventManagement.Core.Mapping.Speakers
{
	public partial class SpeakerProfile : Profile
	{
		public SpeakerProfile()
		{
			AddSpeakerCommandMapping();
			GetSpeakerListQueryMapping();
			GetSpeakerByIdQueryMapping();
		}
	}
}
