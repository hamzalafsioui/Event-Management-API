namespace EventManagement.Core.Features.Events.Queries.Response
{
	public class GetEventPaginatedListResponse
	{


		public int EventId { get; set; }
		public string Title { get; set; }
		public string Description { get; set; } = string.Empty;
		public string Location { get; set; }
		public DateTime StartTime { get; set; }
		public DateTime EndTime { get; set; }
		public string CategoryName { get; set; }
		public string CreatedBy { get; set; }
		public int Capacity { get; set; }

		public DateTime CreatedAt { get; set; }
		public IEnumerable<SpeakerResponse> Speakers { get; set; }

		public GetEventPaginatedListResponse(int eventId, string title, string? description, string location, DateTime startTime,
			DateTime endTime, string categoryName, string createdBy, int capacity, DateTime createdAt, IEnumerable<SpeakerResponse> speakers)
		{
			EventId = eventId;
			Title = title;
			Description = description;
			Location = location;
			StartTime = startTime;
			EndTime = endTime;
			CategoryName = categoryName;
			CreatedBy = createdBy;
			Capacity = capacity;
			CreatedAt = createdAt;
			Speakers = speakers;
		}
	}
}
