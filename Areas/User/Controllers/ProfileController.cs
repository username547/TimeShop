using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using TimeShop.Areas.User.Models;
using TimeShop.Data;
using TimeShop.Models.DTO;

namespace TimeShop.Areas.User.Controllers
{
	[Area("User")]
	[Authorize]
	public class ProfileController : Controller
	{
		private readonly ApplicationDbContext _context;

		public ProfileController(ApplicationDbContext context)
		{
			_context = context;
		}

		[HttpGet]
		public IActionResult Index()
		{
			var currentUser = GetCurrentUser();

			return View(currentUser);
		}

		[HttpGet]
		public IActionResult Orders()
		{
			var currentUser = GetCurrentUser();

            var query = from u in _context.Users
                        join o in _context.Orders on u.UserId equals o.UserId
                        join s in _context.Statuses on o.StatusId equals s.StatusId
                        where u.UserId == currentUser.UserId
                        select new UserOrdersQuery
                        {
                            OrderId = o.OrderId,
                            OrderDate = o.OrderDate,
                            StatusName = s.StatusName,
                        };

            return View(query.ToList());
		}

        [HttpGet]
        public IActionResult Order(int? orderId)
        {
            var currentUser = GetCurrentUser();

            var order = _context.Orders.FirstOrDefault(x => x.OrderId == orderId);

            if (order == null)
                return BadRequest("Order not found");

            var query = from u in _context.Users
                        join o in _context.Orders on u.UserId equals o.UserId
                        join oi in _context.OrderItems on o.OrderId equals oi.OrderId
                        join p in _context.Products on oi.ProductId equals p.ProductId
                        join s in _context.Statuses on o.StatusId equals s.StatusId
                        where u.UserId == currentUser.UserId && o.OrderId == order.OrderId
                        select new OrderQuery
                        {
                            UserId = u.UserId,
                            UserName = u.UserName,
                            UserSurname = u.UserSurname,
                            UserEmail = u.UserEmail,
                            OrderId = o.OrderId,
                            OrderDate = o.OrderDate,
                            StatusName = s.StatusName,
                            OrderItemId = oi.OrderItemId,
                            ProductId = oi.ProductId,
                            ProductName = p.ProductName,
                            ProductPrice = p.ProductPrice,
                            ProductImage = p.ProductImage,
                            ProductQuantity = oi.ProductQuantity,
                            TotalProductPrice = oi.ProductQuantity * p.ProductPrice,
                            TotalPurchasedItems = _context.OrderItems.Where(x => x.OrderId == o.OrderId).Sum(x => x.ProductQuantity),
                            TotalCartPrice = _context.OrderItems
                                .Where(x => x.Order!.UserId == u.UserId && x.OrderId == o.OrderId)
                                .Sum(x => x.ProductQuantity * x.Product!.ProductPrice)
                        };

            return View(query.ToList());
        }

        public UserModel GetCurrentUser()
        {
            var claim = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email);
            string email = claim!.Value;

            var currentUser = _context.Users.FirstOrDefault(x => x.UserEmail == email);

            return currentUser!;
        }
    }
}