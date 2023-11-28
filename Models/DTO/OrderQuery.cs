namespace TimeShop.Models.DTO
{
    public class OrderQuery
    {
        public int UserId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string UserSurname { get; set; } = string.Empty;
        public string UserEmail { get; set; } = string.Empty;
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public string StatusName { get; set; } = string.Empty;
        public int OrderItemId { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public decimal ProductPrice { get; set; }
        public string ProductImage { get; set; } = string.Empty;
        public int ProductQuantity { get; set; }
        public int TotalProductPrice { get; set; }
        public int TotalPurchasedItems { get; set; }
        public int TotalCartPrice { get; set; }
    }
}
