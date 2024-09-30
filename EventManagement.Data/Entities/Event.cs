 namespace EventManagement.Data.Entities
{
	public class Event
	{


		public int EventId { get; set; }
		public string Title { get; set; }
		public string? Description { get; set; }
		public string Location { get; set; }
		public DateTime StartTime { get; set; }
		public DateTime EndTime { get; set; }
		public int CategoryId { get; set; }
		public int CreatedBy { get; set; }
		public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
		public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

		public Category Category { get; set; }
		public User Creator { get; set; }
		public ICollection<Attendee> Attendees { get; set; } = new List<Attendee>();
		public ICollection<Comment> Comments { get; set; } = new List<Comment>();

		public Event(int eventId, string title, string? description, string location, DateTime startTime, DateTime endTime, int categoryId, int createdBy, DateTime createdAt, DateTime updatedAt)
		{
			EventId = eventId;
			Title = title;
			Description = description;
			Location = location;
			StartTime = startTime;
			EndTime = endTime;
			CategoryId = categoryId;
			CreatedBy = createdBy;
			CreatedAt = createdAt;
			UpdatedAt = updatedAt;
		}
		public Event()
		{

		}
	}



}
