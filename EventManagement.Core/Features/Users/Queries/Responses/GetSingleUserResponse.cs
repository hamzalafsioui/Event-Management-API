namespace EventManagement.Core.Features.Users.Queries.Responses
{
	public record GetSingleUserResponse
	{
		public string Id { get; init; }
		public required string Username { get; init; }
		public required string Email { get; init; }
		public required string FirstName { get; init; }
		public required string LastName { get; init; }
		public required int Age { get; init; }
		public string? Image { get; init; }
		public required string Role { get; init; }
		public required DateTime CreatedAt { get; init; }
	}
}
