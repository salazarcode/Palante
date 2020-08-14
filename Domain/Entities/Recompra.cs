using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class Recompra
    {
        public int RecompraID { get; set; }
        public int FondeadorID { get; set; }
        public int ProductoID { get; set; }
        public DateTime Creado { get; set; }
        public DateTime FechaCalculo { get; set; }
        public DateTime Modificado { get; set; }
        public DateTime FechaCierre { get; set; }
        public string CreadoPor { get; set; }
        public List<RecompraDetalle> Detalles { get; set; }
    }
}
