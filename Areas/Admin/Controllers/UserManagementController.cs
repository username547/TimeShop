using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TimeShop.Areas.User.Models;
using TimeShop.Data;

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
			IEnumerable<UserModel> users = _context.Users.Where(x => x.RoleId == 2);
			return View(users);
		}
	}
}