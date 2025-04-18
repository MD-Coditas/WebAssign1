using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using WebAssign1.Data;

namespace WebAssign1.Controllers
{
    public class OrderController : Controller
    {
        private readonly ApplicationDbContext _db;

        public OrderController(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<IActionResult> MyOrders()
        {
            var userEmail = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
            var user = await _db.Customers.FirstOrDefaultAsync(c => c.Email == userEmail);
            if (user == null) return NotFound();

            var orders = await _db.Orders
                .Where(o => o.CustomerId == user.Id)
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.Product)
                .ToListAsync();

            return View(orders);
        }

        public async Task<IActionResult> AllOrders()
        {
            if (!User.IsInRole("Admin"))
                return Unauthorized();

            var orders = await _db.Orders
                .Include(o => o.Customer)
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.Product)
                .ToListAsync();

            return View(orders);
        }


    }

}
