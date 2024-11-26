using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace wfip.administracion
{
    public partial class admMesas : System.Web.UI.Page
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
            lt_jsMsg.Text = "";
            if (!IsPostBack)
            {
                hf_IfFlujo.Value = Request.Params["Id"].ToString();
                pintaDatos();
            }
        }

        private void pintaDatos()
        {
            int IdFlujo = Convert.ToInt32(hf_IfFlujo.Value);
            lbNombreFlujo.Text = (new wfiplib.admFlujo()).daNombre(IdFlujo);
            rptElementos.DataSource = (new wfiplib.admMesa()).Lista(IdFlujo);
            rptElementos.DataBind();
            ddlTipo.DataSource = Enum.GetValues(typeof(wfiplib.E_TipoMesa));
            ddlTipo.DataBind();
        }

        private void enviaMsgCliente(string pMensaje) { lt_jsMsg.Text = "<script type='text/javascript'>$(document).ready(function () { alert('" + pMensaje + "'); });</script>"; }
        protected void btnCerrar_Click(object sender, EventArgs e) { Response.Redirect("admListaFlujos.aspx"); }

        protected void rptElementos_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            wfiplib.admMesa _admMesa = new wfiplib.admMesa();
            wfiplib.mesa _elemento = _admMesa.carga(Convert.ToInt32(e.CommandArgument));
            if (e.CommandName.Equals("activo"))
            {
                if (_elemento.eEstado == wfiplib.E_Estado.Inactivo) _admMesa.activa(_elemento.Id.ToString());
                else _admMesa.desactiva(_elemento.Id.ToString());
                pintaDatos();
            }
            if (e.CommandName.Equals("editar"))
            {
            }
        }

        protected void rptElementos_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                wfiplib.mesa registro = (wfiplib.mesa)e.Item.DataItem;
                if (registro.eEstado == wfiplib.E_Estado.Inactivo)
                {
                    ((ImageButton)e.Item.FindControl("imgBtnActivo")).ImageUrl = "~/img/inactivo.png";
                }
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            wfiplib.mesa datos = recuperaCaptura();
            (new wfiplib.admMesa()).nuevo(datos);
            limpia();
            pintaDatos();
        }

        private void limpia() { txNombre.Text = ""; }
        
        private wfiplib.mesa recuperaCaptura()
        {
            wfiplib.mesa resultado = new wfiplib.mesa();
            resultado.IdFlujo = Convert.ToInt32(hf_IfFlujo.Value);
            resultado.Nombre = txNombre.Text.Trim().ToUpper();
            //resultado.IdTipo = (wfiplib.e_TipoMesa)Enum.Parse(typeof(wfiplib.e_TipoMesa), ddlTipo.Text);
            resultado.IdTipo = Convert.ToInt32(ddlTipo.SelectedValue);
            resultado.IdEtapa = 1;
            resultado.IdMesaPadre = 0;
            resultado.IdEstado = (Int32)wfiplib.E_Estado.Activo;
            return resultado;
        }

        protected void btnModCancela_Click(object sender, EventArgs e)
        {

        }

        protected void btnModificar_Click(object sender, EventArgs e)
        {

        }
    }
}