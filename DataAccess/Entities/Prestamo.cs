using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Entities
{
    public class Prestamo
    {
        public int ID { get; set; }
        public double Capital { get; set; }
        public int ClienteID { get; set; }
    }
}
