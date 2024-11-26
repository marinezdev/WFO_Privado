using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace wfip.administracion
{
    public partial class admPromotoriaUsuario : System.Web.UI.Page
    {
        wfiplib.Concentrado manejo_sesion = new wfiplib.Concentrado();

        protected void Page_Load(object sender, EventArgs e)
        {
            manejo_sesion = (wfiplib.Concentrado)Session["credencial"];
            if (!IsPostBack)
            {
                pintaLstPromotorias();
                pintaUsuariosDisponibles();
            }
        }

        private void pintaLstPromotorias()
        {
            ddlSelPromotoria.DataSource = (new wfiplib.admCatPromotoria(ConfigurationManager.ConnectionStrings["conecta_bd"].ConnectionString)).cargaComboPromotorias();
            ddlSelPromotoria.DataValueField = "valor";
            ddlSelPromotoria.DataTextField = "texto";
            ddlSelPromotoria.DataBind();
        }

        private void pintaUsuariosDisponibles()
        {
            rptUsrSinAsignar.DataSource = (new wfiplib.admCredencial()).usuariosSinPromotoria();
            rptUsrSinAsignar.DataBind();
        }

        protected void ddlSelPromotoria_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!ddlSelPromotoria.SelectedValue.Equals("0"))
            {
                ltNomPromotoria.Text = ddlSelPromotoria.SelectedItem.Text;
                pintaUsuariosPromotoria(ddlSelPromotoria.SelectedValue);
            }
            else ltNomPromotoria.Text = "";
        }

        protected void rptUsrSinAsignar_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName.Equals("agregar"))
            {
                if (!ddlSelPromotoria.SelectedValue.Equals("0"))
                {
                    (new wfiplib.admCredencial()).asignaPromotoria(ddlSelPromotoria.SelectedValue, e.CommandArgument.ToString());
                    pintaUsuariosDisponibles();
                    pintaUsuariosPromotoria(ddlSelPromotoria.SelectedValue);
                }
            }
        }

        private void pintaUsuariosPromotoria(string pIdPromotoria)
        {
            List<wfiplib.CboValorTexto> datos = (new wfiplib.admCredencial()).usuariosConPromotoria(pIdPromotoria);
            if (datos.Count > 0)
            {
                rptUsuariosAsignandos.DataSource = datos;
                rptUsuariosAsignandos.DataBind();
                rptUsuariosAsignandos.Visible = true;
            }
            else rptUsuariosAsignandos.Visible = false;
        }

        protected void rptUsuariosAsignandos_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName.Equals("quitar"))
            {
                (new wfiplib.admCredencial()).quitaPromotoria(e.CommandArgument.ToString());
                pintaUsuariosDisponibles();
                pintaUsuariosPromotoria(ddlSelPromotoria.SelectedValue);
            }
        }
    }
}