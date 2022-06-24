using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yummy.DAL;
using Yummy.Models;

namespace Yummy.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    public class ProductController : Controller
    {
        private AppDbContext _context { get; }
        private IEnumerable<Product> products;
        public ProductController(AppDbContext context)
        {
            _context = context;
            products = _context.Products.Where(p => !p.IsDeleted).ToList();
        }
        public IActionResult Index()
        {
            return View(products);
        }
        public async Task<IActionResult> Delete(int? Id)
        {
            if (Id==null)
            {
                return BadRequest();

            }
            var productDb = _context.Products.Where(p => !p.IsDeleted).FirstOrDefault(p => p.Id == Id);
            if (productDb==null)
            {
                return NotFound();
            }
            productDb.IsDeleted = true;
           await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Create()
        {
            return View();
        }
    }
}
