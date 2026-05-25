using AutoMapper;
using EventManagement.Core.Features.Events.Commands.Handlers;
using EventManagement.Core.Features.Events.Commands.Models;
using EventManagement.Core.Resources;
using EventManagement.Data.Entities;
using EventManagement.Service.Abstracts;
using FluentAssertions;
using Microsoft.Extensions.Localization;
using Moq;
using Xunit;

namespace EventManagement.Tests.Core.Handlers
{
	public class EventCommandHandlerTests
	{
		private readonly Mock<IEventService> _eventServiceMock;
		private readonly Mock<IMapper> _mapperMock;
		private readonly Mock<IStringLocalizer<SharedResources>> _localizerMock;
		private readonly EventCommandHandler _handler;

		public EventCommandHandlerTests()
		{
			_eventServiceMock = new Mock<IEventService>();
			_mapperMock = new Mock<IMapper>();
			_localizerMock = new Mock<IStringLocalizer<SharedResources>>();

			_localizerMock.Setup(x => x[It.IsAny<string>()]).Returns(new LocalizedString("key", "value"));

			_handler = new EventCommandHandler(_eventServiceMock.Object, _mapperMock.Object, _localizerMock.Object);
		}

		[Fact]
		public async Task Handle_AddEventCommand_WhenSuccessful_ReturnsCreatedResponse()
		{
			// Arrange
			var command = new AddEventCommand("Test Event", "Description", "Location", DateTime.Now, DateTime.Now.AddDays(1), 1, 1, 100, new List<int> { 1, 2 });
			var newEvent = new Event { Title = "Test Event", Location = "Location", StartTime = DateTime.Now, EndTime = DateTime.Now.AddDays(1), CategoryId = 1, CreatorId = 1, Capacity = 100 };

			_mapperMock.Setup(m => m.Map<Event>(command)).Returns(newEvent);
			_eventServiceMock.Setup(s => s.AddAsync(newEvent, command.SpeakerIds)).ReturnsAsync(EventManagement.Data.Helper.Result.Success());

			// Act
			var result = await _handler.Handle(command, CancellationToken.None);

			// Assert
			result.StatusCode.Should().Be(System.Net.HttpStatusCode.Created);
			_eventServiceMock.Verify(s => s.AddAsync(newEvent, command.SpeakerIds), Times.Once);
		}

		[Fact]
		public async Task Handle_DeleteEventCommand_WhenEventDoesNotExist_ReturnsNotFoundResponse()
		{
			// Arrange
			var command = new DeleteEventCommand(1);
			_eventServiceMock.Setup(s => s.GetEventByIdAsync(1)).ReturnsAsync((Event)null);

			// Act
			var result = await _handler.Handle(command, CancellationToken.None);

			// Assert
			result.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound);
			_eventServiceMock.Verify(s => s.DeleteAsync(It.IsAny<Event>()), Times.Never);
		}

		[Fact]
		public async Task Handle_CancelEventCommand_WhenSuccessful_ReturnsSuccessResponse()
		{
			// Arrange
			var command = new CancelEventCommand(1);
			_eventServiceMock.Setup(s => s.CancelAsync(1)).ReturnsAsync(false); // Service returns false if there's no error, according to existing code logic!

			// Act
			var result = await _handler.Handle(command, CancellationToken.None);

			// Assert
			result.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
			_eventServiceMock.Verify(s => s.CancelAsync(1), Times.Once);
		}
	}
}
