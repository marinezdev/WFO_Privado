using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace wfip.administracion
{
    public partial class usuariosV2_WFO : System.Web.UI.Page
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
                inicializaPantalla();
        }

        private void inicializaPantalla()
        {
            (new wfiplib.admSeguridad()).registrarServiciosKO( manejo_sesion.Credencial.Id, Convert.ToInt32(Request.QueryString["Id"]));
            (new wfiplib.admSeguridad()).registraTipoTramiteKO(manejo_sesion.Credencial.Id, Convert.ToInt32(Request.QueryString["Id"]));
            (new wfiplib.admSeguridad()).registraMesaKO(manejo_sesion.Credencial.Id, Convert.ToInt32(Request.QueryString["Id"]));

            lblUsuario.Text = (new wfiplib.admSeguridad()).getData("select Nombre from usuarios where Id = " + Request.QueryString["Id"]);
            lblRol.Text = (new wfiplib.admSeguridad()).getData("SELECT NOMBRE FROM SEC_ROLES WHERE ID_ROL = (SELECT IdRol FROM usuarios WHERE Id = " + Request.QueryString["Id"] + ")");
            obtieneTiposdeServicios();
        }

        private void obtieneTiposdeServicios()
        {
            lsTiposServicios.DataSource = (new wfiplib.admSeguridad()).getTipoServicios();
            lsTiposServicios.DataTextField = "Nombre";
            lsTiposServicios.DataValueField = "Id";
            lsTiposServicios.DataBind();
        }

        private void obtieneTiposdeTramites(int IdServicio)
        {
            lblTituloTiposTramite.Text = "Tipos de Trámite <br/>[ Servicio: " + (new wfiplib.admSeguridad()).getData("SELECT Nombre FROM cat_TiposServicios WHERE Id = " + IdServicio.ToString())  + " ]";
            lsTiposTramites.DataSource = (new wfiplib.admSeguridad()).getTipoTramites(IdServicio);
            lsTiposTramites.DataTextField = "Nombre";
            lsTiposTramites.DataValueField = "Id";
            lsTiposTramites.DataBind();
        }

        private void obtieneTiposdeTramitesMov()
        {
            lblTiposMovimientos.Text = "Tipos de Movimientos";
            lsTiposTramitesMov.DataSource = (new wfiplib.admSeguridad()).getTipoTramiteMov();
            lsTiposTramitesMov.DataTextField = "Nombre";
            lsTiposTramitesMov.DataValueField = "Id";
            lsTiposTramitesMov.DataBind();
        }

        private void obtieneMesas(int IdTipoTramite)
        {
            lblTiposMovimientos.Text = "Mesas de Operación <br/>[ Tipo de Trámite: " + (new wfiplib.admSeguridad()).getData("SELECT Nombre FROM tipoTramite WHERE Id = " + IdTipoTramite.ToString()) + " ]";
            lsMesas.DataSource = (new wfiplib.admSeguridad()).getMesas(IdTipoTramite);
            lsMesas.DataTextField = "Nombre";
            lsMesas.DataValueField = "Id";
            lsMesas.DataBind();
        }

        protected void btnSigTipoServicio_Click(object sender, EventArgs a)
        {
            try
            {
                Mensajes mensajes = new Mensajes();
                bool blnError = false;
                int intIdUsuario = Convert.ToInt32(Request.QueryString["Id"]);
                int intIdServicio = -1;

                for (int i = 0; i <= (lsTiposServicios.Items.Count - 1); i++)
                {
                    if (lsTiposServicios.Items[i].Selected)
                    {
                        intIdServicio = Convert.ToInt32(lsTiposServicios.Items[i].Value);
                        if (!(new wfiplib.admSeguridad()).registraServicios(manejo_sesion.Credencial.Id, intIdUsuario, intIdServicio))
                            blnError = true;
                    }                        
                }

                if (!blnError)
                {
                    pnlTiposServicios.Visible = false;
                    pnlTiposTramite.Visible = true;
                    pnlTiposTramiteMov.Visible = false;
                    intIdServicio = (new wfiplib.admSeguridad()).getIdServicio(manejo_sesion.Credencial.Id, intIdUsuario);
                    if (intIdServicio != -1)
                        obtieneTiposdeTramites(intIdServicio);
                    else
                        mensajes.MostrarMensaje(this, "No se encontró el servicio solicitado...");
                }
                else
                {
                    (new wfiplib.admSeguridad()).registrarServiciosKO(manejo_sesion.Credencial.Id, intIdUsuario);
                    mensajes.MostrarMensaje(this, "No se pudó registrar el servicio. Por favor intente de Nuevo.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }

        protected void btnSigTipoTramite_Click(object sender, EventArgs e)
        {
            try
            {
                Mensajes mensajes = new Mensajes();
                int intIdUsuario = Convert.ToInt32(Request.QueryString["Id"]);
                int intIdServicio = -1;
                int intIdTipoTramite = -1;
                bool blnError = false;

                intIdServicio = (new wfiplib.admSeguridad()).getIdServicio(manejo_sesion.Credencial.Id, intIdUsuario);
                if (intIdServicio != -1)
                {
                    for (int i = 0; i <= (lsTiposTramites.Items.Count - 1); i++)
                    {
                        if (lsTiposTramites.Items[i].Selected)
                        {
                            intIdTipoTramite = Convert.ToInt32(lsTiposTramites.Items[i].Value);
                            if (!(new wfiplib.admSeguridad()).registraTipoTramite(manejo_sesion.Credencial.Id, intIdUsuario, intIdServicio, intIdTipoTramite))
                                blnError = true;
                        }
                    }

                    if (!blnError)
                    {
                        (new wfiplib.admSeguridad()).registraServiciosProcesado(manejo_sesion.Credencial.Id, intIdUsuario, intIdServicio);
                        intIdServicio = (new wfiplib.admSeguridad()).getIdServicio(manejo_sesion.Credencial.Id, intIdUsuario);
                        if (intIdServicio != -1)
                            obtieneTiposdeTramites(intIdServicio);
                        else
                        {
                            pnlTiposServicios.Visible = false;
                            pnlTiposTramite.Visible = false;
                            pnlTiposTramiteMov.Visible = true;
                            obtieneTiposdeTramitesMov();

                            intIdTipoTramite = (new wfiplib.admSeguridad()).getIdTipoTramite(manejo_sesion.Credencial.Id, intIdUsuario);
                            if (intIdTipoTramite != -1)
                                obtieneMesas(intIdTipoTramite);
                            else
                                mensajes.MostrarMensaje(this, "No se encontraron mesas para el tipo de Trámite asignado.");
                        }
                    }
                    else
                    {
                        (new wfiplib.admSeguridad()).registraTipoTramiteKO(manejo_sesion.Credencial.Id, intIdUsuario);
                        mensajes.MostrarMensaje(this, "No se pudó registrar el Tipo de Trámite. Por favor consulte a soporte técnico.");
                    }
                }
                else
                    mensajes.MostrarMensaje(this, "No se encontró el servicio solicitado...");
                    
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }

        protected void btnGuardarPermisos_Click(object sender, EventArgs e)
        {
            try
            {
                Mensajes mensajes = new Mensajes();
                int intIdUsuario = Convert.ToInt32(Request.QueryString["Id"]);
                int intIdTipoTramite = -1;
                int intIdMesa = -1;
                int intIdTipoMovimiento = -1;
                bool blnError = false;

                intIdTipoTramite = (new wfiplib.admSeguridad()).getIdTipoTramite(manejo_sesion.Credencial.Id, intIdUsuario);
                if (intIdTipoTramite != -1)
                {
                    for (int i = 0; i <= (lsMesas.Items.Count - 1); i++)
                    {
                        if (lsMesas.Items[i].Selected)
                        {
                            intIdMesa = Convert.ToInt32(lsMesas.Items[i].Value);
                            for (int j = 0; j <= (lsTiposTramitesMov.Items.Count - 1); j++)
                            {
                                if (lsTiposTramitesMov.Items[j].Selected)
                                {
                                    intIdTipoMovimiento = Convert.ToInt32(lsTiposTramitesMov.Items[j].Value);
                                    if (!(new wfiplib.admSeguridad()).registraMesa(manejo_sesion.Credencial.Id, intIdUsuario, intIdTipoTramite, intIdMesa, intIdTipoMovimiento))
                                        blnError = true;
                                }
                            }
                        }
                    }

                    if (!blnError)
                    {
                        (new wfiplib.admSeguridad()).registraTipoTramiteProcesado(manejo_sesion.Credencial.Id, intIdUsuario, intIdTipoTramite);
                        intIdTipoTramite = (new wfiplib.admSeguridad()).getIdTipoTramite(manejo_sesion.Credencial.Id, intIdUsuario);
                        if (intIdTipoTramite != -1)
                        {
                            obtieneTiposdeTramitesMov();
                            obtieneMesas(intIdTipoTramite);
                        }
                        else
                        {
                            pnlTiposServicios.Visible = false;
                            pnlTiposTramite.Visible = false;
                            pnlTiposTramiteMov.Visible = false;

                            if ((new wfiplib.admSeguridad()).CreateProfile(manejo_sesion.Credencial.Id, intIdUsuario))
                                mensajes.MostrarMensaje(this, "El perfil para el usuario se creó satisfactoriamente.");
                            else
                                mensajes.MostrarMensaje(this, "No se creo el perfil para el usuario. Por favor pongase en contacto con su soporte técnico.");
                        }
                    }
                    else
                    {
                        (new wfiplib.admSeguridad()).registraTipoTramiteKO(manejo_sesion.Credencial.Id, intIdUsuario);
                        mensajes.MostrarMensaje(this, "No se pudó registrar el Tipo de Trámite. Por favor consulte a soporte técnico.");
                    }
                }
                else
                    mensajes.MostrarMensaje(this, "No se encontraron mesas para el tipo de Trámite asignado.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }
    }
}