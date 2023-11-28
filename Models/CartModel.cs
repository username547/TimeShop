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

        public ICollection<CartItemModel>? CartItems { get; set; }
    }
}
