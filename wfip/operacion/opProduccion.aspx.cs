using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace wfip.operacion
{
    public partial class opProduccion : System.Web.UI.Page
    {
        protected void Page_Init(object sender, EventArgs e) { if (Session["credencial"] == null) Response.Redirect("~/Default.aspx"); }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) { pintaDatos(); pnProduccion.Visible = false; }
        }

        private void pintaDatos()
        {
            ddlUsuario.DataSource = (new wfiplib.admCredencial()).daListaUsuariosCbo(wfiplib.e_CredencialGrupo.Operador, wfiplib.e_Modulo.Operacion);
            ddlUsuario.DataTextField = "Nombre";
            ddlUsuario.DataValueField = "Usuario";
            ddlUsuario.DataBind();

            ddlMes.SelectedValue = DateTime.Now.Month.ToString().PadLeft(2, '0');
        }

        private void pintaTramites(string pUsuario)
        {
            wfiplib.admBitacora adm = new wfiplib.admBitacora();
            string fhInicio = "";
            string fhTermino = "";

            fhInicio = ddlAnio.SelectedValue + ddlMes.SelectedValue + "01";
            if (ddlMes.SelectedValue == "12") { fhTermino = (Convert.ToInt32(ddlAnio.SelectedValue) + 1).ToString() + "0101"; }
            else { fhTermino = ddlAnio.SelectedValue + (Convert.ToInt32(ddlMes.SelectedValue) + 1).ToString().PadLeft(2, '0') + "01"; }

            rpProduccion.DataSource = adm.daProduccionMes(pUsuario, fhInicio, fhTermino);
            rpProduccion.DataBind();
            pnProduccion.Visible = true;
        }

        protected void btnConsultar_Click(object sender, EventArgs e)
        {
            if (ddlUsuario.SelectedIndex > 0) { pintaTramites(ddlUsuario.SelectedValue); }
            else { pnProduccion.Visible = false; }
        }
    }
}