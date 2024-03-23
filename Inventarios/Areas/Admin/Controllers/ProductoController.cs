using Inventarios.AccesoDatos.Repositorio.IRepositorio;
using Inventarios.Modelos;
using Inventarios.Modelos.ViewModels;
using Inventarios.Utilidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Data;


namespace Inventarios.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductoController : Controller
    {

        private readonly IUnidadTrabajo _unidadTrabajo;
        private readonly IWebHostEnvironment _webHostEnvironment; // Captura Arch.Root

        public ProductoController(IUnidadTrabajo unidadTrabajo, IWebHostEnvironment webHostEnvironment)
        {
            _unidadTrabajo = unidadTrabajo;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index() // Vista llama a bodega.js usa Ajax para listar con
                                     // DataTable
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Upsert(int? id)  
        {

            ProductoVM produvm = new ProductoVM() //Instancio ViewModel de Productp
                                                   // Video 57 y inicializo propiedades
            {
                Producto = new Producto(),
                CategoriaLista = _unidadTrabajo.Producto.ObtenerListaCategyMarca("Categoria"),
                MarcaLista = _unidadTrabajo.Producto.ObtenerListaCategyMarca("Marca"),
                PadreLista = _unidadTrabajo.Producto.ObtenerListaCategyMarca("Producto")

            };

            if (id == null)
            {
                // Crea un nuevo Producto, activa el estado por default
                produvm.Producto.Estado = true;
                return View(produvm);
            }
            else
            {
                produvm.Producto = await _unidadTrabajo.Producto.Obtener(id.GetValueOrDefault());
                if (produvm.Producto == null)
                {
                    return NotFound();
                }

                return View(produvm);
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(ProductoVM produvm)
        {
            if (ModelState.IsValid)
            {
                var files = HttpContext.Request.Form.Files; //Recibe los Arch.Imagenes Video 60
                string webRootPath = _webHostEnvironment.WebRootPath;
                                                                 // Arma la Ruta
                                                                 // donde se guarda la imagen

                if (produvm.Producto.Id == 0)
                {
                    // Crear nuevo Prod.
                    string upload = webRootPath + DS.ImagenRuta;
                    string fileName = Guid.NewGuid().ToString(); //se genera un Id para la Imagen
                    string extension = Path.GetExtension(files[0].FileName); //obtiene extension arch.

                    using (var fileStream = new FileStream(Path.Combine(upload, fileName + extension), FileMode.Create))
                    {
                        files[0].CopyTo(fileStream);
                    }
                    produvm.Producto.ImagenUrl = fileName + extension;
                    await _unidadTrabajo.Producto.Agregar(produvm.Producto);
                }
                else
                {
                    // Actualizar Imagen en Root imagenes y Tabla BD
                    var objProducto = await _unidadTrabajo.Producto.ObtenerPrimero(p => p.Id == produvm.Producto.Id, isTracking: false);
                    if (files.Count > 0)  // Si se carga una nueva Imagen para el producto existente
                    {
                        string upload = webRootPath + DS.ImagenRuta;
                        string fileNAme = Guid.NewGuid().ToString();
                        string extension = Path.GetExtension(files[0].FileName);

                        //Borrar la imagen anterior
                        var anteriorFile = Path.Combine(upload, objProducto.ImagenUrl);
                        if (System.IO.File.Exists(anteriorFile))
                        {
                            System.IO.File.Delete(anteriorFile);
                        }
                        using (var fileStream = new FileStream(Path.Combine(upload, fileNAme + extension), FileMode.Create))
                        {
                            files[0].CopyTo(fileStream);
                        }
                        produvm.Producto.ImagenUrl = fileNAme + extension;
                    } // Caso contrario no se carga una nueva imagen
                    else
                    {
                        produvm.Producto.ImagenUrl = objProducto.ImagenUrl;
                    }
                    _unidadTrabajo.Producto.Actualizar(produvm.Producto);
                }
                TempData[DS.Exitosa] = "Transaccion Exitosa!";
                await _unidadTrabajo.Guardar();
                //return View("Index");
                return RedirectToAction("Index");

            }  // If not Valid
            produvm.CategoriaLista = _unidadTrabajo.Producto.ObtenerListaCategyMarca("Categoria");
            produvm.MarcaLista = _unidadTrabajo.Producto.ObtenerListaCategyMarca("Marca");
            produvm.PadreLista = _unidadTrabajo.Producto.ObtenerListaCategyMarca("Producto");
            return View(produvm);
        }


        #region API
        [HttpGet]

        public async Task<IActionResult> ObtenerTodos()
        {
            var todos = await _unidadTrabajo.Producto.ObtenerTodos(incluirPropiedades:"Categoria,Marca");
            return Json(new { data = todos });
        }

        [HttpPost]

        public async Task<IActionResult> Delete(int id)
        {
            var ProductoDb = await _unidadTrabajo.Producto.Obtener(id);

            if (ProductoDb == null)
            {
                return Json(new { sucess = false, message = "Error al Borrar Producto" });
            }

            // Remueve img antes de borrar producto

            string upload = _webHostEnvironment.WebRootPath + DS.ImagenRuta;
            var anteriorimg = Path.Combine(upload, ProductoDb.ImagenUrl);

            // Verifica exista la Imagen anterior para Borrarla del directorio imagenes
            if (System.IO.File.Exists(anteriorimg))
            {
                System.IO.File.Delete(anteriorimg);

            }

            _unidadTrabajo.Producto.Remover(ProductoDb);
            await _unidadTrabajo.Guardar();
            return Json(new { sucess = true, message = "Exito ! al Borrar Producto" });

        }

        [ActionName("ValidarSerie")]

        public async Task<IActionResult> ValidarSerie(string Serie, int id = 0)
        {
            bool valor = false;
            var lista = await _unidadTrabajo.Producto.ObtenerTodos();

            if (id == 0)
            {
                valor = lista.Any(b => b.NumeroSerie.ToLower().Trim() == Serie.ToLower().Trim());
            }
            else
            {
                valor = lista.Any(b => b.NumeroSerie.ToLower().Trim() == Serie.ToLower().Trim() && b.Id != id);
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