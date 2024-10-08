
using EventManagement.Data.Helper;

namespace EventManagement.Data.Entities
{
	public class User
	{

		public int UserId { get; set; }
		public required string Username { get; set; }
		public required string PasswordHash { get; set; }
		public required string Email { get; set; }
		public required string FirstName { get; set; }
		public required string LastName { get; set; }
		//public byte[]? Image { get; set; }
		public string? Image { get; set; }
		public UserRoleEnum Role { get; set; }
		public DateTime CreatedAt { get; set; }
		public DateTime UpdatedAt { get; set; }

		public ICollection<Event> CreatedEvents { get; set; } = new List<Event>();
		public ICollection<Attendee> AttendingEvents { get; set; } = new List<Attendee>();
		public ICollection<Comment> Comments { get; set; } = new List<Comment>();

		public User(int userId, string username, string passwordHash, string email, string firstName, string lastName, string? image, UserRoleEnum role, DateTime createdAt, DateTime updatedAt)
		{
			UserId = userId;
			Username = username;
			PasswordHash = passwordHash;
			Email = email;
			FirstName = firstName;
			LastName = lastName;
			Image = image;
			Role = role;
			CreatedAt = createdAt;
			UpdatedAt = updatedAt;
		}

		public User()
		{

		}
	}
}
