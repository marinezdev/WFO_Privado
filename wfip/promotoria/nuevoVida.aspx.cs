using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace wfip.promotoria
{
    public partial class nuevoVida : System.Web.UI.Page
    {
        protected void Page_Init(object sender, EventArgs e) { if (Session["credencial"] == null) Response.Redirect("~/Default.aspx"); }
        private void enviaMsgCliente(string pMensaje) { lt_jsMsg.Text = "<script type='text/javascript'>$(document).ready(function () { alert('" + pMensaje + "'); });</script>"; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) { ocultaPaneles(); identificaPromotoria(); }
        }
        
        private void identificaPromotoria()
        {
            wfiplib.credencial oCredencial = (wfiplib.credencial)Session["credencial"];
            if (oCredencial.Modulo == wfiplib.e_Modulo.Promotoria && oCredencial.IdPromotoria > 0) { hf_IdPromotoria.Value = oCredencial.IdPromotoria.ToString(); }
        }

        private void ocultaPaneles() { pnlInfContratanteFisico.Visible = false; pnlInfContratanteMoral.Visible = false; }

        protected void cboTipoContratante_SelectedIndexChanged(object sender, EventArgs e)
        {
            ocultaPaneles();
            if (cboTipoContratante.SelectedValue.Equals("1")) pnlInfContratanteFisico.Visible = true;
            if (cboTipoContratante.SelectedValue.Equals("2")) pnlInfContratanteMoral.Visible = true;
        }

        protected void btnSigFisica_Click(object sender, EventArgs e) { nuevo(); }
        protected void btnSigMoral_Click(object sender, EventArgs e) { nuevo(); }

        private void nuevo()
        {
            //wfiplib.nuevoVida oDatos = recuperaCaptura();
            //if (validaCaptura(oDatos))
            //{
            //    wfiplib.credencial oCredencial = (wfiplib.credencial)Session["credencial"];
            //    wfiplib.tramite oTramite = new wfiplib.tramite();
            //    oTramite.IdFlujo = 2;
            //    oTramite.IdTipoTramite = wfiplib.e_TipoTramite.nuevoVida;
            //    oTramite.UsuarioRegistro = oCredencial.Usuario;

            //    wfiplib.admTramite _adm = new wfiplib.admTramite();
            //    oTramite.IdTramite = _adm.siguienteId();
            //    if (_adm.nuevo(oTramite))
            //    {
            ////        oDatos.Id = idTramite;
            ////        oDatos.NumTramite = idTramite.ToString().PadLeft(5, '0');
            ////        if ((new wfiplib.admNuevoVida()).nuevo(oDatos)) { Response.Redirect("nuevoDocumento.aspx?id=" + idTramite.ToString()); }
            //    }
            //}
            ////Response.Redirect("esperaPromotoria.aspx");
            try
            {
                wfiplib.nuevoVida oDatos = recuperaCaptura();
                if (validaCaptura(oDatos))
                {
                    int idTramite = armaTramiteYGuardaEnMemoria(oDatos.DatosHtml);
                    if (idTramite > 0)
                    {
                        oDatos.Id = idTramite;
                        Session[wfiplib.e_TipoTramite.nuevoVida.ToString()] = oDatos;
                        Response.Redirect("anexaArchivos.aspx");
                    }
                    else enviaMsgCliente("Error al guardar los datos de captura");
                }
                else enviaMsgCliente("Error en los datos de captura");
            }
            catch (Exception ex) { enviaMsgCliente(ex.Message); }
        }

        private int armaTramiteYGuardaEnMemoria(string DatosHtml)
        {
            int Id = (new wfiplib.admTramite()).siguienteId();
            wfiplib.credencial oCredencial = (wfiplib.credencial)Session["credencial"];
            wfiplib.tramite oTramite = new wfiplib.tramite();

            oTramite.Id = Id;
            oTramite.IdTipoTramite = wfiplib.e_TipoTramite.nuevoVida;
            oTramite.IdPromotoria = oCredencial.IdPromotoria;
            oTramite.IdUsuario = oCredencial.Id;
            oTramite.DatosHtml = DatosHtml;
            oTramite.AgenteClave = Convert.ToInt32(txIdAgente.Text);
            Session["tramite"] = oTramite;
            return Id;
        }

        private wfiplib.nuevoVida recuperaCaptura()
        {
            wfiplib.nuevoVida resultado = new wfiplib.nuevoVida();
            try
            {
                resultado.TipoSeguro = "vida";
                if (cboProducto.SelectedIndex > 0) resultado.Producto = cboProducto.SelectedItem.Text;
                resultado.NumSolCPDES = txSolicitud_cpdesweb.Text.Trim().ToUpper();
                resultado.NumSolicitud = txSolicitud.Text.Trim().ToUpper();
                if (cbopEstatus_cpdes.SelectedIndex > 0) resultado.EstatusCPDES = cbopEstatus_cpdes.SelectedItem.Text;
                if (!cboTipoContratante.SelectedValue.Equals("0"))
                {
                    resultado.IdTipoContratante = Convert.ToInt32(cboTipoContratante.SelectedValue);
                    resultado.TipoContratante = cboTipoContratante.SelectedItem.Text;
                    if (cboTipoContratante.SelectedValue.Equals("1"))
                    {
                        resultado.ApPaterno = txApPat.Text.Trim().ToUpper();
                        resultado.ApMaterno = txApMat.Text.Trim().ToUpper();
                        resultado.Nombre = txNombre.Text.Trim().ToUpper();
                        resultado.RFC = txRfc.Text.Trim().ToUpper();
                        resultado.CURP = txCurp.Text.Trim().ToUpper();
                        if (!string.IsNullOrEmpty(txFechaNacimiento.Text.Trim())) resultado.FhNacimiento = Convert.ToDateTime(txFechaNacimiento.Text.Trim());
                    }
                    else
                    {
                        resultado.Nombre = txNombreMoral.Text.Trim().ToUpper();
                        resultado.RFC = txRfcMoral.Text.Trim().ToUpper();
                        if (!string.IsNullOrEmpty(txFechaConstitucion.Text.Trim())) resultado.FhNacimiento = Convert.ToDateTime(txFechaConstitucion.Text.Trim());
                    }
                }
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
            return resultado;
        }

        private bool validaCaptura(wfiplib.nuevoVida pDatos)
        {
            bool resultado = true;
            //if (pDatos.IdTipoContratante == 1 && string.IsNullOrEmpty(pDatos.ApPaterno) && string.IsNullOrEmpty(pDatos.Nombre)) { resultado = false; }
            //else
            //{
            //    if (pDatos.IdTipoContratante == 2 && string.IsNullOrEmpty(pDatos.Nombre)) { resultado = false; }
            //}
            return resultado;
        }

        [System.Web.Services.WebMethod()]
        public static string daNombreDeAgente(string pIdPromotoria, string pClaveAgente)
        {
            string resultado = "NO EXISTE";
            if (!string.IsNullOrEmpty(pIdPromotoria) && !string.IsNullOrEmpty(pClaveAgente))
            {
                wfiplib.Promotoria promotoria = (new wfiplib.admCatPromotoria()).carga(Convert.ToInt32(pIdPromotoria));
                wfiplib.agentePromotoria agente = (new wfiplib.admAgentesPromotoria()).buscaAgenteEnPromotoria(promotoria.Clave, pClaveAgente);
                if (agente.clave > 0) resultado = agente.descripcion;
            }
            return resultado;
        }
    }
}