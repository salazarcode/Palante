using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class PersonaNatural : Persona
    {
        public string cApePat { get; set; }
        public string cApeMat { get; set; }
        public string cNombres { get; set; }
        public int nSexo { get; set; }
        public DateTime dFecNac { get; set; }
        public string cDNI { get; set; }
        public string cMovil { get; set; }
        public string cMail { get; set; }
    }
}
