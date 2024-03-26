using Inventarios.AccesoDatos.Repositorio.IRepositorio;
using Inventarios.Modelos;
using Inventarios.Modelos.Especificaciones;
using Inventarios.Modelos.ViewModels;
using Microsoft.AspNetCore.Mvc;
using NPOI.OpenXmlFormats.Wordprocessing;
using NPOI.SS.Util;
using System.Diagnostics;

namespace Inventarios.Areas.Inventario.Controllers
{
    [Area("Inventario")]

    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnidadTrabajo _unidadTrabajo;

        public HomeController(ILogger<HomeController> logger, IUnidadTrabajo unidadTrabajo)
        {
            _logger = logger;
            _unidadTrabajo = unidadTrabajo;
        }

        public IActionResult Index(int pageNumber = 1, string busqueda = "",
                                   string busquedaActual = "")
        {

            // Filtra por Buscar x Nombre del Producto video 69
            if (!String.IsNullOrEmpty(busqueda))
            {
                pageNumber = 1;
            }
            else
            {
                busqueda = busquedaActual;
            }
            ViewData["BusquedaActual"] = busqueda;

            if (pageNumber < 1) { pageNumber = 1; }

            Parametros parametros = new Parametros()
            {
                PageNumber = pageNumber,
                PageSize = 4  // 4 productos o registros por pagina
            };


            var resultado = _unidadTrabajo.Producto.ObtenerTodosPaginado(parametros);

            // Obtiene por filtrado de Producto
            if (!String.IsNullOrEmpty(busqueda))
            {
                resultado = _unidadTrabajo.Producto.ObtenerTodosPaginado(parametros, p => p.Descripcion.Contains(busqueda));
            }

            ViewData["TotalPaginas"] = resultado.MetaData.TotalPages;
            ViewData["TotalRegistros"] = resultado.MetaData.TotalCount;
            ViewData["PageSizw"] = resultado.MetaData.PageSize;
            ViewData["PageNumber"] = pageNumber;
            ViewData["Previo"] = "disable";  // clase css para desactivar el texto
            ViewData["Siguiente"] = ""; 

            if (pageNumber > 1) { ViewData["Previo"] = ""; }
            if (resultado.MetaData.TotalPages <=1) { ViewData["Siguiente"] = "disable"; }
            return View(resultado);
        }

        public IActionResult MacNova()
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