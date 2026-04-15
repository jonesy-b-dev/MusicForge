using MusicForge.Domain.Models;

namespace MusicForge.Domain.Interfaces;

public interface IUserRepository
{
	void AddUser(User newUser);
}
