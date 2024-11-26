using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace wfip.supervision
{
    public partial class UsuariosAtencion : System.Web.UI.Page
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
                Muestradatos();
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
                if (manejo_sesion.Credencial.IdFlujo > 0)
                    Datos = (new wfiplib.admTramite()).daTablaTramitesFlujoFechas(manejo_sesion.Credencial.IdFlujo, CalDesde.Date, CalHasta.Date);
                else
                    Datos = (new wfiplib.admTramite()).daTablaTramitesOperacionFechas(manejo_sesion.Credencial.IdPerfil, CalDesde.Date, CalHasta.Date);

                //rptTramitesEspera.DataSource = Datos;
                //rptTramitesEspera.DataBind();
            }
        }

        protected void rptTramitesPausa_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.CommandName))
            {
                Response.Redirect(EncripParametros("Id=" + e.CommandArgument.ToString() + ",Reg=1", "OpConsultaTramite.aspx").URL, true);
                //Response.Redirect("OpConsultaTramite.aspx?Id=" + e.CommandArgument.ToString()+"&Reg=1");
            }
        }

        protected void rptTramitesAtencion_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.CommandName))
            {
                //Response.Redirect("OpConsultaTramite.aspx?Id=" + e.CommandArgument.ToString()+"&Reg=1");
                Response.Redirect(EncripParametros("Id=" + e.CommandArgument.ToString() + ",Reg=1", "OpConsultaTramite.aspx").URL, true);
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

            DataTable TramitesPausados = null;
            DataTable TramitesAtencion = null;
            if (manejo_sesion.Credencial.IdFlujo > 0)
            {
                TramitesPausados = (new wfiplib.admTramite()).daTramiteMesaPausados(manejo_sesion.Credencial.Id);
                TramitesAtencion = (new wfiplib.admTramite()).daTramiteMesaAtencion(manejo_sesion.Credencial.Id);
            }
            else
            {
                TramitesPausados = null; //(new wfiplib.admTramite()).daTablaTramitesOperacion(manejo_sesion.Credencial.IdPerfil);
                TramitesAtencion = null; //(new wfiplib.admTramite()).daTablaTramitesOperacion(manejo_sesion.Credencial.IdPerfil);
            }

            rptTramitesPausa.DataSource = TramitesPausados;
            rptTramitesPausa.DataBind();
            rptTramitesAtencion.DataSource = TramitesAtencion;
            rptTramitesAtencion.DataBind();
            
            TramitesPausados = null;
            TramitesAtencion = null;
        }

        protected void btnExportarPausados_Click(object sender, EventArgs e)
        {
            DataSet dsExcel = null;
            DataTable TramitesPausados = null;
            

            if (manejo_sesion.Credencial.IdFlujo > 0)
            {
                TramitesPausados = (new wfiplib.admTramite()).daTramiteMesaPausados(manejo_sesion.Credencial.Id);
            }
            else
            {
                TramitesPausados = null; //(new wfiplib.admTramite()).daTablaTramitesOperacion(manejo_sesion.Credencial.IdPerfil);
            }

            dsExcel = new DataSet();
            dsExcel.Tables.Add(TramitesPausados);

            wfiplib.ExportarAExcel eax = new wfiplib.ExportarAExcel();
            eax.ExportarDataSetAExcel(this, dsExcel);


            TramitesPausados = null;
            dsExcel = null;
        }

        protected void btnRegresar_Clic(object sender, EventArgs e)
        {
            /*Response.Redirect("esperaSupervisorP.aspx", true);*/
            Response.Redirect("esperaSupervisorPR.aspx", true);
        }

        protected void btnExportarAtencion_Click(object sender, EventArgs e)
        {
            DataSet dsExcel = null;
            DataTable TramitesAtencion = null;


            if (manejo_sesion.Credencial.IdFlujo > 0)
            {
                TramitesAtencion = (new wfiplib.admTramite()).daTramiteMesaAtencion(manejo_sesion.Credencial.Id);
            }
            else
            {
                TramitesAtencion = null; //(new wfiplib.admTramite()).daTablaTramitesOperacion(manejo_sesion.Credencial.IdPerfil);
            }

            dsExcel = new DataSet();
            dsExcel.Tables.Add(TramitesAtencion);

            wfiplib.ExportarAExcel eax = new wfiplib.ExportarAExcel();
            eax.ExportarDataSetAExcel(this, dsExcel);

            TramitesAtencion = null;
            dsExcel = null;
            
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