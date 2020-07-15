using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.ValueObjects
{
    public class CreditoSearch
    {
        public string Query { get; set; }
        public DateTime Fecha { get; set; }
        public bool EnFondeador{ get; set; }
    }
}
