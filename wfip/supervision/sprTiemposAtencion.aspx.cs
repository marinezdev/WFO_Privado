using DevExpress.Web;
using DevExpress.XtraCharts;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace wfip.supervision
{
    public partial class sprTiemposAtencion : System.Web.UI.Page
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
            DataTable Mesas = new DataTable();
            DataTable Datos = new DataTable();
            Mesas = (new wfiplib.Reportes()).mesas(IdFlujo); 
           // dvgdMesa.Visible = true;

            Datos = (new wfiplib.Reportes()).TiemposAtencion(CalDesde.Date, CalHasta.Date, IdFlujo);
            dvgdMesa.DataSource = Datos;
            dvgdMesa.DataBind();
            dvgdMesa.Columns.Clear();

            int index = 1;
            GridViewDataColumn Producto = AgregarColumna("TRAMITE","FOLIOCOMPUESTO");
            dvgdMesa.Columns.Add(Producto);
            Producto.VisibleIndex = index;
            foreach(DataRow Registro in Mesas.Rows)
            {
                index++;
                foreach (DataColumn Campo in Mesas.Columns)
                {
                    GridViewBandColumn Col = new GridViewBandColumn(Registro[Campo].ToString());
                    dvgdMesa.Columns.Add(Col);
                    Col.Columns.Add(AgregarColumna("Espera", Registro[Campo].ToString()+"_E"));
                    Col.Columns.Add(AgregarColumna("Atención", Registro[Campo].ToString()+"_A"));
                    Col.VisibleIndex = index;
                }
            }
        }
        protected GridViewDataColumn AgregarColumna( string descripcion,string campo)
        {
            GridViewDataColumn subCol = new GridViewDataColumn();
            subCol.Caption = descripcion;
            subCol.FieldName = campo;
            if (string.Equals(descripcion, "TRAMITE"))
            {
                subCol.Width = 140;
            }
            return subCol;
        }
        protected void lnkExportar_Click(object sender, EventArgs e)
        {
            dvgdMesa.ExportXlsToResponse();
        }
    }
}