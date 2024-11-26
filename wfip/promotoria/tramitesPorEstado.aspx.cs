using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace wfip.promotoria
{
    public partial class tramitesPorEstado : System.Web.UI.Page
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
                Propiedades.UrlCifrardo urlCifrardo = ConsultaParametros();
                if (urlCifrardo.Result)
                {

                    wfiplib.E_EstadoTramite estado;
                    Enum.TryParse(urlCifrardo.Estado, out estado);
                    pintaTramites(estado);

                    Label3.Visible = false;
                    if (Request.Params["estado"] == "PCI")
                        Label3.Visible = true;

                }
                else
                {
                    Response.Redirect("~/Default.aspx");
                }

                //if (Request.Params["estado"] != null)
                //{
                //    wfiplib.E_EstadoTramite estado;
                //    Enum.TryParse(Request.Params["estado"], out estado);
                //    pintaTramites(estado);

                //    Label3.Visible = false;
                //    if (Request.Params["estado"] == "PCI")
                //        Label3.Visible = true;
                //}

            }
        }

        private void pintaTramites(wfiplib.E_EstadoTramite pEstado)
        {
            if (manejo_sesion.Credencial.Modulo == wfiplib.E_Modulo.Promotoria)
            {
                if (manejo_sesion.Credencial.IdPromotoria > 0)
                {
                    DataTable lstTramites = (new wfiplib.admTramite()).daTablaTramitesPromotoriaPorEstado(manejo_sesion.Credencial.IdPromotoria, pEstado);
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
            if (e.CommandName.Equals("Consultar"))
            {
                wfiplib.tramiteP oTramite = (new wfiplib.admTramite()).carga(Convert.ToInt32(e.CommandArgument));
                if (oTramite.Estado == wfiplib.E_EstadoTramite.Ejecucion)
                {
                    Response.Redirect(EncripParametros("Id=" + e.CommandArgument.ToString(), "pmTramiteEjctd.aspx").URL, true);
                    //Response.Redirect("pmTramiteEjctd.aspx?Id=" + e.CommandArgument.ToString());
                }
                else
                {
                    Response.Redirect(EncripParametros("Id=" + e.CommandArgument.ToString(), "pmconsultaTramite.aspx").URL, true);
                    //Response.Redirect("pmconsultaTramite.aspx?Id=" + e.CommandArgument.ToString());
                }
            }
        }


        private Propiedades.UrlCifrardo ConsultaParametros()
        {
            Propiedades.UrlCifrardo urlCifrardo = new Propiedades.UrlCifrardo();
            try
            {
                string parametros = (new Application.Operacion.UrlCifrardo()).Decrypt(Request.QueryString["data"].ToString());
                string estado = "";

                String[] spearator = { "," };
                String[] strlist = parametros.Split(spearator, StringSplitOptions.RemoveEmptyEntries);

                foreach (String s in strlist)
                {

                    string BusqeudaEstado = stringBetween(s + ".", "estado=", ".");
                    if (BusqeudaEstado.Length > 0)
                    {
                        estado = BusqeudaEstado;
                    }
                }

                if (estado.Length > 0)
                {
                    urlCifrardo.Estado = estado.ToString();
                    urlCifrardo.Result = true;
                }
                else
                {
                    urlCifrardo.IdTramite = "";
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