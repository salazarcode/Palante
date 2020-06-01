using System;
using System.Collections.Generic;
using System.Text;


namespace GestionCartera.API.ValueObjects
{
    public class SavePagoVO
    {
        public int PagoID { get; set; }
        public int FondeadorID { get; set; }
        public string Cuotas { get; set; }
    }
}
