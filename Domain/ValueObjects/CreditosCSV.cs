using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.ValueObjects
{
    public class CreditosCSV
    {
        public string CodigoCredito { get; set; }
        public string TipoDocumento { get; set; }
        public string NroDocumento { get; set; }
        public string Importe { get; set; }
        public string Precio { get; set; }
        public string PeriodoTasa { get; set; }
        public string Interes { get; set; }
        public string InteresMoratorio { get; set; }
        public string Destino { get; set; }
        public string CodigoCredito2 { get; set; }
    }
}
