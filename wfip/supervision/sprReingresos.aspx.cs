using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace wfip.supervision
{
    public partial class sprReingresos : System.Web.UI.Page
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
            //cmbEstatus.Items.FindByValue(cmbEstatus.ToString()).Selected = true;

        }
        protected void Page_Load(object sender, EventArgs e)
        {
            LlenarDatos();
        }
        private void LlenarDatos()
        {
            DataTable dt = new DataTable();
            dt = new wfiplib.Reportes().Reingresos(CalDesde.Date, CalHasta.Date,cmbEstatus.SelectedValue);
            dvgdTotales.DataSource = dt;
            dvgdTotales.DataBind();
        }

        protected void lnkExportar_Click(object sender, EventArgs e)
        {

            dvgdTotales.ExportXlsToResponse();
        }

        protected void lnkControl_Click(object sender, EventArgs e)
        {
            Response.Redirect("sprReporteOperativo.aspx");
        }
    }
}