using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GuerreroWeb.Models
{
    public class CatPaises
    {
        public int IdPais { get; set; }
        public string Pais { get; set; }
        public string Abreviatura { get; set; }
        public bool EsDefault { get; set; }
    }
    public class CatEstados
    {
        public int IdEst { get; set; }
        public string Estado { get; set; }
        public string Abreviatura { get; set; }
        public bool EsDefault { get; set; }
        public int IdPais { get; set; }
    }
    public class CatCiudades
    {
        public int IdCiu { get; set; }
        public string Ciudad { get; set; }
        public bool EsDefault { get; set; }
        public int IdEst { get; set; }
    }
    public class CatColonias
    {
        public int IdCol { get; set; }
        public string Colonia { get; set; }
        public string CP { get; set; }
        public bool EsDefault { get; set; }
        public int IdCiu { get; set; }
    }
    public class VtCodPostal
    {
        public int IdCol { get; set; }
        public string Colonia { get; set; }
        public bool EsDefault { get; set; }
        public string CP { get; set; }
        public int IdCiu { get; set; }
        public string Ciudad { get; set; }
        public int IdEst { get; set; }
        public string Estado { get; set; }
        public int IdPais { get; set; }
        public string Pais { get; set; }
    }
    public class VtEstados
    {
        public int IdEst { get; set; }
        public string Estado { get; set; }
        public string Abreviatura { get; set; }
        public bool EsDefault { get; set; }
        public int IdPais { get; set; }
        public string Pais { get; set; }
        public string PaisAbrev { get; set; }
    }
    public class VtCiudades
    {
        public int IdCiu { get; set; }
        public string Ciudad { get; set; }
        public bool EsDefault { get; set; }
        public int IdEst { get; set; }
        public string Estado { get; set; }
        public string Abreviatura { get; set; }
        public int IdPais { get; set; }
        public string Pais { get; set; }
        public string PaisAbrev { get; set; }
    }
}