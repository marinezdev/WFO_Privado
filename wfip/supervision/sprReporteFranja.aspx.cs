using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DevExpress.XtraCharts;

namespace wfip.supervision
{
    // Integración GSL
    public partial class sprReporteFranja : System.Web.UI.Page
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
                hfCambio.Value = "0";
                DateTime Fecha = DateTime.Now;
                Muestradatos(Fecha);
                AcumuladoMensual(Fecha);

            }
        }
        private void AcumuladoMensual(DateTime Fecha)
        {
            // wfiplib.credencial oCredencial = (wfiplib.credencial)Session["credencial"];
            int IdFlujo = manejo_sesion.Credencial.IdFlujo;
            int totalMes = (new wfiplib.Reportes()).TotalMesFranja(Fecha, IdFlujo);
            lblAcumulado.Text = totalMes.ToString();
        }
        private void Muestradatos(DateTime Fecha) 
        {

            dvchtFranja.Series.Clear();
            //wfiplib.credencial oCredencial = (wfiplib.credencial)Session["credencial"];
            int IdFlujo = manejo_sesion.Credencial.IdFlujo;
            DataTable dt = (new wfiplib.Reportes()).Franja(Fecha, IdFlujo);

            dvgdFranja.DataSource = dt;
            dvgdFranja.DataBind();

            Series sri = new Series("INGRESADOS", ViewType.Line);
            Series srt = new Series("TOCADOS", ViewType.Line);
            Series sre = new Series("EJECUTADOS", ViewType.Line);

            foreach (DataRow registro in dt.Rows)
            {
                sri.Points.Add(new SeriesPoint(registro["Franja"].ToString(), registro["ingresados"].ToString()));
                srt.Points.Add(new SeriesPoint(registro["Franja"].ToString(), registro["tocados"].ToString()));
                sre.Points.Add(new SeriesPoint(registro["Franja"].ToString(), registro["ejecutados"].ToString()));
            }

            ((LineSeriesView)sri.View).LineMarkerOptions.Kind = MarkerKind.Diamond;
            ((LineSeriesView)sri.View).Color = System.Drawing.Color.DarkGreen;
            ((LineSeriesView)srt.View).LineMarkerOptions.Kind = MarkerKind.Circle;
            ((LineSeriesView)srt.View).Color = System.Drawing.Color.DarkBlue;
            ((LineSeriesView)sre.View).LineMarkerOptions.Kind = MarkerKind.Square;
            ((LineSeriesView)sre.View).Color = System.Drawing.Color.DarkGray;

            sri.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;
            srt.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;
            sre.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;

            dvchtFranja.Series.Add(sri);
            dvchtFranja.Series.Add(srt);
            dvchtFranja.Series.Add(sre);
        }
        protected void lnkExportar_Click(object sender, EventArgs e)
        {
            grdExport.WriteXlsToResponse();
        }

        protected void ASPxCalendar1_SelectionChanged(object sender, EventArgs e)
        {
            var Fecha = ASPxCalendar1.SelectedDate;

            Muestradatos(Fecha);
            AcumuladoMensual(Fecha);

        }
    }
}