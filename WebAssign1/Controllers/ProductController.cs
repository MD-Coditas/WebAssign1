using Microsoft.AspNetCore.Mvc;
using WebAssign1.Data;
using WebAssign1.Models;
using Microsoft.AspNetCore.Authorization;

namespace WebAssign1.Controllers
{
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _db;
        public ProductController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            List<Product> objProductList = _db.Products.ToList();

            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                
                if (User.IsInRole("Admin"))
                {
                    return View("AdminIndex", objProductList); // New admin view for product management
                }
            }

            return View("UserIndex", objProductList); // New view for customers
        }
        [Authorize(Roles ="Admin")]
        public IActionResult Add()
        {
            if (!User.IsInRole("Admin")) return Unauthorized();
            return View();
        }
        [HttpPost]
        public IActionResult Add(Product obj)
        {
            if (ModelState.IsValid)
            {
                _db.Products.Add(obj);
                _db.SaveChanges();
                TempData["success"] = "Product added successfully";
                return RedirectToAction("Index");
            }
            return View();
        }
        [Authorize(Roles ="Admin")]
        public IActionResult Edit(int? id)
        {
            if (!User.IsInRole("Admin")) return Unauthorized();
            if (id == null || id == 0)
            {
                return NotFound();
            }
            
            Product? productFromDb = _db.Products.FirstOrDefault(a=> a.Id == id);
            if (productFromDb == null)
            {
                return NotFound();
            }
            return View(productFromDb);
        }
        [HttpPost]
        public IActionResult Edit(Product obj)
        {
            if (ModelState.IsValid)
            {
                _db.Products.Update(obj);
                _db.SaveChanges();
                TempData["success"] = "Product updated successfully";
                return RedirectToAction("Index");
            }
            return View();
        }
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int? id)
        {
            if (!User.IsInRole("Admin")) return Unauthorized();
            if (id == null || id == 0)
            {
                return NotFound();
            }
            //Product? productFromDb = _db.Products.Find(id);
            Product? productFromDb = _db.Products.FirstOrDefault(a => a.Id == id);
            if (productFromDb == null)
            {
                return NotFound();
            }
            return View(productFromDb);
        }
        [HttpPost]
        public IActionResult Delete(Product obj)
        {
            var del = _db.Products.FirstOrDefault(a => a.Id == obj.Id);
            if (del != null)
            {
                _db.Products.Remove(del);
                _db.SaveChanges();
                TempData["success"] = "Product deleted successfully";
                return RedirectToAction("Index");
            }
            return View();
        }
    }

}
