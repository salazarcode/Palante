using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class Usuario
    {
        public int UsuarioID { get; set; }
        public string NombreCompleto { get; set; }
        public bool Sexo { get; set; }
        public string Nombre { get; set; }
        public string Clave { get; set; }
        public string SesionToken { get; set; }
        public DateTime Creado { get; set; }
        public Rol Rol{ get; set; }
    }
}
