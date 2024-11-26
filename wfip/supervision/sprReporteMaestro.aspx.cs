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
    public partial class sprReporteMaestro : System.Web.UI.Page
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
            /*dateEdit.TimeSectionProperties.Visible = true;
            dateEdit.UseMaskBehavior = true;
            dateEdit.EditFormatString = "MM/dd/yyyy";
            dateEdit.DisplayFormatString = "None*/
            if (!IsPostBack)
            {
            }

        }
        
    }
}