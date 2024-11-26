using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.DataVisualization.Charting;
using System.Web.UI.WebControls;

namespace wfip.promotoria
{
    public partial class inicioPromotoria : System.Web.UI.Page
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
                pintaTituloPormotoria();
                pintaDatos();
                Session.Contents.Remove("nota");
            }
        }
        private void pintaTituloPormotoria()
        {
            if (manejo_sesion.Credencial.Modulo == wfiplib.E_Modulo.Promotoria)
            {
                if (manejo_sesion.Credencial.IdPromotoria > 0)
                {
                    ltTituloPromotoria.Text = (new wfiplib.admCatPromotoria(ConfigurationManager.ConnectionStrings["conecta_bd"].ConnectionString)).carga(manejo_sesion.Credencial.IdPromotoria).Nombre;
                }
            }
        }

        private void pintaDatos()
        {
            if (manejo_sesion.Credencial.Modulo == wfiplib.E_Modulo.Promotoria)
            {
                if (manejo_sesion.Credencial.IdPromotoria > 0)
                {
                    graficaEstilo();
                    DataTable datos = (new wfiplib.admIndicadores()).IndicadoresGeneralesPromotoria(manejo_sesion.Credencial.IdPromotoria);
                    llenaGraficaUno(datos);
                    datos.Dispose();
                }
            }
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
                serieTotales.ChartType = SeriesChartType.Column;
                serieTotales.IsValueShownAsLabel = true;
                serieTotales.XValueMember = "Estado";
                serieTotales.YValueMembers = "Totales";
                serieTotales.CustomProperties = "ShowMarkerLines=true";
                //serieTotales.PostBackValue = "item";
                serieTotales.PostBackValue = "#VALX";
                serieTotales.IsValueShownAsLabel = true;

                grfGrupoUno.DataBind();

                serieTotales.Points[1].Color = Color.Blue;          // Proceso
                serieTotales.Points[3].Color = Color.Green;         // Ejecucion
                serieTotales.Points[4].Color = Color.Red;           // Rechazo
                serieTotales.Points[2].Color = Color.Yellow;        // Hold
                serieTotales.Points[5].Color = Color.Orange;        // Suspendido
                serieTotales.Points[6].Color = Color.DarkOrange;    // Suspendido
            }
        }

        protected void Timer1_Tick(object sender, EventArgs e)
        {
            pintaDatos();
        }

        protected void grfGrupoUno_Click(object sender, ImageMapEventArgs e)
        {
            string estado = Convert.ToString(e.PostBackValue);
            ltTemp.Text = estado;
            Response.Redirect("tramitesPorEstado.aspx?estado=" + estado);
        }

    }
}