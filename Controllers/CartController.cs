using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Concurrent;
using System.Security.Claims;
using TimeShop.Areas.User.Models;
using TimeShop.Data;
using TimeShop.Models;
using TimeShop.Models.DTO;

namespace TimeShop.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CartController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var currentUser = GetCurrentUser();

            var query = from u in _context.Users
                        join c in _context.Carts on u.UserId equals c.UserId
                        join ci in _context.CartItems on c.CartId equals ci.CartId
                        join p in _context.Products on ci.ProductId equals p.ProductId
                        where u.UserId == currentUser.UserId
                        select new CartQuery
                        {
                            UserId = u.UserId,
                            UserName = u.UserName,
                            UserSurname = u.UserSurname,
                            UserEmail = u.UserEmail,
                            CartId = c.CartId,
                            CartItemId = ci.CartItemId,
                            ProductId = ci.ProductId,
                            ProductName = p.ProductName,
                            ProductPrice = p.ProductPrice,
                            ProductImage = p.ProductImage,
                            ProductQuantity = ci.ProductQuantity,
                            TotalProductPrice = ci.ProductQuantity * p.ProductPrice,
                            TotalPurchasedItems = _context.CartItems.Where(x => x.CartId == c.CartId).Sum(x => x.ProductQuantity),
                            TotalCartPrice = _context.CartItems
                                .Where(x => x.Cart!.UserId == u.UserId && x.CartId == c.CartId)
                                .Sum(x => x.ProductQuantity * x.Product!.ProductPrice)
                        };

            return View(query.ToList());
        }

        [HttpGet]
        public IActionResult AddProduct(int productId)
        {
            UserModel currentUser = GetCurrentUser();

            var cart = _context.Carts.FirstOrDefault(x => x.UserId == currentUser.UserId);

            if (cart == null)
            {
                CreateCart(currentUser.UserId);
                cart = _context.Carts.FirstOrDefault(x => x.UserId == currentUser.UserId);
            }

            var cartItem = _context.CartItems.FirstOrDefault(x => x.CartId == cart!.CartId && x.ProductId == productId);

            if (cartItem == null)
                CreateCartItem(cart!.CartId, productId);
            else
            {
                cartItem.ProductQuantity += 1;
                _context.SaveChanges();
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult DeleteProduct(int productId)
        {
            UserModel currentUser = GetCurrentUser();

            var product = _context.Products.FirstOrDefault(x => x.ProductId == productId);

            var cart = _context.Carts.FirstOrDefault(x => x.UserId == currentUser.UserId);

            var cartItem = _context.CartItems.FirstOrDefault(x => x.CartId == cart!.CartId && x.ProductId == productId);

            _context.CartItems.Remove(cartItem!);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult PlusProduct(int productId)
        {
            UserModel currentUser = GetCurrentUser();

            var product = _context.Products.FirstOrDefault(x => x.ProductId == productId);

            var cart = _context.Carts.FirstOrDefault(x => x.UserId == currentUser.UserId);

            var cartItem = _context.CartItems.FirstOrDefault(x => x.CartId == cart!.CartId && x.ProductId == productId);

            cartItem!.ProductQuantity += 1;
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult MinusProduct(int productId)
        {
            UserModel currentUser = GetCurrentUser();

            var product = _context.Products.FirstOrDefault(x => x.ProductId == productId);

            var cart = _context.Carts.FirstOrDefault(x => x.UserId == currentUser.UserId);

            var cartItem = _context.CartItems.FirstOrDefault(x => x.CartId == cart!.CartId && x.ProductId == productId);

            if (cartItem!.ProductQuantity > 1)
            {
                cartItem!.ProductQuantity -= 1;
                _context.SaveChanges();
            }
            else
            {
                _context.CartItems.Remove(cartItem!);
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult DeleteAllProducts()
        {
            UserModel currentUser = GetCurrentUser();

            var cart = _context.Carts.First(x => x.UserId == currentUser.UserId);

            var cartItems = _context.CartItems.Where(x => x.CartId == cart.CartId).ToList();

            _context.CartItems.RemoveRange(cartItems);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult MakeOrder()
        {
            UserModel currentUser = GetCurrentUser();

            var cart = _context.Carts.FirstOrDefault(x => x.UserId == currentUser.UserId);

            if (cart == null)
                return NotFound();

            var cartItems = _context.CartItems.Where(x => x.CartId == cart.CartId);

            OrderModel order = new OrderModel
            {
                UserId = currentUser.UserId,
                StatusId = 1,
                OrderDate = DateTime.Now,
            };

            _context.Orders.Add(order);
            _context.SaveChanges();

            foreach (var cartItem in cartItems)
            {
                OrderItemModel orderItem = new OrderItemModel
                {
                    OrderId = order.OrderId,
                    ProductId = cartItem.ProductId,
                    ProductQuantity = cartItem.ProductQuantity
                };

                _context.OrderItems.Add(orderItem);
            }

            _context.Carts.Remove(cart);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public UserModel GetCurrentUser()
        {
            var claim = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email);
            string email = claim!.Value;

            var currentUser = _context.Users.FirstOrDefault(x => x.UserEmail == email);

            return currentUser!;
        }

        public void CreateCart(int userId)
        {
            CartModel cart = new CartModel
            {
                UserId = userId,
            };

            _context.Carts.Add(cart);
            _context.SaveChanges();
        }

        public void CreateCartItem(int cartId, int productId)
        {
            CartItemModel cartItem = new CartItemModel
            {
                CartId = cartId,
                ProductId = productId,
                ProductQuantity = 1
            };

            _context.CartItems.Add(cartItem);
            _context.SaveChanges();
        }
    }
}
