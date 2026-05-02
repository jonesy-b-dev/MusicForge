using MusicForge.Domain.Interfaces;
using MusicForge.Domain.Models;

namespace MusicForge.BLL.Services;

public class UserService
{
	private readonly IUserRepository _userRepository;

	public UserService(IUserRepository userRepository)
	{
		_userRepository = userRepository;
	}

	public Guid TryLoginUser(string email, string password)
	{
		return _userRepository.ValidateUser(email, password);
	}
	public void RegisterUser(User newUser)
	{
		_userRepository.AddUser(newUser);
	}

	//public User GetUser()
}
