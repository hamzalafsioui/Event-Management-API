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
		public UserRole Role { get; set; } = UserRole.Attendee;
		public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
		public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

		public ICollection<Event> CreatedEvents { get; set; } = new List<Event>();
		public ICollection<Attendee> AttendingEvents { get; set; } = new List<Attendee>();
		public ICollection<Comment> Comments { get; set; } = new List<Comment>();

		public User(int userId, string username, string passwordHash, string email, string firstName, string lastName, string? image, UserRole role, DateTime createdAt, DateTime updatedAt)
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

	public enum UserRole { Admin, Speaker, Attendee }


}
