using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DevExpress.XtraCharts;
using DevExpress.Web;
using DevExpress.XtraPrinting;
using DevExpress.Export;

namespace wfip.supervision
{
    public partial class sprDetallePromotoria : System.Web.UI.Page
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
            string modo = GridViewDetailExportMode.All.ToString();
            dvgdResumenPromotoria.SettingsDetail.ExportMode = (GridViewDetailExportMode)Enum.Parse(typeof(GridViewDetailExportMode), modo);

            Muestradatos();

           
        }

        private void Muestradatos()
        {
           
            // wfiplib.credencial oCredencial = (wfiplib.credencial)Session["credencial"];
            int IdFlujo = manejo_sesion.Credencial.IdFlujo;
            DataTable dtR = (new wfiplib.Reportes()).ResumenPromotoria(CalDesde.Date,CalHasta.Date,IdFlujo);
            dvgdResumenPromotoria.DataSource = dtR;
            dvgdResumenPromotoria.DataBind();
           

        }
        protected void lnkExportar_Click(object sender, EventArgs e)
        {
            dvgdResumenPromotoria.ExportXlsxToResponse("Promotoria.xlsx", new XlsxExportOptionsEx() { ExportType = ExportType.WYSIWYG });
        }

        protected void dvgdDetallePromotoria_BeforePerformDataSelect(object sender, EventArgs e)
        {
            /*int idPromotoria;
            wfiplib.credencial oCredencial = (wfiplib.credencial)Session["credencial"];
            int IdFlujo = oCredencial.IdFlujo;
            int annio = int.Parse(Annio.SelectedValue);
            int mes = int.Parse(Mes.SelectedValue);
            ASPxGridView grid = (ASPxGridView)sender;
            idPromotoria = int.Parse(grid.GetMasterRowFieldValues("idPromotoria").ToString());
            DataTable dtD = (new wfiplib.Reportes()).DetallePromotoria(annio, mes, IdFlujo);
            grid.DataSource = dtD;*/
            
        }

        protected void dvgdDetallePromotoria_Init(object sender, EventArgs e)
        {
            // wfiplib.credencial oCredencial = (wfiplib.credencial)Session["credencial"];
            int IdFlujo = manejo_sesion.Credencial.IdFlujo;
            ASPxGridView gridDetalle = (ASPxGridView)sender;
            int idPromotoria = int.Parse(gridDetalle.GetMasterRowFieldValues("idPromotoria").ToString());
            DataTable dtD = (new wfiplib.Reportes()).DetallePromotoria(CalDesde.Date, CalHasta.Date, IdFlujo,idPromotoria);
            gridDetalle.DataSource = dtD;
        }
    }
}