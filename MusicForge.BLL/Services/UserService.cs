using System;
using MusicForge.DAL.Repositories;
using MusicForge.Domain.Models;

namespace MusicForge.BLL.Services;

public class UserService
{
	private readonly UserRepository _userRepository;

	public UserService(UserRepository userRepository)
	{
		_userRepository = userRepository;
	}

	public void RegisterUser(User newUser)
	{
		_userRepository.AddUser(newUser);
	}
}
