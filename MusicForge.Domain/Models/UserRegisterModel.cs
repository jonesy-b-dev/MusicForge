using System.ComponentModel.DataAnnotations;

namespace MusicForge.Domain.Models
{
    public class UserRegisterModel
    {
		[Required]
		public string FirstName { get; set; }

		public string? LastName { get; set; }

		[Required]
		public string Email { get; set; }

		[Required]
		public string Password { get; set; }

		[Required]
		public string PasswordRepeat { get; set; }
    }
}
