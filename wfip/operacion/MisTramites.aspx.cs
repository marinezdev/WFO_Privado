using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI.WebControls;

namespace wfip.operacion
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
                

                DataTable Datos = (new wfiplib.admTramite()).daTablaTramitesFechas(manejo_sesion.Credencial.IdFlujo, CalDesde.Date, CalHasta.Date, 250);
                /*
                if (manejo_sesion.Credencial.IdFlujo > 0)
                    Datos = (new wfiplib.admTramite()).daTablaTramitesFlujoFechas(manejo_sesion.Credencial.IdFlujo, F1, F2);
                else
                    Datos = (new wfiplib.admTramite()).daTablaTramitesOperacionFechas(manejo_sesion.Credencial.IdPerfil, F1, F2);
                */
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

            DataTable Datos = (new wfiplib.admTramite()).daTablaTramitesFiltros(Folio, RFC, Nombre, ApPaterno, ApMaterno, manejo_sesion.Credencial.Id);
            /*
            if (manejo_sesion.Credencial.IdFlujo > 0)
                
                Datos = (new wfiplib.admTramite()).daTablaTramitesFlujoFiltros(manejo_sesion.Credencial.IdFlujo, Folio, RFC, Nombre, ApPaterno, ApMaterno);
            else
                Datos = (new wfiplib.admTramite()).daTablaTramitesFlujoFiltros(manejo_sesion.Credencial.IdPerfil, Folio, RFC, Nombre, ApPaterno, ApMaterno);
            */
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

            DataTable Datos = (new wfiplib.admTramite()).daTablaTramitesFiltros("NULL", "NULL", "NULL", "NULL", "NULL", manejo_sesion.Credencial.Id);

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

        private Propiedades.UrlCifrardo EncripParametros(string parametros, string Direccion)
        {
            Propiedades.UrlCifrardo urlCifrardo = new Propiedades.UrlCifrardo();

            string Encrypt = (new Application.Operacion.UrlCifrardo()).Encrypt(parametros);

            urlCifrardo.URL = Direccion + "?data=" + Encrypt;

            return urlCifrardo;
        }

        /*
        private void Muestradatos()
        {
            //SOLO MUESTRA TODO EL LISTADO DE TRAMITES AUN NO HACE EL FILTRO ***
            //DataTable Datos = new DataTable();

            //Datos = (new wfiplib.admTramite()).daTablaTramites();

            DataTable Datos = null;
            if (manejo_sesion.Credencial.IdFlujo > 0)
                Datos = (new wfiplib.admTramite()).daTablaTramitesFlujo(manejo_sesion.Credencial.IdFlujo);
            else
                Datos = (new wfiplib.admTramite()).daTablaTramitesOperacion(manejo_sesion.Credencial.IdPerfil);

            rptTramitesEspera.DataSource = Datos;
            rptTramitesEspera.DataBind();
            //wfiplib.credencial oCredencial = (wfiplib.credencial)Session["credencial"];
            /*wfiplib.admTramiteMesa adm = new wfiplib.admTramiteMesa();

            rptTramitesEspera.DataSource = adm.daListaAtrapados(manejo_sesion.Credencial.Id);
            rptTramitesEspera.DataBind();

            //dvgdTramitesEspera.DataSource = Datos;
            //dvgdTramitesEspera.DataBind();
        }
        */
    }
}
 