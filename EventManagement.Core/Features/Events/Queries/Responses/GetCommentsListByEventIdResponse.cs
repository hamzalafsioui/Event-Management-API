namespace EventManagement.Core.Features.Events.Queries.Responses
{
	public class GetCommentsListByEventIdResponse
	{
		public int CommentId { get; set; }
		public string Content { get; set; }
		public string Creator { get; set; }
	}
}
