using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Inventarios.AccesoDatos.Repositorio.IRepositorio;
using System.Threading.Tasks;
using System.Linq.Expressions;
using Inventarios.AccesoDatos.Data;
using Microsoft.EntityFrameworkCore;

namespace Inventarios.AccesoDatos.Repositorio
{
                             // *****************************
                             //        C L A S E   2 7
                             // *****************************

    public class Repositorio<T> : IRepositorio<T> where T : class  
    {

        private readonly ApplicationDbContext _db;
        internal DbSet<T> dbSet;

        public Repositorio(ApplicationDbContext db)
        {
            _db = db;
            this.dbSet = _db.Set<T>();
        }


        public async Task Agregar(T entidad)
        {
            await dbSet.AddAsync(entidad);    // insert into Table
        }

        public async Task<T> Obtener(int id)
        {
            return await dbSet.FindAsync(id);    // select * from (Solo por Id)
                                                 // Find pues usa filtro (condicionante)
        }

        // ********************************************************
        //       SE Obtiene una Lista de la Entidad (Ordenada)
        // ********************************************************

        public async Task<IEnumerable<T>> ObtenerTodos(Expression<Func<T, bool>> filtro = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string incluirPropiedades = null, bool isTracking = true)
        {
            IQueryable<T> query = dbSet;
            if (filtro != null)
            {
                query = query.Where(filtro);   //  select /* from where ....
            }
            if (incluirPropiedades != null) // pregunta si es nula el array de cadena de caracteres
                                            // si es false recorre la cadena con tipo char(split)
                                            // Ver clase 27
            {
                foreach (var incluirProp in incluirPropiedades.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(incluirProp);    //  ejemplo relación de productos con
                                                           //  "Categoria,Marca"
                }
            }
            if (orderBy != null) // verifica si el parametro de orderby es diferente a nulo
            {
                query = orderBy(query); // orderby es el nombre del parametro segun contrato o la
                                        // interfase IRepositorio Genérico
            }
            if (!isTracking) // si el registro esta siendo utilizado
            {
                query = query.AsNoTracking(); // no traquee el registro si se esta utilizando
            }
            return await query.ToListAsync();

        }
        // ********************************************************
        // Aqui el orderby no va , pues solo se obtiene un Registro
        // ********************************************************

        public async Task<T> ObtenerPrimero(Expression<Func<T, bool>> filtro = null,
            string incluirPropiedades = null, bool isTracking = true)
        {

            IQueryable<T> query = dbSet;
            if (filtro != null)
            {
                query = query.Where(filtro);   //  select /* from where ....
            }
            if (incluirPropiedades != null)
            {
                foreach (var incluirProp in incluirPropiedades.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(incluirProp);    //  ejemplo "Categoria,Marca"
                }
            }

            if (!isTracking)
            {
                query = query.AsNoTracking();
            }
            return await query.FirstOrDefaultAsync(); // First pues es el primero
        }

        // **********************************************************
        // Aqui no es asincrono el metodo y es void se Borra Registro
        // **********************************************************

        public void Remover(T entidad)
        {
            dbSet.Remove(entidad);
        }

        // **********************************************************
        //           Aqui se Borra Registro por Rangos (una lista)
        // **********************************************************

        public void RemoverRango(IEnumerable<T> entidad)
        {
            dbSet.RemoveRange(entidad);
        }
    }

}
