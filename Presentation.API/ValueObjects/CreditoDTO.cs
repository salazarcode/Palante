﻿using System;
using System.Collections.Generic;
using System.Text;


namespace GestionCartera.API.ValueObjects
{
    public class CreditoDTO
    {
        public int nCodCred { get; set; }
        public int nCodAge { get; set; }
        public int nMoneda { get; set; }
        public double nPrestamo { get; set; }
        public DateTime dFechaVig { get; set; }
        public DateTime dFecIni { get; set; }
        public int nEstado { get; set; }
        public int nNroCuotas { get; set; }
        public string dni { get; set; }
        public string nombres { get; set; }
        public string ruc { get; set; }
        public string razonSocial { get; set; }
        public string ccodcta { get; set; }
        public int codigoProducto { get; set; }
        public string nombreProducto { get; set; }
        public decimal nTasaComp { get; set; }
        public decimal nTasaMor { get; set; }
    }
}
