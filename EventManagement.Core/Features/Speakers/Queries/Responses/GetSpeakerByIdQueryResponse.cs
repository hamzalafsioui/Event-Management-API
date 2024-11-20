namespace EventManagement.Core.Features.Speakers.Queries.Responses
{
	public class GetSpeakerByIdQueryResponse
	{
		public int SpeakerId { get; set; }
		public string FullName { get; set; }
		public string Bio { get; set; }
	}
}
