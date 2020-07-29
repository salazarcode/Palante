using System;
using System.Collections.Generic;
using System.Text;


namespace GestionCartera.API.ValueObjects
{
    public class SavePagoVO
    {
        public int PagoID { get; set; }
        public int FondeadorID { get; set; }
        public int ProductoID { get; set; }
        public bool EsMochila { get; set; }
        public string Creador { get; set; }
        public string Pagos { get; set; }
    }
}
