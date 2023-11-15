using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TimeShop.Areas.User.Models;
using TimeShop.Data;

namespace TimeShop.Areas.User.Controllers
{
	[Area("user")]
	public class ShopController : Controller
	{
		private readonly ApplicationDbContext _context;

		public ShopController(ApplicationDbContext context)
		{
			_context = context;
		}

		public IActionResult Index()
		{
			IEnumerable<ProductModel> products = _context.Products.ToList();
			return View(products);
		}
	}
}
