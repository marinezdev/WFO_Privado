using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;

namespace wfip.operacion
{
    public partial class opBuscaTramite : System.Web.UI.Page
    {
        public string Archivo = "";
        wfiplib.Concentrado manejo_sesion = new wfiplib.Concentrado();

        protected void Page_Init(object sender, EventArgs e)
        {
            if (Session["credencial"] == null)
                Response.Redirect("~/Default.aspx");
            manejo_sesion = (wfiplib.Concentrado)Session["credencial"];
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Muestradatos();
            }
            else
            {
                Muestradatos();
            }
        }
        private void Muestradatos()
        {
            // wfiplib.credencial oCredencial = (wfiplib.credencial)Session["credencial"];
            int IdFlujo = manejo_sesion.Credencial.IdFlujo;
           /* DataTable Mesas = new DataTable();
            DataTable Datos = new DataTable();
            Mesas = (new wfiplib.Reportes()).mesas(IdFlujo);
            Datos = (new wfiplib.Reportes()).relojChecador(CalDesde.Date, CalHasta.Date.Date);
            dvgdRelojChecador.DataSource = Datos;
            dvgdRelojChecador.DataBind();*/

        }


        protected void btnAsignar_Click(object sender, EventArgs e)
        {
            Server.Transfer("asignarTramites.aspx",true);
        }
    }
}