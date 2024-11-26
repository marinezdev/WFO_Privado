using System;
using System.Configuration;
using System.Web;
using System.Web.UI;

namespace wfip
{
    public partial class Default : System.Web.UI.Page
    {
        public static int contador = 0;
        wfiplib.Concentrado manejo_sesion = new wfiplib.Concentrado();
        wfiplib.Correo correo = new wfiplib.Correo();
        Mensajes mensajes = new Mensajes();

        protected void Page_Load(object sender, EventArgs e)
        {
            Session.RemoveAll();
            //Eliminar una sola
            Session.Remove("credencial");
            Session["credencial"] = null;
            //Eliminar todas las variables
            Session.Contents.RemoveAll();

#if DEBUG   
            // rutas de archivos que hay que verificar
            // public string CarpetaArchivada = @"F:\files\WFOPRIVADO\expedientes\";            // valor en produccion
            // public string CarpetaArchivada = @"F:\files\WFOPrivado\insumos\";                // valor en produccion

            // valores establecidos para las pruebas de conversiones
            // public string CarpetaArchivada = @"C:\inetpub\wwwroot\ImssPortal\uploadInsumosOK\";
            // public string CarpetaArchivada = @"C:\inetpub\wwwroot\ImssPortal\uploadDocumentsOK\";

            //Response.Write("NOS ENCONTRAMOS EN MODO DEPURACIÓN...");

            //txUsuario.Text = "marisol.flores.op";
            //txUsuario.Text = "marisol.flores.super";
            //txUsuario.Text = "gerardo.hernandez.super";
            //txUsuario.Text = "victor.cordero.super";
            //txUsuario.Text = "julio.valverde.s";
            //txUsuario.Text = "julio.valverde.o";
            //txUsuario.Text = "julio.valverde.p";
            //txUsuario.Text = "pablo.ruiz.op";
            txUsuario.Text = "gerardo.hernandez.op";
            //txUsuario.Text = "sandra.alvarezm.o";
            //txUsuario.Text = "maribel.rico.00F";
            //txUsuario.Text = "angelica.fino.681";
            //txUsuario.Text = "ana.velez.123";
            //txUsuario.Text = "veronica.martinez";  // consulta



#else
            ////////////////////////////////////////////////////////////////////////////////////////////////
            ////////////////////////////////////////////////////////////////////////////////////////////////
            /////////////////////// COMENTAR PARA ENTRAR POR LOGIN NORMAL //////////////////////////////////
            ////////////////////////////////////////////////////////////////////////////////////////////////

            //PARAMETROS DE AUTENTITFIACION POR WEB SERVICE EN EL CASO DE NO EXISTIR REDIRIGIR AL LISTADO DE APLIACIONES.
            if (Request.Params["numlife"] != null && Request.Params["us"] != null && Request.Params["ap"] != null)
            {
                // VALIDACION POR WEB SERVICE. - RETORNARA EL TOKEN Y EL NOMBRE DEL USUARIO.
                string token = Request.Params["numlife"].ToString();
                int IdUsuario = Convert.ToInt32(Request.Params["us"]);
                int IdAplicacion = Convert.ToInt32(Request.Params["ap"]);

                wfiplib.CredencialesWS datos = (new wfiplib.admAutentificar()).Autentificar(IdUsuario, IdAplicacion, token);

                if (datos.Token != "0")
                {
                    manejo_sesion.Inicializar();
                    manejo_sesion.ConfiguracionGeneral = (new wfiplib.admConfiguracionGeneral()).ListaconfiguracionSistema();   //Carga configuración sistema

                    wfiplib.admCredencial adm = new wfiplib.admCredencial();
                    manejo_sesion.Credencial = adm.carga(Convert.ToInt32(datos.Id));  //Carga detalle usuario
                    manejo_sesion.Token = datos.Token;
                    manejo_sesion.Promotoria.Nombre = (new wfiplib.admCatPromotoria(ConfigurationManager.ConnectionStrings["conecta_bd"].ConnectionString)).daNombre(manejo_sesion.Credencial.IdPromotoria); //Nombre Promotoría
                    manejo_sesion.Permisos = (new wfiplib.admPermisos()).PermisosObtenerPorRol(manejo_sesion.Credencial.IdRol); //Permisos para paginas

                    AutenticarPorToken(adm, manejo_sesion.Credencial);
                }
                else
                {
                    string host = HttpContext.Current.Request.Url.Host;
                    Response.Redirect("https://" + host + "/MetLife/");
                }
            }
            else
            {
                string host = HttpContext.Current.Request.Url.Host;
                Response.Redirect("https://"+ host+ "/MetLife/");
            }
#endif
            ////////////////////////////////////////////////////////////////////////////////////////////////
            ///////////////////////////////////////////////////////////////////////////////////////////////*/
        }


        private void AutenticarPorToken(wfiplib.admCredencial pAdm, wfiplib.credencial pCredencial)
        {
            manejo_sesion.Inicializar();
            manejo_sesion.Credencial = pCredencial;
            manejo_sesion.ConfiguracionGeneral = (new wfiplib.admConfiguracionGeneral()).ListaconfiguracionSistema();

            Session["credencial"] = manejo_sesion;

            System.Web.Security.FormsAuthentication.SetAuthCookie(pCredencial.Usuario, false);
            string paginaEspera = "salir.aspx";

            if (pCredencial.Aplicacion == wfiplib.E_Aplicacion.AdminSis)
            {
                if (pCredencial.Modulo == wfiplib.E_Modulo.AdmSys)
                    paginaEspera = "administracion/admsysEspera.aspx";
            }
            else if (pCredencial.Aplicacion == wfiplib.E_Aplicacion.Fto)
            {
                if (pCredencial.Modulo == wfiplib.E_Modulo.Promotoria)
                {
                    if (pCredencial.IdRol == (int)wfiplib.E_AppRoles.SuperPromotoria)
                    {
                        paginaEspera = "promotoria/esperaSeleccionPromotoria.aspx";
                    }
                    else
                    {
                        paginaEspera = "promotoria/esperaPromotoria.aspx";
                    }
                }

                if (pCredencial.Modulo == wfiplib.E_Modulo.Operacion && pCredencial.Grupo == wfiplib.E_CredencialGrupo.Operador)
                    paginaEspera = "operacion/esperaOperacion.aspx";
                if (pCredencial.Modulo == wfiplib.E_Modulo.Operacion && pCredencial.Grupo == wfiplib.E_CredencialGrupo.Supervisor)
                    /*paginaEspera = "supervision/esperaSupervisorP.aspx";*/
                    paginaEspera = "supervision/esperaSupervisorPR.aspx";
                if (pCredencial.Modulo == wfiplib.E_Modulo.Monitor && pCredencial.Grupo == wfiplib.E_CredencialGrupo.Operador)
                    paginaEspera = "tablero/MonitorGeneral.aspx";
                if (pCredencial.Modulo == wfiplib.E_Modulo.MesaAyuda)
                    paginaEspera = "MesaAyuda/maGestionIncidencias.aspx";
            }
            else if (pCredencial.Aplicacion == wfiplib.E_Aplicacion.PolizaGrupal)
                paginaEspera = "Grupal/GpoEspera.aspx";
            else
            {
                switch (pCredencial.IdRol)
                {
                    case (int)wfiplib.E_AppRoles.Administrador:
                    case (int)wfiplib.E_AppRoles.Configuracion:
                        paginaEspera = "administracion/admsysEspera.aspx";
                        break;

                    case (int)wfiplib.E_AppRoles.Promotoria:
                        paginaEspera = "promotoria/esperaPromotoria.aspx";
                        break;

                    case (int)wfiplib.E_AppRoles.SuperPromotoria:
                        paginaEspera = "promotoria/esperaSeleccionPromotoria.aspx";
                        break;

                    case (int)wfiplib.E_AppRoles.Laboratorio:
                        paginaEspera = "laboratorios/MisTramites.aspx";
                        break;

                    case (int)wfiplib.E_AppRoles.Operador:
                        // paginaEspera = "operacion/esperaOperacion.aspx";
                        paginaEspera = "operacion/SeleccionaFlujo.aspx";
                        break;

                    case (int)wfiplib.E_AppRoles.Supervisor:
                        /*paginaEspera = "supervision/esperaSupervisorP.aspx";*/
                        paginaEspera = "supervision/esperaSupervisorPR.aspx";
                        break;

                    case (int)wfiplib.E_AppRoles.SuperReporte:
                        /*paginaEspera = "supervision/esperaSupervisorP.aspx";*/
                        paginaEspera = "supervision/esperaSupervisorPR.aspx";
                        break;


                    case (int)wfiplib.E_AppRoles.Front:
                        paginaEspera = "supervision/esperaFront.aspx";
                        break;

                    default:
                        paginaEspera = "salir.aspx";
                        break;
                }
            }
            Response.Redirect(paginaEspera, true);
        }

        protected void LoginButton_Click(object sender, EventArgs e)
        {
            lnkRecuperarClave.Visible = false;
            try
            {
                string usuario = txUsuario.Text.Trim();
                string userpas = txClave.Text.Trim();

                if (!String.IsNullOrEmpty(usuario) && !String.IsNullOrEmpty(userpas))
                {
                    // wfiplib.admUsuariosPromotoriaPrimierIngreso admUsrPromo = new wfiplib.admUsuariosPromotoriaPrimierIngreso();
                    // int idUsrPromotoriaPrimerIngreso = admUsrPromo.existe(usuario);

                    //if (idUsrPromotoriaPrimerIngreso == 0)
                    //{
                    wfiplib.admCredencial adm = new wfiplib.admCredencial();
                    manejo_sesion.Inicializar();
                    manejo_sesion.ConfiguracionGeneral = (new wfiplib.admConfiguracionGeneral()).ListaconfiguracionSistema();   //Carga configuración sistema

                    if (adm.usuarioClaveCorrecto(usuario, userpas))
                    {
                        manejo_sesion.Credencial = adm.cargaPorNombre(usuario);  //Carga detalle usuario
                        manejo_sesion.Promotoria.Nombre = (new wfiplib.admCatPromotoria(ConfigurationManager.ConnectionStrings["conecta_bd"].ConnectionString)).daNombre(manejo_sesion.Credencial.IdPromotoria); //Nombre Promotoría
                        manejo_sesion.Permisos = (new wfiplib.admPermisos()).PermisosObtenerPorRol(manejo_sesion.Credencial.IdRol); //Permisos para paginas

                        if (manejo_sesion.Credencial.Estado == wfiplib.E_Estado.Activo)
                        {
                            AutenticarUsr(adm, manejo_sesion.Credencial);
                            //if (!adm.estaConectado(usuario))
                            //    AutenticarUsr(adm, manejo_sesion.Credencial);
                            //else
                            //    ltMsg.Text = "Usuario ya conectado en otra sesión.";
                            //    lnkRecuperarClave.Visible = true;
                        }
                        else
                        {
                            ltMsg.Text = "Usuario temporalmente suspendido...";
                            lnkRecuperarClave.Visible = true;
                        }
                    }
                    else
                    {
                        contador += 1;
                        ltMsg.Text = "Credenciales de acceso No válidas.";
                        lnkRecuperarClave.Visible = true;

                        if (contador == manejo_sesion.ConfiguracionGeneral[6].Estado)
                        {
                            //bloquear acceso del usuario
                            adm.DesactivaPorUsuario(usuario);
                            ltMsg.Text = "Acceso bloqueado después de tres intentos, contacte al administrador.";
                            lnkRecuperarClave.Visible = true;
                        }
                    }
                    // else { ltMsg.Text = "Usuario no existe"; }
                    // }
                    // else
                    // {
                    //     Session["primeracceso"] = usuario;
                    //     Response.Redirect("primerAccesoPromotoria.aspx?id=" + idUsrPromotoriaPrimerIngreso.ToString());
                    // }
                }
            }
            catch (Exception ex)
            {
                ltMsg.Text = ex.Message;
            }
        }

        private void AutenticarUsr(wfiplib.admCredencial pAdm, wfiplib.credencial pCredencial)
        {
            pAdm.registraIdSession(pCredencial.Id, Session.SessionID);
            pCredencial.ClavesDeUsuario = (new wfiplib.admCredencial()).ObtenerClaves(pCredencial.Id);

            manejo_sesion.Inicializar();
            manejo_sesion.Credencial = pCredencial;
            manejo_sesion.ConfiguracionGeneral = (new wfiplib.admConfiguracionGeneral()).ListaconfiguracionSistema();

            //Procesos Institucional
            if (manejo_sesion.Credencial.IdRol == 20 || manejo_sesion.Credencial.IdRol == 21 || manejo_sesion.Credencial.IdRol == 22)
            {
                manejo_sesion.ArchivosDependenciasAsignados.Folio = (new wfiplib.ArchivosDependenciasAsignados()).Seleccionar(manejo_sesion.Credencial.Id.ToString());
            }

            Session["credencial"] = manejo_sesion;

            System.Web.Security.FormsAuthentication.SetAuthCookie(pCredencial.Usuario, false);
            string paginaEspera = "salir.aspx";
            if (pCredencial.Aplicacion == wfiplib.E_Aplicacion.AdminSis)
            {
                if (pCredencial.Modulo == wfiplib.E_Modulo.AdmSys)
                    paginaEspera = "administracion/admsysEspera.aspx";
            }
            else if (pCredencial.Aplicacion == wfiplib.E_Aplicacion.Fto)
            {
                if (pCredencial.Modulo == wfiplib.E_Modulo.Promotoria)
                {
                    if (pCredencial.IdRol == (int)wfiplib.E_AppRoles.SuperPromotoria)
                    {
                        paginaEspera = "promotoria/esperaSeleccionPromotoria.aspx";
                    }
                    else
                    {
                        paginaEspera = "promotoria/esperaPromotoria.aspx";
                    }
                }

                if (pCredencial.Modulo == wfiplib.E_Modulo.Operacion && pCredencial.Grupo == wfiplib.E_CredencialGrupo.Operador)
                    paginaEspera = "operacion/esperaOperacion.aspx";
                if (pCredencial.Modulo == wfiplib.E_Modulo.Operacion && pCredencial.Grupo == wfiplib.E_CredencialGrupo.Supervisor)
                    /*paginaEspera = "supervision/esperaSupervisorP.aspx";*/
                    paginaEspera = "supervision/esperaSupervisorPR.aspx";
                if (pCredencial.Modulo == wfiplib.E_Modulo.Monitor && pCredencial.Grupo == wfiplib.E_CredencialGrupo.Operador)
                    paginaEspera = "tablero/MonitorGeneral.aspx";
                if (pCredencial.Modulo == wfiplib.E_Modulo.MesaAyuda)
                    paginaEspera = "MesaAyuda/maGestionIncidencias.aspx";
            }
            else if (pCredencial.Aplicacion == wfiplib.E_Aplicacion.PolizaGrupal)
                paginaEspera = "Grupal/GpoEspera.aspx";
            else
            {
                switch (pCredencial.IdRol)
                {
                    case (int)wfiplib.E_AppRoles.Administrador:
                    case (int)wfiplib.E_AppRoles.Configuracion:
                        paginaEspera = "administracion/admsysEspera.aspx";
                        break;

                    case (int)wfiplib.E_AppRoles.Promotoria:
                        paginaEspera = "promotoria/esperaPromotoria.aspx";
                        break;

                    case (int)wfiplib.E_AppRoles.SuperPromotoria:
                        paginaEspera = "promotoria/esperaSeleccionPromotoria.aspx";
                        break;

                    case (int)wfiplib.E_AppRoles.Laboratorio:
                        paginaEspera = "laboratorios/MisTramites.aspx";
                        break;

                    case (int)wfiplib.E_AppRoles.Operador:
                        // paginaEspera = "operacion/esperaOperacion.aspx";
                        paginaEspera = "operacion/SeleccionaFlujo.aspx";
                        break;

                    case (int)wfiplib.E_AppRoles.Supervisor:
                        /*paginaEspera = "supervision/esperaSupervisorP.aspx";*/
                        paginaEspera = "supervision/esperaSupervisorPR.aspx";
                        break;

                    case (int)wfiplib.E_AppRoles.SuperReporte:
                        /*paginaEspera = "supervision/esperaSupervisorP.aspx";*/
                        paginaEspera = "supervision/esperaSupervisorPR.aspx";
                        break;


                    case (int)wfiplib.E_AppRoles.Front:
                        paginaEspera = "supervision/esperaFront.aspx";
                        break;

                    default:
                        paginaEspera = "salir.aspx";
                        break;
                }
            }

            int diasExpirar = ClavePorExpirarCambiarPass(pCredencial.FechaCambioClave);
            int diasBloquear = ClaveExpiradaCambiarPass(pCredencial.FechaCambioClave);
            int diasTranscurridos = CalcularDiasEntreDosFechas(pCredencial.FechaCambioClave, DateTime.Now);

            string clavereal = wfiplib.Cifrado.Desencriptar(pCredencial.Clave);
            if (clavereal.Contains("MtL#") || clavereal.Equals("ASAE2019"))
            {
                //ClientScript.RegisterStartupScript(GetType(), "alertMessage", "<script>alert('Debe personalizar su contraseña antes de continuar.'); window.location.href='actualizaClave.aspx';</script>");
                mensajes.MostrarMensaje(this, "Debe personalizar su contraseña antes de continuar.", "actualizaClave.aspx");
                return;
            }

            //if (diasExpirar < diasTranscurridos)
            //    ClientScript.RegisterStartupScript(GetType(), "alertMessage", "<script>if (confirm('Su contraseña esta por expirar, ¿cambiarla ahora?')) { window.location.href='actualizaClave.aspx' } else { window.location.href='" + paginaEspera + "' };</script>");
            //else if ((diasBloquear >= diasTranscurridos) && diasBloquear >= diasExpirar)
            //    ClientScript.RegisterStartupScript(GetType(), "alertMessage", "<script>alert('Su contraseña ha expirado, debe cambiarla ahora'); window.location.href='actualizaClave.aspx';</script>");
            //else
            //    Response.Redirect(paginaEspera);

            //Pruebas mensajes
            if (diasTranscurridos > diasExpirar)
                mensajes.MostrarMensaje(this, "Su contraseña ha expirado, debe cambiarla ahora.", "actualizaClave.aspx");
            else if (diasExpirar < diasTranscurridos)
                mensajes.MensajeConfirmacion(this, "Su contraseña esta por expirar, ¿cambiarla ahora?", "actualizaClave.aspx", paginaEspera);
            else if (diasBloquear <= diasTranscurridos && diasBloquear >= diasExpirar)
                mensajes.MostrarMensaje(this, "Su contraseña ha expirado, debe cambiarla ahora.", "actualizaClave.aspx");
            else
                Response.Redirect(paginaEspera, true);
        }

        /// <summary>
        /// Obtiene los dias que han pasado a partir de una fecha
        /// </summary>
        /// <param name="fechacambioclave"></param>
        /// <returns></returns>
        private int ClavePorExpirarCambiarPass(DateTime fechacambioclave)
        {
            TimeSpan diasquehanpasadohastahoy = fechacambioclave.Subtract(DateTime.Now);
            int dias = Math.Abs(int.Parse(diasquehanpasadohastahoy.Days.ToString())) - int.Parse(manejo_sesion.ConfiguracionGeneral[0].Estado.ToString());
            return Math.Abs(dias);
        }

        /// <summary>
        /// Obtiene los dias que faltan para que una clave expire
        /// </summary>
        /// <param name="fechacambioclave"></param>
        /// <returns></returns>
        private int ClaveExpiradaCambiarPass(DateTime fechacambioclave)
        {
            TimeSpan diasquehanpasadohastahoy = fechacambioclave.Subtract(DateTime.Now);
            int dias = Math.Abs(int.Parse(diasquehanpasadohastahoy.Days.ToString())) + int.Parse(manejo_sesion.ConfiguracionGeneral[1].Estado.ToString());
            return Math.Abs(dias);
        }

        /// <summary>
        /// Obtiene los dias que hay entre una fecha inicial y una fecha final
        /// </summary>
        /// <param name="fechaInicio"></param>
        /// <param name="fechaActual"></param>
        /// <returns></returns>
        private int CalcularDiasEntreDosFechas(DateTime fechaInicio, DateTime fechaActual)
        {
            // Diferencia de fechas 
            TimeSpan ts = fechaActual - fechaInicio;

            // Diferencia de días
            return ts.Days;
        }
    }
}