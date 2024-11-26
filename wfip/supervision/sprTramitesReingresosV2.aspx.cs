using System;
using System.Data;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.DataVisualization.Charting;

namespace wfip.supervision
{
    public partial class sprTramitesReingresosV2 : System.Web.UI.Page
    {
        wfiplib.admTramite tramite = new wfiplib.admTramite();

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void CargaInicial(string fechainicial, string fechafinal, string tipotramite)
        {
            if (txtFechaInicio.Text == "" || txtFechaTermino.Text == "" || DDLTipoTramite.SelectedValue == "0")
                return;
            DataSet ds = new DataSet();
            ds = tramite.ReporteTramitesReingresos(fechainicial, fechafinal, tipotramite);
            wfiplib.LlenarControles.LlenarGridView(ref GV01, ds.Tables[0]);
            
            //Carga gráfico
            CargarGrafico(ds.Tables[1]);
            ViewState["Tabla2"] = ds.Tables[1];
            ViewState["Tabla3"] = ds.Tables[2];
            ViewState["Tabla4"] = ds.Tables[3];
        }

        protected void CargarGrafico(DataTable datatable)
        {
            //Carga gráfico
            Chart1.DataSource = datatable;

            Series serie01 = Chart1.Series.Add("Procesos Realizados");
            serie01.ChartArea = "GrupoUno";
            serie01.Font = new Font("Arial", 6.5F);
            serie01.ChartType = SeriesChartType.Bar;
            serie01.Color = Color.SteelBlue;
            serie01.IsValueShownAsLabel = true;
            serie01.XValueMember = "Nombre";
            serie01.YValueMembers = "ProcesosRealizados";
            serie01.CustomProperties = "ShowMarkerLines=true";
            serie01.PostBackValue = "#VALX";
            serie01.IsValueShownAsLabel = true;

            Series serie02 = Chart1.Series.Add("Total de Reingresos");
            serie02.ChartArea = "GrupoUno";
            serie02.Font = new Font("Arial", 6.5F);
            serie02.ChartType = SeriesChartType.Bar;
            serie02.Color = Color.Red;
            serie02.IsValueShownAsLabel = true;
            serie02.YValueMembers = "TotaldeReingresos";
            serie02.CustomProperties = "ShowMarkerLines=true";
            serie02.PostBackValue = "#VALX";
            serie02.IsValueShownAsLabel = true;

            Chart1.DataBind();
        }

        protected void Chart1_Click(object sender, ImageMapEventArgs e)
        {
            CargarGrafico((DataTable)ViewState["Tabla2"]);
            DataTable datatable1 = new DataTable();
            DataTable datatable2 = new DataTable();
            datatable1 = (DataTable)ViewState["Tabla3"];
            datatable2 = (DataTable)ViewState["Tabla4"];
            
            DataView dv = new DataView(datatable1);
            dv.RowFilter = "MESA='" + Convert.ToString(e.PostBackValue) + "'";
            string cadena = string.Empty;
            foreach (DataRowView dr in dv)
            {
                cadena += dr[0] + ",";
            }

            if (cadena.Length == 0)
                return;
            cadena = cadena.Substring(0, cadena.Length - 1);
            DataView dv2 = new DataView(datatable2);
            dv2.RowFilter = "Id IN("+ cadena +")";
            GV03.DataSource = dv2;
            GV03.DataBind();
        }

        protected void imbtnConsultar_Click(object sender, ImageClickEventArgs e)
        {
            ImageButton lbtn = (ImageButton)sender;
            GridViewRow row = (GridViewRow)lbtn.NamingContainer;
            //Response.Redirect("~/Supervision/OpConsultaTramite.aspx?Id=" + GV03.DataKeys[row.RowIndex].Value);
            Response.Redirect(EncripParametros("Id=" + GV03.DataKeys[row.RowIndex].Value, "~/Supervision/OpConsultaTramite.aspx").URL, true);
        }

        protected void BtnAceptar_Click(object sender, EventArgs e)
        {
            CargaInicial(txtFechaInicio.Text, txtFechaTermino.Text, DDLTipoTramite.SelectedValue);
        }

        private Propiedades.UrlCifrardo EncripParametros(string parametros, string Direccion)
        {
            Propiedades.UrlCifrardo urlCifrardo = new Propiedades.UrlCifrardo();

            string Encrypt = (new Application.Operacion.UrlCifrardo()).Encrypt(parametros);

            urlCifrardo.URL = Direccion + "?data=" + Encrypt;

            return urlCifrardo;
        }
    }
}