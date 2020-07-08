﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class Cuota
    {
        public string CodigoCredito { get; set; }
        public string NumeroCuota { get; set; }
        public string FechaPago { get; set; }
        public string Amortizacion { get; set; }
        public string Interes { get; set; }
        public string PeriodoGracia { get; set; }
        public string Encaje { get; set; }
        public string TotalCuota { get; set; }
        public int nEstado { get; set; }
        public int nEstadoCuota { get; set; }
    }
}
