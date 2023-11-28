using System.ComponentModel.DataAnnotations;

namespace TimeShop.Models
{
    public class StatusModel
    {
        [Key]
        public int StatusId { get; set; }

        [Required]
        [StringLength(50)]
        public string StatusName { get; set; } = string.Empty;

        public ICollection<OrderModel>? Orders { get; set; }
    }
}
