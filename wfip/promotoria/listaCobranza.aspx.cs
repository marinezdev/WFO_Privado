using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace wfip.promotoria
{
    public partial class listaCobranza : System.Web.UI.Page
    {
        wfiplib.Concentrado manejo_sesion = new wfiplib.Concentrado();
        wfiplib.admArchivosDependencias ad = new wfiplib.admArchivosDependencias();
        wfiplib.ArchivosDependenciasAsignados ada = new wfiplib.ArchivosDependenciasAsignados();
        wfiplib.admArchivosDependenciasEstados ade = new wfiplib.admArchivosDependenciasEstados();
        wfiplib.admRolesDependencias rd = new wfiplib.admRolesDependencias();

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
                pintaMisTramites(manejo_sesion.Credencial.IdRol);
            }
        }

        private void pintaMisTramites(int rol)
        {
            if (rol == 20 && string.IsNullOrEmpty(Request["es"])) //Todos para el supervisor
                rptTramite.DataSource = ad.Seleccionar();
            else if (rol == 20 && !string.IsNullOrEmpty(Request["es"])) //Por estado para el supervisor
                rptTramite.DataSource = ad.SeleccionarPorEstadoCobertura(Request["es"], Request["cob"]);

            else if (rd.SeleccionarRolEnDependencia(rol) && string.IsNullOrEmpty(Request["es"])) //Todos los del operador/dependencia
                rptTramite.DataSource = ad.SeleccionarPorDependencia(rol);
            else if (rd.SeleccionarRolEnDependencia(rol) && !string.IsNullOrEmpty(Request["es"])) //Por estado para el operador/dependencia
                rptTramite.DataSource = ad.SeleccionarPorDependenciaEstadoCobertura(Request["es"], Request["cob"], rol);

            else if (rol == 21 || rol == 26) //Tramites por analista
                rptTramite.DataSource = ad.SeleccionarTramitesPorUsuario(manejo_sesion.Credencial.Id);
            else if (rol == 21 || rol == 26 && !string.IsNullOrEmpty(Request["es"])) //Tramites por estado por analista
                rptTramite.DataSource = ad.SeleccionarTramitesPorUsuarioPorEstadoPorCobertura(manejo_sesion.Credencial.Id, Request["es"], Request["cob"]);
            else
            {
                rptTramite.DataSource = null; //ad.SeleccionarPorEstadoCobertura(Request["es"], Request["cob"]);
                lblMensajes.Text = "No tiene permisos de acceso a la información.";
            }

            rptTramite.DataBind();

            
        }

        protected void rptTramite_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                DataRowView rowView = e.Item.DataItem as DataRowView;

                //string folio= rowView["Folio"].ToString();
                //Repeater rptArchivos = e.Item.FindControl("rptArchivos") as Repeater;
                //rptArchivos.DataSource = ad.SeleccionarArchivosDeTramite(folio);
                //rptArchivos.DataBind();

                
                string estado = rowView["Estado"].ToString();
                switch (estado)
                {
                    case "7":
                        ((Image)e.Item.FindControl("imgEstado")).ImageUrl = "~/img/bolaGrisObscuro.png";
                        break;
                    case "8":
                    case "6":
                        ((Image)e.Item.FindControl("imgEstado")).ImageUrl = "~/img/bolaVerde.png";
                        break;
                    case "5":
                        ((Image)e.Item.FindControl("imgEstado")).ImageUrl = "~/img/bolaAmarilla.png";
                        break;
                    case "4":
                        ((Image)e.Item.FindControl("imgEstado")).ImageUrl = "~/img/bolaNaranja.png";
                        break;
                    case "2":
                        ((Image)e.Item.FindControl("imgEstado")).ImageUrl = "~/img/bolaMorada.png";
                        break;
                    case "3":
                        ((Image)e.Item.FindControl("imgEstado")).ImageUrl = "~/img/bolaRoja.png";
                        break;
                    case "1":
                        ((Image)e.Item.FindControl("imgEstado")).ImageUrl = "~/img/bolaAzul.png";
                        break;
                } 
                if (manejo_sesion.Credencial.IdRol == 20 && rowView["Estado"].ToString() == "1")
                {
                    ((LinkButton)e.Item.FindControl("lnkReasignar")).Visible = true;
                }
            }
        }

        protected void rptTramite_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName.Equals("Consultar"))
            { string cob = "";
                string[] commandArgs = e.CommandArgument.ToString().Split(new char[] { ',' });
                if (commandArgs[1] == "Básica")
                    cob = "1";
                else
                    cob = "2";
                //if (commandArgs[2] != "6")
                Response.Redirect("Cobranza.aspx?folio=" + commandArgs[0] + "&cobertura=" + cob + "&es=" + commandArgs[2]);
            }
        }

        protected string Estados(string dato, string cobertura)
        {
            string valor = "";
            if (cobertura == "Básica")
            {
                switch (dato)
                {
                    case "1":
                        valor = "En Trámite";
                        break;
                    case "2":
                        valor = "Suspendido";
                        break;
                    case "3":
                        valor = "En Proceso";
                        break;
                    case "4":
                        valor = "Reenvío de Trámite";
                        break;
                    case "5":
                        valor = "En Revisión";
                        break;
                    case "6":
                        valor = "Concluído";
                        break;
                    default:
                        valor = "No Definido";
                        break;
                }
            }
            else if (cobertura == "Potenciada")
            {
                switch (dato)
                {
                    case "1":
                        valor = "En Trámite";
                        break;
                    case "2":
                        valor = "Suspendido";
                        break;
                    case "3":
                        valor = "En Proceso";
                        break;
                    case "4":
                        valor = "Reenvío de Trámite";
                        break;
                    case "5":
                        valor = "En Revisión";
                        break;
                    case "6":
                        valor = "Incompleto";
                        break;
                    case "7":
                        valor = "Carta";
                        break;
                    case "8":
                        valor = "Concluído";
                        break;
                    default:
                        valor = "No Definido";
                        break;
                }
            }
            return valor;
        }

        protected void lnkReasignar_Click(object sender, EventArgs e)
        {
            //Reasigna un trámite a otro analista si no le ha dado seguimiento el asignado actual
            //Elimina el usuario de la tabla principal de archivosdependencia
            ad.ActualizarReasignarTramiteAOtroUsuario(((System.Web.UI.WebControls.LinkButton)sender).CommandArgument);
            //elimina el usuario de la tabla archivosdependenciasasignados
            ada.Eliminar(((System.Web.UI.WebControls.LinkButton)sender).CommandArgument);
            ((System.Web.UI.WebControls.LinkButton)sender).Text = "Trámite Reasignado";
            ((System.Web.UI.WebControls.LinkButton)sender).Enabled = false;
        }
    }
}