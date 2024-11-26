using DevExpress.Internal.WinApi.Windows.UI.Notifications;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Dynamic;
using System.Text;
using System.Web;

namespace wfiplib
{
    public class Reportes
    {
        public DataTable ListaMotivosRechazo(int idMesa, string TipoRechazo)
        {
            DataTable dt = new DataTable();
            bd listaMotivosRechazo = new bd();
            string qListaMotivosRechazo = "SELECT id,idParent,motivoRechazo " +
                                          "FROM MotivosRechazo " +
                                          "WHERE idMesa =" + idMesa.ToString() + " AND idTipoRechazo = (SELECT Id FROM cat_TiposRechazo WHERE Nombre = '" + TipoRechazo + "') AND Activo = 1";

            dt = listaMotivosRechazo.leeDatos(qListaMotivosRechazo);
            listaMotivosRechazo.cierraBD();
            return dt;
        }

        public DataTable ListaMotivosRechazo(string idMesa,int idTipoRechazo)
        {
            DataTable dt = new DataTable();
            bd listaMotivosRechazo = new bd();
            string qListaMotivosRechazo = "SELECT id,idParent,motivoRechazo " +
                                          "FROM MotivosRechazo "+
                                          "WHERE idMesa =" + idMesa + " AND Activo = 1 AND idTipoRechazo =" + idTipoRechazo;

            dt=listaMotivosRechazo.leeDatos(qListaMotivosRechazo);
            listaMotivosRechazo.cierraBD();
            return dt;
        }
        
        public DataTable usuarios()
        {
            DataTable dt = new DataTable();
            bd ListaUsuarios = new bd();
            string qListaUsarios = "SELECT DISTINCT Nombre FROM usuarios";
            dt=ListaUsuarios.leeDatos(qListaUsarios);
            ListaUsuarios.cierraBD();
            return dt;
        }

        public DataTable mesas (int idFlujo)
        {
            DataTable dt = new DataTable();
            bd ListaMesas = new bd();
            string qMesas = "SELECT DISTINCT Nombre FROM mesa WHERE IdEstado=1"; 
            dt = ListaMesas.leeDatos(qMesas);
            ListaMesas.cierraBD();
            return dt;
        }
        public DataTable UltimoEstatusTramite(DateTime fechaDesde, DateTime fechaHasta, int idFlujo)
        {
            string tabla = string.Empty;
            switch (idFlujo)
            {
                case 1:
                    tabla = "TRAM00003P";
                    break;
                case 7:
                    tabla = "TRAM00003P";
                    break;
                default:
                    tabla = "TRAM00003P";
                    break;
            }

            DataTable dt = new DataTable();
            bd uEstatusTramite = new bd();
            string formato = "dd/MM/yyyy";
            string fechaD = fechaDesde.ToString(formato);
            string fechaH = fechaHasta.ToString(formato);
            string qUEstatus = "SELECT * FROM ("+
                               "SELECT DISTINCT TV.IdTramite,VT.PromotoriaClave,VT.IdPromotoria,VT.PromotoriaNombre,F.FolioCompuesto AS NumPoliza,(TV.Nombre +' ' + TV.ApPaterno + ' ' + TV.ApMaterno) AS Contratante," +
                               "VT.EstadoNombre AS EstatusActual, " +
                               "dbo.FN_FechaUltimoMovimiento(TV.IdTramite) AS FechaEstatusActual " +
                               "FROM " + tabla + " TV " +
                               "INNER JOIN vw_tramite VT " +
                               "ON TV.IdTramite = VT.Id " +
                               "INNER JOIN Folio F ON TV.IdTramite = F.IdTramite ) A " +
                               "WHERE CONVERT(DATETIME,FechaEstatusActual,103)>=CONVERT(DATETIME,'" + fechaD + "',103) AND CONVERT(DATETIME,FechaEstatusActual,103)<DATEADD(DAY,1,CONVERT(DATETIME,'" + fechaH + "', 103)) "+
                               "ORDER BY CONVERT(DATETIME,FechaEstatusActual,103)";

            dt = uEstatusTramite.leeDatos(qUEstatus);
            uEstatusTramite.cierraBD();
            return dt;
        }

        public DataTable ExcelDetalleMesa(DateTime fechaDesde, DateTime fechaHasta, int idFlujo)
        {
            string formato = "yyyy-MM-dd";
            string fechaD = fechaDesde.ToString(formato);
            string fechaH = fechaHasta.ToString(formato);
            DataTable dt = new DataTable();
            bd ExportarExcel = new bd();
            string qExportarExcel = "EXEC SABANA_GenerarReporte " + "'" + fechaD + "','" + fechaH + "'";
            dt = ExportarExcel.leeDatos(qExportarExcel);
            ExportarExcel.cierraBD();
            return dt;
        }
        public DataTable DetalleMesa(DateTime fechaDesde, DateTime fechaHasta, string idTramite, int idFlujo)
        {
            DataTable dt = new DataTable();
            string formato = "yyyy-MM-dd";
            string fechaD = fechaDesde.ToString(formato);
            string fechaH = fechaHasta.ToString(formato);
            bd detalleMesa = new bd();
            string qDetalleMesa = "SELECT " +
                                  "B.idTramite," +
                                  "B.idMesa," +
                                  "B.MesaNombre," +
                                  "B.idUsuario AS usuario," +
                                  "TM.FechaRegistro AS FechaInicio," +
                                  "B.FechaTermino," +
                                  "CASE B.Estado " +
                                    "WHEN 0 THEN 'En Trámite' " +
                                    "WHEN 1 THEN 'Atención' " +
                                    "WHEN 2 THEN 'Hold' " +
                                    "WHEN 3 THEN 'RevisionHold' " +
                                    "WHEN 4 THEN 'ReingresoHold' "+
                                    "WHEN 5 THEN 'SolicitudApoyo' " +
                                    "WHEN 6 THEN 'ReingresoApoyo' " +
                                    "WHEN 7 THEN 'Pausa' " +
                                    "WHEN 8 THEN 'Procesado' " +
                                    "WHEN 9 THEN 'Rechazado' " +
                                    "WHEN 10 THEN 'Suspendido' " +
                                    "WHEN 11 THEN 'RevisionSuspencion' " +
                                    "WHEN 12 THEN 'ReingresoSuspencion' " +
                                    "WHEN 13 THEN 'PCI' " +
                                    "WHEN 14 THEN 'ReingresoPCI' " +
                                    "WHEN 15 THEN 'RevisionPCI' " +
                                    "WHEN 16 THEN 'Procesable' " +
                                    "WHEN 17 THEN 'NoProcesable' " +
                                    "WHEN 18 THEN 'Complementario' " +
                                    "WHEN 19 THEN 'CMRevisionProspecto' " +
                                    "WHEN 20 THEN 'CMConfirmacionPendiente' " +
                                    "WHEN 21 THEN 'CMCitaProgramada' " +
                                    "WHEN 22 THEN 'CMCitaReProgramada' " +
                                    "WHEN 23 THEN 'CMEnEsperaResult' " +
                                    "WHEN 24 THEN 'InfoCitasMedicas' " +
                                    "WHEN 25 THEN 'ResponseFromMesa' " +
                                    "WHEN 26 THEN 'SendToMesa' " +
                                    "WHEN 27 THEN 'RevPromotoria' " +
                                    "WHEN 28 THEN 'RevPromotoriaKO' " +
                                    "WHEN 29 THEN 'RevPromotoriaOK' " +
                                    "WHEN 30 THEN 'PromotoriaCancela' " +
                                    "WHEN 31 THEN 'Caducado' " +
                                  "END AS EstadoNombre," +
                                  "B.Observacion," +
                                  "B.ObservacionPrivada " +
                                  "FROM vw_tramiteMesa TM " +
                                  "LEFT JOIN vw_bitacora_dos B " +
                                  "ON TM.IdTramite=B.IdTramite AND TM.IdMesa=B.IdMesa AND TM.IdFlujo=B.IdFlujo "+
                                  "WHERE TM.idTramite ='" + idTramite + "'" + // "'AND IdFlujo=" + idFlujo +
                            //    " AND FechaInicio>=CONVERT(DATETIME,'" + fechaD + "',102) AND FechaInicio<DATEADD(DAY,1,CONVERT(DATETIME,'" + fechaH + "', 102)) " +
                                  " AND B.IdMesa<>-1 "+
                                  " ORDER BY TM.FechaRegistro";
            dt = detalleMesa.leeDatos(qDetalleMesa);
            detalleMesa.cierraBD();
            return dt;
        }

        public DataTable DetalleMonitor(int idFlujo, int idStatus)
        {
            DataTable dt = new DataTable();
            bd monitor = new bd();
            string qDetalleMonitor = "SELECT MesaNombre,UsuarioNombre,FechaRegistro,FechaInicio,PromotoriaNombre " +
                                     "from vw_tramiteMesa " +
                                     "WHERE IdEstadoTramite = " + idStatus +   
                                     " AND idFlujo =" + idFlujo;
            dt = monitor.leeDatos(qDetalleMonitor);
            monitor.cierraBD();
            return dt;
        }
        public DataTable TAT (DateTime fechaDesde, DateTime fechaHasta)
        {
            DataTable dt = new DataTable();
            bd RTAT = new bd();
            string formato = "yyyy-MM-dd";
            string fechaD = fechaDesde.ToString(formato);
            string fechaH = fechaHasta.ToString(formato);

            /* string qTAT = "SELECT IdTramite, TipoProceso, Ramo, Folio, PromotoriaClave, PromotoriaNombre, Estatus," +
                           " RCM_TiempoTotal, RCM_UltimoUsuario, RCM_MotivoSuspension, ADM_tiempoTotal, ADM_UltimoUsuario, ADM_MotivoSuspension,"+
                           " RD_TiempoTotal, RD_UltimoUsuario, RD_MotivoSuspension, PLAD_TiempoTotal, PLAD_UltimoUsuario, PLAD_MotivoSuspension,"+
                           " SELEC_TiempoTotal, SELEC_UltimoUsuario, SELEC_MotivoSuspension, RT_tiempoTotal, RT_UltimoUsuario, RT_MotivoSuspension,"+
                           "LATAM_TiempoTotal, LATAM_UltimoUsuario, LATAM_MotivoSuspension, RMED_TiempoTotal, RMED_UltimoUsuario, RMED_MotivoSuspension,"+
                           "CMED_TiempoTotal, CMED_UltimoUsuario, CMED_MotivoSuspension, CAP_TiempoTotal, CAP_UltimoUsuario, CAP_MotivoSuspension,"+
                           "CTRL_TiempoTotal, CTRL_UltimoUsuario, CTRL_MotivoSuspension, EJEC_TiempoTotal, EJEC_UltimoUsuario, EJEC_MotivoSuspension,"+
                           "TATInicial, TATPromotoria, ENDToEND1, ENDToEND2, TotalSuspendido, ObservacionesPublicas, ObservacionesPrivadas,"+
                           "CAL_TiempoTotal, CAL_UltimoUsuario, CAL_MotivoSuspension, KWIK_TiempoTotal, KWIK_UltimoUsuario, KWIK_MotivoSuspension"+
                           " FROM TAT('" + fechaD + "','" + fechaH + "')";*/
            string qTAT = " SELECT *  FROM TAT('" + fechaD + "','" + fechaH + "')";
            dt = RTAT.leeDatos(qTAT);
            RTAT.cierraBD();
            return dt;

        }
        public DataTable TAT(DateTime fechaDesde, DateTime fechaHasta, string status, int idFlujo, int modo)
        {
            DataTable dt = new DataTable();
            bd estatusTramite = new bd();
            string formato = "yyyy-MM-dd";
            string fechaD = fechaDesde.ToString(formato);
            string fechaH = fechaHasta.ToString(formato);
            string campos = string.Empty;
            string agrupar = string.Empty;
            string qTramite = string.Empty;
            string fechaFiltro = string.Empty;
            switch (modo)
            {
                case 1:
                    fechaFiltro = "FechaIngreso";
                    campos = "PromotoriaNombre AS PROMOTORIA";
                    agrupar = "PromotoriaNombre";
                    break;
                case 3:
                    fechaFiltro = "FechaEstatus";
                    campos = "PromotoriaNombre AS PROMOTORIA";
                    agrupar = "PromotoriaNombre";
                    break;
            }
            if (modo == 1 || modo == 3)
            {
                qTramite = "SELECT Id,IdTipoTramite,FechaIngreso,FolioTramite,Ramo,Producto,Estatus,FechaEstatus,Tiempo,IdPromotoria,PromotoriaClave,Promotoria,ClaveAgente,Poliza,"+
                           "CASE prioridad " +
                             "WHEN 2 THEN 'SUPERVISIÓN' " +
                             "WHEN 3 THEN 'GRANDES SUMAS' " +
                             "WHEN 4 THEN 'HOMBRES CLAVE' " +
                             "ELSE 'NORMAL'" +
                           "END AS prioridad " +
                           "FROM " +
                               "(SELECT T.Id,T.IdTipoTramite,CONVERT(DATE,T.FechaRegistro) AS FechaIngreso,F.FolioCompuesto AS FolioTramite," +
                                      "CRT.Nombre AS Ramo,CP.Nombre AS Producto,VT.EstadoNombre AS Estatus," +
                                      "ISNULL(CONVERT(DATE,Max(VB.FechaTermino)), '') AS FechaEstatus,DATEDIFF(hh, Max(VB.FechaTermino)," +
                                      "GETDATE()) AS Tiempo,T.IdPromotoria AS IdPromotoria,VT.PromotoriaClave, VT.PromotoriaNombre AS Promotoria,T.AgenteClave AS ClaveAgente," +
                                      "T.IdSisLegados AS Poliza,VT.prioridad " +
                               "FROM Tramite T, Folio F, cat_ramosTramite CRT, Producto P, cat_producto CP, vw_tramite VT,vw_bitacora_dos VB " +
                               "WHERE T.Id = F.IdTramite " +
                               "AND T.idRamo = CRT.id AND T.Id = P.IdTramite " +
                               "AND P.IdCatProducto = CP.IdCatProducto " +
                               "AND T.IdTipoTramite = CP.Id_TipoTramite " +
                               "AND T.Id = vt.Id " +
                               "AND T.Id = VB.IdTramite " +
                               "AND VT.EstadoNombre IN(" + status + ") " +
                               "GROUP BY t.Id, T.IdTipoTramite,T.FechaRegistro, F.FolioCompuesto,CRT.Nombre,CP.Nombre,VT.EstadoNombre,T.IdPromotoria,VT.PromotoriaClave,VT.PromotoriaNombre,T.AgenteClave,T.IdSisLegados, VT.prioridad) B " +
                            "WHERE " + fechaFiltro + ">= CONVERT(DATETIME, '" + fechaD + "', 102) AND " + fechaFiltro + "< DATEADD(DAY,1,CONVERT(DATETIME, '" + fechaH + "', 102)) ";
            }
            else
            {
                campos = "Estado as ESTADO, EstadoNombre AS ESTATUS";
                agrupar = "Estado,EstadoNombre";
                qTramite = "SELECT " + campos + ", COUNT(*) AS TOTAL " +
                           "FROM vw_tramite " +
                           "WHERE estado =" + status + // + " AND FechaRegistro>=CONVERT(DATETIME,'" + fechaD + "',102) AND FechaRegistro< DATEADD(DAY,1, CONVERT(DATETIME,'" + fechaH + "', 102)) " +
                           " GROUP BY " + agrupar;
            }

            dt = estatusTramite.leeDatos(qTramite);
            estatusTramite.cierraBD();
            return dt;

        }
        public DataTable EstadosTramite()
        {
            DataTable dt = new DataTable();
            bd estados = new bd();
            string qEstados = "SELECT DISTINCT Estado,EstadoNombre FROM vw_tramite";
            dt = estados.leeDatos(qEstados);
            estados.cierraBD();
            return dt;
        }

        public DataTable TramiteSemana(int mes, int annio, int idFlujo)
        {
            string Flujos = "";
            if (idFlujo == 1)
            {
                Flujos = "1,2,3";     // Flujos definidos
            }
            else
            {
                Flujos = idFlujo.ToString();
            }

            DataTable dt = new DataTable();
            bd tramiteSemanal = new bd();
            string qTramite = "SELECT DS.SEMANA," +
                             "LEFT(CAST(DS.FECHA AS VARCHAR(10)),10) AS FECHA," +
                             "DS.DIA," +
                             "ISNULL(B.TRAMITE, 0) AS TRAMITES " +
                             "FROM FN_DiasSemana(" + annio + "," + mes + ") DS " +
                             "LEFT JOIN " +
                                "(SELECT A.FECHA, SUM(A.TRAMITE) AS TRAMITE FROM " +
                                    "(SELECT " +
                                        "CAST(FechaRegistro AS DATE) AS FECHA," +
                                        "COUNT(*) AS TRAMITE " +
                                     "FROM vw_tramite " +
                                     "WHERE vw_tramite.IdTipoTramite IN (" + Flujos + ") " +
                                     "AND MONTH(FechaRegistro) =" + mes + " AND YEAR(FechaRegistro) =" + annio + 
                                     " GROUP BY  FechaRegistro) A " +
                                "GROUP BY A.FECHA, A.TRAMITE) B " +
                             "ON DS.FECHA = B.FECHA  ORDER BY DS.SEMANA DESC";
            dt = tramiteSemanal.leeDatos(qTramite);
            tramiteSemanal.cierraBD();
            return dt;
        }
        public DataTable TotalTramiteSemana(int mes, int annio, int idFlujo)
        {
            string Flujos = "";
            if (idFlujo == 1)
            {
                Flujos = "1,2,3";     // Flujos definidos
            }
            else
            {
                Flujos = idFlujo.ToString();
            }
            DataTable dt = new DataTable();
            bd tramiteSemanal = new bd();
            string qTotalTramite = "SELECT SEMANA,SUM(TRAMITES) AS TRAMITES FROM " +
                                       "(SELECT DS.SEMANA," +
                                               "LEFT(CAST(DS.FECHA AS VARCHAR(10)),10) AS FECHA," +
                                               "DS.DIA," +
                                               "ISNULL(B.TRAMITE, 0) AS TRAMITES " +
                                               "FROM FN_DiasSemana(" + annio + "," + mes + ") DS " +
                                               "LEFT JOIN " +
                                                "(SELECT A.FECHA, SUM(A.TRAMITE) AS TRAMITE FROM " +
                                                    "(SELECT " +
                                                        "CAST(FechaRegistro AS DATE) AS FECHA," +
                                                        "COUNT(*) AS TRAMITE " +
                                                     "FROM vw_tramite " +
                                                     "WHERE vw_tramite.IdTipoTramite IN (" + Flujos + ") " +
                                                     "AND MONTH(FechaRegistro) =" + mes + " AND YEAR(FechaRegistro) =" + annio + 
                                                     " GROUP BY  FechaRegistro) A " +
                                               "GROUP BY A.FECHA, A.TRAMITE) B " +
                                         "ON DS.FECHA = B.FECHA) C " +
                                    "GROUP BY SEMANA";
            dt = tramiteSemanal.leeDatos(qTotalTramite);
            tramiteSemanal.cierraBD();
            return dt;
        }
        public DataTable PorcentajeMotivosSuspension(DateTime fechaDesde, DateTime fechaHasta, int idFlujo)
        {
            DataTable dt = new DataTable();
            bd datosMotivoSuspension = new bd();
            string formato = "yyyy-MM-dd";
            string fechaD = fechaDesde.ToString(formato);
            string fechaH = fechaHasta.ToString(formato);
            string qMotivos = "SELECT MOTIVO," +
                              "ISNULL(ENERO,0) AS ENERO," +
                              "ISNULL(FEBRERO,0) AS FEBRERO," +
                              "ISNULL(MARZO,0) AS MARZO," +
                              "ISNULL(ABRIL,0) AS ABRIL," +
                              "ISNULL(MAYO,0) AS MAYO," +
                              "ISNULL(ABRIL,0) AS ABRIL," +
                              "ISNULL(MAYO,0) AS MAYO," +
                              "ISNULL(JUNIO,0) AS JUNIO," +
                              "ISNULL(JULIO,0) AS JULIO," +
                              "ISNULL(AGOSTO,0) AS AGOSTO," +
                              "ISNULL(SEPTIEMBRE,0) AS SEPTIEMBRE," +
                              "ISNULL(OCTUBRE,0) AS OCTUBRE," +
                              "ISNULL(NOVIEMBRE,0) AS NOVIEMBRE," +
                              "ISNULL(DICIEMBRE,0) AS DICIEMBRE FROM FN_PorcientoMotivosSuspension ('" + fechaD + "','" + fechaH + "'," + idFlujo + ")";
            dt = datosMotivoSuspension.leeDatos(qMotivos);
            datosMotivoSuspension.cierraBD();
            return dt;
                    
        }
        public DataTable PorcentajeSuspendidos(DateTime fechaDesde, DateTime fechaHasta, int idFlujo)
        {
            string formato = "yyyy-MM-dd";
            string fechaD = fechaDesde.ToString(formato);
            string fechaH = fechaHasta.ToString(formato);
            DataTable dt = new DataTable();
            bd datosPorcSusp = new bd();
            string qSuspendidos = "SELECT * from FN_PorcientoSuspension('" + fechaD + "','" + fechaH + "'," + idFlujo + ")";
            dt = datosPorcSusp.leeDatos(qSuspendidos);
            return dt;
        }
        public DataTable Tendencia(int annio, int idFlujo)
        {
            DataTable dt = new DataTable();
            bd datosTendencia = new bd();
            dt = datosTendencia.leeDatos("SELECT * FROM FN_Tendencia(" + annio + "," + idFlujo +")");
            datosTendencia.cierraBD();
            return dt;
        }
        public DataTable Reingresos(DateTime fechaDesde, DateTime fechaHasta, string Estatus)
        {
            string formato = "yyyy-MM-dd";
            string fechaD = fechaDesde.ToString(formato);
            string fechaH = fechaHasta.ToString(formato);
            string filtro = string.Empty;
            switch (Estatus)
            {
                case "1":
                    filtro = "VT.EstadoNombre=VT.EstadoNombre";
                    break;
                case "2":
                    filtro = "VT.Estado IN(1, 2, 3, 4, 5, 6, 7, 9, 10, 11, 12)";
                    break;
            }
            DataTable dt = new DataTable();
            bd bReingresos = new bd();
            string qReingresos = "SELECT " +
                                "FolioCompuesto," +
                                "FechaRegistro AS IngresoSistema," +
                                "EstadoNombre AS EstadoTramite," +
                                "dbo.FN_ContarReingresosTramite(VT.Id,VT.IdFlujo) AS TieneReingresos," +
                                "dbo.FN_ContarReingresosTramiteTotal(VT.Id,VT.IdFlujo) AS Reingresos," +
                                "Flujo " +
                                "FROM vw_tramite VT " +
                                "INNER JOIN Folio F " +
                                "ON VT.Id = F.IdTramite " +
                                "WHERE VT.FechaRegistro>= CONVERT(DATETIME,'" + fechaD + "',102) AND VT.FechaRegistro<= CONVERT(DATETIME,'" + fechaH + "',102) " +
                                "AND "+ filtro;
            dt= bReingresos.leeDatos(qReingresos);
            bReingresos.cierraBD();
            return dt;
        }
        public DataTable GeneralTotales(DateTime fechaDesde, DateTime fechaHasta, int idFlujo)
        {
            string formato = "yyyy-MM-dd";
            string fechaD = fechaDesde.ToString(formato);
            string fechaH = fechaHasta.ToString(formato);

            DataTable dt = new DataTable();
            bd datosToT = new bd();
            dt = datosToT.leeDatos("SELECT EstadoNombre AS Descripcion, Count(Estado) AS Totales, CONVERT(DECIMAL(10, 2)," +
                                  "(COUNT(Estado)) * 100 / CAST(SUM(Count(*)) OVER() AS FLOAT)) AS Porcentaje " +
                                  "FROM vw_tramite  WHERE FechaRegistro>=CONVERT(DATETIME,'"+ fechaD+ "', 102) AND FechaRegistro< DATEADD(DAY,1,CONVERT(DATETIME,'" + fechaH + "', 102))" +
                                  " GROUP BY EstadoNombre"); 

            datosToT.cierraBD();
            return dt;
        }
        public DataTable GeneralPorcentajes()
        {

            DataTable dt = new DataTable();
            bd datosToT = new bd();
            dt = datosToT.leeDatos("SELECT EstadoNombre AS Descripcion, CONVERT(DECIMAL(10, 2)," +
                                  "(COUNT(Estado)) * 100 / CAST(SUM(Count(*)) OVER() AS FLOAT)) AS Porcentaje " +
                                  "FROM vw_tramite WHERE MONTH(FechaRegistro) = MONTH(GETDATE()) AND YEAR(FechaRegistro) = YEAR(GETDATE()) GROUP BY EstadoNombre");

            datosToT.cierraBD();
            return dt;
        }
        public DataTable TramitesEsperaResultados(int idFlujo)
        {
            DataTable dt = new DataTable();
            string qTramites = "SELECT F.FolioCompuesto as Folio,VT.PromotoriaNombre AS Promotoria FROM vw_tramite VT " +
                        "INNER JOIN Folio F " +
                        "ON VT.Id = F.idFolio " +
                        "WHERE Estado = 9 AND VT.IdFlujo="+ idFlujo;
            bd tramitesEnEspera = new bd();
            dt=tramitesEnEspera.leeDatos(qTramites);
            tramitesEnEspera.cierraBD();
            return dt;
        }
        public DataTable MapaSupervisor(int idFlujo,string mesa)
        {
            DataTable dt = new DataTable();
            bd mapaSupervisor = new bd();
            string qMapaSupervisor = "SELECT * FROM FN_MapaSupervisorMesa ('" + mesa + "'," + idFlujo + ")";
            dt = mapaSupervisor.leeDatos(qMapaSupervisor);
            mapaSupervisor.cierraBD();
            return dt;
        }
        public DataTable MapaSupervisorDetalle(int idMesa,int idFlujo)
        {
            DataTable dt = new DataTable();
            bd MapaSupervisorD = new bd();
            string qDatos = string.Empty;
                     qDatos="SELECT "+
                               "A.IdMesa," +
                               "A.MesaNombre," +
                               "A.EstadoMesa," +
                               "A.prioridadDesc," +
                               "A.IdTramite," +
                               "A.Estado," +
                               "A.FolioCompuesto,"+
                               "CASE "+
                                  "WHEN A.reingresos> 1 THEN A.reingresos -1 "+
                                  "ELSE 0 "+
                               "END AS Reingreso," +
                               "CAST(A.FechaRegistro AS DATE) AS Fecha,"+
                               "CAST(A.FechaIngreso AS DATE) AS FechaIngreso,"+
                               "CAST(A.FechaFin AS DATE) AS FechaFin,"+
                               "A.Usuario,"+
                               "CONVERT(VARCHAR(50), FLOOR(A.tiempoAtencion / 86400)) +'D:' +" +
                               "CONVERT(VARCHAR(50), FLOOR((A.tiempoAtencion / 3600) - FLOOR(A.tiempoAtencion / 86400) * 24)) + 'H:' +"+
                               "CONVERT(VARCHAR(50), FLOOR((A.tiempoAtencion / 60) - FLOOR(A.tiempoAtencion / 3600) * 60)) + 'M:' +"+
                               "CONVERT(VARCHAR(50), A.tiempoAtencion - FLOOR(A.tiempoAtencion / 60) * 60) + 'S' AS tAtencion,"+
                               "CONVERT(VARCHAR(50), FLOOR(A.tiempoMesa / 86400)) +'D:' +"+
                               "CONVERT(VARCHAR(50), FLOOR((A.tiempoMesa / 3600) - FLOOR(A.tiempoMesa / 86400) * 24)) + 'H:' +"+
                               "CONVERT(VARCHAR(50), FLOOR((A.tiempoMesa / 60) - FLOOR(A.tiempoMesa / 3600) * 60)) + 'M:' +"+
                               "CONVERT(VARCHAR(50), A.tiempoMesa - FLOOR(A.tiempoMesa / 60) * 60) + 'S' AS tMesa,"+
                               "A.Contratante, A.Solicitante "+
                           "FROM " +
                               "("+
                                 "SELECT "+
                                   "VTM.idTramite,"+
                                   "F.FolioCompuesto,"+
                                   "VTM.Estado,"+
                                   "VTM.idMesa,"+
                                   "VTM.MesaNombre,"+
                                   "VTM.EstadoNombre AS EstadoMesa,"+
                                   "CASE VTM.prioridad " +
                                        "WHEN 1 THEN 'SUPERVISOR' " +
                                        "WHEN 2 THEN 'SISTEMA' " +
                                        "WHEN 3 THEN 'GRANDES SUMAS' " +
                                        "WHEN 4 THEN 'HOMBRES CLAVE' " +
                                        "WHEN 5 THEN 'NORMAL' " +
                                   "END AS prioridadDesc," +
                                   "dbo.FN_ContarReingresos(VTM.idMesa, VTM.IdTramite) AS reingresos," +
                                   "(SELECT FechaRegistro FROM vw_tramite WHERE id=VTM.IdTramite) AS FechaIngreso,"+
                                   "VTM.FechaRegistro," +
                                   "CASE WHEN VTM.FechaFin IS NULL THEN VTM.FechaRegistro ELSE VTM.FechaFin END as FechaFin," +
                                   "U.Usuario," +
                                   "(TP.Nombre + ' ' + TP.ApPaterno + ' ' + TP.ApMaterno) AS Contratante," +
                                   "(TP.TitularNombre + ' ' + TP.TitularApPat + ' ' + TP.TitularApMat) AS Solicitante," +
                                   "CASE " +
                                     "WHEN VTM.FechaInicio IS NULL THEN 0 " +
                                     "WHEN VTM.FechaFin IS NULL THEN DATEDIFF(SECOND, VTM.FechaInicio, GETDATE()) " +
                                     "ELSE(DATEDIFF(SECOND, VTM.FechaInicio, VTM.FechaFin)) " +
                                   "END AS tiempoAtencion," +
                                   "CASE " +
                                     "WHEN (dbo.Fn_TiempoMesa(VTM.IdTramite,"+ idMesa+")) IS NULL THEN ABS(DATEDIFF(SECOND,VTM.FechaRegistro,GETDATE())) "+
                                     "ELSE dbo.Fn_TiempoMesa(VTM.IdTramite," + idMesa+") "+
                                   "END AS tiempoMesa " +
                                "FROM vw_tramiteMesa VTM " +
                                "INNER JOIN Folio F ON VTM.IdTramite = F.IdTramite "+
                                "INNER JOIN TRAM00003P TP ON VTM.IdTramite = TP.IdTramite "+
                                "LEFT JOIN  usuarios U ON  VTM.IdUsuario = U.Id AND VTM.IdFlujo IN (SELECT DISTINCT IdFlujo FROM MESA WHERE ID IN (SELECT IdMesa FROM usuariosMesa WHERE IdUsuario = U.Id))" +
                                ") A " +
                           "WHERE A.IdMesa=" + idMesa + " AND A.Estado IN(7,0,1,4,6,12,14,25,28) " +
                           "ORDER BY A.IdMesa,A.IdTramite";
            dt = MapaSupervisorD.leeDatos(qDatos);
            MapaSupervisorD.cierraBD();
            return dt;
        }
        public DataTable DetalleHoras(DateTime FechaDesde, DateTime FechaHasta, int idFlujo, String usuarios)
        {
            DataTable dt = new DataTable();
            bd detalleHoras = new bd();
            string formato = "yyyy-MM-dd";
            string qDetalleHoras ="SELECT A.Estado,A.MesaNombre,A.IdUsuario,A.UsuarioNombre,A.diasHabiles,(A.diasHabiles)*8 AS horasHabiles," +
                                  "A.diasNoHabiles,(A.diasNoHabiles)*16 AS horasNoHabiles,ABS((A.diasNoHabiles)*16-(A.diasHabiles)*8) AS horasEfectivas, A.EstadoTramite,A.tramites FROM " +
                                    "(SELECT " +
                                    "VTM.Estado," +
                                    "VTM.MesaNombre," +
                                    "VTM.IdUsuario," +
                                    "VTM.UsuarioNombre," +
                                    "dbo.fn_GetDiasLaborales('" + FechaDesde.ToString(formato) + "','" + FechaHasta.ToString(formato) + "') AS diasHabiles," +
                                    "dbo.fn_GetDiasNoLaborales('" + FechaDesde.ToString(formato) + "','" + FechaHasta.ToString(formato) + "') AS diasNoHabiles," +
                                    "CASE " +
                                    "WHEN VTM.Estado IN(2, 3, 5, 11, 13, 15, 19, 20, 23, 24, 29) THEN 'ATENDIDOS' " +
                                    "WHEN VTM.Estado IN(1, 4, 6, 12, 14, 22) THEN 'PROCESADOS' " +
                                    "WHEN VTM.Estado IN(7,16,21,22,36) THEN 'PENDIENTES' " +
                                    "END AS EstadoTramite," +
                                    "COUNT(VTM.idTramite) AS tramites " +
                                    "FROM vw_tramiteMesa VTM INNER JOIN Usuarios U ON IdUsuario=U.Id " +
                                   "WHERE VTM.IdFlujo =" + idFlujo + " AND VTM.Estado<>30 " +
                                   "AND U.Nombre IN (" + usuarios + ") "+
                                   "AND VTM.Estado IN(2,3,5,11,13,15,19,20,23,24,29,1,4,6,12,14,22,7,16,21,22,36) " +
                                   "GROUP BY VTM.Estado,VTM.IdUsuario,VTM.MesaNombre,VTM.UsuarioNombre " +
                                   ") " +
                                  "A";
            dt = detalleHoras.leeDatos(qDetalleHoras);
            detalleHoras.cierraBD();
            return dt;
        }
        public DataTable DetalleProductividad(DateTime FechaDesde, DateTime FechaHasta, int idFlujo, string Usuario, string Mesa)
        {
            DataTable dt = new DataTable();
            bd DProductividad = new bd();
            string formato = "yyyy-MM-dd";
            string qDProductividad = "SELECT " +
                                    "VBD.IdTramite,F.FolioCompuesto,VBD.IdUsuario,VBD.UsuarioNombre,VBD.IdMesa,VBD.MesaNombre," +
                                    "CASE " +
                                      "WHEN VBD.Estado IN(8, 16, 21) THEN 'ACEPTADO' " +
                                      "WHEN VBD.Estado = 9 THEN 'RECHAZADO' " +
                                      "WHEN VBD.Estado IN(2, 3, 5, 11, 13, 15, 19, 20, 23, 24, 29) THEN 'EN TRÁMITE' " +
                                      "WHEN VBD.Estado IN(1, 4, 6, 12, 14, 22) THEN 'EN PROCESO' " +
                                      "WHEN VBD.Estado = 7 THEN 'EN PAUSA' " +
                                      "WHEN VBD.Estado = 10 THEN 'SUSPENDIDO' " +
                                    "END AS EstadoNombre," +
                                    "VBD.FechaInicio," +
                                    "VBD.FechaTermino," +
                                    "CONVERT(VARCHAR(50), FLOOR(VBD.tiempo / 86400)) + 'D:' +" +
                                    "CONVERT(VARCHAR(50), FLOOR((VBD.tiempo / 3600) - FLOOR(VBD.tiempo / 86400) * 24)) + 'H:' +" +
                                    "CONVERT(VARCHAR(50), FLOOR((VBD.tiempo / 60) - FLOOR(VBD.tiempo / 3600) * 60)) + 'M:' + " +
                                    "CONVERT(VARCHAR(50), VBD.tiempo - FLOOR(VBD.tiempo / 60) * 60) + 'S' AS tiempo " +
                                    "FROM vw_bitacora_dos VBD " +
                                    "INNER JOIN Folio F " +
                                    "ON VBD.IdTramite = F.IdTramite " +
                                    "WHERE VBD.Estado IN(1, 2, 3, 4, 5, 6,7, 8, 9,10,11, 12, 13, 14, 15, 16, 19, 20, 21, 22, 23, 24, 29) " +
                                    "AND (VBD.FechaInicio >=CONVERT(DATETIME,'" + FechaDesde.ToString(formato) + "',102) AND VBD.FechaInicio < DATEADD(DAY,1,CONVERT(DATETIME,'" + FechaHasta.ToString(formato) + "',102))) " +
                                    "AND VBD.MesaNombre='" + Mesa + "' AND VBD.UsuarioNombre='" + Usuario +"'";
            dt = DProductividad.leeDatos(qDProductividad);
            DProductividad.cierraBD();
            return dt;
        }
        
        public DataTable Productividad(DateTime FechaDesde, DateTime FechaHasta, int idFlujo, string usuarios)
        {
            DataTable dt = new DataTable();
            bd productividad = new bd();
            string formato = "yyyy-MM-dd";
            string qProductividad ="SELECT "+
                                   "B.operador,"+
                                   "B.MesaNombre AS mesaNombre,"+
                                   "B.aceptados AS aceptado,"+
                                   "B.proceso,"+
                                   "B.tramite,"+
                                   "B.rechazo,"+
                                   "B.pausa,"+
                                   "B.suspendido,"+
                                   "B.totalTramites,"+
                                   "CONVERT(VARCHAR(50), FLOOR(B.tiempo / 86400)) + 'D:' +" +
                                   "CONVERT(VARCHAR(50), FLOOR((B.tiempo / 3600) - FLOOR(B.tiempo / 86400) * 24)) + 'H:' +" +
                                   "CONVERT(VARCHAR(50), FLOOR((B.tiempo / 60) - FLOOR(B.tiempo / 3600) * 60)) + 'M:' +"+
                                   "CONVERT(VARCHAR(50), B.tiempo - FLOOR(B.tiempo / 60) * 60) + 'S' AS tiempo " +
                                   "FROM "+
                                   "("+
                                     "SELECT "+
                                      "A.operador,"+
                                      "A.MesaNombre,"+
                                      "SUM(ISNULL(A.aceptados, 0)) AS aceptados,"+
                                      "SUM(ISNULL(A.proceso, 0)) AS proceso,"+
                                      "SUM(ISNULL(a.tramite, 0)) AS tramite,"+
                                      "SUM(ISNULL(a.rechazo, 0)) AS rechazo,"+
                                      "SUM(ISNULL(a.pausa, 0)) AS pausa," +
                                      "SUM(ISNULL(a.suspendido, 0)) AS suspendido," +
                                      "(SUM(ISNULL(A.aceptados, 0)) + SUM(ISNULL(A.proceso, 0)) + SUM(ISNULL(a.tramite, 0))+SUM(ISNULL(a.suspendido, 0)) +SUM(ISNULL(a.rechazo, 0))) AS totalTramites," +
                                      "SUM(A.Tiempo) AS tiempo "+
                                    "FROM "+
                                    "("+
                                       "SELECT BD.UsuarioNombre AS operador, BD.MesaNombre,"+
                                       "CASE "+
                                          "WHEN BD.Estado IN(8, 16, 21) THEN COUNT(BD.Estado) "+
                                       "END AS aceptados,"+
                                       "CASE "+
                                          "WHEN BD.Estado IN(1, 4, 6, 12, 14, 22) THEN COUNT(BD.Estado) "+
                                       "END AS proceso,"+
                                       "CASE "+
                                          "WHEN BD.Estado IN(2, 3, 5, 11, 13, 15, 19, 20, 23, 24, 29) THEN COUNT(BD.Estado) "+
                                       "END AS tramite,"+
                                       "CASE " +
                                          "WHEN BD.Estado=9 THEN COUNT(BD.Estado) " +
                                       "END AS rechazo," +
                                       "CASE " +
                                          "WHEN BD.Estado=10 THEN COUNT(BD.Estado) " +
                                       "END AS suspendido," +
                                       "CASE " +
                                          "WHEN BD.Estado IN(7) THEN COUNT(BD.Estado) " +
                                       "END AS pausa," +
                                       "CASE "+
                                          "WHEN BD.Estado IN(7) THEN 0 "+
                                          "ELSE BD.tiempo "+
                                        "END AS tiempo "+
                                       "FROM vw_bitacora_dos BD " +
                                       "WHERE BD.estado IN(1, 2, 3, 4, 5, 6,7, 8, 9,10, 11, 12, 13, 14, 15, 16, 19, 20, 21, 22, 23, 24, 29) "+
                                        " AND (BD.FechaTermino >=CONVERT(DATETIME,'" + FechaDesde.ToString(formato) + "',102) AND BD.FechaTermino <DATEADD(DAY,1,CONVERT(DATETIME,'" + FechaHasta.ToString(formato) + "',102))) " +
                                       " AND BD.UsuarioNombre IN (" + usuarios + ") GROUP BY BD.UsuarioNombre, BD.MesaNombre, BD.tiempo, BD.Estado"+
                                    ") A " +
                                    "GROUP BY A.operador, A.MesaNombre " +
                                ") B";
            dt = productividad.leeDatos(qProductividad);
            productividad.cierraBD();
            return dt;
        }
        public DataTable EticaCumplimiento(DateTime FechaDesde, DateTime FechaHasta,string RFC,string Nombre)
        {
            DataTable dt = new DataTable();
            if (!string.IsNullOrEmpty(RFC)) RFC = "'%" + RFC + "%'";
            else RFC = "'%'";
            if (!string.IsNullOrEmpty(Nombre)) Nombre = "'%" + Nombre + "%'";
            else Nombre="'%'";
            bd REC = new bd();
            string formato = "yyyy-MM-dd";
            string qEticaCumplimiento = "SELECT " +
                                        "T3P.RFC,"+ 
                                        "(T3P.Nombre + ' ' + T3P.ApPaterno + ' ' + T3P.ApMaterno) AS NombreCompleto,"+
                                        "T3P.Entidad AS Estado,"+
                                        "T3P.SumaAsegurada," +
                                        "F.FolioCompuesto,"+
                                        "VT.FechaRegistro,"+
                                        "VT.PromotoriaClave,"+
                                        "VT.PromotoriaNombre,"+
                                        "VT.Flujo "+
                                        "FROM TRAM00003P T3P, vw_tramite VT,Folio F "+
                                        "WHERE T3P.IdTramite = VT.Id AND T3P.IdTramite = F.IdTramite " +
                                        "AND VT.FechaRegistro>=CONVERT(DATETIME,'" + FechaDesde.ToString(formato) + "',102) AND VT.FechaRegistro<DATEADD(DAY,1,CONVERT(DATETIME,'" + FechaHasta.ToString(formato) + "',102)) "+
                                        "AND (T3P.Nombre + ' ' + T3P.ApPaterno + ' ' + T3P.ApMaterno) LIKE " + Nombre +
                                        " AND T3P.RFC LIKE " + RFC;
            dt = REC.leeDatos(qEticaCumplimiento);
            REC.cierraBD();
            return dt;
        }
        public DataTable relojChecador(DateTime FechaDesde, DateTime FechaHasta)
        {
            DataTable dt = new DataTable();
            bd RC = new bd();
            string formato = "yyyy-MM-dd";
            string qRelojChecador = "SELECT " +
                                   "A.idUsuario," +
                                   "u.Nombre," +
                                   "A.Fecha," +
                                   "(ISNULL(A.Espera,0.0))/86400.0  AS TiempoTotal " +
                                   "FROM " +
                                        "(SELECT " +
                                            "idUsuario," +
                                            "CONVERT(DATE, inicioSesion) as Fecha," +
                                            "SUM(DATEDIFF(SECOND, inicioSesion, FinSesion)) AS Espera " +
                                            "FROM logUsuarios " +
                                            "WHERE (inicioSesion >= CONVERT(DATETIME,'" + FechaDesde.ToString(formato) + "',102) AND inicioSesion<DATEADD(DAY,1,CONVERT(DATETIME,'" + FechaHasta.ToString(formato)+"',102))) " +
                                            "AND IdUsuario NOT IN (0)" +
                                            "GROUP BY idUsuario, CONVERT(DATE, inicioSesion)) A " +
                                   "INNER JOIN usuarios u " +
                                    "ON A.idUsuario = u.Id";

            dt = RC.leeDatos(qRelojChecador);
            RC.cierraBD();
            return dt;
        }

        public DataTable TiemposAtencion(DateTime FechaDesde, DateTime FechaHasta, int idFlujo)
        {
            DataTable dt = new DataTable();
            bd TA = new bd();
            string formato = "yyyy-MM-dd";
            string qTiemposAtencion = string.Empty;
            switch (idFlujo)
            {
                case 1: case 2: case 3:
                    qTiemposAtencion = "SELECT * FROM FN_TiemposAtencionGMM('" + FechaDesde.ToString(formato) + "','" + FechaHasta.ToString(formato) +"'," + idFlujo + ")";
                    break;
                
            }
            
            dt = TA.leeDatos(qTiemposAtencion);
            TA.cierraBD();
            return dt;
        }
        public DataTable DetalleCaducados(DateTime FechaDesde,DateTime FechaHasta,int IdFlujo, int Id)
        {
            DataTable dt = new DataTable();
            string formato = "yyyy-MM-dd";
            bd TC = new bd();
            string qDetalleCaducados = string.Empty;
            qDetalleCaducados = "SELECT IdTramite,MesaNombre,UsuarioNombre,FechaInicio,FechaTermino " +
                              "FROM vw_bitacora_dos WHERE IdTramite=" + Id +
                              " AND FechaInicio>= CONVERT(DATETIME,'" + FechaDesde.ToString(formato) + "', 102) AND FechaInicio< DATEADD(DAY,1,CONVERT(DATETIME,'" + FechaHasta.ToString(formato) + "', 102))";

            dt = TC.leeDatos(qDetalleCaducados);
            TC.cierraBD();
            return dt;
        }
        public DataTable DatosCarta(int idTramite)
        {
            DataTable dt = new DataTable();
            bd datosCarta = new bd();
            //string qDatosCarta = "SELECT tramiteNombre,idRamo,AgenteNombre,PromotoriaNombre  from vw_tramiteP WHERE Id="+ idTramite;
            string qDatosCarta = "SELECT VTP.tramiteNombre,CRT.Nombre,VTP.AgenteNombre,VTP.PromotoriaNombre, VTP.Estado,VTP.Producto," +
                                "(TP.Nombre + ' ' + TP.ApPaterno +' '+TP.ApMaterno) AS Contratante, T.IdSisLegados AS Poliza " +
                                "FROM vw_tramiteP VTP, cat_ramosTramite CRT,TRAM00003P TP, tramite T " +
                                "WHERE VTP.idRamo=CRT.id " +
                                "AND VTP.Id=TP.IdTramite " +
                                "AND VTP.Id=T.Id " +
                                "AND VTP.Id=" + idTramite;

            dt = datosCarta.leeDatos(qDatosCarta);
            datosCarta.cierraBD();
            return dt;
        }

        public string MotivosRechazoFechaRechazado(int idTramite)
        {
            string strResultado = "";
            DataTable dtDatos = new DataTable();
            bd mRechazo = new bd();

            string qMRechazo = "SELECT tramiteRechazo.FechaRegistro " +
            "FROM tramiteMesa, tramiteRechazo " +
            "WHERE tramiteMesa.IdRechazo = tramiteRechazo.Id " +
            "	AND tramiteMesa.IdTramite = " + idTramite + 
            ";";

            dtDatos = mRechazo.leeDatos(qMRechazo);
            if (dtDatos.Rows.Count > 0)
            {
                strResultado = Convert.ToString(dtDatos.Rows[0]["FechaRegistro"]);
                strResultado = strResultado.Substring(6, 4) + "/" + strResultado.Substring(3, 2) + "/" + strResultado.Substring(0, 2);
            }
            dtDatos = null;
            mRechazo.cierraBD();
            return strResultado;
        }

        public string getFechaSuspencion(int idTramite, int idFlujo)
        {
            string strResultado = "";
            DataTable dtDatos = new DataTable();
            bd mRechazo = new bd();
            string strSQL = "SELECT FechaFin FROM tramiteMesa WHERE IdTramite = " + idTramite.ToString() + " AND IdMesa = (SELECT mesa.Id FROM mesa WHERE mesa.Nombre = 'SELECCIÓN' AND mesa.IdFlujo = " + idFlujo.ToString() + ") AND (Estado = (SELECT statusMesa.Id FROM statusMesa WHERE statusMesa.Nombre = 'SolicitudApoyo') OR Estado = (SELECT statusMesa.Id FROM statusMesa WHERE statusMesa.Nombre = 'Suspensión Cita Médica') )";
            dtDatos = mRechazo.leeDatos(strSQL);
            if (dtDatos.Rows.Count > 0)
            {
                strResultado = Convert.ToString(dtDatos.Rows[0]["FechaFin"]);
                strResultado = strResultado.Substring(6, 4) + "/" + strResultado.Substring(3, 2) + "/" + strResultado.Substring(0, 2);
            }
            dtDatos = null;
            mRechazo.cierraBD();
            return strResultado;
        }

        public string getSuspencionObservacion(int idTramite, int idFlujo)
        {
            string strResultado = "";
            DataTable dtDatos = new DataTable();
            bd mRechazo = new bd();
            string strSQL = "SELECT ObservacionPublica FROM tramiteMesa WHERE IdTramite = " + idTramite.ToString() + " AND IdMesa = (SELECT mesa.Id FROM mesa WHERE mesa.Nombre = 'SELECCIÓN' AND mesa.IdFlujo = " + idFlujo.ToString() + ") AND (Estado = (SELECT statusMesa.Id FROM statusMesa WHERE statusMesa.Nombre = 'SolicitudApoyo') OR Estado = (SELECT statusMesa.Id FROM statusMesa WHERE statusMesa.Nombre = 'Suspensión Cita Médica') )";
            dtDatos = mRechazo.leeDatos(strSQL);
            if (dtDatos.Rows.Count > 0)
            {
                strResultado = Convert.ToString(dtDatos.Rows[0]["ObservacionPublica"]);
            }
            dtDatos = null;
            mRechazo.cierraBD();
            return strResultado;
        }

        public string MotivosRechazoFecha(int idTramite)
        {
            string strResultado = "";
            DataTable dtDatos = new DataTable();
            bd mRechazo = new bd();
            //string qMRechazo = "SELECT TOP 5 CMR.motivoRechazo FROM TramiteRechazoMotivos TRM, " +
            //                  "tramiteRechazo TR, TramiteMesa TM, MotivosRechazo CMR " +
            //                  "WHERE TRM.idMotivo = CMR.Id AND TR.ID = TRM.Id AND TR.IdTramiteMesa = TM.Id " +
            //                  " AND TM.IdTramite =" + idTramite +
            //                  " AND TM.IdRechazo > 0";

            string qMRechazo = "SELECT MotivosRechazo.motivoRechazo, tramiteMesa.FechaFin " +
            "FROM tramiteMesa, tramiteRechazo, tramiteRechazoMotivos, MotivosRechazo " +
            "WHERE tramiteMesa.Id = tramiteRechazo.IdTramiteMesa " +
            " AND MotivosRechazo.id = tramiteRechazoMotivos.IdMotivo " +
            " AND tramiteRechazoMotivos.Id = tramiteRechazo.Id " +
            " AND tramiteRechazo.Id = (SELECT MAX(tramiteRechazo.Id) FROM tramiteRechazo WHERE tramiteRechazo.IdTramiteMesa = tramiteMesa.Id) " +
            " AND tramiteMesa.IdTramite = " + idTramite +
            " AND tramiteMesa.IdRechazo > 0 ";

            dtDatos = mRechazo.leeDatos(qMRechazo);
            if (dtDatos.Rows.Count > 0)
            {
                strResultado = Convert.ToString(dtDatos.Rows[0]["FechaFin"]);
                strResultado = strResultado.Substring(6, 4) + "/" + strResultado.Substring(3, 2) + "/" + strResultado.Substring(0, 2);
            }
            dtDatos = null;
            mRechazo.cierraBD();
            return strResultado;
        }

        public string getVigencia(int idTramite, string pTabla)
        {
            string strResultado = "";
            DataTable dtDatos = new DataTable();
            bd mRechazo = new bd();

            string qMRechazo = "SELECT Vigencia FROM " + pTabla + " WHERE IdTramite = " + idTramite.ToString();
            dtDatos = mRechazo.leeDatos(qMRechazo);
            if (dtDatos.Rows.Count > 0)
            {
                strResultado = Convert.ToString(dtDatos.Rows[0]["Vigencia"]);
                strResultado = strResultado.Substring(6, 4) + "/" + strResultado.Substring(3, 2) + "/" + strResultado.Substring(0, 2);
            }
            dtDatos = null;
            mRechazo.cierraBD();
            return strResultado;
        }
        public string getProducto(int idTramite)
        {
            string strResultado = "";
            DataTable dtDatos = new DataTable();
            bd mRechazo = new bd();

            string qMRechazo = "SELECT cat_producto.Nombre FROM cat_producto WHERE IdCatProducto IN (SELECT Producto.IdCatProducto FROM Producto WHERE IdTramite = " + idTramite.ToString() + "); ";
            dtDatos = mRechazo.leeDatos(qMRechazo);
            if (dtDatos.Rows.Count > 0)
            {
                strResultado = Convert.ToString(dtDatos.Rows[0]["Nombre"]);
            }
            dtDatos = null;
            mRechazo.cierraBD();
            return strResultado;
        }

        public DataTable MotivosRechazoObservaciones(int idTramite)
        {
            DataTable dt = new DataTable();
            bd mRechazo = new bd();
            string strSentenciaSQL = "SELECT DISTINCT tramiteMesa.ObservacionPublica " +
                                        "FROM tramiteMesa, tramiteRechazo, tramiteRechazoMotivos, MotivosRechazo " +
                                        "WHERE tramiteMesa.Id = tramiteRechazo.IdTramiteMesa " +
                                        " AND MotivosRechazo.id = tramiteRechazoMotivos.IdMotivo " +
                                        " AND tramiteRechazoMotivos.Id = tramiteRechazo.Id " +
                                        " AND tramiteRechazo.Id = (SELECT MAX(tramiteRechazo.Id) FROM tramiteRechazo WHERE tramiteRechazo.IdTramiteMesa = tramiteMesa.Id) " +
                                        " AND tramiteMesa.IdTramite = " + idTramite +
                                        " AND tramiteMesa.IdRechazo > 0 ";

            dt = mRechazo.leeDatos(strSentenciaSQL);
            mRechazo.cierraBD();
            return dt;
        }

        public DataTable MotivosRechazo(int idTramite)
        {
            DataTable dt = new DataTable();
            bd mRechazo = new bd();
            //string qMRechazo = "SELECT TOP 5 CMR.motivoRechazo FROM TramiteRechazoMotivos TRM, " +
            //                  "tramiteRechazo TR, TramiteMesa TM, MotivosRechazo CMR " +
            //                  "WHERE TRM.idMotivo = CMR.Id AND TR.ID = TRM.Id AND TR.IdTramiteMesa = TM.Id " +
            //                  " AND TM.IdTramite =" + idTramite +
            //                  " AND TM.IdRechazo > 0";

            string qMRechazo = "SELECT MotivosRechazo.motivoRechazo, tramiteMesa.FechaFin " + 
            "FROM tramiteMesa, tramiteRechazo, tramiteRechazoMotivos, MotivosRechazo " + 
            "WHERE tramiteMesa.Id = tramiteRechazo.IdTramiteMesa " + 
            " AND MotivosRechazo.id = tramiteRechazoMotivos.IdMotivo " + 
            " AND tramiteRechazoMotivos.Id = tramiteRechazo.Id " + 
            " AND tramiteRechazo.Id = (SELECT MAX(tramiteRechazo.Id) FROM tramiteRechazo WHERE tramiteRechazo.IdTramiteMesa = tramiteMesa.Id) " + 
            " AND tramiteMesa.IdTramite = " + idTramite + 
            " AND tramiteMesa.IdRechazo > 0 ";

            dt = mRechazo.leeDatos(qMRechazo);
            mRechazo.cierraBD();
            return dt;
        }

        public DataTable GeneralMesa(int IdFlujo, DateTime fechaDesde, DateTime fechaHasta)
        {
            DataTable dt = new DataTable();
            string formato = "yyyy-MM-dd";
            string qMesa = "";
            string fechaD = fechaDesde.ToString(formato);
            string fechaH = fechaHasta.ToString(formato);

            DataTable mesa = new DataTable();
            DataTable mesaTranspuesta = new DataTable();
            DataTable Suspendidos = new DataTable();
            DataTable SuspendidosTrans = new DataTable();
            DataTable Hold = new DataTable();
            DataTable Rechazado = new DataTable();
            DataTable Ejecucion = new DataTable();
            DataTable Registrado = new DataTable();
            DataTable Proceso = new DataTable();

            bd datosGenMesa = new bd();
            //string qMesa = "SELECT Nombre as ESTATUS FROM mesa WHERE idFlujo=" + IdFlujo;
            //string qMesa = "SELECT DISTINCT Nombre as ESTATUS FROM mesa WHERE idEstado = 1";
            if(IdFlujo ==  1)
            {
                qMesa = "SELECT DISTINCT mesa.Nombre as ESTATUS FROM mesa " +
                        "INNER JOIN flujo ON flujo.Id = mesa.IdFlujo " +
                        "WHERE mesa.idEstado = 1 " +
                        "AND flujo.Id IN (1,2,3)";     // Flujos definidos
            }
            else
            {
                qMesa = "SELECT DISTINCT mesa.Nombre as ESTATUS FROM mesa " +
                        "INNER JOIN flujo ON flujo.Id = mesa.IdFlujo " +
                        "WHERE mesa.idEstado = 1 " +
                        "AND flujo.Id = " + IdFlujo;
            }
            
            mesa = datosGenMesa.leeDatos(qMesa);
            mesa.Rows.Add("TOTAL");
            string tSuspendido = "";
            string tHold = "";
            string tRechazado = "";
            string tEjecucion = "";
            //string tRegistrado = "";
            string tProceso = "";

            int indice;

            foreach (DataRow mesaNombre in mesa.Rows)
            {
                if (!string.Equals("TOTAL", mesaNombre[0]))
                {
                    tSuspendido += "SELECT ISNULL(SUM(Total),0) as Suspendidos FROM FN_TotalMesa(" + IdFlujo + ",'" + fechaD + "','" + fechaH + "','" + mesaNombre[0] + "','10') " +
                                "UNION ALL ";
                    tHold += "SELECT ISNULL(SUM(Total),0) as Hold FROM FN_TotalMesa(" + IdFlujo + ",'" + fechaD + "','" + fechaH + "','" + mesaNombre[0] + "','2') " +
                                    "UNION ALL ";
                    tRechazado += "SELECT ISNULL(SUM(Total),0) as Rechazados FROM FN_TotalMesa(" + IdFlujo + ",'" + fechaD + "','" + fechaH + "','" + mesaNombre[0] + "','9') " +
                                    "UNION ALL ";
                    tEjecucion += "SELECT ISNULL(SUM(Total),0) as Ejecucion FROM FN_TotalMesa(" + IdFlujo + ",'" + fechaD + "','" + fechaH + "','" + mesaNombre[0] + "','1, 3, 4, 5, 6, 7, 11, 12')" +
                                    "UNION ALL ";
           //       tRegistrado += "SELECT ISNULL(SUM(Total),0) as Registrado FROM FN_TotalMesa(" + IdFlujo + ",'" + fechaD + "','" + fechaH + "','" + mesaNombre[0] + "','0') " +
           //                       "UNION ALL ";
                    tProceso += "SELECT ISNULL(SUM(Total),0) as Procesado FROM FN_TotalMesa(" + IdFlujo + ",'" + fechaD + "','" + fechaH + "','" + mesaNombre[0] + "','8') " +
                                    "UNION ALL ";
                }
            }

            //Totales
            tSuspendido += "SELECT ISNULL(SUM(Total),0) as Suspendidos FROM FN_TotalMesa(" + IdFlujo + ",'" + fechaD + "','" + fechaH + "','%','10')";
            tHold += "SELECT ISNULL(SUM(Total),0) as Hold FROM FN_TotalMesa(" + IdFlujo + ",'" + fechaD + "','" + fechaH + "','%','2')";
            tRechazado += "SELECT ISNULL(SUM(Total),0) as Rechazados FROM FN_TotalMesa(" + IdFlujo + ",'" + fechaD + "','" + fechaH + "','%','9')";
            tEjecucion += "SELECT ISNULL(SUM(Total),0) as Ejecucion FROM FN_TotalMesa(" + IdFlujo + ",'" + fechaD + "','" + fechaH + "','%','1, 3, 4, 5, 6, 7, 11, 12')";
          //  tRegistrado += "SELECT ISNULL(SUM(Total),0) as Registrado FROM FN_TotalMesa(" + IdFlujo + ",'" + fechaD + "','" + fechaH + "','%','0')";
            tProceso += "SELECT ISNULL(SUM(Total),0) as Procesado FROM FN_TotalMesa(" + IdFlujo + ",'" + fechaD + "','" + fechaH + "','%','8')";

            mesaTranspuesta = GenerateTransposedTable(mesa);
            Suspendidos = datosGenMesa.leeDatos(tSuspendido);
            Hold = datosGenMesa.leeDatos(tHold);
            Rechazado = datosGenMesa.leeDatos(tRechazado);
            Ejecucion = datosGenMesa.leeDatos(tEjecucion);
           // Registrado = datosGenMesa.leeDatos(tRegistrado);
            Proceso = datosGenMesa.leeDatos(tProceso);

            foreach (DataColumn NMesa in mesaTranspuesta.Columns)
            {
                if (!string.Equals(NMesa.Caption, "ESTATUS"))
                {
                    dt.Columns.Add(NMesa.Caption, typeof(Int32));
                }
                else dt.Columns.Add(NMesa.Caption);
            }

            DataRow OSupendidos = dt.NewRow();
            indice = 0;
            foreach (DataColumn HeaderDG in dt.Columns)
            {
                if (string.Equals("ESTATUS", HeaderDG.ColumnName))
                {
                    OSupendidos[HeaderDG.ColumnName] = "SUSPENDIDOS";
                }
                else
                {
                    OSupendidos[HeaderDG.ColumnName] = Suspendidos.Rows[indice][0];
                    indice++;
                }
            }
            dt.Rows.Add(OSupendidos);
            DataRow OHold = dt.NewRow();
            indice = 0;
            foreach (DataColumn HeaderDG in dt.Columns)
            {
                if (string.Equals("ESTATUS", HeaderDG.ColumnName))
                {
                    OHold[HeaderDG.ColumnName] = "HOLD";
                }
                else
                {
                    OHold[HeaderDG.ColumnName] = Hold.Rows[indice][0];
                    indice++;
                }

            }
            dt.Rows.Add(OHold);
            DataRow ORechazados = dt.NewRow();
            indice = 0;
            foreach (DataColumn HeaderDG in dt.Columns)
            {
                if (string.Equals("ESTATUS", HeaderDG.ColumnName))
                {
                    ORechazados[HeaderDG.ColumnName] = "RECHAZADOS";
                }
                else
                {
                    ORechazados[HeaderDG.ColumnName] = Rechazado.Rows[indice][0];
                    indice++;
                }
            }
            dt.Rows.Add(ORechazados);

            DataRow OEjecutados = dt.NewRow();
            indice = 0;
            foreach (DataColumn HeaderDG in dt.Columns)
            {
                if (string.Equals("ESTATUS", HeaderDG.ColumnName))
                {
                    OEjecutados[HeaderDG.ColumnName] = "EN ATENCIÓN";
                }
                else
                {
                    OEjecutados[HeaderDG.ColumnName] = Ejecucion.Rows[indice][0];
                    indice++;
                }
            }
            dt.Rows.Add(OEjecutados);
            DataRow OProcesados = dt.NewRow();
            indice = 0;
            foreach (DataColumn HeaderDG in dt.Columns)
            {
                if (string.Equals("ESTATUS", HeaderDG.ColumnName))
                {
                    OProcesados[HeaderDG.ColumnName] = "PROCESADOS";
                }
                else
                {
                    OProcesados[HeaderDG.ColumnName] = Proceso.Rows[indice][0];
                    indice++;
                }

            }
            dt.Rows.Add(OProcesados);
            datosGenMesa.cierraBD();
            return dt;
        }
        public DataTable ResumenMapa(int idFlujo)
        {
            DataTable dt = new DataTable();
            bd resumenMapa = new bd();
            string qDatos = "SELECT * FROM FN_MapaSupervisor(" + idFlujo + ") ";
            dt = resumenMapa.leeDatos(qDatos);
            resumenMapa.cierraBD();
            return dt;
        }
        public DataTable MapaGeneral(int idFlujo)
        {
            DataTable dt = new DataTable();
            bd mapaGeneral = new bd();
            string qDatos = "SELECT A.Id,A.Nombre,A.TotalUsuarios, COUNT(VTM.IdTramite) AS TotalTramites " +
                            "FROM " +
                            "(" +
                            "SELECT M.Id, M.Nombre, COUNT(UM.IdUsuario) AS TotalUsuarios "+
                                "FROM UsuariosMesa UM " +
                                "INNER JOIN mesa M " +
                                "ON UM.IdFlujo = M.IdFlujo AND UM.IdMesa = M.Id " +
                                "INNER JOIN usuarios U " +
                                "ON UM.IdFlujo = U.IdFlujo AND UM.IdUsuario = U.Id " +
                                "WHERE UM.IdFlujo =" + idFlujo +" AND U.Grupo = 10 AND U.Estado = 1 " +
                                "GROUP BY M.ID, M.Nombre) A " +
                            "LEFT JOIN vw_tramiteMesa VTM " +
                            "ON A.Id = VTM.IdMesa " +
                            "WHERE VTM.Estado IS  NULL OR VTM.Estado <> 13 " +
                            "GROUP BY  A.ID,A.Nombre,A.TotalUsuarios";
            dt = mapaGeneral.leeDatos(qDatos);
            mapaGeneral.cierraBD();
            return dt;
        }

        public DataTable Top10Recepcion(DateTime fechaDesde, DateTime fechaHasta, int idFlujo)
        {
            DataTable dt = new DataTable();
            string formato = "yyyy-MM-dd";
            string fechaD = fechaDesde.ToString(formato);
            string fechaH = fechaHasta.ToString(formato);

            bd datosTop = new bd();
            String qDatos = "SELECT TOP 10 " +
                             "B.Promotoria,B.Zona,B.Nombre,B.Suspendido AS NumTramitesSus,B.Ejecucion AS NumTramitesEje " +
                             "FROM " +
                             " (SELECT PV.Promotoria, PV.Nombre,PV.Zona,[Ejecucion],[Suspendido] " +
                             "  FROM " +
                             "   (SELECT " +
                             "    IdTipoTramite,PromotoriaClave AS Promotoria,PromotoriaNombre AS Nombre,EstadoNombre,Zona " +
                             "    FROM vw_tramite " +
                             "    WHERE Estado IN(3, 5) AND FechaRegistro>=CONVERT(DATETIME,'" + fechaD + "',102) AND FechaRegistro< DATEADD(DAY,1,CONVERT(DATETIME,'" + fechaH + "', 102))  " +
                             "   ) A " +
                             " PIVOT(COUNT(IdTipoTramite) FOR EstadoNombre IN([Ejecucion],[Suspendido])) PV) B " +
                             "ORDER BY NumTramitesEje DESC";

            dt = datosTop.leeDatos(qDatos);
            datosTop.cierraBD();
            return dt;
        }
        
        public DataTable Top10Suspendidos(DateTime fechaDesde, DateTime fechaHasta, int idFlujo)
        {
            DataTable dt = new DataTable();
            string formato = "yyyy-MM-dd";
            string fechaD = fechaDesde.ToString(formato);
            string fechaH = fechaHasta.ToString(formato);

            bd datosTop = new bd();
            string qTopSus = "SELECT TOP 10 " +
                             "B.Promotoria,B.Nombre,B.Zona,B.Suspendido AS NumTramitesSus,B.Ejecucion AS NumTramitesEje " +
                             "FROM " +
                             " (SELECT PV.Promotoria, PV.Nombre,PV.Zona,[Ejecucion],[Suspendido] " +
                             "  FROM " +
                             "   (SELECT " +
                             "    IdTipoTramite,PromotoriaClave AS Promotoria,PromotoriaNombre AS Nombre,EstadoNombre,Zona " +
                             "    FROM vw_tramite " +
                             "    WHERE Estado IN(5, 3) AND FechaRegistro>=CONVERT(DATETIME,'" + fechaD + "',102) AND FechaRegistro<DATEADD(DAY,1,CONVERT(DATETIME,'" + fechaH + "', 102)) " +
                             "   ) A " +
                             " PIVOT(COUNT(IdTipoTramite) FOR EstadoNombre IN([Ejecucion],[Suspendido])) PV) B " +
                             "ORDER BY NumTramitesSus DESC";
            dt = datosTop.leeDatos(qTopSus);
            datosTop.cierraBD();
            return dt;
        }


        private DataTable GenerateTransposedTable(DataTable inputTable)
        {
            DataTable outputTable = new DataTable();

            // Se agregan las columnas haciendo un ciclo para cada fila

            // El encabezado de la primera columna es el mismo. 
            outputTable.Columns.Add(inputTable.Columns[0].ColumnName.ToString());

            // El encabezado para las demas columnas
            foreach (DataRow inRow in inputTable.Rows)
            {
                string newColName = inRow[0].ToString();
                outputTable.Columns.Add(newColName);
            }

            // Se agregan las columnas por cada renglón        
            for (int rCount = 1; rCount <= inputTable.Columns.Count - 1; rCount++)
            {
                DataRow newRow = outputTable.NewRow();

                newRow[0] = inputTable.Columns[rCount].ColumnName.ToString();
                for (int cCount = 0; cCount <= inputTable.Rows.Count - 1; cCount++)
                {
                    string colValue = inputTable.Rows[cCount][rCount].ToString();
                    newRow[cCount + 1] = colValue;
                }
                outputTable.Rows.Add(newRow);
            }

            return outputTable;
        }
        public DataTable Franja(DateTime Fecha, int idFlujo)
        {
            DataTable dt = new DataTable();
            bd datosFranja = new bd();

            int pMes = Fecha.Month;
            int pDia = Fecha.Day;
            int pAnnio = Fecha.Year;
            string Flujos = "";

            if (idFlujo == 1)
            {
                Flujos = "1,2,3";     // Flujos definidos
            }
            else
            {
                Flujos = idFlujo.ToString();
            }

            string qdatosFranja = "SELECT " +
                                  "HL.hora AS Franja," +
                                  "ISNULL(B.ingresados, 0) as ingresados," +
                                  "ISNULL(B.tocados, 0) as tocados," +
                                  "ISNULL(B.ejecutados, 0) as ejecutados " +
                                  "FROM HorarioLaboral HL LEFT JOIN " +
                                  " (SELECT hora,[ingresados],[tocados],[ejecutados] " +
                                  "  FROM " +
                                  "   (SELECT " +
                                  "    DATEPART(HOUR, FechaInicio) AS hora," +
                                  "    CASE " +
                                  "      WHEN Estado = 0 THEN 'ingresados' " +
                                  "      WHEN Estado IN(1, 2, 3, 4, 5, 6, 7, 9, 10, 11, 12) THEN 'tocados' " +
                                  "      WHEN Estado = 8 THEN 'ejecutados' " +
                                  "    END AS Movimiento," +
                                  "    IdTramite " +
                                  "    FROM vw_bitacora_dos " +
                                  "    WHERE vw_bitacora_dos.IdTipoTramite IN (" + Flujos + ") " +
                                  "    AND DATEPART(HOUR, FechaInicio) >= 7 " +
                                  "    AND DATEPART(HOUR, FechaInicio) <= 20 " +
                                  "    AND DAY(FechaInicio) =" + pDia +
                                  "    AND MONTH(FechaInicio) =" + pMes +
                                  "    AND YEAR(FechaInicio) ="+ pAnnio +
                                  "    ) A " +
                                  "  PIVOT(COUNT(IdTramite) FOR Movimiento IN([ingresados],[tocados],[ejecutados])) PV) B " +
                                  "ON HL.hora = B.hora ORDER BY HL.hora";
            dt = datosFranja.leeDatos(qdatosFranja);
            datosFranja.cierraBD();
            return dt;
        }
        public int TotalMesFranja(DateTime Fecha, int idFlujo)
        {
            int resultado;
            int pMes = Fecha.Month;
            int pAnnio = Fecha.Year;
            string Flujos = "";

            if (idFlujo == 1)
            {
                Flujos = "1,2,3";     // Flujos definidos
            }
            else
            {
                Flujos = idFlujo.ToString();
            }

            bd acumuladoMes = new bd();
            string qTotalMes = "SELECT COUNT(*) AS Total FROM vw_tramite WHERE vw_tramite.IdTipoTramite IN (" + Flujos + ") AND MONTH(FechaRegistro)=" + pMes + " AND YEAR(FechaRegistro)=" + pAnnio;
            resultado = acumuladoMes.ejecutaCmdScalar(qTotalMes);
            acumuladoMes.cierraBD();
            return resultado;
        }
        public DataTable ResumenPromotoria(DateTime fechaDesde, DateTime fechaHasta, int idFlujo )
        {
            DataTable dt = new DataTable();
            DataTable idPromotorias = new DataTable();
            string formato = "yyyy-MM-dd";
            string fechaD = fechaDesde.ToString(formato);
            string fechaH = fechaHasta.ToString(formato);

            bd datosResumenPromotoria = new bd();
            string idPromotoria = "SELECT id, Nombre,Clave FROM cat_promotorias";
            string qResumenPromotoria = "SELECT * FROM ( ";
            idPromotorias = datosResumenPromotoria.leeDatos(idPromotoria);
            foreach (DataRow idP in idPromotorias.Rows)
            {
                qResumenPromotoria += "SELECT idPromotoria,PromotoriaClave, PromotoriaNombre AS Promotoria,Registro,Proceso,Hold,Ejecucion,Rechazo,Suspendido,RevPromotoria,Cancelado,Total " +
                    " FROM FnTotalPromotoria(" + idP["id"] + ",'" +idP["Nombre"] +"','"  +idP["Clave"] + "','" + fechaD +"','" + fechaH + "'," + idFlujo +") WHERE Total<>0 UNION ";
            }
            qResumenPromotoria = qResumenPromotoria.Substring(0, qResumenPromotoria.Length - ("UNION".Length + 1));
            qResumenPromotoria += " ) A ORDER BY A.IdPromotoria";
            dt = datosResumenPromotoria.leeDatos(qResumenPromotoria);
            datosResumenPromotoria.cierraBD();
            return dt;
        }
        public DataTable DetallePromotoria(DateTime fechaDesde,DateTime fechaHasta, int idFlujo,int idPromotoria)
        {
            DataTable dt = new DataTable();
            string formato = "yyyy-MM-dd";
            string fechaD = fechaDesde.ToString(formato);
            string fechaH = fechaHasta.ToString(formato);
            bd datosDetallePromotoria = new bd();
            string qDetallePromotoria = "SELECT VTM.idTramite,F.FolioCompuesto,VTM.FechaRegistro,VTM.FechaInicio,VTM.UsuarioNombre, VTM.MesaNombre,VTM.EstadoTramite " +
                                       "FROM vw_tramiteMesa VTM " +
                                       "INNER JOIN Folio F ON VTM.idTramite=F.idTramite "+
                                       "WHERE VTM.IdPromotoria ="+idPromotoria+ " AND VTM.FechaRegistro>=CONVERT(DATETIME,'" +fechaD + "', 102) " +
                                       "AND VTM.FechaRegistro< DATEADD(DAY,1,CONVERT(DATETIME,'" + fechaH + "', 102)) " +
                                       "AND VTM.EstadoTramite IN ('En tramite','Proceso','Hold','Ejecucion','Rechazo','Suspendido') ";
           dt= datosDetallePromotoria.leeDatos(qDetallePromotoria);
           datosDetallePromotoria.cierraBD();
           return dt;
        }
        public DataTable ResumenPromotoria(int idFlujo,int estado)
        {
            DataTable dt = new DataTable();
            bd datosResumenPromotoria = new bd();
            string qResumenPromotoria = "SELECT " +
                                        "T.idRamo AS RAMO," +
                                        "RT.Nombre AS NRAMO, " +
                                        "F.FolioCompuesto AS TRAMITE," +
                                        "VT.IdPromotoria AS CVEPROMOTORIA," +
                                        "CP.Nombre AS PROMOTORIA," +
                                        "VT.AgenteClave AS CLAVEAGENTE, " +
                                        "VT.AgenteNombre AS AGENTE," +
                                        "DATEDIFF(DAY, VT.FechaRegistro, GETDATE()) AS DIAS " +
                                        "FROM VW_TRAMITE VT,Tramite T, Folio F, cat_promotorias CP,cat_ramosTramite RT " +
                                        "WHERE VT.Id = T.Id AND T.idRamo=RT.id " +
                                        "AND VT.Id = F.IdTramite " +
                                        "AND VT.IdPromotoria = CP.Id " +
                                        "AND VT.Estado =" + estado; // +
                                        //" AND VT.IdFlujo=" +idFlujo;
            dt = datosResumenPromotoria.leeDatos(qResumenPromotoria);
            datosResumenPromotoria.cierraBD();
            return dt;

        }
        public DataTable EstatusTramite()
        {
            DataTable dt = new DataTable();
            string qEstatusTramite = "SELECT idEstadoTramite,EstadoTramite FROM cat_EstadoTramite";
            bd estatusTramite = new bd();
            dt=estatusTramite.leeDatos(qEstatusTramite);
            estatusTramite.cierraBD();
            return dt;

        }

        /// <summary>
        /// Obtiene los trámites que la mesa de selección aceptó; pero que alguna mesa axuliar estableció como 'NO PROCESABLE'
        /// </summary>
        /// <returns></returns>
        public DataTable SeleccionProcesable(DateTime fechaDesde, DateTime fechaHasta)
        {
            DataTable dtResultado = new DataTable();
            string sentenciaSQL = string.Empty;
            string formato = "yyyy-MM-dd";
            string fechaD = fechaDesde.ToString(formato);
            string fechaH = fechaHasta.ToString(formato);

            sentenciaSQL = "SELECT ";
            sentenciaSQL+="	vw_tramiteP.FechaRegistro AS [Fecha Envío], ";
            sentenciaSQL+="	vw_tramiteP.FolioCompuesto AS [Número de Trámite], ";
            sentenciaSQL+="	vw_tramiteP.EstadoNombre AS [Estado], ";
            sentenciaSQL+="	vw_tramiteP.FechaSolicitud AS [Fecha Firma Solicitud], ";
            sentenciaSQL+="	TRAM00003P.RFC AS [RFC], ";
            sentenciaSQL+="	TRAM00003P.Nombre + ' ' + TRAM00003P.ApMaterno + ' ' + TRAM00003P.ApMaterno AS [Información del Contratante], ";
            sentenciaSQL+="	TRAM00003P.TitularNombre + ' ' + TRAM00003P.TitularApPat + ' ' + TRAM00003P.TitularApMat AS [Información del Titular] ";
            sentenciaSQL+="FROM vw_tramiteP, TRAM00003P ";
            sentenciaSQL+="WHERE vw_tramiteP.Id = TRAM00003P.IdTramite ";
            sentenciaSQL+="   AND vw_tramiteP.Id IN ( ";
            sentenciaSQL+="	    SELECT tramiteMesa.IdTramite ";
            sentenciaSQL+="	    FROM tramiteMesa ";
            sentenciaSQL+="       WHERE tramiteMesa.IdMesa IN (SELECT mesa.Id FROM mesa WHERE Nombre = 'SELECCIÓN') AND tramiteMesa.Estado = (SELECT statusMesa.Id FROM statusMesa WHERE statusMesa.Nombre = 'Procesado') ";
            sentenciaSQL+="	) ";
            sentenciaSQL+="	AND ";
            sentenciaSQL+="		( ";
            sentenciaSQL+="			( ";
            sentenciaSQL+="				vw_tramiteP.Id IN ( ";
            sentenciaSQL+="				SELECT tramiteMesa.IdTramite ";
            sentenciaSQL+="				FROM tramiteMesa ";
            sentenciaSQL+="				WHERE tramiteMesa.IdMesa IN (SELECT mesa.Id FROM mesa WHERE Nombre = 'REVISIÓN TÉCNICA') AND tramiteMesa.Estado = (SELECT statusMesa.Id FROM statusMesa WHERE statusMesa.Nombre = 'No Procesable') ";
            sentenciaSQL+="				) ";
            sentenciaSQL+="			) ";
            sentenciaSQL+="		OR ";
            sentenciaSQL+="			( ";
            sentenciaSQL+="				vw_tramiteP.Id IN ( ";
            sentenciaSQL+="				SELECT tramiteMesa.IdTramite ";
            sentenciaSQL+="				FROM tramiteMesa ";
            sentenciaSQL+="				WHERE tramiteMesa.IdMesa IN (SELECT mesa.Id FROM mesa WHERE Nombre = 'REVISIÓN MÉDICA') AND tramiteMesa.Estado = (SELECT statusMesa.Id FROM statusMesa WHERE statusMesa.Nombre = 'No Procesable') ";
            sentenciaSQL+="				) ";
            sentenciaSQL+="			) ";
            sentenciaSQL+="		) ";
            sentenciaSQL+= " AND vw_tramiteP.FechaRegistro>=CONVERT(DATETIME,'" + fechaD + "',102) ";
            sentenciaSQL += " AND vw_tramiteP.FechaRegistro<DATEADD(DAY,1,CONVERT(DATETIME,'" + fechaH + "',102))";


            bd estatusTramite = new bd();
            dtResultado = estatusTramite.leeDatos(sentenciaSQL.ToString());
            estatusTramite.cierraBD();
            return dtResultado;
        }
    }
}
