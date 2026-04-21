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

	public void OnGet()
	{
		UserLoginModel = new();
	}

	public async Task<IActionResult> OnPost()
	{
		if (!ModelState.IsValid)
			return Page();

		//Validate in database

		List<Claim> claims = new();
		claims.Add(new Claim(ClaimTypes.Email, UserLoginModel.Email));
		claims.Add(new Claim(ClaimTypes.Role, UserRoles.User));

		ClaimsIdentity claimIdentity = new(claims, CookieAuthenticationDefaults.AuthenticationScheme);
		HttpContext.SignInAsync(new ClaimsPrincipal(claimIdentity));

		return new RedirectToPageResult("/Account");
	}
}
