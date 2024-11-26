using System;
using System.Data;
using System.Drawing;
using System.Web.UI.DataVisualization.Charting;

namespace wfip.operacion
{
    public partial class esperaSupervisor : System.Web.UI.Page
    {
        wfiplib.Concentrado manejo_sesion = new wfiplib.Concentrado();

        protected void Page_Init(object sender, EventArgs e)
        {
            if (Session["credencial"] == null) Response.Redirect("~/Default.aspx");
            manejo_sesion = (wfiplib.Concentrado)Session["credencial"];
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                pintaDatos();
            }
        }

        private void pintaDatos()
        {
            graficaEstilo();
            DataTable datos = (new wfiplib.admIndicadores()).IndicadoresGeneralesMesas(1);
            llenaGraficaUno(datos);
            llenaGridDatos(datos);
            llenIndicadorGeneralEfectividad();
            datos.Dispose();
        }

        private void graficaEstilo()
        {
            // Disable axis labels auto fitting of text
            grfGrupoUno.ChartAreas["GrupoUno"].AxisX.IsLabelAutoFit = false;

            // Abilita que se pinten todos los nombres de los puntos
            grfGrupoUno.ChartAreas["GrupoUno"].AxisX.Interval = 1;
            grfGrupoUno.ChartAreas["GrupoUno"].AxisY.Interval = 50;

            // Set axis labels font
            grfGrupoUno.ChartAreas["GrupoUno"].AxisX.LabelStyle.Font = new Font("Arial", 6.5F);
            grfGrupoUno.ChartAreas["GrupoUno"].AxisY.LabelStyle.Font = new Font("Arial", 6.5F);

            // Set axis labels angle
            grfGrupoUno.ChartAreas["GrupoUno"].AxisX.LabelStyle.Angle = 35;

            // Disable offset labels style
            grfGrupoUno.ChartAreas["GrupoUno"].AxisX.LabelStyle.IsStaggered = false;

            // Enable X axis labels
            grfGrupoUno.ChartAreas["GrupoUno"].AxisX.LabelStyle.Enabled = true;

            // Enable AntiAliasing for either Text and Graphics or just Graphics
            grfGrupoUno.AntiAliasing = AntiAliasingStyles.All;
        }

        private void llenaGraficaUno(DataTable pDatos)
        {
            if (pDatos.Rows.Count > 0)
            {
                grfGrupoUno.DataSource = pDatos;

                // Add serie Totales
                Series serieTotales = grfGrupoUno.Series.Add("totales");
                serieTotales.ChartArea = "GrupoUno";
                serieTotales.Font = new Font("Arial", 6.5F);
                serieTotales.ChartType = SeriesChartType.Area;
                serieTotales.IsValueShownAsLabel = true;
                serieTotales.XValueMember = "Nombre";
                serieTotales.YValueMembers = "Totales";
                serieTotales.CustomProperties = "ShowMarkerLines=true";

                // Add serie alerta roja
                Series serieRoja = grfGrupoUno.Series.Add("procesados");
                serieRoja.ChartArea = "GrupoUno";
                serieRoja.ChartType = SeriesChartType.FastLine;
                serieRoja.Color = Color.Green;
                serieRoja.Font = new Font("Arial", 6.5F);
                serieRoja.XValueMember = "Nombre";
                serieRoja.YValueMembers = "Procesados";

                // Add serie alerta naranja
                Series serieNaranja = grfGrupoUno.Series.Add("pendientes");
                serieNaranja.ChartArea = "GrupoUno";
                serieNaranja.ChartType = SeriesChartType.FastLine;
                serieNaranja.Color = Color.Red;
                serieNaranja.Font = new Font("Arial", 6.5F);
                serieNaranja.XValueMember = "Nombre";
                serieNaranja.YValueMembers = "Pendientes";

                grfGrupoUno.DataBind();
            }
        }

        private void llenIndicadorGeneralEfectividad()
        {
            int valor = (new wfiplib.admIndicadores()).IndicadorGeneralEfectividadFlujo(1, 2, 8);
            deMedidorEfectrividad.Value = valor.ToString();
            if (valor <= 50) ASPxGaugeControl2.Value = "1";
            if (valor > 50 && valor <= 80) ASPxGaugeControl2.Value = "2";
            if (valor > 80) ASPxGaugeControl2.Value = "3";

        }

        private void llenaGridDatos(DataTable pDatos)
        {
            if (pDatos.Rows.Count > 0)
            {
                grdDatosTotales.DataSource = pDatos;
                grdDatosTotales.DataBind();
            }
        }

        protected void Timer1_Tick(object sender, EventArgs e) { pintaDatos(); }
    }
}