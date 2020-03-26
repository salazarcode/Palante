using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.ValueObjects
{
    public class PrestamoVO
    {
        public int ID { get; set; }
        public double Capital { get; set; }
        public int ClienteID { get; set; }
    }
}
