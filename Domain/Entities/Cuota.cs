using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace Domain.Entities
{
    public class Cuota
    {
        public int nCodCred { get; set; }
        public int nCodAge { get; set; }
        public int nNroCalendario { get; set; }
        public int nNroCuota { get; set; }
        public DateTime dFecInicio { get; set; }
        public DateTime dFecVcto { get; set; }
        public DateTime dFecPagoCap { get; set; }
        public int nDias { get; set; }
        public decimal nCapitalReducido { get; set; }
        public decimal nCapital { get; set; }
        public decimal nIntereses { get; set; }
        public decimal nInteresComp { get; set; }
        public decimal nInteresMora { get; set; }
        public decimal nPerGracia { get; set; }
        public decimal nSeguro { get; set; }
        public decimal nSegDesgravamen { get; set; }
        public decimal nOtrosCobros { get; set; }
        public decimal nCuotaMensual { get; set; }
        public int nEstadoCuota { get; set; }
        public int nEstado { get; set; }
        public int nDiasAtrasoCuota { get; set; }
        public int nTipoCrono { get; set; }
        public string FondeadorID { get; set; }
        public string codigoFondeador { get; set; }
        public string codigopalante { get; set; }
        public int nSubProd { get; set; }
        public decimal Disponible { get; set; }

        public string CodigoCredito { get; set; }
        public string NumeroCuota { get; set; }
        public string FechaPago { get; set; }
        public string Amortizacion { get; set; }
        public string Interes { get; set; }
        public string PeriodoGracia { get; set; }
        public string Encaje { get; set; }
        public string TotalCuota { get; set; }

        public decimal Pagado { get; set; }
        public Credito Credito{ get; set; }
    }
}
