using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class Cartera
    {
        public int CarteraID { get; set; }
        public int ProductoID{ get; set; }
        public string CreadoPor { get; set; }
        public Fondeador Fondeador { get; set; }
        public DateTime Creado { get; set; }
        public int n { get; set; }
        public DateTime Modificado { get; set; }
        public DateTime FechaDesembolso { get; set; }
        public decimal precio{ get; set; }
        public List<Credito> Creditos { get; set; }
        public Producto Producto { get; set; }
        public bool esrepro{ get; set; }
    }
}
