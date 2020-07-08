using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class Paginacion
    {
        public int page { get; set; }
        public int pagesize { get; set; }
        public int producto { get; set; }
        public bool repro { get; set; }
        public int AmortizacionID { get; set; }
    }
}
