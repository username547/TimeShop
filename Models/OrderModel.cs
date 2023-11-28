using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TimeShop.Areas.User.Models;

namespace TimeShop.Models
{
    public class OrderModel
    {
        [Key]
        public int OrderId { get; set; }

        [ForeignKey("UserId")]
        public int UserId { get; set; }
        public UserModel? User { get; set; }

        [ForeignKey("StatusId")]
        public int StatusId { get; set; }
        public StatusModel? Status { get; set; }

        public DateTime OrderDate { get; set; }

        public ICollection<OrderItemModel>? OrderItems { get; set; }
    }
}
