using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;

namespace wfiplib
{
    /// <summary>
    /// Creación dinámica de menú de operaciones.
    /// </summary>
    /// Comentario
    public class Menu
    {
        BaseDeDatos b = new BaseDeDatos();
#pragma warning disable CS0169 // El campo 'Menu.dt' nunca se usa
        DataTable dt;
#pragma warning restore CS0169 // El campo 'Menu.dt' nunca se usa

        /// <summary>
        /// Construye el menú según el Rol del Usuario
        /// </summary>
        /// <param name="UserRol">Rol del Usuario</param>
        /// <returns>Cadena de texto con el menú armado</returns>
        /// <example>lblEtiqueta.Text = Menu.ConstruyeMenu(1)</example>
        public string MenuConstruir(wfiplib.E_AppRoles UserRol)
        {
            string menu = "";
            switch (UserRol)
            {
                case wfiplib.E_AppRoles.Administrador:
                    menu =
                        "<div id='cssmenu'>" +
                        "   <ul>" +
                        "       <li><a href='admsysEspera.aspx' style='height: 16px; line-height: 16px;'>Inicio</a></li>" +
                        "       <li class='has-sub'><a href='#' style='height:16px;line-height:16px;'><span>Catalogos</span></a>" +
                        "           <ul>" +
                        "               <li><a href='admCatPromotorias.aspx'>Promotorias</a></li>" +
                        "               <li><a href='admPromotoriaUsuario.aspx'>Promotoria usuarios</a></li>" +
                        "               <li><a href='admCatAgentes.aspx'>Agentes</a></li>" +
                        "               <li class='has-sub'><a href='#'><span>Usuarios</span></a>" +
                        "                   <ul>" +
                        "                       <li><a href='admUsuario.aspx'>FTO</a></li>" +
                        "                       <li><a href='usuariosV2_cat.aspx'>WFO</a></li>" +
                        "                       <li><a href='AdmGpoUsuario.aspx'>PLZ GRUPAL</a></li>" +
                        "                   </ul>" +
                        "               </li>" +
                        "               <li><a href='admCatMotivosRechazo.aspx'><span>Motivos rechazo</span></a></li>" +
                        "               <li><a href='admCatProductos.aspx'>Productos</a></li>" +
                        "               <li><a href='admCatSubproductos.aspx'>Subproductos</a></li>" +
                        "               <li><a href='admCatMoneda.aspx'>Catalogo de moneda</a></li>" +
                        "               <li><a href='admCatMensajes.aspx'>Catalogo de mensajes</a></li>" +
                        "           </ul>" +
                        "       </li>" +
                        "       <li><a href='admDxFlujo.aspx' style='height:16px;line-height:16px;'><span>Configuración FTO</span></a>" +
                        "       </li>" +
                        "       <li><a href='admConfiguracionGeneral.aspx' style='height:16px;line-height:16px;'><span>Configuración Sistema</span></a>" +
                        "       </li>" +
                        "       <li class='has-sub'><a href = '#' style='height:16px;line-height:16px;'><span>Permisos</span></a>" +
                        "           <ul>" +
                        "               <li><a href='admPermisos.aspx' style='height:16px;line-height:16px;'>Permisos por Rol</a></li>" +
                        "               <li><a href='admPermisosMenu.aspx' style='height:16px;line-height:16px;'>Permisos Menu</a></li>" +
                        "               <li><a href='admRoles.aspx' style='height:16px;line-height:16px;'>Roles</a></li>" +
                        "           </ul>" +
                        "       </li>" +
                        "   </ul>" +
                        "</div>";
                    break;

                case wfiplib.E_AppRoles.Configuracion:
                    menu =
                        "<div id='cssmenu'>" +
                        "   <ul>" +
                        "       <li><a href='admsysEspera.aspx' style='height: 16px; line-height: 16px;'>Inicio</a></li>" +
                        "       <li class='has-sub'><a href='#' style='height:16px;line-height:16px;'><span>Catalogos</span></a>" +
                        "           <ul>" +
                        "               <li><a href='admCatMoneda.aspx'>Catalogo de moneda</a></li>" +
                        "               <li><a href='admCatMensajes.aspx'>Catalogo de mensajes</a></li>" +
                        "           </ul>" +
                        "       </li>" +
                        "   </ul>" +
                        "</div>";
                    break;

                case wfiplib.E_AppRoles.Promotoria:
                case wfiplib.E_AppRoles.SuperPromotoria:
                    menu =
                        "<div id='cssmenu'>" +
                        "   <ul>" +
                        "       <li><a href='esperaPromotoria.aspx'><span>Inicio</span></a></li>" +
                        "       <li class='has-sub'>" +
                        "           <a href='listaMisTramites.aspx'><span>Mis trámites</span></a>" +
                        "       </li>" +
                        "       <li class='has-sub'>" +
                        "           <a href='pmTramitesPendientes.aspx'><span>Pendientes</span></a>" +
                        "       </li>" +
                        "       <li class='has-sub'>" +
                        "           <a href='#'><span>Individual</span></a>" +
                        "           <ul>" +
                        "               <li class='has-sub'>" +
                        "                   <a href='#'><span>Privado</span></a>" +
                        "                   <ul>" +
                        "                       <li class='has-sub'>" +
                        "                           <a href='EmisionVidaRes.aspx'><span>Emisión </span></a>" +
                        "                       </li>" +
                        "                       <li class='has-sub'>" +
                        "                           <a href='EmisionConversiones.aspx'><span>Conversiones  </span></a>" +
                        "                       </li>" +
                        "                   </ul>" +
                        "               </li>" +
                        "           </ul>" +
                        "       </li>" +
                        "       <li class='has-sub'>" +
                        "           <a href='#'><span>Reportes</span></a>" +
                        "           <ul>" +
                        "               <li><a href='ReporteExtraccionAgentes.aspx'><span> Reportes promotoría </span></a></li>" +
                        "           </ul>" +
                        "       </li>" +
                        "       <li class='has-sub'>" +
                        "           <a href='#'><span> Salir </span></a>" +
                        "           <ul>" +
                        "               <li><a href='../salir.aspx?salida=1'><span> Regreso aplicaciones </span></a></li>" +
                        "               <li><a href='../salir.aspx?salida=2'><span> Terminar sesión </span></a></li>" +
                        "           </ul>" +
                        "       </li>" +
                        "  </ul>" +
                        "</div>";
                    break;

                
                case wfiplib.E_AppRoles.OperadorRes:
                    menu =
                        menu = "<nav role='navigation' class='navbar navbar-default mainmenu'>" +
                        "   <!--Brand and toggle get grouped for better mobile display-->" +
                        "   <div class='navbar-header'>" +
                        "       <button type = 'button' data-target='#navbarCollapse' data-toggle='collapse' class='navbar-toggle'>" +
                        "           <span class='sr-only'>Toggle navigation</span>" +
                        "           <span class='icon-bar'></span>" +
                        "           <span class='icon-bar'></span>" +
                        "           <span class='icon-bar'></span>" +
                        "       </button>" +
                        "   </div>" +
                        "   <!--Collection of nav links and other content for toggling -->" +
                        "   <div id='navbarCollapse' class='collapse navbar-collapse'>" +
                        "       <ul id='fresponsive' class='nav navbar-nav dropdown'>" +
                        "           <li><a href='../operacion/SeleccionaFlujo.aspx'> Inicio </a></li>" +
                        "           <li><a href='../supervision/esperaSupervisorPR.aspx'> Mapa </a></li>" +
                        "           <!--<li><a href='../operacion/Buscar.aspx'> Buscar </a></li>-->" +
                        "           <li><a href='../operacion/listaTramite.aspx'> Pendientes </a></li>" +
                        "           <li><a href='../operacion/MisTramites.aspx'> Mis trámites </a></li>" +
                        "           <li class='dropdown'><a data-toggle='dropdown' class='dropdown-toggle'> Productividad <span class='caret'></span></a>" +
                        "			    <ul class='dropdown-menu'>" +
                        "				    <li><a href= '../supervision/sprReporteProductividad.aspx'> Productividad </a></li>" +
                        "				    <li><a href= '../supervision/sprReporteProductividadOperacion.aspx'> Productividad Operación</a></li>" +
                        "				</ul>" +
                        "			</li>" +
                        "           <li class='dropdown'><a data-toggle='dropdown' class='dropdown-toggle'> Salir <span class='caret'></span></a>" +
                        "			    <ul class='dropdown-menu'>" +
                        "				    <li><a href= '../salir.aspx?salida=1'> Regreso aplicaciones </a></li>" +
                        "				    <li><a href= '../salir.aspx?salida=2'> Terminar sesión  </a></li>" +
                        "				</ul>" +
                        "			</li>" +
                        "       </ul>" +
                        "   </div>" +
                        "</nav>";
                    
                    break;
                case wfiplib.E_AppRoles.Operador:
                    menu =
                        menu =
                    "<div id='cssmenu'>" +
                        "   <ul>" +
                        "       <li><a href='../operacion/SeleccionaFlujo.aspx' style='height:16px; line-height:16px;'>Inicio</a></li>" +
                        "       <li><a href='../supervision/esperaSupervisorPR.aspx' style='height:16px; line-height:16px;'>Mapa</a></li>" +
                        "       <!--<li><a href='../operacion/Buscar.aspx' style='height:16px; line-height:16px;'>Buscar</a></li>-->" +
                        "       <li><a href='../operacion/listaTramite.aspx' style='height:16px; line-height:16px;'>Pendientes</a></li>" +
                        "       <li><a href='../operacion/MisTramites.aspx' style='height:16px; line-height:16px;'>Mis Trámites</a></li>" +
                        "       <li class='has-sub'><a href='#' style='height:16px;line-height:16px;'><span>Productividad</span></a>" +
                        "           <ul>" +
                        "               <li><a href='../supervision/sprReporteProductividad.aspx' style='height:16px; line-height:16px;'>Productividad</a></li>" +
                        "               <li><a href='../supervision/sprReporteProductividadOperacion.aspx' style='height:16px; line-height:16px;'>Productividad Operación</a></li>" +
                        "           </ul>" +
                        "       </li>" +
                        "       <li class='has-sub'><a href='#' style='height:16px;line-height:16px;'><span> Salir </span></a>" +
                        "           <ul>" +
                        "               <li><a href='../salir.aspx?salida=1' style='height:16px; line-height:16px;'> Regreso aplicaciones </a></li>" +
                        "               <li><a href='../salir.aspx?salida=2' style='height:16px; line-height:16px;'> Terminar sesión </a></li>" +
                        "           </ul>" +
                        "       </li>" +
                        "   </ul>" +
                        "</div>";

                    break;
                case wfiplib.E_AppRoles.Laboratorio:
                    menu =
                        "<div id='cssmenu'>" +
                        "   <ul>" +
                        "       <li><a href='MisTramites.aspx' style='height:16px; line-height:16px;'>Mis Trámites</a></li>" +
                        "   </ul>" +
                        "</div>";
                    break;

                case wfiplib.E_AppRoles.Supervisor:
                    menu =
                        "<div id='cssmenu'>   " +
                        "	<ul>       " +
                        "		<li><a href='esperaSupervisorPR.aspx' style='height:16px; line-height:16px;'>Inicio</a></li>" +
                        "		<li class='has-sub'>            " +
                        "			<a href='#' style='height:16px; line-height:16px;'><span>Usuarios</span></a>            " +
                        "			<ul>                " +
                        "				<li class='has-sub'><a href = 'usuariosV2_cat.aspx'><span>Administración de Usuarios</span></a></li>            " +
                        "			</ul>      " +
                        "		</li>       " +
                        "		<li class='has-sub'>            " +
                        "			<a href='#' style='height:16px; line-height:16px;'><span>Acciones</span></a>            " +
                        "			<ul>                " +
                        "				<li class='has-sub'><a href = '#'><span>Trámites</span></a>                   " +
                        "					<ul>                      " +
                        "						<li><a href='MisTramites.aspx'>Listado</a></li>                      " +
                        "						<li><a href='buscarTramites.aspx'>Buscar</a></li>                      " +
                        "						<li><a href='asignarTramites.aspx'>Asignación de trámites</a></li>        " +
                        "						<li><a href='priorizarTramite.aspx'>Priorizar trámites</a></li>              " +
                        "						<li><a href='cancelarTramites.aspx'>Cancelación de trámites</a></li>            " +
                        "					</ul>                " +
                        "				</li>                " +
                        "			</ul>       " +
                        "		</li>       " +
                        "		<li class='has-sub'>            " +
                        "			<a href='#' style='height:16px; line-height:16px;'><span>Reportes</span></a>            " +
                        "			<ul>               " +
                        "				<li><a href='sprReporteGeneralTotales.aspx'>Totales</a></li>               " +
                        "				<li><a href='detalleMesa.aspx'>Detalle por mesa</a></li>               " +
                        "				<li><a href='sprReporteGeneralMesa.aspx'>Mesa</a></li>               " +
                        "				<li><a href='sprReporteGeneralTop10.aspx'>Top 10</a></li>               " +
                        "				<li><a href='sprReporteGeneralFranja.aspx'>Franja</a></li>               " +
                        "				<li><a href='sprDetallePromotoria.aspx'>Detalle Promotoria</a></li>         " +
                        "				<li><a href='sprReportePorcientoSuspension.aspx'>Suspendidos</a></li>          " +
                        "				<li><a href='sprReporteEstatusTramite.aspx'>Estatus Trámite</a></li>              " +
                        "				<li><a href='sprTiemposAtencion.aspx'>Tiempos de atención</a></li>               " +
                        "				<li><a href='sprReporteProductividad.aspx'>Productividad</a></li>               " +
                        "				<li><a href='sprRelojChecador.aspx'>Reloj Checador</a></li>               " +
                        "				<li><a href='sprDetalleHoras.aspx'>Detalle de horas</a></li>               " +
                        "				<li><a href='sprReporteCaducados.aspx'>Trámites caducados</a></li>            " +
                        "				<li><a href='sprReporteSelProcesado.aspx'>Procesable</a></li>               " +
                        "				<li><a href='sprTAT.aspx'>TAT</a></li>               " +
                        "				<li><a href='sprEticaCumplimiento.aspx'>Ética y Cumplimiento</a></li>               " +
                        "				<li><a href='sprReingresos.aspx'>Reingresos</a></li>           " +
                        "			</ul>       " +
                        "		</li>       " +
                        "		<li class='has-sub'>           " +
                        "		    <a href='#' style='height:16px; line-height:16px;'><span>Opciones</span></a>           " +
                        "			<ul>               " +
                        "				<li class='has-sub'><a href='subirAgentes.aspx' style='height:16px;line-height:16px;'>Catálogo Agentes</a></li>               " +
                        "				<li class='has-sub'><a href='subirLaboratorios.aspx' style='height:16px;line-height:16px;'>Catálogo Laboratorios</a></li>           " +
                        "			</ul>       " +
                        "		</li>   " +
                        "		<li class='has-sub'>           " +
                        "		    <a href='#' style='height:16px; line-height:16px;'><span> Salir </span></a>           " +
                        "			<ul>               " +
                        "				<li class='has-sub'><a href='../salir.aspx?salida=1' style='height:16px;line-height:16px;'> Regreso aplicaciones </a></li>               " +
                        "				<li class='has-sub'><a href='../salir.aspx?salida=2' style='height:16px;line-height:16px;'> Terminar sesión </a></li>           " +
                        "			</ul>       " +
                        "		</li>   " +
                        "	</ul>" +
                        "</div>";

                    break;

                case wfiplib.E_AppRoles.SuperReporte:
                    menu =
                        "<div id='cssmenu'>   " +
                        "	<ul>       " +
                        "		<li><a href='esperaSupervisorPR.aspx' style='height:16px; line-height:16px;'>Inicio</a></li>" +
                        "		<li class='has-sub'>            " +
                        "			<a href='#' style='height:16px; line-height:16px;'><span>Reportes</span></a>            " +
                        "			<ul>               " +
                        "				<li><a href='sprReporteGeneralTotales.aspx'>Totales</a></li>               " +
                        "				<li><a href='detalleMesa.aspx'>Detalle por mesa</a></li>               " +
                        "				<li><a href='sprReporteGeneralMesa.aspx'>Mesa</a></li>               " +
                        "				<li><a href='sprReporteGeneralTop10.aspx'>Top 10</a></li>               " +
                        "				<li><a href='sprReporteGeneralFranja.aspx'>Franja</a></li>               " +
                        "				<li><a href='sprDetallePromotoria.aspx'>Detalle Promotoria</a></li>         " +
                        "				<li><a href='sprReportePorcientoSuspension.aspx'>Suspendidos</a></li>          " +
                        "				<li><a href='sprReporteEstatusTramite.aspx'>Estatus Trámite</a></li>              " +
                        "				<li><a href='sprTiemposAtencion.aspx'>Tiempos de atención</a></li>               " +
                        "				<li><a href='sprReporteProductividad.aspx'>Productividad</a></li>               " +
                        "				<li><a href='sprRelojChecador.aspx'>Reloj Checador</a></li>               " +
                        "				<li><a href='sprDetalleHoras.aspx'>Detalle de horas</a></li>               " +
                        "				<li><a href='sprReporteCaducados.aspx'>Trámites caducados</a></li>            " +
                        "				<li><a href='sprReporteSelProcesado.aspx'>Procesable</a></li>               " +
                        "				<li><a href='sprTAT.aspx'>TAT</a></li>               " +
                        "				<li><a href='sprEticaCumplimiento.aspx'>Ética y Cumplimiento</a></li>               " +
                        "				<li><a href='sprReingresos.aspx'>Reingresos</a></li>           " +
                        "			</ul>       " +
                        "		</li>       " +
                        "		<li class='has-sub'>           " +
                        "		    <a href='#' style='height:16px; line-height:16px;'><span>Salir </span></a>           " +
                        "			<ul>               " +
                        "			    <li class='has-sub'><a href='../salir.aspx?salida=1' style='height:16px;line-height:16px;'> Regreso aplicaciones </a></li>               " +
                        "			    <li class='has-sub'><a href='../salir.aspx?salida=2' style='height:16px;line-height:16px;'> Terminar sesión </a></li>               " +
                        "			</ul>       " +
                        "		</li>   " +
                        "	</ul>" +
                        "</div>";

                    break;

                case wfiplib.E_AppRoles.Front:
                    menu =
                        "<div id='cssmenu'>" +
                        "   <ul>" +
                        "       <li><a href='esperaFront.aspx' style='height:16px; line-height:16px;'>Inicio</a></li>" +
                        "       <li class='has-sub'>" +
                        "            <a href='#' style='height:16px; line-height:16px;'><span>Acciones</span></a>" +
                        "            <ul>" +
                        "                <li class='has-sub'><a href = '#'><span>Trámites</span></a>" +
                        "                   <ul>" +
                        "                      <li><a href='MisTramites.aspx'>Listado</a></li>" +
                        "                      <li><a href='buscarTramites.aspx'>Buscar</a></li>" +
                        "                   </ul>" +
                        "                </li>" +
                        "            </ul>" +
                        "       </li>" +
                        "       <li class='has-sub'>" +
                        "            <a href='#' style='height:16px; line-height:16px;'><span>Reportes</span></a>" +
                        "            <ul>" +
                        "               <li><a href='sprReporteGeneralTotales.aspx'>Totales</a></li>" +
                        "               <li><a href='detalleMesa.aspx'>Detalle por mesa</a></li>" +
                        "               <li><a href='sprReporteGeneralMesa.aspx'>Mesa</a></li>" +
                        "               <li><a href='sprReporteGeneralTop10.aspx'>Top 10</a></li>" +
                        "               <li><a href='sprReporteFranja.aspx'>Franja</a></li>" +
                        "               <li><a href='sprReporteTendencia.aspx'>Tendencia</a></li>" +
                        "               <li><a href='sprDetallePromotoria.aspx'>Detalle Promotoria</a></li>" +
                        "               <li><a href='sprReportePorcientoSuspension.aspx'>Suspendidos</a></li>" +
                        "               <li><a href='sprReporteTramiteSemana.aspx'>Trámites por semana</a></li>" +
                        "               <li><a href='sprReporteEstatusTramite.aspx'>Estatus Trámite</a></li>" +
                        "               <li><a href='sprTiemposAtencion.aspx'>Tiempos de atención</a></li>" +
                        "               <li><a href='sprReporteProductividad.aspx'>Productividad</a></li>" +
                        "               <li><a href='sprRelojChecador.aspx'>Reloj Checador</a></li>" +
                        "               <li><a href='sprDetalleHoras.aspx'>Detalle de horas</a></li>" +
                        "               <li><a href='sprReporteCaducados.aspx'>Trámites caducados</a></li>" +
                        "               <li><a href='sprReporteSelProcesado.aspx'>Procesable</a></li>" +
                        "               <li><a href='sprTAT.aspx'>TAT</a></li>" +
                        "               <li><a href ='sprEticaCumplimiento.aspx'>Ética y Cumplimiento</ a ></ li > " +
                        "               <li><a href='sprReingresos.aspx'>Reingresos</a></li>" +
                        "           </ul>" +
                        "       </li>" +
                        "       <li class='has-sub'>" +
                        "           <a href='#' style='height:16px; line-height:16px;'><span>Salir </span></a>" +
                        "           <ul>" +
                        "               <li class='has-sub'><a href='../salir.aspx?salida=1' style='height:16px;line-height:16px;'> Regreso aplicaciones </a></li>" +
                        "               <li class='has-sub'><a href='../salir.aspx?salida=2' style='height:16px;line-height:16px;'> Terminar sesión </a></li>" +
                        "           </ul>" +
                        "       </li>" +
                        "   </ul>" +
                        "</div>";
                    break;

                default:
                    menu = "";
                    break;
            }
            return menu;
        }

        /// <summary>
        /// Construye un menú dinámico de texto
        /// </summary>
        /// <param name="modulo">Número de Módulo</param>
        /// <param name="grupo">Número de Grupo</param>
        /// <returns>Cadena de texto con el menú armado</returns>
        /// <example>lblEtiqueta.Text = Menu.ConstruyeMenu(1,1)</example>
        public string MenuConstruir(E_Modulo modulo, E_CredencialGrupo grupo)
        {
            // Menú de supervisor.... 
            string menu = "";
            if (modulo == E_Modulo.AdmSys)
            {
                menu =
                "<div id='cssmenu'>" +
                "   <ul>" +
                "       <li><a href='admsysEspera.aspx' style='height: 16px; line-height: 16px;'>Inicio</a></li>" +
                "       <li class='has-sub'><a href='#' style='height:16px;line-height:16px;'><span>Catalogos</span></a>" +
                "           <ul>" +
                "               <li><a href='admCatPromotorias.aspx'>Promotorias</a></li>" +
                "               <li><a href='admPromotoriaUsuario.aspx'>Promotoria usuarios</a></li>" +
                "               <li><a href='admCatAgentes.aspx'>Agentes</a></li>" +
                "               <li class='has-sub'><a href='#'><span>Usuarios</span></a>" +
                "                   <ul>" +
                "                       <li><a href='admUsuario.aspx'>FTO</a></li>" +
                "                       <li><a href='usuariosV2_cat.aspx'>WFO</a></li>" +
                "                       <li><a href='AdmGpoUsuario.aspx'>PLZ GRUPAL</a></li>" +
                "                   </ul>" +
                "               </li>" +
                "               <li><a href='admCatMotivosRechazo.aspx'><span>Motivos rechazo</span></a></li>" +
                "               <li><a href='admCatProductos.aspx'>Productos</a></li>" +
                "               <li><a href='admCatSubproductos.aspx'>Subproductos</a></li>" +
                "               <li><a href='admCatMoneda.aspx'>Catalogo de moneda</a></li>" +
                "               <li><a href='admCatMensajes.aspx'>Catalogo de mensajes</a></li>" +
                "           </ul>" +
                "       </li>" +
                "       <li><a href='admDxFlujo.aspx' style='height:16px;line-height:16px;'><span>Configuración FTO</span></a>" +
                "       </li>" +
                "       <li><a href='admConfiguracionGeneral.aspx' style='height:16px;line-height:16px;'><span>Configuración Sistema</span></a>" +
                "       </li>" +
                "       <li class='has-sub'><a href = '#' style='height:16px;line-height:16px;'><span>Permisos</span></a>" +
                "           <ul>" +
                "               <li><a href='admPermisos.aspx' style='height:16px;line-height:16px;'>Permisos por Rol</a></li>" +
                "               <li><a href='admPermisosMenu.aspx' style='height:16px;line-height:16px;'>Permisos Menu</a></li>" +
                "               <li><a href='admRoles.aspx' style='height:16px;line-height:16px;'>Roles</a></li>" +
                "           </ul>" +
                "       </li>" +
                "       <li class='has-sub'><a href = '#' style='height:16px;line-height:16px;'><span> Salir </span></a>" +
                "           <ul>" +
                "               <li><a href='../salir.aspx?salida=1' style='height:16px;line-height:16px;'> Regreso aplicaciones </a></li>" +
                "               <li><a href='../salir.aspx?salida=2' style='height:16px;line-height:16px;'> Terminar sesión </a></li>" +
                "           </ul>" +
                "       </li>" +
                "   </ul>" +
                "</div>";
            }
            if (modulo == E_Modulo.Promotoria)
            {
                menu =
                        "<div id='cssmenu'>" +
                        "   <ul>" +
                        "       <li><a href='esperaPromotoria.aspx'><span>Inicio</span></a></li>" +
                        "       <li class='has-sub'>" +
                        "           <a href='listaMisTramites.aspx'><span>Mis trámites</span></a>" +
                        "       </li>" +
                        "       <li class='has-sub'>" +
                        "           <a href='pmTramitesPendientes.aspx'><span>Pendientes</span></a>" +
                        "       </li>" +
                        "       <li class='has-sub'>" +
                        "           <a href='#'><span>Individual</span></a>" +
                        "           <ul>" +
                        "               <li class='has-sub'>" +
                        "                   <a href='#'><span>Privado</span></a>" +
                        "                   <ul>" +
                        "                       <li class='has-sub'>" +
                        "                           <a href='EmisionVidaRes.aspx'><span>Emisión </span></a>" +
                        "                       </li>" +
                        "                       <li class='has-sub'>" +
                        "                           <a href='EmisionConversiones.aspx'><span>Conversiones  </span></a>" +
                        "                       </li>" +
                        "                   </ul>" +
                        "               </li>" +
                        "           </ul>" +
                        "       </li>" +
                        "       <li class='has-sub'>" +
                        "           <a href='#'><span> Reportes </span></a>" +
                        "           <ul>" +
                        "               <li><a href='ReporteExtraccionAgentes.aspx'><span> Reportes promotoría </span></a></li>" +
                        "           </ul>" +
                        "       </li>" +
                        "       <li class='has-sub'>" +
                        "           <a href='#'><span> Salir </span></a>" +
                        "           <ul>" +
                        "               <li><a href='../salir.aspx?salida=1'><span> Regreso aplicaciones </span></a></li>" +
                        "               <li><a href='../salir.aspx?salida=2'><span> Terminar sesión </span></a></li>" +
                        "           </ul>" +
                        "       </li>" +
                        "  </ul>" +
                        "</div>";
            }
            if (modulo == E_Modulo.Promotoria && grupo == E_CredencialGrupo.PromotoriaRes)
            {
                menu =  "<nav role='navigation' class='navbar navbar-default mainmenu'>" +
                        "   <!--Brand and toggle get grouped for better mobile display-->" + 
                        "   <div class='navbar-header'>" +
                        "       <button type = 'button' data-target='#navbarCollapse' data-toggle='collapse' class='navbar-toggle'>" +
                        "           <span class='sr-only'>Toggle navigation</span>" +
                        "           <span class='icon-bar'></span>" +
                        "           <span class='icon-bar'></span>" +
                        "           <span class='icon-bar'></span>" +
                        "       </button>" +
                        "   </div>" + 
                        "   <!--Collection of nav links and other content for toggling -->" +
                        "   <div id='navbarCollapse' class='collapse navbar-collapse'>" +
                        "       <ul id='fresponsive' class='nav navbar-nav dropdown'>" +
                        "           <li><a href='esperaPromotoria.aspx'> Inicio </a></li>" +
                        "           <li><a href='listaMisTramites.aspx'> Mis trámites </a></li>" +
                        "           <li class='dropdown'><a data-toggle='dropdown' class='dropdown-toggle'> Individual <span class='caret'></span></a>" +
                        "			    <ul class='dropdown-menu'>" +
                        "				    <li>" +
                        "					    <a href='#' data-toggle='dropdown' class='dropdown-toggle'> Privado <span class='caret'></span></a>" +
                        "						<ul class='dropdown-menu'>" +
                        "					        <li><a href= 'EmisionVidaRes.aspx'> Emisión </a></li>" +
                        "                           <li class='has-sub'>" +
                        "                               <a href='EmisionConversiones.aspx'><span>Conversiones  </span></a>" +
                        "                           </li>" +
                        "					    </ul>" +
                        "					</li>" +
                        "				</ul>" +
                        "		    </li>" +
                        "           <li class='dropdown'><a data-toggle='dropdown' class='dropdown-toggle'> Reportes <span class='caret'></span></a>" +
                        "			    <ul class='dropdown-menu'>" +
                        "				    <li><a href= 'ReporteExtraccionAgentes.aspx'> Reportes promotoría </a></li>" +
                        "				</ul>" +
                        "			</li>" +
                        "           <li class='dropdown'><a data-toggle='dropdown' class='dropdown-toggle'> Salir  <span class='caret'></span></a>" +
                        "			    <ul class='dropdown-menu'>" +
                        "				    <li><a href= '../salir.aspx?salida=1'> Regreso aplicaciones </a></li>" +
                        "				    <li><a href= '../salir.aspx?salida=2'> Terminar sesión  </a></li>" +
                        "				</ul>" +
                        "			</li>" +
                        "       </ul>" +
                        "   </div>" +
                        "</nav>";
            }
            if (modulo == E_Modulo.Operacion && grupo == E_CredencialGrupo.Operador)
            {
                menu =
                "<div id='cssmenu'>" +
                "   <ul>" +
                "       <li><a href='../operacion/esperaOperacion.aspx' style='height:16px; line-height:16px;'>Inicio</a></li>" +
                "       <li><a href='../supervision/esperaSupervisorPR.aspx' style='height:16px; line-height:16px;'>Mapa</a></li>" +
                "       <!--<li><a href='Buscar.aspx' style='height:16px; line-height:16px;'>Buscar</a></li>-->" +
                "       <li><a href='../operacion/listaTramite.aspx' style='height:16px; line-height:16px;'>Pendientes</a></li>" +
                "       <li><a href='../operacion/MisTramites.aspx' style='height:16px; line-height:16px;'>Trámites </a></li>" +
                "       <li class='has-sub'><a href='#' style='height:16px;line-height:16px;'><span>Productividad</span></a>" +
                "           <ul>" +
                "               <li><a href='../supervision/sprReporteProductividad.aspx' style='height:16px; line-height:16px;'>Productividad</a></li>" +
                "               <li><a href='../supervision/sprReporteProductividadOperacion.aspx' style='height:16px; line-height:16px;'>Productividad Operación</a></li>" +
                "           </ul>" +
                "       </li>" +
                "       <li class='has-sub'><a href='#' style='height:16px;line-height:16px;'><span> Salir </span></a>" +
                "           <ul>" +
                "               <li><a href='../salir.aspx?salida=1' style='height:16px; line-height:16px;'> Regreso aplicaciones </a></li>" +
                "               <li><a href='../salir.aspx?salida=2' style='height:16px; line-height:16px;'> Terminar sesión </a></li>" +
                "           </ul>" +
                "       </li>" +
                "   </ul>" +
                "</div>";
            }
            if (modulo == E_Modulo.Operacion && grupo == E_CredencialGrupo.Supervisor)
            {
                menu =
                "<div id='cssmenu'>" +
                "   <ul>" +
                "       <li><a href='esperaSupervisorPR.aspx' style='height:16px; line-height:16px;'>Inicio</a></li>" +

                "       <li class='has-sub'>" +
                "            <a href='#' style='height:16px; line-height:16px;'><span>Usuarios</span></a>" +
                "            <ul>" +
                "                <li class='has-sub'><a href = 'usuariosV2_cat.aspx'><span>Administración de Usuarios</span></a>" +
                "                </li>" +
                "            </ul>" +
                "       </li>" +

                "       <li class='has-sub'>" +
                "            <a href='#' style='height:16px; line-height:16px;'><span>Acciones</span></a>" +
                "            <ul>" +
                "                <li class='has-sub'><a href = '#'><span>Trámites</span></a>" +
                "                   <ul>" +
                "                      <li><a href='MisTramites.aspx'>Listado</a></li>" +
                "                      <li><a href='buscarTramites.aspx'>Buscar</a></li>" +
                "                      <li><a href='asignarTramites.aspx'>Asignación de trámites</a></li>" +
                "                      <li><a href='priorizarTramite.aspx'>Priorizar trámites</a></li>" +
                "                      <li><a href='cancelarTramites.aspx'>Cancelación de trámites</a></li>" +
                "                   </ul>" +
                "                </li>" +
                "            </ul>" +
                "       </li>" +

                "       <li class='has-sub'>" +
                "            <a href='#' style='height:16px; line-height:16px;'><span>Reportes</span></a>" +
                "            <ul>" +
                "                <li class='has-sub'><a href = '#'><span>Reporde de Trámites</span></a>" +
                "                   <ul>" +
                "                           <li><a href='sprReporteProductividadPromotorias.aspx'>Trámites Ingresados</a></li>" +
                "                           <li><a href='sprReporteTramitesAnuales.aspx'>Trámites por Año</a></li>" +
                "                           <li><a href='sprAlmacenamientoTramites.aspx'>Almacen de Expedientes</a></li>" +
                "                           <li><a href='sprTramitesReingresosV2.aspx'>Reingresos de Trámites</a></li>" +
                "                   </ul>" +
                "                </li>" +
                "                <li class='has-sub'><a href = '#'><span>Productividad </span></a>" +
                "                   <ul>" +
                "                           <li><a href='sprReporteProductividad.aspx'>Productividad </a></li>" +
                "                           <li><a href='sprReporteProductividadOperacion.aspx'>Productividad operación </a></li>" +
                "                   </ul>" +
                "                </li>" +
                "               <li><a href='sprReporteGeneralTotales.aspx'>Totales</a></li>" +
                "               <li><a href='detalleMesaR.aspx'>Detalle por mesa</a></li>" +
                "               <li><a href='detalleMesaRv2.aspx'>Sábana v2</a></li>" +
                "				<li><a href='sprReporteGrandesSumas.aspx'>Grandes Sumas</a></li> " +
                "               <li><a href='sprReporteLaboratorios.aspx'> Reporte Laboratorios </a></li>" +
                "               <li><a href='sprReportePCI.aspx'> Reporte PCI </a></li>" +
                "               <li><a href='sprReporteGeneralMesa.aspx'>Mesa</a></li>" +
                "               <li><a href='sprReporteGeneralTop10.aspx'>Top 10</a></li>" +
                "               <li><a href='sprReporteGeneralFranja.aspx'>Franja</a></li>" +

                "                <li class='has-sub'><a href = '#'><span>Detalle Promotoria </span></a>" +
                "                   <ul>" +
                "                           <li><a href='sprDetallePromotoria.aspx'> Estatus Trámites por Promotoría </a></li>" +
                "                           <li><a href='sprReportePromotorias.aspx'> Trámites por Promotoría </a></li>" +
                "                   </ul>" +
                "                </li>" +

                "               <li><a href='sprReportePorcientoSuspension.aspx'>Suspendidos</a></li>" +
                "               <li><a href='sprReporteEstatusTramite.aspx'>Estatus Trámite</a></li>" +
                "               <li><a href='sprTiemposAtencion.aspx'>Tiempos de atención</a></li>" +
                "               <li><a href='sprRelojChecador.aspx'>Reloj Checador</a></li>" +
                "               <li><a href='sprDetalleHoras.aspx'>Detalle de horas</a></li>" +
                "               <li><a href='sprReporteCaducados.aspx'>Trámites caducados</a></li>" +
                "               <li><a href='sprReporteSelProcesado.aspx'>Procesable</a></li>" +
                "               <li><a href='sprTAT.aspx'>TAT</a></li>" +
                "               <li><a href='sprEticaCumplimiento.aspx'>Ética y Cumplimiento</a></li>" +
                "               <li><a href='sprReingresos.aspx'>Reingresos</a></li>" +
                "           </ul>" +
                "       </li>" +

                "       <li class='has-sub'>" +
                "           <a href='#' style='height:16px; line-height:16px;'><span>Opciones</span></a>" +
                "           <ul>" +
                "               <li class='has-sub'><a href='subirAgentes.aspx' style='height:16px;line-height:16px;'>Catálogo Agentes</a></li>" +
                "               <li class='has-sub'><a href='subirLaboratorios.aspx' style='height:16px;line-height:16px;'>Catálogo Laboratorios</a></li>" +
                "           </ul>" +
                "       </li>" +

                "       <li class='has-sub'>" +
                "           <a href='#' style='height:16px; line-height:16px;'><span> Salir </span></a>" +
                "           <ul>" +
                "               <li class='has-sub'><a href='../salir.aspx?salida=1' style='height:16px;line-height:16px;'> Regreso aplicaciones </a></li>" +
                "               <li class='has-sub'><a href='../salir.aspx?salida=2' style='height:16px;line-height:16px;'> Terminar sesión </a></li>" +
                "           </ul>" +
                "       </li>" +

                "   </ul>" +
                "</div>";
            }
            if (modulo == E_Modulo.Operacion && grupo == E_CredencialGrupo.SupervisorRes)
            {
                menu =
                "<nav role='navigation' class='navbar navbar-default mainmenu'>" +
                "                    <!--Brand and toggle get grouped for better mobile display-->" +
                "                    <div class='navbar-header'>" +
                "                        <button type = 'button' data-target='#navbarCollapse' data-toggle='collapse' class='navbar-toggle'>" +
                "                            <span class='sr-only'>Toggle navigation</span>" +
                "                            <span class='icon-bar'></span>" +
                "                            <span class='icon-bar'></span>" +
                "                            <span class='icon-bar'></span>" +
                "                        </button>" +
                "                    </div>" +
                "                    <!--Collection of nav links and other content for toggling -->" +
                "                    <div id='navbarCollapse' class='collapse navbar-collapse'>" +
                "                        <ul id='fresponsive' class='nav navbar-nav dropdown'>" +
                "                            <li><a href='esperaSupervisorPR.aspx'> Inicio </a></li>" +
                "                            <li class='dropdown'><a data-toggle='dropdown' class='dropdown-toggle'>Usuarios<span class='caret'></span></a>" +
			    "						            <ul class='dropdown-menu'>"+
				"						            <li><a href= 'usuariosV2_cat.aspx'> Administración de Usuarios</a></li>" +
				"					            </ul>"+
				"				            </li>" +
                "                            <li class='dropdown'><a data-toggle='dropdown' class='dropdown-toggle'>Acciones<span class='caret'></span></a>" +
				"					            <ul class='dropdown-menu'>" +
				"						            <li>" +
				"							            <a href='#' data-toggle='dropdown' class='dropdown-toggle'>Trámites<span class='caret'></span></a>" +
				"							            <ul class='dropdown-menu'>"+
                "								            <li><a href= 'MisTramites.aspx'> Listado </a></li>" +
                "                                           <li><a href= 'buscarTramites.aspx'>Buscar</a></li>" +
                "                                           <li><a href= 'asignarTramites.aspx'> Asignación de trámites</a></li>" +
                "                                           <li><a href= 'priorizarTramite.aspx'> Priorizar trámites</a></li>" +
                "                                           <li><a href= 'cancelarTramites.aspx'> Cancelación de trámites</a></li>" +
                "							            </ul>" +
                "						            </li>" +
				"					            </ul>" +
				"				            </li>" +
                "                            <li class='dropdown'><a data-toggle='dropdown' class='dropdown-toggle'>Reportes<span class='caret'></span></a>" +
                "					            <ul class='dropdown-menu'>" +
                "						            <li>" +
                "							            <a href = '#' data-toggle='dropdown' class='dropdown-toggle'>Reporde de Trámites<span class='caret'></span></a>" +
                "							            <ul class='dropdown-menu'>" +
				"								            <li><a href='sprReporteProductividadPromotorias.aspx'> Trámites Ingresados</a></li>" +
                "								            <li><a href='sprReporteTramitesAnuales.aspx'> Trámites por Año</a></li>" +
                "                                           <li><a href='sprAlmacenamientoTramites.aspx'> Almacen de Expedientes</a></li>" +
                "                                           <li><a href='sprTramitesReingresosV2.aspx'> Reingresos de Trámites</a></li>" +
                "                                           <li><a href='sprTramitesFacturaKWIKV1.aspx'> Facturación KWIK</a></li>" +
                "							            </ul>" +
				"						            </li>" +
                "						            <li>" +
                "							            <a href = '#' data-toggle='dropdown' class='dropdown-toggle'>Productividad<span class='caret'></span></a>" +
                "							            <ul class='dropdown-menu'>" +
                "								            <li><a href='sprReporteProductividad.aspx'> Productividad </a></li>" +
                "								            <li><a href='sprReporteProductividadOperacion.aspx'> Productividad operación </a></li>" +
                "							            </ul>" +
                "						            </li>" +
                "                                   <li><a href= 'sprReporteGeneralTotales.aspx'> Totales </ a ></ li >" +
                "                                   <li><a href= 'detalleMesaR.aspx'> Detalle por mesa</a></li>" +
                "                                   <li><a href= 'detalleMesaRv2.aspx'>Sábana v2</a></li>" +

                "				                    <li><a href='sprReporteGrandesSumas.aspx'>Grandes Sumas</a></li>" +
                "				                    <li><a href='sprReporteOneShot.aspx'>OneShot</a></li>" +
                "				                    <li><a href='sprReporteCapturaPromotoria.aspx'>Captura Promotoría</a></li>" +
                "                                   <li><a href= 'sprReporteLaboratorios.aspx'> Reporte Laboratorios </a></li>" +
                "                                   <li><a href= 'sprReportePCI.aspx'> Reporte PCI </a></li>" +
                "                                   <li><a href= 'sprReporteGeneralMesa.aspx'> Mesa </ a ></ li >" +
                "                                   <li><a href= 'sprReporteGeneralTop10.aspx'> Top 10</a></li>" +
                "                                   <li><a href= 'sprReporteGeneralFranja.aspx'> Franja </a></li>" +

                "						            <li>" +
                "							            <a href = '#' data-toggle='dropdown' class='dropdown-toggle'>Detalle Promotoria<span class='caret'></span></a>" +
                "							            <ul class='dropdown-menu'>" +
                "								            <li><a href='sprDetallePromotoria.aspx'> Estatus Trámites por Promotoría </a></li>" +
                "								            <li><a href='sprReportePromotorias.aspx'> Trámites por Promotoría </a></li>" +
                "							            </ul>" +
                "						            </li>" +

                "                                   <li><a href= 'sprReportePorcientoSuspension.aspx'> Suspendidos </a></li>" +
                "                                   <li><a href= 'sprReporteEstatusTramite.aspx'> Estatus Trámite</a></li>" +
                "                                   <li><a href= 'sprTiemposAtencion.aspx'> Tiempos de atención</a></li>" +
                "                                   <li><a href= 'sprReporteProductividad.aspx'> Productividad </a></li>" +
                "                                   <li><a href= 'sprRelojChecador.aspx'> Reloj Checador</a></li>" +
                "                                   <li><a href= 'sprDetalleHoras.aspx'> Detalle de horas</a></li>" +
                "                                   <li><a href= 'sprReporteCaducados.aspx'> Trámites caducados</a></li>" +
                "                                   <li><a href= 'sprReporteSelProcesado.aspx'> Procesable </ a ></ li >" +
                "                                       <li><a href= 'sprTAT.aspx'> TAT </a></li>" +
                "                                       <li><a href= 'sprEticaCumplimiento.aspx'> Ética y Cumplimiento</a></li>" +
                "                                   <li><a href = 'sprReingresos.aspx' > Reingresos </a></li>" +
                "                                   </ul>" +
                "                                </li>" +
                "                                <li class='dropdown'><a data-toggle='dropdown' class='dropdown-toggle'>Opciones<span class='caret'></span></a>" +
                "						            <ul class='dropdown-menu'> " +
                "                                       <li><a href = 'subirAgentes.aspx' > Catálogo Agentes</a></li>" +
                "                                       <li><a href = 'subirLaboratorios.aspx' > Catálogo Laboratorios</a></li>" +
                "					                </ul>" +
                "				                 </li>" +
                "                                <li class='dropdown'><a data-toggle='dropdown' class='dropdown-toggle'> Salir <span class='caret'></span></a>" +
                "						            <ul class='dropdown-menu'> " +
                "							            <li><a href = '../salir.aspx?salida=1'> Regreso aplicaciones </a></li>" +
                "                                       <li><a href = '../salir.aspx?salida=2'> Terminar sesión </a></li>" +
                "					                </ul>" +
                "				                 </li>" +
                "				            </li>" +
                "                        </ul>" +
                "                    </div>" +
                "                </nav>";
            }
            if (modulo == E_Modulo.Monitor && grupo == E_CredencialGrupo.Operador)
            {
                menu = "";
            }
            if (modulo == E_Modulo.MesaAyuda)
            {
                menu =
                "<div id='cssmenu'>" +
                "   <ul>" +
                "       <li><a href='esperaMesaAyuda.aspx' style='height: 16px; line-height: 16px;'>Inicio</a></li>" +
                "       <li><a href='buscaTramite.aspx' style='height: 16px; line-height: 16px;'><span>Buscar trámite</span></a></li>" +
                "       <li><a href='maGestionIncidencias.aspx' style='height: 16px; line-height: 16px;'><span>Gestión incidencias</span></a></li>" +
                "       <li class='has-sub'><a href='#' style='height:16px;line-height:16px;'><span> Salir </span></a>" +
                "           <ul>" +
                "               <li><a href='../salir.aspx?salida=1' style='height:16px;line-height:16px;'> Regreso aplicaciones </a></li>" +
                "               <li><a href='../salir.aspx?salida=2' style='height:16px;line-height:16px;'> Terminar sesión </a></li>" +
                "           </ul>" +
                "       </li>" +
                "   </ul>" +
                "</div>";
            }
            return menu;
        }

        /// <summary>
        /// Obtiene los datos para un menú desde una base de datos
        /// </summary>
        /// <param name="modulo">Número de módulo</param>
        /// <param name="grupo">Número de grupo</param>
        /// <returns>Lista con los datos</returns>
        public List<MenuPropiedades> MenuDinamicoObtener(int idrol)
        {
            List<MenuPropiedades> menutotal = new List<MenuPropiedades>();
            string consulta = "SELECT b.IdMenu, b.Descripcion, b.URL, b.PerteneceA, b.Activo, b.Orden " +
            "FROM PermisosMenu a, menu b " +
            "WHERE a.IdMenu = b.IdMenu " +
            "AND a.IdRol=@idrol " +
            "AND a.Activo=1 " +
            "ORDER BY b.Orden"; 

            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@idrol", idrol, SqlDbType.Int);
            var reader = b.ExecuteReader();

            while (reader.Read())
            {
                MenuPropiedades menuitems = new MenuPropiedades()
                {
                    IdMenu = int.Parse(reader[0].ToString()),
                    PerteneceA = int.Parse(reader[3].ToString()),
                    Descripcion = reader[1].ToString(),
                    URL = reader[2].ToString() ?? "#",
                    //Modulo = int.Parse(reader[4].ToString()),
                    //Grupo = int.Parse(reader[5].ToString())
                };
                menutotal.Add(menuitems);
            }
            reader = null;
            b.ConnectionCloseToTransaction();
            return menutotal;
        }

        public List<MenuPropiedades> MenuDinamicoPorRolObtener(int idrol)
        {
            List<MenuPropiedades> menutotal = new List<MenuPropiedades>();
            string consulta = "SELECT * FROM PermisosMenu a RIGHT JOIN Menu b ON a.IdMenu = b.IdMenu WHERE a.IdRol=@idrol";
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@idrol", idrol, SqlDbType.Int);
            var reader = b.ExecuteReader();

            while (reader.Read())
            {
                MenuPropiedades menuitems = new MenuPropiedades()
                {
                    IdMenu = int.Parse(reader[0].ToString()),
                    PerteneceA = int.Parse(reader[6].ToString()),
                    Descripcion = reader[4].ToString() == null ? "" : reader[4].ToString(),
                    URL = reader[5].ToString() == null ? "" : reader[5].ToString(),
                    //Modulo = reader[7].ToString() == null ? 0 : int.Parse(reader[7].ToString()),
                    //Grupo = reader[8].ToString() == null ? 0 : int.Parse(reader[8].ToString()),
                    Activo = bool.Parse(reader[2].ToString())
                };
                menutotal.Add(menuitems);
            }
            reader = null;
            b.ConnectionCloseToTransaction();
            return menutotal;
        }

        public void MenuLlenar_TreeView(ref TreeView treeview)
        {
            LlenarControles.LlenarTreeViewMenu(MenuDinamicoObtener(1), null, ref treeview);
        }

        public void MenuLlenarPorRol_TreeView(ref TreeView treeview, int idrol)
        {
            LlenarControles.LlenarTreeViewMenu(MenuDinamicoPorRolObtener(idrol), null, ref treeview);
        }

        public int MenuActualizarPermisos(int activo, int idrol, int idmenu)
        {
            string consulta = "UPDATE permisosmenu SET Activo=@activo WHERE idmenu=@idmenu AND IdRol=@idrol";
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@activo", activo, SqlDbType.Int);
            b.AddParameter("@idmenu", idmenu, SqlDbType.Int);
            b.AddParameter("@idrol", idrol, SqlDbType.Int);
            return b.InsertUpdateDelete();
        }

        private DataTable MenusObtener()
        {
            string consulta = "SELECT DISTINCT b.IdModulo, b.Descripcion FROM menu a, modulos b WHERE a.Modulo = b.IdModulo";
            b.ExecuteCommandQuery(consulta);
            return b.Select();
        }

        /// <summary>
        /// Para contar cuantas opciones de menu agregar a un nuevo rol
        /// </summary>
        /// <returns>Filas obtenidas</returns>
        public int ContarOpciones()
        {
            string consulta = "SELECT COUNT(IdMenu) FROM menu";
            b.ExecuteCommandQuery(consulta);
            return int.Parse(b.SelectString());
        }

        public void MenusObtener_DropDownList(ref DropDownList dropdownlist)
        {
            LlenarControles.LlenarDropDownList(ref dropdownlist, MenusObtener(), "Descripcion", "IdModulo");
        }

        public int MenuPermisosGuardar(int idmenu, int idrol)
        {
            string consulta = "INSERT INTO PermisosMenu VALUES(@idmenu, @idrol, 0)";
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@idmenu", idmenu, SqlDbType.Int);
            b.AddParameter("@idrol", idrol, SqlDbType.Int);
            return b.InsertUpdateDelete();
        }

        public void CrearMenuDinamico()
        {
            string menuConstruido = "<div id='cssmenu'><ul>";

            foreach (var padre in MenuDinamicoObtener(1))
            {
                if (int.Parse(padre.PerteneceA.ToString()) == 0)
                    menuConstruido += "<li class='has-sub'><a href='" + padre.URL + "'>" + padre.Descripcion + "</a>";

                //else if (int.Parse(padre.IdPerteneceA.ToString()) > 0)
                //{
                //menuConstruido += "<u>";
                //foreach (var hijos1 in menu.obtenerMenuDinamico(modulo, grupo))
                //{
                //    if (hijos1.IdPerteneceA.ToString() == padre.IdMenu.ToString())
                //    {
                //        menuConstruido += "<li class='has-sub'><a href='" + hijos1.Ruta + "'>" + hijos1.Nombre + "</a></li>";
                //    }                        
                //}
                //menuConstruido += "</u>";
                //}
                menuConstruido += " </li>";
            }

            menuConstruido += "</ul></div>";



           
        }

        /// <summary>
        /// Menú que se construye dinámicamente por base de datos (3 subniveles)
        /// </summary>
        /// <returns>cadena con el menú construído</returns>
        public string CrearMenuHTML(int idrol)
        {
            List<MenuPropiedades> mp = new List<MenuPropiedades>();

            mp = MenuDinamicoObtener(idrol);

            string menuConstruido = "<div id='cssmenu'><ul>";

            foreach (var padre in mp)
            {
                if (int.Parse(padre.PerteneceA.ToString()) == 0)
                {
                    menuConstruido += "<li class='has-sub'><a href='" + padre.URL + "'>" + padre.Descripcion + "</a>";

                    menuConstruido += "<ul>";
                    foreach (var hijo1 in mp)
                    {
                        if (int.Parse(hijo1.PerteneceA.ToString()) == padre.IdMenu)
                        {
                            menuConstruido += "<li class='has-sub'><a href='" + hijo1.URL + "'>" + hijo1.Descripcion + "</a>";

                            menuConstruido += "<ul>";
                            foreach (var hijo2 in mp)
                            {
                                if (int.Parse(hijo2.PerteneceA.ToString()) == hijo1.IdMenu)
                                {
                                    menuConstruido += "<li class='has-sub'><a href='" + hijo2.URL + "'>" + hijo2.Descripcion + "</a>";

                                    menuConstruido += "<ul>";
                                    foreach (var hijo3 in mp)
                                    {
                                        if (int.Parse(hijo3.PerteneceA.ToString()) == hijo2.IdMenu)
                                        {
                                            menuConstruido += "<li><a href='" + hijo3.URL + "'>" + hijo3.Descripcion + "</a></li>";
                                        }
                                    }
                                    menuConstruido += "</ul></li>";
                                }
                            }
                            menuConstruido += "</li></ul>";
                        }
                    }
                    menuConstruido += "</ul></li>";
                }
            }

            menuConstruido += "</ul></div>";

            return menuConstruido;
        }
    }

    public class MenuPropiedades
    {
        public int IdMenu { get; set; }
        public int PerteneceA { get; set; }
        public string Descripcion { get; set; }
        public string URL { get; set; }
        public int Modulo { get; set; }
        public int Grupo { get; set; }
        public bool Activo { get; set; }
        public int Orden { get; set; }
    }
}