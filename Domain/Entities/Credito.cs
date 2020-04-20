using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class Credito
    {
        public int nCodCred { get; set; }
        public int nCodAge { get; set; }
        public int nMoneda { get; set; }
        public double nPrestamo { get; set; }
        public DateTime dFechaVig { get; set; }
        public DateTime dFecIni { get; set; }
        public int nEstado { get; set; }
        public int nNroCuotas { get; set; }
        public double nMontoCuota { get; set; }
        public Persona Persona { get; set; }
    }
}
