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
    public partial class sprReporteLaboratorios : System.Web.UI.Page
    {
        wfiplib.Concentrado manejo_sesion = new wfiplib.Concentrado();

        protected void Page_Init(object sender, EventArgs e)
        {
            if (Session["credencial"] == null)
                Response.Redirect("~/Default.aspx");
                manejo_sesion = (wfiplib.Concentrado)Session["credencial"];
                CalDesde.EditFormatString = "yyyy-MM-dd";
                CalDesde.Date = DateTime.Now.AddDays(-1);
                CalDesde.MaxDate = DateTime.Today;
                CalHasta.EditFormatString = "yyyy-MM-dd";
                CalHasta.Date = DateTime.Today;
                CalHasta.MaxDate = DateTime.Today;
        }

        protected void btnFiltroMes_Click(object sender, EventArgs e)
        {
            Mensaje.Text = "";
            if (CalDesde.Date <= CalHasta.Date)
            {
                DataTable dt = (new wfiplib.admCatProveedor()).LaboratorioConCitasMedicas(CalDesde.Date, CalHasta.Date);
                rptTramitesEspera.DataSource = dt;
                rptTramitesEspera.DataBind();
            }
            else
            {

                Mensaje.Text = "La fecha 'Desde' debe ser menor a la fecha 'Hasta'";
            }
        }

        [WebMethod]
        public static ConsultaLaboratorios consultaLaboratorios(int Id, int Combo)
        {
            DataTable dt = (new wfiplib.admCatProveedor()).consultaLaboratorios(Id, Combo);
            /* LLENAR JSON PARA RETORNAR */
            ConsultaLaboratorios jsonObject = new ConsultaLaboratorios();
            jsonObject.citaMedicas = new List<CitaMedica>();

            foreach (DataRow row in dt.Rows)
            {
                jsonObject.citaMedicas.Add(new CitaMedica()
                {
                    FolioCompuesto = row["FolioCompuesto"].ToString(),
                    Estatus = row["Estatus"].ToString(),
                    DatosHtml = row["DatosHtml"].ToString(),
                    Sexo = row["Sexo"].ToString(),
                    Edad = row["Edad"].ToString(),
                    SumaPolizas = row["SumaPolizas"].ToString(),
                    PrimaTotal = row["PrimaTotal"].ToString(),
                    Notas = row["Notas"].ToString(),
                });
            }

            return jsonObject;
        }
    }

    public class CitaMedica
    {
        public string FolioCompuesto { get; set; }
        public string Estatus { get; set; }
        public string DatosHtml { get; set; }
        public string Sexo { get; set; }
        public string Edad { get; set; }
        public string SumaPolizas { get; set; }
        public string PrimaTotal { get; set; }
        public string Notas { get; set; }
    }


    public class ConsultaLaboratorios
    {
        public List<CitaMedica> citaMedicas { get; set; }
    }
}