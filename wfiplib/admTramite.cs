using OfficeOpenXml.FormulaParsing.Excel.Functions.Logical;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace wfiplib
{
    public class admTramite
    {
        /// <summary>
        /// Agrega un registro y obtiene su Id
        /// </summary>
        /// <returns></returns>
        public int siguienteId()
        {
            int resultado = 0;
            StringBuilder SqlCmd = new StringBuilder("Insert Into tramiteControl(Fecha) Values(GetDate()) SELECT SCOPE_IDENTITY()");
            bd BD = new bd();
            resultado = BD.ejecutaCmdScalar(SqlCmd.ToString());
            BD.cierraBD();
            return resultado;
        }

        public string getFolio(int IdTramite)
        {
            string resultado = "";
            DataTable datos;
            try
            {
                string strSQL = "SELECT FolioCompuesto FROM folio WHERE IdTramite = " + IdTramite.ToString() + ";";
                bd BD = new bd();
                datos = BD.leeDatos(strSQL);
                if (datos.Rows.Count > 0)
                {
                    resultado = datos.Rows[0][0].ToString();
                }
                BD.cierraBD();
            }
            catch
            {
                resultado = "Folio: Error";
            }

            return resultado;
        }

        public string getPolizaTramite(int IdTramite)
        {
            string resultado = "";
            DataTable datos;
            try
            {
                string strSQL = "SELECT idSisLegados FROM tramite WHERE id = " + IdTramite.ToString() + ";";
                bd BD = new bd();
                datos = BD.leeDatos(strSQL);
                if (datos.Rows.Count > 0)
                {
                    resultado = datos.Rows[0][0].ToString();
                }
                BD.cierraBD();
            }
            catch
            {
                resultado = "Folio: Error";
            }

            return resultado;
        }

        /// <summary>
        /// Agrega un nuevo trámite
        /// </summary>
        /// <param name="pTramite"></param>
        /// <returns></returns>
        public bool nuevo(tramiteP pTramite)
        {
            //TODO: Actualizar correctamente el proceso de la Sentencia SQL
            bool resultado = false;
            StringBuilder SqlCmd = new StringBuilder("Insert Into tramite (Id,IdTipoTramite,FechaRegistro,Estado,DatosHtml,IdPromotoria,IdUsuario,AgenteClave,NumeroOrden,FechaSolicitud,TipoTramite, idRamo, prioridad)");
            SqlCmd.Append(" Values(");
            SqlCmd.Append(pTramite.Id.ToString());
            SqlCmd.Append("," + pTramite.IdTipoTramite.ToString("d"));
            SqlCmd.Append(",GetDate()");
            SqlCmd.Append("," + E_EstadoTramite.Registro.ToString("d"));
            SqlCmd.Append(",'" + pTramite.DatosHtml.Replace("'", "''") + "'");
            SqlCmd.Append("," + pTramite.IdPromotoria.ToString());
            SqlCmd.Append("," + pTramite.IdUsuario.ToString());
            SqlCmd.Append(",'" + pTramite.AgenteClave.ToString() + "'");
            SqlCmd.Append(",'" + pTramite.NumeroOrden.ToString() + "'");
            SqlCmd.Append(",'" + pTramite.FechaSolicitud.ToString() + "'");
            SqlCmd.Append(",'" + pTramite.TipoTramite.ToString() + "'");
            SqlCmd.Append("," + pTramite.IdRamo);
            SqlCmd.Append("," + pTramite.Prioridad);
            SqlCmd.Append(")");
            bd BD = new bd();
            resultado = BD.ejecutaCmd(SqlCmd.ToString());
            BD.cierraBD();
            return resultado;
        }

        public bool GuardaCapturaAntesDeCambio(int IdTramite)
        {
            bool resultado = false;
            StringBuilder SqlCmd = new StringBuilder("");

            SqlCmd.Append("insert into tramiteCapturaCambio ");
            SqlCmd.Append("select ");
            SqlCmd.Append("	  tramite.id, tramite.DatosHtml, tramite.NumeroOrden, tramite.FechaSolicitud, tramite.prioridad ");
            SqlCmd.Append("	  ,TRAM00003P.CPDES, TRAM00003P.FolioCPDES, TRAM00003P.EstatusCPDES, TRAM00003P.TipoPersona, TRAM00003P.Nombre, TRAM00003P.ApPaterno, TRAM00003P.ApMaterno, TRAM00003P.Sexo, TRAM00003P.FechaNacimiento, TRAM00003P.RFC, TRAM00003P.FechaConst, TRAM00003P.Nacionalidad, TRAM00003P.TitularNombre, TRAM00003P.TitularApPat, TRAM00003P.TitularApMat, TRAM00003P.TitularNacionalidad, TRAM00003P.TitularSexo, TRAM00003P.TitularFechaNacimiento, TRAM00003P.SumaAsegurada, TRAM00003P.SumaPolizas, TRAM00003P.PrimaTotal, TRAM00003P.IdMoneda, TRAM00003P.Detalle, TRAM00003P.HombreClave, TRAM00003P.Entidad, TRAM00003P.TitularEntidad, TRAM00003P.Excel, TRAM00003P.OneShot ");
            SqlCmd.Append("	 ,Producto.idProducto, Producto.IdCatProducto, Producto.IdCatSubProducto ");
            SqlCmd.Append("from tramite ");
            SqlCmd.Append("inner join TRAM00003P on tramite.id = TRAM00003P.IdTramite ");
            SqlCmd.Append("inner join Producto on tramite.Id = Producto.IdTramite ");
            SqlCmd.Append("where tramite.id = " + IdTramite.ToString());

            bd BD = new bd();
            resultado = BD.ejecutaCmd(SqlCmd.ToString());
            BD.cierraBD();
            return resultado;
        }

        public bool ActualiaOrdenFecha(EmisionVG oEmisionVG)
        {
            bool resultado = false;
            StringBuilder SqlCmd = new StringBuilder("UPDATE [dbo].[tramite] SET DatosHtml ='" + oEmisionVG.DatosHtml + "', NumeroOrden = '" + oEmisionVG.NumeroOrden + "', FechaSolicitud = '" + oEmisionVG.FechaSolicitud + "', prioridad= '" + Convert.ToInt32(oEmisionVG.Prioridad) + "' WHERE id = '" + oEmisionVG.IdTramite + "'");
            bd BD = new bd();
            resultado = BD.ejecutaCmd(SqlCmd.ToString());
            BD.cierraBD();
            return resultado;
        }
        
        public bool ActualiaEstadoLaboratorio(int IdTramite)
        {
            bool resultado = false;
            StringBuilder SqlCmd = new StringBuilder("UPDATE [tramite] SET [Estado] = '" + wfiplib.E_EstadoTramite.Proceso.ToString("d") + "' WHERE Id = '" + IdTramite + "'");
            bd BD = new bd();
            resultado = BD.ejecutaCmd(SqlCmd.ToString());
            BD.cierraBD();
            return resultado;
        }

        public bool ActualiaEstadoLaboratorioMesa(int IdTramite)
        {
            bool resultado = false;
            StringBuilder SqlCmd = new StringBuilder("UPDATE [tramiteMesa] SET [Estado] = '" + wfiplib.E_EstadoMesa.Registro.ToString("d") + "' WHERE IdTramite = '" + IdTramite + "' AND idmesa IN (SELECT mesa.Id FROM mesa WHERE mesa.Nombre = 'CITAS MÉDICAS')");
            bd BD = new bd();
            resultado = BD.ejecutaCmd(SqlCmd.ToString());
            BD.cierraBD();
            return resultado;
        }

        /// <summary>
        /// Registra las observaciones de la promotoría en la bitácora
        /// </summary>
        /// <param name="pTramite"></param>
        /// <returns></returns>
        public bool registraBitacora(tramiteP pTramite, wfiplib.credencial usuario, string bitacora)
        {
            bool blnResultado = false;
            StringBuilder SqlCmd = new StringBuilder("INSERT INTO bitacora (IdFlujo, IdTipoTramite, IdTramite, IdMesa, Usuario, FechaInicio, FechaTermino, Estado, Observacion, ObservacionPrivada, IdUsuario) ");
            SqlCmd.Append(" Values(");

            SqlCmd.Append(pTramite.IdFlujo.ToString("d"));
            SqlCmd.Append(", " + pTramite.IdTipoTramite.ToString("d"));
            SqlCmd.Append(", " + pTramite.Id.ToString());
            SqlCmd.Append(", -1");
            SqlCmd.Append(", '" + usuario.Usuario.ToString() + "'");
            SqlCmd.Append(", GETDATE()");
            SqlCmd.Append(", GETDATE()");
            SqlCmd.Append(", 0");
            SqlCmd.Append(", '" + bitacora + "'");
            SqlCmd.Append(", 'NA'");
            SqlCmd.Append(", " + usuario.Id.ToString());

            SqlCmd.Append(")");
            bd BD = new bd();
            blnResultado = BD.ejecutaCmd(SqlCmd.ToString());
            BD.cierraBD();
            return blnResultado;
        }

        /// <summary>
        /// Cambio el tipo de trámite, a partir del cambio en Recepción citas médicas 
        /// </summary>
        /// <param name="IdTramite"></param>
        /// <returns></returns>
        public bool CambiaTramite(int IdTramite)
        {
            bool resultado = false;
            StringBuilder SqlCmd = new StringBuilder("UPDATE [tramite] SET [IdTipoTramite] = 2 WHERE [IdTipoTramite] = 3 AND [Id] = '" + IdTramite + "';");
            bd BD = new bd();
            resultado = BD.ejecutaCmd(SqlCmd.ToString());
            BD.cierraBD();
            return resultado;
        }

        /// <summary>
        /// Agrega un nuevo registro y obtiene su folio compuesto
        /// </summary>
        /// <param name="pTramite"></param>
        /// <returns></returns>
        public bool nuevoFolio(tramiteP pTramite)
        {
            bool resultado = true;
            String Abreviatura = "";

            switch (pTramite.IdTipoTramite)
            {
                case wfiplib.E_TipoTramite.indPriEmisionVida:
                case wfiplib.E_TipoTramite.indPriEmisionVidaCM:
                    Abreviatura = "NV";
                    break;
                
                case wfiplib.E_TipoTramite.indPriEmisionGMM:
                    Abreviatura = "NG";
                    break;
            }

            StringBuilder SqlCmd = new StringBuilder("INSERT INTO dbo.Folio(IdTramite,TramitePP,TipoTramite,Year,Mes,Dia,IdPromotoria,Folio,FolioCompuesto)");
            SqlCmd.Append(" Values(");
            SqlCmd.Append(pTramite.Id.ToString());
            SqlCmd.Append(",'" + pTramite.TipoTramite.ToString() + "'");
            SqlCmd.Append(",'" + Abreviatura + "'");
            SqlCmd.Append(",FORMAT(GETDATE(), 'yy')");
            SqlCmd.Append(",FORMAT(GETDATE(), 'MM')");
            SqlCmd.Append(",FORMAT(GETDATE(), 'dd')");
            SqlCmd.Append("," + pTramite.IdPromotoria.ToString());
            SqlCmd.Append(",ISNULL((SELECT MAX(Folio) FROM dbo.Folio WHERE [TramitePP] = '" + pTramite.TipoTramite.ToString() + "' AND [TipoTramite] = '" + Abreviatura + "' AND [YEAR] = FORMAT(GETDATE(), 'yy') ),0) + 1");
            //SqlCmd.Append(",'" + Abreviatura + "'+FORMAT(GETDATE(),'yyMMdd')+RIGHT('000' + Ltrim(Rtrim('" + pTramite.IdPromotoria.ToString() + "')),4)+RIGHT('00000000' + Ltrim(Rtrim(ISNULL((SELECT MAX(Folio) FROM dbo.Folio WHERE[TramitePP] = '" + pTramite.TipoTramite.ToString() + "' AND [TipoTramite] = '" + Abreviatura + "' ),0) + 1)),8)");
            //SqlCmd.Append(",'" + Abreviatura + "'+FORMAT(GETDATE(),'yyMMdd')+RIGHT('000' + Ltrim(Rtrim((SELECT Clave FROM [cat_promotorias] WHERE Id = '" + pTramite.IdPromotoria.ToString() + "'))),4)+RIGHT('00000000' + Ltrim(Rtrim(ISNULL((SELECT MAX(Folio) FROM dbo.Folio WHERE[TramitePP] = '" + pTramite.TipoTramite.ToString() + "' AND [TipoTramite] = '" + Abreviatura + "' ),0) + 1)),8)");
            SqlCmd.Append(",'" + Abreviatura + "'+FORMAT(GETDATE(),'yy')+RIGHT('0000' + Ltrim(Rtrim((SELECT Clave FROM [cat_promotorias] WHERE Id = '" + pTramite.IdPromotoria.ToString() + "'))),4)+RIGHT('000000' + Ltrim(Rtrim(ISNULL((SELECT MAX(Folio) FROM dbo.Folio WHERE [TramitePP] = '" + pTramite.TipoTramite.ToString() + "' AND [TipoTramite] = '" + Abreviatura + "' AND [YEAR] = FORMAT(GETDATE(), 'yy') ),0) + 1)),6)");
            SqlCmd.Append(")");
            bd BD = new bd();
            resultado = BD.ejecutaCmd(SqlCmd.ToString());
            BD.cierraBD();
            return resultado;
        }

        /// <summary>
        /// Regresa el Id del Status de la Mesa Indicado
        /// </summary>
        /// <param name="NombreMesa">Nombre de la Mesa</param>
        /// <returns></returns>
        public int getIdStatusMesa(string NombreMesa)
        {
            int intId = -1;
            string strSQL = "SELECT Id FROM statusMesa WHERE Nombre = '" + NombreMesa + "';";
            bd BD = new bd();
            DataTable datos = BD.leeDatos(strSQL);
            if (datos.Rows.Count > 0)
            {
                intId = Convert.ToInt32(datos.Rows[0]["Id"]);
            }
            datos.Dispose();
            BD.cierraBD();
            return intId;
        }

        /// <summary>
        /// Valida si una mesa tiene permisos para establecer algún status del tramite
        /// </summary>
        /// <param name="IdFlujo"></param>
        /// <param name="IdTipoTramite"></param>
        /// <param name="IdMesa"></param>
        /// <param name="IdStatusMesa"></param>
        /// <returns></returns>
        public Boolean getPermisoMesaStatus(int IdFlujo, wfiplib.E_TipoTramite IdTipoTramite, int IdMesa, int IdStatusMesa)
        {
            Boolean blnPermiso = false;
            string strSQL = "SELECT Activo FROM statusMesaAsign WHERE IdFlujo = " + IdFlujo.ToString() + " AND IdTipoTramite = " + Convert.ToInt16(IdTipoTramite) + " AND IdMesa = " + IdMesa.ToString() + " AND IdStatusMesa = " + IdStatusMesa.ToString() + ";";
            bd BD = new bd();
            DataTable datos = BD.leeDatos(strSQL);
            if (datos.Rows.Count > 0)
            {
                blnPermiso = Convert.ToBoolean(datos.Rows[0]["Activo"]);
            }
            datos.Dispose();
            BD.cierraBD();
            return blnPermiso;
        }

        /// <summary>
        /// Obtiene si el el trámite es procesable
        /// </summary>
        /// <param name="pIdTramite">Id del Trámite.</param>
        /// <param name="pIdFlujo">Id del Flujo.</param>
        /// <returns>Verdadero si el trámite es procesable por las mesas de Revisión Técnico / Médico</returns>
        public Boolean getProcesableTramite(int pIdTramite, int pIdFlujo)
        {
            Boolean blnProcesable = true;
            string strSQL = "SELECT IdMesa FROM tramiteMesa WHERE IdTramite = " + pIdTramite.ToString() + " AND IdMesa IN (SELECT Id FROM mesa WHERE IdFlujo = " + pIdFlujo.ToString() + " AND Nombre in ('REVISIÓN TÉCNICA','REVISIÓN MÉDICA')) AND Estado = (SELECT Id FROM statusMesa WHERE Nombre = 'No Procesable');";
            Console.Write(strSQL);
            bd BD = new bd();
            DataTable datos = BD.leeDatos(strSQL);
            if (datos.Rows.Count > 0)
            {
                //blnPermiso = Convert.ToBoolean(datos.Rows[0]["Activo"]);
                blnProcesable = false;
            }
            datos.Dispose();
            BD.cierraBD();
            return blnProcesable;
        }

        /// <summary>
        /// Obtiene el detalle de un trámite
        /// </summary>
        /// <param name="pId"></param>
        /// <returns></returns>
        public tramiteP carga(int pId)
        {
            tramiteP resultado = null;
            String SqlCmd = "SELECT * FROM vw_tramiteP WHERE Id = " + pId.ToString();
            bd BD = new bd();
            DataTable datos = BD.leeDatos(SqlCmd);
            if (datos.Rows.Count > 0) { resultado = arma(datos.Rows[0]); }
            datos.Dispose();
            BD.cierraBD();
            return resultado;
        }

        public bool existeTramiteMesa(int pIdTramite, int pIdMesa)
        {
            Boolean blnResultado = false;
            string strSQL = "SELECT * FROM tramiteMesa WHERE tramiteMesa.IdTramite = " + pIdTramite.ToString() + " AND tramiteMesa.IdMesa = " + pIdMesa.ToString() + ";";
            Console.Write(strSQL);
            bd BD = new bd();
            DataTable datos = BD.leeDatos(strSQL);
            if (datos.Rows.Count > 0)
            {
                //blnPermiso = Convert.ToBoolean(datos.Rows[0]["Activo"]);
                blnResultado = true;
            }
            datos.Dispose();
            BD.cierraBD();
            return blnResultado;
        }

        public bool ExisteTramiteMesaCitasMedicas(int pIdTramite)
        {
            Boolean blnResultado = false;
            string strSQL = "SELECT * FROM tramiteMesa WHERE tramiteMesa.IdTramite = " + pIdTramite.ToString() + " AND tramiteMesa.IdMesa IN( SELECT mesa.Id FROM mesa WHERE mesa.Nombre = 'CITAS MÉDICAS' AND mesa.IdFlujo = (SELECT tipoTramite.IdFlujo FROM tipoTramite WHERE tipoTramite.Id IN (SELECT tramite.IdTipoTramite FROM tramite WHERE tramite.Id = " + pIdTramite.ToString() + ")));";
            Console.Write(strSQL);
            bd BD = new bd();
            DataTable datos = BD.leeDatos(strSQL);
            if (datos.Rows.Count > 0)
            {
                //blnPermiso = Convert.ToBoolean(datos.Rows[0]["Activo"]);
                blnResultado = true;
            }
            datos.Dispose();
            BD.cierraBD();
            return blnResultado;
        }

        public DataTable ExisteTramiteMesaCitasMedicasFlujoTramite(int pIdTramite)
        {
            bd BD = new bd();
            StringBuilder SqlCmd = new StringBuilder("SELECT mesa.Id FROM mesa WHERE mesa.Nombre = 'CITAS MÉDICAS' AND mesa.IdFlujo = (SELECT tipoTramite.IdFlujo FROM tipoTramite WHERE tipoTramite.Id IN( SELECT tramite.IdTipoTramite FROM tramite WHERE tramite.Id = " + pIdTramite.ToString() + "))");
            DataTable datos = BD.leeDatos(SqlCmd.ToString());
            BD.cierraBD();
            return datos;
        }

        public bool existeTramiteMesaCancelado(int pIdTramite, string pNombreMesa)
        {
            Boolean blnResultado = false;
            string strSQL = "SELECT * FROM tramiteMesa WHERE tramiteMesa.IdTramite = " + pIdTramite.ToString() + " AND tramiteMesa.IdMesa IN (SELECT mesa.Id FROM mesa WHERE mesa.Nombre = '" + pNombreMesa + "' AND mesa.IdFlujo = ( SELECT tipoTramite.IdFlujo FROM tipoTramite WHERE tipoTramite.Id IN ( SELECT tramite.IdTipoTramite FROM tramite WHERE tramite.Id = " + pIdTramite.ToString() + ") ) ) AND tramiteMesa.Estado = (SELECT statusMesa.Id FROM statusMesa WHERE statusMesa.Nombre = 'Rechazo') ;";
            Console.Write(strSQL);
            bd BD = new bd();
            DataTable datos = BD.leeDatos(strSQL);
            if (datos.Rows.Count > 0)
            {
                //blnPermiso = Convert.ToBoolean(datos.Rows[0]["Activo"]);
                blnResultado = true;
            }
            datos.Dispose();
            BD.cierraBD();
            return blnResultado;
        }

        /// <summary>
        /// Realiza la cancelación del trámite
        /// </summary>
        /// <param name="Tramite">Id del Trámite</param>
        /// <param name="DataSession">Información de la sesión para el registro de la actividad.</param>
        /// <param name="statusCancelacion">Indica el status de Cancelación (existen varios estados de cancelación).</param>
        /// <returns>Regresa Verdadero [TRUE] si la cancelación fue exítosa.</returns>
        public bool CancelarTramite(wfiplib.tramiteP Tramite, wfiplib.credencial DataSession, wfiplib.E_EstadoTramite statusCancelacion)
        {
            bd BD = null;
            bool blnResultado = false;
            string strStatusName = "";
            string strObservaciones = "";

            try
            {
                switch (statusCancelacion)
                {
                    case E_EstadoTramite.Cancelado:
                        strStatusName = "Cancelado";
                        strObservaciones = "Cancelado por Supervisión";
                        break;

                    case E_EstadoTramite.PromotoriaCancela:
                        strStatusName = "Promotoría Cancela";
                        strObservaciones = "Cancelado por Promotoría";
                        break;
                }

                string strSQL = "UPDATE tramiteMesa SET Estado = (SELECT statusMesa.Id FROM statusMesa WHERE statusMesa.Nombre = '" + strStatusName + "'), ObservacionPublica = '" + strObservaciones + "', ObservacionPrivada = '" + strObservaciones + "', FechaFin = GETDATE() WHERE tramiteMesa.IdTramite = " + Tramite.Id.ToString() + " AND tramiteMesa.IdMesa IN (SELECT tramiteMesa.IdMesa FROM tramiteMesa WHERE NOT tramiteMesa.Estado IN (SELECT statusMesa.Id FROM statusMesa WHERE statusMesa.Nombre IN ('Registro', 'Atrapado', 'Hold', 'RevisionHold', 'ReingresoHold', 'SolicitudApoyo', 'ReingresoApoyo', 'Pausa', 'Rechazo', 'Suspendido', 'RevisionSuspencion', 'ReingresoSuspencion', 'PCI', 'ReingresoPCI', 'RevisionPCI', 'Complementario', 'Revisión con Prospecto', 'Confirmación Pendiente', 'En espera de resultados', 'Información para Citas Medicas', 'Revisión Promotoría KO', 'Revisión Promotoría OK'))); ";
                strSQL += "UPDATE tramite SET tramite.Estado = (SELECT statusTramite.Id FROM statusTramite WHERE statusTramite.Nombre = '" + strStatusName + "'), tramite.FechaTermino = GETDATE() WHERE tramite.Id = " + Tramite.Id.ToString() + "; ";
                strSQL += "INSERT INTO bitacora (IdFlujo, IdTipoTramite, IdTramite, IdMesa, Usuario, FechaInicio, FechaTermino, Estado, Observacion, ObservacionPrivada, IdUsuario) VALUES (" + Tramite.IdFlujo.ToString() + ", " + Tramite.IdTipoTramite.ToString("d") + ", " + Tramite.Id.ToString() + ", -1, '" + DataSession.Usuario.ToString() + "', GETDATE(), GETDATE(), (SELECT statusMesa.Id FROM statusMesa WHERE statusMesa.Nombre = '" + strStatusName + "'), '" + strObservaciones + "', '" + strObservaciones + "', " + DataSession.Id.ToString() + ")";
                BD = new bd();
                blnResultado = BD.ejecutaCmd(strSQL);
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }
            finally
            {
                BD.cierraBD();
            }

            return blnResultado;
        }
        /// <summary>
        /// ESTRUCUTRA DE CANCELACION DE TRAMITE POR PARTE DEL OPERADOR, MODIFICA TABLA, tramiteMesa,tramite, bitacora, sendToMesa
        /// </summary>
        /// <param name="Tramite"></param>
        /// <param name="DataSession"></param>
        /// <param name="statusCancelacion"></param>
        /// <returns></returns>
        public bool CancelarTramiteServisor(wfiplib.tramiteP Tramite, wfiplib.credencial DataSession, wfiplib.E_EstadoTramite statusCancelacion)
        {
            bd BD = null;
            bool blnResultado = false;
            string strStatusName = "";
            string strObservaciones = "";

            try
            {
                switch (statusCancelacion)
                {
                    case E_EstadoTramite.Cancelado:
                        strStatusName = "Cancelado";
                        strObservaciones = "Cancelado por Supervisión";
                        break;

                    case E_EstadoTramite.PromotoriaCancela:
                        strStatusName = "Promotoría Cancela";
                        strObservaciones = "Cancelado por Promotoría";
                        break;
                }

                string strSQL = "UPDATE tramiteMesa SET Estado = "+
                                "(SELECT statusMesa.Id FROM statusMesa WHERE statusMesa.Nombre = '" + strStatusName + "'), " +
                                "ObservacionPublica = '" + strObservaciones + "', " +
                                "ObservacionPrivada = '" + strObservaciones + "', " +
                                "FechaFin = GETDATE() WHERE tramiteMesa.IdTramite = " + Tramite.Id.ToString() + " " +
                                "AND tramiteMesa.IdMesa IN (" +
                                    "SELECT mesa.Id FROM tramite " +
                                    "INNER JOIN tramiteMesa ON tramiteMesa.IdTramite = tramite.Id " +
                                    "INNER JOIN statusMesa ON tramiteMesa.Estado = statusMesa.Id " +
                                    "INNER JOIN statusTramite ON  tramite.Estado = statusTramite.Id " +
                                    "INNER JOIN mesa ON tramiteMesa.IdMesa = mesa.Id " +
                                    "WHERE NOT statusMesa.Nombre IN('Procesado', 'Cancelado', 'Rechazo') " +
			                        "AND NOT statusTramite.Nombre IN('Ejecucion', 'Rechazo', 'Cancelado', 'Promotoría Cancela', 'Caducado') " +
                                    "AND tramiteMesa.IdTramite = " + Tramite.Id.ToString() + " "+
                                "); ";
                strSQL += "UPDATE tramite SET tramite.Estado = (SELECT statusTramite.Id FROM statusTramite WHERE statusTramite.Nombre = '" + strStatusName + "'), tramite.FechaTermino = GETDATE() WHERE tramite.Id = " + Tramite.Id.ToString() + "; ";
                strSQL += "INSERT INTO bitacora (IdFlujo, IdTipoTramite, IdTramite, IdMesa, Usuario, FechaInicio, FechaTermino, Estado, Observacion, ObservacionPrivada, IdUsuario) VALUES (" + Tramite.IdFlujo.ToString() + ", " + Tramite.IdTipoTramite.ToString("d") + ", " + Tramite.Id.ToString() + ", -1, '" + DataSession.Usuario.ToString() + "', GETDATE(), GETDATE(), (SELECT statusMesa.Id FROM statusMesa WHERE statusMesa.Nombre = '" + strStatusName + "'), '" + strObservaciones + "', '" + strObservaciones + "', " + DataSession.Id.ToString() + ") ";
                BD = new bd();
                blnResultado = BD.ejecutaCmd(strSQL);
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }
            finally
            {
                BD.cierraBD();
            }

            return blnResultado;
        }

        public bool CancelarTramite(wfiplib.tramiteP Tramite, wfiplib.credencial DataSession, wfiplib.E_EstadoTramite statusCancelacion, string observacion)
        {
            bd BD = null;
            bool blnResultado = false;
            string strStatusName = "";
            string strObservaciones = "";

            try
            {
                switch (statusCancelacion)
                {
                    case E_EstadoTramite.Cancelado:
                        strStatusName = "Cancelado";
                        strObservaciones = "Cancelado por Supervisor " + observacion;
                        break;

                    case E_EstadoTramite.PromotoriaCancela:
                        strStatusName = "Promotoría Cancela";
                        strObservaciones = "Cancelado por Promotoría ";
                        break;
                }

                string strSQL = "UPDATE tramiteMesa SET Estado = (SELECT statusMesa.Id FROM statusMesa WHERE statusMesa.Nombre = '" + strStatusName + "'), ObservacionPublica = '" + strObservaciones + "', ObservacionPrivada = '" + strObservaciones + "', FechaFin = GETDATE() WHERE tramiteMesa.IdTramite = " + Tramite.Id.ToString() + " AND tramiteMesa.IdMesa IN (SELECT tramiteMesa.IdMesa FROM tramiteMesa WHERE NOT tramiteMesa.Estado IN (SELECT statusMesa.Id FROM statusMesa WHERE statusMesa.Nombre IN ('Registro', 'Atrapado', 'Hold', 'RevisionHold', 'ReingresoHold', 'SolicitudApoyo', 'ReingresoApoyo', 'Pausa', 'Rechazo', 'Suspendido', 'RevisionSuspencion', 'ReingresoSuspencion', 'PCI', 'ReingresoPCI', 'RevisionPCI', 'Complementario', 'Revisión con Prospecto', 'Confirmación Pendiente', 'En espera de resultados', 'Información para Citas Medicas', 'Revisión Promotoría KO', 'Revisión Promotoría OK'))); ";
                strSQL += "UPDATE tramite SET tramite.Estado = (SELECT statusTramite.Id FROM statusTramite WHERE statusTramite.Nombre = '" + strStatusName + "'), tramite.FechaTermino = GETDATE() WHERE tramite.Id = " + Tramite.Id.ToString() + "; ";
                strSQL += "INSERT INTO bitacora (IdFlujo, IdTipoTramite, IdTramite, IdMesa, Usuario, FechaInicio, FechaTermino, Estado, Observacion, ObservacionPrivada, IdUsuario) VALUES (" + Tramite.IdFlujo.ToString() + ", " + Tramite.IdTipoTramite.ToString("d") + ", " + Tramite.Id.ToString() + ", -1, '" + DataSession.Usuario.ToString() + "', GETDATE(), GETDATE(), (SELECT statusMesa.Id FROM statusMesa WHERE statusMesa.Nombre = '" + strStatusName + "'), '" + strObservaciones + "', '" + strObservaciones + "', " + DataSession.Id.ToString() + ")";
                BD = new bd();
                blnResultado = BD.ejecutaCmd(strSQL);
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }
            finally
            {
                BD.cierraBD();
            }

            return blnResultado;
        }

        private tramiteP arma(DataRow pRegistro)
        {
            tramiteP resultado = new tramiteP();
            if (!pRegistro.IsNull("Id")) resultado.Id = Convert.ToInt32(pRegistro["Id"]);
            if (!pRegistro.IsNull("IdTipoTramite")) resultado.IdTipoTramite = (E_TipoTramite)(pRegistro["IdTipoTramite"]);
            if (!pRegistro.IsNull("idRamo")) resultado.IdRamoTramite = (E_RamoTramite)(pRegistro["idRamo"]);
            if (!pRegistro.IsNull("FechaRegistro")) resultado.FechaRegistro = Convert.ToDateTime(pRegistro["FechaRegistro"]);
            if (!pRegistro.IsNull("FechaTermino")) resultado.FechaTermino = Convert.ToDateTime(pRegistro["FechaTermino"]);
            if (!pRegistro.IsNull("Estado")) resultado.Estado = (E_EstadoTramite)pRegistro["Estado"];
            if (!pRegistro.IsNull("EstadoNombre")) resultado.EstadoNombre = Convert.ToString(pRegistro["EstadoNombre"]);
            if (!pRegistro.IsNull("DatosHtml")) resultado.DatosHtml = Convert.ToString(pRegistro["DatosHtml"]);
            if (!pRegistro.IsNull("IdPromotoria")) resultado.IdPromotoria = Convert.ToInt32(pRegistro["IdPromotoria"]);
            if (!pRegistro.IsNull("IdUsuario")) resultado.IdUsuario = Convert.ToInt32(pRegistro["IdUsuario"]);
            if (!pRegistro.IsNull("AgenteClave")) resultado.AgenteClave = Convert.ToString(pRegistro["AgenteClave"]);
            if (!pRegistro.IsNull("IdFlujo")) resultado.IdFlujo = Convert.ToInt32(pRegistro["IdFlujo"]);
            if (!pRegistro.IsNull("Flujo")) resultado.Flujo = Convert.ToString(pRegistro["Flujo"]);
            if (!pRegistro.IsNull("TramiteNombre")) resultado.TramiteNombre = Convert.ToString(pRegistro["TramiteNombre"]);
            if (!pRegistro.IsNull("FolioCompuesto")) resultado.FolioCompuesto = Convert.ToString(pRegistro["FolioCompuesto"]);
            if (!pRegistro.IsNull("Tabla")) resultado.Tabla = Convert.ToString(pRegistro["Tabla"]);
            if (!pRegistro.IsNull("PromotoriaNombre")) resultado.PromotoriaNombre = Convert.ToString(pRegistro["PromotoriaNombre"]);
            if (!pRegistro.IsNull("PromotoriaClave")) resultado.PromotoriaClave = Convert.ToString(pRegistro["PromotoriaClave"]);
            if (!pRegistro.IsNull("AgenteNombre")) resultado.AgenteNombre = Convert.ToString(pRegistro["AgenteNombre"]);
            if (!pRegistro.IsNull("UsuarioNombre")) resultado.UsuarioNombre = Convert.ToString(pRegistro["UsuarioNombre"]);
            if (!pRegistro.IsNull("IdSisLegados")) resultado.IdSisLegados = Convert.ToString(pRegistro["IdSisLegados"]);
            if (!pRegistro.IsNull("CPDES")) resultado.CPDES = Convert.ToString(pRegistro["CPDES"]);
            if (!pRegistro.IsNull("EstatusCPDES")) resultado.EstatusCPDES = Convert.ToString(pRegistro["EstatusCPDES"]);
            if (!pRegistro.IsNull("kwik")) resultado.kwik = Convert.ToString(pRegistro["kwik"]);
            return resultado;
        }

        /// <summary>
        /// Obtiene una lista con todos los trámites que pertenecen a un usuario
        /// </summary>
        /// <param name="pIdUsuario"></param>
        /// <returns></returns>
        public List<tramiteP> daListaPorCreador(int pIdUsuario)
        {
            List<tramiteP> resultado = new List<tramiteP>();
            bd BD = new bd();
            StringBuilder SqlCmd = new StringBuilder("SELECT * FROM vw_tramiteP WHERE IdUsuario=" + pIdUsuario.ToString());
            DataTable datos = BD.leeDatos(SqlCmd.ToString());
            if (datos.Rows.Count > 0) foreach (DataRow registro in datos.Rows) resultado.Add(arma(registro));
            BD.cierraBD();
            return resultado;
        }

        /// <summary>
        /// Obtiene todos los tramites que pertenecen a un usuario
        /// </summary>
        /// <param name="pIdUsuario"></param>
        /// <returns></returns>
        public DataTable daTablaTramitesCreador(int pIdUsuario)
        {
            bd BD = new bd();
            StringBuilder SqlCmd = new StringBuilder("SELECT * FROM vw_tramiteP WHERE IdUsuario=" + pIdUsuario.ToString());
            DataTable datos = BD.leeDatos(SqlCmd.ToString());
            BD.cierraBD();
            return datos;
        }

        /// <summary>
        /// Obtiene todos los tramites que pertenecen a una promotoría  
        /// </summary>
        /// <param name="pIdPromotoria"></param>
        /// <returns></returns>
        public DataTable daTablaTramitesPromotoria(int pIdPromotoria)
        {
            bd BD = new bd();
            StringBuilder SqlCmd = new StringBuilder("SELECT * FROM vw_tramiteP WHERE IdPromotoria = " + pIdPromotoria.ToString() + " ORDER BY FechaRegistro DESC");
            DataTable datos = BD.leeDatos(SqlCmd.ToString());
            BD.cierraBD();
            return datos;
        }
        public DataTable daTablaTramitesPromotoriaExcel(int pIdPromotoria)
        {
            bd BD = new bd();
            string SqlCmd = "SELECT " +
                            "FechaRegistro,"+
                            "FolioCompuesto,"+
                            "NumeroOrden,"+
                            "Flujo,"+
                            "Producto,"+
                            "RTRIM(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(DatosHtml, '<P>', ''), '</P>', ''), '<b>', ''), '</b>', '  '), '</br>', '')) AS DatosHtml,"+
                            "FechaSolicitud,"+
                            "EstadoNombre,"+
                            "IdSisLegados,"+
                            "kwik "+
                            "FROM vw_tramiteP WHERE IdPromotoria = " + pIdPromotoria.ToString() + 
                            " ORDER BY FechaRegistro DESC";
            DataTable datos = BD.leeDatos(SqlCmd.ToString());
            BD.cierraBD();
            return datos;
        }

        public DataTable daTablaTramites()
        {
            bd BD = new bd();
            StringBuilder SqlCmd = new StringBuilder("SELECT * FROM vw_tramiteP  ORDER BY FechaRegistro DESC");
            DataTable datos = BD.leeDatos(SqlCmd.ToString());
            BD.cierraBD();
            return datos;
        }

        /// <summary>
        /// Obtiene los trámites que puede visualizar el operaddaTablaTramitesOperacionFechasor.
        /// </summary>
        /// <param name="IdFlujo">Id del Flujo asignado al operador</param>
        /// <returns>[DataTable] Información de los Trámites</returns>
        public DataTable daTablaTramitesFlujoFechas(int idFlujo, DateTime fechaDesde, DateTime fechaHasta)
        {
            TimeSpan In = new TimeSpan(00, 00, 00);
            TimeSpan Fin = new TimeSpan(23, 59, 59);
            fechaDesde = fechaDesde.Date + In;
            fechaHasta = fechaHasta.Date + Fin;

            string formato = "yyyy-MM-dd HH:mm:ss";
            string fechaD = fechaDesde.ToString(formato);
            string fechaH = fechaHasta.ToString(formato);

            bd BD = new bd();
            StringBuilder SqlCmd = new StringBuilder("" +
                "DECLARE @FECHAINI  DATETIME;" +
                "DECLARE @FECHAFIN  DATETIME;" +
                "SET @FECHAINI = TRY_CONVERT(DATETIME, '" + fechaD + "',121);" +
                "SET @FECHAFIN = TRY_CONVERT(DATETIME, '" + fechaH + "',121);" +
                "SELECT * FROM vw_tramiteP WHERE IdFlujo = " + idFlujo.ToString() + " AND FechaRegistro >= @FECHAINI and FechaRegistro <= @FECHAFIN ORDER BY FechaRegistro DESC");
        //  StringBuilder SqlCmd = new StringBuilder("SELECT * FROM vw_tramiteP WHERE IdFlujo = " + idFlujo.ToString() + "  ORDER BY FechaRegistro DESC");
            DataTable datos = BD.leeDatos(SqlCmd.ToString());
            BD.cierraBD();
            return datos;
        }

        public DataTable daTablaTramitesFechas(DateTime fechaDesde, DateTime fechaHasta, int IdUsuario)
        {
            BaseDeDatos b = new BaseDeDatos();
            b.ExecuteCommandSP("Tramite_Supervision_MisTramites_Fechas_IdUsuario");
            b.AddParameter("@IdUsuario", (int)IdUsuario, SqlDbType.Int);
            b.AddParameter("@FechaInicio", fechaDesde, SqlDbType.DateTime);
            b.AddParameter("@FechaFin", fechaHasta, SqlDbType.DateTime);
            return b.Select();
        }

        //public DataTable daTablaTramitesFechas(DateTime fechaDesde, DateTime fechaHasta)
        //{
        //    TimeSpan In = new TimeSpan(00, 00, 00);
        //    TimeSpan Fin = new TimeSpan(23, 59, 59);
        //    fechaDesde = fechaDesde.Date + In;
        //    fechaHasta = fechaHasta.Date + Fin;

        //    string formato = "yyyy-MM-dd HH:mm:ss";
        //    string fechaD = fechaDesde.ToString(formato);
        //    string fechaH = fechaHasta.ToString(formato);

        //    bd BD = new bd();
        //    StringBuilder SqlCmd = new StringBuilder("" +
        //        "DECLARE @FECHAINI  DATETIME;" +
        //        "DECLARE @FECHAFIN  DATETIME;" +
        //        "SET @FECHAINI = TRY_CONVERT(DATETIME, '" + fechaD + "',121);" +
        //        "SET @FECHAFIN = TRY_CONVERT(DATETIME, '" + fechaH + "',121);" +
        //        "SELECT * FROM vw_tramiteP WHERE FechaRegistro >= @FECHAINI and FechaRegistro <= @FECHAFIN ORDER BY FechaRegistro DESC");
        //    //  StringBuilder SqlCmd = new StringBuilder("SELECT * FROM vw_tramiteP WHERE IdFlujo = " + idFlujo.ToString() + "  ORDER BY FechaRegistro DESC");
        //    DataTable datos = BD.leeDatos(SqlCmd.ToString());
        //    BD.cierraBD();
        //    return datos;
        //}


        public DataTable daTablaTramitesFlujoFiltros(int idFlujo, string Folio, string RFC, string Nombre, string ApPaterno, string ApMaterno)
        {
            bd BD = new bd();
            StringBuilder SqlCmd = new StringBuilder("SELECT * FROM vw_tramiteP WHERE IdFlujo = " + idFlujo.ToString() + " AND FolioCompuesto like '%"+ Folio + "%' AND DatosHtml LIKE '%"+ RFC + "%' AND DatosHtml LIKE '%" + Nombre + "%' AND DatosHtml LIKE '%" + ApPaterno + "%' AND DatosHtml LIKE '%" + ApMaterno + "%'  ORDER BY FechaRegistro DESC");
            //  StringBuilder SqlCmd = new StringBuilder("SELECT * FROM vw_tramiteP WHERE IdFlujo = " + idFlujo.ToString() + "  ORDER BY FechaRegistro DESC");
            DataTable datos = BD.leeDatos(SqlCmd.ToString());
            BD.cierraBD();
            return datos;
        }

        public DataTable daTablaTramitesFiltros(string Folio, string RFC, string Nombre, string ApPaterno, string ApMaterno, int IdUsuario)
        {
            BaseDeDatos b = new BaseDeDatos();
            b.ExecuteCommandSP("Tramite_Supervision_MisTramites_IdUsuario");
            b.AddParameter("@IdUsuario", (int)IdUsuario, SqlDbType.Int);
            b.AddParameter("@Folio", Folio, SqlDbType.NVarChar);
            b.AddParameter("@RFC", RFC, SqlDbType.NVarChar);
            b.AddParameter("@Nombre", Nombre, SqlDbType.NVarChar);
            b.AddParameter("@ApPaterno", ApPaterno, SqlDbType.NVarChar);
            b.AddParameter("@ApMaterno", ApMaterno, SqlDbType.NVarChar);
            return b.Select();
        }

        //public DataTable daTablaTramitesFiltros(string Folio, string RFC, string Nombre, string ApPaterno, string ApMaterno, int IdUsuario)
        //{
        //    bd BD = new bd();
        //    StringBuilder SqlCmd = new StringBuilder("SELECT * FROM vw_tramiteP WHERE FolioCompuesto like '%" + Folio + "%' AND DatosHtml LIKE '%" + RFC + "%' AND DatosHtml LIKE '%" + Nombre + "%' AND DatosHtml LIKE '%" + ApPaterno + "%' AND DatosHtml LIKE '%" + ApMaterno + "%' and dbo.vw_tramiteP.IdFlujo in (CCCC and Activo = 1)   ORDER BY FechaRegistro DESC");
        //    //  StringBuilder SqlCmd = new StringBuilder("SELECT * FROM vw_tramiteP WHERE IdFlujo = " + idFlujo.ToString() + "  ORDER BY FechaRegistro DESC");
        //    DataTable datos = BD.leeDatos(SqlCmd.ToString());
        //    BD.cierraBD();
        //    return datos;
        //}

        /// <summary>
        /// Obtiene los trámites que puede visualizar el operador.
        /// </summary>
        /// <param name="IdFlujo">Id del Flujo asignado al operador</param>
        /// <returns>[DataTable] Información de los Trámites</returns>
        public DataTable daTablaTramitesFlujo(int idFlujo)
        {
            bd BD = new bd();
            StringBuilder SqlCmd = new StringBuilder("SELECT * FROM vw_tramiteP WHERE IdFlujo = " + idFlujo.ToString() + "  ORDER BY FechaRegistro DESC");
            DataTable datos = BD.leeDatos(SqlCmd.ToString());
            BD.cierraBD();
            return datos;
        }

        public DataTable daTablaTramitesFlujo(int idFlujo, E_EstadoTramite statusTramite)
        {
            bd BD = new bd();
            StringBuilder SqlCmd = new StringBuilder("SELECT * FROM vw_tramiteP WHERE IdFlujo = " + idFlujo.ToString() + " and estado = " + statusTramite.ToString("d") + "  ORDER BY FechaRegistro DESC");
            DataTable datos = BD.leeDatos(SqlCmd.ToString());
            BD.cierraBD();
            return datos;
        }

        public DataTable daTramiteMesaAtencion(int usuarioId)
        {
            bd BD = new bd();
            StringBuilder SqlCmd = new StringBuilder("SELECT Tramite.Id, Folio.FolioCompuesto AS Folio, tramite.FechaRegistro, tramite.FechaSolicitud, TRAM00003P.Nombre + ' ' + TRAM00003P.ApPaterno + ' ' + TRAM00003P.ApMaterno + ' '  AS Contratante, usuarios.Nombre, mesa.Nombre AS Mesa, statusMesa.Nombre AS StatusMesa, (SELECT dbo.calcularTiempo (tramiteMesa.FechaInicio,GETDATE(),1)) as TiempoAtencion FROM tramite INNER JOIN Folio ON tramite.Id = Folio.IdTramite INNER JOIN TRAM00003P ON tramite.Id = TRAM00003P.IdTramite INNER JOIN tramiteMesa ON tramite.Id = tramiteMesa.IdTramite INNER JOIN mesa ON mesa.Id = tramiteMesa.IdMesa INNER JOIN statusMesa ON statusMesa.Id = tramiteMesa.Estado INNER JOIN usuarios ON usuarios.Id = tramiteMesa.IdUsuario WHERE statusMesa.Nombre =  'Atrapado' and tramite.idtipotramite in (select id from tipotramite where idflujo in (select flujoid from usuariosflujo where usuarioId = " + usuarioId.ToString() + " and activo = 1)); ");
            DataTable datos = BD.leeDatos(SqlCmd.ToString());
            BD.cierraBD();
            return datos;
        }

        public DataTable daTramiteMesaPausados(int usuarioId)
        {
            bd BD = new bd();
            StringBuilder SqlCmd = new StringBuilder("SELECT Tramite.Id, Folio.FolioCompuesto AS Folio, tramite.FechaRegistro, tramite.FechaSolicitud, TRAM00003P.Nombre + ' ' + TRAM00003P.ApPaterno + ' ' + TRAM00003P.ApMaterno + ' '  AS Contratante, usuarios.Nombre, mesa.Nombre AS Mesa, statusMesa.Nombre AS StatusMesa, tramiteMesa.ObservacionPublica, tramiteMesa.ObservacionPrivada FROM tramite INNER JOIN Folio ON tramite.Id = Folio.IdTramite INNER JOIN TRAM00003P ON tramite.Id = TRAM00003P.IdTramite INNER JOIN tramiteMesa ON tramite.Id = tramiteMesa.IdTramite INNER JOIN mesa ON mesa.Id = tramiteMesa.IdMesa INNER JOIN statusMesa ON statusMesa.Id = tramiteMesa.Estado INNER JOIN usuarios ON usuarios.Id = tramiteMesa.IdUsuario WHERE statusMesa.Nombre = 'Pausa' and tramite.idtipotramite in (select id from tipotramite where idflujo in (select flujoid from usuariosflujo where usuarioId = " + usuarioId.ToString() + " and activo = 1));");
            DataTable datos = BD.leeDatos(SqlCmd.ToString());
            BD.cierraBD();
            return datos;
        }

        /// <summary>
        /// Obtiene los trámites que puede visualizar el operador.
        /// </summary>
        /// <returns>[DataTable] Información de los Trámites</returns>
        public DataTable daTablaTramitesFlujo()
        {
            bd BD = new bd();
            StringBuilder SqlCmd = new StringBuilder("SELECT * FROM vw_tramiteP ORDER BY FechaRegistro DESC");
            DataTable datos = BD.leeDatos(SqlCmd.ToString());
            BD.cierraBD();
            return datos;
        }

        public DataTable seguimientoTramiteBitacora(int pIdTramite, int pIdMesa)
        {
            bd BD = new bd();
            //StringBuilder SqlCmd = new StringBuilder("SELECT (SELECT statusMesa.Nombre FROM statusMesa WHERE statusMesa.Id = bitacora.Estado) AS Estado, Observacion, ObservacionPrivada, FechaTermino FROM bitacora WHERE IdTramite = " + pIdTramite.ToString() + " and IdMesa = " + pIdMesa.ToString() + " ORDER BY FechaTermino DESC;");
            StringBuilder SqlCmd = new StringBuilder("SELECT (CASE bitacora.IdMesa WHEN -1 THEN 'PROMOTORÍA' WHEN -2 THEN 'LABORATORIO' ELSE (SELECT mesa.Nombre FROM mesa WHERE mesa.Id = bitacora.IdMesa) END) AS Mesa, (SELECT statusMesa.Nombre FROM statusMesa WHERE statusMesa.Id = bitacora.Estado) AS Estado, Observacion, ObservacionPrivada, FechaTermino FROM bitacora WHERE IdTramite = " + pIdTramite.ToString() + " AND NOT Estado = (SELECT statusMesa.Id FROM statusMesa WHERE Nombre = 'Pausa') ORDER BY FechaTermino DESC;");
            DataTable datos = BD.leeDatos(SqlCmd.ToString());
            BD.cierraBD();
            return datos;
        }

        /// <summary>
        /// Obtiene los trámites que puede visualizar el perfil del operador.
        /// </summary>
        /// <param name="IdPerfil">Id del Perfil del Operador</param>
        /// <returns>[DataTable] Información de los Trámites</returns>
        public DataTable daTablaTramitesOperacionFechas(int IdPerfil, DateTime fechaDesde, DateTime fechaHasta)
        {
            TimeSpan In = new TimeSpan(00, 00, 00);
            TimeSpan Fin = new TimeSpan(23, 59, 59);
            fechaDesde = fechaDesde.Date + In;
            fechaHasta = fechaHasta.Date + Fin;

            string formato = "yyyy-MM-dd HH:mm:ss";
            string fechaD = fechaDesde.ToString(formato);
            string fechaH = fechaHasta.ToString(formato);

            bd BD = new bd();
            //StringBuilder SqlCmd = new StringBuilder("SELECT * FROM vw_tramiteP where FechaRegistro >= '" + fecha1.ToString("yyyy-MM-dd hh:mm:ss") + "' and FechaRegistro <= '" + fecha2.ToString("yyyy-MM-dd hh:mm:ss") + "' ORDER BY FechaRegistro DESC");
            StringBuilder SqlCmd = new StringBuilder("" +
                "DECLARE @FECHAINI  DATETIME;" +
                "DECLARE @FECHAFIN  DATETIME;" +
                "SET @FECHAINI = TRY_CONVERT(DATETIME, '" + fechaD + "',121);" +
                "SET @FECHAFIN = TRY_CONVERT(DATETIME, '" + fechaH + "',121);" +
                "SELECT * FROM vw_tramiteP WHERE IdFlujo IN (SELECT tipoTramite.IdFlujo FROM tipoTramite WHERE tipoTramite.Id IN (SELECT SEC_MODULOS.ID_ITEM FROM SEC_MODULOS WHERE SEC_MODULOS.ID_MODULO IN (SELECT SEC_ASIGNPERMISSIONS.ID_MODULO FROM SEC_ASIGNPERMISSIONS WHERE SEC_ASIGNPERMISSIONS.ID_PERFIL = " + IdPerfil.ToString() + " AND SEC_ASIGNPERMISSIONS.PERMISO = 1 AND SEC_ASIGNPERMISSIONS.ACTIVO = 1) AND SEC_MODULOS.CATEGORIA = 'WORKFLOW - TIPOTRAMITE')) AND FechaRegistro >= @FECHAINI and FechaRegistro <= @FECHAFIN ORDER BY FechaRegistro DESC");
            DataTable datos = BD.leeDatos(SqlCmd.ToString());
            BD.cierraBD();
            return datos;
        }

        public DataTable daTablaTramitesFechas(int IdPerfil, DateTime fechaDesde, DateTime fechaHasta, int usuarioId)
        {
            TimeSpan In = new TimeSpan(00, 00, 00);
            TimeSpan Fin = new TimeSpan(23, 59, 59);
            fechaDesde = fechaDesde.Date + In;
            fechaHasta = fechaHasta.Date + Fin;

            string formato = "yyyy-MM-dd HH:mm:ss";
            string fechaD = fechaDesde.ToString(formato);
            string fechaH = fechaHasta.ToString(formato);
            bd BD = new bd();
            //StringBuilder SqlCmd = new StringBuilder("SELECT * FROM vw_tramiteP where FechaRegistro >= '" + fecha1.ToString("yyyy-MM-dd hh:mm:ss") + "' and FechaRegistro <= '" + fecha2.ToString("yyyy-MM-dd hh:mm:ss") + "' ORDER BY FechaRegistro DESC");
            StringBuilder SqlCmd = new StringBuilder("" +
                "DECLARE @FECHAINI  DATETIME;" +
                "DECLARE @FECHAFIN  DATETIME;" +
                "SET @FECHAINI = TRY_CONVERT(DATETIME, '" + fechaD + "',121);" +
                "SET @FECHAFIN = TRY_CONVERT(DATETIME, '" + fechaH + "',121);" +
                "SELECT * FROM vw_tramiteP WHERE FechaRegistro >= @FECHAINI  and FechaRegistro <= @FECHAFIN and dbo.vw_tramiteP.IdFlujo in (select flujoid from usuariosflujo where usuarioid= 250 and Activo = 1)   ORDER BY FechaRegistro DESC");
            DataTable datos = BD.leeDatos(SqlCmd.ToString());
            BD.cierraBD();
            return datos;
        }

        /// <summary>
        /// Obtiene los trámites que puede visualizar el perfil del operador.
        /// </summary>
        /// <param name="IdPerfil">Id del Perfil del Operador</param>
        /// <returns>[DataTable] Información de los Trámites</returns>
        public DataTable daTablaTramitesOperacion(int IdPerfil)
        {
            bd BD = new bd();
            StringBuilder SqlCmd = new StringBuilder("SELECT * FROM vw_tramiteP WHERE vw_tramiteP.IdFlujo IN (SELECT tipoTramite.IdFlujo FROM tipoTramite WHERE tipoTramite.Id IN (SELECT SEC_MODULOS.ID_ITEM FROM SEC_MODULOS WHERE SEC_MODULOS.ID_MODULO IN (SELECT SEC_ASIGNPERMISSIONS.ID_MODULO FROM SEC_ASIGNPERMISSIONS WHERE SEC_ASIGNPERMISSIONS.ID_PERFIL = " + IdPerfil.ToString() + " AND SEC_ASIGNPERMISSIONS.PERMISO = 1 AND SEC_ASIGNPERMISSIONS.ACTIVO = 1) AND SEC_MODULOS.CATEGORIA = 'WORKFLOW - TIPOTRAMITE'))  ORDER BY FechaRegistro DESC");
            DataTable datos = BD.leeDatos(SqlCmd.ToString());
            BD.cierraBD();
            return datos;
        }

        public DataTable daTablaTramitesOperacion(int IdPerfil, E_EstadoTramite statusTramite)
        {
            bd BD = new bd();
            StringBuilder SqlCmd = new StringBuilder("SELECT * FROM vw_tramiteP WHERE vw_tramiteP.IdFlujo IN (SELECT tipoTramite.IdFlujo FROM tipoTramite WHERE tipoTramite.Id IN (SELECT SEC_MODULOS.ID_ITEM FROM SEC_MODULOS WHERE SEC_MODULOS.ID_MODULO IN (SELECT SEC_ASIGNPERMISSIONS.ID_MODULO FROM SEC_ASIGNPERMISSIONS WHERE SEC_ASIGNPERMISSIONS.ID_PERFIL = " + IdPerfil.ToString() + " AND SEC_ASIGNPERMISSIONS.PERMISO = 1 AND SEC_ASIGNPERMISSIONS.ACTIVO = 1) AND SEC_MODULOS.CATEGORIA = 'WORKFLOW - TIPOTRAMITE')) AND vw_tramiteP.Estado IN (" + statusTramite.ToString("d") + ") ORDER BY FechaRegistro DESC");
            DataTable datos = BD.leeDatos(SqlCmd.ToString());
            BD.cierraBD();
            return datos;
        }

        public DataTable daTablaTramitesLaboratorios(int IdProveedor, E_EstadoTramite statusTramite)
        {
            bd BD = new bd();
            //StringBuilder SqlCmd = new StringBuilder("SELECT * FROM vw_tramiteP WHERE vw_tramiteP.IdFlujo IN (SELECT tipoTramite.IdFlujo FROM tipoTramite WHERE tipoTramite.Id IN (SELECT SEC_MODULOS.ID_ITEM FROM SEC_MODULOS WHERE SEC_MODULOS.ID_MODULO IN (SELECT SEC_ASIGNPERMISSIONS.ID_MODULO FROM SEC_ASIGNPERMISSIONS WHERE SEC_ASIGNPERMISSIONS.ID_PERFIL = " + IdProveedor.ToString() + " AND SEC_ASIGNPERMISSIONS.PERMISO = 1 AND SEC_ASIGNPERMISSIONS.ACTIVO = 1) AND SEC_MODULOS.CATEGORIA = 'WORKFLOW - TIPOTRAMITE')) AND vw_tramiteP.Estado IN (" + statusTramite.ToString("d") + ") ORDER BY FechaRegistro DESC");
            StringBuilder SqlCmd = new StringBuilder("SELECT * FROM vw_tramiteP WHERE vw_tramiteP.Id IN (SELECT CitasMedicas.IdTramite FROM CitasMedicas WHERE LaboratorioHospital = " + IdProveedor.ToString() + ") AND vw_tramiteP.Estado IN (" + statusTramite.ToString("d") + ") ORDER BY FechaRegistro DESC");
            DataTable datos = BD.leeDatos(SqlCmd.ToString());
            BD.cierraBD();
            return datos;
        }

        public DataTable daTablaTramitesLaboratoriosFechas(int IdProveedor, E_EstadoTramite statusTramite, DateTime fecha1, DateTime fecha2)
        {
            bd BD = new bd();
            //StringBuilder SqlCmd = new StringBuilder("SELECT * FROM vw_tramiteP WHERE vw_tramiteP.IdFlujo IN (SELECT tipoTramite.IdFlujo FROM tipoTramite WHERE tipoTramite.Id IN (SELECT SEC_MODULOS.ID_ITEM FROM SEC_MODULOS WHERE SEC_MODULOS.ID_MODULO IN (SELECT SEC_ASIGNPERMISSIONS.ID_MODULO FROM SEC_ASIGNPERMISSIONS WHERE SEC_ASIGNPERMISSIONS.ID_PERFIL = " + IdProveedor.ToString() + " AND SEC_ASIGNPERMISSIONS.PERMISO = 1 AND SEC_ASIGNPERMISSIONS.ACTIVO = 1) AND SEC_MODULOS.CATEGORIA = 'WORKFLOW - TIPOTRAMITE')) AND vw_tramiteP.Estado IN (" + statusTramite.ToString("d") + ") ORDER BY FechaRegistro DESC");
            StringBuilder SqlCmd = new StringBuilder("SELECT * FROM vw_tramiteP WHERE vw_tramiteP.Id IN (SELECT CitasMedicas.IdTramite FROM CitasMedicas WHERE LaboratorioHospital = " + IdProveedor.ToString() + ") AND vw_tramiteP.Estado IN (" + statusTramite.ToString("d") + ") AND FechaRegistro >= '" + fecha1.ToString("yyyy-MM-dd hh:mm:ss") + "' and FechaRegistro <= '" + fecha2.ToString("yyyy-MM-dd hh:mm:ss") + "' ORDER BY FechaRegistro DESC");
            DataTable datos = BD.leeDatos(SqlCmd.ToString());
            BD.cierraBD();
            return datos;
        }

        public DataTable daTablaTramitesParaCancelacionFechas(DateTime fecha1, DateTime fecha2, int IdUsuario)
        {
            BaseDeDatos b = new BaseDeDatos();
            b.ExecuteCommandSP("Tramite_Supervision_CancelacionFechas_IdUsuario");
            b.AddParameter("@IdUsuario", (int)IdUsuario, SqlDbType.Int);
            b.AddParameter("@FechaInicio", fecha1, SqlDbType.DateTime);
            b.AddParameter("@FechaFin", fecha2, SqlDbType.DateTime);
            return b.Select();
        }

        public DataTable daTablaTramitesParaCancelacionFiltro(string Folio, string RFC, string Nombre, string ApPaterno, string ApMaterno, int IdUsuario)
        {
            BaseDeDatos b = new BaseDeDatos();
            b.ExecuteCommandSP("Tramite_Supervision_CancelacionFiltro_IdUsuario");
            b.AddParameter("@IdUsuario", (int)IdUsuario, SqlDbType.Int);
            b.AddParameter("@Folio", Folio, SqlDbType.NVarChar);
            b.AddParameter("@RFC", RFC, SqlDbType.NVarChar);
            b.AddParameter("@Nombre", Nombre, SqlDbType.NVarChar);
            b.AddParameter("@ApPaterno", ApPaterno, SqlDbType.NVarChar);
            b.AddParameter("@ApMaterno", ApMaterno, SqlDbType.NVarChar);
            return b.Select();
        }

        //public DataTable daTablaTramitesParaCancelacionFechas(DateTime fecha1, DateTime fecha2, int IdUsuario)
        //{
        //    bd BD = new bd();
        //    StringBuilder SqlCmd = new StringBuilder("SELECT tramite.Id, tramite.FechaRegistro,Folio.FolioCompuesto,tramite.DatosHtml,statusTramite.Nombre,tramite.FechaSolicitud,tramite.IdSisLegados,tramite.kwik, CASE tramite.prioridad WHEN 1 THEN '...' WHEN 2 THEN 'Supervisor' WHEN 3 THEN 'Grandes Sumas' WHEN 4 THEN 'Hombres Clave' WHEN 5 THEN '' END AS Prioridad FROM tramite "+
        //                                             "INNER JOIN tramiteMesa ON tramiteMesa.IdTramite = tramite.Id " +
        //                                             "INNER JOIN statusMesa ON tramiteMesa.Estado = statusMesa.Id " +
        //                                             "INNER JOIN statusTramite ON  tramite.Estado = statusTramite.Id " +
        //                                             "INNER JOIN mesa ON tramiteMesa.IdMesa = mesa.Id " +
        //                                             "INNER JOIN Folio ON Folio.IdTramite = tramite.Id " +
        //                                             "INNER JOIN TRAM00003P ON TRAM00003P.IdTramite = tramite.Id " +
        //                                             "INNER JOIN tipoTramite ON tipoTramite.Id = tramite.IdTipoTramite" +
        //                                             "INNER JOIN flujo ON flujo.Id = tipoTramite.IdFlujo" +
        //                                             "WHERE NOT statusMesa.Nombre IN('Procesado', 'Cancelado', 'Rechazo') " +
        //                                             "AND NOT statusTramite.Nombre IN('Ejecucion', 'Rechazo', 'Cancelado', 'Promotoría Cancela', 'Caducado') " +
        //                                             "AND tramite.FechaRegistro >= '" + fecha1.ToString("yyyy-MM-dd hh:mm:ss") + "' AND tramite.FechaRegistro <= '" + fecha2.ToString("yyyy-MM-dd hh:mm:ss") + "' " +
        //                                             "AND flujo.Id IN(SELECT flujoId FROM usuariosFlujo WHERE usuarioId = " + IdUsuario + " AND activo = 1)" +
        //                                             "GROUP BY tramite.Id, tramite.FechaRegistro, Folio.FolioCompuesto, tramite.DatosHtml, statusTramite.Nombre,tramite.FechaSolicitud, tramite.IdSisLegados, tramite.kwik, Prioridad " +
        //                                             "ORDER BY tramite.FechaRegistro DESC "
        //                                             );
        //    DataTable datos = BD.leeDatos(SqlCmd.ToString());
        //    BD.cierraBD();
        //    return datos;
        //}

        //public DataTable daTablaTramitesParaCancelacionFiltro(string Folio, string RFC, string Nombre, string ApPaterno, string ApMaterno, int IdUsuario)
        //{
        //    bd BD = new bd();
        //    StringBuilder SqlCmd = new StringBuilder("SELECT tramite.Id, tramite.FechaRegistro,Folio.FolioCompuesto,tramite.DatosHtml,statusTramite.Nombre,tramite.FechaSolicitud,tramite.IdSisLegados,tramite.kwik, CASE tramite.prioridad WHEN 1 THEN '...' WHEN 2 THEN 'Supervisor' WHEN 3 THEN 'Grandes Sumas' WHEN 4 THEN 'Hombres Clave' WHEN 5 THEN '' END AS Prioridad FROM tramite " +
        //                                             "INNER JOIN tramiteMesa ON tramiteMesa.IdTramite = tramite.Id " +
        //                                             "INNER JOIN statusMesa ON tramiteMesa.Estado = statusMesa.Id " +
        //                                             "INNER JOIN statusTramite ON  tramite.Estado = statusTramite.Id " +
        //                                             "INNER JOIN mesa ON tramiteMesa.IdMesa = mesa.Id " +
        //                                             "INNER JOIN Folio ON Folio.IdTramite = tramite.Id " +
        //                                             "INNER JOIN TRAM00003P ON TRAM00003P.IdTramite = tramite.Id " +
        //                                             "INNER JOIN tipoTramite ON tipoTramite.Id = tramite.IdTipoTramite" +
        //                                             "INNER JOIN flujo ON flujo.Id = tipoTramite.IdFlujo" +
        //                                             "WHERE NOT statusMesa.Nombre IN('Procesado', 'Cancelado', 'Rechazo') " +
        //                                             "AND NOT statusTramite.Nombre IN('Ejecucion', 'Rechazo', 'Cancelado', 'Promotoría Cancela', 'Caducado') " +
        //                                             "AND flujo.Id IN(SELECT flujoId FROM usuariosFlujo WHERE usuarioId = " + IdUsuario + " AND activo = 1)" +
        //                                             "AND Folio.FolioCompuesto LIKE '%" + Folio + "%' " +
        //                                             "AND TRAM00003P.RFC LIKE '%" + RFC + "%' " +
        //                                             "AND TRAM00003P.Nombre LIKE '%" + Nombre + "%' " +
        //                                             "AND TRAM00003P.ApPaterno LIKE '%" + ApPaterno + "%' " +
        //                                             "AND TRAM00003P.ApMaterno LIKE '%" + ApMaterno + "%' " +
        //                                             "GROUP BY tramite.Id, tramite.FechaRegistro, Folio.FolioCompuesto, tramite.DatosHtml, statusTramite.Nombre,tramite.FechaSolicitud, tramite.IdSisLegados, tramite.kwik, Prioridad " +
        //                                             "ORDER BY tramite.FechaRegistro DESC "
        //                                             );
        //    DataTable datos = BD.leeDatos(SqlCmd.ToString());
        //    BD.cierraBD();
        //    return datos;
        //}

        /// <summary>
        /// Realiza filtros a partir del tipo de trámite
        /// </summary>
        /// <param name="pIdPromotoria"></param>
        /// <param name="TipoTramite"></param>
        /// <returns></returns>
        public DataTable daTablaTramitesPromotoriaTipoTramite(int pIdPromotoria, string TipoTramite)
        {
            bd BD = new bd();
            StringBuilder SqlCmd = new StringBuilder("SELECT * FROM vw_tramitePTIPO WHERE IdPromotoria = " + pIdPromotoria.ToString()  + " AND TipoTramite = '" + TipoTramite.ToString() + "'");
            DataTable datos = BD.leeDatos(SqlCmd.ToString());
            BD.cierraBD();
            return datos;
        }
        public DataTable daTablaTramitesPromotoriaTipoTramiteExcel(int pIdPromotoria, string TipoTramite)
        {
            bd BD = new bd();
            string SqlCmd ="SELECT  " +
                           "FechaRegistro,"+
                           "FolioCompuesto,"+
                           "NumeroOrden,"+
                           "Flujo,"+
                           "Producto,"+
                           "RTRIM(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(DatosHtml, '<P>', ''), '</P>', ''), '<b>', ''), '</b>', '  '), '</br>', '')) AS DatosHtml,"+
                           "FechaSolicitud,"+
                           "EstadoNombre,"+
                           "IdSisLegados,"+
                           "kwik "+
                           "FROM vw_tramitePTIPO WHERE IdPromotoria = " + pIdPromotoria.ToString() + " AND TipoTramite = '" + TipoTramite.ToString() + "'";
            DataTable datos = BD.leeDatos(SqlCmd.ToString());
            BD.cierraBD();
            return datos;
        }

        public DataTable daTablaTramitesCreadorPendientes(int pIdUsuario)
        {
            bd BD = new bd();
            StringBuilder SqlCmd = new StringBuilder("SELECT * FROM vw_tramiteP WHERE IdUsuario =" + pIdUsuario.ToString() + " AND (Estado=" + E_EstadoTramite.Hold.ToString("d") + " OR Estado=" + E_EstadoTramite.Suspendido.ToString("d") + ")");
            DataTable datos = BD.leeDatos(SqlCmd.ToString());
            BD.cierraBD();
            return datos;
        }

        public DataTable daTablaTramitesPromotoriaPendientes(int pIdPromotoria)
        {
            bd BD = new bd();
            StringBuilder SqlCmd = new StringBuilder("SELECT * FROM vw_tramiteP WHERE IdPromotoria = " + pIdPromotoria + " AND (Estado=" + E_EstadoTramite.Hold.ToString("d") + " OR Estado=" + E_EstadoTramite.Suspendido.ToString("d") + " OR Estado = " + E_EstadoTramite.PCI.ToString("d") + " OR Estado = " + E_EstadoTramite.RevPromotoria.ToString("d") + " OR Estado = " + E_EstadoTramite.InfoCitaMedica.ToString("d") + " OR Estado = " + E_EstadoTramite.SuspensionCitaMedica.ToString("d") + ")");
            DataTable datos = BD.leeDatos(SqlCmd.ToString());
            BD.cierraBD();
            return datos;
        }

        public DataTable daTablaTramitesPromotoriaPendientesTipoTramite(int pIdPromotoria, string TipoTramite)
        {
            bd BD = new bd();
            StringBuilder SqlCmd = new StringBuilder("SELECT * FROM vw_tramitePTIPO WHERE TipoTramite = '" + TipoTramite.ToString() + "' AND IdPromotoria = " + pIdPromotoria + " AND (Estado=" + E_EstadoTramite.Hold.ToString("d") + " OR Estado=" + E_EstadoTramite.Suspendido.ToString("d") + ")");
            DataTable datos = BD.leeDatos(SqlCmd.ToString());
            BD.cierraBD();
            return datos;
        }

        public DataTable daTablaTramitesPromotoriaPorEstado(int pIdPromotoria, E_EstadoTramite pEstado)
        {
            bd BD = new bd();
            StringBuilder SqlCmd = new StringBuilder("SELECT * FROM vw_tramiteP WHERE IdPromotoria = " + pIdPromotoria.ToString() + " AND Estado=" + pEstado.ToString("d") + " AND Estado = " + pEstado.ToString("d"));
            DataTable datos = BD.leeDatos(SqlCmd.ToString());
            BD.cierraBD();
            return datos;
        }

        public DataTable daTablaTodosLosTramitesPendientes()
        {
            bd BD = new bd();
            StringBuilder SqlCmd = new StringBuilder("SELECT * FROM vw_tramiteP WHERE (Estado=" + E_EstadoTramite.Hold.ToString("d") + " OR Estado=" + E_EstadoTramite.Suspendido.ToString("d") + ") ORDER BY Id");
            DataTable datos = BD.leeDatos(SqlCmd.ToString());
            BD.cierraBD();
            return datos;
        }

        public bool AlteraEjecucion(EmisionVG oEmisionVG)
        {
            bool resultado = false;
            StringBuilder SqlCmd = new StringBuilder("UPDATE [dbo].[tramite] SET IdSisLegados = '" + oEmisionVG.IdSisLegados + "' WHERE Id = '" + oEmisionVG.IdTramite + "';");
            bd BD = new bd();
            resultado = BD.ejecutaCmd(SqlCmd.ToString());
            BD.cierraBD();
            return resultado;
        }

        public bool AlteraKwik(EmisionVG oEmisionVG)
        {
            bool resultado = false;
            StringBuilder SqlCmd = new StringBuilder("UPDATE [dbo].[tramite] SET kwik = '" + oEmisionVG.kwik + "' WHERE Id = '" + oEmisionVG.IdTramite + "';");
            bd BD = new bd();
            resultado = BD.ejecutaCmd(SqlCmd.ToString());
            BD.cierraBD();
            return resultado;
        }

        /// <summary>
        /// Actualiza un tramite a un estado indicado
        /// </summary>
        /// <param name="pId"></param>
        /// <param name="pEstado"></param>
        /// <returns></returns>
        public Boolean cambiaEstado(int pId, E_EstadoTramite pEstado)
        {
            Boolean resultado = false;

            StringBuilder SqlCmd = new StringBuilder();
            SqlCmd.Append("UPDATE tramite SET Estado=" + pEstado.ToString("d"));
            if (pEstado == wfiplib.E_EstadoTramite.Ejecucion)
                SqlCmd.Append(", FechaTermino = GETDATE() ");
            SqlCmd.Append(" WHERE Id=" + pId.ToString());
            bd BD = new bd();
            resultado = BD.ejecutaCmd(SqlCmd.ToString());
            BD.cierraBD();
            return resultado;
        }

        public Boolean GeneraCitaMedica(int pIdTramite)
        {
            Boolean resultado = false;
            StringBuilder SqlCmd = new StringBuilder();
            SqlCmd.Append("UPDATE CitasMedicas SET CMGenerada = 1");
            SqlCmd.Append(" WHERE IdTramite = " + pIdTramite.ToString());
            bd BD = new bd();
            resultado = BD.ejecutaCmd(SqlCmd.ToString());
            BD.cierraBD();
            return resultado;
        }

        public Boolean CitaMedicaCheckFechaSelected(int pIdTramite)
        {
            Boolean blnResultado = false;

            string strSQL = "SELECT CMGenerada FROM CitasMedicas WHERE IdTramite = " + pIdTramite.ToString() + " AND NOT FechaSeleccionada IS NULL;";
            bd BD = new bd();
            DataTable datos = BD.leeDatos(strSQL);
            if (datos.Rows.Count > 0)
            {
                blnResultado = true;
            }
            datos.Dispose();
            BD.cierraBD();

            return blnResultado;
        }

        public DateTime getEjecutaTramite(int pIdTramite)
        {
            DateTime dtFechaEjecucion = DateTime.Now;

            string strSQL = "SELECT FechaFin FROM tramiteMesa WHERE IdTramite = " + pIdTramite.ToString() + " AND IdMesa IN (SELECT mesa.Id FROM mesa WHERE mesa.Nombre = 'EJECUCIÓN');";
            bd BD = new bd();
            DataTable datos = BD.leeDatos(strSQL);
            if (datos.Rows.Count > 0)
            {
                dtFechaEjecucion = Convert.ToDateTime(datos.Rows[0]["FechaFin"]);
            }
            datos.Dispose();
            BD.cierraBD();

            return dtFechaEjecucion;
        }

        public int getEtapaMesaCalidad(int pIdFlujo)
        {
            int resultado = -1;
            StringBuilder SqlCmd = new StringBuilder();
            SqlCmd.Append("SELECT IdEtapa FROM cat_etapas WHERE IdFlujo = " + pIdFlujo.ToString() + " and Nombre = 'CALIDAD'");
            bd BD = new bd();
            DataTable datos = BD.leeDatos(SqlCmd.ToString());
            if (datos.Rows.Count > 0)
            {
                resultado = Convert.ToInt32(datos.Rows[0]["IdEtapa"]);
            }
            BD.cierraBD();
            return resultado;
        }

        public List<tramiteP> buscaEnDatosHtml(string pNombre, string pRFC, string pFolio)
        {
            List<tramiteP> resultado = new List<tramiteP>();
            string strClausulaWhere = "";

            strClausulaWhere += " WHERE FolioCompuesto LIKE '%" + pFolio + "%'";
            strClausulaWhere += " AND DatosHtml LIKE '%" + pRFC + "%' ";
            strClausulaWhere += " AND DatosHtml LIKE '%" + pNombre + "%'";

            string SqlCmd = "SELECT * FROM vw_tramiteP " + strClausulaWhere + " ORDER BY FechaRegistro; ";
            bd BD = new bd();
            DataTable datos = BD.leeDatos(SqlCmd);
            if (datos.Rows.Count > 0) { foreach (DataRow registro in datos.Rows) resultado.Add(arma(registro)); }
            datos.Dispose();
            BD.cierraBD();
            return resultado;
        }

        public DataTable buscaEnDatosHtmlDataTable(string pDatos)
        {
            DataTable resultado = new DataTable();
            string SqlCmd = "Select * From vw_tramiteP Where DatosHtml Like '%" + pDatos + "%' Order by Id";
            bd BD = new bd();
            resultado = BD.leeDatos(SqlCmd);
            BD.cierraBD();
            return resultado;
        }

        /// <summary>
        /// Elimina un tramite
        /// </summary>
        /// <param name="pId"></param>
        /// <returns></returns>
        public bool elimina(int pId)
        {
            bool resultado = false;
            bd BD = new bd();
            resultado = BD.ejecutaCmd("DELETE From tramite WHERE Id=" + pId.ToString());
            BD.cierraBD();
            return resultado;
        }

        public DataTable TramitesPorMesa(string fechainicial, string fechafinal, string statustramite, string mesa)
        {
            BaseDeDatos b = new BaseDeDatos();
            b.ExecuteCommandQuery("SELECT tramite.Id, " +
            "Folio.FolioCompuesto AS 'FolioCompuesto', " +
            "tramite.FechaRegistro AS 'FechaRegistroTramite', " +
            "tramite.Estado, " +
            "statusTramite.Nombre AS 'StatusTramite', " +
            "tramiteMesa.IdMesa, " +
            "mesa.Nombre AS 'NombreMesa', " +
            "tramiteMesa.FechaRegistro AS 'FechaRegistroMesa', " +
            "tramiteMesa.FechaInicio AS 'FechaInicioMesa', " +
            "tramiteMesa.FechaFin AS 'FechaFinMesa', " +
            "tramiteMesa.Estado AS Estado2, " +
            "statusMesa.Nombre AS 'StatusMesa' " +
            "FROM tramite, Folio, statusTramite, tramiteMesa, mesa, statusMesa " +
            "WHERE tramite.Id = Folio.IdTramite " +
            "AND statusTramite.Id = tramite.Estado " +
            "AND tramite.Id = tramiteMesa.IdTramite " +
            "AND tramiteMesa.IdMesa = mesa.Id " +
            "AND tramiteMesa.Estado = statusMesa.Id " +
            "AND tramite.FechaRegistro > '2019-01-01' " +
            "AND(tramiteMesa.FechaFin IS NULL OR tramiteMesa.Estado = (select statusMesa.Id from statusMesa where statusMesa.Nombre in ('PAUSA'))) " +
            "--     AND tramite.Estado = 18 " +
            "AND tramite.FechaRegistro BETWEEN @fechainicial AND @fechafinal " +
            "AND statusTramite.Nombre=@statustramite " +
            "AND tramiteMesa.IdMesa=@mesa " +
            "ORDER BY tramite.Id,Folio DESC");
            b.AddParameter("@fechainicial", fechainicial, SqlDbType.VarChar);
            b.AddParameter("@fechafinal", fechafinal, SqlDbType.VarChar);
            b.AddParameter("@statustramite", statustramite, SqlDbType.VarChar);
            b.AddParameter("@mesa", mesa, SqlDbType.Int);
            return b.Select();
        }

        public DataTable StatusTramite_DropDownList()
        {
            BaseDeDatos b = new BaseDeDatos();
            b.ExecuteCommandQuery("SELECT Id, Nombre FROM statustramite");
            return b.Select();
        }

        public DataTable Tramites_Selecionar_PorAgentesStatus(int IdPromotoria, DateTime fechaDesde, DateTime fechaHasta, String Agentes, string Status)
        {
            TimeSpan In = new TimeSpan(00, 00, 00);
            TimeSpan Fin = new TimeSpan(23, 59, 59);
            fechaDesde = fechaDesde.Date + In;
            fechaHasta = fechaHasta.Date + Fin;

            string formato = "yyyy-MM-dd HH:mm:ss";
            string fechaD = fechaDesde.ToString(formato);
            string fechaH = fechaHasta.ToString(formato);

            DataTable dt = new DataTable();
            bd data = new bd();
            string q = "DECLARE @FECHAINI  DATETIME;" +
                "DECLARE @FECHAFIN  DATETIME;" +
                "SET @FECHAINI = TRY_CONVERT(DATETIME, '" + fechaD + "',121);" +
                "SET @FECHAFIN = TRY_CONVERT(DATETIME, '" + fechaH + "',121);" +
                "EXECUTE Tramites_Selecionar_PorAgentesStatus '" + IdPromotoria + "' , @FECHAINI, @FECHAFIN,'" + Agentes + "','" + Status + "'";
            dt = data.leeDatos(q);
            data.cierraBD();
            return dt;


        }

        public DataTable Tramites_Selecionar_StatusPCI()
        {
            DataTable dt = new DataTable();
            bd Data = new bd();
            string qData = "EXEC Tramites_Selecionar_StatusPCI ";
            dt = Data.leeDatos(qData);
            Data.cierraBD();
            return dt;
        }

        public DataTable Tramites_Selecionar_StatusPCI_Total()
        {
            DataTable dt = new DataTable();
            bd Data = new bd();
            string qData = "EXEC Tramites_Selecionar_StatusPCI_Total";
            dt = Data.leeDatos(qData);
            Data.cierraBD();
            return dt;
        }

        public DataTable spTramiteNuevo(wfiplib.tramiteP tramite, wfiplib.EmisionVG emisionVG)
        {

#if DEBUG
            System.Diagnostics.Debug.WriteLine("");
            System.Diagnostics.Debug.WriteLine("");

            System.Diagnostics.Debug.WriteLine("spTramiteNuevo");
            System.Diagnostics.Debug.WriteLine(", @IdTipoTramite = " + ((int)tramite.IdTipoTramite).ToString());
            System.Diagnostics.Debug.WriteLine(", @IdPromotoria = " + tramite.IdPromotoria);
            System.Diagnostics.Debug.WriteLine(", @IdUsuario = " + tramite.IdUsuario);
            System.Diagnostics.Debug.WriteLine(", @IdStatus = " + (int)tramite.Estado);
            System.Diagnostics.Debug.WriteLine(", @idPrioridad = " + tramite.Prioridad);

            System.Diagnostics.Debug.WriteLine(", @DatosHtml = '" + tramite.DatosHtml + "'");
            System.Diagnostics.Debug.WriteLine(", @AgenteClave = '" + tramite.AgenteClave + "'");
            System.Diagnostics.Debug.WriteLine(", @NumeroOrden = '" + tramite.NumeroOrden + "'");
            System.Diagnostics.Debug.WriteLine(", @FechaSolicitud = '" +  tramite.FechaSolicitud + "'");
            System.Diagnostics.Debug.WriteLine(", @CPDES = '" + emisionVG.CPDES + "'");
            System.Diagnostics.Debug.WriteLine(", @FolioCPDES = '" + emisionVG.FolioCPDES + "'");
            System.Diagnostics.Debug.WriteLine(", @EstatusCPDES = '" + emisionVG.EstatusCPDES + "'");

            System.Diagnostics.Debug.WriteLine(", @TipoPersona = " + emisionVG.TipoPersona);
            System.Diagnostics.Debug.WriteLine(", @Nombre = '" + emisionVG.Nombre + "'");
            System.Diagnostics.Debug.WriteLine(", @ApPaterno = '" + emisionVG.ApPaterno + "'");
            System.Diagnostics.Debug.WriteLine(", @ApMaterno = '" + emisionVG.ApMaterno + "'");
            System.Diagnostics.Debug.WriteLine(", @Sexo = '" + emisionVG.Sexo + "'");
            System.Diagnostics.Debug.WriteLine(", @FechaNacimiento = '" + emisionVG.FechaNacimiento + "'");
            System.Diagnostics.Debug.WriteLine(", @RFC = '" + emisionVG.RFC + "'");
            System.Diagnostics.Debug.WriteLine(", @FechaConst = '" + emisionVG.FechaConst + "'");
            System.Diagnostics.Debug.WriteLine(", @Nacionalidad = '" + emisionVG.Nacionalidad + "'");
            System.Diagnostics.Debug.WriteLine(", @TitularNombre = '" + emisionVG.TitularNombre + "'");
            System.Diagnostics.Debug.WriteLine(", @TitularApPat = '" + emisionVG.TitularApPat + "'");
            System.Diagnostics.Debug.WriteLine(", @TitularApMat = '" + emisionVG.TitularApMat + "'");
            System.Diagnostics.Debug.WriteLine(", @TitularNacionalidad = '" + emisionVG.TitularNacionalidad + "'");
            System.Diagnostics.Debug.WriteLine(", @TitularSexo = '" + emisionVG.TitularSexo + "'");
            System.Diagnostics.Debug.WriteLine(", @TitularFechaNacimiento = '" + emisionVG.TitularFechaNacimiento + "'");

            System.Diagnostics.Debug.WriteLine(", @SumaAsegurada = '" + emisionVG.SumaAsegurada + "'");
            System.Diagnostics.Debug.WriteLine(", @SumaPolizas = '" + emisionVG.SumaPolizas + "'");
            System.Diagnostics.Debug.WriteLine(", @PrimaTotal = '" + emisionVG.PrimaTotal + "'");
            System.Diagnostics.Debug.WriteLine(", @IdMoneda = '" + emisionVG.IdMoneda + "'");
            System.Diagnostics.Debug.WriteLine(", @Detalle = '" + emisionVG.Detalle + "'");

            System.Diagnostics.Debug.WriteLine(", @HombreClave = '" + emisionVG.HombreClave + "'");
            System.Diagnostics.Debug.WriteLine(", @Entidad = '" + emisionVG.EntidadFederativa + "'");
            System.Diagnostics.Debug.WriteLine(", @TitularEntidad = '" + emisionVG.TitularEntidad + "'");

            System.Diagnostics.Debug.WriteLine(", @IdProducto = " + emisionVG.Producto1);
            System.Diagnostics.Debug.WriteLine(", @IdSubProducto = " + emisionVG.Plan1);

            System.Diagnostics.Debug.WriteLine(", @UsuarioNombre = '" + tramite.UsuarioNombre + "'");

            System.Diagnostics.Debug.WriteLine(", @OneShot = '" + emisionVG.OneShot + "'");
            //System.Diagnostics.Debug.WriteLine(", @MetaLifeEspecial", emisionVG.MetaLifeEspecial);
            //System.Diagnostics.Debug.WriteLine(", @MetaLifeEspecial = " + emisionVG.MetaLifeEspecial);
            System.Diagnostics.Debug.WriteLine(", @IdInstituciones = " + emisionVG.IdInstituciones);

#endif

            BaseDeDatos b = new BaseDeDatos();
            b.ExecuteCommandSP("spTramiteNuevo");
            b.AddParameter("@IdTipoTramite", (int) tramite.IdTipoTramite, SqlDbType.Int);
            b.AddParameter("@IdPromotoria", tramite.IdPromotoria, SqlDbType.Int);
            b.AddParameter("@IdUsuario", tramite.IdUsuario, SqlDbType.Int);
            b.AddParameter("@IdStatus", (int) tramite.Estado, SqlDbType.Int);
            b.AddParameter("@idPrioridad", tramite.Prioridad, SqlDbType.Int);

            b.AddParameter("@DatosHtml", tramite.DatosHtml, SqlDbType.NVarChar);
            b.AddParameter("@AgenteClave", tramite.AgenteClave, SqlDbType.NVarChar);
            b.AddParameter("@NumeroOrden", tramite.NumeroOrden, SqlDbType.NVarChar);
            b.AddParameter("@FechaSolicitud", tramite.FechaSolicitud, SqlDbType.NVarChar);
            b.AddParameter("@CPDES", emisionVG.CPDES, SqlDbType.NVarChar);
            b.AddParameter("@FolioCPDES", emisionVG.FolioCPDES, SqlDbType.NVarChar);
            b.AddParameter("@EstatusCPDES", emisionVG.EstatusCPDES, SqlDbType.NVarChar);

            b.AddParameter("@TipoPersona", (int)emisionVG.TipoPersona, SqlDbType.Int);
            b.AddParameter("@Nombre", emisionVG.Nombre, SqlDbType.NVarChar);
            b.AddParameter("@ApPaterno", emisionVG.ApPaterno, SqlDbType.NVarChar);
            b.AddParameter("@ApMaterno", emisionVG.ApMaterno, SqlDbType.NVarChar);
            b.AddParameter("@Sexo", emisionVG.Sexo, SqlDbType.NVarChar);
            b.AddParameter("@FechaNacimiento", emisionVG.FechaNacimiento, SqlDbType.NVarChar);
            b.AddParameter("@RFC", emisionVG.RFC, SqlDbType.NVarChar);
            b.AddParameter("@FechaConst", emisionVG.FechaConst, SqlDbType.NVarChar);
            b.AddParameter("@Nacionalidad", emisionVG.Nacionalidad, SqlDbType.NVarChar);
            b.AddParameter("@TitularNombre", emisionVG.TitularNombre, SqlDbType.NVarChar);
            b.AddParameter("@TitularApPat", emisionVG.TitularApPat, SqlDbType.NVarChar);
            b.AddParameter("@TitularApMat", emisionVG.TitularApMat, SqlDbType.NVarChar);
            b.AddParameter("@TitularNacionalidad", emisionVG.TitularNacionalidad, SqlDbType.NVarChar);
            b.AddParameter("@TitularSexo", emisionVG.TitularSexo, SqlDbType.NVarChar);
            b.AddParameter("@TitularFechaNacimiento", emisionVG.TitularFechaNacimiento, SqlDbType.NVarChar);

            b.AddParameter("@SumaAsegurada", emisionVG.SumaAsegurada, SqlDbType.NVarChar);
            b.AddParameter("@SumaPolizas", emisionVG.SumaPolizas, SqlDbType.NVarChar);
            b.AddParameter("@PrimaTotal", emisionVG.PrimaTotal, SqlDbType.NVarChar);
            b.AddParameter("@IdMoneda", emisionVG.IdMoneda, SqlDbType.NVarChar);
            b.AddParameter("@Detalle", emisionVG.Detalle, SqlDbType.NVarChar);
            
            b.AddParameter("@HombreClave", emisionVG.HombreClave, SqlDbType.NVarChar);
            b.AddParameter("@Entidad", emisionVG.EntidadFederativa, SqlDbType.NVarChar);
            b.AddParameter("@TitularEntidad", emisionVG.TitularEntidad, SqlDbType.NVarChar);

            b.AddParameter("@IdProducto", emisionVG.Producto1, SqlDbType.Int);
            b.AddParameter("@IdSubProducto", emisionVG.Plan1, SqlDbType.Int);

            b.AddParameter("@UsuarioNombre", tramite.UsuarioNombre, SqlDbType.NVarChar);

            b.AddParameter("@OneShot", emisionVG.OneShot, SqlDbType.NVarChar);
            //b.AddParameter("@MetaLifeEspecial", emisionVG.MetaLifeEspecial, SqlDbType.NVarChar);
            //b.AddParameter("@MetaLifeEspecial", emisionVG.MetaLifeEspecial, SqlDbType.NVarChar);
            b.AddParameter("@IdInstituciones", emisionVG.IdInstituciones, SqlDbType.Int);
            return b.Select();
        }

        #region ****************************** Mantenimiento *****************************************************

        public DataTable TramiteEstatus(string folio)
        {
            BaseDeDatos bd = new BaseDeDatos();

            bd.ExecuteCommandQuery("SELECT Id, " +
            "(SELECT Folio.FolioCompuesto FROM Folio WHERE Folio.IdTramite = tramite.Id) AS Folio, " +
            "IdTipoTramite, " +
            "(SELECT tipoTramite.Nombre FROM tipoTramite WHERE tipoTramite.Id = tramite.IdTipoTramite) AS TipoTramite, " +
            "FechaRegistro, FechaTermino, Estado, " +
            "(SELECT statusTramite.Nombre FROM statusTramite WHERE statusTramite.Id = tramite.Estado) AS statusTramite, " +
            "DatosHtml, IdPromotoria, IdUsuario, AgenteClave, NumeroOrden, FechaSolicitud, tipoTramite, idRamo, prioridad, IdSisLegados, kwik " +
            "FROM tramite " +
            //"WHERE id IN (@id)");
            "WHERE (SELECT Folio.FolioCompuesto FROM Folio WHERE Folio.IdTramite = tramite.Id) LIKE @folio ");
            //bd.AddParameter("@id", id, SqlDbType.Int);
            bd.AddParameter("@folio", "%" + folio + "%", SqlDbType.VarChar);
            return bd.Select();
        }

        #endregion

        #region Procesos para reportes nuevos Jose Luis Villarreal

        public DataSet ReporteProductividadPromotorias(string ann, string promotorias)
        {
            BaseDeDatos b = new BaseDeDatos();

            b.ExecuteCommandSP("Tramite_Seleccionar_ResumenTramites");
            b.AddParameter("@ann", ann, SqlDbType.Int);
            b.AddParameter("@clavepromotoria", promotorias, SqlDbType.VarChar);
            return b.SelectExecuteFunctions();
        }

        public DataTable ReporteTramitesAnuales()
        {
            BaseDeDatos b = new BaseDeDatos();

            b.ExecuteCommandSP("Tramite_Seleccionar_ResumenAnual");
            return b.Select();
        }

        public DataSet ReporteTramitesAnualesGrafico()
        {
            BaseDeDatos b = new BaseDeDatos();
            string consulta = "";
            int InicioOperacion = 2018;
            int NowYear = int.Parse(DateTime.Now.ToString("yyyy"));

            if ((NowYear - 5) > InicioOperacion)
                InicioOperacion = NowYear - 5;

            for (int _Indice = InicioOperacion; _Indice <= NowYear; _Indice++)
            {
                consulta += "exec Tramite_Seleccionar_ResumenAnual_ParaGrafico " + _Indice.ToString() + "; ";
            }



            //b.ExecuteCommandQuery("exec Tramite_Seleccionar_ResumenAnual_ParaGrafico 2018; exec Tramite_Seleccionar_ResumenAnual_ParaGrafico 2019; exec Tramite_Seleccionar_ResumenAnual_ParaGrafico 2020;");
            b.ExecuteCommandQuery(consulta);
            return b.SelectExecuteFunctions();
        }

        public DataTable ReporteAlmacenamientoTramites()
        {
            BaseDeDatos b = new BaseDeDatos();
            b.ExecuteCommandQuery("" +
            "SELECT " +
            "(SELECT COUNT(DATEDIFF(dd, fecharegistro, GETDATE())) FROM tramite WHERE  DATEDIFF(dd, FechaRegistro, GETDATE()) BETWEEN 0 AND 30) AS A, " +
            "(SELECT COUNT(DATEDIFF(dd, fecharegistro, GETDATE())) FROM tramite WHERE  DATEDIFF(dd, FechaRegistro, GETDATE()) BETWEEN 31 AND 60) AS B, " +
            "(SELECT COUNT(DATEDIFF(dd, fecharegistro, GETDATE())) FROM tramite WHERE DATEDIFF(dd, FechaRegistro, GETDATE()) BETWEEN 61 AND 90) AS C, " +
            "(SELECT COUNT(DATEDIFF(dd, fecharegistro, GETDATE())) FROM tramite WHERE DATEDIFF(dd, FechaRegistro, GETDATE()) BETWEEN 91 AND 120) AS D, " +
            "(SELECT COUNT(DATEDIFF(dd, fecharegistro, GETDATE())) FROM tramite WHERE DATEDIFF(dd, FechaRegistro, GETDATE()) BETWEEN 121 AND 150) AS E, " +
            "(SELECT COUNT(DATEDIFF(dd, fecharegistro, GETDATE())) FROM tramite WHERE DATEDIFF(dd, FechaRegistro, GETDATE()) BETWEEN 151 AND 180) AS F, " +
            "(SELECT COUNT(DATEDIFF(dd, fecharegistro, GETDATE())) FROM tramite WHERE DATEDIFF(dd, FechaRegistro, GETDATE()) BETWEEN 181 AND 210) AS G, " +
            "(SELECT COUNT(DATEDIFF(dd, fecharegistro, GETDATE())) FROM tramite WHERE DATEDIFF(dd, FechaRegistro, GETDATE()) BETWEEN 211 AND 240) AS H, " +
            "(SELECT COUNT(DATEDIFF(dd, fecharegistro, GETDATE())) FROM tramite WHERE DATEDIFF(dd, FechaRegistro, GETDATE()) BETWEEN 241 AND 270) AS I, " +
            "(SELECT COUNT(DATEDIFF(dd, fecharegistro, GETDATE())) FROM tramite WHERE DATEDIFF(dd, FechaRegistro, GETDATE()) BETWEEN 271 AND 300) AS J, " +
            "(SELECT COUNT(DATEDIFF(dd, fecharegistro, GETDATE())) FROM tramite WHERE DATEDIFF(dd, FechaRegistro, GETDATE()) BETWEEN 301 AND 330) AS K, " +
            "(SELECT COUNT(DATEDIFF(dd, fecharegistro, GETDATE())) FROM tramite WHERE DATEDIFF(dd, FechaRegistro, GETDATE()) BETWEEN 331 AND 360) AS L, " +
            "(SELECT COUNT(DATEDIFF(dd, fecharegistro, GETDATE())) FROM tramite WHERE DATEDIFF(dd, FechaRegistro, GETDATE()) > 361) AS M, " +
            "((SELECT COUNT(DATEDIFF(dd, fecharegistro, GETDATE())) FROM tramite WHERE DATEDIFF(dd, FechaRegistro, GETDATE()) BETWEEN 91 AND 120) + " +
            "(SELECT COUNT(DATEDIFF(dd, fecharegistro, GETDATE())) FROM tramite WHERE DATEDIFF(dd, FechaRegistro, GETDATE()) BETWEEN 121 AND 150) + " +
            "(SELECT COUNT(DATEDIFF(dd, fecharegistro, GETDATE())) FROM tramite WHERE DATEDIFF(dd, FechaRegistro, GETDATE()) BETWEEN 151 AND 180) + " +
            "(SELECT COUNT(DATEDIFF(dd, fecharegistro, GETDATE())) FROM tramite WHERE DATEDIFF(dd, FechaRegistro, GETDATE()) BETWEEN 181 AND 210) + " +
            "(SELECT COUNT(DATEDIFF(dd, fecharegistro, GETDATE())) FROM tramite WHERE DATEDIFF(dd, FechaRegistro, GETDATE()) BETWEEN 211 AND 240) + " +
            "(SELECT COUNT(DATEDIFF(dd, fecharegistro, GETDATE())) FROM tramite WHERE DATEDIFF(dd, FechaRegistro, GETDATE()) BETWEEN 241 AND 270) + " +
            "(SELECT COUNT(DATEDIFF(dd, fecharegistro, GETDATE())) FROM tramite WHERE DATEDIFF(dd, FechaRegistro, GETDATE()) BETWEEN 271 AND 300) + " +
            "(SELECT COUNT(DATEDIFF(dd, fecharegistro, GETDATE())) FROM tramite WHERE DATEDIFF(dd, FechaRegistro, GETDATE()) BETWEEN 301 AND 330) + " +
            "(SELECT COUNT(DATEDIFF(dd, fecharegistro, GETDATE())) FROM tramite WHERE DATEDIFF(dd, FechaRegistro, GETDATE()) BETWEEN 331 AND 360) + " +
            "(SELECT COUNT(DATEDIFF(dd, fecharegistro, GETDATE())) FROM tramite WHERE DATEDIFF(dd, FechaRegistro, GETDATE()) > 361)) AS Acumulado");
            return b.Select();
        }

        public DataTable ReporteAlmacenamientoTramitesDetalle(string promedio)
        {
            BaseDeDatos b = new BaseDeDatos();
            string consulta = "SELECT tramite.FechaRegistro, Folio.FolioCompuesto, tramite.DatosHtml, cat_EstadoTramite.EstadoTramite AS EstadoNombre, " +
            "tramite.FechaSolicitud,  tramite.IdSisLegados, '' AS Prioridad " +
            "FROM tramite INNER JOIN Folio ON tramite.Id = Folio.IdTramite " +
            "INNER JOIN cat_EstadoTramite ON tramite.Estado = cat_EstadoTramite.idEstadoTramite " +
            "WHERE DATEDIFF(dd, tramite.FechaRegistro, GETDATE()) ";
            if (promedio == "1")
                consulta += "BETWEEN 0 AND 30";
            else if (promedio == "2")
                consulta += "BETWEEN 31 AND 60";
            else if (promedio == "3")
                consulta += "BETWEEN 61 AND 90";
            else if (promedio == "4")
                consulta += "BETWEEN 91 AND 120";
            else if (promedio == "5")
                consulta += "BETWEEN 121 AND 150";
            else if (promedio == "6")
                consulta += "BETWEEN 151 AND 180";
            else if (promedio == "7")
                consulta += "BETWEEN 181 AND 210";
            else if (promedio == "8")
                consulta += "BETWEEN 211 AND 240";
            else if (promedio == "9")
                consulta += "BETWEEN 241 AND 270";
            else if (promedio == "10")
                consulta += "BETWEEN 271 AND 300";
            else if (promedio == "11")
                consulta += "BETWEEN 301 AND 330";
            else if (promedio == "12")
                consulta += "BETWEEN 331 AND 360";
            else if (promedio == "13")
                consulta += "> 361";
            b.ExecuteCommandQuery(consulta);
            return b.Select();
        }

        public DataTable ReporteAlmacenamientoTramitesGrafico()
        {
            BaseDeDatos b = new BaseDeDatos();
            b.ExecuteCommandSP("Tramite_Seleccionar_Almacenamiento_ParaGrafico");
            return b.Select();
        }

        public DataSet ReporteTramitesReingresos(string fechainicial, string fechafinal, string tipotramite)
        {
            BaseDeDatos b = new BaseDeDatos();
            b.ExecuteCommandSP("Tramite_Seleccionar_Reingresos");
            var fecha1 = string.Format("{0:yyyyMMdd 00:00:00}", DateTime.Parse(fechainicial + " " + DateTime.Now.ToLongTimeString()));
            var fecha2 = string.Format("{0:yyyyMMdd 23:59:59}", DateTime.Parse(fechafinal + " " + DateTime.Now.ToLongTimeString()));
            b.AddParameter("@fechainicial", fecha1, SqlDbType.VarChar);
            b.AddParameter("@fechafinal", fecha2, SqlDbType.VarChar);
            b.AddParameter("@tipotramite", tipotramite, SqlDbType.Int);
            return b.SelectExecuteFunctions();
        }



        #endregion
    }

    [Serializable]
    //public class tramite
    public class tramite
    {
        private int mId = 0;
        public int Id { get { return mId; } set { mId = value; } }
        private E_TipoTramite mIdTipoTramite = E_TipoTramite.Ninguno;
        public E_TipoTramite IdTipoTramite { get { return mIdTipoTramite; } set { mIdTipoTramite = value; } }
        private DateTime mFechaRegistro = new DateTime(1900, 1, 1, 0, 0, 0);
        public DateTime FechaRegistro { get { return mFechaRegistro; } set { mFechaRegistro = value; } }
        private DateTime mFechaTermino = new DateTime(1900, 1, 1, 0, 0, 0);
        public DateTime FechaTermino { get { return mFechaTermino; } set { mFechaTermino = value; } }
        public E_EstadoTramite Estado { get; set; }
        private string mEstadoNombre = "";
        public string EstadoNombre { get { return mEstadoNombre; } set { mEstadoNombre = value; } }
        private string mDatosHtml = "";
        public string DatosHtml { get { return mDatosHtml; } set { mDatosHtml = value; } }
        private int mIdPromotoria = 0;
        public int IdPromotoria { get { return mIdPromotoria; } set { mIdPromotoria = value; } }
        private int mIdUsuario = 0;
        public int IdUsuario { get { return mIdUsuario; } set { mIdUsuario = value; } }
        private int mAgenteClave = 0;
        public int AgenteClave { get { return mAgenteClave; } set { mAgenteClave = value; } }
        // NUEVOS DATOS 
        private string mNumeroOrden = "";
        public string NumeroOrden { get { return mNumeroOrden; } set { mNumeroOrden = value; } }
        private string mFechaSolicitud = "";
        public string FechaSolicitud { get { return mFechaSolicitud; } set { mFechaSolicitud = value; } }
        private string mFolioCPDES = "";
        public string FolioCPDES { get { return mFolioCPDES; } set { mFolioCPDES = value; } }
        private string mEstatusCPDES = "";
        public string EstatusCPDES { get { return mEstatusCPDES; } set { mEstatusCPDES = value; } }

        private int mIdFlujo = 0;
        public int IdFlujo { get { return mIdFlujo; } set { mIdFlujo = value; } }
        private string mFlujo = "";
        public string Flujo { get { return mFlujo; } set { mFlujo = value; } }
        private string mTramiteNombre = "";
        public string TramiteNombre { get { return mTramiteNombre; } set { mTramiteNombre = value; } }
        private string mTabla = "";
        public string Tabla { get { return mTabla; } set { mTabla = value; } }
        private string mPromotoriaNombre = "";
        public string PromotoriaNombre { get { return mPromotoriaNombre; } set { mPromotoriaNombre = value; } }
        private int mPromotoriaClave = 0;
        public int PromotoriaClave { get { return mPromotoriaClave; } set { mPromotoriaClave = value; } }
        private string mAgenteNombre = "";
        public string AgenteNombre { get { return mAgenteNombre; } set { mAgenteNombre = value; } }
        private string mUsuarioNombre = "";
        public string UsuarioNombre { get { return mUsuarioNombre; } set { mUsuarioNombre = value; } }
    }
}
