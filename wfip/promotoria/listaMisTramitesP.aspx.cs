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
    public partial class listaMisTramitesP : System.Web.UI.Page
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
            pintaMisTramites();
            if (!IsPostBack)
            {
                pintaMisTramites();
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

        private void pintaMisTramites()
        {
            if (manejo_sesion.Credencial.Modulo == wfiplib.E_Modulo.Promotoria)
            {
                if (manejo_sesion.Credencial.IdPromotoria > 0)
                {
                    String TipoTramite = Request.QueryString["t"];
                    //string TipoTramite = Label2.Text.ToString();
                    DataTable lstTramites = (new wfiplib.admTramite()).daTablaTramitesPromotoriaTipoTramite(manejo_sesion.Credencial.IdPromotoria, TipoTramite);
                    DataTable lstTramitesE = (new wfiplib.admTramite()).daTablaTramitesPromotoriaTipoTramiteExcel(manejo_sesion.Credencial.IdPromotoria, TipoTramite);
                    dvgdListaTramites.DataSource = lstTramitesE;
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
            if (e.CommandName.Equals("Consultar"))
            {
                wfiplib.tramiteP oTramite = (new wfiplib.admTramite()).carga(Convert.ToInt32(e.CommandArgument));
                if (oTramite.Estado == wfiplib.E_EstadoTramite.Ejecucion)
                {
                    Response.Redirect("pmTramiteEjctd.aspx?Id=" + e.CommandArgument.ToString());
                }
                else
                    Response.Redirect("pmconsultaTramite.aspx?Id=" + e.CommandArgument.ToString());
            }
        }
        protected void lnkExportar_Click(object sender, EventArgs e)
        {
            dvgdListaTramites.ExportXlsxToResponse();
        }

    }
}