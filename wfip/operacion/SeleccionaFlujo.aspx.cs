using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace wfip.operacion
{
    public partial class SeleccionaFlujo : System.Web.UI.Page
    {
        wfiplib.Concentrado manejo_sesion = new wfiplib.Concentrado();

        #region "Eventos de la Página Web"
        /// <summary>
        /// Inicialización de la Página.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Init(object sender, EventArgs e)
        {
            if (Session["credencial"] == null)
                Response.Redirect("../Default.aspx", true);
            manejo_sesion = (wfiplib.Concentrado)Session["credencial"];
        }


        /// <summary>
        /// Carga Inicial de la Página
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            Mensajes mensajes = new Mensajes();
            if (!IsPostBack)
            {
                // warningMessages
                string mensajeShow = (new wfiplib.admCatMensajes()).mensaje("operador");
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

                List<wfiplib.TipoTramite> TiposTramite = (new wfiplib.admSeguridad()).getAssignTiposTramites(manejo_sesion.Credencial.IdPerfil);
                switch (TiposTramite.Count)
                {
                    case 0:
                        mensajes.MostrarMensaje(this, "El usuario NO tiene configurado ningún Tipo de Trámite para atender, favor de validar con el Administrador del Sistema.");
                        break;

                    case 1:
                        Response.Redirect(EncripParametros("flujo=" + TiposTramite[0].IdFlujo.ToString(), "esperaOperacion.aspx").URL, true);
                        break;

                    default:
                        rpt_Flujos.DataSource = TiposTramite;
                        rpt_Flujos.DataBind();
                        rpt_Flujos.Visible = true;
                        break;
                }
            }
        }
        #endregion

        #region "Eventos de Controles Web"

        /// <summary>
        /// Selección del Flujo sobre el cual se desea trabajar
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        protected void rpt_Flujos_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName.Equals("seleccionaFlujo"))
            {
                Response.Redirect(EncripParametros("flujo=" + e.CommandArgument.ToString(), "esperaOperacion.aspx").URL, true);
            }
        }

        private Propiedades.UrlCifrardo EncripParametros(string parametros, string Direccion)
        {
            Propiedades.UrlCifrardo urlCifrardo = new Propiedades.UrlCifrardo();
            string Encrypt = (new Application.Operacion.UrlCifrardo()).Encrypt(parametros);
            urlCifrardo.URL = Direccion + "?data=" + Encrypt;
            return urlCifrardo;
        }
        #endregion
    }
}