using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MusicForge.Domain.Models;

namespace MusicForge.Web.Pages;

public class RegisterModel : PageModel
{
	[BindProperty]
	public UserRegisterModel UserRegisterModel { get; set; }

	public void OnGet()
	{
		UserRegisterModel = new();
	}

	public async Task<IActionResult> OnPost()
	{
		if(!ModelState.IsValid)
			return new RedirectToPageResult("/");

		return new RedirectToPageResult("/Privacy");

	}
}
