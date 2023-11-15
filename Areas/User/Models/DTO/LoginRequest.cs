using System.ComponentModel.DataAnnotations;

namespace TimeShop.Areas.User.Models.DTO
{
	public class LoginRequest
	{
		[Required(ErrorMessage = "Email not specified")]
		[StringLength(50, ErrorMessage = "The email is too long")]
		[EmailAddress(ErrorMessage = "Invalid email address")]
		public string Email { get; set; } = string.Empty;

		[Required(ErrorMessage = "Password not specified")]
		[DataType(DataType.Password)]
		public string Password { get; set; } = string.Empty;
	}
}
