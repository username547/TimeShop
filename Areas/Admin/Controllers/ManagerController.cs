using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TimeShop.Areas.User.Models;
using TimeShop.Areas.User.Models.DTO;
using TimeShop.Data;

namespace TimeShop.Areas.Admin.Controllers
{
    [Area("admin")]
    [Authorize(Roles = ("Admin"))]
    public class ManagerController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ManagerController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            IEnumerable<UserModel> users = _context.Users.Where(x => x.RoleId == 2);
            return View(users);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(SignupRequest request)
        {
            if (!ModelState.IsValid)
                return View(request);

			var checkEmail = _context.Users.FirstOrDefault(x => x.UserEmail == request.Email);

			if (checkEmail != null)
				return BadRequest("Email already exists");

			UserModel manager = new UserModel
			{
				UserName = request.Name,
				UserSurname = request.Surname,
				UserEmail = request.Email,
				UserPasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
				RoleId = 2
			};

			_context.Users.Add(manager);
			_context.SaveChanges();

			return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Update(int? userId)
        {
            var user = _context.Users.Find(userId);

            if (user == null)
                return BadRequest("User not found");

            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(UserModel user)
        {
            if(!ModelState.IsValid)
                return View(user);

			_context.Users.Update(user);
			_context.SaveChanges();

			return RedirectToAction("Index");
		}

        [HttpGet]
        public IActionResult Delete(int? userId)
        {
			var user = _context.Users.Find(userId);

			if (user == null)
				return BadRequest("User not found");

            _context.Users.Remove(user);
            _context.SaveChanges();

            return RedirectToAction("Index");
		}
    }
}
