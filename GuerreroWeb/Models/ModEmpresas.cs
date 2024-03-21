using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GuerreroWeb.Models
{
    public class ModEmpresas
    {
        public int IdEmpresa { get; set; }
        public string Empresa { get; set; }
        public string NombreCom { get; set; }
        public string RFC { get; set; }
        public string Direccion { get; set; }
        public string NoExt { get; set; }
        public string NoInt { get; set; }
        public int IdCol { get; set; }
        public string CP { get; set; }
        public int IdCiu { get; set; }
        public int IdEst { get; set; }
        public int IdPais { get; set; }
        public int IdRFiscal { get; set; }
        public string PaginaWeb { get; set; }
        public string Email { get; set; }
        public string Telefono1 { get; set; }
        public string Telefono2 { get; set; }
    }
    public class VtEmpresas
    {
        public int IdEmpresa { get; set; }
        public string Empresa { get; set; }
        public string NombreCom { get; set; }
        public string RFC { get; set; }
        public string Direccion { get; set; }
        public string NoExt { get; set; }
        public string NoInt { get; set; }
        public int IdCol { get; set; }
        public string CP { get; set; }
        public int IdCiu { get; set; }
        public int IdEst { get; set; }
        public int IdPais { get; set; }
        public int IdRFiscal { get; set; }
        public string PaginaWeb { get; set; }
        public string Email { get; set; }
        public string Telefono1 { get; set; }
        public string Telefono2 { get; set; }
        public string DireccionC { get; set; }
        public string Colonia { get; set; }
        public string Ciudad { get; set; }
        public string Estado { get; set; }
        public string Pais { get; set; }
        public string RegimenF { get; set; }
        public string ClvRFiscal { get; set; }
    }
    public class CatAlmacenes
    {
        public int IdAlm { get; set; }
        public string Almacen { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public string NoExt { get; set; }
        public string NoInt { get; set; }
        public int IdCol { get; set; }
        public int IdCiu { get; set; }
        public int IdEst { get; set; }
        public int IdPais { get; set; }
        public string CP { get; set; }
        public int IdSuc { get; set; }
        public int IdEncargado { get; set; }
    }
    public class CatDepartamentos
    {
        public int IdDepto { get; set; }
        public string Departamento { get; set; }
    }
    public class CatSucursales
    {
        public int IdSuc { get; set; }
        public string Sucursal { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public string NoExt { get; set; }
        public string NoInt { get; set; }
        public int IdCol { get; set; }
        public int IdCiu { get; set; }
        public int IdEst { get; set; }
        public int IdPais { get; set; }
        public string CP { get; set; }
        public string Telefono1 { get; set; }
        public string Telefono2 { get; set; }
    }
    public class VtSucursales
    {
        public int IdSuc { get; set; }
        public string Sucursal { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public string NoExt { get; set; }
        public string NoInt { get; set; }
        public int IdCol { get; set; }
        public string CP { get; set; }
        public int IdCiu { get; set; }
        public int IdEst { get; set; }
        public int IdPais { get; set; }
        public string Telefono1 { get; set; }
        public string Telefono2 { get; set; }
        public string DireccionC { get; set; }
        public string Colonia { get; set; }
        public string Ciudad { get; set; }
        public string Estado { get; set; }
        public string Pais { get; set; }
    }
    public class VtAlmacenes
    {
        public int IdAlm { get; set; }
        public string Almacen { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public string NoExt { get; set; }
        public string NoInt { get; set; }
        public int IdCol { get; set; }
        public string CP { get; set; }
        public int IdCiu { get; set; }
        public int IdEst { get; set; }
        public int IdPais { get; set; }
        public string DireccionC { get; set; }
        public string Colonia { get; set; }
        public string Ciudad { get; set; }
        public string Estado { get; set; }
        public string Pais { get; set; }
        public string Sucursal { get; set; }
        public string SucursalNom { get; set; }
        public int IdSuc { get; set; }
        public int IdEncargado {  get; set; }
        public string Encargado { get; set; }   
    }
    public class CatCajas
    {
		public int IdCaja { get; set; }
		public string Caja { get; set; }
		public string NoCaja { get; set; }
		public int IdSuc { get; set; }
		public int IdDepto { get; set; }
		public int Consecutivo { get; set; }
		public bool Activo { get; set; }
	}
    public class VtCajas
    {
		public int IdCaja { get; set; }
		public string Caja { get; set; }
		public string NoCaja { get; set; }
		public int IdSuc { get; set; }
		public string Sucursal { get; set; }
		public int IdDepto { get; set; }
		public string Departamento { get; set; }
		public int Consecutivo { get; set; }
		public bool Activo { get; set; }
	}
	public class ModEmpleados
	{
		public int IdEmpleado { get; set; }
		public string Codigo { get; set; }
		public string Nombre { get; set; }
		public string Nombre1 { get; set; }
		public string Nombre2 { get; set; }
		public string Paterno { get; set; }
		public string Materno { get; set; }
		public string NombreC { get; set; }
		public DateTime FeNac { get; set; }
		public string CURP { get; set; }
		public string RFC { get; set; }
		public string NSS { get; set; }
		public DateTime FeAlta { get; set; }
		public string Departamento { get; set; }
		public string Puesto { get; set; }
		public string Telefono { get; set; }
		public string Email { get; set; }
		public string Sucursal { get; set; }


	}
    public class ModEmpleadoCumple
    {
		public string NombreC { get; set; }
		public string Sucursal { get; set; }
		public string Dia { get; set; }

	}
}