using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;

namespace wfip.promotoria
{
    public partial class anexoDocumento : System.Web.UI.Page
    {
        private int mIdTramite = 0;
        wfiplib.Concentrado manejo_sesion = new wfiplib.Concentrado();

        public string ArchivoMaximo1 { get; set; }
        public string ArchivoMaximo2 { get; set; }

        protected void Page_Init(object sender, EventArgs e)
        {
            if (Session["credencial"] == null)
                Response.Redirect("~/Default.aspx");
            manejo_sesion = (wfiplib.Concentrado)Session["credencial"];

            ArchivoMaximo1 = System.Web.Configuration.WebConfigurationManager.AppSettings["ArchivoMaximo1"].ToString();
            ArchivoMaximo2 = System.Web.Configuration.WebConfigurationManager.AppSettings["ArchivoMaximo2"].ToString();
        }

        private void enviaMsgCliente(string pMensaje)
        {
            lt_jsMsg.Text = "<script type='text/javascript'>$(document).ready(function () { alert('" + pMensaje + "'); });</script>";
        }

        protected void BtnCancelar_Click(object sender, EventArgs e)
        {
            Session.Remove("expediente");
            Session.Remove("insumos");
            Response.Redirect(EncripParametros("Id=" + mIdTramite.ToString(), "pmconsultaTramite.aspx").URL, true);
            //Response.Redirect("pmconsultaTramite.aspx?Id=" + mIdTramite.ToString());
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Propiedades.UrlCifrardo urlCifrardo = ConsultaParametros();

            if (urlCifrardo.Result)
            {
                mIdTramite = Convert.ToInt32(urlCifrardo.IdTramite);
                lt_jsMsg.Text = "";
                if (!IsPostBack)
                {
                    Session.Remove("expediente");
                    Session.Remove("insumos");
                    pintaDatosContratante(mIdTramite);
                }
            }
            else
            {
                Response.Redirect("~/Default.aspx");
            }
            
            
        }

        private void pintaDatosContratante(int pIdTramite)
        {
            try
            {
                wfiplib.tramiteP oTramite = (new wfiplib.admTramite()).carga(pIdTramite);
                switch (oTramite.IdTipoTramite)
                {
                    case wfiplib.E_TipoTramite.serviciosVida:
                        wfiplib.serviciosVidaP oServiciosVida = (new wfiplib.admServiciosVida()).carga(pIdTramite);
                        ltInfContratante.Text = oServiciosVida.CabeceraHtml;
                        break;
                    case wfiplib.E_TipoTramite.ServicioGmm:
                        wfiplib.ServicioGmm oServiciosGmm = (new wfiplib.admServicioGmm()).carga(pIdTramite);
                        ltInfContratante.Text = oServiciosGmm.CabeceraHtml;
                        break;
                    case wfiplib.E_TipoTramite.indPriEmisionVida:
                    case wfiplib.E_TipoTramite.indPriEmisionVidaCM:
                        //wfiplib.EmisionVida oEmisionVida = (new wfiplib.admEmisionVida()).carga(pIdTramite);
                        wfiplib.EmisionVG oEmisionVida = (new wfiplib.admEmisionVG()).carga(pIdTramite);
                        ltInfContratante.Text = oEmisionVida.DatosHtml;
                        break;
                    case wfiplib.E_TipoTramite.indPriEmisionGMM:
                        wfiplib.EmisionVG oEmisionGmm = (new wfiplib.admEmisionVG()).carga(pIdTramite);
                        //wfiplib.EmisionGmm oEmisionGmm = (new wfiplib.admEmisionGmm()).carga(pIdTramite);
                        ltInfContratante.Text = oEmisionGmm.DatosHtml;
                        break;
                    default:
                        break;
                }

                List<wfiplib.tramiteMesa> Lista = new List<wfiplib.tramiteMesa>();
                if (oTramite.Estado.Equals(wfiplib.E_EstadoTramite.Hold))
                {
                    Lista = (new wfiplib.admTramiteMesa()).DaMesasTramitesRevisionHold(pIdTramite);
                    rpObsrv.DataSource = Lista;
                    rpObsrv.DataBind();
                }

                if (oTramite.Estado.Equals(wfiplib.E_EstadoTramite.Suspendido))
                {
                    rpObsrv.DataSource = Lista;
                    rpObsrv.DataBind();
                }
            }
            catch (Exception ex) { enviaMsgCliente(ex.Message); }
        }

        /// <summary>
        /// Borrado del Expediente.
        /// </summary>
        /// <param name="pIdTramite"></param>
        private void BorrarExpediente(int pIdTramite)
        {
            //string strDoctoWeb = "";
            string strDoctoServer = "";

            try
            {
                wfiplib.tramiteP oTramite = (new wfiplib.admTramite()).carga(pIdTramite);
                if (oTramite.Estado == wfiplib.E_EstadoTramite.PCI)
                {
                    wfiplib.expediente ArchivoFusion = (new wfiplib.admExpediente()).daFusion(pIdTramite);
                    if (!string.IsNullOrEmpty(ArchivoFusion.NmArchivo))
                    {
                        //strDoctoWeb = "..\\DocsUp\\" + ArchivoFusion.NmArchivo;
                        // strDoctoServer = Server.MapPath("~") + "\\DocsUp\\" + ArchivoFusion.NmArchivo;
                        strDoctoServer = ArchivoFusion.CarpetaArchivada + ArchivoFusion.NmArchivo;
                        if (File.Exists(strDoctoServer))
                        {
                            File.Delete(strDoctoServer);
                        }
                    }
                }               
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        protected void btnSubirDocumento_Click(object sender, EventArgs e)
        {
            List<wfiplib.expediente> LstArchExpediente = new List<wfiplib.expediente>();
            if (Session["expediente"] != null)
            {
                LstArchExpediente = (List<wfiplib.expediente>)Session["expediente"];
            }

            if (fileUpDocumento.HasFile)
            {
                
                String fileExtension = System.IO.Path.GetExtension(fileUpDocumento.FileName).ToLower();
                string fileExtension2 = "";
                if (".pdf".Contains(fileExtension) ^ ".jpg".Contains(fileExtension) ^ ".png".Contains(fileExtension))
                {
                    try
                    {
                        wfiplib.expediente oExpediente = new wfiplib.expediente();
                        int fileSize = fileUpDocumento.PostedFile.ContentLength;
                        //if (fileSize < 41943040)
                        if (fileSize < int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ArchivoMaximoByte1"].ToString()))
                        {
                            BorrarExpediente(mIdTramite);

                            
                            DataTable data = new wfiplib.admExpediente().ControlArchivoNuevoID();
                            int _Id = 0;
                            string nombreArchivo = "";
                            foreach (DataRow control_Archivos in data.Rows)
                            {
                                _Id = Convert.ToInt32(control_Archivos["Id"].ToString());
                                nombreArchivo = control_Archivos["Clave"].ToString();
                            }
                            int IdControlArchivo = _Id;
                            string directorioTemporal = Server.MapPath("~") + oExpediente.CarpetaInicial;
                            //int IdArchivo = (new wfiplib.admDirectorio()).daSiguienteIdArchivo();
                            //string nombreArchivo = IdArchivo.ToString().PadLeft(8, '0');
                            //string directorioTemporal = Server.MapPath("~") + "\\DocsUp\\";
                            fileUpDocumento.PostedFile.SaveAs(directorioTemporal + nombreArchivo + fileExtension);
                            
                            /* COMBERTIR EL ARCHIVO A PDF ANTES DE SER ENVIADO A LA RUTA EN ESPECIFICO */
                            wfiplib.admPdfFusion ConJPGaPDF = new wfiplib.admPdfFusion();
                            if (!fileExtension.Equals(".pdf"))
                            {
                                if (ConJPGaPDF.ConviertePDF(directorioTemporal + nombreArchivo + fileExtension, directorioTemporal + nombreArchivo + ".pdf"))
                                {
                                    fileExtension2 = ".pdf";
                                }
                            }
                            fileExtension2 = ".pdf";

                            bool archivoEnPdf = false;
                            if (!fileExtension2.Equals(".pdf"))
                            {

                                //string origen = directorioTemporal + nombreArchivo + fileExtension;
                                //string destino = directorioTemporal + nombreArchivo + ".pdf";
                                //Jpgpdf obj = new Jpgpdf();
                                //archivoEnPdf = obj.convierte(origen, destino);
                                //if (archivoEnPdf) nombreArchivo = nombreArchivo + ".pdf";
                                archivoEnPdf = false;
                            }
                            else
                            {
                                nombreArchivo = nombreArchivo + fileExtension2;
                                archivoEnPdf = true;
                            }

                            if (archivoEnPdf)
                            {
                                oExpediente.IdTramite = mIdTramite;
                                //oExpediente.Id = IdArchivo;
                                oExpediente.Id = _Id;
                                oExpediente.NmArchivo = nombreArchivo;
                                oExpediente.NmOriginal = fileUpDocumento.FileName;
                                oExpediente.Activo = wfiplib.E_SiNo.Si;
                                oExpediente.Fusion = wfiplib.E_SiNo.No;

                                LstArchExpediente.Add(oExpediente);
                                lstDocumentos.DataSource = LstArchExpediente;
                                lstDocumentos.DataValueField = "Id";
                                lstDocumentos.DataTextField = "NmOriginal";
                                lstDocumentos.DataBind();

                                Session["expediente"] = LstArchExpediente;

                            }
                            else
                            {
                                enviaMsgCliente("El archivo no se puede convertir a PDF.");
                            }
                        }
                        else
                        {
                            enviaMsgCliente("El archivo excede el límite de " + ArchivoMaximo1 + "MB.");
                        }
                    }
                    catch (Exception ex)
                    {
                        enviaMsgCliente("Problemas con el archivo " + ex.Message);
                    }

                }
                else { enviaMsgCliente("El archivo no es un PDF."); }
            }
            else
            {
                /* No a cargado ningun tipo de archivo */
                enviaMsgCliente("No has seleccionado un archivo ");
            }
        }

        protected void btnSubirInsumo_Click(object sender, EventArgs e)
        {
            List<wfiplib.insumos> LstArchInsumos = new List<wfiplib.insumos>();
            if (Session["insumos"] != null) { LstArchInsumos = (List<wfiplib.insumos>)Session["insumos"]; }

            if (fileUpInsumo.HasFile)
            {
                try
                {
                    String fileExtension = System.IO.Path.GetExtension(fileUpInsumo.FileName).ToLower();
                    wfiplib.insumos oInsumo = new wfiplib.insumos();
                    int fileSize = fileUpInsumo.PostedFile.ContentLength;
                    //if (fileSize < 41943040)
                    if (fileSize < int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["ArchivoMaximoByte2"].ToString()))
                    {
                        DataTable data = new wfiplib.admExpediente().ControlArchivoNuevoID();
                        int _Id = 0;
                        string nombre = "";
                        foreach (DataRow control_Archivos in data.Rows)
                        {
                            _Id = Convert.ToInt32(control_Archivos["Id"].ToString());
                            nombre = control_Archivos["Clave"].ToString();
                        }

                        //int IdArchivo = (new wfiplib.admDirectorio()).daSiguienteIdArchivo();
                        //string nombreArchivo = IdArchivo.ToString().PadLeft(8, '0') + fileExtension;
                        //string directorioTemporal = "C:\\Sitios\\wfip\\wfip\\DocsDown\\";
                        string nombreArchivo = nombre + fileExtension;
                        string directorioTemporal = Server.MapPath("~") + oInsumo.CarpetaInicial;

                        //fileUpInsumo.PostedFile.SaveAs(Properties.Settings.Default.dirDoctosWeb + nombreArchivo);
                        fileUpInsumo.PostedFile.SaveAs(directorioTemporal + nombreArchivo);

                        oInsumo.IdTramite = mIdTramite;
                        oInsumo.Id = _Id;
                        oInsumo.NmArchivo = nombreArchivo;
                        oInsumo.NmOriginal = fileUpInsumo.FileName;
                        oInsumo.Activo = wfiplib.E_SiNo.Si;

                        LstArchInsumos.Add(oInsumo);
                        lstInsumos.DataSource = LstArchInsumos;
                        lstInsumos.DataValueField = "Id";
                        lstInsumos.DataTextField = "NmOriginal";
                        lstInsumos.DataBind();

                        Session["insumos"] = LstArchInsumos;
                    }
                    else
                    {
                        enviaMsgCliente("El archivo excede el límite de "+ ArchivoMaximo2 + "MB.");
                    }
                }
                catch (Exception ex)
                {
                    enviaMsgCliente("Problemas con el archivo " + ex.Message);
                }
            }
        }

        protected void btnEliminaDocumento_Click(object sender, EventArgs e)
        {
            if (lstDocumentos.Items.Count > 0 && lstDocumentos.SelectedIndex > -1)
            {
                List<wfiplib.expediente> LstArchExpediente = new List<wfiplib.expediente>();
                List<wfiplib.expediente> LstArchExpedienteTmp = new List<wfiplib.expediente>();
                if (Session["expediente"] != null) { LstArchExpediente = (List<wfiplib.expediente>)Session["expediente"]; }
                int contador = 0;
                foreach (wfiplib.expediente oExp in LstArchExpediente)
                {
                    if (contador != lstDocumentos.SelectedIndex) { LstArchExpedienteTmp.Add(oExp); }
                    contador += 1;
                }
                lstDocumentos.DataSource = LstArchExpedienteTmp;
                lstDocumentos.DataValueField = "Id";
                lstDocumentos.DataTextField = "NmOriginal";
                lstDocumentos.DataBind();
                Session["expediente"] = LstArchExpedienteTmp;
            }
        }

        protected void btnEliminaInsumo_Click(object sender, EventArgs e)
        {
            if (lstInsumos.Items.Count > 0 && lstInsumos.SelectedIndex > -1)
            {
                List<wfiplib.insumos> LstArchInsumos = new List<wfiplib.insumos>();
                List<wfiplib.insumos> LstArchInsumosTmp = new List<wfiplib.insumos>();
                if (Session["insumos"] != null) { LstArchInsumos = (List<wfiplib.insumos>)Session["insumos"]; }
                int contador = 0;
                foreach (wfiplib.insumos oInsumo in LstArchInsumos)
                {
                    if (contador != lstInsumos.SelectedIndex) { LstArchInsumosTmp.Add(oInsumo); }
                    contador += 1;
                }
                lstInsumos.DataSource = LstArchInsumosTmp;
                lstInsumos.DataValueField = "Id";
                lstInsumos.DataTextField = "NmOriginal";
                lstInsumos.DataBind();
                Session["insumos"] = LstArchInsumosTmp;
            }
        }

        protected void BtnContinuar_Click(object sender, EventArgs e)
        {
            List<wfiplib.expediente> LstExpediente = new List<wfiplib.expediente>();
            if (Session["expediente"] != null)
            {
                LstExpediente = (List<wfiplib.expediente>)Session["expediente"];
            }

            if (LstExpediente.Count > 0)
            {
                List<wfiplib.insumos> LstInsumos = new List<wfiplib.insumos>();
                if (Session["insumos"] != null)
                {
                    LstInsumos = (List<wfiplib.insumos>)Session["insumos"];
                }

                string msgError = registraDocExp(mIdTramite, LstExpediente);
                if (string.IsNullOrEmpty(msgError))
                {
                    registraDocInsumos(mIdTramite, LstInsumos);
                    Response.Redirect(EncripParametros("Id=" + mIdTramite.ToString(), "pmconsultaTramite.aspx").URL, true);
                    //Response.Redirect("pmconsultaTramite.aspx?Id=" + mIdTramite.ToString());
                }
                else enviaMsgCliente(msgError);
            }
            else enviaMsgCliente("Es necesario que ingrese los archivos para los documentos requeridos.");
        }

        private string registraDocExp(int pIdTramite, List<wfiplib.expediente> pLstExpediente)
        {

            /*String cadena = pIdTramite.ToString();
            string msgError = cadena;*/
            string msgError = "";
            
            try
            {
                wfiplib.expediente expediente = new wfiplib.expediente();

                // string directorioTemporal = "C:\\Sitios\\wfip\\wfip\\DocsDown\\";
                string directorioTemporal = Server.MapPath("~") + expediente.CarpetaInicial;

                wfiplib.admExpediente oAdmExp = new wfiplib.admExpediente();
                List<string> lstArchivos = new List<string>();
                foreach (wfiplib.expediente oExp in pLstExpediente)
                {
                    // string Origen = directorioTemporal + oExp.NmArchivo;
                    // string Destino = (new wfiplib.admDirectorio()).daDirectorio(Properties.Settings.Default.dirAlmacenDocumentos, oExp.Id) + oExp.NmArchivo;
                    // if (File.Exists(Origen)) { File.Copy(Origen, Destino, true); }
                    if (File.Exists(directorioTemporal + oExp.NmArchivo))
                    {
                        if (!oAdmExp.existe(oExp.IdTramite, oExp.Id))
                        {
                            oAdmExp.Nuevo(oExp);
                        }
                        lstArchivos.Add(directorioTemporal + oExp.NmArchivo);
                    }
                }

                
                
                wfiplib.expediente oFusionAnt = oAdmExp.daFusion(pIdTramite);
                //String cadena = oFusionAnt.Id.ToString();
                //msgError = cadena;
                
                string ArchFusionAnt = "";
                if (oFusionAnt.Id > 0)
                {
                    ArchFusionAnt = expediente.CarpetaArchivada + oFusionAnt.NmArchivo;
                }
                //msgError = ArchFusionAnt;


                //string nombreArchivo = IdArchivo.ToString().PadLeft(8, '0');

                DataTable data = new wfiplib.admExpediente().ControlArchivoNuevoID();
                int _Id = 0;
                string nombreArchivo = "";

                foreach (DataRow control_Archivos in data.Rows)
                {
                    _Id = Convert.ToInt32(control_Archivos["Id"].ToString());
                    nombreArchivo = control_Archivos["Clave"].ToString();
                }
                string nombreFusion = directorioTemporal + nombreArchivo + ".pdf";
                if (File.Exists(nombreFusion))
                {
                    File.Delete(nombreFusion);
                }

                //int IdArchivo = (new wfiplib.admDirectorio()).daSiguienteIdArchivo();
                ////string nombreFusion = Properties.Settings.Default.dirDoctosWeb + IdArchivo.ToString().PadLeft(8, '0') + ".pdf";
                //string nombreFusion = directorioTemporal + IdArchivo.ToString().PadLeft(8, '0') + ".pdf";
                //if (File.Exists(nombreFusion)) {
                //    File.Delete(nombreFusion);
                //}

                //wfiplib.credencial oCredencial = (wfiplib.credencial)Session["credencial"];
                //string nmSeparador =Properties.Settings.Default.dirDoctosWeb + oCredencial.Usuario + ".pdf";
                string nmSeparador = directorioTemporal + manejo_sesion.Credencial.Usuario + ".pdf";
                string nmLogo = Server.MapPath("~\\img") + @"\logo_sep.png";
                msgError = (new wfiplib.admPdfFusion()).Adiciona(lstArchivos, ArchFusionAnt, nombreFusion, manejo_sesion.Credencial.Nombre, nmSeparador, nmLogo);
                
                
                if (string.IsNullOrEmpty(msgError))
                {
                    // string Destino = directorioTemporal + IdArchivo.ToString().PadLeft(8, '0') + ".pdf";
                    // File.Copy(nombreFusion, Destino, true);
                    wfiplib.expediente oFusion = new wfiplib.expediente();
                    oFusion.IdTramite = pIdTramite;
                    oFusion.Id = _Id;
                    oFusion.NmArchivo = nombreArchivo + ".pdf";
                    oFusion.NmOriginal = "";
                    oFusion.Activo = wfiplib.E_SiNo.Si;
                    oFusion.Fusion = wfiplib.E_SiNo.Si;
                    oAdmExp.eliminaFusion(pIdTramite);
                    oAdmExp.Nuevo(oFusion);

                    File.Copy(nombreFusion, expediente.CarpetaArchivada + nombreArchivo + ".pdf");

                    msgError = "";
                }
            }
            catch (Exception ex) { msgError = ex.Message; }
            return msgError;
        }

        private bool registraDocInsumos(int pIdTramite, List<wfiplib.insumos> pLstInsumos)
        {
            bool resultado = false;
            try
            {
                string strArchivoOrigen = "";
                wfiplib.admInsumos oAdmInsumos = new wfiplib.admInsumos();
                foreach (wfiplib.insumos oInsumo in pLstInsumos)
                {
                    strArchivoOrigen = Server.MapPath("~") + oInsumo.CarpetaInicial + oInsumo.NmArchivo;
                    if (File.Exists(strArchivoOrigen))
                    {
                        oAdmInsumos.nuevo(oInsumo);
                        File.Copy(strArchivoOrigen, oInsumo.CarpetaArchivada + oInsumo.NmArchivo);
                    }

                    //string Origen = Properties.Settings.Default.dirDoctosWeb + oInsumo.NmArchivo;
                    //string Destino = (new wfiplib.admDirectorio()).daDirectorio(Properties.Settings.Default.dirAlmacenDocumentos, oInsumo.Id) + oInsumo.NmArchivo;
                    //if (File.Exists(Origen)) File.Copy(Origen, Destino, true);
                    //if (File.Exists(Destino))
                    //{
                    //    if (!oAdmInsumos.existe(oInsumo.IdTramite, oInsumo.Id)) { oAdmInsumos.nuevo(oInsumo); }
                    //}
                }
                resultado = true;
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); resultado = false; }
            return resultado;
        }


        private Propiedades.UrlCifrardo ConsultaParametros()
        {
            Propiedades.UrlCifrardo urlCifrardo = new Propiedades.UrlCifrardo();
            try
            {
                string parametros = (new Application.Operacion.UrlCifrardo()).Decrypt(Request.QueryString["data"].ToString());
                string IdTramite = "";

                String[] spearator = { "," };
                String[] strlist = parametros.Split(spearator, StringSplitOptions.RemoveEmptyEntries);

                foreach (String s in strlist)
                {

                    string BusqeudaIdTramite = stringBetween(s + ".", "Id=", ".");
                    if (BusqeudaIdTramite.Length > 0)
                    {
                        IdTramite = BusqeudaIdTramite;
                    }
                }

                if (IdTramite.Length > 0)
                {
                    urlCifrardo.IdTramite = IdTramite.ToString();
                    urlCifrardo.Result = true;
                }
                else
                {
                    urlCifrardo.IdTramite = "";
                }
            }
            catch (Exception)
            {
                urlCifrardo.Result = false;
            }

            return urlCifrardo;
        }
        private Propiedades.UrlCifrardo EncripParametros(string parametros, string Direccion)
        {
            Propiedades.UrlCifrardo urlCifrardo = new Propiedades.UrlCifrardo();

            string Encrypt = (new Application.Operacion.UrlCifrardo()).Encrypt(parametros);

            urlCifrardo.URL = Direccion + "?data=" + Encrypt;

            return urlCifrardo;
        }
        public static string stringBetween(string Source, string Start, string End)
        {
            string result = "";
            if (Source.Contains(Start) && Source.Contains(End))
            {
                int StartIndex = Source.IndexOf(Start, 0) + Start.Length;
                int EndIndex = Source.IndexOf(End, StartIndex);
                result = Source.Substring(StartIndex, EndIndex - StartIndex);
                return result;
            }

            return result;
        }
    }
}