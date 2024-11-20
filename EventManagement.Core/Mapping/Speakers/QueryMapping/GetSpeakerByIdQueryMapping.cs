using EventManagement.Core.Features.Speakers.Queries.Responses;
using EventManagement.Data.Entities;

namespace EventManagement.Core.Mapping.Speakers
{
	public partial class SpeakerProfile
	{
		private void GetSpeakerByIdQueryMapping()
		{
			CreateMap<Speaker, GetSpeakerByIdQueryResponse>()
				.ForMember(x => x.FullName, opt => opt.MapFrom(x => $"{x.User.FirstName} {x.User.LastName}"))
				.ForMember(x => x.SpeakerId, opt => opt.MapFrom(x => x.Id));
		}
	}
}
