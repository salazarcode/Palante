using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class Recompra
    {
        public int RecompraID { get; set; }
        public Fondeador Fondeador { get; set; }
        public Producto Producto { get; set; }
        public int FondeadorID { get; set; }
        public int ProductoID { get; set; }
        public DateTime Creado { get; set; }
        public DateTime Modificado { get; set; }
        public DateTime FechaCierre { get; set; }
        public decimal Monto{ get; set; }
        public string CreadoPor { get; set; }
        public List<Credito> Creditos { get; set; }
    }
}
