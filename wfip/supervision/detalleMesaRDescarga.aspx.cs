using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Timers;
namespace wfip.supervision
{
    public partial class detalleMesaRDescarga : System.Web.UI.Page
    {
        wfiplib.Concentrado manejo_sesion = new wfiplib.Concentrado();

        protected void Page_Init(object sender, EventArgs e)
        {
            Propiedades.UrlCifrardo urlCifrardo = ConsultaParametros();

            if (urlCifrardo.Result)
            {
                DateTime CalDesde = Convert.ToDateTime(urlCifrardo.CalDesde);
                DateTime CalHasta = Convert.ToDateTime(urlCifrardo.CalHasta);
                int nu = Convert.ToInt32(urlCifrardo.Numero);

                LabelFechaInicio.Text = CalDesde.Date.ToShortDateString();
                LabelFechaFin.Text = CalHasta.Date.ToShortDateString();
                LabelNum.Text = nu.ToString();
            }
            //if (!String.IsNullOrEmpty(Request.QueryString["In"]) && !String.IsNullOrEmpty(Request.QueryString["Fn"]) && !String.IsNullOrEmpty(Request.QueryString["nu"]))
            //{
            //    DateTime CalDesde = Convert.ToDateTime(Request.QueryString["In"].ToString());
            //    DateTime CalHasta = Convert.ToDateTime(Request.QueryString["Fn"].ToString());
            //    int nu = Convert.ToInt32(Request.QueryString["nu"].ToString());

            //    LabelFechaInicio.Text = CalDesde.Date.ToShortDateString();
            //    LabelFechaFin.Text = CalHasta.Date.ToShortDateString();
            //    LabelNum.Text = nu.ToString();
            //}

            manejo_sesion = (wfiplib.Concentrado)Session["credencial"];
        }

        protected void Page_Load(object sender, EventArgs e)
        {
           
        }

        protected void BtnDescargar_Click(object sender, EventArgs e)
        {
            DateTime CalDesde = Convert.ToDateTime(LabelFechaInicio.Text.ToString());
            DateTime CalHasta = Convert.ToDateTime(LabelFechaFin.Text.ToString());
            int nu = Convert.ToInt32(LabelNum.Text.ToString());
            DataSet dt = new DataSet();

            if (nu == 1)
            {
                dt = (new wfiplib.NReportes()).Sabana(CalDesde.Date, CalHasta.Date, manejo_sesion.Credencial.Id);
            }else if(nu == 2)
            {
                dt = (new wfiplib.NReportes()).SabanaPrivada(CalDesde.Date, CalHasta.Date, manejo_sesion.Credencial.Id);
            }
            

            Informacion.Visible = false;
            InformacionFin.Visible = true;

            Descarga(dt);
        }

        protected void Descarga(DataSet dt)
        {
            var wb = new XLWorkbook();
            // Add all DataTables in the DataSet as a worksheets
            wb.Worksheets.Add(dt);
            
            // Prepare the response
            HttpResponse httpResponse = Response;
            httpResponse.Clear();
            httpResponse.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            httpResponse.AddHeader("content-disposition", "attachment;filename=\"Sabana.xlsx\"");

            
            // Flush the workbook to the Response.OutputStream
            using (MemoryStream memoryStream = new MemoryStream())
            {
                wb.SaveAs(memoryStream);
                memoryStream.WriteTo(httpResponse.OutputStream);
                memoryStream.Close();
            }

            httpResponse.End();
        }

        private Propiedades.UrlCifrardo ConsultaParametros()
        {
            Propiedades.UrlCifrardo urlCifrardo = new Propiedades.UrlCifrardo();
            urlCifrardo.Result = false;
            try
            {
                string parametros = (new Application.Operacion.UrlCifrardo()).Decrypt(Request.QueryString["data"].ToString());

                String[] spearator = { "," };
                String[] strlist = parametros.Split(spearator, StringSplitOptions.RemoveEmptyEntries);

                string BusquedaCalDesde = After(strlist[0], "=");
                string BusquedaCalHasta = After(strlist[1], "=");
                string BusquedaNumero = After(strlist[3], "=");

                if (BusquedaCalDesde.Length > 0)
                {
                    urlCifrardo.CalDesde = BusquedaCalDesde.ToString();
                    urlCifrardo.Result = true;
                }
                else
                {
                    urlCifrardo.CalDesde = "";
                }

                if (BusquedaCalHasta.Length > 0)
                {
                    urlCifrardo.CalHasta = BusquedaCalHasta.ToString();
                }
                else
                {
                    urlCifrardo.CalHasta = "";
                    urlCifrardo.Result = false;
                }

                if (BusquedaNumero.Length > 0)
                {
                    urlCifrardo.Numero = BusquedaNumero.ToString();
                }
                else
                {
                    urlCifrardo.Numero = "";
                    urlCifrardo.Result = false;
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

        public static string Before(string value, string a)
        {
            int posA = value.IndexOf(a);
            if (posA == -1)
            {
                return "";
            }
            return value.Substring(0, posA);
        }

        public static string After(string value, string a)
        {
            string result = "";
            int posA = value.LastIndexOf(a);
            if (posA == -1)
            {
                return result;
            }
            int adjustedPosA = posA + a.Length;
            if (adjustedPosA >= value.Length)
            {
                return result;
            }
            result = value.Substring(adjustedPosA);
            return result;
        }
    }
}