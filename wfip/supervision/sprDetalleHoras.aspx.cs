using DevExpress.Web;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace wfip.supervision
{
    public partial class sprDetalleHoras : System.Web.UI.Page
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
            string dUsuarios = string.Empty;
            string usuarios = cmbUsuarios.Text;
            if (string.IsNullOrEmpty(usuarios))
            {
                dUsuarios = "' '";
            }
            else
            {
                string[] listaUsuarios = usuarios.Split(';');
                foreach (string usuario in listaUsuarios)
                {
                    dUsuarios += "'" + usuario.Trim() + "',";

                }
                dUsuarios = dUsuarios.Trim(',');
            }
            Datos = (new wfiplib.Reportes()).DetalleHoras(CalDesde.Date, CalHasta.Date, IdFlujo, dUsuarios);
            dvgdProductividad.DataSource = Datos;
            dvgdProductividad.DataBind();
        }

        protected void lnkExportar_Click(object sender, EventArgs e)
        {
            dvgdProductividad.ExportXlsToResponse();

        }
        protected void listUsuario_Init(object sender, EventArgs e)
        {
            ASPxListBox listaUsuarios = (ASPxListBox)sender;
            DataTable dtD = (new wfiplib.Reportes()).usuarios();
            foreach (DataRow usuario in dtD.Rows)
            {
                listaUsuarios.Items.Add(usuario["Nombre"].ToString());

            }

        }

    }
}