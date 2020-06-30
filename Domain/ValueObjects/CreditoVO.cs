using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.ValueObjects
{
    public class CreditoVO
    {
        public int nCodCred { get; set; }
        public int nCodAge { get; set; }
        public int nMoneda { get; set; }
        public double nPrestamo { get; set; }
        public int nEstado { get; set; }
        public int nNroCuotas { get; set; }
        public double nMontoCuota { get; set; }
        public string dni { get; set; }
        public string nombres { get; set; }
        public string ruc { get; set; }
        public string razonSocial { get; set; }
        public string precio { get; set; }
        public string ccodcta { get; set; }
        public int codigoProducto { get; set; }
        public string nombreProducto { get; set; }
    }
}
