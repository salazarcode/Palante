using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.ValueObjects
{
    public class CuotaVO
    {
        public string CodigoCredito { get; set; }
        public string NumeroCuota { get; set; }
        public string FechaPago { get; set; }
        public string Amortizacion { get; set; }
        public string Interes { get; set; }
        public string PeriodoGracia { get; set; }
        public string Encaje { get; set; }
        public string TotalCuota { get; set; }
    }
}
