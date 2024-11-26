using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace wfip.promotoria
{
    public partial class pmTramitesPendientes : System.Web.UI.Page
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
                Session.Contents.Remove("nota");
            }
        }

        private void pintaTramitesPendientes()
        {
            if (manejo_sesion.Credencial.Modulo == wfiplib.E_Modulo.Promotoria)
            {
                int IdPromotoria = (new wfiplib.admCredencial()).daPromotoria(manejo_sesion.Credencial.Id);
                if (IdPromotoria > 0)
                {
                    DataTable lstTramites = (new wfiplib.admTramite()).daTablaTramitesPromotoriaPendientes(IdPromotoria);
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
                    case wfiplib.E_EstadoTramite.PCI:
                        ((Image)e.Item.FindControl("imgEstado")).ImageUrl = "~/img/bolaNaranja.png";
                        break;
                    case wfiplib.E_EstadoTramite.RevPromotoria:
                        ((Image)e.Item.FindControl("imgEstado")).ImageUrl = "~/img/bolaMorada.png";
                        break;
                    case wfiplib.E_EstadoTramite.InfoCitaMedica:
                        ((Image)e.Item.FindControl("imgEstado")).ImageUrl = "~/img/bolaMorada.png";
                        break;
                    default:
                        ((Image)e.Item.FindControl("imgEstado")).ImageUrl = "~/img/bolaGris.png";
                        break;
                }
            }
        }

        protected void rptTramite_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName.Equals("Consultar")) {
                Response.Redirect(EncripParametros("Id=" + e.CommandArgument.ToString(), "pmconsultaTramite.aspx").URL, true);
                //Response.Redirect("pmconsultaTramite.aspx?Id=" + e.CommandArgument.ToString()); 
            }
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