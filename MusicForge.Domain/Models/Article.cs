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
		public Guid Id { get; }
		public Guid UserId { get; }
		public string Title { get; }
		public string Path { get; }
		public int Upvotes { get; }
		public DateTime CreationDate { get; }
	}
}
