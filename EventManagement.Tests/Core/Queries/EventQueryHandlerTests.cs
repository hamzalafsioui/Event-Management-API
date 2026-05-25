using AutoMapper;
using EventManagement.Core.Features.Events.Queries.Handlers;
using EventManagement.Core.Features.Events.Queries.Models;
using EventManagement.Core.Features.Events.Queries.Responses;
using EventManagement.Core.Resources;
using EventManagement.Data.Entities;
using EventManagement.Service.Abstracts;
using EventManagement.Tests.Helpers;
using FluentAssertions;
using Microsoft.Extensions.Localization;
using Moq;
using Xunit;

namespace EventManagement.Tests.Core.Queries
{
	public class EventQueryHandlerTests
	{
		private readonly Mock<IStringLocalizer<SharedResources>> _localizerMock;
		private readonly Mock<IEventService> _eventServiceMock;
		private readonly Mock<IMapper> _mapperMock;
		private readonly Mock<IAttendeeService> _attendeeServiceMock;
		private readonly Mock<ICommentService> _commentServiceMock;
		private readonly EventQueryHandler _handler;

		public EventQueryHandlerTests()
		{
			_localizerMock = new Mock<IStringLocalizer<SharedResources>>();
			_eventServiceMock = new Mock<IEventService>();
			_mapperMock = new Mock<IMapper>();
			_attendeeServiceMock = new Mock<IAttendeeService>();
			_commentServiceMock = new Mock<ICommentService>();

			_localizerMock.Setup(x => x[It.IsAny<string>()]).Returns(new LocalizedString("key", "value"));

			_handler = new EventQueryHandler(
				_localizerMock.Object,
				_eventServiceMock.Object,
				_mapperMock.Object,
				_attendeeServiceMock.Object,
				_commentServiceMock.Object);
		}

		[Fact]
		public async Task Handle_GetEventByIdQuery_WhenIdIsInvalid_ReturnsBadRequest()
		{
			// Arrange
			var query = new GetEventByIdQuery { Id = 0 };

			// Act
			var result = await _handler.Handle(query, CancellationToken.None);

			// Assert
			result.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);
			_eventServiceMock.Verify(s => s.GetEventByIdAsync(It.IsAny<int>()), Times.Never);
		}

		[Fact]
		public async Task Handle_GetEventByIdQuery_WhenEventNotFound_ReturnsNotFound()
		{
			// Arrange
			var query = new GetEventByIdQuery { Id = 99 };
			_eventServiceMock.Setup(s => s.GetEventByIdAsync(99)).ReturnsAsync((Event)null);

			// Act
			var result = await _handler.Handle(query, CancellationToken.None);

			// Assert
			result.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound);
		}

		[Fact]
		public async Task Handle_GetEventByIdQuery_WhenValid_ReturnsEventWithAttendeesAndComments()
		{
			// Arrange
			var query = new GetEventByIdQuery { Id = 1, AttendeePageNumber = 1, AttendeePageSize = 10, CommentPageNumber = 1, CommentPageSize = 10 };
			var @event = new Event { EventId = 1, Title = "Test Event", Location = "Location", StartTime = DateTime.Now, EndTime = DateTime.Now, CategoryId = 1, CreatorId = 1, Capacity = 100 };
			var eventResponse = new GetEventByIdResponse { EventId = 1, Title = "Test Event", Description = "Desc", Location = "Location", StartTime = DateTime.Now, EndTime = DateTime.Now, CategoryName = "Cat", CreatedBy = "User", Capacity = 100, AttendeesList = null!, CommentsList = null! };

			_eventServiceMock.Setup(s => s.GetEventByIdAsync(1)).ReturnsAsync(@event);
			_mapperMock.Setup(m => m.Map<GetEventByIdResponse>(@event)).Returns(eventResponse);

			// Mocking Attendees and Comments as empty AsyncEnumerables
			var attendees = new List<Attendee>();
			var comments = new List<Comment>();

			_attendeeServiceMock.Setup(s => s.GetAttendeesByEventIdQueryable(1))
				.Returns(new TestAsyncEnumerable<Attendee>(attendees));

			_commentServiceMock.Setup(s => s.GetCommentsByEventIdQueryable(1))
				.Returns(new TestAsyncEnumerable<Comment>(comments));

			// Act
			var result = await _handler.Handle(query, CancellationToken.None);

			// Assert
			result.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
			result.Data.Title.Should().Be("Test Event");
			result.Data.AttendeesList.Data.Should().BeEmpty();
			result.Data.CommentsList.Data.Should().BeEmpty();
		}

		[Fact]
		public async Task Handle_GetEventListQuery_ReturnsMappedEventList()
		{
			// Arrange
			var query = new GetEventListQuery();
			var events = new List<Event> 
			{ 
				new Event { EventId = 1, Title = "Event 1", Location = "Location", StartTime = DateTime.Now, EndTime = DateTime.Now, CategoryId = 1, CreatorId = 1, Capacity = 100 },
				new Event { EventId = 2, Title = "Event 2", Location = "Location", StartTime = DateTime.Now, EndTime = DateTime.Now, CategoryId = 1, CreatorId = 1, Capacity = 100 }
			};
			var responseList = new List<GetEventListResponse>
			{
				new GetEventListResponse { EventId = 1, Title = "Event 1", Description = "Desc", Location = "Location", StartTime = DateTime.Now, EndTime = DateTime.Now, CategoryName = "Cat", CreatedBy = "User", Capacity = 100 },
				new GetEventListResponse { EventId = 2, Title = "Event 2", Description = "Desc", Location = "Location", StartTime = DateTime.Now, EndTime = DateTime.Now, CategoryName = "Cat", CreatedBy = "User", Capacity = 100 }
			};

			_eventServiceMock.Setup(s => s.GetEventsListAsync()).ReturnsAsync(events);
			_mapperMock.Setup(m => m.Map<List<GetEventListResponse>>(events)).Returns(responseList);

			// Act
			var result = await _handler.Handle(query, CancellationToken.None);

			// Assert
			result.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
			result.Data.Should().HaveCount(2);
			result.Data.First().Title.Should().Be("Event 1");
		}
	}
}
