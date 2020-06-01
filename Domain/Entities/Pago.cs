using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class Pago
    {
        public int PagoID { get; set; }
        public Fondeador Fondeador { get; set; }
        public DateTime Creado { get; set; }
        public DateTime Modificado { get; set; }
        public DateTime FechaCierre { get; set; }
        public decimal MontoTotal{ get; set; }
        public string CreadoPor { get; set; }
        public List<Cuota> Cuotas { get; set; }
    }
}
