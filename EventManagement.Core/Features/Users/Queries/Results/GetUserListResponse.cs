namespace EventManagement.Core.Features.Users.Queries.Results
{
	public class GetUserListResponse
	{
		public int UserId { get; set; }
		public required string Username { get; set; }
		public required string Email { get; set; }
		public required string FirstName { get; set; }
		public required string LastName { get; set; }
		public string? Image { get; set; }
		public string Role { get; set; }
		public DateTime CreatedAt { get; set; }
	}
}
