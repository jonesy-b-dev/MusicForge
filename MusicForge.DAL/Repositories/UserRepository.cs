using System.Data;
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
				query = "INSERT INTO Users VALUES (@FirstName, @LastName, @Email, @Password, @Role);";

				SqlCommand command = new(query, connection);

				command.Parameters.AddWithValue("@FirstName", newUser.FirstName);
				command.Parameters.AddWithValue("@LastName", newUser.LastName);
				command.Parameters.AddWithValue("@Email", newUser.Email);
				command.Parameters.AddWithValue("@Password", newUser.Password);
				command.Parameters.AddWithValue("@Role", newUser.Role);

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
				query = "SELECT Id,Email,Password FROM Users WHERE Email=@Email AND Password = @Password;";
				SqlCommand command = new(query, connection);
				command.Parameters.AddWithValue("@Email", email);
				command.Parameters.AddWithValue("@Password", password);

				connection.Open();
				SqlDataReader reader = command.ExecuteReader();

				Guid userGuid = Guid.Empty;

				while (reader.Read())
				{
					userGuid = (Guid)reader["Id"];
				}

				return userGuid;
			}
		}
		catch (Exception e)
		{
			Console.WriteLine($"Failed to validate user.\n Query: {query}, Exeption: {e}");
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
}
