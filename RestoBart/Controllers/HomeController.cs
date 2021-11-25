using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RestoBart.Context;
using RestoBart.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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

        [HttpPost]
        public JsonResult AjaxGuardarPedido()
        {
            String idUsuario = HttpContext.Request.Form["idUsuario"];
            String platosAll = HttpContext.Request.Form["platosPedido"];
            String[] platos = platosAll.Split(';');

            Cliente cliente = (from Cliente in _dbContext.Clientes where Cliente.IdUsuario == 1 select Cliente).Single();
            Pedido pedido = new Pedido(cliente, DateTime.Now.ToString(), 500);;
            PedidosController pedidos = new PedidosController(_dbContext);
            pedidos.Create(pedido);

            foreach (String plato in platos)
            {
                if (plato != "")
                {
                    String[] PlatoXCantidad = plato.Split('|');
                    int idPlato = Int32.Parse(PlatoXCantidad[0]);
                    int cantidadPlato = Int32.Parse(PlatoXCantidad[1]);

                    Console.WriteLine("El plato seleccionado es " + idPlato + " y la cantidad es " + cantidadPlato);
                }
            }

            return Json("true");
        }
    }
}
