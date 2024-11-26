using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace wfip.supervision
{
    public partial class sprReporteEstatusTramiteR : System.Web.UI.Page
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
            LlenarDatos();
        }
        private void LlenarDatos()
        {
            DataTable dt = new DataTable();
            int IdFlujo = manejo_sesion.Credencial.IdFlujo;
            string dEstatus = string.Empty;
            string estatus = cmbEstatus.Text;
            if (string.IsNullOrEmpty(estatus))
            {
                dEstatus = "' '";
            }
            else
            {
                string[] listaEstatus = estatus.Split(';');
                foreach (string estado in listaEstatus)
                {
                    dEstatus += "'" + estado.Trim() + "',";

                }
                dEstatus = dEstatus.Trim(',');
            }
            dt = new wfiplib.Reportes().TAT(CalDesde.Date, CalHasta.Date, dEstatus, IdFlujo, 1);
            dvgdEstatusTramite.DataSource = dt;
            dvgdEstatusTramite.DataBind();
            dvgdEstatusTramite.Caption = "ESTATUS TRÁMITE";
        }

        protected void lnkExportar_Click(object sender, EventArgs e)
        {

            dvgdEstatusTramite.ExportXlsToResponse();
            // grdExport.WriteXlsToResponse();
        }
    }
}