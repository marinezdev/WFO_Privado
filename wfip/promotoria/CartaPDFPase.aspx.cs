using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace wfip.promotoria
{
    public partial class CartaPDFPase : System.Web.UI.Page
    {
        wfiplib.Concentrado manejo_sesion = new wfiplib.Concentrado();

        protected void Page_Init(object sender, EventArgs e)
        {
            if (Session["credencial"] == null)
                Response.Redirect("~/Default.aspx");
            manejo_sesion = (wfiplib.Concentrado)Session["credencial"];
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Propiedades.UrlCifrardo urlCifrardo = ConsultaParametros();

            if (urlCifrardo.Result)
            {
                wfiplib.admCartas cartas = new wfiplib.admCartas();
                cartas.CartaPasePDF(Int32.Parse(urlCifrardo.IdTramite), Response, Int32.Parse(urlCifrardo.PDF), manejo_sesion.Credencial.Id);
            }

            //// VALIDA EL TIPO DE TRAMITE TANTO PUBLICO COMO PRIVADO
            //if (!String.IsNullOrEmpty(Request.QueryString["Id"]))
            //{
            //    String IdTramite = Request.Params["Id"].ToString();
            //    int Id = Int32.Parse(IdTramite);

            //    int CreaPDF = 0;

            //    if (!String.IsNullOrEmpty(Request.QueryString["PDF"]))
            //    {
            //        String PDF = Request.Params["PDF"].ToString();
            //        CreaPDF = Int32.Parse(PDF);
            //    }

            //    wfiplib.admCartas cartas = new wfiplib.admCartas();
            //    cartas.CartaPasePDF(Id, Response, CreaPDF, manejo_sesion.Credencial.Id);
            //}
        }

        private Propiedades.UrlCifrardo ConsultaParametros()
        {
            Propiedades.UrlCifrardo urlCifrardo = new Propiedades.UrlCifrardo();
            try
            {
                string parametros = (new Application.Operacion.UrlCifrardo()).Decrypt(Request.QueryString["data"].ToString());
                string IdTramite = "";
                string PDF = "";

                String[] spearator = { "," };
                String[] strlist = parametros.Split(spearator, StringSplitOptions.RemoveEmptyEntries);

                foreach (String s in strlist)
                {

                    string BusqeudaIdTramite = stringBetween(s + ".", "Id=", ".");
                    if (BusqeudaIdTramite.Length > 0)
                    {
                        IdTramite = BusqeudaIdTramite;
                    }

                    string BusqeudaPDF = stringBetween(s + ".", "PDF=", ".");
                    if (BusqeudaPDF.Length > 0)
                    {
                        PDF = BusqeudaPDF;
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

                if (PDF.Length > 0)
                {
                    urlCifrardo.PDF = PDF.ToString();
                    urlCifrardo.Result = true;
                }
                else
                {
                    urlCifrardo.PDF = "0";
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