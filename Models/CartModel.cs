using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using TimeShop.Areas.User.Models;

namespace TimeShop.Models
{
    public class CartModel
    {
        [Key]
        public int CartId { get; set; }

        [ForeignKey("UserId")]
        public int UserId { get; set; }
        public UserModel? User { get; set; }

        [ForeignKey("StatusId")]
        public int StatusId { get; set; }
        public StatusModel? Status { get; set; }

        public ICollection<CartItemModel>? CartItems { get; set; }
    }
}
