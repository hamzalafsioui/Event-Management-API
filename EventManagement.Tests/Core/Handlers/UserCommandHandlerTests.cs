using AutoMapper;
using EventManagement.Core.Features.Users.Commands.Handlers;
using EventManagement.Core.Features.Users.Commands.Models;
using EventManagement.Core.Resources;
using EventManagement.Data.Entities.Identity;
using EventManagement.Service.Abstracts;
using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using Moq;
using Xunit;

namespace EventManagement.Tests.Core.Handlers
{
	public class UserCommandHandlerTests
	{
		private readonly Mock<UserManager<User>> _userManagerMock;
		private readonly Mock<IMapper> _mapperMock;
		private readonly Mock<IStringLocalizer<SharedResources>> _localizerMock;
		private readonly Mock<IUserService> _userServiceMock;
		private readonly Mock<IFileService> _fileServiceMock;
		private readonly UserCommandHandler _handler;

		public UserCommandHandlerTests()
		{
			var store = new Mock<IUserStore<User>>();
			_userManagerMock = new Mock<UserManager<User>>(store.Object, null, null, null, null, null, null, null, null);
			_mapperMock = new Mock<IMapper>();
			_localizerMock = new Mock<IStringLocalizer<SharedResources>>();
			_userServiceMock = new Mock<IUserService>();
			_fileServiceMock = new Mock<IFileService>();

			_localizerMock.Setup(x => x[It.IsAny<string>()]).Returns(new LocalizedString("key", "value"));

			_handler = new UserCommandHandler(
				_userManagerMock.Object,
				_mapperMock.Object,
				_localizerMock.Object,
				_userServiceMock.Object,
				_fileServiceMock.Object);
		}

		[Fact]
		public async Task Handle_AddUserCommand_WhenSuccessful_ReturnsCreatedResponse()
		{
			// Arrange
			var command = new AddUserCommand { UserName = "testuser", Password = "Password123!", ConfirmPassword = "Password123!", FirstName = "Test", LastName = "User", DateOfBirth = DateTime.Now, Email = "test@test.com", Image = null };
			var userMapping = new User { UserName = "testuser", Email = "test@test.com", FirstName = "Test", LastName = "User" };
			
			_mapperMock.Setup(m => m.Map<User>(command)).Returns(userMapping);
			_userServiceMock.Setup(s => s.AddAsync(userMapping, command.Password)).ReturnsAsync("Success");

			// Act
			var result = await _handler.Handle(command, CancellationToken.None);

			// Assert
			result.StatusCode.Should().Be(System.Net.HttpStatusCode.Created);
			_userServiceMock.Verify(s => s.AddAsync(userMapping, command.Password), Times.Once);
		}

		[Fact]
		public async Task Handle_DeleteUserCommand_WhenUserNotFound_ReturnsNotFoundResponse()
		{
			// Arrange
			var command = new DeleteUserCommand(1);
			_userManagerMock.Setup(m => m.FindByIdAsync("1")).ReturnsAsync((User)null);

			// Act
			var result = await _handler.Handle(command, CancellationToken.None);

			// Assert
			result.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound);
			_userManagerMock.Verify(m => m.DeleteAsync(It.IsAny<User>()), Times.Never);
		}

		[Fact]
		public async Task Handle_DeleteUserCommand_WhenUserExists_ReturnsDeletedResponse()
		{
			// Arrange
			var command = new DeleteUserCommand(1);
			var user = new User { Id = 1, UserName = "testuser", FirstName = "Test", LastName = "User" };
			
			_userManagerMock.Setup(m => m.FindByIdAsync("1")).ReturnsAsync(user);
			_userManagerMock.Setup(m => m.DeleteAsync(user)).ReturnsAsync(IdentityResult.Success);

			// Act
			var result = await _handler.Handle(command, CancellationToken.None);

			// Assert
			result.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
			_userManagerMock.Verify(m => m.DeleteAsync(user), Times.Once);
		}
	}
}
