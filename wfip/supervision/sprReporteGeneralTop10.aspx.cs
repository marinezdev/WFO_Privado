using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DevExpress.XtraCharts;

namespace wfip
{
    public partial class sprReporteGeneralTop10 : System.Web.UI.Page
    {
        wfiplib.Concentrado manejo_sesion = new wfiplib.Concentrado();

        protected void Page_Init(object sender, EventArgs e)
        {
            if (Session["credencial"] == null)
                Response.Redirect("~/Default.aspx");
            manejo_sesion = (wfiplib.Concentrado)Session["credencial"];
            CalDesdeTE.EditFormatString = "dd/MM/yyyy";
            CalDesdeTE.Date = DateTime.Today;
            CalHastaTE.EditFormatString = "dd/MM/yyyy";
            CalHastaTE.Date = DateTime.Today;
            CalDesdeTS.EditFormatString = "dd/MM/yyyy";
            CalDesdeTS.Date = DateTime.Today;
            CalHastaTS.EditFormatString = "dd/MM/yyyy";
            CalHastaTS.Date = DateTime.Today;
        }

        protected void Page_Load(object sender, EventArgs e)
        {

            Muestradatos();
            MuestraPorcentajeSuspendidos();

        }

        private void Muestradatos()
        {
            // wfiplib.credencial oCredencial = (wfiplib.credencial)Session["credencial"];
            int IdFlujo = manejo_sesion.Credencial.IdFlujo;
            DataTable dt = (new wfiplib.Reportes()).Top10Recepcion(CalDesdeTE.Date, CalHastaTE.Date, IdFlujo);
            dvgdPromotorias.DataSource = dt;
            dvgdPromotorias.DataBind();

            //===========================

            dxChtTotales.Series.Clear();

            dxChtTotales.DataSource = dt;
            foreach (DataColumn Campo in dt.Columns)
            {
                if (!string.Equals("Promotoria", Campo.ColumnName) && !string.Equals("Nombre", Campo.ColumnName) && !string.Equals("Zona", Campo.ColumnName))
                {
                    Series SerieS = new Series(Campo.ColumnName, ViewType.Bar);
                    dxChtTotales.Series.Add(SerieS);
                    SerieS.ArgumentScaleType = ScaleType.Qualitative;
                    SerieS.ArgumentDataMember = "Promotoria";
                    SerieS.ValueScaleType = ScaleType.Numerical;
                    SerieS.ValueDataMembers.AddRange(new string[] { Campo.ColumnName });
                }

            }
            dxChtTotales.DataBind();
        }

        private void MuestraPorcentajeSuspendidos()
        {
            // wfiplib.credencial oCredencial = (wfiplib.credencial)Session["credencial"];
            int IdFlujo = manejo_sesion.Credencial.IdFlujo;
            DataTable dt = (new wfiplib.Reportes()).Top10Suspendidos(CalDesdeTS.Date, CalHastaTS.Date, IdFlujo);
            dgvsuspendidos.DataSource = dt;
            dgvsuspendidos.DataBind();

            //===========================

            dxChtSuspendidos.Series.Clear();

            dxChtSuspendidos.DataSource = dt;
            foreach (DataColumn Campo in dt.Columns)
            {
                if (!string.Equals("Promotoria", Campo.ColumnName) && !string.Equals("Nombre", Campo.ColumnName) && !string.Equals("Zona", Campo.ColumnName))
                {
                    Series SerieE = new Series(Campo.ColumnName, ViewType.Bar);
                    dxChtSuspendidos.Series.Add(SerieE);
                    SerieE.ArgumentScaleType = ScaleType.Qualitative;
                    SerieE.ArgumentDataMember = "Promotoria";
                    SerieE.ValueScaleType = ScaleType.Numerical;
                    SerieE.ValueDataMembers.AddRange(new string[] { Campo.ColumnName });
                }

            }
            dxChtSuspendidos.DataBind();



            //==========================





            /* dgvsuspendidos.DataBind();

             dxChtSuspendidos.DataSource = dt;
             dxChtSuspendidos.SeriesDataMember = "Promotoria";
             dxChtSuspendidos.SeriesTemplate.SetDataMembers("Promotoria", "NumTramitesSus");
             dxChtSuspendidos.DataBind();*/
        }

        protected void lnkExportar_Click(object sender, EventArgs e)
        {
            //grdExport.WriteXlsToResponse();
            dvgdPromotorias.ExportXlsToResponse();
        }

        protected void lnkExportSuspend_Click(object sender, EventArgs e)
        {
            //grdExportSuspendidos.WriteXlsToResponse();
            dgvsuspendidos.ExportXlsToResponse();
        }
    }
}
