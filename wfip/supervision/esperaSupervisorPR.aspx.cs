using DevExpress.Export;
using DevExpress.Web;
using DevExpress.XtraPrinting;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace wfip.supervision
{
    public partial class esperaSupervisorPR : System.Web.UI.Page
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
            if (!IsPostBack)
            {
                // warningMessages
                string mensajeShow = (new wfiplib.admCatMensajes()).mensaje("supervisor");
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

                MuestraMesas();
            }
        }

        protected void MuestraMesas()
        {
            DataTable dt = (new wfiplib.admMesa()).MesasUsuarios_Conetados_Mesas(manejo_sesion.Credencial.Id);
            string Mesas = "";

            foreach (DataRow row in dt.Rows)
            {
                string Encrypt = (new Application.Operacion.UrlCifrardo()).Encrypt("Id=" + row["Mesa"].ToString());

                //
                Mesas += "<div class='control-label col-md-2 col-sm-2 col-xs-6'>" +
                            "<div class='x_panel text-center'>" +
                                //"<a href = 'sprMapaSupervisorR.aspx?Id=" + row["Mesa"].ToString() + "' >" +
                                "<a href = 'sprMapaSupervisorR.aspx?data=" + Encrypt + "' >" +
                                    "<img src='" + row["Imagen"].ToString() + "' class='img-thumbnail'/>" +
                                    "<div class='form-group text-center'>" +
                                        "<h5><small style='color: #003657;'><strong>" + row["Mesa"].ToString() + "</strong></small></h5>" +
                                        "<h5><small style='color: #003657;'>Usuarios conectados: <strong>" + row["Usuarios"].ToString() + "</strong></small></h5>" +
                                        "<h5><small style='color: #003657;'>Tramites: <strong>" + row["Tramites"].ToString() + "</strong></small></h5>" +
                                        "<hr />" +
                                    "</div>" +
                                "</a>" +
                            "</div>" +
                        "</div>";
            }

            MesasLiteral.Text = Mesas;

        }


        protected void LinkUsuarios_Click(object sender, EventArgs e)
        {
            Response.Redirect("UsuariosAtencion.aspx", true);
        }

       

    }
}