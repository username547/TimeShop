using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TimeShop.Areas.User.Models
{
    public class OrderItemModel
    {
        [Key]
        public int OrderItemId { get; set; }

        [ForeignKey("OrderId")]
        public int OrderId { get; set; }
        public OrderModel? Order { get; set; }

        [ForeignKey("ProductId")]
        public int ProductId { get; set; }
        public ProductModel? Product { get; set; }
    }
}
