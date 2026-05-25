using EventManagement.Core.Features.Users.Commands.Models;
using EventManagement.Core.Resources;
using EventManagement.Data.Entities.Identity;
using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using Moq;
using Xunit;

namespace EventManagement.Tests.Core.Validators
{
	public class AddUserValidatorTests
	{
		private readonly Mock<UserManager<User>> _userManagerMock;
		private readonly Mock<IStringLocalizer<SharedResources>> _localizerMock;
		private readonly AddUserValidator _validator;

		public AddUserValidatorTests()
		{
			var store = new Mock<IUserStore<User>>();
			_userManagerMock = new Mock<UserManager<User>>(store.Object, null, null, null, null, null, null, null, null);
			_localizerMock = new Mock<IStringLocalizer<SharedResources>>();

			_localizerMock.Setup(x => x[It.IsAny<string>()]).Returns(new LocalizedString("key", "value"));

			_validator = new AddUserValidator(_userManagerMock.Object, _localizerMock.Object);
		}

		[Fact]
		public async Task Validate_WhenPasswordsDoNotMatch_ShouldHaveValidationError()
		{
			// Arrange
			var command = new AddUserCommand { UserName = "testuser", Password = "Password123!", ConfirmPassword = "DifferentPassword", FirstName = "Test", LastName = "User", DateOfBirth = DateTime.Now, Email = "test@test.com", Image = null };

			_userManagerMock.Setup(x => x.FindByNameAsync(It.IsAny<string>())).ReturnsAsync((User)null);
			_userManagerMock.Setup(x => x.FindByEmailAsync(It.IsAny<string>())).ReturnsAsync((User)null);

			// Act
			var result = await _validator.ValidateAsync(command);

			// Assert
			result.IsValid.Should().BeFalse();
			result.Errors.Should().Contain(e => e.PropertyName == "ConfirmPassword");
		}

		[Fact]
		public async Task Validate_WhenUsernameAlreadyExists_ShouldHaveValidationError()
		{
			// Arrange
			var command = new AddUserCommand { UserName = "existinguser", Password = "Password123!", ConfirmPassword = "Password123!", FirstName = "Test", LastName = "User", DateOfBirth = DateTime.Now, Email = "new@test.com", Image = null };
			var existingUser = new User { UserName = "existinguser", FirstName = "Test", LastName = "User" };

			_userManagerMock.Setup(x => x.FindByNameAsync("existinguser")).ReturnsAsync(existingUser);
			_userManagerMock.Setup(x => x.FindByEmailAsync(It.IsAny<string>())).ReturnsAsync((User)null);

			// Act
			var result = await _validator.ValidateAsync(command);

			// Assert
			result.IsValid.Should().BeFalse();
			result.Errors.Should().Contain(e => e.PropertyName == "UserName");
		}

		[Fact]
		public async Task Validate_WhenValidCommand_ShouldNotHaveValidationErrors()
		{
			// Arrange
			var command = new AddUserCommand { UserName = "testuser", Password = "Password123!", ConfirmPassword = "Password123!", FirstName = "Test", LastName = "User", DateOfBirth = DateTime.Now, Email = "test@test.com", Image = null };

			_userManagerMock.Setup(x => x.FindByNameAsync(It.IsAny<string>())).ReturnsAsync((User)null);
			_userManagerMock.Setup(x => x.FindByEmailAsync(It.IsAny<string>())).ReturnsAsync((User)null);

			// Act
			var result = await _validator.ValidateAsync(command);

			// Assert
			result.IsValid.Should().BeTrue();
		}
	}
}
