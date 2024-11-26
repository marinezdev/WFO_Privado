using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace wfip.supervision
{
    public partial class sprReporteOneShot : System.Web.UI.Page
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

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
            }
        }

        protected void btnFiltroMes_Click(object sender, EventArgs e)
        {
            Mensaje.Text = "";
            if (CalDesde.Date <= CalHasta.Date)
            {
                DataTable dt = (new wfiplib.admCatPrioridad()).OneShot(CalDesde.Date, CalHasta.Date);
                rptTramitesEspera.DataSource = dt;
                rptTramitesEspera.DataBind();

                string script = "";
                script = "$('#tblTramitesEspera').DataTable({" +
                    "'language': {'url': '//cdn.datatables.net/plug-ins/1.10.15/i18n/Spanish.json'}," +
                    "scrollY: '400px'," +
                    "scrollX: true," +
                    "scrollCollapse: true, " +
                    "fixedColumns: true," +
                    "dom: 'Blfrtip', " +
                    "buttons: [" +
                    "'copyHtml5'," +
                    "'excelHtml5'," +
                    "'csvHtml5'," +
                    "'pdfHtml5'" +
                    "]" +
                    "}); ";
                ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);
            }
            else
            {

                Mensaje.Text = "La fecha 'Desde' debe ser menor a la fecha 'Hasta'";
                //rptTramitesEspera.Visible = false;
            }
        }

        protected void rptTramitesEspera_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.CommandName))
            {
                //Response.Redirect("OpConsultaTramite.aspx?Id=" + e.CommandArgument.ToString());
                Response.Redirect(EncripParametros("Id=" + e.CommandArgument.ToString(), "OpConsultaTramite.aspx").URL, true);
            }
        }

        private Propiedades.UrlCifrardo EncripParametros(string parametros, string Direccion)
        {
            Propiedades.UrlCifrardo urlCifrardo = new Propiedades.UrlCifrardo();

            string Encrypt = (new Application.Operacion.UrlCifrardo()).Encrypt(parametros);

            urlCifrardo.URL = Direccion + "?data=" + Encrypt;

            return urlCifrardo;
        }

        [WebMethod]
        public static ConsultasMesas Busqueda(int Id)
        {
            DataTable dt = (new wfiplib.NReportes()).InformacionTramiteBitacora(Id);
            /* LLENAR JSON PARA RETORNAR */
            ConsultasMesas jsonObject = new ConsultasMesas();
            jsonObject.consulta = new List<Consulta>();

            foreach (DataRow row in dt.Rows)
            {
                jsonObject.consulta.Add(new Consulta()
                {
                    Orden = row["NORDENREPORTE"].ToString(),
                    IdTramite = row["IdTramite"].ToString(),
                    FechaRegistro = row["FechaRegistro"].ToString(),
                    NMESA = row["NMESA"].ToString(),
                    FechaInicio = row["FechaInicio"].ToString(),
                    FechaTermino = row["FechaTermino"].ToString(),
                    EstadoMesa = row["EstadoMesa"].ToString(),
                    Observacion = row["Observacion"].ToString(),
                    NombreUsuario = row["NombreUsuario"].ToString(),
                });
            }

            return jsonObject;
        }

    }
}