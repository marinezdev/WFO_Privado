using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace wfip.promotoria
{
    public partial class EmisionGmm_Moral : System.Web.UI.Page
    {
        private wfiplib.E_TipoTramite mTipoTramite = wfiplib.E_TipoTramite.indPriEmisionGMM;
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
                Session.Remove(mTipoTramite.ToString());
                identificaPromotoria();
            }
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

        protected void BtnCancelar_Click(object sender, EventArgs e) { Response.Redirect("esperaPromotoria.aspx"); }
        private void enviaMsgCliente(string pMensaje) { lt_jsMsg.Text = "<script type='text/javascript'>$(document).ready(function () { alert('" + pMensaje + "'); });</script>"; }

        protected void BtnContinuar_Click(object sender, EventArgs e)
        {
            try
            {
                if (armaTramiteYGuardaEnMemoria()) { Response.Redirect("anexaArchivos.aspx"); }
            }
            catch (Exception ex) { enviaMsgCliente(ex.Message); }
        }

        private bool armaTramiteYGuardaEnMemoria()
        {
            wfiplib.EmisionGmm Datos = cargaDatos();

            int Id = (new wfiplib.admTramite()).siguienteId();
            //wfiplib.credencial oCredencial = (wfiplib.credencial)Session["credencial"];
            wfiplib.tramite oTramite = new wfiplib.tramite();

            oTramite.Id = Id;
            oTramite.IdTipoTramite = mTipoTramite;
            oTramite.IdPromotoria = manejo_sesion.Credencial.IdPromotoria;
            oTramite.IdUsuario = manejo_sesion.Credencial.Id;
            oTramite.DatosHtml = Datos.DatosHtml;
            oTramite.AgenteClave = Convert.ToInt32(txIdAgente.Text);
            Session["tramite"] = oTramite;

            Datos.IdTramite = Id;
            Session[oTramite.IdTipoTramite.ToString()] = Datos;

            return (Id > 0);
        }

        private wfiplib.EmisionGmm cargaDatos()
        {
            wfiplib.EmisionGmm Resultado = new wfiplib.EmisionGmm();

            Resultado.TipoPersona = wfiplib.E_TipoPersona.Moral;
            Resultado.NumFolio = txNumFolio.Text.Trim();
            Resultado.Nombre =  txNombre.Text.Trim();
            Resultado.RFC= txRfc.Text.Trim();
            Resultado.FolioMercantil= txFolioMerantil.Text.Trim();
            Resultado.DetalleGiroMercantil = txDetalleGiro.Text.Trim();
            Resultado.Representante1 = txRepresentante1.Text.Trim();
            Resultado.RepresentanteNacionalidad= txNacionalidad.Text.Trim();
            Resultado.Representante2 = txRepresentante2.Text.Trim();
            Resultado.Representante3 = txRepresentante3.Text.Trim();
            Resultado.Representante4=  txRepresentante4.Text.Trim();
            Resultado.Calle= txCalle.Text.Trim();
            Resultado.NoExterior= txNumExt.Text.Trim();
            Resultado.NoInterior= txNumInt.Text.Trim();
            Resultado.CP= txCP.Text.Trim();
            Resultado.Colonia=dpColonia.SelectedItem.Text.Trim();
            Resultado.Municipio= txMpio.Text.Trim();
            Resultado.Ciudad= txCiudad.Text.Trim();
            Resultado.Estado = txEstado.Text.Trim();
            Resultado.Pais= txPais.Text.Trim();
            Resultado.ZonaFronteriza = Convert.ToInt32 (dpZonaFonteriza.SelectedValue);
            Resultado.Telefono1 = txTelUno.Text.Trim();
            Resultado.Telefono2 = txTelefonoDos.Text.Trim();
            Resultado.Correo = txCorreo.Text.Trim();

            return Resultado;
        }

        protected void dpColonia_SelectedIndexChanged(object sender, EventArgs e)
        {
            txMpio.Text = "";
            txCiudad.Text = "";
            txEstado.Text = "";
            if ((dpColonia.SelectedValue != "0") && (!string.IsNullOrEmpty(txCP.Text)))
            {
                wfiplib.CodigoPostal datos = (new wfiplib.admCatCodigoPostal()).Carga(txCP.Text.Trim(), dpColonia.SelectedValue);
                txMpio.Text = datos.Municipio;
                txCiudad.Text = datos.Ciudad;
                txEstado.Text = datos.Estado;
            }
        }

        protected void btnBuscarCP_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txCP.Text)) { pintaColonias(txCP.Text); }
            else dpColonia.Enabled = false;
        }

        private void pintaColonias(string pCp)
        {
            List<wfiplib.valorTexto> datos = (new wfiplib.admCatCodigoPostal()).llenarCombo(pCp);
            if (datos.Count > 1)
            {
                dpColonia.DataSource = datos;
                dpColonia.DataValueField = "Valor";
                dpColonia.DataTextField = "Texto";
                dpColonia.DataBind();
                dpColonia.Enabled = true;
            }
            else dpColonia.Enabled = false;
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
    }
}