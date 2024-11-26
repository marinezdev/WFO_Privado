using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace wfip.supervision
{
    public partial class sprReporteMesa : System.Web.UI.Page
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
            this.Muestradatos();  
        }

        private void Muestradatos()
        {

            DataTable dt = new DataTable();
            dt.Columns.Add("Tipo");
            dt.Columns.Add("Admision");
            dt.Columns.Add("RevisionDocumental");
            dt.Columns.Add("Plad");
            dt.Columns.Add("Ejecucion");
            dt.Rows.Add(new object[] {"Suspendidos", "16", "14", "2", "0" });
            dt.Rows.Add(new object[] { "Rechazados", "0", "0", "0", "69" });
            dt.Rows.Add(new object[] { "Espera Atencion", "34", "153", "93", "22" });
            dt.Rows.Add(new object[] { "Hold/suspension", "0", "300", "157", "0" });



            dvgdSuspendidos.DataSource = dt;
            dvgdSuspendidos.DataBind();
            dvgdSuspendidos.GroupBy(dvgdSuspendidos.Columns["Tipo"], 0);
            dvgdSuspendidos.ExpandAll();



            //Series sre = new Series("sreFranjaZ", ViewType.Line);
            //sre.Points.Add(new SeriesPoint(8, 29));
            //sre.Points.Add(new SeriesPoint(9, 77));
            //sre.Points.Add(new SeriesPoint(10, 103));
            //sre.Points.Add(new SeriesPoint(11, 169));
            //sre.Points.Add(new SeriesPoint(12, 180));
            //sre.Points.Add(new SeriesPoint(13, 170));
            //sre.Points.Add(new SeriesPoint(14, 134));
            //sre.Points.Add(new SeriesPoint(15, 155));
            //sre.Points.Add(new SeriesPoint(16, 179));
            //sre.Points.Add(new SeriesPoint(17, 201));
            //sre.Points.Add(new SeriesPoint(18, 65));
            //sre.Points.Add(new SeriesPoint(19, 32));
            //sre.Points.Add(new SeriesPoint(20, 12));

            //dvchtSuspendidos.DataSource = dt;
            //dvchtSuspendidos.SeriesDataMember = "Promotoria";
            //dvchtSuspendidos.SeriesTemplate.SetDataMembers("Promotoria", "NumTramites");
            //dvchtSuspendidos.DataBind();

        }

        protected void lnkExportar_Click(object sender, EventArgs e)
        {
            grdExport.WriteXlsToResponse();
        }
    }
}