namespace MusicForge.Domain.Models
{
	public class User
	{
		public User(
				Guid id,
				string firstName,
				string lastName,
				string email,
				string password,
				string role
				)
		{
			Id = id;
			FirstName = firstName;
			LastName = lastName;
			Email = email;
			Password = password;
			Role = role;
		}

		public User() { }

		public Guid Id { get; }
		public string? FirstName { get; }
		public string? LastName { get; }
		public string? Email { get; }
		public string? Password { get; set; }
		public string? Role { get; set; }
	}
}
