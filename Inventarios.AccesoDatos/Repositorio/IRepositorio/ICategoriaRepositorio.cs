using Inventarios.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventarios.AccesoDatos.Repositorio.IRepositorio
{
    public interface ICategoriaRepositorio : IRepositorio<Categoria> // accede a los metodos
                                             // como Obtener, ObtenerTodos, ObtenerPrimero
    {
        void Actualizar(Categoria categoria);
    }
}
