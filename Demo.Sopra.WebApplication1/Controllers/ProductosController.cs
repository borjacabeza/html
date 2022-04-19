using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
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
            var producto = _context.Products
                .Include(r => r.Category)
                .Where(r => r.ProductID == id)
                .FirstOrDefault();

            return View(producto);
        }

        public ProductosController(ModelNorthwind context)
        {
            _context = context;
        }
    }
}