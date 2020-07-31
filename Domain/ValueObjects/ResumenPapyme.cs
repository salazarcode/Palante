using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.ValueObjects
{
    public class ResumenPapyme
	{
        public int ID { get; set; }
        public string NroOperacion { get; set; }
        public string DniClienteRepresentanteLegal { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string Nombre { get; set; }
        public string Fondeador { get; set; }
        public int NumeroVenta { get; set; }
        public string FechaVenta { get; set; }
        public string RUC { get; set; }
        public string RazonSocial { get; set; }
        public string FechaDesembolso { get; set; }
        public string Plaza { get; set; }
        public string TipoVivienda { get; set; }
        public string ValorComercial { get; set; }
        public string ValorRealizacion { get; set; }
        public string MontoAprobadoSoles { get; set; }
    }
}
