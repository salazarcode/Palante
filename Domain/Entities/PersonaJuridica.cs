using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class PersonaJuridica : Persona, IPersona
    {
        public string cRazonSocial { get; set; }
        public string cRUC { get; set; }
    }
}
