using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MusicForge.BLL.Services;
using MusicForge.Domain.Models;

namespace MusicForge.Web.Pages;

public class LoginModel : PageModel
{
	[BindProperty]
	public UserLoginModel UserLoginModel { get; set; }
	readonly UserService _userService;
	public LoginModel(UserService userService)
	{
		_userService = userService;
	}

	public void OnGet()
	{
		UserLoginModel = new();
	}

	public async Task<IActionResult> OnPost()
	{
		return new RedirectToPageResult("/index");
	}
}
