using System.ComponentModel.DataAnnotations;

namespace MusicForge.Domain.Models
{
    public class UserRegisterModel
    {
		[Required]
		public required string FirstName { get; set; }

		public string? LastName { get; set; }

		[Required(ErrorMessage = "Please provide a valid email adress")]
		[EmailAddress]
		public required string Email { get; set; }

		[Required(ErrorMessage = "Please provide a strong password")]
		[DataType(DataType.Password)]
		public required string Password { get; set; }

		[Required(ErrorMessage = "Please repeat you password")]
		[Compare("Password", ErrorMessage = "Repeat Password must be the same as password")]
		[DataType(DataType.Password)]
		public required string PasswordRepeat { get; set; }
    }
}
