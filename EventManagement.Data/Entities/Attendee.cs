using EventManagement.Data.Abstracts;
using EventManagement.Data.Entities.Identity;
using System.Text.Json.Serialization;

namespace EventManagement.Data.Entities
{
	public class Attendee : IHasCreatedAt, IHasUpdatedAt
	{
		public int EventId { get; set; }
		public int UserId { get; set; }
		public RSVPStatus Status { get; set; }
		public DateTime CreatedAt { get; set; }
		public DateTime UpdatedAt { get; set; }

		public Event Event { get; set; }
		public User User { get; set; }

		public DateTime? RSVPDate { get; set; }
		public bool HasAttended { get; set; } = false;
	}


	[JsonConverter(typeof(JsonStringEnumConverter))]
	public enum RSVPStatus   // Répondez s'il vous plaît
	{
		Going,
		NotGoing,
		Interested,
		Cancelled,
		Maybe,
		Pending
	}
}
