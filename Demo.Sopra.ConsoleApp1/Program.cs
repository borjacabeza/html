using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Linq;
using Demo.Sopra.ConsoleApp1.Models;
using Newtonsoft.Json;


namespace Demo.Sopra.ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Clear();
            Console.Write("ID Cliente: ");
            var id = Console.ReadLine();

            var http = new HttpClient();
            http.BaseAddress = new Uri("https://localhost:3001");

            var respuesta = http.GetAsync($"/api/clientes/{id}").Result;
            if(respuesta.IsSuccessStatusCode)
            {
                var contentJSON = respuesta.Content.ReadAsStringAsync().Result;
                var cliente = JsonConvert.DeserializeObject<Customer>(contentJSON);

                Console.WriteLine($"{cliente.CustomerID} - {cliente.CompanyName} - {cliente.Country}");
            }
            else Console.Write($"Estado: {respuesta.StatusCode}");
        }
    }
}
