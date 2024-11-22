using EventManagement.Core.Wrappers;

namespace EventManagement.Core.Features.Events.Queries.Responses
{
	public class GetEventByIdResponse
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
		public List<SpeakerResponse>? SpeakersList { get; set; }

		public PaginatedResult<AttendeeResponse>? AttendeesList { get; set; }
		public PaginatedResult<CommentResponse>? CommentsList { get; set; }

	}
	public class SpeakerResponse
	{

		public string SpeakerName { get; set; }
		public string Bio { get; set; }

	}
	public class AttendeeResponse
	{

		public string AttendeeName { get; set; }
		public AttendeeResponse(string attendeeName)
		{
			AttendeeName = attendeeName;
		}
	}
	public class CommentResponse
	{

		public int CommentId { get; set; }
		public string WriteBy { get; set; }
		public string Content { get; set; }
		public CommentResponse(int commentId, string writeBy, string content)
		{
			CommentId = commentId;
			WriteBy = writeBy;
			Content = content;
		}

	}
}
