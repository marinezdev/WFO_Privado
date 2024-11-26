using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace wfip.supervision
{
    public partial class opOperadoresR : System.Web.UI.Page
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
            if (!IsPostBack) { pintaDatos(); pnTrmPausa.Visible = false; pnTrmProceso.Visible = false; }
        }

        private void pintaDatos()
        {
            //wfiplib.credencial oCredencial = (wfiplib.credencial)Session["credencial"];
            List<wfiplib.credencial> datos = (new wfiplib.admCredencial()).daListaUsuariosReporte(wfiplib.E_CredencialGrupo.Operador, wfiplib.E_Modulo.Operacion, manejo_sesion.Credencial.IdFlujo);

            lstBxOperadores.DataSource = datos;
            lstBxOperadores.DataTextField = "Nombre";
            lstBxOperadores.DataValueField = "Id";
            lstBxOperadores.DataBind();
        }

        private void pintaTramites(int pIdUsuario)
        {
            wfiplib.admTramiteMesa adm = new wfiplib.admTramiteMesa();
            rptTrmPausa.DataSource = adm.daListaEnEspera(pIdUsuario);
            rptTrmPausa.DataBind();
            pnTrmPausa.Visible = true;

            rpTrmProceso.DataSource = adm.daListaAtrapados(pIdUsuario);
            rpTrmProceso.DataBind();
            pnTrmProceso.Visible = true;
        }

        protected void rptTrmPausa_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.CommandName))
            {
                int idMesa = Convert.ToInt32(e.CommandName.ToString());
                int idTramite = Convert.ToInt32(e.CommandArgument.ToString());
                if ((new wfiplib.admTramiteMesa()).liberaTramite(idTramite, idMesa))
                {
                    wfiplib.admTramiteMesa adm = new wfiplib.admTramiteMesa();
                    rptTrmPausa.DataSource = adm.daListaEnEspera(Convert.ToInt32(lstBxOperadores.SelectedValue));
                    rptTrmPausa.DataBind();
                    pnTrmPausa.Visible = true;
                }
            }
        }

        protected void lstBxOperadores_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstBxOperadores.SelectedIndex > -1) { pintaTramites(Convert.ToInt32(lstBxOperadores.SelectedValue)); }
            else { pnTrmPausa.Visible = false; pnTrmProceso.Visible = false; }
        }
    }
}