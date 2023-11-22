using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using TimeShop.Areas.User.Models;
using TimeShop.Data;

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

        public UserModel GetCurrentUser()
        {
            var claim = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email);
            string email = claim!.Value;

            var currentUser = _context.Users.FirstOrDefault(x => x.UserEmail == email);

            return currentUser!;
        }
    }
}