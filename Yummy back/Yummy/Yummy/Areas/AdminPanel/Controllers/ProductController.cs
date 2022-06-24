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
            if (Id == null)
            {
                return BadRequest();

            }
            var productDb = _context.Products.Where(p => !p.IsDeleted).FirstOrDefault(p => p.Id == Id);
            if (productDb == null)
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Product product)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            bool IsExist = _context.Products.Where(p => !p.IsDeleted).Any(p => p.Title.ToLower() == product.Title.ToLower());
            if (IsExist)
            {
                ModelState.AddModelError("Title", $"{product.Title} is already exist!");
                return View();
            }
            if (product.Description.Length < 10)
            {
                ModelState.AddModelError("Description", $"{product.Description} must be at least 10 character!");
                return View();
            }


            await _context.AddAsync(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Update(int? Id)
        {
            if (Id == null)
            {
                return BadRequest();
            }
            var productDb = _context.Products.Where(p => !p.IsDeleted).FirstOrDefault(p => p.Id == Id);
            if (productDb == null)
            {
                return NotFound();

            }
            return View(productDb);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task< IActionResult> Update(int? Id, Product product)
        {
            if (Id == null)
            {
                return BadRequest();
            }
            var productDb = _context.Products.Where(p => !p.IsDeleted).FirstOrDefault(p => p.Id == Id);
            if (productDb == null)
            {
                return NotFound();

            }
            bool IsExist = _context.Products.Where(p => !p.IsDeleted).Any(p => p.Title.ToLower() == product.Title.ToLower());
            if (IsExist)
            {
                ModelState.AddModelError("Title", $"{product.Title} is already exist!");
                return View();
            }
            if (product.Description.Length < 10)
            {
                ModelState.AddModelError("Description", $"{product.Description} must be at least 10 character!");
                return View();
            }

            product.Title = productDb.Title;
            product.Description = productDb.Description;
            product.Price = productDb.Price;
            await _context.SaveChangesAsync();
            return RedirectToAction();


        }

    }

}
