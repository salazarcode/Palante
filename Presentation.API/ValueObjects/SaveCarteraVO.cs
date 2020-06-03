using System;
using System.Collections.Generic;
using System.Text;


namespace GestionCartera.API.ValueObjects
{
    public class SaveCarteraVO
    {
        public int CarteraID { get; set; }
        public int FondeadorID { get; set; }
        public string Creditos { get; set; }
        public int ProductoID { get; set; }
        public DateTime Creado { get; set; }
    }
}
