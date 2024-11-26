using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DevExpress.XtraCharts;
using DevExpress.Web;

namespace wfip.supervision
{
    public partial class sprReporteSelProcesado : System.Web.UI.Page
    {
        wfiplib.Concentrado manejo_sesion = new wfiplib.Concentrado();

        protected void Page_Init(object sender, EventArgs e)
        {
            if (Session["credencial"] == null)
            {
                Response.Redirect("~/Default.aspx");
            }

            manejo_sesion = (wfiplib.Concentrado)Session["credencial"];
            CalDesde.EditFormatString = "dd/MM/yyyy";
            CalDesde.Date = DateTime.Today;
            CalHasta.EditFormatString = "dd/MM/yyyy";
            CalHasta.Date = DateTime.Today;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Muestradatos();
        }

        private void Muestradatos()
        {
            DataTable datos = new DataTable();
            datos = (new wfiplib.Reportes()).SeleccionProcesable(CalDesde.Date, CalHasta.Date.Date);
            rptProcesable.DataSource = datos;
            rptProcesable.DataBind();
            int Index = 1;
            rptProcesable.Columns.Clear();
            foreach (DataColumn Campo in datos.Columns)
            {
                GridViewDataColumn Col = new GridViewDataColumn();
                Col.VisibleIndex = Index;
                Col.Caption = Campo.ColumnName;
                Col.FieldName = Campo.ColumnName;
                Col.Width = 200;
                rptProcesable.Columns.Add(Col);
                Index++;
            }
        }

        protected void lnkExportar_Click(object sender, EventArgs e)
        {
            rptProcesable.ExportXlsToResponse();
        }
    }
}