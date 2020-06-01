using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.ValueObjects
{
    public class ClientesCSV
	{
		public string id { get; set; }
		public string nombre { get; set; }
		public string dFecNac { get; set; }
		public string nSexo { get; set; }
		public string nEstadoCivil { get; set; }
		public string ubigeo { get; set; }
		public string direccion { get; set; }
		public string cTelefono { get; set; }
	}
}
