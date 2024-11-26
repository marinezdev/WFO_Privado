using System;
using System.Data;
using System.Web.UI.DataVisualization.Charting;

namespace wfip.supervision
{
    public partial class sprReporteTramitesAnuales : System.Web.UI.Page
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
            if (!IsPostBack)
            {
                CargaInicial();
            }
        }

        private const String CHART_AREA_MAIN = "chartAreaMain";
        private const String SERIES_BENCH_AMOUNT = "seriesBenchAmount";
        private const String SERIES_FOOD_INTAKE = "seriesFoodIntake";


        protected void CargaInicial()
        {
            wfiplib.admTramite tramite = new wfiplib.admTramite();

            dvgdEstatusTramite.DataSource = tramite.ReporteTramitesAnuales();
            dvgdEstatusTramite.DataBind();


            //Llenado del gráfico5
            DataTable ChartData = tramite.ReporteTramitesAnualesGrafico().Tables[0];

            string[] XPointMember = new string[ChartData.Rows.Count];
            int[] YPointMember = new int[ChartData.Rows.Count];

            for (int count = 0; count < ChartData.Rows.Count; count++)
            {
                //storing Values for X axis  
                XPointMember[count] = ChartData.Rows[count]["Mes"].ToString();
                //storing values for Y Axis  
                YPointMember[count] = Convert.ToInt32(ChartData.Rows[count]["Tramites"]);
            }

            int InicioOperacion = 2018;
            int NowYear = int.Parse(DateTime.Now.ToString("yyyy"));
            int contador = 1;

            if ((NowYear - 5) > InicioOperacion)
                InicioOperacion = NowYear - 5;

            for (int _Indice = InicioOperacion; _Indice <= NowYear; _Indice++)
            { 
                Chart1.Series.Add(_Indice.ToString());
                Chart1.Series[_Indice.ToString()].LegendText = _Indice.ToString();
                Chart1.Series[_Indice.ToString()].ChartArea = "ChartArea1";
                Chart1.Series[_Indice.ToString()].BorderWidth = 10;
                Chart1.Series[_Indice.ToString()].MarkerSize = 2;
                //Chart1.Series[_Indice.ToString()].ToolTip = _Indice.ToString();
                //Chart1.Series[_Indice.ToString()].IsValueShownAsLabel = true;


                switch (contador)
                {
                    case 1:
                        Chart1.Series[_Indice.ToString()].Color = System.Drawing.Color.Aquamarine;
                        break;

                    case 2:
                        Chart1.Series[_Indice.ToString()].Color = System.Drawing.Color.Yellow;
                        break;

                    case 3:
                        Chart1.Series[_Indice.ToString()].Color = System.Drawing.Color.Green;
                        break;

                    case 4:
                        Chart1.Series[_Indice.ToString()].Color = System.Drawing.Color.Blue;
                        break;

                    case 5:
                        Chart1.Series[_Indice.ToString()].Color = System.Drawing.Color.Red;
                        break;

                    default:
                        Chart1.Series[_Indice.ToString()].Color = System.Drawing.Color.Black;
                        break;

                }
                contador += 1;
            }

            //binding chart control  
            Chart1.Series[0].Points.DataBindXY(XPointMember, YPointMember);

            //Setting width of line  
            Chart1.Series[0].BorderWidth = 1;
            //setting Chart type   
            Chart1.Series[0].ChartType = SeriesChartType.Line;
            Chart1.Series[0].Points[0].AxisLabel = "Ene";
            Chart1.Series[0].Points[1].AxisLabel = "Feb";
            Chart1.Series[0].Points[2].AxisLabel = "Mar";
            Chart1.Series[0].Points[3].AxisLabel = "Abr";
            Chart1.Series[0].Points[4].AxisLabel = "May";
            Chart1.Series[0].Points[5].AxisLabel = "Jun";
            Chart1.Series[0].Points[6].AxisLabel = "Jul";
            Chart1.Series[0].Points[7].AxisLabel = "Ago";
            Chart1.Series[0].Points[8].AxisLabel = "Sep";
            Chart1.Series[0].Points[9].AxisLabel = "Oct";
            Chart1.Series[0].Points[10].AxisLabel = "Nov";
            Chart1.Series[0].Points[11].AxisLabel = "Dic";


            int _Contador = 0;
            DataSet dsReporte = tramite.ReporteTramitesAnualesGrafico();

            foreach (DataTable dtGrafica in dsReporte.Tables)
            {
                string[] XXPoint = new string[dtGrafica.Rows.Count];
                int[] YYPoint = new int[dtGrafica.Rows.Count];

                for (int count = 0; count < dtGrafica.Rows.Count; count++)
                {
                    XXPoint[count] = dtGrafica.Rows[count]["Mes"].ToString();
                    YYPoint[count] = Convert.ToInt32(dtGrafica.Rows[count]["Tramites"]);
                }

                Chart1.Series[_Contador].Points.DataBindXY(XXPoint, YYPoint);
                Chart1.Series[_Contador].BorderWidth = 1;
                Chart1.Series[_Contador].ChartType = SeriesChartType.Line;
                _Contador += 1;
            }
            Chart1.Legends[0].Enabled = true;
        }
    }
}