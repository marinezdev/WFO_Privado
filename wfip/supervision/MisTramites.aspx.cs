using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace wfip.supervision
{
    public partial class MisTramites : System.Web.UI.Page
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
                //Muestradatos();
            }

        }

        protected void ConsultaFechasBD(object sender, EventArgs e)
        {
            MSresultado2.Text = "";
            DateTime Fecha1 = Convert.ToDateTime(CalDesde.Text.ToString());
            DateTime Fecha2 = Convert.ToDateTime(CalHasta.Text.ToString());

            DateTime hora1 = Convert.ToDateTime("00:00:00");
            DateTime hora2 = Convert.ToDateTime("23:59:59");

            DateTime F1 = Fecha1.AddHours(hora1.Hour).AddMinutes(hora1.Minute).AddSeconds(hora1.Second);
            DateTime F2 = Fecha2.AddHours(hora2.Hour).AddMinutes(hora2.Minute).AddSeconds(hora2.Second);

            if (Fecha1 > Fecha2)
            {
                MSresultado2.Text = "La fecha inicial no puede ser mayor a la fecha final ";
            }
            else
            {
                DataTable Datos = null;
                Datos = (new wfiplib.admTramite()).daTablaTramitesFechas(CalDesde.Date, CalHasta.Date, manejo_sesion.Credencial.Id);

                rptTramitesEspera.DataSource = Datos;
                rptTramitesEspera.DataBind();
            }
        }

        protected void ConsultaFiltros(object sender, EventArgs e)
        {
            MSresultado2.Text = "";
            string Folio = TextFolio.Text.ToString().Trim();
            string RFC = TextRFC.Text.ToString().Trim();
            string Nombre = txNombre.Text.ToString().Trim();
            string ApPaterno = txApPat.Text.ToString().Trim();
            string ApMaterno = txApMat.Text.ToString().Trim();

            DataTable Datos = null;
            Datos = (new wfiplib.admTramite()).daTablaTramitesFiltros(Folio, RFC, Nombre, ApPaterno, ApMaterno, manejo_sesion.Credencial.Id);

            rptTramitesEspera.DataSource = Datos;
            rptTramitesEspera.DataBind();
        }


        protected void LimpiaFormulario(object sender, EventArgs e)
        {
            MSresultado2.Text = "";
            TextFolio.Text = "";
            TextRFC.Text = "";
            txNombre.Text = "";
            txApPat.Text = "";
            txApMat.Text = "";
            CalDesde.Text = "";
            CalHasta.Text = "";

            DataTable Datos = (new wfiplib.admTramite()).daTablaTramitesFlujoFiltros(manejo_sesion.Credencial.IdFlujo, "NULL", "NULL", "NULL", "NULL", "NULL");
            
            rptTramitesEspera.DataSource = Datos;
            rptTramitesEspera.DataBind();
        }

        protected void rptTramitesEspera_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.CommandName))
            {
                Response.Redirect(EncripParametros("Id=" + e.CommandArgument.ToString(), "OpConsultaTramite.aspx").URL, true);
                //Response.Redirect("OpConsultaTramite.aspx?Id=" + e.CommandArgument.ToString());
            }
        }

        protected string GetFormatString(object value)
        {
            return value == null ? string.Empty : value.ToString();
        }

        private void Muestradatos()
        {
            //SOLO MUESTRA TODO EL LISTADO DE TRAMITES AUN NO HACE EL FILTRO ****//
            //DataTable Datos = new DataTable();
            //Datos = (new wfiplib.admTramite()).daTablaTramites();

            DataTable Datos = null;
            if (manejo_sesion.Credencial.IdFlujo > 0)
            {
                Datos = (new wfiplib.admTramite()).daTablaTramitesFlujo();
            }
            else
            {
                Datos = (new wfiplib.admTramite()).daTablaTramitesOperacion(manejo_sesion.Credencial.IdPerfil);
            }

            rptTramitesEspera.DataSource = Datos;
            rptTramitesEspera.DataBind();
            //wfiplib.credencial oCredencial = (wfiplib.credencial)Session["credencial"];
            /*wfiplib.admTramiteMesa adm = new wfiplib.admTramiteMesa();

            rptTramitesEspera.DataSource = adm.daListaAtrapados(manejo_sesion.Credencial.Id);
            rptTramitesEspera.DataBind();

            //dvgdTramitesEspera.DataSource = Datos;
            //dvgdTramitesEspera.DataBind();*/
        }

        private Propiedades.UrlCifrardo EncripParametros(string parametros, string Direccion)
        {
            Propiedades.UrlCifrardo urlCifrardo = new Propiedades.UrlCifrardo();

            string Encrypt = (new Application.Operacion.UrlCifrardo()).Encrypt(parametros);

            urlCifrardo.URL = Direccion + "?data=" + Encrypt;

            return urlCifrardo;
        }
    }
}