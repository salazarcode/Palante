﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class Cuota
    {
        public int nCodCred { get; set; }
        public int nNroCalendario { get; set; }
        public int nNroCuota { get; set; }
        public decimal nCapital { get; set; }
        public decimal nInteres { get; set; }
        public int nEstado { get; set; }
        public int nEstadoCuota { get; set; }
    }
}
