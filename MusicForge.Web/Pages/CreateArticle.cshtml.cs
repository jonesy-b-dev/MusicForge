using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MusicForge.BLL.Services;
using MusicForge.Domain.Models;

namespace MusicForge.Web.Pages;

[Authorize(Roles = UserRoles.Writer)]
public class CreateArticle : PageModel
{
	[BindProperty]
	public string Title { get; set; } = string.Empty;

	[BindProperty]
	public IFormFile? UploadedFile { get; set; }

	public string? FileContents { get; set; }
	public string? ErrorMessage { get; set; }
	readonly ArticleService _articleService;

	public CreateArticle(ArticleService articleService)
	{
		_articleService = articleService;
	}
	public void OnGet() { }

	public async Task<IActionResult> OnPostAsync()
	{
		if (!ModelState.IsValid)
			return Page();

		if (UploadedFile == null || UploadedFile.Length == 0)
		{
			ErrorMessage = "Please select a file.";
			return Page();
		}

		using var stream = UploadedFile.OpenReadStream();


		if (!_articleService.UploadArticle(Title, stream, UploadedFile.FileName, Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value)))
		{
			TempData["Failed"] = "Article upload to database was not successfull please try again";
			return RedirectToPage();
		}
		TempData["Success"] = "Article uploaded successfully!";

		return RedirectToPage();
	}
}


