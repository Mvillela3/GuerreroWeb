using GuerreroWeb.Controllers;
using GuerreroWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Drawing.Imaging;

namespace GuerreroWeb
{
    public partial class Inicio : System.Web.UI.Page
    {
        CtrlUsuarios Usuarios = new CtrlUsuarios();
        VtUsuarios mUsuario = new VtUsuarios();
        ModEmpleadoCumple mEmpleado = new ModEmpleadoCumple();
        CtrlEmpresas ctrlEmpresa = new CtrlEmpresas();

        protected void Page_Load(object sender, EventArgs e)
        {
            string usuario = "";

            if (Session["Usuario"] != null)
            {
                usuario = Session["Usuario"].ToString();
            }


            if (usuario.Length == 0)
            {
                Response.Redirect("~/Login.aspx");
            }

            var usuarioDet = Usuarios.VtUsuario(usuario);

            if (usuarioDet == null)
            {
                Response.Redirect("~/Login.aspx");
            }

            if (usuarioDet.Usuario == "Error")
            {
                Response.Redirect("~/Login.aspx");
            }

            //string prueba = "Charles from time to time opened his eyes, his mind grew weary, and, sleep coming upon him, he soon fell into a doze wherein, his recent sensations blending with memories, he became conscious of a double self, at once student and married man, lying in his bed as but now, and crossing the operation theatre as of old. The warm smell of poultices mingled in his brain with the fresh odour of dew; he heard the iron rings rattling along the curtain-rods of the bed and saw his wife sleeping. As he passed Vassonville he came upon a boy sitting on the grass at the edge of a ditch.";
            mUsuario.Usuario = usuarioDet.Usuario;
            //Label1.Text = prueba;
            //CreaImagen();

        }
        private void CreaImagen()
        {
            string lempleado = "Mes de " + DateTime.Now.ToString("MMMM").ToUpper() + "\n\n";
            string ImagenBan1 = @"~\Resources\FondoCumpleGro1.jpg";
			string ImagenBan2 = @"~\Resources\FondoCumpleGro2.jpg";
			string ImagenBan3 = @"~\Resources\FondoCumpleGro3.jpg";
			string ImagenBan4 = @"~\Resources\FondoCumpleGro4.jpg";
			string ImagenBan5 = @"~\Resources\FondoCumpleGro5.jpg";
			string dia2 = "";

            List<ModEmpleadoCumple> Lista = ctrlEmpresa.EmpleadosCumple("Atequiza");

            if (Lista.Count > 0)
            {
                //lempleado = "";

                for (int i = 0; i < Lista.Count; i++)
                {
                    dia2 = "0" + Lista[i].Dia;
                    lempleado += dia2.Substring(dia2.Length - 2 , 2) + " - " + Lista[i].NombreC + "\n";
                }
            }
            if (lempleado.Length > 0) {

            }
            //Font marcaAguaFuente = ObtenerFuente();

            System.Drawing.Image bitmap = (System.Drawing.Image)Bitmap.FromFile(Server.MapPath(ImagenBan2));
            //Dibujo la imagen
            Graphics graphicsImage = Graphics.FromImage(bitmap);

            //Establezco la orientación mediante coordenadas  
            StringFormat stringformat = new StringFormat();
            stringformat.Alignment = StringAlignment.Near;
            stringformat.LineAlignment = StringAlignment.Center;

            //StringFormat stringformat2 = new StringFormat();
            //stringformat2.Alignment = StringAlignment.Center;
            //stringformat2.LineAlignment = StringAlignment.Center;

            //Propiedades de la fuente  
            Color StringColor = System.Drawing.ColorTranslator.FromHtml("#000000");
            //le damos color
			//Color StringColor2 = System.Drawing.ColorTranslator.FromHtml("#e80c88");//le damos color
			//string Str_TextOnImage = "Happy";//Your Text On Image
			//string Str_TextOnImage2 = "Onam";//Your Text On Image
			
            Rectangle rect1;
			rect1 = new Rectangle(80, 100, 800, 400);


			graphicsImage.DrawString(lempleado, new Font("arial", 20, FontStyle.Regular), new SolidBrush(StringColor), rect1, stringformat); 
            Response.ContentType = "image/jpeg";

			//bitmap.Save(@"~\Resources\Banner2.jpg", ImageFormat.Jpeg);
			//bitmap.Save(Response.OutputStream, ImageFormat.Jpeg);
		}
        
    }
}