using System;
using System.Collections.Generic;
using System.EnterpriseServices.Internal;
using System.Linq;
using System.Web;
using System.Windows.Forms;

namespace GuerreroWeb.Models
{
    public class ModMsgBox
    {
        public string Mensaje { get; set; }
        //public string Titulo {  get; set; }
        public MessageBoxButtons Boton { get; set; }
        public string Icono {get; set; }

    }
    public class MsgBoxIcon
    {
        public string Error = "Error";
        public string Warning = "Warning";
        public string Hand = "Hand";
        public string Exclamation = "Exclamation";
        public string Stop = "Stop";
        public string Question = "Question";
        public string OK = "OK";
        public string Information = "Information";
        public string Asterisk = "Asterisk";
        public string None = "None";

    }
}