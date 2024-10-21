namespace EventManagement.Core.Features.Users.Queries.Results
{
	public class GetUserPaginatedListResponse
	{
		public int Id { get; set; }
		public string Username { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Email { get; set; }
		public int Age { get; set; }
		public string? Image { get; set; }
		public string Role { get; set; }
		public DateTime CreatedAt { get; set; }


		public GetUserPaginatedListResponse()
		{

		}
	}
}
