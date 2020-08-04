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
        public List<PagoVO> Pagos { get; set; }
    }

    public class PagoVO
    {
        public string codigoFondeador { get; set; }
        public int nNroCuota { get; set; }
        public decimal Monto { get; set; }
    }
}
