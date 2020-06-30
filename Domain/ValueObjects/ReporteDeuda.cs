using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.ValueObjects
{
    public class ReporteDeuda
	{
        public int nCodCred { get; set; }        
        public int nMoneda { get; set; }
        public decimal nPrestamo { get; set; }
        public DateTime dFecIni { get; set; }
        public int nEstado { get; set; }
        public int nNroCuotas { get; set; }
        public string dni { get; set; }
        public string nombres { get; set; }
        public string ruc { get; set; }
        public string razonSocial { get; set; }
        public string nombreProducto { get; set; }
        public decimal precio { get; set; }
        public decimal deuda { get; set; }
    }
}
