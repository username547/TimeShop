namespace TimeShop.Models.DTO
{
    public class UserOrdersQuery
    {
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public string StatusName { get; set; } = string.Empty;
    }
}
