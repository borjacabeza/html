using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Demo.Sopra.WebApplication1.Models;
using Microsoft.EntityFrameworkCore;

namespace Demo.Sopra.WebApplication1.Controllers
{
    public class ClientesController : Controller
    {
        private readonly ModelNorthwind _context;
        public IActionResult Index()
        {
            var clientes = _context.Customers.ToList();
            return View(clientes);
        }

        public IActionResult Ficha(string id)
        {
            var cliente = _context.Customers
                .Where(r => r.CustomerID == id)
                .FirstOrDefault();

            ViewBag.Pedidos = _context.Orders
                .Include(r => r.Employee)
                .Include(r => r.ShipViaNavigation)
                .Where(r => r.CustomerID == id)
                .ToList();

            return View(cliente);
        }

        public ClientesController(ModelNorthwind context)
        {
            _context = context;
        }
    }
}