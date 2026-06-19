using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
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

	public void OnGet() { }

	public async Task<IActionResult> OnPost()
	{
		if (!ModelState.IsValid)
			return Page();

		Guid userGuid = _userService.TryLoginUser(UserLoginModel.Email, UserLoginModel.Password);

		if (userGuid == Guid.Empty)
		{
			TempData["Failed"] = "Password or email is incorrect";
			return Page();
		}

		User loggedInUser = _userService.GetUserById(userGuid);
		if (loggedInUser == null)
		{
			TempData["Failed"] = "Failed to fetch userdata please try again";
			return RedirectToPage();
		}

		List<Claim> claims =
		[
			new Claim(ClaimTypes.Role, loggedInUser.Role ?? throw new InvalidOperationException("Role is required")),
			new Claim(ClaimTypes.NameIdentifier, userGuid.ToString())
		];

		ClaimsIdentity claimIdentity = new(claims, CookieAuthenticationDefaults.AuthenticationScheme);
		await HttpContext.SignInAsync(new ClaimsPrincipal(claimIdentity));

		return new RedirectToPageResult("/Account");
	}
}
