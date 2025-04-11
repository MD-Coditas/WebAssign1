using Microsoft.AspNetCore.Mvc;
using WebAssign1.Data;
using WebAssign1.Models;

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
            // Fetching all products from the database
            List<Product> objProductList = _db.Products.ToList();
            return View(objProductList);
        }
        public IActionResult Add()
        {
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

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            //Product? productFromDb = _db.Products.Find(id);
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
        public IActionResult Delete(int? id)
        {
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
