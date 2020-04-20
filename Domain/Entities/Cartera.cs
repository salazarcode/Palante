using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class Cartera
    {
        public int CarteraID { get; set; }
        public Fondeador Fondeador { get; set; }
        public DateTime Creado { get; set; }
        public DateTime Modificado { get; set; }
        public string CreadoPor { get; set; }
        public List<Credito> Creditos { get; set; }
    }
}
