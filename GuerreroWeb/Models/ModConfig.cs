using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GuerreroWeb.Models
{
	public class ModConfig
	{
		public int IdConf { get; set; }
		public int IdEmpresa { get; set; }
		public int Decimales1 { get; set; }
		public int Decimales2 { get; set; }
		public bool VendeExt0 { get; set; }
		public bool SalidaExt0 { get; set; }
		public bool ActivoInv { get; set; }
		public bool ActivoVenta { get; set; }
		public bool ActivoComp { get; set; }
		public bool ActivoCXC { get; set; }
		public bool ActivoCXP { get; set; }
		public bool ActivoCont { get; set; }
		public bool ActivoWati { get; set; }
		public bool ActivoReporte { get; set; }
	}
}