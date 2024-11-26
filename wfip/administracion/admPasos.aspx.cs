using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace wfip.administracion
{
    public partial class admPasos : System.Web.UI.Page
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
            lbIdEtapa.Visible = false;
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
            rptElementos.DataSource = (new wfiplib.admCatEtapas()).Lista(IdFlujo);
            rptElementos.DataBind();
        }

        private void enviaMsgCliente(string pMensaje) { lt_jsMsg.Text = "<script type='text/javascript'>$(document).ready(function () { alert('" + pMensaje + "'); });</script>"; }
        protected void btnCerrar_Click(object sender, EventArgs e) { Response.Redirect("admListaFlujos.aspx"); }

        protected void rptElementos_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            wfiplib.admCatEtapas _adm = new wfiplib.admCatEtapas();
            wfiplib.etapa _elemento = _adm.carga(Convert.ToInt32(e.CommandArgument));
            if (e.CommandName.Equals("activo"))
            {
                if (_elemento.IdEstado == 0) _adm.activa(_elemento.Id.ToString());
                else _adm.desactiva(_elemento.Id.ToString());
                pintaDatos();
            }
            if (e.CommandName.Equals("editar")) { pintaModificar(_elemento); }
        }

        protected void rptElementos_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                wfiplib.etapa registro = (wfiplib.etapa)e.Item.DataItem;
                if (registro.IdEstado == 0)
                {
                    ((ImageButton)e.Item.FindControl("imgBtnActivo")).ImageUrl = "~/img/inactivo.png";
                }
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            wfiplib.etapa datos = recuperaCaptura();
            (new wfiplib.admCatEtapas()).nuevo(datos);
            limpia();
            pintaDatos();
        }

        private void limpia() { txNombre.Text = ""; lbIdEtapa.Text = ""; lbIdEtapa.Visible = false; }

        private wfiplib.etapa recuperaCaptura()
        {
            wfiplib.etapa resultado = new wfiplib.etapa();
            resultado.IdFlujo = Convert.ToInt32(hf_IfFlujo.Value);
            resultado.Nombre = txNombre.Text.Trim().ToUpper();
            resultado.IdEstado = 1;
            return resultado;
        }

        private void pintaModificar(wfiplib.etapa pDatos)
        {
            try
            {
                limpia();
                hf_IdElemento.Value = pDatos.Id.ToString();
                txNombre.Text = pDatos.Nombre;
                lbIdEtapa.Text = "Etapa: " + pDatos.IdEtapa.ToString();
                lbIdEtapa.Visible = true;

                btnGuardar.Visible = false;
                btnModificar.Visible = true;
                btnModCancela.Visible = true;
            }
            catch (Exception ex) { enviaMsgCliente(ex.Message); }
        }

        protected void btnModCancela_Click(object sender, EventArgs e) { cancelaModifica(); }

        protected void btnModificar_Click(object sender, EventArgs e)
        {
            wfiplib.etapa _paso = recuperaCaptura();
            _paso.Id = Convert.ToInt32(hf_IdElemento.Value);
            (new wfiplib.admCatEtapas()).modifica(_paso);
            cancelaModifica();
            pintaDatos();
        }

        private void cancelaModifica()
        {
            limpia();
            btnGuardar.Visible = true;
            btnModificar.Visible = false;
            btnModCancela.Visible = false;
        }
    }
}