using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public abstract class Persona : IPersona
    {
        public int nCodPers { get; set; }
        public int nTipoPersona { get; set; }
        public string cTelefono { get; set; }
        public int nCodigoCiudad { get; set; }
        public int nEstado { get; set; }
        public int nCategoria { get; set; }
        public DateTime dFecRegisto { get; set; }
    }
}
