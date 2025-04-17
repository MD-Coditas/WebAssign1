using Microsoft.AspNetCore.Mvc;
using WebAssign1.Data;
using WebAssign1.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;

namespace WebAssign1.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly string adminEmail = "admin@example.com";
        public AccountController(ApplicationDbContext db)
        {
            _db = db;
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(Customer customer)
        {
            if (ModelState.IsValid)
            {
                var existingUser = await _db.Customers.FirstOrDefaultAsync(x => x.Email == customer.Email);
                if (existingUser != null)
                {
                    ModelState.AddModelError("Email", "Email is already registered.");
                    return View(customer);
                }

                customer.Password = BCrypt.Net.BCrypt.HashPassword(customer.Password);
                _db.Customers.Add(customer);
                await _db.SaveChangesAsync();
                return RedirectToAction("Login");
            }
            return View(customer);
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(string email, string password)
        {
            var user = _db.Customers.FirstOrDefault(x => x.Email == email);
            if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.Password))
            {
                ViewBag.Error = "Email not found or incorrect password.";
                return View();
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.FullName),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Email == adminEmail ? "Admin" : "User")
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            var authProperties = new AuthenticationProperties
            {
                IsPersistent = true, 
                ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(30)
            };

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, authProperties);

            if (user.Email == adminEmail)
                return RedirectToAction("AdminDashboard", "Dashboard");
            else
                return RedirectToAction("UserDashboard", "Dashboard");
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Login");
        }
    }
}
