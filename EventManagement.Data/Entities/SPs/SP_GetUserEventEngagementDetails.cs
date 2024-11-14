namespace EventManagement.Data.Entities.SPs
{
	public class SP_GetUserEventEngagementDetails
	{
		public int UserId { get; set; }
		public string UserName { get; set; }
		public string Role { get; set; }
		public int TotalEventsAttended { get; set; }
		public int TotalCommentsMade { get; set; }
		public int TotalEventsCreated { get; set; }
		public int TotalEventsPhysicallyAttended { get; set; }
		public int TotalEventsRelated { get; set; }
		public string? LastCategoryAttended { get; set; }
		public DateTime? LastEventAttendedDate { get; set; }
	}
	public class SP_GetUserEventEngagementDetailsParameters
	{
		public int UserId { get; set; } = 0; // Initial Value
	}

}
