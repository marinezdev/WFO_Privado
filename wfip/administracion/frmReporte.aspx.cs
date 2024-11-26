using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.DataVisualization.Charting;
using System.Drawing;

namespace wfip.administracion
{
    public partial class frmReporte : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void chrReporte_Click(object sender, ImageMapEventArgs e)
        {
        }

        protected void BtnAceptar_Click(object sender, EventArgs e)
        {
            //Buscar los datos en la fecha indicada

            wfiplib.TiemposOperacion to = new wfiplib.TiemposOperacion();

            System.Data.DataTable dt = new System.Data.DataTable();

            dt = to.Seleccionar(txtInicio.Text, txtFinal.Text);

            chrReporte.ChartAreas["GrupoUno"].AxisX.IsLabelAutoFit = false;

            // Abilita que se pinten todos los nombres de los puntos
            chrReporte.ChartAreas["GrupoUno"].AxisX.Interval = 1;
            chrReporte.ChartAreas["GrupoUno"].AxisY.Interval = 50;

            // Set axis labels font
            chrReporte.ChartAreas["GrupoUno"].AxisX.LabelStyle.Font = new Font("Arial", 6.5F);
            chrReporte.ChartAreas["GrupoUno"].AxisY.LabelStyle.Font = new Font("Arial", 6.5F);

            // Set axis labels angle
            chrReporte.ChartAreas["GrupoUno"].AxisX.LabelStyle.Angle = 35;

            // Disable offset labels style
            chrReporte.ChartAreas["GrupoUno"].AxisX.LabelStyle.IsStaggered = false;

            // Enable X axis labels
            chrReporte.ChartAreas["GrupoUno"].AxisX.LabelStyle.Enabled = true;

            // Enable AntiAliasing for either Text and Graphics or just Graphics
            chrReporte.AntiAliasing = AntiAliasingStyles.All;

            chrReporte.DataSource = dt;
            chrReporte.DataBind();

            chrReporte.Visible = true;

            //-------------------------------------------------------------------------------------------------

            chrTiempos1.DataSource = dt;
            Series serieTiempos1 = chrTiempos1.Series.Add("totales02");
            serieTiempos1.ChartArea = "Dato1";
            serieTiempos1.Font = new Font("Arial", 6.5F);
            serieTiempos1.ChartType = SeriesChartType.Pie;
            serieTiempos1.IsValueShownAsLabel = true;
            serieTiempos1.XValueMember = "Mesa";
            serieTiempos1.YValueMembers = "Tiempotal";
            serieTiempos1.CustomProperties = "ShowMarkerLines=true";
            serieTiempos1.IsValueShownAsLabel = true;

            chrTiempos1.DataBind();
            chrTiempos1.Visible = true;

            //-------------------------------------------------------------------------------------------------

            chrTiempos2.DataSource = dt;
            Series serieTiempos2 = chrTiempos2.Series.Add("totales02");
            serieTiempos2.Font = new Font("Arial", 6.5F);
            serieTiempos2.ChartArea = "Dato1";
            serieTiempos2.ChartType = SeriesChartType.Pie;
            serieTiempos2.IsValueShownAsLabel = true;
            serieTiempos2.XValueMember = "Mesa";
            serieTiempos2.YValueMembers = "TiempoPromedio";
            serieTiempos2.CustomProperties = "ShowMarkerLines=true";
            serieTiempos2.IsValueShownAsLabel = true;

            chrTiempos2.DataBind();
            chrTiempos2.Visible = true;
        }
    }
}