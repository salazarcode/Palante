using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class Credito
    {
        public int nCodCred { get; set; }
        public int nCodAge { get; set; }
        public int nMoneda { get; set; }
        public double nPrestamo { get; set; }
        public DateTime dFechaVig { get; set; }
        public DateTime dFecIni { get; set; }
        public int nEstado { get; set; }
        public int nNroCuotas { get; set; }
        public double nMontoCuota { get; set; }
        public Persona Persona { get; set; }
        public string dni { get; set; }
        public string nombres { get; set; }
        public string ruc { get; set; }
        public string razonSocial { get; set; }
        public Producto Producto { get; set; }
        public bool DisponibleParaCartera { get; set; }
        public string precio { get; set; }
        public string ccodcta { get; set; }
        public int EsRepro { get; set; }
        public int codigoProducto { get; set; }
        public string nombreProducto { get; set; }
        public int pages { get; set; }
        public List<Cronograma> CronogramasPalante { get; set; }
        public List<Cronograma> CronogramasFondeador { get; set; }
    }
}
