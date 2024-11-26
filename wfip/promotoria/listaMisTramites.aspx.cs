using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DevExpress.XtraPrinting;

namespace wfip.promotoria
{
    public partial class listaMisTramites : System.Web.UI.Page
    {
        wfiplib.Concentrado manejo_sesion = new wfiplib.Concentrado();

        protected void Page_Init(object sender, EventArgs e)
        {
            if (Session["credencial"] == null)
                Response.Redirect("~/Default.aspx");
            manejo_sesion = (wfiplib.Concentrado)Session["credencial"];
        }

        private void showMessage(string Mensaje)
        {
            ScriptManager.RegisterStartupScript(this.mensajesInformativos, typeof(string), "Alert", "alert('" + Mensaje + "');", true);
            // Response.Write("<script language=javascript>alert('" + Mensaje + "')</script>");
        }

        protected void Page_Load(object sender, EventArgs e)
        {

            pintaMisTramitesExcel();

            if (!IsPostBack)
            {
                pintaMisTramites();

                int showMensaje = Convert.ToInt32(Request.QueryString["msg"]);
                if (showMensaje == 1)
                {
                    string strFolio = Convert.ToString(Request.QueryString["folio"]);
                    showMessage("Su tramite ha sido registrado correctamente con el folio: " + strFolio);
                }
                pintaMisTramites();
                Session.Contents.Remove("nota");
            }
        }

        private void pintaMisTramitesExcel()
        {
            // wfiplib.credencial oCredencial = (wfiplib.credencial)Session["credencial"];
            if (manejo_sesion.Credencial.Modulo == wfiplib.E_Modulo.Promotoria)
            {
                if (manejo_sesion.Credencial.IdPromotoria > 0)
                {
                    DataTable lstTramites = (new wfiplib.admTramite()).daTablaTramitesPromotoria(manejo_sesion.Credencial.IdPromotoria);
                    DataTable lstTramitesE = (new wfiplib.admTramite()).daTablaTramitesPromotoriaExcel(manejo_sesion.Credencial.IdPromotoria);
                    dvgdListaTramites.DataSource = lstTramitesE;
                    dvgdListaTramites.DataBind();
                }
            }
        }

        private void pintaMisTramites()
        {
            // wfiplib.credencial oCredencial = (wfiplib.credencial)Session["credencial"];
            if (manejo_sesion.Credencial.Modulo == wfiplib.E_Modulo.Promotoria)
            {
                if (manejo_sesion.Credencial.IdPromotoria > 0)
                {
                    DataTable lstTramites = (new wfiplib.admTramite()).daTablaTramitesPromotoria(manejo_sesion.Credencial.IdPromotoria);
                    rptTramite.DataSource = lstTramites;
                    rptTramite.DataBind();
                }
            }
        }

        protected void rptTramite_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                //    wfiplib.E_EstadoTramite Estado = (wfiplib.E_EstadoTramite)((DataRowView)(e.Item.DataItem))["Estado"];
                //    switch (Estado)
                //    {
                //        case wfiplib.E_EstadoTramite.Ejecucion:
                //            ((Image)e.Item.FindControl("imgEstado")).ImageUrl = "~/img/bolaVerde.png";
                //            break;
                //        case wfiplib.E_EstadoTramite.Rechazo:
                //            ((Image)e.Item.FindControl("imgEstado")).ImageUrl = "~/img/bolaRoja.png";
                //            break;
                //        case wfiplib.E_EstadoTramite.Proceso:
                //            ((Image)e.Item.FindControl("imgEstado")).ImageUrl = "~/img/bolaAzul.png";
                //            break;
                //        case wfiplib.E_EstadoTramite.Hold:
                //            ((Image)e.Item.FindControl("imgEstado")).ImageUrl = "~/img/bolaAmarilla.png";
                //            break;
                //        case wfiplib.E_EstadoTramite.Suspendido:
                //            ((Image)e.Item.FindControl("imgEstado")).ImageUrl = "~/img/bolaNaranja.png";
                //            break;
                //        case wfiplib.E_EstadoTramite.PCI:
                //            ((Image)e.Item.FindControl("imgEstado")).ImageUrl = "~/img/bolaNaranja.png";
                //            break;
                //        case wfiplib.E_EstadoTramite.RevPromotoria:
                //            ((Image)e.Item.FindControl("imgEstado")).ImageUrl = "~/img/bolaMorada.png";
                //            break;
                //        case wfiplib.E_EstadoTramite.InfoCitaMedica:
                //            ((Image)e.Item.FindControl("imgEstado")).ImageUrl = "~/img/bolaMorada.png";
                //            break;
                //        case wfiplib.E_EstadoTramite.PromotoriaCancela:
                //            ((Image)e.Item.FindControl("imgEstado")).ImageUrl = "~/img/bolaRoja.png";
                //            //((ImageButton)e.Item.FindControl("imgBtnEliminar")).Visible = false;
                //            break;
                //        default:
                //            ((Image)e.Item.FindControl("imgEstado")).ImageUrl = "~/img/bolaGris.png";
                //            break;
                //    }
            }
        }

        protected void rptTramite_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            //if (e.CommandName.Equals("Eliminar"))
            //{
            //    wfiplib.tramiteP oTramite = (new wfiplib.admTramite()).carga(Convert.ToInt32(e.CommandArgument));
            //    if ((new wfiplib.admTramite()).CancelarTramite(oTramite, manejo_sesion.Credencial))
            //    {
            //        showMessage("El Trámite se Canceló Correctamente.");
            //        Response.Redirect("esperaPromotoria.aspx", true);
            //    }
            //    else
            //        showMessage("No se pudó cancelar el Trámite.");
            //}

            if (e.CommandName.Equals("Consultar"))
            {
                wfiplib.tramiteP oTramite = (new wfiplib.admTramite()).carga(Convert.ToInt32(e.CommandArgument));
                if (oTramite.Estado == wfiplib.E_EstadoTramite.Ejecucion)
                    Response.Redirect(EncripParametros("Id=" + e.CommandArgument.ToString(), "pmTramiteEjctd.aspx").URL, true);
                    //Response.Redirect("pmTramiteEjctd.aspx?Id=" + e.CommandArgument.ToString());
                else if (oTramite.IdTipoTramite == wfiplib.E_TipoTramite.InsPrivadoServicios)
                    Response.Redirect(EncripParametros("Id=" + e.CommandArgument.ToString(), "pmconsultaTramiteIns.aspx").URL, true);
                    //Response.Redirect("pmconsultaTramiteIns.aspx?Id=" + e.CommandArgument.ToString());
                else
                    Response.Redirect(EncripParametros("Id=" + e.CommandArgument.ToString(), "pmconsultaTramite.aspx").URL, true);
                    //Response.Redirect("pmconsultaTramite.aspx?Id=" + e.CommandArgument.ToString());
            }
        }
        protected void lnkExportar_Click(object sender, EventArgs e)
        {
            dvgdListaTramites.ExportXlsxToResponse();
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