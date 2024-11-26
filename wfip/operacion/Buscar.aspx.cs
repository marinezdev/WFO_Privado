using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace wfip.operacion
{
    public partial class Buscar : System.Web.UI.Page
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
                int showMensaje = Convert.ToInt32(Request.QueryString["msg"]);
                if (showMensaje == 1)
                {
                    string strFolio = Convert.ToString(Request.QueryString["folio"]);
                    //showMessage("Su tramite ha sido registrado correctamente con el folio: " + strFolio);
                }
                pintaBusqueda();
                Session.Contents.Remove("nota");
            }
        }

        private void pintaBusqueda()
        {
            if (manejo_sesion.Credencial.Modulo == wfiplib.E_Modulo.Operacion)
            {
                if (manejo_sesion.Credencial.Id > 0)
                {
                    DataTable lstBuscar = (new wfiplib.admBuscar()).daTablaTramitesPromotoria(manejo_sesion.Credencial.Id);
                    rptBuscar.DataSource = lstBuscar;
                    rptBuscar.DataBind();
                }
            }
        }

        protected void rptBuscar_ItemDataBound(object sender, RepeaterItemEventArgs e)
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
                    case wfiplib.E_EstadoTramite.PCI:
                        ((Image)e.Item.FindControl("imgEstado")).ImageUrl = "~/img/bolaNaranja.png";
                        break;
                    default:
                        ((Image)e.Item.FindControl("imgEstado")).ImageUrl = "~/img/bolaGris.png";
                        break;
                }
            }
        }

        protected void rptBuscar_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName.Equals("Consultar"))
            {
                wfiplib.tramiteP oTramite = (new wfiplib.admBuscar()).carga(Convert.ToInt32(e.CommandArgument));
                if (oTramite.Estado == wfiplib.E_EstadoTramite.Ejecucion)
                {
                    Response.Redirect("pmTramiteEjctd.aspx?Id=" + e.CommandArgument.ToString());
                }
                else if (oTramite.IdTipoTramite == wfiplib.E_TipoTramite.InsPrivadoServicios)
                {
                    Response.Redirect("pmconsultaTramiteIns.aspx?Id=" + e.CommandArgument.ToString());
                }
                else
                    Response.Redirect("pmconsultaTramite.aspx?Id=" + e.CommandArgument.ToString());
            }
        }
    }
}
