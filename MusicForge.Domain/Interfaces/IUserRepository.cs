using MusicForge.Domain.Models;

namespace MusicForge.Domain.Interfaces;

public interface IUserRepository
{
	void AddUser(User newUser);
	Guid ValidateUser(string email, string password);
}
