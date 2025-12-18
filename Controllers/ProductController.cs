using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebShopApp.Data;

namespace WebShopApp.Controllers
{
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Category(int id)
        {
            var products = _context.Products
                .Where(p => p.CategoryId == id)
                .ToList();

            var category = _context.Categories.FirstOrDefault(c => c.Id == id);
            ViewBag.CategoryName = category?.Name;

            return View(products);
        }
        public IActionResult IsNew()
        {
            var products = _context.Products
                .Where(p => p.IsNew)
                .ToList();

            ViewBag.CategoryName = "New products";
            return View("Category", products);
        }

        public IActionResult IsPro()
        {
            var products = _context.Products
                .Where(p => p.IsPromotion)
                .ToList();

            ViewBag.CategoryName = "Promotional products";
            return View("Category", products);
        }
        public async Task<IActionResult> Detail(int id)
        {
            var product = await _context.Products
                .Include(p => p.Category)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }
    }

}
