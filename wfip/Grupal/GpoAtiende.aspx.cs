using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

namespace wfip.Grupal
{
    public partial class GpoAtiende : System.Web.UI.Page
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

        protected void BtnCerrar_Click(object sender, EventArgs e)
        {
            Response.Redirect("GpoEspera.aspx");
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Lt_jsMsg.Text = "";
            if (!IsPostBack)
            {
                if (Request.Params["id"] != null)
                {
                    Session.Remove("documentos");
                    PintaDatos(Request.Params["id"]);
                }
                else { Response.Redirect("GpoEspera.aspx"); }
            }
        }

        private void PintaDatos(string pId)
        {
            wfiplib.credencial _credencial = (wfiplib.credencial)Session["credencial"];
            wfiplib.AdmBuzonTramite _AdmBuzonTramite = new wfiplib.AdmBuzonTramite();

            CboBuzon.DataSource = _AdmBuzonTramite.CboBuzonesSalida(_credencial.Id);
            CboBuzon.DataValueField = "valor";
            CboBuzon.DataTextField = "texto";
            CboBuzon.DataBind();

            wfiplib.BuzonTramite tramite = _AdmBuzonTramite.Carga(pId);
            wfiplib.SolicitudGrupal solicitud = (new wfiplib.AdmSolicitudGrupal()).Carga(tramite.Tramite_Id);

            Hf_TramiteId.Value = tramite.Buzon_Id.ToString();
            Hf_TramiteId.Value = solicitud.IdTramite.ToString();
            TxAsunto.Text = solicitud.Asunto;
            txNombre.Text = solicitud.Nombre;
            txRfc.Text = solicitud.Rfc;
            txCalle.Text = solicitud.Calle;
            txNumExt.Text = solicitud.NoExterior;
            txNumInt.Text = solicitud.NoInterior;
            txCP.Text = solicitud.CP;
            TxColonia.Text = solicitud.Colonia;
            txMpio.Text = solicitud.Municipio;
            txCiudad.Text = solicitud.Ciudad;
            txEstado.Text = solicitud.Entidad;

            RptDocAnexados.DataSource = (new wfiplib.admExpediente()).DaLista(tramite.Tramite_Id);
            RptDocAnexados.DataBind();
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
                    int TramiteId = Convert.ToInt32(Hf_TramiteId.Value);
                    LstArchivosAnexo.ForEach(i => i.IdTramite = TramiteId);
                    string msgError = RegistraDocExp(LstArchivosAnexo);
                    if (string.IsNullOrEmpty(msgError))
                    {
                        wfiplib.BuzonTramite _BuzonTramiteEnvio = new wfiplib.BuzonTramite()
                        {
                            Buzon_Id = Convert.ToInt32(CboBuzon.SelectedValue),
                            Tramite_Id = TramiteId,
                            UsuarioRegistro_Id = _credencial.Id,
                            ObsEntrada = TxObs.Text,
                            Estado = wfiplib.E_EstadoBuzon.Espera
                        };
                        wfiplib.BuzonTramite _BuzonTramiteAtendido = new wfiplib.BuzonTramite()
                        {
                            Id = Convert.ToInt32(Request.Params["Id"]),
                            UsuarioAtiende_Id = _credencial.Id,
                            ObsSalida = TxObs.Text,
                            Estado = wfiplib.E_EstadoBuzon.Atendido
                        };

                        wfiplib.AdmBuzonTramite _AdmBuzonTramite = new wfiplib.AdmBuzonTramite();
                        if ((CboBuzon.SelectedItem).Text.Equals("TERMINADO"))
                        {
                            _BuzonTramiteEnvio.UsuarioAtiende_Id = _credencial.Id;
                            _BuzonTramiteEnvio.ObsSalida = TxObs.Text;
                            _BuzonTramiteEnvio.Estado = wfiplib.E_EstadoBuzon.Atendido;

                            _AdmBuzonTramite.Termina(_BuzonTramiteAtendido);
                            _AdmBuzonTramite.RegistraTerminado(_BuzonTramiteEnvio);
                            Response.Redirect("GpoEspera.aspx");
                        }
                        else
                        {
                            _AdmBuzonTramite.Termina(_BuzonTramiteAtendido);
                            _AdmBuzonTramite.Nuevo(_BuzonTramiteEnvio);
                            Response.Redirect("GpoEspera.aspx");
                        }
                    }
                    else { EnviaMsgCliente("Error al registrar los documentos"); }
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

        protected void RptDocAnexados_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName.Equals("Descargar"))
            {
                wfiplib.expediente archivo = (new wfiplib.admExpediente()).carga(Convert.ToInt32(e.CommandArgument));
                string archOrigen = (new wfiplib.admDirectorio()).daDirectorio(Properties.Settings.Default.dirAlmacenDocumentos, archivo.Id) + archivo.NmArchivo;
                Response.ContentType = ContentType;
                Response.AppendHeader("Content-Disposition", "attachment; filename=" + archivo.NmArchivo);
                Response.WriteFile(archOrigen);
                Response.End();
            }
        }
    }
}