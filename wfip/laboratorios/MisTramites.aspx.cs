using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace wfip.laboratorios
{
    public partial class MisTramites : System.Web.UI.Page
    {
        wfiplib.ConcentradoLaboratorios manejo_sesion_labs = new wfiplib.ConcentradoLaboratorios();

        protected void Page_Init(object sender, EventArgs e)
        {
            if (Session["SesionLabs"] == null)
                Response.Redirect("Default.aspx");
            manejo_sesion_labs = (wfiplib.ConcentradoLaboratorios)Session["SesionLabs"];
        }

        public void MuestraTramiteOnclick(Object sender, CommandEventArgs e)
        {
            if (e.CommandName.Equals("Consultar"))
            {
                Response.Redirect("OpConsultaTramite.aspx?Id=" + e.CommandArgument.ToString());
                //MSresultado2.Text = e.CommandArgument.ToString();
            }

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Muestradatos();
        }

        protected void ConsultaFechasBD(object sender, EventArgs e)
        {
            //MSresultado2.Text = "";
            DateTime Fecha1 = Convert.ToDateTime(CalDesde.Text.ToString());
            DateTime Fecha2 = Convert.ToDateTime(CalHasta.Text.ToString());

            DateTime hora1 = Convert.ToDateTime("00:00:00");
            DateTime hora2 = Convert.ToDateTime("23:59:59");

            DateTime F1 = Fecha1.AddHours(hora1.Hour).AddMinutes(hora1.Minute).AddSeconds(hora1.Second);
            DateTime F2 = Fecha2.AddHours(hora2.Hour).AddMinutes(hora2.Minute).AddSeconds(hora2.Second);

            if (Fecha1 > Fecha2)
            {
                MSresultado2.Text = "La fecha inicial no puede ser mayor a la fecha final ";
            }
            else
            {
                DataTable Datos = null;
                Datos = (new wfiplib.admTramite()).daTablaTramitesLaboratoriosFechas(manejo_sesion_labs.CPP.Id_proveedor, wfiplib.E_EstadoTramite.CMConfirmacionPendiente, F1, F2);

                dvgdTramitesEspera.DataSource = Datos;
                dvgdTramitesEspera.DataBind();
            }
        }

        protected string GetFormatString(object value)
        {
            return value == null ? string.Empty : value.ToString();
        }

        private void Muestradatos()
        {
            DataTable Datos = null;
            Datos = (new wfiplib.admTramite()).daTablaTramitesLaboratorios(manejo_sesion_labs.CPP.Id_proveedor, wfiplib.E_EstadoTramite.CMConfirmacionPendiente);


            dvgdTramitesEspera.DataSource = Datos;
            dvgdTramitesEspera.DataBind();
        }
    }
}