using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TimeShop.Data;
using TimeShop.Models;

namespace TimeShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class ProductManagementController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductManagementController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            IEnumerable<ProductModel> products = _context.Products.ToList();
            return View(products);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Create(ProductModel request, IFormFile image)
		{
            if (!ModelState.IsValid || image == null)
				return View(request);

			string folder = Path.Combine(_webHostEnvironment.WebRootPath, "ProductImages");

			if (!Directory.Exists(folder))
				Directory.CreateDirectory(folder);

			string temp = Path.GetFileName(image.FileName);
			string timeStamp = DateTime.Now.ToString("yyyyMMddHHmmssfff");
			string imageName = $"{Path.GetFileNameWithoutExtension(temp)}_{timeStamp}{Path.GetExtension(temp)}";
			string imagePath = Path.Combine(folder, imageName);

			image.CopyTo(new FileStream(imagePath, FileMode.Create));

			ProductModel product = new ProductModel
			{
				ProductName = request.ProductName,
				ProductPrice = request.ProductPrice,
				ProductImage = imageName
			};

			_context.Products.Add(product);
			_context.SaveChanges();

			return RedirectToAction("Create");
		}

		[HttpGet]
        public IActionResult Update(int? productId)
        {
            var product = _context.Products.FirstOrDefault(x => x.ProductId == productId);

            if (product == null)
                return NotFound();

            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(ProductModel request, IFormFile? image)
        {
            if (!ModelState.IsValid)
				return View(request);

			var product = _context.Products.FirstOrDefault(x => x.ProductId == request.ProductId);

			if (product == null)
				return NotFound();

			product.ProductName = request.ProductName;
			product.ProductPrice = request.ProductPrice;

            if (image != null)
            {
				string folder = Path.Combine(_webHostEnvironment.WebRootPath, "ProductImages");

				if (!Directory.Exists(folder))
					Directory.CreateDirectory(folder);

				string temp = Path.GetFileName(image.FileName);
				string timeStamp = DateTime.Now.ToString("yyyyMMddHHmmssfff");
				string imageName = $"{Path.GetFileNameWithoutExtension(temp)}_{timeStamp}{Path.GetExtension(temp)}";
				string imagePath = Path.Combine(folder, imageName);

				image.CopyTo(new FileStream(imagePath, FileMode.Create));

				product.ProductImage = imageName;
			}

            _context.Products.Update(product);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(int? productId)
        {
            var product = _context.Products.FirstOrDefault(x => x.ProductId == productId);

            if (product == null)
                return NotFound();

            _context.Products.Remove(product);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}