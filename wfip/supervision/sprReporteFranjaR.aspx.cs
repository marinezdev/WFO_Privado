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
    public partial class sprReporteFranjaR : System.Web.UI.Page
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

            Series sre = new Series("sreFranjaZ", ViewType.Line);

            foreach (DataRow registro in dt.Rows)
            {
                sre.Points.Add(new SeriesPoint(registro["Franja"].ToString(), registro["Total"].ToString()));
            }

            ((LineSeriesView)sre.View).LineMarkerOptions.Kind = MarkerKind.Diamond;
            ((LineSeriesView)sre.View).Color = System.Drawing.Color.DarkGreen;
            sre.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;
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