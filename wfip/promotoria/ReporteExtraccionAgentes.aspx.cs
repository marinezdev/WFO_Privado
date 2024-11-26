using DevExpress.Web;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace wfip.promotoria
{
    public partial class ReporteExtraccionAgentes : System.Web.UI.Page
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
            cargarStatusTramiteCombo_db();
            cargaAgentes();
        }

        protected void btnFiltroMes_Click(object sender, EventArgs e)
        {
            Mensaje.Text = "";
            if (CalDesde.Date <= CalHasta.Date)
            {
                string Agentes = "";
                for (int i = 0; i < listBoxAgentes.Items.Count; i++)
                {
                    if (listBoxAgentes.Items[i].Selected)
                    {
                        Agentes += listBoxAgentes.Items[i].Value.ToString() + ",";
                    }
                }

                Agentes = Agentes.TrimEnd(',');

                string status = "";
                for (int i = 0; i < listBoxEstatus.Items.Count; i++)
                {
                    if (listBoxEstatus.Items[i].Selected)
                    {
                        status += listBoxEstatus.Items[i].Value.ToString() + ",";
                    }
                }

                status = status.TrimEnd(',');
                
                //Mensaje.Text = Agentes + " - " + status;

                //Mensaje.Text = "REALIZA CONSULTA";
                DataTable dt = (new wfiplib.admTramite()).Tramites_Selecionar_PorAgentesStatus(manejo_sesion.Credencial.IdPromotoria,CalDesde.Date, CalHasta.Date, Agentes, status);
                rptTramites.DataSource = dt;
                rptTramites.DataBind();
            }
            else
            {
                Mensaje.Text = "La fecha 'Desde' debe ser menor a la fecha 'Hasta'";
            }
        }

        private void cargaAgentes()
        {
            DataTable dtAgentes = (new wfiplib.admAgentesPromotoria()).AgentesPromotoria_Seleccionar_PorIdPromotoria(manejo_sesion.Credencial.IdPromotoria);

            listBoxAgentes.DataSource = dtAgentes;
            listBoxAgentes.TextField = "DESCRIPCION";
            listBoxAgentes.ValueField = "CLAVE";
            listBoxAgentes.DataBind();
        }

        private void cargarStatusTramiteCombo_db()
        {
            DataTable dtSatus = (new wfiplib.admStatusTramite()).statusTramite_Seleccionar();

            listBoxEstatus.DataSource = dtSatus;
            listBoxEstatus.TextField = "Nombre";
            listBoxEstatus.ValueField = "Id";
            listBoxEstatus.DataBind();
        }
    }
}