using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MusicForge.BLL.Services;
using MusicForge.Domain.Models;

namespace MusicForge.Web.Pages
{
	public class ArticleBrowserModel : PageModel
	{
		public List<Article> _allArticles = [];
		readonly UserService _userService;
		readonly ArticleService _articleService;

		public ArticleBrowserModel(UserService userService, ArticleService articleService)
		{
			_userService = userService;
			_articleService = articleService;
		}

		public void OnGet()
		{
			_allArticles = _articleService.GetAllArticles();
		}

		public string GetArticleAuthorName(Article article)
		{
			User articleAuthor = _userService.GetUserById(article.UserId);
			if (articleAuthor != null)
			{
				return articleAuthor.FirstName + " " + articleAuthor.LastName;
			}
			else
			{
				return "Error fetching name";
			}

		}
	}
}
