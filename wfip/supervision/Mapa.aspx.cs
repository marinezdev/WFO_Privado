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
    public partial class Mapa : System.Web.UI.Page
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
            mostrarMapa();

            if (!IsPostBack)
            {
            }
        }
        private void mostrarMapa()
        {
            int IdFlujo = manejo_sesion.Credencial.IdFlujo;
            DataTable dt = (new wfiplib.Reportes()).ResumenMapa(IdFlujo);

            dvgdIndicadores.DataSource = dt;
            dvgdIndicadores.DataBind();
            DataTable datos = new DataTable();

            datos = (new wfiplib.Reportes()).MapaSupervisor(IdFlujo, "%");
            dvgdTramites.DataSource = datos;
            dvgdTramites.DataBind();

        }

        [WebMethod]
        public static string asignacionAutomatica(int pIdFlujo, int pParametro)
        {
            string resultado = "Configuracion actualizada";
            //wfiplib.admFlujo adm = new wfiplib.admFlujo();
            //adm.cambiaModo(pIdFlujo, pParametro);
            return resultado;
        }

        //================================================================

        protected void dvgdDetallePromotoria_BeforePerformDataSelect(object sender, EventArgs e)
        {


        }
        protected void dvgdDetallePromotoria_Init(object sender, EventArgs e)
        {
            //try
            //{
            //    int IdFlujo = manejo_sesion.Credencial.IdFlujo;
            //    ASPxGridView gridDetalle = (ASPxGridView)sender;
            //    int estado = int.Parse(gridDetalle.GetMasterRowFieldValues("ESTADO").ToString());
            //    DataTable dtD = (new wfiplib.Reportes()).ResumenPromotoria(IdFlujo, estado);
            //    gridDetalle.DataSource = dtD;
            //}
            //catch (Exception ex)
            //{
            //    string mensaje = ex.Message.ToString();

            //}

        }
        public void MuestraTramiteOnclick(Object sender, CommandEventArgs e)
        {
            //if (e.CommandName.Equals("Consultar"))
            //{
            //    Response.Redirect(EncripParametros("Id=" + e.CommandArgument.ToString(), "sprMapaSupervisor.aspx").URL, true);
            //}
        }
        protected void lnkExportar_Click(object sender, EventArgs e)
        {
            //grdExport.WriteXlsxToResponse(new XlsxExportOptionsEx() { ExportType = ExportType.WYSIWYG });
        }

        protected void LinkExportarMapa_Click(object sender, EventArgs e)
        {
            //dvgdTramites.ExportXlsxToResponse("MapaSupervisor.xlsx", new XlsxExportOptionsEx() { ExportType = ExportType.WYSIWYG });
        }

        protected void LinkUsuarios_Click(object sender, EventArgs e)
        {
            //Response.Redirect("UsuariosAtencion.aspx", true);
        }

        protected void dvgdDetalleTramite_Init(object sender, EventArgs e)
        {
            //int IdFlujo = manejo_sesion.Credencial.IdFlujo;
            //ASPxGridView gridDetalle = (ASPxGridView)sender;
            //int idMesa = int.Parse(gridDetalle.GetMasterRowFieldValues("idMesa").ToString());
            //DataTable dtD = (new wfiplib.Reportes()).MapaSupervisorDetalle(idMesa, IdFlujo);
            //gridDetalle.DataSource = dtD;
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