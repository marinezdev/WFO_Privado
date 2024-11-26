using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace wfip.supervision
{
    public partial class sprScore : System.Web.UI.Page
    {
        wfiplib.Concentrado manejo_sesion = new wfiplib.Concentrado();

        protected void Page_Init(object sender, EventArgs e)
        {
            if (Session["credencial"] == null)
                Response.Redirect("~/Default.aspx");
            manejo_sesion = (wfiplib.Concentrado)Session["credencial"];
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            //wfiplib.credencial oCredencial = (wfiplib.credencial)Session["credencial"];
            ltTituloGrafica.Text = (new wfiplib.admFlujo()).daNombre(manejo_sesion.Credencial.IdFlujo);
            pnlScore.Visible = (manejo_sesion.Credencial.IdFlujo == 1);
        }
    }
}