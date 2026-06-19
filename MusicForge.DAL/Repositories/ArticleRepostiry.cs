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
					query = "INSERT INTO Articles VALUES (@Id, @Title, @Path, @User_Id, @Upvotes, @Creation_Date);";

					SqlCommand command = new(query, connection);

					command.Parameters.AddWithValue("@Id", article.Id);
					command.Parameters.AddWithValue("@Title", article.Title);
					command.Parameters.AddWithValue("@Path", article.Path);
					command.Parameters.AddWithValue("@User_Id", article.UserId);
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

		public List<Article> GetAllArticles()
		{
			string query = "";
			List<Article> articles = new();
			try
			{
				using (SqlConnection connection = new(_connectionString))
				{
					query = "SELECT * FROM Articles;";
					SqlCommand command = new(query, connection);
					connection.Open();
					using (SqlDataReader reader = command.ExecuteReader())
					{
						while (reader.Read())
						{
							articles.Add(new Article(
									(Guid)reader["id"],
									(Guid)reader["user_id"],
									(string)reader["title"],
									(string)reader["filePath"],
									(int)reader["upvotes"],
									(DateTime)reader["created_at"]
								)
							);
						}
					}
				}
			}
			catch (Exception e)
			{
				Console.WriteLine($"Failed to Retrieve.\n Query: {query}\n Exception: {e}");
				return new();
			}
			return articles;
		}
		public List<Article> GetAllArticlesFromWriter(Guid userId)
		{
			string query = "";
			List<Article> articles = [];
			try
			{
				using (SqlConnection connection = new(_connectionString))
				{
					query = "SELECT * FROM Articles WHERE user_id = @User_Id;";
					SqlCommand command = new(query, connection);

					command.Parameters.AddWithValue("@User_Id", userId);

					connection.Open();
					using (SqlDataReader reader = command.ExecuteReader())
					{
						while (reader.Read())
						{
							articles.Add(new Article(
									(Guid)reader["id"],
									(Guid)reader["user_id"],
									(string)reader["title"],
									(string)reader["filePath"],
									(int)reader["upvotes"],
									(DateTime)reader["created_at"]
								)
							);
						}
					}
				}
			}
			catch (Exception e)
			{
				Console.WriteLine($"Failed to Retrieve.\n Query: {query}\n Exception: {e}");
				return new();
			}
			return articles;
		}
	}
}
