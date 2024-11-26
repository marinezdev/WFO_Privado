using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace wfip.promotoria
{
    public partial class Displaypdf : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["credencial"] == null)
            {
                Response.Redirect("~/Default.aspx");
            }
            else 
            {
                Propiedades.UrlCifrardo urlCifrardo = ConsultaParametros();

                if (urlCifrardo.Result)
                {
                    MuestraPDF(Convert.ToInt32(urlCifrardo.IdTramite));
                }
                else
                {
                    MuestraPDF(Convert.ToInt32(0));
                }
            }
        }

        protected void MuestraPDF(int IdTramite)
        {
            wfiplib.admExpediente admExpediente = new wfiplib.admExpediente();
            wfiplib.expediente ArchivoFusion = admExpediente.daFusion(Convert.ToInt32(IdTramite));

            string strDoctoWeb = "";
            bool busqueda = false;
            strDoctoWeb = "..\\DocsTemplate\\DocumentoError.pdf";

            if (ArchivoFusion.IdTramite > 0)
            {
                if (!string.IsNullOrEmpty(ArchivoFusion.NmArchivo))
                {
                    strDoctoWeb = ArchivoFusion.CarpetaArchivada + ArchivoFusion.NmArchivo;

                    if (File.Exists(strDoctoWeb))
                    {
                        busqueda = true;
                    }
                    else
                    {
                        // AGREGAR ARCHIVO NO ENCONTRADO
                        strDoctoWeb = "..\\DocsTemplate\\DocumentoError.pdf";
                    }
                }
                else
                {
                    // AGREGAR ARCHIVO NO ENCONTRADO
                    strDoctoWeb = "..\\DocsTemplate\\DocumentoError.pdf";
                }
                
            }

            string FilePath = "";

            if (busqueda)
            {
                FilePath = strDoctoWeb;
            }
            else
            {
                FilePath = Server.MapPath(strDoctoWeb);
            }


            WebClient User = new WebClient();
            Byte[] FileBuffer = User.DownloadData(FilePath);
            if (FileBuffer != null)
            {
                Response.ContentType = "application/pdf";
                Response.AddHeader("content-length", FileBuffer.Length.ToString());
                Response.BinaryWrite(FileBuffer);
            }
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

                    string BusqeudaIdTramite = stringBetween(s + ".", "IdTramite=", ".");
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