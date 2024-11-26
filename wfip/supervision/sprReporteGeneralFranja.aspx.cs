using DevExpress.Internal.WinApi.Windows.UI.Notifications;
using DevExpress.XtraCharts;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace wfip.supervision
{
    public partial class sprReporteGeneralFranja : System.Web.UI.Page
    {
        wfiplib.Concentrado manejo_sesion = new wfiplib.Concentrado();

        protected void Page_Init(object sender, EventArgs e)
        {
            if (Session["credencial"] == null)
                Response.Redirect("~/Default.aspx");
            manejo_sesion = (wfiplib.Concentrado)Session["credencial"];
            CalFranja.EditFormatString = "dd/MM/yyyy";
            CalFranja.Date = DateTime.Now;
            int annio = DateTime.Now.Year;
            int mes = DateTime.Now.Month;
            AnnioTS.Items.Add(new ListItem((DateTime.Now.Year).ToString(), (DateTime.Now.Year).ToString()));
            AnnioTS.Items.Add(new ListItem((DateTime.Now.Year-1).ToString(), (DateTime.Now.Year-1).ToString()));
            AnnioTS.Items.Add(new ListItem((DateTime.Now.Year-2).ToString(), (DateTime.Now.Year-2).ToString()));
            MesTS.Items.FindByValue(mes.ToString()).Selected = true;


            DataTable dt = (new wfiplib.admUsuarioFlujos()).usuariosFlujo_Seleccionar_IdUsuario(manejo_sesion.Credencial.Id);
            ListaFlujo.DataSource = dt;
            ListaFlujo.TextField = "Nombre";
            ListaFlujo.ValueField = "Id";
            ListaFlujo.DataBind();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
           hfCambio.Value = "0";
            //MuestradatosFranja(CalFranja.Date);
            //AcumuladoMensualFranja(CalFranja.Date);
            //int annio = int.Parse(AnnioTS.SelectedValue);
            //int mes = int.Parse(MesTS.SelectedValue);
            //LlenarDatosTS(mes, annio);
            //DateTime Fecha = DateTime.Now;

            try
            {
                int IdFlujo = Convert.ToInt32(ListaFlujo.SelectedItem.Value.ToString());

                MuestradatosFranja(CalFranja.Date, IdFlujo);
                AcumuladoMensualFranja(CalFranja.Date, IdFlujo);
                int annio = int.Parse(AnnioTS.SelectedValue);
                int mes = int.Parse(MesTS.SelectedValue);
                LlenarDatosTS(mes, annio, IdFlujo);

                MuestradatosTendencia(IdFlujo);
            }
            catch (Exception ex)
            {

            }
        }

        #region Franja

        protected void BtnCosnultarFranja_Click(object sender, EventArgs e)
        {
            int IdFlujo = Convert.ToInt32(ListaFlujo.SelectedItem.Value.ToString());
            MuestradatosFranja(CalFranja.Date, IdFlujo);
            AcumuladoMensualFranja(CalFranja.Date, IdFlujo);
            int annio = int.Parse(AnnioTS.SelectedValue);
            int mes = int.Parse(MesTS.SelectedValue);
            LlenarDatosTS(mes, annio, IdFlujo);
   
            MuestradatosTendencia(IdFlujo);
        }

        private void MuestradatosFranja(DateTime Fecha, int IdFlujo)
        {
            dvchtFranja.Series.Clear();
            //wfiplib.credencial oCredencial = (wfiplib.credencial)Session["credencial"];
            //int IdFlujo = manejo_sesion.Credencial.IdFlujo;
            DataTable dt = (new wfiplib.Reportes()).Franja(Fecha, IdFlujo);

            dvgdFranja.DataSource = dt;
            dvgdFranja.DataBind();

            Series sri = new Series("INGRESADOS", ViewType.Line);
            Series srt = new Series("PENDIENTES DE ATENCIÓN", ViewType.Line);
            Series sre = new Series("PROCESADOS", ViewType.Line);

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

        private void AcumuladoMensualFranja(DateTime Fecha, int IdFlujo)
        {
            // wfiplib.credencial oCredencial = (wfiplib.credencial)Session["credencial"];
            //int IdFlujo = manejo_sesion.Credencial.IdFlujo;
            int totalMes = (new wfiplib.Reportes()).TotalMesFranja(Fecha, IdFlujo);
            lblAcumulado.Text = totalMes.ToString();
        }

        protected void lnkExportarFranja_Click(object sender, EventArgs e)
        {
            grdExportFranja.WriteXlsToResponse();
        }
        #endregion

        #region Tramites por semana

        protected void BtnCosnultarSemana_Click(object sender, EventArgs e)
        {
            int IdFlujo = Convert.ToInt32(ListaFlujo.SelectedItem.Value.ToString());
            int annio = int.Parse(AnnioTS.SelectedValue);
            int mes = int.Parse(MesTS.SelectedValue);
            LlenarDatosTS(mes, annio, IdFlujo);
        }

        private void LlenarDatosTS(int mes, int annio, int IdFlujo)
        {
            DataTable dt = new DataTable();
            //wfiplib.credencial oCredencial = (wfiplib.credencial)Session["credencial"];
            //int IdFlujo = manejo_sesion.Credencial.IdFlujo;
            dt = new wfiplib.Reportes().TramiteSemana(mes, annio, IdFlujo);
            dvgdTramiteSemana.DataSource = dt;
            dvgdTramiteSemana.DataBind();
            dvgdTramiteSemana.Caption = "TRÁMITES POR SEMANA";


            DataTable dtTotalSemana = new DataTable();
            DataTable dtTotales = new DataTable();
            dtTotales = new wfiplib.Reportes().TotalTramiteSemana(mes, annio, IdFlujo);
            dtTotalSemana.Columns.Add("SEMANA");
            dtTotalSemana.Columns.Add("TRAMITES", typeof(Int32));

            // MUESTRA DATOS EN LA GRAFICA
            foreach (DataRow tramite in dtTotales.Rows)
            {
                dtTotalSemana.Rows.Add(new object[] { tramite["SEMANA"], tramite["TRAMITES"] });
            }
            dxChtTotalesTS.DataSource = dtTotalSemana;
            dxChtTotalesTS.SeriesDataMember = "SEMANA";
            dxChtTotalesTS.SeriesTemplate.SetDataMembers("SEMANA", "TRAMITES");
            dxChtTotalesTS.SeriesTemplate.ArgumentDataMember = "SEMANA";
            dxChtTotalesTS.DataBind();
        }
        protected void lnkExportarTS_Click(object sender, EventArgs e)
        {

            dvgdTramiteSemana.ExportXlsToResponse();
            // grdExport.WriteXlsToResponse();
        }
        #endregion
        #region Reporte de tendencia por año
        private void MuestradatosTendencia(int IdFlujo)
        {
            DateTime fecha = DateTime.Now;
            int annioActual = fecha.Year;
            int annioAnterior = fecha.Year - 1;
            //wfiplib.credencial oCredencial = (wfiplib.credencial)Session["credencial"];
            //int IdFlujo = manejo_sesion.Credencial.IdFlujo;
            DataTable dt = (new wfiplib.Reportes()).Tendencia(annioActual, IdFlujo);
            DataTable dt2 = (new wfiplib.Reportes()).Tendencia(annioAnterior, IdFlujo);
            dt.Merge(dt2);
            Series srI = new Series(annioActual.ToString()+"-INGRESADOS", ViewType.Line);
            Series srEA = new Series(annioActual.ToString() + "-EN ATENCIÓN", ViewType.Line);
            srI.Points.Add(new SeriesPoint("Enero", dt.Rows[0]["Enero"]));
            srEA.Points.Add(new SeriesPoint("Enero", dt.Rows[1]["Enero"]));
            srI.Points.Add(new SeriesPoint("Febrero", dt.Rows[0]["Febrero"]));
            srEA.Points.Add(new SeriesPoint("Febrero", dt.Rows[1]["Febrero"]));
            srI.Points.Add(new SeriesPoint("Marzo", dt.Rows[0]["Marzo"]));
            srEA.Points.Add(new SeriesPoint("Marzo", dt.Rows[1]["Marzo"]));
            srI.Points.Add(new SeriesPoint("Abril", dt.Rows[0]["Abril"]));
            srEA.Points.Add(new SeriesPoint("Abril", dt.Rows[1]["Abril"]));
            srI.Points.Add(new SeriesPoint("Mayo", dt.Rows[0]["Mayo"]));
            srEA.Points.Add(new SeriesPoint("Mayo", dt.Rows[1]["Mayo"]));
            srI.Points.Add(new SeriesPoint("Junio", dt.Rows[0]["Junio"]));
            srEA.Points.Add(new SeriesPoint("Junio", dt.Rows[1]["Junio"]));
            srI.Points.Add(new SeriesPoint("Julio", dt.Rows[0]["Julio"]));
            srEA.Points.Add(new SeriesPoint("Julio", dt.Rows[1]["Julio"]));
            srI.Points.Add(new SeriesPoint("Agosto", dt.Rows[0]["Agosto"]));
            srEA.Points.Add(new SeriesPoint("Agosto", dt.Rows[1]["Agosto"]));
            srI.Points.Add(new SeriesPoint("Septiembre", dt.Rows[0]["Septiembre"]));
            srEA.Points.Add(new SeriesPoint("Septiembre", dt.Rows[1]["Septiembre"]));
            srI.Points.Add(new SeriesPoint("Octubre", dt.Rows[0]["Octubre"]));
            srEA.Points.Add(new SeriesPoint("Octubre", dt.Rows[1]["Octubre"]));
            srI.Points.Add(new SeriesPoint("Noviembre", dt.Rows[0]["Noviembre"]));
            srEA.Points.Add(new SeriesPoint("Noviembre", dt.Rows[1]["Noviembre"]));
            srI.Points.Add(new SeriesPoint("Diciembre", dt.Rows[0]["Diciembre"]));
            srEA.Points.Add(new SeriesPoint("Diciembre", dt.Rows[1]["Diciembre"]));

            ((LineSeriesView)srI.View).LineMarkerOptions.Kind = MarkerKind.Diamond;
            ((LineSeriesView)srI.View).Color = System.Drawing.Color.DarkBlue;
            srI.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;
            dvchtTendencia.Series.Add(srI);

            ((LineSeriesView)srEA.View).LineMarkerOptions.Kind = MarkerKind.Square;
            ((LineSeriesView)srEA.View).Color = System.Drawing.Color.DarkCyan;
            srEA.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;
            dvchtTendencia.Series.Add(srEA);
         
            Series srIAA = new Series(annioAnterior.ToString()+"-INGRESADOS", ViewType.Line);
            Series srEAAA = new Series(annioAnterior.ToString()+"-EN ATENCIÓN", ViewType.Line);

            srIAA.Points.Add(new SeriesPoint("Enero", dt.Rows[2]["Enero"]));
            srEAAA.Points.Add(new SeriesPoint("Enero", dt.Rows[3]["Enero"]));
            srIAA.Points.Add(new SeriesPoint("Febrero", dt.Rows[2]["Febrero"]));
            srEAAA.Points.Add(new SeriesPoint("Febrero", dt.Rows[3]["Febrero"]));
            srIAA.Points.Add(new SeriesPoint("Marzo", dt.Rows[2]["Marzo"]));
            srEAAA.Points.Add(new SeriesPoint("Marzo", dt.Rows[3]["Marzo"]));
            srIAA.Points.Add(new SeriesPoint("Abril", dt.Rows[2]["Abril"]));
            srEAAA.Points.Add(new SeriesPoint("Abril", dt.Rows[3]["Abril"]));
            srIAA.Points.Add(new SeriesPoint("Mayo", dt.Rows[2]["Mayo"]));
            srEAAA.Points.Add(new SeriesPoint("Mayo", dt.Rows[3]["Mayo"]));
            srIAA.Points.Add(new SeriesPoint("Junio", dt.Rows[2]["Junio"]));
            srEAAA.Points.Add(new SeriesPoint("Junio", dt.Rows[3]["Junio"]));
            srIAA.Points.Add(new SeriesPoint("Julio", dt.Rows[2]["Julio"]));
            srEAAA.Points.Add(new SeriesPoint("Julio", dt.Rows[3]["Julio"]));
            srIAA.Points.Add(new SeriesPoint("Agosto", dt.Rows[2]["Agosto"]));
            srEAAA.Points.Add(new SeriesPoint("Agosto", dt.Rows[3]["Agosto"]));
            srIAA.Points.Add(new SeriesPoint("Septiembre", dt.Rows[2]["Septiembre"]));
            srEAAA.Points.Add(new SeriesPoint("Septiembre", dt.Rows[3]["Septiembre"]));
            srIAA.Points.Add(new SeriesPoint("Octubre", dt.Rows[2]["Octubre"]));
            srEAAA.Points.Add(new SeriesPoint("Octubre", dt.Rows[3]["Octubre"]));
            srIAA.Points.Add(new SeriesPoint("Noviembre", dt.Rows[2]["Noviembre"]));
            srEAAA.Points.Add(new SeriesPoint("Noviembre", dt.Rows[3]["Noviembre"]));
            srIAA.Points.Add(new SeriesPoint("Diciembre", dt.Rows[2]["Diciembre"]));
            srEAAA.Points.Add(new SeriesPoint("Diciembre", dt.Rows[3]["Diciembre"]));


            ((LineSeriesView)srIAA.View).LineMarkerOptions.Kind = MarkerKind.Diamond;
            ((LineSeriesView)srIAA.View).Color = System.Drawing.Color.DarkRed;
            srIAA.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;
            dvchtTendencia.Series.Add(srIAA);

            ((LineSeriesView)srEAAA.View).LineMarkerOptions.Kind = MarkerKind.Square;
            ((LineSeriesView)srEAAA.View).Color = System.Drawing.Color.DarkOrange;
            srIAA.LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;
            dvchtTendencia.Series.Add(srEAAA);

            dvgdTendencia.DataSource = dt;
            dvgdTendencia.DataBind();

        }
        protected void lnkExportarTendencia_Click(object sender, EventArgs e)
        {
            grdExportT.WriteXlsToResponse();
        }
        #endregion

       

       
    }
}