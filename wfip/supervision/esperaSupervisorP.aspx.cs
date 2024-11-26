using DevExpress.Web;
using System;
using DevExpress.XtraPrinting;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Export;

namespace wfip.supervision
{
    public partial class esperaSupervisorP : System.Web.UI.Page
    {
        wfiplib.Concentrado manejo_sesion = new wfiplib.Concentrado();
        Mensajes mensajes = new Mensajes();

        protected void Page_Init(object sender, EventArgs e)
        {
            if (Session["credencial"] == null)
                Response.Redirect("~/Default.aspx");
            manejo_sesion = (wfiplib.Concentrado)Session["credencial"];
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            string modo = GridViewDetailExportMode.All.ToString();
            dvgdTramites.SettingsDetail.ExportMode = (GridViewDetailExportMode)Enum.Parse(typeof(GridViewDetailExportMode), modo);
            LlenarDatos("estado");
            mostrarMapa();

            if (!IsPostBack)
            {
                Propiedades.UrlCifrardo urlCifrardo = ConsultaParametros();

                if (urlCifrardo.Result)
                {
                    int showMensaje = Convert.ToInt32(urlCifrardo.Action);
                    string strFolio = Convert.ToString(urlCifrardo.Folio);

                    switch (showMensaje)
                    {
                        case 1:
                            mensajes.MostrarMensaje(this, "El trámite se canceló correctamente.");
                            break;

                        case 2:
                            mensajes.MostrarMensaje(this, "El trámite (Folio:" + strFolio + ") se asignó correctamente.");
                            break;
                    }
                }
                //int showMensaje = Convert.ToInt32(Request.QueryString["action"]);
                //string strFolio = Convert.ToString(Request.QueryString["folio"]);

                //switch (showMensaje)
                //{
                //    case 1:
                //        mensajes.MostrarMensaje(this, "El trámite se canceló correctamente.");
                //        break;

                //    case 2:
                //        mensajes.MostrarMensaje(this, "El trámite (Folio:" + strFolio + ") se asignó correctamente.");
                //        break;
                //}
                //if (showMensaje == 1)
                //{
                //    string strFolio = Convert.ToString(Request.QueryString["folio"]);
                //    mensajes.MostrarMensaje(this, "El trámite se canceló correctamente.");
                //}
            }
        }
        private void mostrarMapa()
        {
            int IdFlujo = manejo_sesion.Credencial.IdFlujo;
            DataTable dt = (new wfiplib.Reportes()).ResumenMapa(IdFlujo);
            
            dvgdIndicadores.DataSource = dt;
            dvgdIndicadores.DataBind();
            DataTable datos = new DataTable();
            
            datos = (new wfiplib.Reportes()).MapaSupervisor(IdFlujo,"%");
            dvgdTramites.DataSource = datos;
            dvgdTramites.DataBind();
            
        }

        [WebMethod]
        public static string asignacionAutomatica(int pIdFlujo, int pParametro)
        {
            string resultado = "Configuracion actualizada";
            wfiplib.admFlujo adm = new wfiplib.admFlujo();
            adm.cambiaModo(pIdFlujo, pParametro);
            return resultado;
        }

        //================================================================

        private void LlenarDatos(string estatus)
        {
            DataTable dt = new DataTable();
            int IdFlujo = manejo_sesion.Credencial.IdFlujo;
            DateTime Hasta = DateTime.Now;
            DateTime Desde = DateTime.Now.AddDays(-30);
            dt = new wfiplib.Reportes().TAT(Desde, Hasta, estatus, IdFlujo, 2);
            dvgdEstatusTramite.DataSource = dt;
            dvgdEstatusTramite.DataBind();
            dvgdEstatusTramite.Caption = "TOTALES POR ESTATUS";

            DataTable dtTotales = new DataTable();
            dtTotales.Columns.Add("ESTATUS");
            dtTotales.Columns.Add("TOTAL", typeof(Int32));

            // MUESTRA DATOS EN LA GRAFICA
            foreach (DataRow tramite in dt.Rows)
            {
                dtTotales.Rows.Add(new object[] { tramite["ESTATUS"], tramite["TOTAL"] });
            }

            dxChtTotales.DataSource = dtTotales;
            dxChtTotales.SeriesDataMember = "ESTATUS";
            dxChtTotales.SeriesTemplate.SetDataMembers("ESTATUS", "TOTAL");
            dxChtTotales.SeriesTemplate.ArgumentDataMember = "ESTATUS";
            dxChtTotales.DataBind();
        }
        protected void dvgdDetallePromotoria_BeforePerformDataSelect(object sender, EventArgs e)
        {


        }
        protected void dvgdDetallePromotoria_Init(object sender, EventArgs e)
        {
            try
            {
                int IdFlujo = manejo_sesion.Credencial.IdFlujo;
                ASPxGridView gridDetalle = (ASPxGridView)sender;
                int estado = int.Parse(gridDetalle.GetMasterRowFieldValues("ESTADO").ToString());
                DataTable dtD = (new wfiplib.Reportes()).ResumenPromotoria(IdFlujo, estado);
                gridDetalle.DataSource = dtD;
            }
            catch (Exception ex)
            {
                string mensaje = ex.Message.ToString();

            }

        }
        public void MuestraTramiteOnclick(Object sender, CommandEventArgs e)
        {
            if (e.CommandName.Equals("Consultar"))
            {
                Response.Redirect(EncripParametros("Id=" + e.CommandArgument.ToString(), "sprMapaSupervisor.aspx").URL, true);
                //Response.Redirect("sprMapaSupervisor.aspx?Id=" + e.CommandArgument.ToString());
            }
        }
        protected void lnkExportar_Click(object sender, EventArgs e)
        {
               grdExport.WriteXlsxToResponse(new XlsxExportOptionsEx() { ExportType = ExportType.WYSIWYG });
        }

        protected void LinkExportarMapa_Click(object sender, EventArgs e)
        {
            dvgdTramites.ExportXlsxToResponse("MapaSupervisor.xlsx", new XlsxExportOptionsEx() { ExportType = ExportType.WYSIWYG });
        }

        protected void LinkUsuarios_Click(object sender, EventArgs e)
        {
            Response.Redirect("UsuariosAtencion.aspx", true);
        }

        protected void dvgdDetalleTramite_Init(object sender, EventArgs e)
        {
            int IdFlujo = manejo_sesion.Credencial.IdFlujo;
            ASPxGridView gridDetalle = (ASPxGridView)sender;
            int idMesa = int.Parse(gridDetalle.GetMasterRowFieldValues("idMesa").ToString());
            DataTable dtD = (new wfiplib.Reportes()).MapaSupervisorDetalle(idMesa, IdFlujo);
            gridDetalle.DataSource = dtD;
        }


        private Propiedades.UrlCifrardo ConsultaParametros()
        {
            Propiedades.UrlCifrardo urlCifrardo = new Propiedades.UrlCifrardo();
            urlCifrardo.Result = false;
            try
            {
                string parametros = (new Application.Operacion.UrlCifrardo()).Decrypt(Request.QueryString["data"].ToString());
                string action = "";
                string folio = "";

                String[] spearator = { "," };
                String[] strlist = parametros.Split(spearator, StringSplitOptions.RemoveEmptyEntries);

                foreach (String s in strlist)
                {

                    string BusquedaAction = stringBetween(s + ".", "action=", ".");
                    if (BusquedaAction.Length > 0)
                    {
                        action = BusquedaAction;
                    }

                    string BusquedaFolio = stringBetween(s + ".", "folio=", ".");
                    if (BusquedaFolio.Length > 0)
                    {
                        folio = BusquedaFolio;
                    }

                }

                if (action.Length > 0)
                {
                    urlCifrardo.Action = action.ToString();
                    urlCifrardo.Result = true;
                }
                else
                {
                    urlCifrardo.Action = "";
                    urlCifrardo.Result = false;
                }

                if (folio.Length > 0)
                {
                    urlCifrardo.Folio = folio.ToString();
                }
                else
                {
                    urlCifrardo.Folio = "";
                    urlCifrardo.Result = false;
                }

            }
            catch (Exception)
            {
                urlCifrardo.Result = false;
            }

            return urlCifrardo;
        }
        private Propiedades.UrlCifrardo EncripParametros(string parametros, string Direccion)
        {
            Propiedades.UrlCifrardo urlCifrardo = new Propiedades.UrlCifrardo();

            string Encrypt = (new Application.Operacion.UrlCifrardo()).Encrypt(parametros);

            urlCifrardo.URL = Direccion + "?data=" + Encrypt;

            return urlCifrardo;
        }
        public static string stringBetween(string Source, string Start, string End)
        {
            string result = "";
            if (Source.Contains(Start) && Source.Contains(End))
            {
                int StartIndex = Source.IndexOf(Start, 0) + Start.Length;
                int EndIndex = Source.IndexOf(End, StartIndex);
                result = Source.Substring(StartIndex, EndIndex - StartIndex);
                return result;
            }

            return result;
        }

    }
}