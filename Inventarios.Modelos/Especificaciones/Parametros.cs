﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventarios.Modelos.Especificaciones
{
    public class Parametros // Modelo Paginación para mostrar productos en pag.principal
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 4;
    }
}