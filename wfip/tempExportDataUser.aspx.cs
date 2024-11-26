using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace wfip
{
    public partial class tempExportDataUser : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            wfiplib.admCredencial adm = new wfiplib.admCredencial();
            adm.DesencriptarALLUsers("export_UsuariosWFONew", "contrasena", "Desifrado");
        }
    }
}