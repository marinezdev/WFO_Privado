using DevExpress.Web;
using DevExpress.XtraCharts;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace wfip.tablero
{
    public partial class MonitorGeneral : System.Web.UI.Page
    {
        wfiplib.Concentrado manejo_sesion = new wfiplib.Concentrado();
        protected void Page_Init(object sender, EventArgs e) {
            if (Session["credencial"] == null) Response.Redirect("~/Default.aspx");
            manejo_sesion = (wfiplib.Concentrado)Session["credencial"];

        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                llenaCboFlujos();
                int idFlujo = int.Parse(ddlFlujo.SelectedValue);
                Muestradatos(idFlujo);

            }
            else
            {

                dvgdMesa.Columns.Clear();
                int idFlujo = int.Parse(ddlFlujo.SelectedValue);
                Muestradatos(idFlujo);
            }
        }

        private void Muestradatos(int IdFlujo)
        {
            DataTable datos = new DataTable();

            datos = (new wfiplib.Reportes()).GeneralMesa(IdFlujo, DateTime.Now, DateTime.Now);
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

        private void pintaGrafica(DataTable pDatos)
        {
            dxChtTotales.Series.Clear();
            DataTable dtDatos = new DataTable();
            dtDatos = pDatos;
            dxChtTotales.DataSource = dtDatos;
            foreach (DataColumn Campo in dtDatos.Columns)
            {
                if (!string.Equals("ESTATUS", Campo.ColumnName))
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
        private void llenaCboFlujos()
        {
            ddlFlujo.DataSource = (new wfiplib.admFlujo()).Lista();
            ddlFlujo.DataValueField = "Id";
            ddlFlujo.DataTextField = "Nombre";
            ddlFlujo.DataBind();
        }

        protected void ActualizaInfo_Tick(object sender, EventArgs e)
        {
            DetalleReporte.Update();
        }
        protected void dvgdDetalleTramite_Init(object sender, EventArgs e)
        {
            int IdFlujo = int.Parse(ddlFlujo.SelectedValue);
            ASPxGridView gridDetalle = (ASPxGridView)sender;
            string status = gridDetalle.GetMasterRowFieldValues("ESTATUS").ToString();
            int idStatus = 0;
            switch (status)
            {
                case "Suspendidos":
                    idStatus = 5;
                    break;
                case "Hold":
                    idStatus = 2;
                    break;
                case "Rechazados":
                    idStatus = 4;
                    break;
                case "Ejecutados":
                    idStatus = 3;
                    break;
                case "Registrados":
                    idStatus = 0;
                    break;
            }
            DataTable dtD = (new wfiplib.Reportes()).DetalleMonitor(IdFlujo,idStatus);
            gridDetalle.DataSource = dtD;
        }
    }
}