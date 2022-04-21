using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Demo.Sopra.WebApplication1.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace Demo.Sopra.WebApplication1.Controllers
{
    public class ProductosController : Controller
    {
        private readonly ModelNorthwind _context;

        public async Task<IActionResult> Index()
        {
            var productos =  _context.Products
                .Include(r => r.Category);

            return View(await productos.ToListAsync());
        }

        public async Task<IActionResult> Ficha(int id)
        {
            ViewData["Categorias"] = new SelectList(await _context.Categories.ToListAsync(), "CategoryID", "CategoryName");
            ViewData["Proveedores"] = new SelectList(await _context.Suppliers.ToListAsync(), "SupplierID", "CompanyName");

            var producto = await _context.Products
                .Where(r => r.ProductID == id)
                .FirstOrDefaultAsync();

            return View(producto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Ficha(int id, Product product)
        {
            if(id != product.ProductID) return NotFound();

            if(ModelState.IsValid)
            {
                _context.Update(product);
                _context.SaveChanges();

                return RedirectToAction("index");
            }
            else
            { 
                ViewData["Categorias"] = new SelectList(_context.Categories.ToList(), "CategoryID", "CategoryName");
                ViewData["Proveedores"] = new SelectList(_context.Suppliers.ToList(), "SupplierID", "CompanyName");

                return View(product);
            }
        }

        public IActionResult Nuevo()
        {
            ViewData["Categorias"] = new SelectList(_context.Categories.ToList(), "CategoryID", "CategoryName");
            ViewData["Proveedores"] = new SelectList(_context.Suppliers.ToList(), "SupplierID", "CompanyName");

            return View("Ficha", new Product());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Nuevo(Product product)
        {
            if(ModelState.IsValid)
            {
                _context.Products.Add(product);
                _context.SaveChanges();

                return RedirectToAction("index");
            }
            else
            {
                ViewData["Categorias"] = new SelectList(_context.Categories.ToList(), "CategoryID", "CategoryName");
                ViewData["Proveedores"] = new SelectList(_context.Suppliers.ToList(), "SupplierID", "CompanyName");

                return View("Ficha", product);
            }
        }

        public ProductosController(ModelNorthwind context)
        {
            _context = context;
        }
    }
}