using Inventarios.AccesoDatos.Data;
using Inventarios.AccesoDatos.Repositorio.IRepositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventarios.AccesoDatos.Repositorio
{
    //Unidad Trabaja engloca a todos Modelos con sus Repositoriod
    // Se invoca en cualquier momento, pero para que se implemente en
    // el proyectp especialmente en ños Controladores
    // luego se crea el servicio en Program.cs VIDEO 30

    public class UnidadTrabajo : IUnidadTrabajo
    {
        private readonly ApplicationDbContext _db;

        public IBodegaRepositorio Bodega { get; private set; }

        public UnidadTrabajo(ApplicationDbContext db)
        {
            _db = db;
            Bodega = new BodegaRepositorio(_db);
        }

        public void Dispose()
        {
            _db.Dispose();
        }

        public async Task Guardar()
        {
            await _db.SaveChangesAsync();
        }
    }
}
