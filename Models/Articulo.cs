﻿using System;
using System.Collections.Generic;

#nullable disable

namespace ADMINISTRACION.Models
{
    public partial class Articulo
    {
        public long ArticuloId { get; set; }
        public string Nombre { get; set; }
        public decimal Precio { get; set; }
        public long? ProveedorId { get; set; }
    }
}
