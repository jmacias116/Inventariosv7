using Inventarios.Modelos.Especificaciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Inventarios.AccesoDatos.Repositorio.IRepositorio
{
    public interface IRepositorio<T>  where T : class
    {
        Task<T> Obtener(int id);

        Task<IEnumerable<T>> ObtenerTodos(
           Expression<Func<T, bool>> filtro = null,
           Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
           string incluirPropiedades = null,
           bool isTracking = true
           );

        //  Interfase para el Paginado de las diferentes entidades VIDEO 67
        //   va a ser un Metodo de Tipo PagedList del proyecto Modelos
            PagedList<T> ObtenerTodosPaginado(Parametros parametros, Expression<Func<T, bool>> filtro = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string incluirPropiedades = null,
            bool isTracking = true);



        Task<T> ObtenerPrimero(
            Expression<Func<T, bool>> filtro = null,
            string incluirPropiedades = null,
            bool isTracking = true
            );

        Task Agregar(T entidad);

        void Remover(T entidad);

        void RemoverRango(IEnumerable<T> entidad);
    }
    
}
