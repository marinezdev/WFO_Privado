using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DevExpress.XtraCharts;
using DevExpress.Web;
using DevExpress.Utils;

namespace wfip.supervision
{
    public partial class sprReporteGeneralMesaR : System.Web.UI.Page
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
            var valor = Ancho.Value;
            dxChtTotales.Width = new Unit(Convert.ToInt32(Ancho.Value));
            Muestradatos();
        }

        private void Muestradatos()
        {
            //wfiplib.credencial oCredencial = (wfiplib.credencial)Session["credencial"];
            int IdFlujo = manejo_sesion.Credencial.IdFlujo;
            DataTable datos = new DataTable();
            datos = (new wfiplib.Reportes()).GeneralMesa(IdFlujo, CalDesde.Date, CalHasta.Date);
            dvgdMesa.DataSource = datos;
            dvgdMesa.DataBind();
            int Index = 1;
            dvgdMesa.Columns.Clear();
            foreach (DataColumn Campo in datos.Columns)
            {
                GridViewDataColumn Col = new GridViewDataColumn();
                Col.VisibleIndex = Index;
                Col.Caption = Campo.ColumnName;
                Col.FieldName = Campo.ColumnName;
                dvgdMesa.Columns.Add(Col);
                Index++;
            }
            pintaGrafica(datos);
        }
        protected void dxChtTotales_CustomCallback(object sender, DevExpress.XtraCharts.Web.CustomCallbackEventArgs e)
        {
            dxChtTotales.Width = new Unit(Convert.ToInt32(Ancho.Value));
        }
        private void pintaGrafica(DataTable pDatos)
        {
            dxChtTotales.Legend.Visibility = DefaultBoolean.False;
            dxChtTotales.Series.Clear();
            DataTable dtDatos = new DataTable();
            dtDatos = pDatos;
            dxChtTotales.DataSource = dtDatos;
            foreach (DataColumn Campo in dtDatos.Columns)
            {
                if (!string.Equals("ESTATUS", Campo.ColumnName) && !string.Equals("TOTAL", Campo.ColumnName))
                {
                    Series Serie = new Series(Campo.ColumnName, ViewType.Bar);
                    dxChtTotales.Series.Add(Serie);
                    Serie.ArgumentScaleType = ScaleType.Auto;
                    Serie.ArgumentDataMember = "ESTATUS";
                    Serie.ValueScaleType = ScaleType.Numerical;
                    Serie.ValueDataMembers.AddRange(new string[] { Campo.ColumnName });
                }

            }

            dxChtTotales.DataBind();
        }

        protected void lnkExportar_Click(object sender, EventArgs e)
        {
            dvgdMesa.ExportXlsToResponse();
        }

    }
}