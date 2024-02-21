using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GuerreroWeb.Models
{
    public class CatFormaPago
    {
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
    }
    public class CatImpuestos
    {
        public int IdImp { get; set; }
        public string Impuesto { get; set; }
        public bool Retencion { get; set; }
        public bool Traslada { get; set; }
        public string ClaveSat { get; set; }
        public decimal Porcentaje { get; set; }
    }
    public class CatMetodoPago
    {
        public int IdMetodo { get; set; }
        public string Clave { get; set; }
        public string MetodoPago { get; set; }
    }
    public class CatRegimenFiscal
    {
        public int IdRFiscal { get; set; }
        public string Clave { get; set; }
        public string RegimenF { get; set; }
    }
}