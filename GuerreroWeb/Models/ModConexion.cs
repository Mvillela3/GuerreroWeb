using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace GuerreroWeb.Models
{
    public class ModConexion
    {
        public static SqlConnection conn { get; set; }
        //public SqlCommand Cmd { get; set; }

    }
}