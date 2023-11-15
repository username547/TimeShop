using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using TimeShop.Areas.User.Models;
using TimeShop.Areas.User.Models.DTO;
using TimeShop.Data;

namespace TimeShop.Areas.User.Controllers
{
	[Area("user")]
	public class AccountController : Controller
	{
		private readonly ApplicationDbContext _context;

		public AccountController(ApplicationDbContext context)
		{
			_context = context;
		}

		[HttpGet]
		public IActionResult Signup()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Signup(SignupRequest request)
		{
			if (ModelState.IsValid)
			{
				var checkEmail = await _context.Users.FirstOrDefaultAsync(x => x.UserEmail == request.Email);

				if (checkEmail != null)
                    return BadRequest("Email already exists");

                UserModel user = new UserModel
				{
					UserName = request.Name,
					UserSurname = request.Surname,
					UserEmail = request.Email,
					UserPasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
					RoleId = 3
				};

				_context.Users.Add(user);
				await _context.SaveChangesAsync();

				return RedirectToAction("Login");
			}

			return View(request);
		}

		[HttpGet]
		public IActionResult Login()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Login(LoginRequest request)
		{
			if (!ModelState.IsValid)
                return BadRequest();

            var user = await _context.Users.FirstOrDefaultAsync(x => x.UserEmail == request.Email);

			if (user == null)
				return BadRequest("Email is not valid");

			if (!BCrypt.Net.BCrypt.Verify(request.Password, user.UserPasswordHash))
				return BadRequest("Password is not valid");

			var role = await _context.Roles.FirstOrDefaultAsync(x => x.RoleId == user.RoleId);

			List<Claim> claims = new List<Claim>
			{
				new Claim(ClaimTypes.Email, user.UserEmail),
				new Claim(ClaimTypes.Role, role.RoleName)
			};

			var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

			AuthenticationProperties authProperties = new AuthenticationProperties
			{
				AllowRefresh = true,
				ExpiresUtc = DateTimeOffset.Now.AddHours(12),
				IsPersistent = true,
			};

			await HttpContext.SignInAsync(
				CookieAuthenticationDefaults.AuthenticationScheme,
				new ClaimsPrincipal(claimsIdentity),
				authProperties);

			return Redirect("/Home");
		}

		[HttpGet]
		[Authorize]
		public async Task<IActionResult> Logout()
		{
			await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

			return Redirect("/Home");
		}

		[HttpGet]
		public IActionResult AccessDenied()
		{
			return View();
		}
	}
}
