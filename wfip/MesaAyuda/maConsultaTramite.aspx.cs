using System;
using System.Collections.Generic;
using System.IO;
using System.Web.UI.WebControls;

namespace wfip.MesaAyuda
{
    public partial class maConsultaTramite : System.Web.UI.Page
    {
        wfiplib.Concentrado manejo_sesion = new wfiplib.Concentrado();
        public string Archivo = "";
        protected void Page_Init(object sender, EventArgs e)
        {
            if (Session["credencial"] == null)
                Response.Redirect("~/Default.aspx");
            manejo_sesion = (wfiplib.Concentrado)Session["credencial"];
        }

        private void enviaMsgCliente(string pMensaje)
        {
            lt_jsMsg.Text = "<script type='text/javascript'>$(document).ready(function () { alert('" + pMensaje + "'); });</script>";
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.Params["Id"] != null)
                    pintaTramite(Request.Params["Id"]);
            }
        }

        private void pintaTramite(string pIdTramite)
        {
            wfiplib.tramiteP oTramite = (new wfiplib.admTramite()).carga(Convert.ToInt32(pIdTramite));
            if (oTramite != null)
            {
                if (oTramite.Id > 0) { pintaEsteTramite(oTramite); }
                else { lbNoExiste.Visible = true; }
            }
            else { lbNoExiste.Visible = true; }
        }

        private void pintaEsteTramite(wfiplib.tramiteP pTramite)
        {
            lbFolio.Text = pTramite.Id.ToString().PadLeft(5, '0');
            lbFechaRegistro.Text = pTramite.FechaRegistro.ToString();
            lbFlujoNomnre.Text = pTramite.Flujo;
            lbTramiteNombre.Text = pTramite.TramiteNombre;
            pnlDatosTramite.Visible = true;
            pintaDatos(pTramite.Id);
            MuestraPDF(pTramite.Id);
            pintaInsumos(pTramite.Id);
        }

        private void pintaDatos(int pIdTramite)
        {
            wfiplib.tramiteP oTramite = (new wfiplib.admTramite()).carga(pIdTramite);
            switch (oTramite.IdTipoTramite)
            {
                case wfiplib.E_TipoTramite.serviciosVida:
                    wfiplib.serviciosVidaP oServiciosVida = (new wfiplib.admServiciosVida()).carga(pIdTramite);
                    ltInfContratante.Text = oServiciosVida.TextoHtml;
                    break;
                case wfiplib.E_TipoTramite.ServicioGmm:
                    wfiplib.ServicioGmm oServiciosGmm = (new wfiplib.admServicioGmm()).carga(pIdTramite);
                    ltInfContratante.Text = oServiciosGmm.TextoHtml;
                    break;
                case wfiplib.E_TipoTramite.indPriEmisionVida:
                    wfiplib.EmisionVG oEmisionVida = (new wfiplib.admEmisionVG()).carga(pIdTramite);
                    ltInfContratante.Text = oEmisionVida.DatosHtml;
                    break;
                case wfiplib.E_TipoTramite.indPriEmisionGMM:
                    wfiplib.EmisionVG oEmisionGmm = (new wfiplib.admEmisionVG()).carga(pIdTramite);
                    ltInfContratante.Text = oEmisionGmm.DatosHtml;
                    break;
                default:
                    break;
            }

            List<wfiplib.bitacora> lsBitacora = (new wfiplib.admBitacora()).daLista(pIdTramite);
            foreach (wfiplib.bitacora oBitacora in lsBitacora)
            {
                ltBitacora.Text = ltBitacora.Text + oBitacora.TextoHtml;
            }
            pnlDatosContratante.Visible = true;
        }

        private void pintaInsumos(int pIdTramite)
        {
            List<wfiplib.insumos> LstArchInsumos = new List<wfiplib.insumos>();
            LstArchInsumos = (new wfiplib.admInsumos()).daLista(pIdTramite);
            if (LstArchInsumos.Count > 0)
            {
                rptInsumos.DataSource = LstArchInsumos;
                rptInsumos.DataBind();
                btnMostrarInsumos.Visible = true;
            }
            else { rptInsumos.DataSource = null; rptInsumos.DataBind(); btnMostrarInsumos.Visible = false; }
        }

        private void MuestraPDF(int pIdTramite)
        {
            try
            {
                wfiplib.expediente ArchivoFusion = (new wfiplib.admExpediente()).daFusion(pIdTramite);
                if (!string.IsNullOrEmpty(ArchivoFusion.NmArchivo))
                {
                    string dirWebTemp = Properties.Settings.Default.dirDoctosWeb;
                    string archOrigen = (new wfiplib.admDirectorio()).daDirectorio(Properties.Settings.Default.dirAlmacenDocumentos, ArchivoFusion.Id) + ArchivoFusion.NmArchivo;
                    string archDestino = Properties.Settings.Default.dirDoctosWeb + ArchivoFusion.NmArchivo;
                    if (File.Exists(archOrigen)) { File.Copy(archOrigen, archDestino, true); }

                    if (File.Exists(archDestino))
                    {
                        Archivo = Properties.Settings.Default.urlDoctosWeb + ArchivoFusion.NmArchivo;
                        Session["consulta_docPop"] = Archivo;
                    }
                    else
                    {
                        Archivo = Properties.Settings.Default.urlDoctosWeb + "DocumentoError.pdf";
                        Session["consulta_docPop"] = Archivo;
                    }
                    ltMuestraPdf.Text = "<embed src='" + Archivo + "' style='width:100%; height:100%' type='application/pdf'>";
                }
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
        }

        protected void rptInsumos_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                try
                {
                    wfiplib.insumos oInsumo = (wfiplib.insumos)(e.Item.DataItem);
                    string archOrigen = (new wfiplib.admDirectorio()).daDirectorio(Properties.Settings.Default.dirAlmacenDocumentos, oInsumo.Id) + oInsumo.NmArchivo;
                    string archDestino = Properties.Settings.Default.dirDoctosWeb + oInsumo.NmArchivo;
                    if (!File.Exists(archDestino)) { File.Copy(archOrigen, archDestino); }
                    ImageButton btnExp = (ImageButton)(e.Item.FindControl("ImgExp"));
                    btnExp.OnClientClick = "Descarga('" + Properties.Settings.Default.urlDoctosWeb + oInsumo.NmArchivo + "'); return false;";
                }
                catch (Exception ex) { Console.WriteLine(ex.Message); }
            }
        }
    }
}