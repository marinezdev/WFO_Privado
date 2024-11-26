using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace wfip.promotoria
{
    public partial class InsEmisionVida : System.Web.UI.Page
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
            if (!IsPostBack)
            {
                Session.Remove("tramite");
                identificaPromotoria();
            }
        }

        private void enviaMsgCliente(string pMensaje)
        {
            lt_jsMsg.Text = "<script type='text/javascript'>$(document).ready(function () { alert('" + pMensaje + "'); });</script>";
        }

        private void identificaPromotoria()
        {
            //wfiplib.credencial oCredencial = (wfiplib.credencial)Session["credencial"];
            if (manejo_sesion.Credencial.Modulo == wfiplib.E_Modulo.Promotoria)
            {
                if (manejo_sesion.Credencial.IdPromotoria > 0)
                {
                    hf_IdPromotoria.Value = manejo_sesion.Credencial.IdPromotoria.ToString();
                }
            }
        }

        protected void BtnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("esperaPromotoria.aspx");
        }

        protected void cboTipoContratante_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboTipoContratante.SelectedValue.Equals("1"))
            {
                pnPrsFisica.Visible = true; pnPrsMoral.Visible = false;
            }
            else if (cboTipoContratante.SelectedValue.Equals("2"))
            {
                pnPrsMoral.Visible = true; pnPrsFisica.Visible = false;
            }
            else { pnPrsFisica.Visible = false; pnPrsMoral.Visible = false; }

            lbNombreAgente.Text = daNombreDeAgente(hf_IdPromotoria.Value, txIdAgente.Text);
        }

        [System.Web.Services.WebMethod()]
        public static string TieneTramitesanteriores(string rfc)
        {
            string dato = "0";
            bool resultado = (new wfiplib.admServiciosUtiler()).buscaRFCAntecedente(rfc);
            if (resultado) { dato = "1"; }
            return dato;
        }

        [System.Web.Services.WebMethod()]
        public static string daNombreDeAgente(string pIdPromotoria, string pClaveAgente)
        {
            string resultado = "NO EXISTE";
            if (!string.IsNullOrEmpty(pIdPromotoria) && !string.IsNullOrEmpty(pClaveAgente))
            {
                wfiplib.Promotoria promotoria = (new wfiplib.admCatPromotoria(ConfigurationManager.ConnectionStrings["conecta_bd"].ConnectionString)).carga(Convert.ToInt32(pIdPromotoria));
                wfiplib.agentePromotoria agente = (new wfiplib.admAgentesPromotoria()).buscaAgenteEnPromotoria(promotoria.Clave, pClaveAgente);
                if (agente.clave > 0) resultado = agente.descripcion;
            }
            return resultado;
        }
        [ScriptMethod(), WebMethod()]
        public static string[] regresaNombreRegion(string pIdPromotoria)
        {
            String[] array = new string[10];
            array[0] = string.Concat(pIdPromotoria, " REGIÓN");
            array[1] = string.Concat(pIdPromotoria, " SUBDIRECCIÓN");
            array[2] = string.Concat(pIdPromotoria, " GERENTE COMERCIAL");
            array[3] = string.Concat(pIdPromotoria, " EJECUTIVO COMERCIAL");

            return array;
        }
    }
}