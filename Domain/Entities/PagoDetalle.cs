using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class PagoDetalle
    {
        public int PagoID { get; set; }
        public int nCodCred { get; set; }
        public int nNroCalendario { get; set; }
        public int nNroCuota { get; set; }
        public decimal Monto { get; set; }
        public bool EsDeuda { get; set; }
    }
}
