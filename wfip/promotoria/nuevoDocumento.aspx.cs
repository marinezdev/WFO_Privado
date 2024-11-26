using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace wfip.promotoria
{
    public partial class nuevoDocumento : System.Web.UI.Page
    {
        private int mIdTramite = 0;
        protected void Page_Init(object sender, EventArgs e) { if (Session["credencial"] == null) Response.Redirect("~/Default.aspx"); }
        private void enviaMsgCliente(string pMensaje) { lt_jsMsg.Text = "<script type='text/javascript'>$(document).ready(function () { alert('" + pMensaje + "'); });</script>"; }
        protected void Page_Load(object sender, EventArgs e)
        {
            mIdTramite = Convert.ToInt32(Request.QueryString["id"]);
            if (!IsPostBack)
            {
                pintaDoctosDisponibles();
                pintaDoctosIntegrados();
                pintaDatosContratante(mIdTramite);
            }
        }

        private void pintaDoctosDisponibles()
        {
            wfiplib.tramite oTramite = (new wfiplib.admTramite()).carga(mIdTramite);
            List<wfiplib.tipoTramiteDoctos> listaDoctos = (new wfiplib.admTipoTramiteDoctos()).daListaDisponibles(oTramite.IdFlujo, oTramite.IdTipoTramite, oTramite.IdTramite);
            lstCatalogo.DataSource = listaDoctos;
            lstCatalogo.DataTextField = "Nombre";
            lstCatalogo.DataValueField = "IdTipoDocto";
            lstCatalogo.DataBind();
        }

        private void pintaDoctosIntegrados()
        {
            List<wfiplib.documento> listaDoctos = (new wfiplib.admDocumento()).daLista(mIdTramite);
            lstTramite.DataSource = listaDoctos;
            lstTramite.DataTextField = "Nombre";
            lstTramite.DataValueField = "IdTipoDocto";
            lstTramite.DataBind();
        }

        private void pintaDatosContratante(int pIdTramite)
        {
            wfiplib.tramite oTramite = (new wfiplib.admTramite()).carga(pIdTramite);
            switch (oTramite.IdTipoTramite)
            {
                case wfiplib.e_TipoTramite.serviciosVida:
                    wfiplib.serviciosVida oServiciosVida = (new wfiplib.admServiciosVida()).carga(pIdTramite);
                    ltInfContratante.Text = oServiciosVida.TextoHtml;
                    break;
                case wfiplib.e_TipoTramite.ServicioGmm:
                    wfiplib.ServicioGmm oServiciosGmm = (new wfiplib.admServicioGmm()).carga(pIdTramite);
                    ltInfContratante.Text = oServiciosGmm.TextoHtml;
                    break;
                case wfiplib.e_TipoTramite.nuevoVida:
                    wfiplib.nuevoVida oNuevoVida = (new wfiplib.admNuevoVida()).carga(pIdTramite);
                    ltInfContratante.Text = oNuevoVida.TextoHtml;
                    break;
                default:
                    break;
            }
        }

        protected void btnSubir_Click(object sender, EventArgs e)
        {
            try
            {
                if (lstCatalogo.SelectedIndex > -1 && !string.IsNullOrEmpty(FileUpload1.FileName))
                {
                    wfiplib.admDirectorio oAdmDir = new wfiplib.admDirectorio();
                    wfiplib.documento oDatos = new wfiplib.documento();
                    oDatos.IdArchivo = oAdmDir.daSiguienteIdArchivo();
                    oDatos.IdTramite = mIdTramite;
                    oDatos.IdTipoDocto = Convert.ToInt32(lstCatalogo.SelectedValue);
                    oDatos.NmArchivo = oDatos.IdArchivo.ToString().PadLeft(8, '0') + ".pdf";

                    string rutaBase = oAdmDir.daDirectorio(Properties.Settings.Default.dirDocumentos, oDatos.IdArchivo);
                    string nombreArchivo = rutaBase + oDatos.NmArchivo;
                    FileUpload1.SaveAs(nombreArchivo);

                    (new wfiplib.admDocumento()).nuevo(oDatos);
                    pintaDoctosIntegrados();
                    pintaDoctosDisponibles();
                }
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
        }

        protected void btnContinuar_Click(object sender, EventArgs e)
        {
            try
            {
                wfiplib.admTramiteMesa oAdmPaso = new wfiplib.admTramiteMesa();
                wfiplib.tramiteMesa datos = recuperaDatos();
                if (oAdmPaso.registra(datos))
                {
                    if (registraSiguiente(datos.IdMesa))
                    {
                        Response.Redirect("esperaPromotoria.aspx");
                    }
                }
            }
            catch (Exception ex) { enviaMsgCliente(ex.Message); }
        }

        private wfiplib.tramiteMesa recuperaDatos()
        {
            wfiplib.credencial oCredencial = (wfiplib.credencial)Session["credencial"];
            wfiplib.tramite oTramite = (new wfiplib.admTramite()).carga(mIdTramite);
            wfiplib.mesa oPrimerMesa = (new wfiplib.admMesa()).daPrimerMesa(oTramite.IdFlujo);

            wfiplib.tramiteMesa resultado = new wfiplib.tramiteMesa();
            resultado.IdTramite = oTramite.IdTramite;
            resultado.IdMesa = oPrimerMesa.IdMesa;
            resultado.IdFlujo = oTramite.IdFlujo;
            resultado.IdTipoTramite = oTramite.IdTipoTramite;
            resultado.Usuario = oCredencial.Usuario;
            resultado.Observacion = txComentarios.Text.Trim();
            resultado.Estado = wfiplib.e_EstadoMesa.Procesado;
            return resultado;
        }

        private bool registraSiguiente(int pIdMesa)
        {
            bool resultado = false;
            wfiplib.tramite oTramite = (new wfiplib.admTramite()).carga(mIdTramite);

            wfiplib.tramiteMesa siguiente = new wfiplib.tramiteMesa();
            siguiente.IdTramite = mIdTramite;
            siguiente.IdFlujo = oTramite.IdFlujo;
            siguiente.IdTipoTramite = oTramite.IdTipoTramite;
            
            List<wfiplib.mesa> lstSigMesa = (new wfiplib.admMesa()).daSiguienteMesa(oTramite.IdFlujo, pIdMesa);
            if (lstSigMesa.Count > 0)
            {
                wfiplib.admTramiteMesa oAdmPaso = new wfiplib.admTramiteMesa();
                foreach (wfiplib.mesa oMesa in lstSigMesa)
                {
                    siguiente.IdMesa = oMesa.IdMesa;
                    resultado = oAdmPaso.registra(siguiente);
                }
            }
            else
            {
                enviaMsgCliente("Flujo incompleto.");
            }
            return resultado;
        }
    }
}