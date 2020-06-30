using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.ValueObjects
{
    public class PagosCSV
    {
        public string Identificacion { get; set; }
        public string ApellidoPat { get; set; }
        public string ApellidoMat { get; set; }
        public string Nombres { get; set; }
        public string CodigoCredito { get; set; }
        public string NroCuota { get; set; }
        public string FechaPago { get; set; }
        public string Amortizacion { get; set; }
        public string Interes { get; set; }
        public string PeriodoGracia { get; set; }
        public string Encaje { get; set; }
        public string TotalCuota { get; set; }
    }
}
