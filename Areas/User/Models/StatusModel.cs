using System.ComponentModel.DataAnnotations;

namespace TimeShop.Areas.User.Models
{
    public class StatusModel
    {
        [Key]
        public int StatusId { get; set; }

        [Required(ErrorMessage = "Status name not specified")]
        [StringLength(50, ErrorMessage = "Status name is too long")]
        public string StatusName { get; set; } = string.Empty;

        public ICollection<OrderModel>? Orders { get; set; }
    }
}
