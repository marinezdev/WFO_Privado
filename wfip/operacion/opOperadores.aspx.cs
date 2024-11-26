using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace wfip.operacion
{
    public partial class opOperadores : System.Web.UI.Page
    {
        protected void Page_Init(object sender, EventArgs e) { if (Session["credencial"] == null) Response.Redirect("~/Default.aspx"); }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) { pintaDatos(); pnTrmPausa.Visible = false; pnTrmProceso.Visible = false; }
        }

        private void pintaDatos()
        {
            ddlUsuario.DataSource = (new wfiplib.admCredencial()).daListaUsuariosCbo(wfiplib.e_CredencialGrupo.Operador, wfiplib.e_Modulo.Operacion);
            ddlUsuario.DataTextField = "Nombre";
            ddlUsuario.DataValueField = "Usuario";
            ddlUsuario.DataBind();
        }

        protected void ddlUsuario_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlUsuario.SelectedIndex > 0) { pintaTramites(ddlUsuario.SelectedValue); }
            else { pnTrmPausa.Visible = false; pnTrmProceso.Visible = false; }
        }

        private void pintaTramites(string pUsuario)
        {
            wfiplib.admTramiteMesa adm = new wfiplib.admTramiteMesa();
            rptTrmPausa.DataSource = adm.daListaEnEspera(pUsuario);
            rptTrmPausa.DataBind();
            pnTrmPausa.Visible = true;

            rpTrmProceso.DataSource = adm.daListaAtrapados(pUsuario);
            rpTrmProceso.DataBind();
            pnTrmProceso.Visible = true;
        }

        protected void rptTrmPausa_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.CommandName)) {
                int idMesa = Convert.ToInt32(e.CommandName.ToString());
                int idTramite = Convert.ToInt32(e.CommandArgument.ToString());
                if ((new wfiplib.admTramiteMesa()).liberaTramite(idTramite, idMesa))
                {
                    wfiplib.admTramiteMesa adm = new wfiplib.admTramiteMesa();
                    rptTrmPausa.DataSource = adm.daListaEnEspera(ddlUsuario.SelectedValue);
                    rptTrmPausa.DataBind();
                    pnTrmPausa.Visible = true;
                }
            }
        }
    }
}