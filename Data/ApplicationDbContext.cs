using Microsoft.EntityFrameworkCore;
using TimeShop.Areas.User.Models;
using TimeShop.Models;

namespace TimeShop.Data
{
	public class ApplicationDbContext : DbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
		{
		}
		public DbSet<RoleModel> Roles { get; set; }
		public DbSet<UserModel> Users { get; set; }
		public DbSet<StatusModel> Statuses { get; set; }
		public DbSet<CartModel> Carts { get; set; }
		public DbSet<CartItemModel> CartItems { get; set; }
		public DbSet<OrderModel> Orders { get; set; }
		public DbSet<OrderItemModel> OrderItems { get; set; }
		public DbSet<ProductModel> Products { get; set; }
	}
}