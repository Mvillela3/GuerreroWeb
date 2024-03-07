using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GuerreroWeb.Models
{
	public class CatLineas
	{
		public int IdLinea { get; set; }
		public string Linea { get; set; }
	}
	public class CatCategorias
	{
		public int IdCat { get; set; }
		public int IdLinea { get; set; }
		public string Categoria { get; set; }
	}
	public class VtCategorias
	{
		public int IdCat { get; set; }
		public string Categoria { get; set; }
		public int IdLinea { get; set; }
		public string Linea { get; set; }
	}
	public class CatColores
	{
		public int IdColor { get; set; }
		public int IdCat { get; set; }
		public string Color { get; set; }
	}
	public class CatModelos
	{
		public int IdMod { get; set; }
		public int IdMarca { get; set; }
		public string Modelo { get; set; }
	}
	public class CatTallas
	{
		public int IdTalla { get; set; }
		public int IdCat { get; set; }
		public string Talla { get; set; }
	}
	public class CatUnidad
	{
		public string Unidad { get; set; }
		public string Descripcion { get; set; }
		public string ClaveSAT { get; set; }
	}
	public class CatArticulos
	{
		public int IdArt { get; set; }
		public string Codigo { get; set; }
		public string BarCode { get; set; }
		public string Descripcion { get; set; }
		public int idLin { get; set; }
		public int IdCat { get; set; }
		public int IdMarca { get; set; }
		public int IdMod { get; set; }
		public decimal CostoProm { get; set; }
		public decimal UltCosto { get; set; }
		public DateTime UltCompra { get; set; }
		public decimal Precio { get; set; }
		public decimal Contado { get; set; }
		public decimal PrecioMin { get; set; }
		public string Unidad { get; set; }
		public bool EsPaquete { get; set; }
		public bool EsServicio { get; set; }
		public string CodigoCFDI { get; set; }
		public string CContable1 { get; set; }
		public string CContable2 { get; set; }
		public string CContable3 { get; set; }
		public int IdImpuesto1 { get; set; }
		public int IdImpuesto2 { get; set; }
		public int IdImpuesto3 { get; set; }
		public string UsuAdd { get; set; }
		public DateTime FechaAdd { get; set; }
		public string UsuMod { get; set; }
		public DateTime FechaMod { get; set; }
		public string UsuDel { get; set; }
		public DateTime FechaDel { get; set; }
		public string Estatus { get; set; }
	}
	public class CatArticuloColores
	{
		public int IdArtColor { get; set; }
		public int IdArt { get; set; }
		public int IdColor { get; set; }
	}
	public class CatArticuloImagen
	{
		public int IdArtImg { get; set; }
		public int IdArt { get; set; }
		public string Imagen { get; set; }
	}
	public class CatArticuloTallas
	{
		public int IdArtTalla { get; set; }
		public int IdArt { get; set; }
		public int IdTalla { get; set; }
	}
	public class CatFamilias
	{
		public int IdFam { get; set; }
		public int IdCat { get; set; }
		public string Familia { get; set; }
	}
	public class VtFamilias
	{
		public int IdFam { get; set; }
		public string Familia { get; set; }
		public int IdCat { get; set; }
		public string Categoria { get; set; }
		public int IdLinea { get; set; }
		public string Linea { get; set; }
	}
	public class CatMarcas
	{
		public int IdMarca { get; set; }
		public string Marca { get; set; }
		public int IdLinea { get; set; }
	}
	public class VtMarcas
	{
		public int IdMarca { get; set; }
		public string Marca { get; set; }
		public int IdLinea { get; set; }
		public string Linea { get; set; }
	}
	public class VtModelos
	{
		public int IdMod { get; set; }
		public string Modelo { get; set; }
		public int IdMarca { get; set; }
		public string Marca { get; set; }
		public int IdLinea { get; set; }
		public string Linea { get; set; }
	}
	public class VtColores
	{
		public int IdColor { get; set; }
		public string Color { get; set; }
		public int IdCat { get; set; }
		public string Categoria { get; set; }
		public int IdLinea { get; set; }
		public string Linea { get; set; }
	}
}