using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TimeShop.Models
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

        [Required]
        public int ProductQuantity { get; set; }
    }
}
