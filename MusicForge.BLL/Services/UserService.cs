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

	public void LoginUser(User user)
	{

	}
	public void RegisterUser(User newUser)
	{
		_userRepository.AddUser(newUser);
	}
}
