using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TimeShop.Areas.User.Models;
using TimeShop.Data;
using TimeShop.Models;
using TimeShop.Models.DTO;

namespace TimeShop.Areas.Admin.Controllers
{
	[Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class UserManagementController : Controller
	{
		private readonly ApplicationDbContext _context;

		public UserManagementController(ApplicationDbContext context)
		{
			_context = context;
		}

		[HttpGet]
		public IActionResult Index()
		{
            IEnumerable<UserModel> users = _context.Users.ToList();
            return View(users);
		}

		[HttpGet]
		public IActionResult Orders(int? userId)
		{
			var user = _context.Users.FirstOrDefault(x => x.UserId == userId);

			if (user == null)
				return NotFound();

            var query = from u in _context.Users
                        join o in _context.Orders on u.UserId equals o.UserId
                        join s in _context.Statuses on o.StatusId equals s.StatusId
                        where u.UserId == user.UserId
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
            var order = _context.Orders.FirstOrDefault(x => x.OrderId == orderId);

            if (order == null)
                return NotFound();

            var query = from u in _context.Users
                        join o in _context.Orders on u.UserId equals o.UserId
                        join oi in _context.OrderItems on o.OrderId equals oi.OrderId
                        join p in _context.Products on oi.ProductId equals p.ProductId
                        join s in _context.Statuses on o.StatusId equals s.StatusId
                        where o.OrderId == order.OrderId
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

        [HttpGet]
        public IActionResult UpdateOrder(int? orderId)
        {
            var order = _context.Orders.FirstOrDefault(x => x.OrderId == orderId);

            if (order == null)
                return NotFound();

            ViewBag.StatusList = _context.Statuses.ToList();

            return View(order);
        }

        [HttpPost]
        public IActionResult UpdateOrder(OrderModel request)
        {
            if (!ModelState.IsValid)
                return View(request);

            var order = _context.Orders.FirstOrDefault(x => x.OrderId == request.OrderId);

            if (order == null)
                return NotFound();

            order.StatusId = request.StatusId;

            _context.SaveChanges();

            return RedirectToAction("Order", new { orderId = order.OrderId });
        }
	}
}