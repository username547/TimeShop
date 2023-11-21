namespace TimeShop.Models
{
    public class Result
    {
        public int UserId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string UserSurname { get; set; } = string.Empty;
        public int CartId { get; set; }
        public int CardItemId { get; set; }
        public int ProductId { get; set; }
        public int ProductQuantity { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public int ProductPrice { get; set; }
        public string ProductImage { get; set; } = string.Empty;
    }
}
