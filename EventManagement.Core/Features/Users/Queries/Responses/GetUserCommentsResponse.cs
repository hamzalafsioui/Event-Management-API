﻿namespace EventManagement.Core.Features.Users.Queries.Responses
{
	public class GetUserCommentsResponse
	{
		public int EventId { get; set; }
		public string EventTitle { get; set; }
		public string CommentText { get; set; }
	}
}