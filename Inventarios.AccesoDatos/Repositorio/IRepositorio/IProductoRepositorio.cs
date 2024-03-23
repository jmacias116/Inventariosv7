using Inventarios.Modelos;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventarios.AccesoDatos.Repositorio.IRepositorio
{
    public interface IProductoRepositorio : IRepositorio<Producto> // accede a los metodos
                                                                 // como Obtener, ObtenerTodos, ObtenerPrimero
    {
        void Actualizar(Producto producto);

        IEnumerable<SelectListItem> ObtenerListaCategyMarca(string obj);
    }
   
}
