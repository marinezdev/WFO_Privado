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
    public partial class sprReporteTendenciaR : System.Web.UI.Page
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

                DateTime Fecha = DateTime.Now;
                Muestradatos(Fecha);
            }
        }

        private void Muestradatos(DateTime Fecha)
        {
            DateTime fecha = DateTime.Now;
            int annioActual = fecha.Year;
            int annioAnterior = fecha.Year - 1;
            //wfiplib.credencial oCredencial = (wfiplib.credencial)Session["credencial"];
            int IdFlujo = manejo_sesion.Credencial.IdFlujo;
            DataTable dt = (new wfiplib.Reportes()).Tendencia(annioActual, IdFlujo);
            DataTable dt2 = (new wfiplib.Reportes()).Tendencia(annioAnterior, IdFlujo);
            DataTable Datos = new DataTable();
            foreach (DataColumn campo in dt.Columns)
            {
                Datos.Columns.Add(campo.ColumnName);
            }
            if (dt != null && dt.Rows.Count > 0)
            {
                Datos.Rows.Add(new object[] {
                   dt.Rows[0]["Annio"],
                   dt.Rows[0]["Enero"],
                   dt.Rows[0]["Febrero"],
                   dt.Rows[0]["Marzo"],
                   dt.Rows[0]["Abril"],
                   dt.Rows[0]["Mayo"],
                   dt.Rows[0]["Junio"],
                   dt.Rows[0]["Julio"],
                   dt.Rows[0]["Agosto"],
                   dt.Rows[0]["Septiembre"],
                   dt.Rows[0]["Octubre"],
                   dt.Rows[0]["Noviembre"],
                   dt.Rows[0]["Diciembre"]
                });
                Series srf = new Series(annioActual.ToString(), ViewType.Line);
                srf.Points.Add(new SeriesPoint("Enero", dt.Rows[0]["Enero"]));
                srf.Points.Add(new SeriesPoint("Febrero", dt.Rows[0]["Febrero"]));
                srf.Points.Add(new SeriesPoint("Marzo", dt.Rows[0]["Marzo"]));
                srf.Points.Add(new SeriesPoint("Abril", dt.Rows[0]["Abril"]));
                srf.Points.Add(new SeriesPoint("Mayo", dt.Rows[0]["Mayo"]));
                srf.Points.Add(new SeriesPoint("Junio", dt.Rows[0]["Junio"]));
                srf.Points.Add(new SeriesPoint("Julio", dt.Rows[0]["Julio"]));
                srf.Points.Add(new SeriesPoint("Agosto", dt.Rows[0]["Agosto"]));
                srf.Points.Add(new SeriesPoint("Septiembre", dt.Rows[0]["Septiembre"]));
                srf.Points.Add(new SeriesPoint("Octubre", dt.Rows[0]["Octubre"]));
                srf.Points.Add(new SeriesPoint("Noviembre", dt.Rows[0]["Noviembre"]));
                srf.Points.Add(new SeriesPoint("Diciembre", dt.Rows[0]["Diciembre"]));
                ((LineSeriesView)srf.View).LineMarkerOptions.Kind = MarkerKind.Diamond;
                ((LineSeriesView)srf.View).Color = System.Drawing.Color.DarkBlue;
                srf.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;
                dvchtTendencia.Series.Add(srf);
            }
            else
            {
                Datos.Rows.Add(new object[] { annioActual, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 });
                Series srf = new Series(annioAnterior.ToString(), ViewType.Line);
                srf.Points.Add(new SeriesPoint("Enero", 0));
                srf.Points.Add(new SeriesPoint("Febrero", 0));
                srf.Points.Add(new SeriesPoint("Marzo", 0));
                srf.Points.Add(new SeriesPoint("Abril", 0));
                srf.Points.Add(new SeriesPoint("Mayo", 0));
                srf.Points.Add(new SeriesPoint("Junio", 0));
                srf.Points.Add(new SeriesPoint("Julio", 0));
                srf.Points.Add(new SeriesPoint("Agosto", 0));
                srf.Points.Add(new SeriesPoint("Septiembre", 0));
                srf.Points.Add(new SeriesPoint("Octubre", 0));
                srf.Points.Add(new SeriesPoint("Noviembre", 0));
                srf.Points.Add(new SeriesPoint("Diciembre", 0));
                ((LineSeriesView)srf.View).LineMarkerOptions.Kind = MarkerKind.Diamond;
                ((LineSeriesView)srf.View).Color = System.Drawing.Color.DarkBlue;
                srf.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;
                dvchtTendencia.Series.Add(srf);

            }
            if (dt2 != null && dt2.Rows.Count > 0)
            {
                Datos.Rows.Add(new object[] {
                   dt2.Rows[0]["Annio"],
                   dt2.Rows[0]["Enero"],
                   dt2.Rows[0]["Febrero"],
                   dt2.Rows[0]["Marzo"],
                   dt2.Rows[0]["Abril"],
                   dt2.Rows[0]["Mayo"],
                   dt2.Rows[0]["Junio"],
                   dt2.Rows[0]["Julio"],
                   dt2.Rows[0]["Agosto"],
                   dt2.Rows[0]["Septiembre"],
                   dt2.Rows[0]["Octubre"],
                   dt2.Rows[0]["Noviembre"],
                   dt2.Rows[0]["Diciembre"]
                });
                Series sre = new Series(annioAnterior.ToString(), ViewType.Line);
                sre.Points.Add(new SeriesPoint("Enero", dt2.Rows[0]["Enero"]));
                sre.Points.Add(new SeriesPoint("Febrero", dt2.Rows[0]["Febrero"]));
                sre.Points.Add(new SeriesPoint("Marzo", dt2.Rows[0]["Marzo"]));
                sre.Points.Add(new SeriesPoint("Abril", dt2.Rows[0]["Abril"]));
                sre.Points.Add(new SeriesPoint("Mayo", dt2.Rows[0]["Mayo"]));
                sre.Points.Add(new SeriesPoint("Junio", dt2.Rows[0]["Junio"]));
                sre.Points.Add(new SeriesPoint("Julio", dt2.Rows[0]["Julio"]));
                sre.Points.Add(new SeriesPoint("Agosto", dt2.Rows[0]["Agosto"]));
                sre.Points.Add(new SeriesPoint("Septiembre", dt2.Rows[0]["Septiembre"]));
                sre.Points.Add(new SeriesPoint("Octubre", dt2.Rows[0]["Octubre"]));
                sre.Points.Add(new SeriesPoint("Noviembre", dt2.Rows[0]["Noviembre"]));
                sre.Points.Add(new SeriesPoint("Diciembre", dt2.Rows[0]["Diciembre"]));
                ((LineSeriesView)sre.View).LineMarkerOptions.Kind = MarkerKind.Diamond;
                ((LineSeriesView)sre.View).Color = System.Drawing.Color.DarkGreen;
                sre.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;
                dvchtTendencia.Series.Add(sre);
            }
            else
            {
                Datos.Rows.Add(new object[] { annioAnterior, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 });
                Series sre = new Series(annioAnterior.ToString(), ViewType.Line);
                sre.Points.Add(new SeriesPoint("Enero", 0));
                sre.Points.Add(new SeriesPoint("Febrero", 0));
                sre.Points.Add(new SeriesPoint("Marzo", 0));
                sre.Points.Add(new SeriesPoint("Abril", 0));
                sre.Points.Add(new SeriesPoint("Mayo", 0));
                sre.Points.Add(new SeriesPoint("Junio", 0));
                sre.Points.Add(new SeriesPoint("Julio", 0));
                sre.Points.Add(new SeriesPoint("Agosto", 0));
                sre.Points.Add(new SeriesPoint("Septiembre", 0));
                sre.Points.Add(new SeriesPoint("Octubre", 0));
                sre.Points.Add(new SeriesPoint("Noviembre", 0));
                sre.Points.Add(new SeriesPoint("Diciembre", 0));
                ((LineSeriesView)sre.View).LineMarkerOptions.Kind = MarkerKind.Diamond;
                ((LineSeriesView)sre.View).Color = System.Drawing.Color.DarkGreen;
                sre.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;
                dvchtTendencia.Series.Add(sre);
            }

            dvgdTendencia.Caption = "TRAMITES REALIZADOS";
            dvgdTendencia.DataSource = Datos;
            dvgdTendencia.DataBind();

        }
        protected void lnkExportar_Click(object sender, EventArgs e)
        {
            grdExport.WriteXlsToResponse();
        }
    }
}