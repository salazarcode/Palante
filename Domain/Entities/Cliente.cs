using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class Cliente
    {
        public int cli_id { get; set; }
        public string cli_identificacion { get; set; }
        public string cli_nombre1 { get; set; }
        public string cli_nombre2 { get; set; }
        public string cli_apellido1 { get; set; }
        public string cli_apellido2 { get; set; }
        public string cli_sexo { get; set; }
        public string cli_nacionalidad { get; set; }
        public string cli_fecha_nacimiento { get; set; }
        public List<Credito> Creditos { get; set; }
    }
}

