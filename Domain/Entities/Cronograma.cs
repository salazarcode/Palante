using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class Cronograma
    {
        public int nNroCalendario { get; set; }
        public List<Cuota> Cuotas { get; set; }
    }
}
