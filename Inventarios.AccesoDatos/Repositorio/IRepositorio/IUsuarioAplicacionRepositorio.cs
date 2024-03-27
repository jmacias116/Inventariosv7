using Inventarios.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventarios.AccesoDatos.Repositorio.IRepositorio
{
    public interface IUsuarioAplicacionRepositorio : IRepositorio<UsuarioAplicacion>
    {
      // No hay metodo lo genera IdentityUser por su cuenta
    }
}