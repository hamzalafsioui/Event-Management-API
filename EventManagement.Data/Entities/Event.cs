using EventManagement.Data.Abstracts;
using EventManagement.Data.Entities.Identity;

namespace EventManagement.Data.Entities
{
	public class Event : IHasCreatedAt, IHasUpdatedAt
	{
		public int EventId { get; set; }
		public required string Title { get; set; }
		public string? Description { get; set; } = string.Empty;
		public required string Location { get; set; }
		public required DateTime StartTime { get; set; } // validate always StartTime before EndTime
		public required DateTime EndTime { get; set; }
		public required int CategoryId { get; set; }
		public required int CreatorId { get; set; }
		public required int Capacity { get; set; }
		public EventStatus Status { get; set; } = EventStatus.Actived;

		public DateTime CreatedAt { get; set; }
		public DateTime UpdatedAt { get; set; }

		public Category Category { get; set; }
		public User Creator { get; set; }
		public virtual ICollection<Attendee> Attendees { get; set; } = new HashSet<Attendee>();
		public virtual ICollection<Comment> Comments { get; set; } = new HashSet<Comment>();

		public Event(int eventId, string title, string? description, string location, DateTime startTime, DateTime endTime, int categoryId, int createdBy, int capacity)
		{
			if (endTime <= startTime)
				throw new ArgumentException("End time must be after start time.");

			EventId = eventId;
			Title = title;
			Description = description;
			Location = location;
			StartTime = startTime;
			EndTime = endTime;
			CategoryId = categoryId;
			CreatorId = createdBy;
			Capacity = capacity;

		}
		public Event()
		{
			Attendees = new HashSet<Attendee>();
			Comments = new HashSet<Comment>();
		}
	}

	public enum EventStatus
	{
		Actived,
		Canceled,
	}

}
