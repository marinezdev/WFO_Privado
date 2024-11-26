using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Web.UI.WebControls;

namespace wfip.MesaAyuda
{
    public partial class buscaTramite : System.Web.UI.Page
    {
        wfiplib.Concentrado manejo_sesion = new wfiplib.Concentrado();
        public string Archivo = "";

        protected void Page_Init(object sender, EventArgs e)
        {
            if (Session["credencial"] == null)
                Response.Redirect("~/Default.aspx");
        }

        private void enviaMsgCliente(string pMensaje)
        {
            lt_jsMsg.Text = "<script type='text/javascript'>$(document).ready(function () { alert('" + pMensaje + "'); });</script>";
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) { pnlSeleccionaFlujo.Visible = false; pintaListaDeFlujos(); limpiaPantalla(); }
        }

        private void pintaListaDeFlujos()
        {
            ddlLstFlujos.DataSource = (new wfiplib.admFlujo()).ListaCbo();
            ddlLstFlujos.DataTextField = "Nombre";
            ddlLstFlujos.DataValueField = "Id";
            ddlLstFlujos.DataBind();
        }

        protected void ddlLstFlujos_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void btnBuscaFilio_Click(object sender, EventArgs e)
        {
            limpiaPantalla();
            if (!string.IsNullOrEmpty(txFolioBuscar.Text))
            {
                wfiplib.tramiteP oTramite = (new wfiplib.admTramite()).carga(Convert.ToInt32(txFolioBuscar.Text));
                if (oTramite != null)
                {
                    if (oTramite.Id > 0) { pintaEsteTramite(oTramite); }
                    else { lbNoExiste.Visible = true; }
                }
                else { lbNoExiste.Visible = true; }
            }
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
            PintaHistoriaBitacora(pTramite.Id);
        }

        private void limpiaPantalla()
        {
            hdIdTramite.Value = "";
            ltInfContratante.Text = "";
            ltBitacora.Text = "";
            rptInsumos.DataSource = null;
            rptInsumos.DataBind();

            lbNoExiste.Visible = false;
            ltMuestraPdf.Text = "";

            pnlDatosContratante.Visible = false;
            pnlDatosTramite.Visible = false;
            pnlResultadoNombre.Visible = false;
            pnlDatosBitacora.Visible = false;
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

        private void limpiaDirectorioUsr(string Ruta, string CodigoUsr)
        {
            string NomArchivo = CodigoUsr + "*.*";
            string[] archivos = System.IO.Directory.GetFiles(Ruta, NomArchivo);
            foreach (string arch in archivos)
            {
                try { System.IO.File.Delete(arch); }
                catch (Exception e) { Console.WriteLine("{0} Exception caught.", e); }
            }
        }

        protected void btnBuscaNombre_Click(object sender, EventArgs e)
        {
            limpiaPantalla();
            if (!string.IsNullOrEmpty(txNombreBuscar.Text))
            {
                DataTable lstTramites = (new wfiplib.admTramite()).buscaEnDatosHtmlDataTable(txNombreBuscar.Text);
                if (lstTramites.Rows.Count > 0)
                {
                    pnlResultadoNombre.Visible = true;
                    rptResultadoNombre.DataSource = lstTramites;
                    rptResultadoNombre.DataBind();
                }
                else { lbNoExiste.Visible = true; }
            }
        }

        protected void rptResultadoNombre_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName.Equals("Consultar"))
            {
                wfiplib.tramiteP oTramite = (new wfiplib.admTramite()).carga(Convert.ToInt32(e.CommandArgument));
                if (oTramite != null)
                {
                    if (oTramite.Id > 0) { pintaEsteTramite(oTramite); }
                    else { lbNoExiste.Visible = true; }
                }
                else { lbNoExiste.Visible = true; }
            }
        }

        private void PintaHistoriaBitacora(int pId)
        {
            RptDatosBitacora.DataSource = (new wfiplib.admBitacora()).DaHistoria(pId);
            RptDatosBitacora.DataBind();
            pnlDatosBitacora.Visible = true;
        }

        protected void RptDatosBitacora_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                DataRowView drv = e.Item.DataItem as DataRowView;
                int tiempo = Convert.ToInt32(drv.Row["Tiempo"]);
                if(tiempo > 20)
                {
                    Image ctlImg = e.Item.FindControl("imgEstado") as Image;
                    ctlImg.ImageUrl = "~/img/estrellaRoja.png";
                }
            }
        }
    }
}