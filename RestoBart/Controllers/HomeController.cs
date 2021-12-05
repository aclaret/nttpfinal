using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Nancy.Json;
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
            Cliente cliente = null;
            Plato plato = null;

            //Verifico si quiere ingresar o es registro
            String user_accion = HttpContext.Request.Form["user_accion"];

            String username = HttpContext.Request.Form["username"];
            String password = HttpContext.Request.Form["password"];

            if (user_accion == "registrar") {
                String nombre_completo = HttpContext.Request.Form["nombre_completo"];
                String telefono = HttpContext.Request.Form["telefono"];
                String email = HttpContext.Request.Form["email"];
                String direccion = HttpContext.Request.Form["direccion"];

                //Registro el usuario
                bool existe_cliente = _dbContext.Clientes.Any(u => u.Username == username);

                //Si el cliente no existe lo registro sino lanzo el error
                if (!existe_cliente) {

                    cliente = new Cliente();
                    _dbContext.Clientes.Add(cliente);
                } else {
                    return Json("El nombre de usuario se encuentra en uso");
                }
            } else {
                //Intento buscar al usuario y traigo su ID para generar el pedido
                cliente = _dbContext.Clientes.Where(cliente => cliente.Username == username && cliente.Password == password).Single();
            }

            String platosAll = HttpContext.Request.Form["platosPedido"];

            String[] platos_pedido = platosAll.Split(';');


            //Busco los platos para conocer el total del pedido
            double totalPedido = 0;
            ArrayList platos_registrar = new ArrayList();
            foreach (String plato_pedido in platos_pedido)
            {
                if (plato_pedido != "")
                {
                    String[] PlatoXCantidad = plato_pedido.Split('|');
                    int idPlato = Int32.Parse(PlatoXCantidad[0]);
                    int cantidadPlato = Int32.Parse(PlatoXCantidad[1]);

                    plato = _dbContext.Platos.Where(plato => plato.IdPlato == idPlato).Single();

                    //Verifico que el plato exista
                    if (plato != null)
                    {
                        totalPedido += plato.Precio * cantidadPlato;
                        platos_registrar.Add(plato);
                    }
                }
            }

            Pedido pedido = new Pedido(cliente, DateTime.Now.ToString(), totalPedido);
            PedidosController pedidos = new PedidosController(_dbContext);
            //var id_pedido = pedidos.Create(pedido); }
            _dbContext.Pedidos.Add(pedido);
            _dbContext.SaveChanges();

            foreach (Plato plato_pedido in platos_registrar)
            {
                PlatosXPedidos platoPedido = new PlatosXPedidos(pedido.IdPedido, plato_pedido, 3);
                _dbContext.PlatosXPedidos.Add(platoPedido);
                _dbContext.SaveChanges();
            }

            return Json("true");
        }
    }
}
