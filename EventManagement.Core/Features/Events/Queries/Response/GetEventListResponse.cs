namespace EventManagement.Core.Features.Events.Queries.Response
{
	public class GetEventListResponse
	{
		public int EventId { get; set; }
		public required string Title { get; set; }
		public string? Description { get; set; } = string.Empty;
		public required string Location { get; set; }
		public required DateTime StartTime { get; set; }
		public required DateTime EndTime { get; set; }
		public required string CategoryName { get; set; }
		public required string CreatedBy { get; set; }
		public required int Capacity { get; set; }

		public DateTime CreatedAt { get; set; }
	}
}
