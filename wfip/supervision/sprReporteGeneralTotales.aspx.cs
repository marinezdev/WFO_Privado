using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DevExpress.Utils;
using DevExpress.XtraCharts;
using DevExpress.XtraCharts.Web;
using System.Threading;

namespace wfip.supervision
{
    public partial class sprReporteGeneralTotales : System.Web.UI.Page
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
            LlenarDatos();
            
        }
        private void LlenarDatos()
        {
            //wfiplib.credencial oCredencial = (wfiplib.credencial)Session["credencial"];
            int IdFlujo = manejo_sesion.Credencial.IdFlujo;
            DataTable dt = new DataTable();
            dt = new wfiplib.Reportes().GeneralTotales(CalDesde.Date, CalHasta.Date, IdFlujo);
            string tTotalTramites = string.Empty;
            string tTotalPorcentajes = string.Empty;

            //Totales
            Object TotalTramites= dt.Compute("Sum(Totales)", string.Empty);
            Object TotalPorcentajes = dt.Compute("Sum(Porcentaje)", string.Empty);

            if (string.IsNullOrEmpty(TotalTramites.ToString())) tTotalTramites = "0";
            else tTotalTramites = TotalTramites.ToString();

            if (string.IsNullOrEmpty(TotalPorcentajes.ToString())) tTotalPorcentajes = "0";
            else tTotalPorcentajes = "100.0";

            dt.Rows.Add("TOTALES", int.Parse(tTotalTramites), float.Parse(tTotalPorcentajes));

           dvgdTotales.DataSource = dt;
           dvgdTotales.DataBind();
           dvgdTotales.Caption = "ACUMULADO DEL PERIODO";


            DataTable dtPorcentaje = new DataTable();
            dtPorcentaje.Columns.Add("Descripcion");
            dtPorcentaje.Columns.Add("Porcentaje", typeof(Int32));

            // MUESTRA DATOS EN LA GRAFICA
            foreach (DataRow tramite in dt.Rows)
            {
               if (!string.Equals(tramite["Descripcion"].ToString(), "TOTALES"))
                {
                    dtPorcentaje.Rows.Add(new object[] { tramite["Descripcion"], tramite["Porcentaje"] });

                }
            }
            dxChtTotales.Series.Clear();
            dxChtTotales.DataSource = dtPorcentaje;
            Series SerieS = new Series("Porcentaje", ViewType.Bar);
            dxChtTotales.Series.Add(SerieS);
            SerieS.ArgumentScaleType = ScaleType.Qualitative;
            SerieS.ArgumentDataMember = "Descripcion";
            SerieS.ValueScaleType = ScaleType.Numerical;
            SerieS.ValueDataMembers.AddRange(new string[] { "Porcentaje" });
            dxChtTotales.DataBind();
            ClientScript.RegisterStartupScript(GetType(), "hwa", "ocultarSpinner();", true);
        }

        protected void lnkExportar_Click(object sender, EventArgs e)
        {

            dvgdTotales.ExportXlsToResponse();
        }

        protected void lnkControl_Click(object sender, EventArgs e)
        {
            Response.Redirect("sprReporteOperativo.aspx");
        }

    }
}