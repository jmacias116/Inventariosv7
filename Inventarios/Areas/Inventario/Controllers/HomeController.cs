﻿using Inventarios.AccesoDatos.Repositorio.IRepositorio;
using Inventarios.Modelos;
using Inventarios.Modelos.ViewModels;
using Microsoft.AspNetCore.Mvc;
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

        public async Task<IActionResult> Index()
        {
            IEnumerable<Producto> productoLista = await _unidadTrabajo.Producto.ObtenerTodos();
            return View(productoLista);
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