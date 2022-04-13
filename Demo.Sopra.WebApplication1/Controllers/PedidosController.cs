using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Demo.Sopra.WebApplication1.Models;
using Microsoft.EntityFrameworkCore;

namespace Demo.Sopra.WebApplication1.Controllers
{
    public class PedidosController : Controller
    {
        private readonly ModelNorthwind _context;

        [HttpGet]
        public IActionResult Index()
        {
            var pedidos = _context.Orders
                .Include(r => r.Employee)
                .Include(r => r.ShipViaNavigation)
                .ToList();

            return View(pedidos);
        }

        [HttpPost]
        public IActionResult Detalles(int id)
        {
            var lineas = _context.Order_Details
                .Include(r => r.Product)
                .Where(r => r.OrderID == id)
                .ToList();

            return PartialView("_PedidosDetalles", lineas);
        }

        public PedidosController(ModelNorthwind context)
        {
            _context = context;
        }
    }
}