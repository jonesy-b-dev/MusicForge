namespace MusicForge.Domain.Models
{
	public class Article
	{
		public User Author { get; set; }
		public string Title { get; set; }
		public string Content { get; set; }
		public int Upvotes { get; set; }
		public DateTime CreationDate { get; set; }
	}
}
