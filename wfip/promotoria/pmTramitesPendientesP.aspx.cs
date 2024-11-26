using DevExpress.XtraPrinting;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace wfip.promotoria
{
    public partial class pmTramitesPendientesP : System.Web.UI.Page
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
            pintaTramitesPendientes();
            if (!IsPostBack)
            {
                pintaTramitesPendientes();
                Session.Contents.Remove("nota");
            }
            // VALIDA EL TIPO DE TRAMITE TANTO PUBLICO COMO PRIVADO
            if (!String.IsNullOrEmpty(Request.QueryString["t"]))
            {
                // Query string value is there so now use it
                String TipoTramite = Request.QueryString["t"];
                if (TipoTramite == "privado" || TipoTramite == "publico")
                {
                    Label2.Text = TipoTramite.ToUpper();
                }
                else
                {
                    Response.Redirect("~/Default.aspx");
                }
            }
            else
            {
                Response.Redirect("~/Default.aspx");
            }
        }

        private void pintaTramitesPendientes()
        {
            //wfiplib.credencial oCredencial = (wfiplib.credencial)Session["credencial"];
            if (manejo_sesion.Credencial.Modulo == wfiplib.E_Modulo.Promotoria)
            {
                int IdPromotoria = (new wfiplib.admCredencial()).daPromotoria(manejo_sesion.Credencial.Id);
                if (IdPromotoria > 0)
                {
                    String TipoTramite = Request.QueryString["t"];
                    DataTable lstTramites = (new wfiplib.admTramite()).daTablaTramitesPromotoriaPendientesTipoTramite(IdPromotoria, TipoTramite);
                    dvgdListaTramites.DataSource = lstTramites;
                    dvgdListaTramites.DataBind();
                    rptTramite.DataSource = lstTramites;
                    rptTramite.DataBind();
                }
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
            if (e.CommandName.Equals("Consultar")) { Response.Redirect("pmconsultaTramite.aspx?Id=" + e.CommandArgument.ToString()); }
        }
        protected void lnkExportar_Click(object sender, EventArgs e)
        {
            DevExpress.XtraPrinting.XlsExportOptions options = new XlsExportOptions();
            options.TextExportMode = TextExportMode.Text;
#pragma warning disable CS0618 // 'ASPxGridExporterBase.WriteXlsToResponse(XlsExportOptions)' está obsoleto: 'Use another overload of this method instead.'
            grdExport.WriteXlsToResponse(options);
#pragma warning restore CS0618 // 'ASPxGridExporterBase.WriteXlsToResponse(XlsExportOptions)' está obsoleto: 'Use another overload of this method instead.'

        }
    }
}