using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RestoBart.Context;
using RestoBart.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace RestoBart.Controllers
{
    public class HomeController : Controller
    {
        private readonly RestoBartDatabaseContext _dbContext;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, RestoBartDatabaseContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            Categoria[] categorias = (Categoria[])Enum.GetValues(typeof(Categoria));
            ViewData["categorias"] = categorias;

            foreach (var categoria in categorias) {
                String[] platos = (from Plato in _dbContext.Platos where (Plato.Categoria == categoria) select Plato.Nombre).ToArray();
                ViewData["categorias_" + categoria] = platos;
            }

            

            return View();
        }

        public IActionResult AboutUs()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
