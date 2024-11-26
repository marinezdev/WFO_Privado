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
    public partial class sprReporteGeneralTop10R : System.Web.UI.Page
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
            var valor = Ancho.Value;
            dxChtTotales.Width = new Unit(Convert.ToInt32(Ancho.Value));
            dxChtSuspendidos.Width = new Unit(Convert.ToInt32(Ancho.Value));
            Muestradatos();
            MuestraPorcentajeSuspendidos();
            UPTopSus.Update();

        }
        protected void dxChtTotales_CustomCallback(object sender, DevExpress.XtraCharts.Web.CustomCallbackEventArgs e)
        {
            dxChtTotales.Width = new Unit(Convert.ToInt32(Ancho.Value));
        }
        protected void dxChtSuspendidos_CustomCallback(object sender, DevExpress.XtraCharts.Web.CustomCallbackEventArgs e)
        {
            dxChtSuspendidos.Width = new Unit(Convert.ToInt32(Ancho.Value));
        }
        private void Muestradatos()
        {
            // wfiplib.credencial oCredencial = (wfiplib.credencial)Session["credencial"];
            int IdFlujo = manejo_sesion.Credencial.IdFlujo;
            DataTable dt = (new wfiplib.Reportes()).Top10Recepcion(CalDesdeTE.Date, CalHastaTE.Date, IdFlujo);
            dvgdPromotorias.DataSource = dt;
            dvgdPromotorias.DataBind();
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
            int IdFlujo = manejo_sesion.Credencial.IdFlujo;
            DataTable dt = (new wfiplib.Reportes()).Top10Suspendidos(CalDesdeTS.Date, CalHastaTS.Date, IdFlujo);
            dgvsuspendidos.DataSource = dt;
            dgvsuspendidos.DataBind();
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
        }
    }
}
