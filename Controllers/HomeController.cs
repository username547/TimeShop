﻿using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TimeShop.Data;
using TimeShop.Models;

namespace TimeShop.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
		{
			_logger = logger;
			_context = context;
		}

		[HttpGet]
		public IActionResult Index()
		{
            IEnumerable<ProductModel> products = _context.Products.ToList();
            return View(products);
		}

		public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}