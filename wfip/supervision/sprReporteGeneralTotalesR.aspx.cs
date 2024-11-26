using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DevExpress.Utils;
using DevExpress.XtraCharts;
using DevExpress.XtraCharts.Web;

namespace wfip.supervision
{
    public partial class sprReporteGeneralTotalesR : System.Web.UI.Page
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
            //wfiplib.credencial oCredencial = (wfiplib.credencial)Session["credencial"];
            int IdFlujo = manejo_sesion.Credencial.IdFlujo;
            DataTable dt = new DataTable();
            dt = new wfiplib.Reportes().GeneralTotales(CalDesde.Date, CalHasta.Date, IdFlujo);
            dvgdTotales.DataSource = dt;
            dvgdTotales.DataBind();
            dvgdTotales.Caption = "ACUMULADO MENSUAL";

            DataTable dtPorcentaje = new DataTable();
            dtPorcentaje.Columns.Add("Descripcion");
            dtPorcentaje.Columns.Add("Porcentaje", typeof(Int32));

            // MUESTRA DATOS EN LA GRAFICA
            foreach (DataRow tramite in dt.Rows)
            {
                dtPorcentaje.Rows.Add(new object[] { tramite["Descripcion"], tramite["Porcentaje"] });
            }

            dxChtTotales.DataSource = dtPorcentaje;
            dxChtTotales.SeriesDataMember = "Descripcion";
            dxChtTotales.SeriesTemplate.SetDataMembers("Descripcion", "Porcentaje");
            dxChtTotales.SeriesTemplate.ArgumentDataMember = "Descripcion";
            dxChtTotales.DataBind();

        }

        protected void lnkExportar_Click(object sender, EventArgs e)
        {

            dvgdTotales.ExportXlsToResponse();
            // grdExport.WriteXlsToResponse();
        }

        protected void lnkControl_Click(object sender, EventArgs e)
        {
            Response.Redirect("sprReporteOperativo.aspx");
        }
    }
}