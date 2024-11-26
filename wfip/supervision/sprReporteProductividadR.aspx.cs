using DevExpress.Export;
using DevExpress.Web;
using DevExpress.XtraPrinting;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace wfip.supervision
{
    public partial class sprReporteProductividadR : System.Web.UI.Page
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
            dvgdProductividad.SettingsDetail.ExportMode = (GridViewDetailExportMode)Enum.Parse(typeof(GridViewDetailExportMode), modo);
            Muestradatos();
        }

        private void Muestradatos()
        {
            // wfiplib.credencial oCredencial = (wfiplib.credencial)Session["credencial"];
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

            Datos = (new wfiplib.Reportes()).Productividad(CalDesde.Date, CalHasta.Date.Date, IdFlujo, dUsuarios);
            dvgdProductividad.DataSource = Datos;
            dvgdProductividad.DataBind();
        }

        protected void lnkExportar_Click(object sender, EventArgs e)
        {
            dvgdProductividad.ExportXlsxToResponse("Productividad.xlsx", new XlsxExportOptionsEx() { ExportType = ExportType.WYSIWYG });


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

        protected void dvgdDetalleProducividad_Init(object sender, EventArgs e)
        {
            int IdFlujo = manejo_sesion.Credencial.IdFlujo;
            ASPxGridView gridDetalle = (ASPxGridView)sender;
            string mesaNombre = gridDetalle.GetMasterRowFieldValues("mesaNombre").ToString();
            string usuario = gridDetalle.GetMasterRowFieldValues("operador").ToString();
            DataTable dtD = (new wfiplib.Reportes()).DetalleProductividad(CalDesde.Date, CalHasta.Date, IdFlujo, usuario, mesaNombre);
            gridDetalle.DataSource = dtD;
        }
    }
}