using DevExpress.Export;
using DevExpress.Web;
using DevExpress.XtraPrinting;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace wfip.supervision
{
    public partial class sprReporteCaducadosR : System.Web.UI.Page
    {
        protected wfiplib.Concentrado manejo_sesion = new wfiplib.Concentrado();
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
            string modo = GridViewDetailExportMode.All.ToString();
            dvgdEstatusTramite.SettingsDetail.ExportMode = (GridViewDetailExportMode)Enum.Parse(typeof(GridViewDetailExportMode), modo);
            LlenarDatos();
        }
        private void LlenarDatos()
        {
            DataTable dt = new DataTable();
            int IdFlujo = manejo_sesion.Credencial.IdFlujo;
            string dEstatus = string.Empty;
            dEstatus = "'Caducado'";
            dt = new wfiplib.Reportes().TAT(CalDesde.Date, CalHasta.Date, dEstatus, IdFlujo, 3);
            dvgdEstatusTramite.DataSource = dt;
            dvgdEstatusTramite.DataBind();
        }

        protected void lnkExportar_Click(object sender, EventArgs e)
        {

            dvgdEstatusTramite.ExportXlsxToResponse("Caducados.xlsx", new XlsxExportOptionsEx() { ExportType = ExportType.WYSIWYG });
        }
        protected void dvgdDetalleCaducados_Init(object sender, EventArgs e)
        {
            int IdFlujo = manejo_sesion.Credencial.IdFlujo;
            ASPxGridView gridDetalle = (ASPxGridView)sender;
            int IdTramite = int.Parse(gridDetalle.GetMasterRowFieldValues("Id").ToString());
            DataTable dtD = (new wfiplib.Reportes()).DetalleCaducados(CalDesde.Date, CalHasta.Date, IdFlujo, IdTramite);
            gridDetalle.DataSource = dtD;
        }

    }
}