using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

namespace wfip.Grupal
{
    public partial class GpoCaptura : System.Web.UI.Page
    {
        wfiplib.Concentrado manejo_sesion = new wfiplib.Concentrado();

        protected void Page_Init(object sender, EventArgs e)
        {
            if (Session["credencial"] == null)
                Response.Redirect("~/Default.aspx");
            manejo_sesion = (wfiplib.Concentrado)Session["credencial"];
        }

        private void EnviaMsgCliente(string pMensaje)
        {
            Lt_jsMsg.Text = "<script type='text/javascript'>$(document).ready(function () { alert('" + pMensaje + "'); });</script>";
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Lt_jsMsg.Text = "";
            if (!IsPostBack)
            {
                wfiplib.credencial _credencial = (wfiplib.credencial)Session["credencial"];
                Session.Remove("documentos");
                CboBuzon.DataSource = (new wfiplib.AdmBuzonTramite()).CboBuzonesSalida(_credencial.Id);
                CboBuzon.DataValueField = "valor";
                CboBuzon.DataTextField = "texto";
                CboBuzon.DataBind();
            }
        }

        protected void BtnCerrar_Click(object sender, EventArgs e)
        {
            Response.Redirect("GpoEspera.aspx");
        }

        protected void BtnBuscarCP_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txCP.Text)) { PintaColonias(txCP.Text); }
            else DpColonia.Enabled = false;
        }

        private void PintaColonias(string pCp)
        {
            List<wfiplib.valorTexto> datos = (new wfiplib.admCatCodigoPostal()).llenarCombo(pCp);
            if (datos.Count > 1)
            {
                DpColonia.DataSource = datos;
                DpColonia.DataValueField = "Valor";
                DpColonia.DataTextField = "Texto";
                DpColonia.DataBind();
                DpColonia.Enabled = true;
            }
            else DpColonia.Enabled = false;
        }

        protected void DpColonia_SelectedIndexChanged(object sender, EventArgs e)
        {
            txMpio.Text = "";
            txCiudad.Text = "";
            txEstado.Text = "";
            if ((DpColonia.SelectedValue != "0") && (!string.IsNullOrEmpty(txCP.Text)))
            {
                wfiplib.CodigoPostal datos = (new wfiplib.admCatCodigoPostal()).Carga(txCP.Text.Trim(), DpColonia.SelectedValue);
                txMpio.Text = datos.Municipio;
                txCiudad.Text = datos.Ciudad;
                txEstado.Text = datos.Estado;
            }
        }

        protected void BtnAgregaDocumento_Click(object sender, EventArgs e)
        {
            List<wfiplib.expediente> LstArchivosAnexo = new List<wfiplib.expediente>();
            if (Session["documentos"] != null) { LstArchivosAnexo = (List<wfiplib.expediente>)Session["documentos"]; }

            if (FupArchivo.HasFile)
            {
                String fileExtension = System.IO.Path.GetExtension(FupArchivo.FileName).ToLower();
                if (".pdf|.jpg".Contains(fileExtension))
                {
                    try
                    {
                        wfiplib.expediente oDocumento = new wfiplib.expediente();
                        int fileSize = FupArchivo.PostedFile.ContentLength;
                        if (fileSize < 41943040)
                        {
                            int IdArchivo = (new wfiplib.admDirectorio()).daSiguienteIdArchivo();
                            string nombreArchivo = IdArchivo.ToString().PadLeft(8, '0');
                            string directorioTemporal = Server.MapPath("~") + Properties.Settings.Default.dirCargaArchivos;
                            FupArchivo.PostedFile.SaveAs(directorioTemporal + nombreArchivo + fileExtension);

                            bool archivoEnPdf = false;
                            if (!fileExtension.Equals(".pdf"))
                            {
                                //string origen = directorioTemporal + nombreArchivo + fileExtension;
                                //string destino = directorioTemporal + nombreArchivo + ".pdf";
                                //promotoria.Jpgpdf obj = new promotoria.Jpgpdf();
                                //archivoEnPdf = obj.convierte(origen, destino);
                                //if (archivoEnPdf) { nombreArchivo = nombreArchivo + ".pdf"; }
                                archivoEnPdf = false;
                            }
                            else
                            {
                                nombreArchivo = nombreArchivo + fileExtension;
                                archivoEnPdf = true;
                            }

                            if (archivoEnPdf)
                            {
                                oDocumento.Id = IdArchivo;
                                oDocumento.NmArchivo = nombreArchivo;
                                oDocumento.NmOriginal = FupArchivo.FileName;
                                oDocumento.Activo = wfiplib.E_SiNo.Si;
                                oDocumento.Fusion = wfiplib.E_SiNo.No;
                                oDocumento.RutaTemporal = directorioTemporal;
                                oDocumento.Descripcion = txDescripcionDocto.Text;

                                LstArchivosAnexo.Add(oDocumento);
                                Session["documentos"] = LstArchivosAnexo;

                                RptDocumentos.DataSource = LstArchivosAnexo;
                                RptDocumentos.DataBind();
                                txDescripcionDocto.Text = "";
                            }
                            else { EnviaMsgCliente("El archivo no se puede convertir a PDF."); }
                        }
                        else { EnviaMsgCliente("El archivo excede el límite de 40MB."); }
                    }
                    catch (Exception ex)
                    {
                        EnviaMsgCliente("Problemas con el archivo " + ex.Message);
                    }
                }
                else { EnviaMsgCliente("El archivo no es un PDF o JPG."); }
            }

        }

        protected void RptDocumentos_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName.Equals("Eliminar"))
            {
                List<wfiplib.expediente> LstArchivosAnexo = (List<wfiplib.expediente>)Session["documentos"];
                int Id = Convert.ToInt32(e.CommandArgument);
                LstArchivosAnexo.Remove(LstArchivosAnexo.Single(r => r.Id == Id));
                Session["documentos"] = LstArchivosAnexo;
                RptDocumentos.DataSource = LstArchivosAnexo;
                RptDocumentos.DataBind();
            }
        }

        protected void BtnEnviar_Click(object sender, EventArgs e)
        {
            if (Session["documentos"] != null)
            {
                List<wfiplib.expediente> LstArchivosAnexo = (List<wfiplib.expediente>)Session["documentos"];
                if (LstArchivosAnexo.Count > 0)
                {
                    wfiplib.credencial _credencial = (wfiplib.credencial)Session["credencial"];
                    int Id = (new wfiplib.admTramite()).siguienteId();
                    LstArchivosAnexo.ForEach(i => i.IdTramite = Id);
                    wfiplib.SolicitudGrupal solicitud = RecuperaCaptura();
                    solicitud.IdTramite = Id;

                    string msgError = RegistraDocExp(LstArchivosAnexo);
                    if (string.IsNullOrEmpty(msgError))
                    {
                        wfiplib.AdmSolicitudGrupal _AdmSolicitudGrupal = new wfiplib.AdmSolicitudGrupal();
                        if (_AdmSolicitudGrupal.Nuevo(solicitud))
                        {
                            wfiplib.BuzonTramite _BuzonTramite = new wfiplib.BuzonTramite()
                            {
                                Buzon_Id = Convert.ToInt32(CboBuzon.SelectedValue),
                                Tramite_Id = Id,
                                UsuarioRegistro_Id = _credencial.Id,
                                ObsEntrada = TxObs.Text,
                                Estado = wfiplib.E_EstadoBuzon.Espera
                            };
                            wfiplib.AdmBuzonTramite _AdmBuzonTramite = new wfiplib.AdmBuzonTramite();
                            if (_AdmBuzonTramite.Nuevo(_BuzonTramite)) { Response.Redirect("GpoEspera.aspx"); }
                            else { EnviaMsgCliente("Error al registrar el tramite"); }
                        }
                        else { EnviaMsgCliente("Error al registrar los datos"); }
                    }
                }
                else { EnviaMsgCliente("Falta anexar documentos."); }
            }
        }

        private wfiplib.SolicitudGrupal RecuperaCaptura()
        {
            return new wfiplib.SolicitudGrupal
            {
                Asunto = TxAsunto.Text,
                Nombre = txNombre.Text,
                Rfc = txRfc.Text,
                Calle = txCalle.Text,
                NoExterior = txNumExt.Text,
                NoInterior = txNumInt.Text,
                CP = txCP.Text,
                Colonia = DpColonia.SelectedItem.Text,
                Municipio = txMpio.Text,
                Ciudad = txCiudad.Text,
                Entidad = txEstado.Text,
            };
        }

        private string RegistraDocExp(List<wfiplib.expediente> pListaArchivos)
        {
            string msgError = "";
            try
            {
                wfiplib.admExpediente oAdmExp = new wfiplib.admExpediente();
                foreach (wfiplib.expediente oArchivo in pListaArchivos)
                {
                    string Origen = oArchivo.RutaTemporal + oArchivo.NmArchivo;
                    string Destino = (new wfiplib.admDirectorio()).daDirectorio(Properties.Settings.Default.dirAlmacenDocumentos, oArchivo.Id) + oArchivo.NmArchivo;
                    if (File.Exists(Origen)) { File.Copy(Origen, Destino, true); }
                    if (File.Exists(Destino))
                    {
                        if (!oAdmExp.existe(oArchivo.IdTramite, oArchivo.Id)) { oAdmExp.Nuevo(oArchivo); }
                    }
                }
            }
            catch (Exception ex) { msgError = ex.Message; }
            return msgError;
        }
    }
}
