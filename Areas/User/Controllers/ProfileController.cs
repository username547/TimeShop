using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using TimeShop.Areas.User.Models;
using TimeShop.Data;

namespace TimeShop.Areas.User.Controllers
{
	[Area("user")]
	public class ProfileController : Controller
	{
		private readonly ApplicationDbContext _context;

		public ProfileController(ApplicationDbContext context)
		{
			_context = context;
		}

		[HttpGet]
		[Authorize]
		public async Task<IActionResult> Index()
		{
			var emailClaim = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email);
			string email = emailClaim.Value;

			var currentUser = await _context.Users.FirstOrDefaultAsync(x => x.UserEmail == email);

			return View(currentUser);
		}
	}
}