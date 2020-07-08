using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class Reprogramacion
    {
        public int ReprogramacionID { get; set; }
        public decimal Tasa { get; set; }
        public decimal SaldoCapital { get; set; }
        public decimal NuevoCapital { get; set; }
        public DateTime UltimoVencimiento { get; set; }
        public DateTime Hoy { get; set; }
        public int DiasTranscurridos { get; set; }
        public decimal Factor { get; set; }
        public decimal InteresesTranscurridos { get; set; }
        public decimal KI { get; set; }
        public decimal Amortizacion { get; set; }
        public decimal Capital { get; set; }
        public int nCodCred { get; set; }
        public decimal Total { get; set; }
        public int NroCalendarioCOF { get; set; }
        public DateTime Confirmacion { get; set; }
    }
}
