using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Demo.Sopra.WebApi1.Models;

namespace Demo.Sopra.WebApi1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PedidosController : ControllerBase
    {
        private readonly ModelNorthwind _context;
        private readonly IConfiguration _config;


        // GET api/pedidos/
        [HttpGet]
        public List<Order> Get()
        {            
            return _context.Orders.ToList();
        }

        // GET api/pedidos/
        [HttpPost("filter")]
        public List<Order> Get([FromBody] OrdersFilterObject filter)
        {            
            var id = HttpContext.Request.Query["CustomerID"].ToString();

            if(filter.CustomerID != null)
            {
                return _context.Orders.Where(r => r.CustomerID == filter.CustomerID).ToList();
            }
            else return _context.Orders.ToList();
        }

        // GET api/pedidos?customerID=ANATR
        [HttpGet("filter2")]
        public List<Order> GetWithParams([FromQuery] string customerID)
        {            
            //var id = HttpContext.Request.Query["CustomerID"].ToString();

            if(customerID != null)
            {
                return _context.Orders.Where(r => r.CustomerID == customerID).ToList();
            }
            else return _context.Orders.ToList();
        }

        // GET api/pedidos/5
        [HttpGet("{id}")]
        public Order Get(int id)
        {
            var pedido = _context.Orders
                .Where(r => r.OrderID == id)
                .FirstOrDefault();

            return pedido;
        }
        
        public PedidosController(ModelNorthwind context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }
    }
}