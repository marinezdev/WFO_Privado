using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace wfip.administracion
{
    public partial class admListaFlujos : System.Web.UI.Page
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
            if (!IsPostBack) { MuestraFLujos(); }
        }

        private void MuestraFLujos()
        {
            wfiplib.admFlujo adm = new wfiplib.admFlujo();
            List<wfiplib.flujo> Lista = adm.Lista();
            rpFlujos.DataSource = Lista;
            rpFlujos.DataBind();
        }

        protected void btnCerrar_Click(object sender, EventArgs e){Response.Redirect("admsysEspera.aspx");}

        protected void rpFlujos_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName.Equals("Ver")){Response.Redirect("admFlujo.aspx?Id=" + e.CommandArgument.ToString());}
            if (e.CommandName.Equals("Doctos")) { Response.Redirect("admDoctosFlujo.aspx?Id=" + e.CommandArgument.ToString()); }
            if (e.CommandName.Equals("mesas")) { Response.Redirect("admMesas.aspx?Id=" + e.CommandArgument.ToString()); }
            if (e.CommandName.Equals("pasos")) { Response.Redirect("admPasos.aspx?Id=" + e.CommandArgument.ToString()); }
        }

        protected void btnNuevo_Click(object sender, EventArgs e)
        {

        }
    }

}