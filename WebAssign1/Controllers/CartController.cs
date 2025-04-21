using Microsoft.AspNetCore.Mvc;
using WebAssign1.Models;
using WebAssign1.Data;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace WebAssign1.Controllers
{
    public class CartController : Controller
    {
        private readonly ApplicationDbContext _db;
        private string SessionKey => $"Cart_{User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value ?? HttpContext.Session.Id}";


        public CartController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            var cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>(SessionKey) ?? new List<CartItem>();
            ViewBag.NetTotal = cart.Sum(c => c.Total);
            return View(cart);
        }

        public IActionResult AddToCart(int id)
        {
            var product = _db.Products.FirstOrDefault(p => p.Id == id);
            if (product == null) return NotFound();

            var cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>(SessionKey) ?? new List<CartItem>();
            var item = cart.FirstOrDefault(c => c.ProductId == id);
            if (item != null)
            {
                item.Quantity++;
            }
            else
            {
                cart.Add(new CartItem
                {
                    ProductId = product.Id,
                    Name = product?.Name ?? string.Empty,
                    Price = product?.Price ?? 0,
                    Quantity = 1
                });
            }

            HttpContext.Session.SetObjectAsJson(SessionKey, cart);
            return RedirectToAction("Index");
        }
        public IActionResult IncreaseQuantity(int id)
        {
            var cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>(SessionKey) ?? new List<CartItem>();
            var item = cart.FirstOrDefault(c => c.ProductId == id);

            if (item != null)
            {
                var product = _db.Products.FirstOrDefault(p => p.Id == id);
                if (product == null)
                {
                    TempData["error"] = "Product not found.";
                    return RedirectToAction("Index");
                }

                if (item.Quantity < product.Quantity)
                {
                    item.Quantity++;
                    HttpContext.Session.SetObjectAsJson(SessionKey, cart);
                }
                else
                {
                    TempData["error"] = $"Cannot increase quantity. Only {product.Quantity} item(s) available in stock.";
                }
            }

            return RedirectToAction("Index");
        }
        public IActionResult DecreaseQuantity(int id)
        {
            var cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>(SessionKey) ?? new List<CartItem>();
            var item = cart.FirstOrDefault(c => c.ProductId == id);

            if (item != null)
            {
                item.Quantity--;

                if (item.Quantity <= 0)
                {
                    cart.Remove(item);
                }

                HttpContext.Session.SetObjectAsJson(SessionKey, cart);
            }

            return RedirectToAction("Index");
        }


        [HttpPost]
        public IActionResult RemoveFromCart(int productId)
        {
            var cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>(SessionKey) ?? new List<CartItem>();
            var item = cart.FirstOrDefault(c => c.ProductId == productId);
            if (item != null)
                cart.Remove(item);

            HttpContext.Session.SetObjectAsJson(SessionKey, cart);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> PlaceOrder()
        {
            var cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>(SessionKey);
            if (cart == null || !cart.Any())
            {
                TempData["error"] = "Your cart is empty.";
                return RedirectToAction("Index");
            }

            // Get the logged-in user's email
            var userEmail = User.Claims.FirstOrDefault(c => c.Type == System.Security.Claims.ClaimTypes.Email)?.Value;
            if (userEmail == null)
            {
                TempData["error"] = "User not found.";
                return RedirectToAction("Index", "Account");
            }

            var user = await _db.Customers.FirstOrDefaultAsync(c => c.Email == userEmail);
            if (user == null)
            {
                TempData["error"] = "User not found in database.";
                return RedirectToAction("Index");
            }

            foreach (var item in cart)
            {
                var product = await _db.Products.FirstOrDefaultAsync(p => p.Id == item.ProductId);
                if (product != null)
                {
                    if (product.Quantity < item.Quantity)
                    {
                        TempData["error"] = $"Not enough stock for {product.Name}.";
                        return RedirectToAction("Index");
                    }

                    product.Quantity -= item.Quantity;
                }
            }

            var order = new Order
            {
                CustomerId = user.Id,
                OrderDate = DateTime.Now,
                TotalAmount = cart.Sum(c => c.Total),
                OrderItems = cart.Select(item => new OrderItem
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    UnitPrice = item.Price
                }).ToList()
            };

            _db.Orders.Add(order);
            await _db.SaveChangesAsync();

            HttpContext.Session.Remove(SessionKey);
            TempData["success"] = "Order placed successfully!";
            return RedirectToAction("Index", "Product");
        }

    }
}
