using System.ComponentModel.DataAnnotations;

namespace MusicForge.Domain.Models
{
    public class UserRegisterModel
    {
		[Required]
		string UserName { get; set; }
		[Required]
		string Password { get; set; }
		[Required]
		string PasswordRepeat { get; set; }
    }
}
