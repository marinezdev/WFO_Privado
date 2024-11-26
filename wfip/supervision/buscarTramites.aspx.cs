using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace wfip.supervision
{
    public partial class buscarTramites : System.Web.UI.Page
    {
        wfiplib.Concentrado manejo_sesion = new wfiplib.Concentrado();

        protected void Page_Init(object sender, EventArgs e)
        {
            if (Session["credencial"] == null)
                Response.Redirect("~/Default.aspx");
            manejo_sesion = (wfiplib.Concentrado)Session["credencial"];
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            
                this.Muestradatos();
        }

        private void Muestradatos()
        {
            try
            {
                string tramite = "' '";
                string rfc = "' '";
                string contratante = "' '";
                string asegurado = "' '";
                DataTable dt = new DataTable();
                if (!string.IsNullOrEmpty(txtTramite.Text)) tramite = "'%" + txtTramite.Text.Trim() + "%'";
                if (!string.IsNullOrEmpty(txtRFC.Text)) rfc = "'%" + txtRFC.Text.Trim() + "%'";
                if (!string.IsNullOrEmpty(txtContratante.Text)) contratante = "'%" + txtContratante.Text.Trim() + "%'";
                if (!string.IsNullOrEmpty(txtAsegurado.Text)) asegurado = "'%" + txtAsegurado.Text.Trim() + "%'";
                dt = (new wfiplib.admMesa()).BuscarTramites(tramite, rfc, contratante, asegurado, manejo_sesion.Credencial.Id);
                dvgdTramites.DataSource = dt;
                dvgdTramites.DataBind();
                
                //=====================================
                var listaMesas = dvgdTramites.GetCurrentPageRowValues("IdMesa");
                string mesas = string.Join(",", listaMesas);
            }
            catch
            {

            }

        }
        protected void lnkExportar_Click(object sender, EventArgs e)
        {

            dvgdTramites.ExportXlsToResponse();
            // grdExport.WriteXlsToResponse();
        }

    }
}