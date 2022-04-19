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

        public IActionResult Index()
        {
            var productos = _context.Products
                .Include(r => r.Category)
                .ToList();

            return View(productos);
        }

        public IActionResult Ficha(int id)
        {
            ViewData["Categorias"] = new SelectList(_context.Categories.ToList(), "CategoryID", "CategoryName");
            ViewData["Proveedores"] = new SelectList(_context.Suppliers.ToList(), "SupplierID", "CompanyName");

            var producto = _context.Products
                .Include(r => r.Category)
                .Where(r => r.ProductID == id)
                .FirstOrDefault();

            return View(producto);
        }

        [HttpPost]
        public IActionResult Ficha(int id, Product product)
        {
            if(id != product.ProductID) return NotFound();

            if(ModelState.IsValid)
            {
                _context.Update(product);
                _context.SaveChanges();

                return RedirectToAction("index");
            }
            else return View(product);
        }

        public ProductosController(ModelNorthwind context)
        {
            _context = context;
        }
    }
}