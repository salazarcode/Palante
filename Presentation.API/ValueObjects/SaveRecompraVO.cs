using System;
using System.Collections.Generic;
using System.Text;


namespace GestionCartera.API.ValueObjects
{
    public class SaveRecompraVO
    {
        public int RecompraID { get; set; }
        public int FondeadorID { get; set; }
        public int ProductoID { get; set; }
        public string Creditos { get; set; }
    }
}
