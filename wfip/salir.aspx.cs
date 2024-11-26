using System;
using System.Web;

namespace wfip
{
    public partial class salir : System.Web.UI.Page
    {
        wfiplib.Concentrado manejo_sesion = new wfiplib.Concentrado();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["credencial"] != null)
            {
                if (Session["TramiteAutomatico"] != null)
                {
                    Session["TramiteAutomatico"] = false;
                }

                if (Session["tramiteActualId"] != null && Session["mesaActual"] != null)
                {
                    try
                    {
                        int IdTramite = Convert.ToInt32(Session["tramiteActualId"]);
                        int IdMesa = Convert.ToInt32(Session["mesaActual"]);

                        if ((new wfiplib.admTramiteMesa()).cambiaEstado(IdTramite, IdMesa, wfiplib.E_EstadoMesa.Pausa, "Trámite en pausar por botón salir.", "Trámite en pausar por botón salir."))
                        {
                            registraBitacora(IdTramite, IdMesa);
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }

                //wfiplib.credencial credencial = (wfiplib.credencial)Session["credencial"];
                manejo_sesion = (wfiplib.Concentrado)Session["credencial"];
                // DESCONECTA AL USUARIO, LUNPIANDO LOS ATRIBUTOS QUE DE Conectado = 'N', ConectadoFecha = NULL

                // (new wfiplib.admCredencial()).desconecta(manejo_sesion.Credencial.Id, Session.SessionID);
            }


#if DEBUG
            (new wfiplib.admCredencial()).desconecta(manejo_sesion.Credencial.Id, Session.SessionID);
            Response.Redirect("Default.aspx", true);
#else

            ////////////////////////////////////////////////////////////////////////////////////////////////
            ////////////////////////////////////////////////////////////////////////////////////////////////
            /////////////////////// COMENTAR PARA ENTRAR POR LOGIN NORMAL //////////////////////////////////
            ////////////////////////////////////////////////////////////////////////////////////////////////

            Session.RemoveAll();
            //Eliminar una sola
            Session.Remove("credencial");
            Session["credencial"] = null;
            //Eliminar todas las variables
            Session.Contents.RemoveAll();

            Session.Clear();
            System.Web.Security.FormsAuthentication.SignOut();

            /* REALIZA LA SALIDA DEL SISTEMA APARTIR DEL PARAMETRO
                1 SALIDA A CAMBIO DE APLIACION, 
                2 SALIDA POR CIERRE DE SESION TOTAL. 

                LA SALIDA SIN PARAMETROS POR URL, NOS REDIRECCIONARA AL CAMBIO DE APLIACION, ENCASO DE NO TENER UN TOKEN, SALDRA AL LOGIN MAESTRO.
                OBTENER LA RUTA ORGIEN PARA REDIRECCIONAR AL LOGEO
             * */
            string host = HttpContext.Current.Request.Url.Host;

            // VALIDA LA VARIABLE DE SESION PARA REGISTRAR SU SALIDA
            if (manejo_sesion != null)
            {
                // CAMBIAR LA RUTA PARA INGRESAR CON WWWW O SIN ELLA. (TOMAR LA RUTA ORIGUEN)
                // VALIDA EL PARAMETRO DE OPERACION.
                if (Request.Params["salida"] != null)
                {
                    string _Token = manejo_sesion.Token;

                    string op = Request.Params["salida"].ToString().Trim();
                    if (op == "1")
                    {   // ENVIA EL TOKEN REGISTRADO EN SUS SECION, PARA SER AUTENTIFICADO EN EL LOGIN MAESTRO.
                        // Response.Redirect("http://localhost:51634/Procesos/Aplicaciones/Default.aspx?numlife=" + _Token);
                        Response.Redirect("https://" + host + "/MetLife/Default.aspx?numlife=" + _Token);
                    }
                    else if (op == "2")
                    {
                        //CIERRA SESION A TRAVES DEL WEB SERVICE, PROPORCIONA LLAVE PARA CERRAR SESION EN EL LOGIN PRINCIPAL.
                        try
                        {
                            wfiplib.CredencialesWS datos = (new wfiplib.admAutentificar()).CierreSesion(_Token);
                            Response.Redirect("https://" + host + "/MetLife/Default.aspx?numlife=" + _Token);
                        }
                        catch (Exception)
                        {
                            Response.Redirect("https://" + host + "/MetLife/");
                        }
                    }
                    else
                    {
                        Response.Redirect("https://" + host + "/MetLife/");
                    }
                }
                else
                {
                    Response.Redirect("https://" + host + "/MetLife/");
                }
            }
            else
            {
                Response.Redirect("https://" + host + "/MetLife/");
            }
#endif
            ////////////////////////////////////////////////////////////////////////////////////////////////
            ////////////////////////////////////////////////////////////////////////////////////////////////
        }

        private void registraBitacora(int pIdTramite, int pIdMesa)
        {
            try
            {
                wfiplib.tramiteMesa oTramiteMesa = (new wfiplib.admTramiteMesa()).carga(pIdTramite, pIdMesa);
                wfiplib.bitacora oBitacora = new wfiplib.bitacora();
                oBitacora.IdFlujo = oTramiteMesa.IdFlujo;
                oBitacora.IdTipoTramite = oTramiteMesa.IdTipoTramite;
                oBitacora.IdTramite = oTramiteMesa.IdTramite;
                oBitacora.IdMesa = oTramiteMesa.IdMesa;
                oBitacora.Usuario = oTramiteMesa.UsuarioNombre;
                oBitacora.IdUsuario = oTramiteMesa.IdUsuario;
                oBitacora.FechaInicio = oTramiteMesa.FechaInicio;
                oBitacora.Estado = oTramiteMesa.Estado;
                oBitacora.Observacion = oTramiteMesa.ObservacionPublica;
                oBitacora.ObservacionPrivada = oTramiteMesa.ObservacionPrivada;
                (new wfiplib.admBitacora()).Nuevo(oBitacora);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }


}