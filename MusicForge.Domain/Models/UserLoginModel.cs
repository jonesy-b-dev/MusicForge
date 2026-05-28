using System.ComponentModel.DataAnnotations;

namespace MusicForge.Domain.Models
{
    public class UserLoginModel
    {
		[Required(ErrorMessage = "Please provide a valid email adress")]
		public required string Email { get; set; }

		[Required(ErrorMessage = "Please provide your passward")]
		[DataType(DataType.Password)]
		public required string Password { get; set; }
    }
}
