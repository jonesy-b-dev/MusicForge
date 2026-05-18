namespace MusicForge.Domain.Models
{
	public class Article
	{
		public Guid userId { get; set; }
		public string Title { get; set; }
		public string Content { get; set; }
		public int Upvotes { get; set; }
		public DateTime CreationDate { get; set; }
	}
}
