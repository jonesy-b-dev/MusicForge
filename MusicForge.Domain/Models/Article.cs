namespace MusicForge.Domain.Models
{
	public class Article
	{
		public Article(
				Guid id,
				Guid userId,
				string title,
				string path,
				int upvotes,
				DateTime creationDate
				)
		{
			Id = id;
			UserId = userId;
			Title = title;
			Path = path;
			Upvotes = upvotes;
			CreationDate = creationDate;
		}
		public Guid Id { get; set; }
		public Guid UserId { get; set; }
		public string Title { get; set; }
		public string Path { get; set; }
		public int Upvotes { get; set; }
		public DateTime CreationDate { get; set; }
	}
}
