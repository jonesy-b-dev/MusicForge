namespace MusicForge.Domain.Models
{
	public class User
	{
		public User(
				string firstName,
				string lastName,
				string email,
				string password,
				string role
				)
		{
			FirstName = firstName;
			LastName = lastName;
			Email = email;
			Password = password;
			Role = role;
		}

		public User(){}

		public string? FirstName { get; private set; }
		public string? LastName { get;  private set; }
		public string? Email { get;  private set; }
		public string? Password { get;  private set; }
		public string? Role { get;  private set; }
	}
}
