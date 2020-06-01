using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class Cuota
    {
        public int nCodCred { get; set; }
        public int nNroCuota{ get; set; }
        public DateTime dFecVcto { get; set; }
        public decimal nCapital { get; set; }
        public decimal nInteres { get; set; }
        public decimal nSeguro { get; set; }
        public decimal nCuotaMensual{ get; set; }
    }
}
