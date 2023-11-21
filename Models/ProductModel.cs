using System.ComponentModel.DataAnnotations;

namespace TimeShop.Models
{
    public class ProductModel
    {
        [Key]
        public int ProductId { get; set; }

        [Required(ErrorMessage = "Product name not specified")]
        [StringLength(50, ErrorMessage = "The product name is too long")]
        public string ProductName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Product price not specified")]
        public int ProductPrice { get; set; }

        public string ProductImage { get; set; } = string.Empty;

        public ICollection<CartItemModel>? CartItems { get; set; }
    }
}
