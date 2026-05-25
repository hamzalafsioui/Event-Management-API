using AutoMapper;
using EventManagement.Core.Bases;
using EventManagement.Core.Features.Attendees.Command.Handlers;
using EventManagement.Core.Features.Attendees.Command.Models;
using EventManagement.Core.Resources;
using EventManagement.Data.Entities;
using EventManagement.Data.Entities.Identity;
using EventManagement.Service.Abstracts;
using FluentAssertions;
using Microsoft.Extensions.Localization;
using Moq;
using Xunit;

namespace EventManagement.Tests.Core.Handlers
{
	public class AttendeeCommandHandlerTests
	{
		private readonly Mock<IStringLocalizer<SharedResources>> _localizerMock;
		private readonly Mock<IAttendeeService> _attendeeServiceMock;
		private readonly Mock<IMapper> _mapperMock;
		private readonly Mock<IEventService> _eventServiceMock;
		private readonly Mock<IUserService> _userServiceMock;
		private readonly Mock<IEmailService> _emailServiceMock;
		private readonly AttendeeCommandHandler _handler;

		public AttendeeCommandHandlerTests()
		{
			_localizerMock = new Mock<IStringLocalizer<SharedResources>>();
			_attendeeServiceMock = new Mock<IAttendeeService>();
			_mapperMock = new Mock<IMapper>();
			_eventServiceMock = new Mock<IEventService>();
			_userServiceMock = new Mock<IUserService>();
			_emailServiceMock = new Mock<IEmailService>();

			// Setup localizer dummy returns
			_localizerMock.Setup(x => x[It.IsAny<string>()]).Returns(new LocalizedString("key", "value"));

			_handler = new AttendeeCommandHandler(
				_localizerMock.Object,
				_attendeeServiceMock.Object,
				_mapperMock.Object,
				_eventServiceMock.Object,
				_userServiceMock.Object,
				_emailServiceMock.Object);
		}

		[Fact]
		public async Task Handle_AddAttendeeCommand_WhenEventFull_SetsStatusToWaitlisted()
		{
			// Arrange
			var command = new AddAttendeeCommand(UserId: 1, EventId: 1, Status: "Going");
			var attendeeMapping = new Attendee { UserId = 1, EventId = 1, Status = RSVPStatus.Going };
			
			_mapperMock.Setup(m => m.Map<Attendee>(command)).Returns(attendeeMapping);

			var eventDetails = new Event { EventId = 1, Title = "Test Event", Capacity = 5, Location = "Test", StartTime = DateTime.Now, EndTime = DateTime.Now.AddDays(1), CategoryId = 1, CreatorId = 1 };
			_eventServiceMock.Setup(x => x.GetEventByIdAsync(1)).ReturnsAsync(eventDetails);

			// Simulate that the event is full (current going count >= capacity)
			_attendeeServiceMock.Setup(x => x.GetGoingAttendeesCountAsync(1)).ReturnsAsync(5);
			_attendeeServiceMock.Setup(x => x.AddAsync(It.IsAny<Attendee>())).ReturnsAsync(attendeeMapping);

			// Act
			var result = await _handler.Handle(command, CancellationToken.None);

			// Assert
			attendeeMapping.Status.Should().Be(RSVPStatus.Waitlisted);
			_attendeeServiceMock.Verify(x => x.AddAsync(It.Is<Attendee>(a => a.Status == RSVPStatus.Waitlisted)), Times.Once);
			result.StatusCode.Should().Be(System.Net.HttpStatusCode.Created);
		}

		[Fact]
		public async Task Handle_LeaveEventCommand_WhenGoingAttendeeLeaves_TriggersWaitlistEmails()
		{
			// Arrange
			var command = new LeaveEventCommand(UserId: 1, EventId: 1);
			var leavingAttendee = new Attendee { UserId = 1, EventId = 1, Status = RSVPStatus.Going };
			
			_attendeeServiceMock.Setup(x => x.GetAttendeeByUserIdEventIdAsync(1, 1)).ReturnsAsync(leavingAttendee);
			_attendeeServiceMock.Setup(x => x.DeleteAsync(leavingAttendee)).ReturnsAsync(true);

			var waitlistedAttendees = new List<Attendee>
			{
				new Attendee { UserId = 2, EventId = 1, Status = RSVPStatus.Waitlisted }
			};
			_attendeeServiceMock.Setup(x => x.GetAllWaitlistedAttendeesAsync(1)).ReturnsAsync(waitlistedAttendees);

			var eventDetails = new Event { EventId = 1, Title = "Test Event", Capacity = 5, Location = "Test", StartTime = DateTime.Now, EndTime = DateTime.Now.AddDays(1), CategoryId = 1, CreatorId = 1 };
			_eventServiceMock.Setup(x => x.GetEventByIdAsync(1)).ReturnsAsync(eventDetails);

			var waitlistedUser = new User { Id = 2, Email = "test@test.com", FirstName = "Wait", LastName = "Listed" };
			_userServiceMock.Setup(x => x.GetByIdAsync(2)).ReturnsAsync(waitlistedUser);

			// Act
			var result = await _handler.Handle(command, CancellationToken.None);

			// Assert
			_emailServiceMock.Verify(x => x.SendEmailAsync("test@test.com", It.IsAny<string>(), It.IsAny<string>()), Times.Once);
			_attendeeServiceMock.Verify(x => x.UpdateAsyc(It.IsAny<Attendee>()), Times.Never); // Waitlisted user status should not be auto-updated
			result.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
		}
	}
}
