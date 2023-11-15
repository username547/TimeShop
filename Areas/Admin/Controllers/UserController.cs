using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TimeShop.Areas.User.Models;
using TimeShop.Data;

namespace TimeShop.Areas.Admin.Controllers
{
	[Area("admin")]
    [Authorize(Roles = ("Admin"))]
    public class UserController : Controller
	{
		private readonly ApplicationDbContext _context;

		public UserController(ApplicationDbContext context)
		{
			_context = context;
		}

		[HttpGet]
		public IActionResult Index()
		{
			IEnumerable<UserModel> users = _context.Users.Where(x => x.RoleId == 3);
			return View(users);
		}
	}
}
