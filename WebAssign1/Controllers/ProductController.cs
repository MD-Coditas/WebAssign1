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
                return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult Edit()
        {
            return View();
        }

    }

}
