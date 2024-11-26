using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace wfip.supervision
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
            {
                Propiedades.UrlCifrardo urlCifrardo = ConsultaParametros();

                if (urlCifrardo.Result)
                {
                    inicializaPantalla();
                }
                

            }
                
        }

        private void inicializaPantalla()
        {
            Propiedades.UrlCifrardo urlCifrardo = ConsultaParametros();

            (new wfiplib.admSeguridad()).registrarServiciosKO(manejo_sesion.Credencial.Id, Convert.ToInt32(urlCifrardo.IdUsuario));
            (new wfiplib.admSeguridad()).registraTipoTramiteKO(manejo_sesion.Credencial.Id, Convert.ToInt32(urlCifrardo.IdUsuario));
            (new wfiplib.admSeguridad()).registraMesaKO(manejo_sesion.Credencial.Id, Convert.ToInt32(urlCifrardo.IdUsuario));

            lblUsuario.Text = (new wfiplib.admSeguridad()).getData("select Nombre from usuarios where Id = " + urlCifrardo.IdUsuario);
            lblRol.Text = (new wfiplib.admSeguridad()).getData("SELECT NOMBRE FROM SEC_ROLES WHERE ID_ROL = (SELECT IdRol FROM usuarios WHERE Id = " + urlCifrardo.IdUsuario + ")");
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
            lblTituloTiposTramite.Text = "Tipos de Trámite <br/>[ Servicio: " + (new wfiplib.admSeguridad()).getData("SELECT Nombre FROM cat_TiposServicios WHERE Id = " + IdServicio.ToString()) + " ]";
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
                Propiedades.UrlCifrardo urlCifrardo = ConsultaParametros();

                bool blnError = false;
                bool blnSelected = false;
                int intIdUsuario = Convert.ToInt32(urlCifrardo.IdUsuario);
                int intIdServicio = -1;

                for (int i = 0; i <= (lsTiposServicios.Items.Count - 1); i++)
                {
                    if (lsTiposServicios.Items[i].Selected)
                    {
                        blnSelected = true;
                        intIdServicio = Convert.ToInt32(lsTiposServicios.Items[i].Value);
                        if (!(new wfiplib.admSeguridad()).registraServicios(manejo_sesion.Credencial.Id, intIdUsuario, intIdServicio))
                            blnError = true;
                    }
                }

                if (blnSelected)
                {
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
                else
                {
                    mensajes.MostrarMensaje(this, "Por favor seleccione un servicio...");
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
                Propiedades.UrlCifrardo urlCifrardo = ConsultaParametros();

                int intIdUsuario = Convert.ToInt32(urlCifrardo.IdUsuario);
                int intIdServicio = -1;
                int intIdTipoTramite = -1;
                bool blnError = false;
                bool blnSelected = false;

                intIdServicio = (new wfiplib.admSeguridad()).getIdServicio(manejo_sesion.Credencial.Id, intIdUsuario);
                if (intIdServicio != -1)
                {
                    for (int i = 0; i <= (lsTiposTramites.Items.Count - 1); i++)
                    {
                        if (lsTiposTramites.Items[i].Selected)
                        {
                            blnSelected = true;
                            intIdTipoTramite = Convert.ToInt32(lsTiposTramites.Items[i].Value);
                            if (!(new wfiplib.admSeguridad()).registraTipoTramite(manejo_sesion.Credencial.Id, intIdUsuario, intIdServicio, intIdTipoTramite))
                                blnError = true;
                        }
                    }

                    if (blnSelected)
                    {
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
                                {
                                    obtieneMesas(intIdTipoTramite);
                                }
                                else
                                {
                                    mensajes.MostrarMensaje(this, "No se encontraron mesas para el tipo de Trámite asignado.");
                                }
                            }
                        }
                        else
                        {
                            (new wfiplib.admSeguridad()).registraTipoTramiteKO(manejo_sesion.Credencial.Id, intIdUsuario);
                            mensajes.MostrarMensaje(this, "No se pudó registrar el Tipo de Trámite. Por favor consulte a soporte técnico.");
                        }
                    }
                    else
                    {
                        mensajes.MostrarMensaje(this, "Por favor seleccione al menos un tipo de trámite....");
                    }
                }
                else
                {
                    mensajes.MostrarMensaje(this, "No se encontró el servicio solicitado...");
                }

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
                Propiedades.UrlCifrardo urlCifrardo = ConsultaParametros();
                int intIdUsuario = Convert.ToInt32(urlCifrardo.IdUsuario);
                int intIdTipoTramite = -1;
                int intIdMesa = -1;
                int intIdTipoMovimiento = -1;
                bool blnError = false;
                bool blnSelectedTipoTramite = false;
                bool blnSelectedMesa = false;

                intIdTipoTramite = (new wfiplib.admSeguridad()).getIdTipoTramite(manejo_sesion.Credencial.Id, intIdUsuario);
                if (intIdTipoTramite != -1)
                {
                    for (int i = 0; i <= (lsMesas.Items.Count - 1); i++)
                    {
                        if (lsMesas.Items[i].Selected)
                        {
                            blnSelectedMesa = true;
                            intIdMesa = Convert.ToInt32(lsMesas.Items[i].Value);
                            for (int j = 0; j <= (lsTiposTramitesMov.Items.Count - 1); j++)
                            {
                                if (lsTiposTramitesMov.Items[j].Selected)
                                {
                                    blnSelectedTipoTramite = true;
                                    intIdTipoMovimiento = Convert.ToInt32(lsTiposTramitesMov.Items[j].Value);
                                    if (!(new wfiplib.admSeguridad()).registraMesa(manejo_sesion.Credencial.Id, intIdUsuario, intIdTipoTramite, intIdMesa, intIdTipoMovimiento))
                                    {
                                        blnError = true;
                                    }
                                }
                            }
                        }
                    }

                    if (blnSelectedTipoTramite && blnSelectedMesa)
                    {
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
                                {
                                    mensajes.MostrarMensaje(this, "El perfil para el usuario se creó satisfactoriamente.");
                                }
                                else
                                {
                                    mensajes.MostrarMensaje(this, "No se creo el perfil para el usuario. Por favor pongase en contacto con su soporte técnico.");
                                }
                            }
                        }
                        else
                        {
                            (new wfiplib.admSeguridad()).registraTipoTramiteKO(manejo_sesion.Credencial.Id, intIdUsuario);
                            mensajes.MostrarMensaje(this, "No se pudó registrar el Tipo de Trámite. Por favor consulte a soporte técnico.");
                        }
                    }
                    else
                    {
                        mensajes.MostrarMensaje(this, "Debe seleccionar al menos un tipo de movimiento y al menos una mesa...");
                    }
                }
                else
                {
                    mensajes.MostrarMensaje(this, "No se encontraron mesas para el tipo de Trámite asignado.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }

        private Propiedades.UrlCifrardo ConsultaParametros()
        {
            Propiedades.UrlCifrardo urlCifrardo = new Propiedades.UrlCifrardo();
            try
            {
                string parametros = (new Application.Operacion.UrlCifrardo()).Decrypt(Request.QueryString["data"].ToString());
                string idusuario = "";

                String[] spearator = { "," };
                String[] strlist = parametros.Split(spearator, StringSplitOptions.RemoveEmptyEntries);

                foreach (String s in strlist)
                {

                    string BusquedaIdUsuario = stringBetween(s + ".", "Id=", ".");
                    if (BusquedaIdUsuario.Length > 0)
                    {
                        idusuario = BusquedaIdUsuario;
                    }
                }

                if (idusuario.Length > 0)
                {
                    urlCifrardo.IdUsuario = idusuario.ToString();
                    urlCifrardo.Result = true;
                }
                else
                {
                    urlCifrardo.IdUsuario = "";
                    urlCifrardo.Result = false;
                }
            }
            catch (Exception)
            {
                urlCifrardo.Result = false;
            }

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