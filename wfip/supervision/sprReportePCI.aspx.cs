using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace wfip.supervision
{
    public partial class sprReportePCI : System.Web.UI.Page
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

        }


        protected void btnConsulta_Click(object sender, EventArgs e)
        {
            DataTable dt = (new wfiplib.admTramite()).Tramites_Selecionar_StatusPCI();
            rptTramitesPCI.DataSource = dt;
            rptTramitesPCI.DataBind(); 
        }

        [WebMethod]
        public static ConsultaTramitesPCI consultaLaboratorios()
        {
            DataTable dt = (new wfiplib.admTramite()).Tramites_Selecionar_StatusPCI_Total();
            
            /* LLENAR JSON PARA RETORNAR */
            ConsultaTramitesPCI jsonObject = new ConsultaTramitesPCI();
            jsonObject.statusPCI = new List<StatusPCI>();

            foreach (DataRow row in dt.Rows)
            {
                jsonObject.statusPCI.Add(new StatusPCI()
                {
                    labels = row["CLAVE PROMOTORIA"].ToString(),
                    data = row["Total"].ToString(),
                    backgroundColor = "getRandomColorHex()"
                });
            }

            return jsonObject;
        }
    }

    public class StatusPCI
    {
        public string labels { get; set; }
        public string data { get; set; }
        public string backgroundColor { get; set; }
    }
    
    public class ConsultaTramitesPCI
    {
        public List<StatusPCI> statusPCI { get; set; }
    }
}