namespace EventManagement.Data.Entities
{
	public class Comment
	{
		public int CommentId { get; set; }
		public int EventId { get; set; }
		public int UserId { get; set; }
		public string Content { get; set; }
		public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
		public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

		public Event Event { get; set; }
		public User User { get; set; }
	}



}
