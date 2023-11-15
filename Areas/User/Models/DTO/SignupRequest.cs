using System.ComponentModel.DataAnnotations;

namespace TimeShop.Areas.User.Models.DTO
{
	public class SignupRequest
	{
		[Required(ErrorMessage = "Name not specified")]
		[StringLength(50, ErrorMessage = "The name is too long")]
		[MinLength(3, ErrorMessage = "The name is too short")]
		public string Name { get; set; } = string.Empty;

		[Required(ErrorMessage = "Surname not specified")]
		[StringLength(50, ErrorMessage = "The surname is too long")]
		[MinLength(3, ErrorMessage = "The surname is too short")]
		public string Surname { get; set; } = string.Empty;

		[Required(ErrorMessage = "Email not specified")]
		[StringLength(50, ErrorMessage = "The email is too long")]
		[EmailAddress(ErrorMessage = "Invalid email address")]
		public string Email { get; set; } = string.Empty;

		[Required(ErrorMessage = "Password not specified")]
		[DataType(DataType.Password)]
		public string Password { get; set; } = string.Empty;

		[Required(ErrorMessage = "Password not specified")]
		[Compare("Password", ErrorMessage = "The password and confirmation password do not match")]
		[DataType(DataType.Password)]
		public string ConfirmPassword { get; set; } = string.Empty;
	}
}
