using EventManagement.Data.Abstracts;
using EventManagement.Data.Helper;
using Microsoft.AspNetCore.Identity;

namespace EventManagement.Data.Entities
{
	public class User : IdentityUser<int>, IHasCreatedAt, IHasUpdatedAt
	{

		public required string FirstName { get; set; }
		public required string LastName { get; set; }
		public DateTime DateOfBirth { get; set; }
		public string? Image { get; set; }
		public UserRoleEnum Role { get; set; }
		public DateTime CreatedAt { get; set; }
		public DateTime UpdatedAt { get; set; }
		public DateTime LastLoginDate { get; set; }
		public bool IsDeleted { get; set; }

		public virtual ICollection<Event> CreatedEvents { get; set; } = new HashSet<Event>();
		public virtual ICollection<Attendee> AttendingEvents { get; set; } = new HashSet<Attendee>();
		public virtual ICollection<Comment> Comments { get; set; } = new HashSet<Comment>();



		public User()
		{
			CreatedEvents = new HashSet<Event>();
			AttendingEvents = new HashSet<Attendee>();
			Comments = new HashSet<Comment>();
		}
	}
}
