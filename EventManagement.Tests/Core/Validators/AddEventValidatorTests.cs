using EventManagement.Core.Features.Events.Commands.Models;
using EventManagement.Core.Features.Events.Commands.Validatiors;
using EventManagement.Core.Resources;
using EventManagement.Service.Abstracts;
using FluentAssertions;
using Microsoft.Extensions.Localization;
using Moq;
using Xunit;

namespace EventManagement.Tests.Core.Validators
{
	public class AddEventValidatorTests
	{
		private readonly Mock<IEventService> _eventServiceMock;
		private readonly Mock<ICategoryService> _categoryServiceMock;
		private readonly Mock<ISpeakerService> _speakerServiceMock;
		private readonly Mock<IStringLocalizer<SharedResources>> _localizerMock;
		private readonly AddEventValidator _validator;

		public AddEventValidatorTests()
		{
			_eventServiceMock = new Mock<IEventService>();
			_categoryServiceMock = new Mock<ICategoryService>();
			_speakerServiceMock = new Mock<ISpeakerService>();
			_localizerMock = new Mock<IStringLocalizer<SharedResources>>();

			_localizerMock.Setup(x => x[It.IsAny<string>()]).Returns(new LocalizedString("key", "value"));

			_validator = new AddEventValidator(
				_eventServiceMock.Object,
				_categoryServiceMock.Object,
				_speakerServiceMock.Object,
				_localizerMock.Object);
		}

		[Fact]
		public async Task Validate_WhenCategoryDoesNotExist_ShouldHaveValidationError()
		{
			// Arrange
			var command = new AddEventCommand("Test Event", "Description", "Location", DateTime.Now, DateTime.Now.AddDays(1), 999, 1, 100, null);

			_categoryServiceMock.Setup(x => x.IsCategoryIdExist(999)).ReturnsAsync(false);
			_speakerServiceMock.Setup(x => x.IsSpeakerExistAsync(It.IsAny<int>())).ReturnsAsync(true);

			// Act
			var result = await _validator.ValidateAsync(command);

			// Assert
			result.IsValid.Should().BeFalse();
			result.Errors.Should().Contain(e => e.PropertyName == "CategoryId");
		}

		[Fact]
		public async Task Validate_WhenEndTimeBeforeStartTime_ShouldHaveValidationError()
		{
			// Arrange
			var startTime = DateTime.Now;
			var endTime = startTime.AddDays(-1); // End time before start time
			var command = new AddEventCommand("Test Event", "Description", "Location", startTime, endTime, 1, 1, 100, null);

			_categoryServiceMock.Setup(x => x.IsCategoryIdExist(1)).ReturnsAsync(true);
			_speakerServiceMock.Setup(x => x.IsSpeakerExistAsync(It.IsAny<int>())).ReturnsAsync(true);

			// Act
			var result = await _validator.ValidateAsync(command);

			// Assert
			result.IsValid.Should().BeFalse();
			result.Errors.Should().Contain(e => e.PropertyName == "EndTime");
		}

		[Fact]
		public async Task Validate_WhenSpeakerDoesNotExist_ShouldHaveValidationError()
		{
			// Arrange
			var command = new AddEventCommand("Test Event", "Description", "Location", DateTime.Now, DateTime.Now.AddDays(1), 1, 1, 100, new List<int> { 5 });

			_categoryServiceMock.Setup(x => x.IsCategoryIdExist(1)).ReturnsAsync(true);
			_speakerServiceMock.Setup(x => x.IsSpeakerExistAsync(5)).ReturnsAsync(false); // Speaker doesn't exist

			// Act
			var result = await _validator.ValidateAsync(command);

			// Assert
			result.IsValid.Should().BeFalse();
			result.Errors.Should().Contain(e => e.PropertyName == "SpeakerIds");
		}

		[Fact]
		public async Task Validate_WhenValidCommand_ShouldNotHaveValidationErrors()
		{
			// Arrange
			var command = new AddEventCommand("Test Event", "Description", "Location", DateTime.Now, DateTime.Now.AddDays(1), 1, 1, 100, new List<int> { 1 });

			_categoryServiceMock.Setup(x => x.IsCategoryIdExist(1)).ReturnsAsync(true);
			_speakerServiceMock.Setup(x => x.IsSpeakerExistAsync(1)).ReturnsAsync(true);

			// Act
			var result = await _validator.ValidateAsync(command);

			// Assert
			result.IsValid.Should().BeTrue();
		}
	}
}
