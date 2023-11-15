using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TimeShop.Areas.User.Models
{
    public class OrderModel
    {
        [Key]
        public int OrderId { get; set; }

        [ForeignKey("StatusId")]
        public int StatusId { get; set; }
        public StatusModel? Status { get; set; }

        [ForeignKey("UserId")]
        public int UserId { get; set; }
        public UserModel? User { get; set; }

        public ICollection<OrderItemModel>? OrderItems { get; set; }
    }
}