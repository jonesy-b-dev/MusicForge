using Microsoft.Data.SqlClient;

using MusicForge.Domain.Models;

namespace MusicForge.DAL.Repositories;

public class UserRepository
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
}
