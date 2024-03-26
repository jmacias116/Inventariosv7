using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventarios.Modelos.Especificaciones
{
    public  class MetaData
    {
        public int TotalPages { get; set; } // Total Pagiunas Lista de Productos

        public int PageSize { get; set; }

        public int TotalCount { get; set; } // Total de Registros 
    }
}
