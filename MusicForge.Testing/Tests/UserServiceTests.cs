using Moq;
using MusicForge.BLL.Services;
using MusicForge.Domain.Interfaces;
using MusicForge.Domain.Models;

namespace MusicForge.Testing.Tests;

public class UserServiceTests
{
	// GetUserById
	[Fact]
	public void GetUserById_ReturnsCorrectUser()
	{
		// Arrange
		var userGuid = Guid.NewGuid();
		var expectedUser = new User(
								userGuid,
								"Bob",
								"Bobbert",
								"bob@bobbert.nl",
								"asdf1234",
								"User"
								);

		var mockRepo = new Mock<IUserRepository>();
		mockRepo.Setup(r => r.GetUserById(userGuid))
				.Returns(expectedUser);

		var service = new UserService(mockRepo.Object);

		// Act
		var result = service.GetUserById(userGuid);

		// Assert
		Assert.NotNull(result);
		Assert.Equal(userGuid, result.Id);
		Assert.Equal("Bob", result.FirstName);
		Assert.Equal("Bobbert", result.LastName);
		Assert.Equal("bob@bobbert.nl", result.Email);
		Assert.Equal("asdf1234", result.Password);
		Assert.Equal("User", result.Role);
	}

	[Fact]
	public void GetUserById_WhenUserNotFound_ReturnsNull()
	{
		// Arrange
		var mockRepo = new Mock<IUserRepository>();
		mockRepo.Setup(r => r.GetUserById(Guid.NewGuid()))
				.Returns((User)null);

		var service = new UserService(mockRepo.Object);

		// Act
		var result = service.GetUserById(Guid.NewGuid());

		// Assert
		Assert.Null(result);
	}

	// TryLoginUser
	[Fact]
	public void TryLoginUser_WithValidCredentials_ReturnsUserGuid()
	{
		var expectedGuid = Guid.NewGuid();
		var mockRepo = new Mock<IUserRepository>();
		mockRepo.Setup(r => r.ValidateUser("bob@bobbert.nl", "asdf1234"))
				.Returns(expectedGuid);
		var service = new UserService(mockRepo.Object);

		var result = service.TryLoginUser("bob@bobbert.nl", "asdf1234");

		Assert.Equal(expectedGuid, result);
	}

	[Fact]
	public void TryLoginUser_WithInvalidCredentials_ReturnsEmptyGuid()
	{
		var mockRepo = new Mock<IUserRepository>();
		mockRepo.Setup(r => r.ValidateUser(It.IsAny<string>(), It.IsAny<string>()))
				.Returns(Guid.Empty);
		var service = new UserService(mockRepo.Object);

		var result = service.TryLoginUser("wrong@email.nl", "wrongpassword");

		Assert.Equal(Guid.Empty, result);
	}

	[Theory]
	[InlineData("", "asdf1234")]
	[InlineData(null, "asdf1234")]
	public void TryLoginUser_WithEmptyOrNullEmail_ReturnsEmptyGuid(string email, string password)
	{
		var mockRepo = new Mock<IUserRepository>();
		var service = new UserService(mockRepo.Object);

		var result = service.TryLoginUser(email, password);

		Assert.Equal(Guid.Empty, result);
		mockRepo.Verify(r => r.ValidateUser(It.IsAny<string>(), It.IsAny<string>()), Times.Never);
	}

	[Theory]
	[InlineData("bob@bobbert.nl", "")]
	[InlineData("bob@bobbert.nl", null)]
	public void TryLoginUser_WithEmptyOrNullPassword_ReturnsEmptyGuid(string email, string password)
	{
		var mockRepo = new Mock<IUserRepository>();
		var service = new UserService(mockRepo.Object);

		var result = service.TryLoginUser(email, password);

		Assert.Equal(Guid.Empty, result);
		mockRepo.Verify(r => r.ValidateUser(It.IsAny<string>(), It.IsAny<string>()), Times.Never);
	}

	//RegisterUser()
	[Fact]
	public void RegisterUser_WithValidUser_ReturnsTrue()
	{
		var mockRepo = new Mock<IUserRepository>();
		var service = new UserService(mockRepo.Object);
		var newUser = new User(Guid.NewGuid(), "Bob", "Bobbert", "bob@bobbert.nl", "asdf1234", "Admin");

		var result = service.RegisterUser(newUser);

		Assert.True(result);
		mockRepo.Verify(r => r.AddUser(newUser), Times.Once);
	}

	[Fact]
	public void RegisterUser_WithNullUser_ReturnsFalse()
	{
		var mockRepo = new Mock<IUserRepository>();
		var service = new UserService(mockRepo.Object);

		var result = service.RegisterUser(null);

		Assert.False(result);
		mockRepo.Verify(r => r.AddUser(It.IsAny<User>()), Times.Never);
	}

	[Theory]
	[InlineData("", "bob@bobbert.nl", "asdf1234")]
	[InlineData(null, "bob@bobbert.nl", "asdf1234")]
	public void RegisterUser_WithEmptyOrNullFirstName_ReturnsFalse(string firstName, string email, string password)
	{
		var mockRepo = new Mock<IUserRepository>();
		var service = new UserService(mockRepo.Object);
		var newUser = new User(Guid.NewGuid(), firstName, "Bobbert", email, password, "User");

		var result = service.RegisterUser(newUser);

		Assert.False(result);
		mockRepo.Verify(r => r.AddUser(It.IsAny<User>()), Times.Never);
	}

	[Theory]
	[InlineData("Bob", "", "asdf1234")]
	[InlineData("Bob", null, "asdf1234")]
	public void RegisterUser_WithEmptyOrNullEmail_ReturnsFalse(string firstName, string email, string password)
	{
		var mockRepo = new Mock<IUserRepository>();
		var service = new UserService(mockRepo.Object);
		var newUser = new User(Guid.NewGuid(), firstName, "Bobbert", email, password, "User");

		var result = service.RegisterUser(newUser);

		Assert.False(result);
		mockRepo.Verify(r => r.AddUser(It.IsAny<User>()), Times.Never);
	}

	[Theory]
	[InlineData("Bob", "bob@bobbert.nl", "")]
	[InlineData("Bob", "bob@bobbert.nl", null)]
	public void RegisterUser_WithEmptyOrNullPassword_ReturnsFalse(string firstName, string email, string password)
	{
		var mockRepo = new Mock<IUserRepository>();
		var service = new UserService(mockRepo.Object);
		var newUser = new User(Guid.NewGuid(), firstName, "Bobbert", email, password, "User");

		var result = service.RegisterUser(newUser);

		Assert.False(result);
		mockRepo.Verify(r => r.AddUser(It.IsAny<User>()), Times.Never);
	}

	[Theory]
	[InlineData("")]
	[InlineData(null)]
	public void RegisterUser_WithEmptyOrNullRole_DefaultsToUserRole(string role)
	{
		var mockRepo = new Mock<IUserRepository>();
		var service = new UserService(mockRepo.Object);
		var newUser = new User(Guid.NewGuid(), "Bob", "Bobbert", "bob@bobbert.nl", "asdf1234", role);

		var result = service.RegisterUser(newUser);

		Assert.True(result);
		Assert.Equal(UserRoles.User, newUser.Role);
		mockRepo.Verify(r => r.AddUser(newUser), Times.Once);
	}
}
