using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MusicForge.Domain.Models;
using MusicForge.BLL.Services;
using System.Security.Claims;

namespace MusicForge.Web.Pages;

[Authorize(Roles = $"{UserRoles.User},{UserRoles.Writer},{UserRoles.Admin}")]
public class AccountModel : PageModel
{
	readonly UserService _userService;
	public User? loggedInUser;
	public AccountModel(UserService userService)
	{
		_userService = userService;
	}
	public void OnGet()
	{
		Guid guid = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value
				?? throw new InvalidOperationException("User identifier claim is missing on Account page load"));
		loggedInUser = _userService.GetUserById(guid);
	}
}
