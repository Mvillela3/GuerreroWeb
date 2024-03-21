using GuerreroWeb.Controllers;
using GuerreroWeb.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using AjaxControlToolkit;

namespace GuerreroWeb
{
	/// <summary>
	/// Descripción breve de WSUsuarios
	/// </summary>
	[WebService(Namespace = "http://tempuri.org/")]
	[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
	[System.ComponentModel.ToolboxItem(false)]
	// Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
	 [System.Web.Script.Services.ScriptService]
	public class WSUsuarios : System.Web.Services.WebService
	{

		[WebMethod]
		[System.Web.Script.Services.ScriptMethod]
		public static List<string> AutoCompleta(string prefixText)
		{
			CtrlUsuarios Usuarios = new CtrlUsuarios();


			List<string> Lista = Usuarios.AutoCompletaUsuarios(prefixText);
			//List<string> Lista = new List<string>();

			//Lista.Add("juan");
			//Lista.Add("pedro");
			//Lista.Add("jose");
			//Lista.Add("josue");
			//Lista.Add("patricia");
			//Lista.Add("laura");

			return Lista;

		}

	}
}
