using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using wfiplib;

namespace wfip.promotoria
{
    public partial class esperaSeleccionPromotoria : System.Web.UI.Page
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
                // warningMessages
                Mensajes mensajes = new Mensajes();
                string mensajeShow = (new wfiplib.admCatMensajes()).mensaje("promotoria");
                if (mensajeShow.Length > 5)
                {
                    lblMessageBySystem.Visible = true;
                    lblMessageBySystem.Text = mensajeShow;
                    mensajes.MostrarMensaje(this, mensajeShow);
                }
                else
                {
                    lblMessageBySystem.Visible = false;
                    lblMessageBySystem.Text = string.Empty;
                }

                ListaPromotorias();
            }
        }

        private void ListaPromotorias()
        {
            List<wfiplib.Promotoria> lista = (new wfiplib.admCatPromotoria(ConfigurationManager.ConnectionStrings["conecta_bd"].ConnectionString)).ListaPromotorias();
            rptPromo.DataSource = lista;
            rptPromo.DataBind();
        }

        protected void rptPromo_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName.Equals("Editar"))
            {
                hdIdProm.Value = e.CommandArgument.ToString();
                wfiplib.admCatPromotoria adm = new wfiplib.admCatPromotoria(ConfigurationManager.ConnectionStrings["conecta_bd"].ConnectionString);
                wfiplib.Promotoria oDt = adm.carga(Convert.ToInt32(hdIdProm.Value));

                if (adm.AsignarUsuarioPromotoria(manejo_sesion.Credencial.Id, oDt.Id))
                {
                    manejo_sesion.Credencial.IdPromotoria = oDt.Id;
                    Response.Redirect("esperaPromotoria.aspx", true);
                }
                else
                {
                    Mensajes mensajes = new Mensajes();
                    mensajes.MostrarMensaje(this, "No se pudó asignar la promotoría seleccionada...");
                }
            }
        }

    }
}