using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace wfip.supervision
{
    public partial class priorizarTramite : System.Web.UI.Page
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
            Muestradatos();
        }

        private void Muestradatos()
        {
            string tramite = string.Empty;
            string rfc = string.Empty;
            string contratante = string.Empty;
            string asegurado = string.Empty;
            int IdFlujo = manejo_sesion.Credencial.IdFlujo;
            DataTable dtU = new DataTable();

            if (!string.IsNullOrEmpty(txtTramite.Text)) tramite = "'%" + txtTramite.Text.Trim() + "%'";
            else tramite = "'%'";
            if (!string.IsNullOrEmpty(txtRFC.Text)) rfc = "'%" + txtRFC.Text.Trim() + "%'";
            else rfc = "'%'";
            if (!string.IsNullOrEmpty(txtContratante.Text)) contratante = "'%" + txtContratante.Text.Trim() + "%'";
            else contratante = "'%'";
            if (!string.IsNullOrEmpty(txtAsegurado.Text)) asegurado = "'%" + txtAsegurado.Text.Trim() + "%'";
            else asegurado = "'%'";

            DataTable dtT = new DataTable();
            dtT = (new wfiplib.admMesa()).TramitesEnOperacion(tramite, rfc, contratante, asegurado, 2, manejo_sesion.Credencial.Id);
            GridTramites.DataSource = dtT;
            GridTramites.DataBind();
        }

        protected void btnPriorizar_Command(object sender, CommandEventArgs e)
        {

            if (e.CommandName.Equals("Consultar"))
            {
                bool resultado = (new wfiplib.admMesa()).PriorizarTramites(e.CommandArgument.ToString(), "1");
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "Mensaje", "alert('Se prorizó el trámite con éxito')", true);
                // Muestradatos();
                //DetalleTramites.Update();
            }
        }
    }
}
