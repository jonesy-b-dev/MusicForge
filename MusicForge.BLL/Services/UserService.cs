using MusicForge.Domain.Interfaces;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;
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
		if (Equals(email, string.Empty) || email == null)
			return Guid.Empty;

		if (Equals(password, string.Empty) || password == null)
			return Guid.Empty;

		return _userRepository.ValidateUser(email, password);
	}
	public bool RegisterUser(User newUser)
	{
		if (newUser == null)
			return false;
		if (Equals(newUser.FirstName, string.Empty) || newUser.FirstName == null)
			return false;
		if (Equals(newUser.Email, string.Empty) || newUser.Email == null)
			return false;
		if (Equals(newUser.Password, string.Empty) || newUser.Password == null)
			return false;
		if (Equals(newUser.Role, string.Empty) || newUser.Role == null)
			newUser.Role = UserRoles.User;

		newUser.Password = HashPassword(newUser.Password);

		_userRepository.AddUser(newUser);

		return true;
	}

	public User GetUserById(Guid id)
	{
		if (id == Guid.Empty)
			return null;

		return _userRepository.GetUserById(id);
	}
	private string HashPassword(string unhashedPassword)
	{
		byte[] salt = RandomNumberGenerator.GetBytes(128 / 8);

		// derive a 256-bit subkey (use HMACSHA256 with 100,000 iterations)
		string hash = Convert.ToBase64String(KeyDerivation.Pbkdf2(
			password: unhashedPassword,
			salt: salt,
			prf: KeyDerivationPrf.HMACSHA256,
			iterationCount: 100000,
			numBytesRequested: 256 / 8));

		return $"{Convert.ToBase64String(salt)}:{hash}";
	}
}
