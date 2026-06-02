using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MusicForge.Domain.Models;

namespace MusicForge.Web.Pages
{
    public class ArticleBrowserModel : PageModel
    {
		public List<Article> mockArticles = [
			new Article(Guid.NewGuid(),
					Guid.NewGuid(),
					"Article1",
					"path/to/article",
					-12,
					DateTime.Now),
			new Article(Guid.NewGuid(),
					Guid.NewGuid(),
					"Article2",
					"path/to/article",
					15,
					DateTime.Now),
			new Article(Guid.NewGuid(),
					Guid.NewGuid(),
					"Article3",
					"path/to/article",
					16,
					DateTime.Now),
			new Article(Guid.NewGuid(),
					Guid.NewGuid(),
					"Article4",
					"path/to/article",
					12,
					DateTime.Now),
		];

        public void OnGet()
        {
        }
    }
}
