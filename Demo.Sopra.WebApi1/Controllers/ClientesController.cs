using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Demo.Sopra.WebApi1.Models;
using Microsoft.EntityFrameworkCore;

namespace Demo.Sopra.WebApi1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientesController : ControllerBase
    {
        private readonly ModelNorthwind _context;
        private readonly IConfiguration _config;


        // GET api/clientes/
        [HttpGet]
        public List<Customer> Get()
        {            
            var clientes = _context.Customers.ToList();
            return clientes;
        }

        // GET api/clientes/ANATR
        [HttpGet("{id}")]
        public Customer Get(string id)
        {
            var cliente = _context.Customers
                .Where(r => r.CustomerID == id)
                .FirstOrDefault();

            return cliente;
        }

        // GET api/clientes/ANATR/orders
        [HttpGet("{id}/orders")]
        public List<Order> GetOrders(string id)
        {
            var pedidos = _context.Orders

                .Where(r => r.CustomerID == id)
                .Include(r => r.Employee)
                .Include(r => r.ShipViaNavigation)
                .ToList();

            return pedidos;
        }

        // POST api/clientes
        [HttpPost]
        public ActionResult Post([FromBody] Customer cliente)
        {
            _context.Customers.Add(cliente);
            _context.SaveChanges();

            return Created($"/api/clientes/{cliente.CustomerID}", cliente);
        }

        // PUT api/clientes/ANATR
        [HttpPut("{id}")]
        public ActionResult Put(string id, [FromBody] Customer cliente)
        {
            if(id != cliente.CustomerID) return BadRequest();
            _context.Update(cliente);
            _context.SaveChanges();

            return NoContent();
        }

        // DELETE api/clientes/ANATR
        [HttpDelete("{id}")]
        public Customer Delete(string id)
        {
            var cliente = _context.Customers
                .Where(r => r.CustomerID == id)
                .FirstOrDefault();

            _context.Customers.Remove(cliente);
            _context.SaveChanges();

            return cliente;
        }

        public ClientesController(ModelNorthwind context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }
    }
}