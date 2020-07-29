using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class Pago
    {
        public int PagoID { get; set; }
        public Fondeador Fondeador { get; set; }
        public Producto Producto { get; set; }
        public bool EsMochila{ get; set; }
        public string CreadoPor { get; set; }
        public DateTime Creado { get; set; }
        public DateTime Modificado { get; set; }
        public DateTime FechaCierre { get; set; }
        public List<PagoDetalle> Detalles { get; set; }
    }
}
