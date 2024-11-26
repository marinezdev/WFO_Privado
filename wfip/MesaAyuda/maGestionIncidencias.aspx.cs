using System;
using System.Data;
using System.Web.UI.WebControls;

namespace wfip.MesaAyuda
{
    public partial class maGestionIncidencias : System.Web.UI.Page
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
                pintaTramitesPendientes();
                //Revisar esto GSL
                //=============================
                //string Mes = "";
                //string Annio = "";
                DateTime desde = DateTime.Now;
                //========================
                // wfiplib.credencial oCredencial = (wfiplib.credencial)Session["credencial"];
                int IdFlujo = manejo_sesion.Credencial.IdFlujo;
                DataTable dtTotales = (new wfiplib.Reportes()).Top10Recepcion(desde, desde, IdFlujo);
                dxChtTotales.DataSource = dtTotales;
                dxChtTotales.SeriesDataMember = "Promotoria";
                dxChtTotales.SeriesTemplate.SetDataMembers("Nombre", "NumTramites");
                dxChtTotales.DataBind();

                DataTable dtSuspendidos = (new wfiplib.Reportes()).Top10Suspendidos(desde, desde,IdFlujo);
                dxChtSuspendidos.DataSource = dtSuspendidos;
                dxChtSuspendidos.SeriesDataMember = "Promotoria";
                dxChtSuspendidos.SeriesTemplate.SetDataMembers("Nombre", "Porcentaje");
                dxChtSuspendidos.DataBind();
            }
        }

        private void pintaTramitesPendientes()
        {
            // wfiplib.credencial oCredencial = (wfiplib.credencial)Session["credencial"];
            if (manejo_sesion.Credencial.Modulo == wfiplib.E_Modulo.MesaAyuda)
            {
                DataTable lstTramites = (new wfiplib.admTramite()).daTablaTodosLosTramitesPendientes();
                rptTramite.DataSource = lstTramites;
                rptTramite.DataBind();
            }
        }

        protected void rptTramite_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                wfiplib.E_EstadoTramite Estado = (wfiplib.E_EstadoTramite)((DataRowView)(e.Item.DataItem))["Estado"];
                switch (Estado)
                {
                    case wfiplib.E_EstadoTramite.Ejecucion:
                        ((Image)e.Item.FindControl("imgEstado")).ImageUrl = "~/img/bolaVerde.png";
                        break;
                    case wfiplib.E_EstadoTramite.Rechazo:
                        ((Image)e.Item.FindControl("imgEstado")).ImageUrl = "~/img/bolaRoja.png";
                        break;
                    case wfiplib.E_EstadoTramite.Proceso:
                        ((Image)e.Item.FindControl("imgEstado")).ImageUrl = "~/img/bolaAzul.png";
                        break;
                    case wfiplib.E_EstadoTramite.Hold:
                        ((Image)e.Item.FindControl("imgEstado")).ImageUrl = "~/img/bolaAmarilla.png";
                        break;
                    case wfiplib.E_EstadoTramite.Suspendido:
                        ((Image)e.Item.FindControl("imgEstado")).ImageUrl = "~/img/bolaNaranja.png";
                        break;
                    default:
                        ((Image)e.Item.FindControl("imgEstado")).ImageUrl = "~/img/bolaGris.png";
                        break;
                }
            }
        }

        protected void rptTramite_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName.Equals("Consultar")) { Response.Redirect("maConsultaTramite.aspx?Id=" + e.CommandArgument.ToString()); }
        }
    }
}