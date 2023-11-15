using System.ComponentModel.DataAnnotations;

namespace TimeShop.Areas.User.Models
{
	public class RoleModel
	{
		[Key]
		public int RoleId { get; set; }

		[Required(ErrorMessage = "Role name not specified")]
		[StringLength(50, ErrorMessage = "Role name is too long")]
		public string RoleName { get; set; } = string.Empty;

        public ICollection<UserModel>? Users { get; set; }
    }
}