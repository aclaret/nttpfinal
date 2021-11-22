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
            //Obtengo una array de categorias desde el modelo
            Categoria[] categorias = (Categoria[])Enum.GetValues(typeof(Categoria));

            //Paso las categorias a la vista
            ViewData["categorias"] = categorias;

            foreach (var categoria in categorias) {
                //ACA DEBERIA TRAER LOS PLATOS CON LOS PRECIOS Y NO SOLO EL NOMBRE,PARA PODER MOSTRARLOS EN LA HOME
                //String[] platos = (from Plato in _dbContext.Platos where (Plato.Categoria == categoria) select Plato.Nombre).ToArray();

                Plato[] platos = (from Plato in _dbContext.Platos where (Plato.Categoria == categoria) select Plato).ToArray();

                //Aca agrego los platos a la categoria para pasarlo a la vista
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
