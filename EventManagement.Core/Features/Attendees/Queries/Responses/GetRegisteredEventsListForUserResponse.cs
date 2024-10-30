namespace EventManagement.Core.Features.Attendees.Queries.Responses
{
	public class GetRegisteredEventsListForUserResponse
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public DateTime StartTime { get; set; }
		public DateTime EndTime { get; set; }
	}


}
