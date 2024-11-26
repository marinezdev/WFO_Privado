using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace wfip.supervision
{
    public partial class sprReportePromotorias : System.Web.UI.Page
    {
        wfiplib.Concentrado manejo_sesion = new wfiplib.Concentrado();
        protected void Page_Init(object sender, EventArgs e)
        {
            if (Session["credencial"] == null)
                Response.Redirect("~/Default.aspx");
            manejo_sesion = (wfiplib.Concentrado)Session["credencial"];
            CalDesde.EditFormatString = "yyyy-MM-dd";
            CalDesde.Date = DateTime.Now.AddDays(-1);
            CalDesde.MaxDate = DateTime.Today;
            CalHasta.EditFormatString = "yyyy-MM-dd";
            CalHasta.Date = DateTime.Today;
            CalHasta.MaxDate = DateTime.Today;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            cargaPromotoria();
        }

        private void cargaPromotoria()
        {
            DataTable dtPromotorias = (new wfiplib.admCatPromotorias().Promotorias_Selecionar());
          
            listBoxPromotorias.DataSource = dtPromotorias;
            listBoxPromotorias.TextField = "Nombre";
            listBoxPromotorias.ValueField = "Id";
            listBoxPromotorias.DataBind();
        }

        protected void btnFiltroMes_Click(object sender, EventArgs e)
        {
            Mensaje.Text = "";
            if (CalDesde.Date <= CalHasta.Date)
            {
                string promotorias = "";
                for (int i = 0; i < listBoxPromotorias.Items.Count; i++)
                {
                    if (listBoxPromotorias.Items[i].Selected)
                    {
                        promotorias += listBoxPromotorias.Items[i].Value.ToString() + ",";
                    }
                }

                promotorias = promotorias.TrimEnd(',');

                DataTable dt = (new wfiplib.admCatPromotorias()).Tramites_Selecionar_PorPromotorias( CalDesde.Date, CalHasta.Date, promotorias);
                rptTramites.DataSource = dt;
                rptTramites.DataBind();

                string script = "";
                script = "$('#tblTramitesEspera').DataTable({" +
                    "'language': {'url': '//cdn.datatables.net/plug-ins/1.10.15/i18n/Spanish.json'}," +
                    "scrollY: '400px'," +
                    "scrollX: true," +
                    "scrollCollapse: true, " +
                    "fixedColumns: true," +
                    "dom: 'Blfrtip', " +
                    "buttons: [" +
                    "'copyHtml5'," +
                    "'excelHtml5'," +
                    "'csvHtml5'," +
                    "'pdfHtml5'" +
                    "]" +
                    "}); ";
                ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);
            }
            else
            {
                Mensaje.Text = "La fecha 'Desde' debe ser menor a la fecha 'Hasta'";
            }
        }
    }
}