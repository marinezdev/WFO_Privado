using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace wfip.administracion
{
    public partial class admCatObservaciones : System.Web.UI.Page
    {
        wfiplib.Concentrado manejo_sesion = new wfiplib.Concentrado();

        protected void Page_Init(object sender, EventArgs e)
        {
            if (Session["credencial"] == null)
                Response.Redirect("~/Default.aspx");
            manejo_sesion = (wfiplib.Concentrado)Session["credencial"];
        }

        private void enviaMsgCliente(string pMensaje) { lt_jsMsg.Text = "<script type='text/javascript'>$(document).ready(function () { alert('" + pMensaje + "'); });</script>"; }

        protected void btnCerrar_Click(object sender, EventArgs e) { Response.Redirect("admsysEspera.aspx"); }

        protected void Page_Load(object sender, EventArgs e)
        {
            lt_jsMsg.Text = "";
            if (!IsPostBack) { llenaCboFlujo(); }
        }

        private void llenaCboFlujo()
        {
            ddlFlujo.DataSource = (new wfiplib.admFlujo()).ListaCbo();
            ddlFlujo.DataValueField = "Id";
            ddlFlujo.DataTextField = "Nombre";
            ddlFlujo.DataBind();
        }

        protected void ddlFlujo_SelectedIndexChanged(object sender, EventArgs e)
        {
            int idFlujo = Convert.ToInt32(ddlFlujo.SelectedValue);
            if (idFlujo > 0) llenaCboTipoTramite(idFlujo);
        }

        private void llenaCboTipoTramite(int pIdFlujo)
        {
            ddlTipoTramite.DataSource = (new wfiplib.admTipoTramite()).ListaCbo(pIdFlujo);
            ddlTipoTramite.DataValueField = "Id";
            ddlTipoTramite.DataTextField = "Nombre";
            ddlTipoTramite.DataBind();
        }

        protected void ddlTipoTramite_SelectedIndexChanged(object sender, EventArgs e)
        {
            int tipoTramite = Convert.ToInt32(ddlTipoTramite.SelectedValue);
            if (tipoTramite > 0 ) Consulta();
        }

        protected void btnConsultar_Click(object sender, EventArgs e) { Consulta(); }

        private void Consulta() {
            int idFlujo = Convert.ToInt32(ddlFlujo.SelectedValue);
            int IdTipotramite = Convert.ToInt32(ddlTipoTramite.SelectedValue);

            wfiplib.admCatMotivoRechazo admCatalogo = new wfiplib.admCatMotivoRechazo();

            // HOLD
            List<wfiplib.catMotivoRechazo> datosHold = admCatalogo.Lista(idFlujo, IdTipotramite, wfiplib.E_EstadoMesa.Hold);
            if (datosHold.Count > 0) { rptMotHold.DataSource = datosHold; rptMotHold.DataBind(); }
            else { rptMotHold.DataSource = null; rptMotHold.DataBind(); }

            // SUSPENDIDO
            List<wfiplib.catMotivoRechazo> datosSuspendido = admCatalogo.Lista(idFlujo, IdTipotramite, wfiplib.E_EstadoMesa.Suspendido);
            if (datosSuspendido.Count > 0) { rptMotSuspendido.DataSource = datosSuspendido; rptMotSuspendido.DataBind(); }
            else { rptMotSuspendido.DataSource = null; rptMotSuspendido.DataBind(); }

            // RECHAZO
            List<wfiplib.catMotivoRechazo> datosRechazo = admCatalogo.Lista(idFlujo, IdTipotramite, wfiplib.E_EstadoMesa.Rechazo);
            if (datosRechazo.Count > 0) { rptMotRechazo.DataSource = datosRechazo; rptMotRechazo.DataBind(); }
            else { rptMotRechazo.DataSource = null; rptMotRechazo.DataBind(); }
        }

        protected void btnRegistrar_Click(object sender, EventArgs e)
        {
            wfiplib.catMotivoRechazo oMotivo = new wfiplib.catMotivoRechazo();
            oMotivo.IdFlujo = Convert.ToInt32(ddlFlujo.SelectedValue);
            oMotivo.IdTipoTramite = Convert.ToInt32(ddlTipoTramite.SelectedValue);
            oMotivo.IdTipoRechazo = Convert.ToInt32(ddlTipoRechazo.SelectedValue);
            oMotivo.Nombre = txDescripcion.Text.Trim();
            if (oMotivo.Nombre.Length > 50) { oMotivo.Nombre = oMotivo.Nombre.Substring(1, 49); }
            wfiplib.admCatMotivoRechazo adm = new wfiplib.admCatMotivoRechazo();
            if (!adm.Existe(oMotivo))
            {
                if (adm.nuevo(oMotivo))
                {
                    Limpiar();
                    Consulta();
                }
                else { enviaMsgCliente("¡No se registro la información, intente nuevamente!"); }
            }
            else { enviaMsgCliente("La descripcion ya se encuentra registrada!"); }
        }

        private void Limpiar()
        {
            txDescripcion.Text = string.Empty;
        }       

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            btnModificar.Visible = false ;
            btnCancelar.Visible = false;
            btnRegistrar.Visible = true;
            this.Limpiar();
        }
        protected void btnModificar_Click(object sender, EventArgs e)
        {
            if(!string.IsNullOrEmpty(hdId.Value) && !string.IsNullOrEmpty (txDescripcion.Text.Trim())){
                wfiplib.catMotivoRechazo oMot = (new wfiplib.admCatMotivoRechazo()).carga(Convert.ToInt32(hdId.Value ));
                oMot.Nombre = txDescripcion.Text.Trim();
                wfiplib.admCatMotivoRechazo adm = new wfiplib.admCatMotivoRechazo();
                if (!adm.Existe(oMot)) {
                    bool resultado= adm.Modifica(hdId.Value, txDescripcion.Text.Trim());
                    btnModificar.Visible = false;
                    btnCancelar.Visible = false;
                    btnRegistrar.Visible = true;
                    Limpiar();
                    Consulta();
                }else { enviaMsgCliente("La descripcion ya existe!"); }             
            }else{enviaMsgCliente ("Datos incorrectos!");}             
        }

        protected void rptMotHold_ItemCommand(object source, RepeaterCommandEventArgs e)  { atiendeComando(e); }
        protected void rptMotSuspendido_ItemCommand(object source, RepeaterCommandEventArgs e)  { atiendeComando(e); }
        protected void rptMotRechazo_ItemCommand(object source, RepeaterCommandEventArgs e) { atiendeComando(e); }

        void atiendeComando(RepeaterCommandEventArgs e)
        {
            if (e.CommandName.Equals("Editar")) { pintaModifica(Convert.ToInt32(e.CommandArgument)); }
            if (e.CommandName.Equals("activo")) { activaDesactiva(Convert.ToInt32(e.CommandArgument)); }
        }

        void pintaModifica(int pId)
        {
            wfiplib.catMotivoRechazo oMot = (new wfiplib.admCatMotivoRechazo()).carga(pId);
            txDescripcion.Text = oMot.Nombre;
            hdId.Value = oMot.Id.ToString();
            btnModificar.Visible = true;
            btnCancelar.Visible = true;
            btnRegistrar.Visible = false;
        }

        void activaDesactiva(int pId)
        {
            wfiplib.admCatMotivoRechazo admCat = new wfiplib.admCatMotivoRechazo();
            wfiplib.catMotivoRechazo oMot = admCat.carga(pId);
            if(oMot.Activo == 1) admCat.cambiaActivo(pId,0);
            else admCat.cambiaActivo(pId, 1);
            Consulta();
        }

        protected void rptMotHold_ItemDataBound(object sender, RepeaterItemEventArgs e) { pintaBtnActivo(e); }
        protected void rptMotSuspendido_ItemDataBound(object sender, RepeaterItemEventArgs e) { pintaBtnActivo(e); }
        protected void rptMotRechazo_ItemDataBound(object sender, RepeaterItemEventArgs e) { pintaBtnActivo(e); }

        void pintaBtnActivo(RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                wfiplib.catMotivoRechazo registro = (wfiplib.catMotivoRechazo)e.Item.DataItem;
                if (registro.Activo == 0)
                {
                    ((ImageButton)e.Item.FindControl("imgBtnActivo")).ImageUrl = "~/img/inactivo.png";
                }
            }
        }


    }
}