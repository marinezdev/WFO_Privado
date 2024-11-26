using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace wfip.supervision
{
    public partial class sprReporteTramiteSemanaR : System.Web.UI.Page
    {
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

                int annio = DateTime.Now.Year;
                int mes = DateTime.Now.Month;
                Annio.Items.FindByValue(annio.ToString()).Selected = true;
                Mes.Items.FindByValue(mes.ToString()).Selected = true;
                LlenarDatos(mes, annio);

            }
            else
            {
                int annio = int.Parse(Annio.SelectedValue);
                int mes = int.Parse(Mes.SelectedValue);
                LlenarDatos(mes, annio);
            }

        }
        private void LlenarDatos(int mes, int annio)
        {
            DataTable dt = new DataTable();
            //wfiplib.credencial oCredencial = (wfiplib.credencial)Session["credencial"];
            int IdFlujo = manejo_sesion.Credencial.IdFlujo;
            dt = new wfiplib.Reportes().TramiteSemana(mes, annio, IdFlujo);
            dvgdTramiteSemana.DataSource = dt;
            dvgdTramiteSemana.DataBind();
            dvgdTramiteSemana.Caption = "TRÁMITES POR SEMANA";


            DataTable dtTotalSemana = new DataTable();
            DataTable dtTotales = new DataTable();
            dtTotales = new wfiplib.Reportes().TotalTramiteSemana(mes, annio, IdFlujo);
            dtTotalSemana.Columns.Add("SEMANA");
            dtTotalSemana.Columns.Add("TRAMITES", typeof(Int32));

            // MUESTRA DATOS EN LA GRAFICA
            foreach (DataRow tramite in dtTotales.Rows)
            {
                dtTotalSemana.Rows.Add(new object[] { tramite["SEMANA"], tramite["TRAMITES"] });
            }

            dxChtTotales.DataSource = dtTotalSemana;
            dxChtTotales.SeriesDataMember = "SEMANA";
            dxChtTotales.SeriesTemplate.SetDataMembers("SEMANA", "TRAMITES");
            dxChtTotales.SeriesTemplate.ArgumentDataMember = "SEMANA";
            dxChtTotales.DataBind();
        }

        protected void lnkExportar_Click(object sender, EventArgs e)
        {

            dvgdTramiteSemana.ExportXlsToResponse();
            // grdExport.WriteXlsToResponse();
        }
    }
}