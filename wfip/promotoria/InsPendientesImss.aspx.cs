using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace wfip.promotoria
{
    public partial class PendientesImss : System.Web.UI.Page
    {
        wfiplib.Concentrado manejo_sesion = new wfiplib.Concentrado();
        Mensajes mensajes = new Mensajes();

        protected void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
            }
            manejo_sesion = (wfiplib.Concentrado)Session["credencial"];
            if (Session["credencial"] == null)
                Response.Redirect("~/Default.aspx");
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            { }
        }

        protected void ddlAnnQuincena_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Poliza");
            dt.Columns.Add("Unidad");
            dt.Columns.Add("Estatus");
            dt.Columns.Add("Resultado");
            dt.Columns.Add("Folio");
            dt.Columns.Add("FechaEnvio");

            dt.Rows.Add("FAZ502", "214", "Aceptada", "", "JA201801200001", "10-10-2018");
            dt.Rows.Add("FAZ502", "214", "Aceptada", "", "JA201801200027", "12-10-2018");
            dt.Rows.Add("ATT217", "217", "Aceptada", "", "JA201801200027", "12-10-2018");
            dt.Rows.Add("AXM506", "217", "Aceptada", "", "JA201801200027", "12-10-2018");
            dt.Rows.Add("EVC270", "217", "Aceptada", "", "JA201801200027", "12-10-2018");
            dt.Rows.Add("MLI867", "217", "Aceptada", "", "JA201801200027", "12-10-2018");

            GVDetalle.DataSource = dt;
            GVDetalle.DataBind();

        }
    }
}