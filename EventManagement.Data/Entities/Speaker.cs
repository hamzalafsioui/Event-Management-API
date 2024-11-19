using EventManagement.Data.Entities.Identity;

namespace EventManagement.Data.Entities
{
	public class Speaker
	{
		public int Id { get; set; }

		public int UserId { get; set; }
		public User User { get; set; }

		public string Bio { get; set; }

		public ICollection<SpeakerEvent> SpeakerEvents { get; set; } = new List<SpeakerEvent>();

	}
}
