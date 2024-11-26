using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

namespace wfip.promotoria
{
    public partial class pmTramiteEjctd : System.Web.UI.Page
    {
        wfiplib.Concentrado manejo_sesion = new wfiplib.Concentrado();

        protected void Page_Init(object sender, EventArgs e)
        {
            if (Session["credencial"] == null)
                Response.Redirect("~/Default.aspx");
            manejo_sesion = (wfiplib.Concentrado)Session["credencial"];
        }

        private void enviaMsgCliente(string pMensaje) { lt_jsMsg.Text = "<script type='text/javascript'>$(document).ready(function () { alert('" + pMensaje + "'); });</script>"; }

        protected void Page_Load(object sender, EventArgs e)
        { 
            if (!IsPostBack)
            {
                Propiedades.UrlCifrardo urlCifrardo = ConsultaParametros();

                if (urlCifrardo.Result)
                {
                    pintaTramite(urlCifrardo.IdTramite);
                }
                //if (Request.Params["Id"] != null)
                //{
                //    pintaTramite(Request.Params["Id"]);
                //}
            }
            if (popCartaAceptado.IsCallback) MuestraCarta();
        }

        private void pintaTramite(string pIdTramite)
        {
            wfiplib.tramiteP oTramite = (new wfiplib.admTramite()).carga(Convert.ToInt32(pIdTramite));
            if (oTramite != null)
            {
                if (oTramite.Id > 0)
                {
                    pintaEsteTramite(oTramite);
                }
                else
                {
                    lbNoExiste.Visible = true;
                }
            }
            else
            {
                lbNoExiste.Visible = true;
            }
        }

        private void pintaEsteTramite(wfiplib.tramiteP pTramite)
        {
            lbFolio.Text = pTramite.FolioCompuesto;
            lbFechaRegistro.Text = pTramite.FechaRegistro.ToString();
            lbFlujoNomnre.Text = pTramite.Flujo;
            lbTramiteNombre.Text = pTramite.TramiteNombre;
            pnlDatosTramite.Visible = true;
            pintaDatos(pTramite.Id);
            MuestraPDF(pTramite.Id);
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

        private void MuestraPDF(int pIdTramite)
        {
          
            ltMuestraPdf.Text = "";
            ltMuestraPdf.Text = "<iframe src='" + EncripParametros("IdTramite=" + pIdTramite, "Displaypdf.aspx").URL + "' style='width:100%; height:540px' style='border: none;'></iframe>";

            //string strDoctoWeb = "";
            //string strDoctoServer = "";
            //try
            //{
            //    wfiplib.expediente ArchivoFusion = (new wfiplib.admExpediente()).daFusion(pIdTramite);
            //    if (!string.IsNullOrEmpty(ArchivoFusion.NmArchivo))
            //    {
            //        strDoctoWeb = "..\\DocsUp\\" + ArchivoFusion.NmArchivo;
            //        strDoctoServer = Server.MapPath("~") + "\\DocsUp\\" + ArchivoFusion.NmArchivo;
            //        if (File.Exists(strDoctoServer))
            //        {
            //            Session["consulta_docPop"] = strDoctoWeb;
            //        }
            //        else
            //        {
            //            strDoctoWeb = "..\\DocsTemplate\\DocumentoError.pdf";
            //            Session["consulta_docPop"] = strDoctoWeb;
            //        }
            //    }
            //    else
            //    {
            //        //TODO: Crear un documento que indique que el archivo no se fuciono o no se cargo desde el inicio del tramite
            //        strDoctoWeb = "..\\DocsTemplate\\DocumentoError.pdf";
            //        Session["consulta_docPop"] = strDoctoWeb;
            //    }
            //    ltMuestraPdf.Text = "<embed src='" + strDoctoWeb + "' style='width:100%; height:100%' type='application/pdf'>";
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine(ex.Message);
            //}


            /*
            try
            {
                wfiplib.expediente ArchivoFusion = (new wfiplib.admExpediente()).daFusion(pIdTramite);
                //wfiplib.expediente ArchivoFusion = (new wfiplib.admExpediente()).daFusion(7);


                if (!string.IsNullOrEmpty(ArchivoFusion.NmArchivo))
                {
                    // dirWebTemp = C:\\Users\\Rafael Villa Develop\\Desktop\\PRUEBAS\\wfip\\wfip\\DocsDown
                    string dirWebTemp = Properties.Settings.Default.dirDoctosWeb;
                    // dirAlmacenDocumentos == C:\\Metlife\\wfip\\
                    // NO MUESTRA LA RUTA CORRETA, AL COMPARARLA LA MARCARA ERRONEA AL NO EXISTIR DICHA RUTA Y MANDARA EL PDF DE ERROR 

                    //string archOrigen = (new wfiplib.admDirectorio()).daDirectorio(Properties.Settings.Default.dirAlmacenDocumentos, ArchivoFusion.Id) + ArchivoFusion.NmArchivo;
                    string archOrigen = "C:\\Metlife\\wfip\\00\\00\\00" + ArchivoFusion.NmArchivo;
                    //string archDestino = Properties.Settings.Default.dirDoctosWeb + ArchivoFusion.NmArchivo;
                    string archDestino = "C:\\Sitios\\wfip\\wfip\\DocsDown\\" + ArchivoFusion.NmArchivo;


                    
                    if (File.Exists(archOrigen))
                    {
                        File.Copy(archOrigen, archDestino, true);
                    }

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
            */
        }

        private void MuestraCarta()
        {
            ltCarta.Text = "<embed src='" + String.Format("http://{0}{1}", Request.Url.Authority, "/DocsDown/CartaAtencion.pdf") + "' style='width:100%; height:100%' type='application/pdf'>";
        }

        protected void btnImprimeCartaAceptacion_Click(object sender, EventArgs e)
        {
            Propiedades.UrlCifrardo urlCifrardo = ConsultaParametros();
            Response.Write("<script type = 'text/javascript' > window.open('" + EncripParametros("Id=" + urlCifrardo.IdTramite, "CartaPDFAceptado.aspx").URL + "', '_blank', 'PDF MetLife', 'width=400,height=500')</script >;");
            //Response.Write("<script type = 'text/javascript' > window.open('CartaPDFAceptado.aspx?Id=" + Request.Params["Id"].ToString() + "', '_blank', 'PDF MetLife', 'width=400,height=500')</script >;");
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