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
    public partial class sprReportePorcientoSuspensionR : System.Web.UI.Page
    {
        wfiplib.Concentrado manejo_sesion = new wfiplib.Concentrado();

        protected void Page_Init(object sender, EventArgs e)
        {
            if (Session["credencial"] == null)
                Response.Redirect("~/Default.aspx");
            manejo_sesion = (wfiplib.Concentrado)Session["credencial"];
            CalDesde.EditFormatString = "dd/MM/yyyy";
            CalDesde.Date = DateTime.Today;
            CalHasta.EditFormatString = "dd/MM/yyyy";
            CalHasta.Date = DateTime.Today;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            var valor = Ancho.Value;
            dxChtTotales.Width = new Unit(Convert.ToInt32(Ancho.Value));
            LlenarDatos();
        }
        protected void dxChtTotales_CustomCallback(object sender, DevExpress.XtraCharts.Web.CustomCallbackEventArgs e)
        {
            dxChtTotales.Width = new Unit(Convert.ToInt32(Ancho.Value));
        }
        private void LlenarDatos()
        {

            DataTable dt = new DataTable();
            //wfiplib.credencial oCredencial = (wfiplib.credencial)Session["credencial"];
            int IdFlujo = manejo_sesion.Credencial.IdFlujo;

            dt = new wfiplib.Reportes().PorcentajeSuspendidos(CalDesde.Date, CalHasta.Date, IdFlujo);
            dvgdPorcientoSuspension.DataSource = dt;
            dvgdPorcientoSuspension.DataBind();
            dvgdPorcientoSuspension.Caption = "CIFRAS DE CONTROL";
            DataTable dtMotivos = new DataTable();
            dtMotivos = new wfiplib.Reportes().PorcentajeMotivosSuspension(CalDesde.Date, CalHasta.Date, IdFlujo);
            dvgdMotivosSuspension.DataSource = dtMotivos;
            dvgdMotivosSuspension.DataBind();
            dvgdMotivosSuspension.Caption = "MOTIVOS DE SUSPENSIÓN (RESUMEN ANUAL)";


            DataTable dtPorcentaje = new DataTable();
            dtPorcentaje.Columns.Add("Promotoria", typeof(string));
            dtPorcentaje.Columns.Add("Suspendidos", typeof(Int32));

            // MUESTRA DATOS EN LA GRAFICA
            foreach (DataRow tramite in dt.Rows)
            {
                dtPorcentaje.Rows.Add(new object[] { tramite["PromotoriaClave"], tramite["suspendidos"] });
            }

            dxChtTotales.Series.Clear();
            dxChtTotales.DataSource = dtPorcentaje;
            Series SerieS = new Series("Suspendidos", ViewType.Bar);
            dxChtTotales.Series.Add(SerieS);
            SerieS.ArgumentScaleType = ScaleType.Qualitative;
            SerieS.ArgumentDataMember = "Promotoria";
            SerieS.ValueScaleType = ScaleType.Numerical;
            SerieS.ValueDataMembers.AddRange(new string[] { "Suspendidos" });
            dxChtTotales.DataBind();
        }

        protected void lnkExportar_Click(object sender, EventArgs e)
        {

            dvgdPorcientoSuspension.ExportXlsToResponse();
            // grdExport.WriteXlsToResponse();
        }

    }
}