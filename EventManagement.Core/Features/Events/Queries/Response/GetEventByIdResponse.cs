﻿using EventManagement.Core.Wrappers;
using EventManagement.Data.Entities;

namespace EventManagement.Core.Features.Events.Queries.Response
{
	public class GetEventByIdResponse
	{
		public int EventId { get; set; }
		public required string Title { get; set; }
		public string? Description { get; set; } = string.Empty;
		public required string Location { get; set; }
		public required DateTime StartTime { get; set; } 
		public required DateTime EndTime { get; set; }
		public required string CategoryName { get; set; }
		public required string CreatedBy { get; set; }
		public required int Capacity { get; set; }

		public DateTime CreatedAt { get; set; }

		public PaginatedResult<AttendeeResponse>? AttendeesList {  get; set; }
		public PaginatedResult<CommentResponse>? CommentsList {  get; set; }

	}
	public class AttendeeResponse
	{
	

		public int AttendeeId { get; set; }
		public string AttendeeName { get; set; }
		public AttendeeResponse(int attendeeId, string attendeeName)
		{
			AttendeeId = attendeeId;
			AttendeeName = attendeeName;
		}
	}
	public class CommentResponse
	{
	
		public int CommentId { get; set; }
		public string WriteBy { get; set; }
		public string Content {  get; set; }
		public CommentResponse(int commentId, string writeBy, string content)
		{
			CommentId = commentId;
			WriteBy = writeBy;
			Content = content;
		}

	}
}
