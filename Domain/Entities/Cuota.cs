using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class Cuota
    {
        public int nNroCuota { get; set; }
        public string dFecVcto { get; set; }
        public string nCapital { get; set; }
        public decimal nInteresComp { get; set; }
        public decimal nPerGracia { get; set; }
        public decimal nSeguro { get; set; }
        public decimal nSegDesgravamen { get; set; }
        public int nEstado { get; set; }
        public int nEstadoCuota { get; set; }
    }
}
