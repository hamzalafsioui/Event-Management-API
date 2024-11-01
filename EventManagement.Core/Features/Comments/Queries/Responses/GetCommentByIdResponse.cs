namespace EventManagement.Core.Features.Comments.Queries.Responses
{
	public class GetCommentByIdResponse
	{
		public string CommentId { get; set; }
		public string CommentText { get; set; }
		public string creator { get; set; }
	}
}
