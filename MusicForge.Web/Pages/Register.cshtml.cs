using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MusicForge.BLL.Services;
using MusicForge.Domain.Models;

namespace MusicForge.Web.Pages;

public sealed class RegisterModel : PageModel
{
	[BindProperty]
	public UserRegisterModel UserRegisterModel { get; set; }

	readonly UserService _userService;

	public RegisterModel(UserService userService)
	{
		_userService = userService;
	}

	public void OnGet()
	{
		UserRegisterModel = new();
	}

	public async Task<IActionResult> OnPost()
	{
		if (!ModelState.IsValid)
			return Page();

		if (UserRegisterModel.Password != UserRegisterModel.PasswordRepeat)
			return Page();

		if (UserRegisterModel.LastName == null)
			UserRegisterModel.LastName = string.Empty;

		User newUser = new(
			UserRegisterModel.FirstName,
			UserRegisterModel.LastName,
			UserRegisterModel.Email,
			UserRegisterModel.Password,
			"User"
		);

		_userService.RegisterUser(newUser);
		return new RedirectToPageResult("/Login");
	}
}
