using DevExpress.Web;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Services;
using ClosedXML.Excel;
using System.Web;
using System.IO;
using System.Web.UI;

namespace wfip.supervision
{
    public partial class detalleMesaR : System.Web.UI.Page
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
                DataTable dt = (new wfiplib.NReportes()).UltimoEstatusTramite(CalDesde.Date, CalHasta.Date);
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


        protected void btnExportar_Click(object sender, EventArgs e)
        {
            Mensaje.Text = "";
            if (CalDesde.Date <= CalHasta.Date)
            {
                string Encrypt = (new Application.Operacion.UrlCifrardo()).Encrypt("In=" + CalDesde.Date + ",Fn=" + CalHasta.Date + ",Us=" + manejo_sesion.Credencial.Id + ",nu=1");
                string script = "window.open('detalleMesaRDescarga.aspx?data=" + Encrypt + "','Expediente', 'width = 800, height = 400');";
                ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);
                
            }
            else
            {
                Mensaje.Text = "La fecha 'Desde' debe ser menor a la fecha 'Hasta'";
                //rptTramitesEspera.Visible = false;
            }
        }

        protected void btnExportar_Click2(object sender, EventArgs e)
        {
            Mensaje.Text = "";
            if (CalDesde.Date <= CalHasta.Date)
            {
                string Encrypt = (new Application.Operacion.UrlCifrardo()).Encrypt("In=" + CalDesde.Date + ",Fn=" + CalHasta.Date + ",Us=" + manejo_sesion.Credencial.Id.ToString() + ",nu=2");
                string script = "window.open('detalleMesaRDescarga.aspx?data=" + Encrypt + "','Expediente', 'width = 800, height = 400');";
                ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);
            }
            else
            {
                Mensaje.Text = "La fecha 'Desde' debe ser menor a la fecha 'Hasta'";
                //rptTramitesEspera.Visible = false;
            }
        }

        [WebMethod]
        public static ConsultaBitacoraSabana BusquedaBitacoraDescraga()
        {
            DataTable dt = (new wfiplib.NReportes()).SabanaConsultaBitacoraDescarga();
            /* LLENAR JSON PARA RETORNAR */
            ConsultaBitacoraSabana jsonObject = new ConsultaBitacoraSabana();
            jsonObject.bitacoraSabanas = new List<BitacoraSabana>();

            foreach (DataRow row in dt.Rows)
            {
                jsonObject.bitacoraSabanas.Add(new BitacoraSabana()
                {
                    FechaRegistro = row["FechaRegistro"].ToString(),
                    FechaInicio = row["FechaInicio"].ToString(),
                    FechaFin = row["FechaFin"].ToString(),
                    NumRegistros = row["NumRegistro"].ToString(),
                    Usuario = row["Usuario"].ToString(),
                    NumSolicitudes = row["NumSolicitudes"].ToString(),
                });
            }

            return jsonObject;
        }
        
        [WebMethod]
        public static ConsultasMesas Busqueda(int Id)
        {
            DataTable dt = (new wfiplib.NReportes()).InformacionTramiteBitacoraPrivada(Id);
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
                    ObservacionPrivada = row["ObservacionPrivada"].ToString(),
                    NombreUsuario = row["NombreUsuario"].ToString(),
                });
            }
            
            return jsonObject;
        }
        
    }

    public class Consulta
    {
        public string Orden { get; set; }
        public string IdTramite { get; set; }
        public string FechaRegistro { get; set; }
        public string NMESA { get; set; }
        public string FechaInicio { get; set; }
        public string FechaTermino { get; set; }
        public string EstadoMesa { get; set; }
        public string Observacion { get; set; }
        public string ObservacionPrivada { get; set; }
        public string NombreUsuario { get; set; }
    }

    public class ConsultasMesas
    {
        public List<Consulta> consulta { get; set; }
    }

    public class BitacoraSabana
    {
        public string FechaRegistro { get; set; }
        public string FechaInicio { get; set; }
        public string FechaFin { get; set; }
        public string NumRegistros { get; set; }
        public string Usuario { get; set; }
        public string NumSolicitudes { get; set; }
    }

    public class ConsultaBitacoraSabana
    {
        public List<BitacoraSabana> bitacoraSabanas { get; set; }
    }
}