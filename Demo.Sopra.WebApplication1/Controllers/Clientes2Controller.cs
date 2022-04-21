using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc;
using Demo.Sopra.WebApplication1.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Demo.Sopra.WebApplication1.Controllers
{
    public class Clientes2Controller : Controller
    {
        private readonly HttpClient _http;
        public IActionResult Index()
        {
            _http.DefaultRequestHeaders.Add("APIKey", "UBsnxHho!PHt4bzvOm^%uMVw68qzSeVI");

            var response = _http.GetAsync("/api/clientes").Result;
            if(response.IsSuccessStatusCode) 
            {
                var clientes = JsonConvert.DeserializeObject<List<Customer>>(response.Content.ReadAsStringAsync().Result);
                return View(clientes);
            }
            else return NotFound();
        }

        public IActionResult Ficha(string id)
        {
            var response = _http.GetAsync($"/api/clientes/{id}").Result;
            if(response.IsSuccessStatusCode) 
            {
                var cliente = JsonConvert.DeserializeObject<Customer>(response.Content.ReadAsStringAsync().Result);

                response = _http.GetAsync($"/api/clientes/{id}/orders").Result;
                ViewBag.Pedidos = JsonConvert.DeserializeObject<List<Order>>(response.Content.ReadAsStringAsync().Result);

                return View(cliente);
            }
            else return NotFound();
        }

        public Clientes2Controller(IHttpClientFactory clientFactory)
        {
            _http = clientFactory.CreateClient("default");
        }
    }
}