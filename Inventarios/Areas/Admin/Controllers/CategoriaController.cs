using Inventarios.AccesoDatos.Repositorio.IRepositorio;
using Inventarios.Modelos;
using Inventarios.Utilidades;
using Microsoft.AspNetCore.Mvc;

namespace Inventarios.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoriaController : Controller
    {
        private readonly IUnidadTrabajo _unidadTrabajo;

        public CategoriaController(IUnidadTrabajo unidadTrabajo)
        {
            _unidadTrabajo = unidadTrabajo;
        }

        public IActionResult Index() // Vista llama a catejoria.js usa Ajax para listar con
                                     // DataTable
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Upsert(int? id)
        {
            Categoria categoria = new Categoria();

            if (id == null)
            {
                // Crear una nueva Bodega
                categoria.Estado = true;
                return View(categoria);
            }


            // Actualizamos Bodega
            categoria= await _unidadTrabajo.Categoria.Obtener(id.GetValueOrDefault());
            if (categoria == null)
            {
                return NotFound();
            }
            return View(categoria);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(Categoria categoria)
        {

            if (ModelState.IsValid)
            {
                if (categoria.Id == 0)
                {
                    await _unidadTrabajo.Categoria.Agregar(categoria);
                    TempData[DS.Exitosa] = "OK ha sido creada Categoria"; // Video 42
                }
                else
                {
                    _unidadTrabajo.Categoria.Actualizar(categoria);
                    TempData[DS.Exitosa] = "Categoria Actualizada Exitosamente";
                }

                await _unidadTrabajo.Guardar();
                return RedirectToAction(nameof(Index));
            }
            TempData[DS.Error] = "Error: categoria no fué Grabada";
            return View(categoria);
        }

        #region API
        [HttpGet]

        public async Task<IActionResult> ObtenerTodos()
        {
            var todos = await _unidadTrabajo.Categoria.ObtenerTodos();
            return Json(new { data = todos });
        }

        [HttpPost]

        public async Task<IActionResult> Delete(int id)
        {
            var CategoriaDb = await _unidadTrabajo.Categoria.Obtener(id);

            if (CategoriaDb == null)
            {
                return Json(new { sucess = false, message = "Error al Borrar Categoria" });
            }
            _unidadTrabajo.Categoria.Remover(CategoriaDb);
            await _unidadTrabajo.Guardar();
            return Json(new { sucess = true, message = "Exito ! al Borrar Categoria" });

        }

        [ActionName("ValidarNombre")]

        public async Task<IActionResult> ValidarNombre(string Nombre, int id = 0)
        {
            bool valor = false;
            var lista = await _unidadTrabajo.Categoria.ObtenerTodos();

            if (id == 0)
            {
                valor = lista.Any(b => b.Nombre.ToLower().Trim() == Nombre.ToLower().Trim());
            }
            else
            {
                valor = lista.Any(b => b.Nombre.ToLower().Trim() == Nombre.ToLower().Trim() && b.Id != id);
            }

            if (valor)
            {
                return Json(new { data = true });
            }
            return Json(new { data = false });

        }

        #endregion
    }
}
