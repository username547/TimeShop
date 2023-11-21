namespace TimeShop.Models.DTO
{
    public class CartQuery
    {
        public int UserId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string UserSurname { get; set; } = string.Empty;
        public string UserEmail { get; set; } = string.Empty;
        public int CartId { get; set; }
        public int CartItemId { get; set; }
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
