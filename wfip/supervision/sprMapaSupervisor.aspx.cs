using DevExpress.Export;
using DevExpress.Web;
using DevExpress.XtraPrinting;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace wfip.supervision
{
    public partial class sprMapaSupervisor : System.Web.UI.Page
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
                string modo = GridViewDetailExportMode.All.ToString();
                //string Mesa = Request.Params["Id"];
                string Mesa = urlCifrardo.Mesa;
                lblMesa.Text = "MESA: " + Mesa;
                Muestradatos(Mesa);
                dvgdTramites.SettingsDetail.ExportMode = (GridViewDetailExportMode)Enum.Parse(typeof(GridViewDetailExportMode), modo);
            }
                

        }
        private void Muestradatos(string Mesa)
        {
            // wfiplib.credencial oCredencial = (wfiplib.credencial)Session["credencial"];
            int IdFlujo = manejo_sesion.Credencial.IdFlujo;
            DataTable dt = (new wfiplib.Reportes()).MapaSupervisor(IdFlujo,Mesa);
            dvgdTramites.DataSource = dt;
            dvgdTramites.DataBind();
            //dvgdTramites.DetailRows.ExpandRow(0);

        }
        protected void dvgdDetalleTramite_Init(object sender, EventArgs e)
        {
            int IdFlujo = manejo_sesion.Credencial.IdFlujo;
            ASPxGridView gridDetalle = (ASPxGridView)sender;
            int idMesa = int.Parse(gridDetalle.GetMasterRowFieldValues("idMesa").ToString());
            DataTable dtD = (new wfiplib.Reportes()).MapaSupervisorDetalle(idMesa,IdFlujo);
            gridDetalle.DataSource = dtD;
        }
        protected void lnkExportar_Click(object sender, EventArgs e)
        {
            //grdExport.WriteXlsToResponse();
            dvgdTramites.ExportXlsxToResponse("MapaSupervisor.xlsx", new XlsxExportOptionsEx() { ExportType = ExportType.WYSIWYG });
        }

        private Propiedades.UrlCifrardo ConsultaParametros()
        {
            Propiedades.UrlCifrardo urlCifrardo = new Propiedades.UrlCifrardo();
            try
            {
                string parametros = (new Application.Operacion.UrlCifrardo()).Decrypt(Request.QueryString["data"].ToString());
                string Mesa = "";

                String[] spearator = { "," };
                String[] strlist = parametros.Split(spearator, StringSplitOptions.RemoveEmptyEntries);

                foreach (String s in strlist)
                {

                    string BusqeudaMesa = stringBetween(s + ".", "Id=", ".");
                    if (BusqeudaMesa.Length > 0)
                    {
                        Mesa = BusqeudaMesa;
                    }
                }

                if (Mesa.Length > 0)
                {
                    urlCifrardo.Mesa = Mesa.ToString();
                    urlCifrardo.Result = true;
                }
                else
                {
                    urlCifrardo.IdTramite = "";
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

    }
}