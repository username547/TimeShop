using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using TimeShop.Models;

namespace TimeShop.Areas.User.Models
{
	public class UserModel
	{
		[Key]
		public int UserId { get; set; }

		[Required]
		[StringLength(50)]
		public string UserName { get; set; } = string.Empty;

		[Required]
		[StringLength(50)]
		public string UserSurname { get; set; } = string.Empty;

		[Required]
		[StringLength(50)]
		[EmailAddress]
		public string UserEmail { get; set; } = string.Empty;

		[Required]
		public string UserPasswordHash { get; set; } = string.Empty;

		[ForeignKey("RoleId")]
		public int? RoleId { get; set; }
		public RoleModel? Role { get; set; }

		public ICollection<CartModel>? Carts { get; set; }
    }
}
