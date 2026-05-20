using Microsoft.Data.SqlClient;
using MusicForge.Domain.Interfaces;
using MusicForge.Domain.Models;

namespace MusicForge.DAL.Repositories
{
	public class ArticleRepostiry : IArticleRepository
	{
		private readonly string _connectionString;
		public ArticleRepostiry()
		{
			string password = Environment.GetEnvironmentVariable("DB_PASSWORD") ?? "Rasa_!1071!";

			_connectionString = $"Server=mssqlstud.fhict.local;Database=dbi572325;User Id=dbi572325;Password={password};TrustServerCertificate=True;";

		}
		public void AddArticle(Article article)
		{
			string query = "";
			int result = -1;
			try
			{
				using (SqlConnection connection = new(_connectionString))
				{
					query = "INSERT INTO Articles VALUES (@Title, @Content, @User_Id, @Upvotes, @Creation_Date);";

					SqlCommand command = new(query, connection);

					command.Parameters.AddWithValue("@Title", article.Title);
					command.Parameters.AddWithValue("@Content", article.Content);
					command.Parameters.AddWithValue("@User_Id", article.userId);
					command.Parameters.AddWithValue("@Upvotes", article.Upvotes);
					command.Parameters.AddWithValue("@Creation_Date", article.CreationDate);

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
}
