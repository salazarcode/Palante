using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.ValueObjects
{
    public class ResumenPapyme
	{
		public string operacion { get; set; }
		public string cliente { get; set; }
		public string identificacion { get; set; }
		public string fechaVenta { get; set; }
		public string moneda { get; set; }
		public string consecionario { get; set; }
		public string marca { get; set; }
		public string modelo { get; set; }
		public string valorAutomovilUsd { get; set; }
		public string cuotaInicialUds { get; set; }
		public string porcIncial { get; set; }
		public string gpsSegurosYOtrosUsd { get; set; }
		public string montoConcedido { get; set; }
		public string cal { get; set; }
		public string tipoCliente { get; set; }

	}
}
