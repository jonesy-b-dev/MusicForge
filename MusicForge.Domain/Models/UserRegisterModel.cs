using System.ComponentModel.DataAnnotations;

namespace MusicForge.Domain.Models
{
    public class UserRegisterModel
    {
		[Required]
		public string FirstName { get; set; }

		public string? LastName { get; set; }

		[Required(ErrorMessage = "Please provide a valid email adress")]
		[EmailAddress]
		public string Email { get; set; }

		[Required(ErrorMessage = "Please provide a strong password")]
		[DataType(DataType.Password)]
		public string Password { get; set; }

		[Required(ErrorMessage = "Repeat Password must be the same as password")]
		[DataType(DataType.Password)]
		public string PasswordRepeat { get; set; }
    }
}
