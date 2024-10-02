namespace EventManagement.Core.Features.Users.Queries.Results
{
	public class GetSingleUserResponse
	{
		public string UserId { get; set; }
		public required string Username { get; set; }
		public required string Email { get; set; }
		public required string FirstName { get; set; }
		public required string LastName { get; set; }
		public string? Image { get; set; }
		public required string Role { get; set; }
		public DateTime CreatedAt { get; set; }
	}
}
