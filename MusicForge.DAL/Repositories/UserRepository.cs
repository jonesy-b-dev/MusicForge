using System.Data;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;
using Microsoft.Data.SqlClient;
using MusicForge.Domain.Interfaces;
using MusicForge.Domain.Models;

namespace MusicForge.DAL.Repositories;

public class UserRepository : IUserRepository
{
	private readonly string _connectionString;

	public UserRepository()
	{
		string password = Environment.GetEnvironmentVariable("DB_PASSWORD") ?? "Rasa_!1071!";

		_connectionString = $"Server=mssqlstud.fhict.local;Database=dbi572325;User Id=dbi572325;Password={password};TrustServerCertificate=True;";

	}

	public void AddUser(User newUser)
	{
		string query = "";
		int result = -1;
		try
		{
			using (SqlConnection connection = new(_connectionString))
			{
				query = "INSERT INTO Users VALUES (@FirstName, @LastName, @Email, @Password, @Role, @Id);";

				SqlCommand command = new(query, connection);

				command.Parameters.AddWithValue("@FirstName", newUser.FirstName);
				command.Parameters.AddWithValue("@LastName", newUser.LastName);
				command.Parameters.AddWithValue("@Email", newUser.Email);
				command.Parameters.AddWithValue("@Password", HashPassword(newUser.Password));
				command.Parameters.AddWithValue("@Role", newUser.Role);
				command.Parameters.AddWithValue("@Id", Guid.NewGuid());

				connection.Open();
				result = command.ExecuteNonQuery();
			}
		}
		catch (Exception e)
		{
			Console.WriteLine($"Failed to Insert.\n Query: {query}, Result: {result}\n Exeption: {e}");
		}

	}

	public Guid ValidateUser(string email, string password)
	{
		string query = "";
		try
		{
			using (SqlConnection connection = new(_connectionString))
			{
				query = "SELECT Id, Password FROM Users WHERE Email = @Email;";

				SqlCommand command = new(query, connection);

				command.Parameters.AddWithValue("@Email", email);

				connection.Open();
				SqlDataReader reader = command.ExecuteReader();

				if (reader.Read())
				{
					string storedHash = reader["Password"].ToString()!;
					Guid userId = (Guid)reader["Id"];

					// Verify the password against the stored salt:hash
					if (VerifyPassword(password, storedHash))
						return userId;
				}

				return Guid.Empty;
			}
		}
		catch (Exception e)
		{
			Console.WriteLine($"Failed to validate user.\n Query: {query}, Exception: {e}");
			return Guid.Empty;
		}
	}

	public User GetUserById(Guid id)
	{
		string query = "";
		User resultUser = new();
		try
		{
			using (SqlConnection connection = new(_connectionString))
			{
				query = "SELECT FirstName, LastName, Email, Role FROM Users WHERE id= @userId;";
				SqlCommand command = new(query, connection);
				command.Parameters.AddWithValue("@userId", id);

				connection.Open();
				SqlDataReader reader = command.ExecuteReader();

				while (reader.Read())
				{
					resultUser = new User(
							id,
							(string)reader["FirstName"],
							(string)reader["LastName"],
							(string)reader["Email"],
							string.Empty,
							(string)reader["Role"]
							);
				}
			}
			return resultUser;
		}
		catch (Exception e)
		{
			Console.WriteLine($"Failed to Insert.\n Query: {query}, Student: {resultUser}, Exeption: {e}");
			return null;
		}

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
	private bool VerifyPassword(string password, string storedHash)
	{
		// Split out the salt and hash
		string[] parts = storedHash.Split(':');
		if (parts.Length != 2)
			return false;

		byte[] salt = Convert.FromBase64String(parts[0]);

		string hash = Convert.ToBase64String(KeyDerivation.Pbkdf2(
			password: password,
			salt: salt,
			prf: KeyDerivationPrf.HMACSHA256,
			iterationCount: 100000,
			numBytesRequested: 256 / 8));

		return hash == parts[1];
	}
}
