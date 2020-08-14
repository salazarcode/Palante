using System;

namespace Domain.Entities
{
    public class RecompraDetalle
    {
        public int RecompraID { get; set; }
        public string codigoFondeador { get; set; }

        public decimal CapitalVigenteVencido  { get; set; }
        public decimal GraciaVigenteVencido   { get; set; }
        public decimal InteresVigenteVencido  { get; set; }
        public decimal CapitalPorVencer { get; set; }
        public decimal Tasa { get; set; }
        public decimal CapitalTotal           { get; set; }
        public decimal GraciaTotal            { get; set; }
        public decimal InteresTotal           { get; set; }
        public decimal PrecioRecompra         { get; set; }
    }
}