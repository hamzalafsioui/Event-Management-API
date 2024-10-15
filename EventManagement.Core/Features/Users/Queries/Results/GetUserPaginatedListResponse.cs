namespace EventManagement.Core.Features.Users.Queries.Results
{
	public class GetUserPaginatedListResponse
	{
		public int UserId { get; set; }
		public string Username { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Email { get; set; }
		public int Age { get; set; }
		public string? Image { get; set; }
		public string Role { get; set; }
		public DateTime CreatedAt { get; set; }


		public GetUserPaginatedListResponse(int userId, string username, string firstName, string lastName, DateTime dateOfBirth, string email, string? image, string role, DateTime createdAt)
		{
			UserId = userId;
			Username = username;
			Email = email;
			FirstName = firstName;
			LastName = lastName;
			Age = DateTime.UtcNow.Year - dateOfBirth.Year;
			Image = image;
			Role = role;
			CreatedAt = createdAt;
		}
	}
}
