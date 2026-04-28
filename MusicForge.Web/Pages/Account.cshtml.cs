using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MusicForge.Domain.Models;

namespace MusicForge.Web.Pages;

[Authorize(Roles = UserRoles.User)]
public class AccountModel : PageModel
{
	public void OnGet()
	{
	}
}
