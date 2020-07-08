using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.ValueObjects
{
    public class ClientesCSV
	{
		public string TipoDocumento { get; set; }
		public string Id { get; set; }
		public string ApePat { get; set; }
		public string ApeMat { get; set; }
		public string Nombres { get; set; }
		public string FechaNacimiento { get; set; }
		public string Sexo { get; set; }
		public string EstadoCivil { get; set; }
		public string Ubigeo { get; set; }
		public string Direccion { get; set; }
		public string Telefono { get; set; }
	}
}
