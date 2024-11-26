using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace wfip.promotoria
{
    public partial class nuevoTramite : System.Web.UI.Page
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
            if (!IsPostBack) { Session.Contents.Remove("nota"); pnlEmisionServicio.Visible = true; }
        }

        protected void btnNuevo_Click(object sender, EventArgs e)
        {

        }

        protected void BtnRenovacion_Click(object sender, EventArgs e)
        {

        }

        protected void BtnEmisionServicio_Click(object sender, CommandEventArgs e)
        {
            switch(e.CommandName)
            {
                case "emision":
                    hfTipoTramite.Value = "emision";
                    lbTipoProducto.Text = "emision";
                    pnlEmisionServicio.Visible = false;
                    pnlVidaGastosMedicos.Visible = true;
                    //Response.Redirect("enconstruccion.aspx");
                    break;
                case "servicio":
                    hfTipoTramite.Value = "servicio";
                    lbTipoProducto.Text = "servicio";
                    pnlEmisionServicio.Visible = false;
                    pnlVidaGastosMedicos.Visible = true;
                    break;
                default:
                    lbMsg.Text = "ND";
                    break;
            }
        }

        protected void BtnVidaGmm_Click(object sender, CommandEventArgs e)
        {
            string paginaDestino = "";
            switch(e.CommandName)
            {
                case "vida":
                    if (hfTipoTramite.Value.Equals("servicio")) { paginaDestino = "servicioVida.aspx"; }
                    else if (hfTipoTramite.Value.Equals("emision")) { paginaDestino = "nuevoVida.aspx"; }
                    break;
                case "gmm":
                    if (hfTipoTramite.Value.Equals("servicio")) { paginaDestino = "servicioGmm.aspx"; }
                    else if (hfTipoTramite.Value.Equals("emision")) { }
                    break;
                default:
                    paginaDestino = "";
                    break;
            }
            if (!string.IsNullOrEmpty(paginaDestino)) Response.Redirect(paginaDestino);
        }
    }
}