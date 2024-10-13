
using EventManagement.Data.Helper;

namespace EventManagement.Data.Entities
{
	public class User
	{

		public int UserId { get; set; }
		public required string Username { get; set; }
		public required string Password { get; set; }
		public required string FirstName { get; set; }
		public required string LastName { get; set; }
		public required string Email { get; set; }
		//public byte[]? Image { get; set; }
		public string? Image { get; set; }
		public UserRoleEnum Role { get; set; }
		public DateTime CreatedAt { get; set; }
		public DateTime UpdatedAt { get; set; }

		public virtual ICollection<Event> CreatedEvents { get; set; }
		public virtual ICollection<Attendee> AttendingEvents { get; set; }
		public virtual ICollection<Comment> Comments { get; set; }

		public User(int userId, string username, string password, string email, string firstName, string lastName, string? image, UserRoleEnum role, DateTime createdAt, DateTime updatedAt)
		{
			UserId = userId;
			Username = username;
			Password = password;
			Email = email;
			FirstName = firstName;
			LastName = lastName;
			Image = image;
			Role = role;
			CreatedAt = createdAt;
			UpdatedAt = updatedAt;
			CreatedEvents = new HashSet<Event>();
			AttendingEvents = new HashSet<Attendee>();
			Comments = new HashSet<Comment>();
		}

		public User()
		{
			CreatedEvents = new HashSet<Event>();
			AttendingEvents = new HashSet<Attendee>();
			Comments = new HashSet<Comment>();
		}
	}
}
