using Moq;
using MusicForge.BLL.Services;
using MusicForge.Domain.Interfaces;
using MusicForge.Domain.Models;

namespace MusicForge.Testing;

public class UserServiceTests
{
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
		mockRepo.Setup(r => r.GetUserById(It.IsAny<Guid>()))
				.Returns((User)null); // simulate user not found

		var service = new UserService(mockRepo.Object);

		// Act
		var result = service.GetUserById(Guid.NewGuid());

		// Assert
		Assert.Null(result);
	}
}
