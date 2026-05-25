using EventManagement.Data.Entities;
using EventManagement.Infrustructure.Repositories;
using EventManagement.Service.Implementations;
using EventManagement.Tests.Helpers;
using FluentAssertions;
using Moq;
using Xunit;

namespace EventManagement.Tests.Service
{
	public class AttendeeServiceTests
	{
		private readonly Mock<IAttendeeRepository> _attendeeRepositoryMock;
		private readonly AttendeeService _attendeeService;

		public AttendeeServiceTests()
		{
			_attendeeRepositoryMock = new Mock<IAttendeeRepository>();
			_attendeeService = new AttendeeService(_attendeeRepositoryMock.Object);
		}

		[Fact]
		public async Task GetGoingAttendeesCountAsync_ShouldReturnOnlyGoingAttendeesForSpecificEvent()
		{
			// Arrange
			var eventId = 1;
			var attendees = new List<Attendee>
			{
				new Attendee { EventId = eventId, UserId = 1, Status = RSVPStatus.Going },
				new Attendee { EventId = eventId, UserId = 2, Status = RSVPStatus.Going },
				new Attendee { EventId = eventId, UserId = 3, Status = RSVPStatus.Waitlisted },
				new Attendee { EventId = 2, UserId = 4, Status = RSVPStatus.Going } // Different event
			};

			var mockQueryable = new TestAsyncEnumerable<Attendee>(attendees);

			_attendeeRepositoryMock.Setup(repo => repo.GetTableNoTracking())
				.Returns(mockQueryable);

			// Act
			var result = await _attendeeService.GetGoingAttendeesCountAsync(eventId);

			// Assert
			result.Should().Be(2);
		}

		[Fact]
		public async Task GetAllWaitlistedAttendeesAsync_ShouldReturnWaitlistedOrderedByDate()
		{
			// Arrange
			var eventId = 1;
			var attendees = new List<Attendee>
			{
				new Attendee { EventId = eventId, UserId = 1, Status = RSVPStatus.Waitlisted, RSVPDate = new DateTime(2023, 1, 3) },
				new Attendee { EventId = eventId, UserId = 2, Status = RSVPStatus.Waitlisted, RSVPDate = new DateTime(2023, 1, 1) }, // Oldest
				new Attendee { EventId = eventId, UserId = 3, Status = RSVPStatus.Going, RSVPDate = new DateTime(2023, 1, 2) }
			};

			var mockQueryable = new TestAsyncEnumerable<Attendee>(attendees);

			_attendeeRepositoryMock.Setup(repo => repo.GetTableNoTracking())
				.Returns(mockQueryable);

			// Act
			var result = await _attendeeService.GetAllWaitlistedAttendeesAsync(eventId);

			// Assert
			result.Should().HaveCount(2);
			result.First().UserId.Should().Be(2); // The one with oldest date
			result.Last().UserId.Should().Be(1);
		}
	}
}
