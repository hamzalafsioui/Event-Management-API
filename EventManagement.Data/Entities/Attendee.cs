namespace EventManagement.Data.Entities
{
	public class Attendee
	{
		public int AttendeeId { get; set; }
		public int EventId { get; set; }
		public int UserId { get; set; }
		public RSVPStatus Status { get; set; }
		public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

		public Event Event { get; set; }
		public User User { get; set; }

		public DateTime? RSVPDate { get; set; }
		public bool HasAttended { get; set; } = false;
	}



	public enum RSVPStatus   // Répondez s'il vous plaît
	{
		Going,
		NotGoing,
		Interested,
		Maybe
	}
}
