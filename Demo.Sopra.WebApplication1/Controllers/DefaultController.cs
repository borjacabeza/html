using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Demo.Sopra.WebApplication1.Models;

namespace Demo.Sopra.WebApplication1.Controllers
{
    public class DefaultController : Controller
    {
        public IActionResult Index()
        {
            ViewData["Title"] = "Default - Index";
            ViewData["Mensaje"] = "Hola a todos !!!";
            ViewBag.Numero = 10;

            TempData["Secreto"] = "1234567890";

            var alumno = new Alumno() { Nombre = "Ana", Apellidos = "Sanz Rozas", Edad = 24 };
            ViewBag.datos = alumno;

            return View(alumno);
        }

        public IActionResult Demo2()
        {
            ViewData["Title"] = "Default - Demo2";
            return View("Index", new Alumno() { Nombre = "Adrian", Apellidos = "Sánchez", Edad = 24 });            
        }

        public IActionResult Demo3()
        {
            ViewData["Title"] = "Default - Demo3";
            return RedirectToAction("Index");            
        }

        public IActionResult Demo4()
        {
            return Content("En un lugar de la mancha ...");
        }

        public IActionResult Demo5()
        {
            return Json(new Alumno() { Nombre = "Adrian", Apellidos = "Sánchez", Edad = 24 });
        }

        public IActionResult Demo6()
        {
            return PartialView("_Header");
        }

        public IActionResult Demo7()
        {
            byte[] bytes = System.IO.File.ReadAllBytes("jwt-handbook-v0_14_1.pdf");
            return File(bytes, "application/octet-stream", "jwt-handbook-v0_14_1.pdf");
        }


    }
}