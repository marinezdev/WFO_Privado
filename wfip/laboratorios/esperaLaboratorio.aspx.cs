using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace wfip.laboratorios
{
    public partial class esperaLaboratorio : System.Web.UI.Page
    {
        wfiplib.ConcentradoLaboratorios manejo_sesion_labs = new wfiplib.ConcentradoLaboratorios();

        protected void Page_Init(object sender, EventArgs e)
        {
            if (Session["SesionLabs"] == null)
                Response.Redirect("Default.aspx");
            manejo_sesion_labs = (wfiplib.ConcentradoLaboratorios)Session["SesionLabs"];
        }
    }
}