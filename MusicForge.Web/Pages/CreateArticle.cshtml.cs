using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
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

		using var reader = new StreamReader(UploadedFile.OpenReadStream());
		FileContents = await reader.ReadToEndAsync();

		//TODO: Send to BLL and upload to db in DAL
		return Page();
	}
}
