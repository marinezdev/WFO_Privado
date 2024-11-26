using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace wfip.supervision
{
    public partial class sprEticaCumplimiento : System.Web.UI.Page
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
                Muestradatos();
        }
        private void Muestradatos()
        {
            int IdFlujo = manejo_sesion.Credencial.IdFlujo;
            DataTable Datos = new DataTable();
            Datos = (new wfiplib.Reportes()).EticaCumplimiento(CalDesde.Date, CalHasta.Date.Date, txtRFC.Text,txtContratante.Text);
            dvgdEticaCumplimiento.DataSource = Datos;
            dvgdEticaCumplimiento.DataBind();
        }

        protected void lnkExportar_Click(object sender, EventArgs e)
        {
            dvgdEticaCumplimiento.ExportXlsToResponse();
        }
    }
}