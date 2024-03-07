using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GuerreroWeb.Models
{
	public class ModModulos
	{
		public int IdMod { get; set; }
		public string Modulo { get; set; }
		public string Nombre { get; set; }
		public bool Activo { get; set; }
	}
	public class ModMovimientos
	{
		public int IdMovto { get; set; }
		public int IdMod { get; set; }
		public string Movimiento { get; set; }
		public string Tipo { get; set; }
		public bool Activo { get; set; }
		public bool AfectaInv { get; set; }
		public bool AfectaCont { get; set; }
		public bool AfectaCXP { get; set; }
		public bool AfectaCXC { get; set; }
	}
	public class VtMovimientos
	{
		public int IdMovto { get; set; }
		public string Movimiento { get; set; }
		public bool Activo { get; set; }
		public bool AfectaInv { get; set; }
		public bool AfectaCont { get; set; }
		public bool AfectaCXC { get; set; }
		public bool AfectaCXP { get; set; }
		public string Tipo { get; set; }
		public string TipoDesc { get; set; }
		public int IdMod { get; set; }
		public string Modulo { get; set; }
		public string ModuloNom { get; set; }
	}
	public class TipoMovtos
	{
		public string Tipo { get; set; }
		public string TipoDesc { get; set;}
	}
	public class CatFolios
	{
		public int IdFolio { get; set; }
		public int IdEmpresa { get; set; }
		public int IdMod { get; set; }
		public int IdMovto { get; set; }
		public int IdSuc { get; set; }
		public string Serie { get; set; }
		public int Consecutivo { get; set; }
	}
	public class VtFolios
	{
		public int IdFolio { get; set; }
		public int IdEmpresa { get; set; }
		public string Empresa { get; set; }
		public int IdMod { get; set; }
		public string Modulo { get; set; }
		public int IdMovto { get; set; }
		public string Movimiento { get; set; }
		public int IdSuc { get; set; }
		public string Sucursal { get; set; }
		public string Serie { get; set; }
		public int Consecutivo { get; set; }
	}

}