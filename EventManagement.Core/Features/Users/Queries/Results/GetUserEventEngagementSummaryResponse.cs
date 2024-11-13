﻿namespace EventManagement.Core.Features.Users.Queries.Results
{
	public class GetUserEventEngagementSummaryResponse
	{
		public int UserId { get; set; }

		public string FirstName { get; set; }

		public string LastName { get; set; }

		public string Role { get; set; }

		public string Email { get; set; }

		public int TotalEventsAttended { get; set; }

		public int TotalCommentsMade { get; set; }

		public int TotalEventsCreated { get; set; }

		public int TotalEventsPhysicallyAttended { get; set; }

		public int TotalEventsRelated { get; set; }

		public string LastCategoryAttended { get; set; }

		public DateTime? LastEventAttendedDate { get; set; }

	}
}
