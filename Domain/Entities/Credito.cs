﻿using System;
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
        public DateTime fechaVenta { get; set; }
        public DateTime fechaRecompra { get; set; }
        public DateTime fechaUltimoPago { get; set; }
        public int nEstado { get; set; }
        public int nNroCuotas { get; set; }
        public double nMontoCuota { get; set; }
        public int FondeadorID{ get; set; }
        public int Fondeador { get; set; }
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
        public string codigoFondeador { get; set; }
        public string nombreProducto { get; set; }
        public int pages { get; set; }
        public decimal nTasaComp { get; set; }
        public decimal nTasaMor { get; set; }
        public List<Cronograma> CronogramasPalante { get; set; }
        public List<Cronograma> CronogramasFondeador { get; set; }
        public decimal deuda { get; set; }
        public List<PagoDetalle> Pagos { get; set; }
        public List<PagoDetalle> Deudas { get; set; }
        public List<Cuota> CuotasVencidasVigentes { get; set; }
        public List<Cuota> Cuotas { get; set; }
        public string id { get; set; }
        public decimal disponible { get; set; }
        public int nSubProd { get; set; }
        public decimal CapitalSinPagar { get; set; }
        public decimal CapitalPorVencerSinPagar { get; set; }
        public decimal CuotasVencidasVigenteSinPagar { get; set; }
        public decimal CuotasVencidasVigenteSinPerGracia { get; set; }

        public decimal CapitalVigenteVencido { get; set; }
        public decimal GraciaVigenteVencido { get; set; }
        public decimal InteresVigenteVencido { get; set; }
        public decimal CapitalPorVencer { get; set; }

        public bool EsReprogramado { get; set; }
        public decimal PrecioRecompra { get; set; }
    }
}
