﻿using DevExpress.Export;
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
    public partial class DetalleMesaV2 : System.Web.UI.Page
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
            // wfiplib.credencial oCredencial = (wfiplib.credencial)Session["credencial"];
            int IdFlujo = manejo_sesion.Credencial.IdFlujo;
            DataTable dt = (new wfiplib.Reportes()).UltimoEstatusTramite(CalDesde.Date, CalHasta.Date, IdFlujo);
            dvgdTramites.DataSource = dt;
            dvgdTramites.DataBind();

            DataTable datos = new DataTable();

            datos = (new wfiplib.Reportes()).ExcelDetalleMesa(CalDesde.Date, CalHasta.Date, IdFlujo);
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
                if (string.Equals(Col.Caption, "IdTramite")) Col.Visible = false;
                dvgdMesa.Columns.Add(Col);
                Index++;
            }

        }

        protected void btnAsignar_Click(object sender, EventArgs e)
        {
            try
            {
                var tramites = dvgdTramites.GetFilteredSelectedValues("IdTramite");
                var mesas = dvgdTramites.GetFilteredSelectedValues("IdMesa");
                string idTramite = "(" + String.Join(",", tramites) + ")";
                string idMesa = mesas[0].ToString();
            }
            catch
            {
                // Mensaje.Text = "Seleccione uno o más trámites y algún usuario";
            }
        }
        protected void dvgdDetalleTramite_Init(object sender, EventArgs e)
        {
            int IdFlujo = manejo_sesion.Credencial.IdFlujo;
            ASPxGridView gridDetalle = (ASPxGridView)sender;
            string idTramite = gridDetalle.GetMasterRowFieldValues("IdTramite").ToString();
            DataTable dtD = (new wfiplib.Reportes()).DetalleMesa(CalDesde.Date, CalHasta.Date, idTramite, IdFlujo);
            gridDetalle.DataSource = dtD;
        }
        protected void lnkExportar_Click(object sender, EventArgs e)
        {
            XlsxExportOptionsEx modo = new XlsxExportOptionsEx();
            modo.TextExportMode = TextExportMode.Text;
            //modo.ExportType = ExportType.WYSIWYG;
            dvgdMesa.ExportXlsxToResponse("Sabana.xlsx", true, modo);
        }
    }
}