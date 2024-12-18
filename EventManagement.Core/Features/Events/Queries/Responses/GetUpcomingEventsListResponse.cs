﻿namespace EventManagement.Core.Features.Events.Queries.Responses
{
	public class GetUpcomingOrPastEventsListResponse
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Location { get; set; }
		public DateTime StartTime { get; set; }
		public DateTime EndTime { get; set; }
		public string Creator { get; set; }
		public List<SpeakerResponse> Speakers { get; set; }
	}
}
