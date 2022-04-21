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
    public class ProductosController : ControllerBase
    {
        private readonly ModelNorthwind _context;
        private readonly IConfiguration _config;


        // GET api/productos/
        [HttpGet]
        public List<Product> Get()
        {            
            var productos = _context.Products.ToList();
            return productos;
        }

        // GET api/productos/5
        [HttpGet("{id}")]
        public Product Get(int id)
        {
            var producto = _context.Products
                .Where(r => r.ProductID == id)
                .FirstOrDefault();

            return producto;
        }

        // POST api/productos
        [HttpPost]
        public ActionResult Post([FromBody] Product producto)
        {
            _context.Products.Add(producto);
            _context.SaveChanges();

            return Created($"/api/clientes/{producto.ProductID}", producto);
        }

        // PUT api/productos/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Product producto)
        {
            if(id != producto.ProductID) return BadRequest();
            _context.Update(producto);
            _context.SaveChanges();

            return NoContent();
        }

        // DELETE api/productos/5
        [HttpDelete("{id}")]
        public Product Delete(int id)
        {
            var producto = _context.Products
                .Where(r => r.ProductID == id)
                .FirstOrDefault();

            _context.Products.Remove(producto);
            _context.SaveChanges();

            return producto;
        }

        public ProductosController(ModelNorthwind context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }
    }
}