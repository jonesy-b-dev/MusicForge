using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MusicForge.Domain.Models;
using MusicForge.BLL.Services;
using System.Security.Claims;

namespace MusicForge.Web.Pages;

[Authorize(Roles = UserRoles.User)]
public class AccountModel : PageModel
{
	private UserService _userService;
	public User loggedInUser;
	public AccountModel(UserService userService)
	{
		_userService = userService;
	}
	public void OnGet()
	{
		Guid guid = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
		loggedInUser = _userService.GetUserById(guid);
	}
}
