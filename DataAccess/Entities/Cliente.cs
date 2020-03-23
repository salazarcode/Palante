using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Entities
{
    public class Cliente
    {
        public int ID { get; set; }
        public string Nombres { get; set; }
        public List<Prestamo> Prestamos { get; set; }
    }
}
