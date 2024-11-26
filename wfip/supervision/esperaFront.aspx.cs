using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace wfip
{
    public partial class esperaFront : System.Web.UI.Page
    {
        wfiplib.Concentrado manejo_sesion = new wfiplib.Concentrado();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["credencial"] == null)
                Response.Redirect("~/Default.aspx");

            manejo_sesion = (wfiplib.Concentrado)Session["credencial"];

#if DEBUG
            if (manejo_sesion.Credencial.Correo == "Veronica.martinez@metlife.com.mx")
#else
            if (manejo_sesion.Credencial.Correo == "robot.syntheticmonitoring@metlifeexternos.com.mx")
#endif
                Response.Redirect("Mapa.aspx");

        }
    }
}