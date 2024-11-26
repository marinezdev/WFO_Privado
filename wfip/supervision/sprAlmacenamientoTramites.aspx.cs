using System;
using System.Data;
using System.Web.UI.DataVisualization.Charting;
using System.Web.UI.WebControls;

namespace wfip.supervision
{
    public partial class sprAlmacenamientoTramites : System.Web.UI.Page
    {
        wfiplib.admTramite tramite = new wfiplib.admTramite();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request["a"] != null)
                CargarDetalle(Request["a"]);
            if (!IsPostBack)
            {
                CargaGrafico();
                Muestradatos();
            }
        }

        protected void CargarDetalle(string opcion)
        {
            rptTramitesPromedio.DataSource = tramite.ReporteAlmacenamientoTramitesDetalle(opcion);
            rptTramitesPromedio.DataBind();
            //wfiplib.LlenarControles.LlenarGridView(ref GVDetalle, tramite.ReporteAlmacenamientoTramitesDetalle(opcion));
        }

        protected void rptTramitesEspera_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.CommandName))
            {
                Response.Redirect(EncripParametros("Id=" + e.CommandArgument.ToString(), "OpConsultaTramite.aspx").URL, true);
                //Response.Redirect("OpConsultaTramite.aspx?Id=" + e.CommandArgument.ToString());
            }
        }

        private void Muestradatos()
        {
            if (Request["a"] != null)
            {
                rptTramitesPromedio.DataSource = tramite.ReporteAlmacenamientoTramitesDetalle(Request["a"]); ;
                rptTramitesPromedio.DataBind();
            }
        }

        protected void CargaGrafico()
        {
            wfiplib.LlenarControles.LlenarGridView(ref GVPromedio, tramite.ReporteAlmacenamientoTramites());
            DataTable ChartData = tramite.ReporteAlmacenamientoTramitesGrafico();

            string[] XPointMember = new string[ChartData.Rows.Count];
            int[] YPointMember = new int[ChartData.Rows.Count];

            for (int count = 0; count<ChartData.Rows.Count; count++)
            {
                //storing Values for X axis  
                XPointMember[count] = ChartData.Rows[count][0].ToString();
                //storing values for Y Axis  
                YPointMember[count] = Convert.ToInt32(ChartData.Rows[count][0]);
            }
            //binding chart control  
            Chart1.Series[0].Points.DataBindXY(XPointMember, YPointMember);

            //Setting width of line  
            Chart1.Series[0].BorderWidth = 1;
            //setting Chart type   
            Chart1.Series[0].ChartType = SeriesChartType.Line;
            Chart1.Series[0].Points[0].AxisLabel = "0 a 30";
            Chart1.Series[0].Points[1].AxisLabel = "31 a 60";
            Chart1.Series[0].Points[2].AxisLabel = "61 a 90";
            Chart1.Series[0].Points[3].AxisLabel = "91 a 120";
            Chart1.Series[0].Points[4].AxisLabel = "121 a 150";
            Chart1.Series[0].Points[5].AxisLabel = "151 a 180";
            Chart1.Series[0].Points[6].AxisLabel = "181 a 210";
            Chart1.Series[0].Points[7].AxisLabel = "211 a 230";
            Chart1.Series[0].Points[8].AxisLabel = "231 a 260";
            Chart1.Series[0].Points[9].AxisLabel = "261 a 290";
            Chart1.Series[0].Points[10].AxisLabel = "291 a 320";
            Chart1.Series[0].Points[11].AxisLabel = "321 a 350";
            Chart1.Series[0].Points[12].AxisLabel = "más de 351";

            //DataTable ChartData2 = tramite.ReporteTramitesAnualesGrafico().Tables[1];

            //string[] XXPointMember = new string[ChartData2.Rows.Count];
            //int[] YYPointMember = new int[ChartData2.Rows.Count];

            //for (int count = 0; count < ChartData2.Rows.Count; count++)
            //{
            //    //storing Values for X axis  
            //    XXPointMember[count] = ChartData2.Rows[count]["Mes"].ToString();
            //    //storing values for Y Axis  
            //    YYPointMember[count] = Convert.ToInt32(ChartData2.Rows[count]["Tramites"]);
            //}
            //binding chart control  
            //Chart1.Series[1].Points.DataBindXY(XXPointMember, YYPointMember);

            //Setting width of line  
            //Chart1.Series[1].BorderWidth = 1;
            //setting Chart type   
            //Chart1.Series[1].ChartType = SeriesChartType.Line;

            Chart1.Legends[0].Enabled = true;
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