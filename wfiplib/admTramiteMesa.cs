using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace wfiplib
{
    public class admTramiteMesa
    {
        public bool registra(tramiteMesa pTramiteMesa)
        {
            bool resultado = false;
            if (existe(pTramiteMesa.IdTramite, pTramiteMesa.IdMesa))
                resultado = reinicia(pTramiteMesa.IdTramite, pTramiteMesa.IdMesa, pTramiteMesa.Estado, -1, -1);
            else
                resultado = nuevo(pTramiteMesa);
            return resultado;
        }

        public bool registra(tramiteMesa pTramiteMesa, int Procede, int AnswerTo)
        {
            bool resultado = false;
            if (existe(pTramiteMesa.IdTramite, pTramiteMesa.IdMesa))
                resultado = reinicia(pTramiteMesa.IdTramite, pTramiteMesa.IdMesa, pTramiteMesa.Estado, Procede, AnswerTo);
            else
                resultado = nuevo(pTramiteMesa, Procede, AnswerTo);
            return resultado;
        }

        public bool EnviaMesa(tramiteMesa pTramiteMesa, int IdMesaOrigen, int IdMesaDestino, int IdUsuario)
        {
            bool resultado = false;
            if (existe(pTramiteMesa.IdTramite, IdMesaDestino))
                resultado = regresaMesa(pTramiteMesa.IdTramite, wfiplib.E_EstadoMesa.Registro, IdMesaOrigen, IdMesaDestino, IdUsuario);
            else
                resultado = nuevo(pTramiteMesa,IdMesaDestino, IdMesaOrigen, IdMesaDestino);
            return resultado;
        }

        private Boolean existe(int pIdTramite, int pIdMesa)
        {
            Boolean resultado = false;
            String SqlCmd = "SELECT * FROM tramiteMesa WHERE IdTramite = " + pIdTramite.ToString() + " AND IdMesa = " + pIdMesa.ToString();
            bd BD = new bd();
            DataTable datos = BD.leeDatos(SqlCmd);
            resultado = (datos.Rows.Count > 0);
            datos.Dispose();
            BD.cierraBD();
            return resultado;
        }

        /// <summary>
        /// Agrega un Id a la tabla TramiteMesaControl antes de agregarlo a un proceso
        /// </summary>
        /// <returns>Id agregado</returns>
        public int siguienteId()
        {
            int resultado = 0;
            bd BD = new bd();
            resultado = BD.ejecutaCmdScalar("Insert Into tramiteMesaControl(Fecha) Values(GetDate()) SELECT SCOPE_IDENTITY()");
            BD.cierraBD();
            return resultado;
        }

        public bool nuevo(tramiteMesa pTramiteMesa)
        {
            bool resultado = false;
            int id = siguienteId();
            StringBuilder SqlCmd = new StringBuilder("Insert Into tramiteMesa(Id,IdTramite,IdMesa,FechaRegistro,Estado,IdRechazo, Procede, AnswerTo)");
            SqlCmd.Append(" Values(");
            SqlCmd.Append(id.ToString());
            SqlCmd.Append("," + pTramiteMesa.IdTramite.ToString());
            SqlCmd.Append("," + pTramiteMesa.IdMesa.ToString());
            SqlCmd.Append(",getdate()");
            SqlCmd.Append("," + pTramiteMesa.Estado.ToString("d"));
            SqlCmd.Append(",0");
            SqlCmd.Append(",-1");
            SqlCmd.Append(",-1");
            SqlCmd.Append(")");
            bd BD = new bd();
            resultado = BD.ejecutaCmd(SqlCmd.ToString());
            BD.cierraBD();
            return resultado;
        }

        private bool nuevo(tramiteMesa pTramiteMesa, int Procede, int AnswerTo)
        {
            bool resultado = false;
            int id = siguienteId();
            StringBuilder SqlCmd = new StringBuilder("Insert Into tramiteMesa(Id,IdTramite,IdMesa,FechaRegistro,Estado,IdRechazo, Procede, AnswerTo)");
            SqlCmd.Append(" Values(");
            SqlCmd.Append(id.ToString());
            SqlCmd.Append("," + pTramiteMesa.IdTramite.ToString());
            SqlCmd.Append("," + pTramiteMesa.IdMesa.ToString());
            SqlCmd.Append(",getdate()");
            SqlCmd.Append("," + pTramiteMesa.Estado.ToString("d"));
            SqlCmd.Append(",0");
            SqlCmd.Append("," + Procede.ToString());
            SqlCmd.Append("," + AnswerTo.ToString());
            SqlCmd.Append(")");
            bd BD = new bd();
            resultado = BD.ejecutaCmd(SqlCmd.ToString());
            BD.cierraBD();
            return resultado;
        }

        private bool nuevo(tramiteMesa pTramiteMesa, int IdMesa , int Procede, int AnswerTo)
        {
            bool resultado = false;
            int id = siguienteId();
            StringBuilder SqlCmd = new StringBuilder("Insert Into tramiteMesa(Id,IdTramite,IdMesa,FechaRegistro,Estado,IdRechazo, Procede, AnswerTo)");
            SqlCmd.Append(" Values(");
            SqlCmd.Append(id.ToString());
            SqlCmd.Append("," + pTramiteMesa.IdTramite.ToString());
            SqlCmd.Append("," + IdMesa.ToString());
            SqlCmd.Append(",getdate()");
            SqlCmd.Append("," + E_EstadoMesa.FromMesa.ToString("d"));
            SqlCmd.Append(",0");
            SqlCmd.Append("," + Procede.ToString());
            SqlCmd.Append("," + AnswerTo.ToString());
            SqlCmd.Append(")");
            bd BD = new bd();
            resultado = BD.ejecutaCmd(SqlCmd.ToString());
            BD.cierraBD();
            return resultado;
        }

        public bool regresaMesa(int pIdTramite, E_EstadoMesa pEstado, int IdMesaOrigen, int IdMesaDestino, int IdUsuario)
        {
            bool resultado = false;
            StringBuilder SqlCmd = new StringBuilder("Update tramiteMesa");
            SqlCmd.Append(" SET FechaInicio=null");

            if (IdUsuario == -1)
            {
                SqlCmd.Append(",IdUsuario=null");
            }
            else if (IdUsuario == -2)
            {
                SqlCmd.Append("");
            }
            else
            {
                SqlCmd.Append(",IdUsuario=" + IdUsuario.ToString());
            }

            SqlCmd.Append(",Estado=" + pEstado.ToString("d"));
            SqlCmd.Append(",Procede=" + IdMesaOrigen.ToString());
            SqlCmd.Append(",AnswerTo=" + IdMesaOrigen.ToString());
            SqlCmd.Append(" WHERE IdTramite=" + pIdTramite.ToString());
            SqlCmd.Append(" AND IdMesa=" + IdMesaDestino.ToString());
            bd BD = new bd();
            resultado = BD.ejecutaCmd(SqlCmd.ToString());
            BD.cierraBD();
            return resultado;
        }

        /// <summary>
        /// Obtiene el Id de la Masa a la que se tiene que responder
        /// </summary>
        /// <param name="pIdTramite"></param>
        /// <param name="pIdMesa"></param>
        /// <param name="pIdMesaProcede"></param>
        /// <returns></returns>
        public int getMesaFromProcede(int pIdTramite, int pIdMesa, int pIdMesaProcede)
        {
            int intResultado = -1;
            string strSQL = "SELECT Procede FROM tramiteMesa WHERE IdTramite = " + pIdTramite.ToString() + " AND IdMesa = " + pIdMesaProcede.ToString() + ";";
            bd BD = new bd();
            DataTable datos = BD.leeDatos(strSQL);
            if (datos.Rows.Count > 0)
            {
                intResultado = Convert.ToInt32(datos.Rows[0]["Procede"]);
            }
            datos.Dispose();
            BD.cierraBD();
            return intResultado;
        }

        public int getMesaFromToAnswer(int pIdTramite, int pIdMesa, int pIdMesaProcede)
        {
            int intResultado = -1;
            string strSQL = "SELECT AnswerTo FROM tramiteMesa WHERE IdTramite = " + pIdTramite.ToString() + " AND IdMesa = " + pIdMesaProcede.ToString() + ";";
            bd BD = new bd();
            DataTable datos = BD.leeDatos(strSQL);
            if (datos.Rows.Count > 0)
            {
                intResultado = Convert.ToInt32(datos.Rows[0]["AnswerTo"]);
            }
            datos.Dispose();
            BD.cierraBD();
            return intResultado;
        }

        public bool resetMotivosRechazp(int pIdTramite)
        {
            bool resultado = false;
            StringBuilder SqlCmd = new StringBuilder("UPDATE tramiteMesa");
            SqlCmd.Append(" SET IdRechazo = IdRechazo * -1 ");
            SqlCmd.Append(" WHERE IdTramite=" + pIdTramite.ToString());
            SqlCmd.Append(" AND IdRechazo > 0; ");
            bd BD = new bd();
            resultado = BD.ejecutaCmd(SqlCmd.ToString());
            BD.cierraBD();
            return resultado;
        }

        public bool reinicia(int pIdTramite, int pIdMesa, E_EstadoMesa pEstado)
        {
            bool resultado = false;
            StringBuilder SqlCmd = new StringBuilder("Update tramiteMesa");

            if (pEstado == wfiplib.E_EstadoMesa.RevPromotoriaOK)
            {
                SqlCmd.Append(" SET FechaFin=GETDATE()");
            }
            else
            {
                SqlCmd.Append(" SET FechaInicio=null");
                //SqlCmd.Append(",IdUsuario=null");
            }

            SqlCmd.Append(",Estado=" + pEstado.ToString("d"));
            SqlCmd.Append(" WHERE IdTramite=" + pIdTramite.ToString());
            SqlCmd.Append(" AND IdMesa=" + pIdMesa.ToString());
            bd BD = new bd();
            resultado = BD.ejecutaCmd(SqlCmd.ToString());
            BD.cierraBD();
            return resultado;
        }

        /// <summary>
        /// Actualiza un tramite en una mesa al estado indicado
        /// </summary>
        /// <param name="pIdTramite"></param>
        /// <param name="pIdMesa"></param>
        /// <param name="pEstado"></param>
        /// <returns></returns>
        public bool reinicia(int pIdTramite, int pIdMesa, E_EstadoMesa pEstado, int Procede, int AnswerTo)
        {
            bool resultado = false;
            StringBuilder SqlCmd = new StringBuilder("Update tramiteMesa");

            if (pEstado == wfiplib.E_EstadoMesa.RevPromotoriaOK)
            {
                SqlCmd.Append(" SET FechaFin=GETDATE()");
            }
            else
            {
                SqlCmd.Append(" SET FechaInicio=null");
                //SqlCmd.Append(",IdUsuario=null");
            }
            
            SqlCmd.Append(",Estado=" + pEstado.ToString("d"));
            SqlCmd.Append(",Procede=" + Procede.ToString("d"));
            SqlCmd.Append(",AnswerTo=" + AnswerTo.ToString("d"));
            SqlCmd.Append(" WHERE IdTramite=" + pIdTramite.ToString());
            SqlCmd.Append(" AND IdMesa=" + pIdMesa.ToString());
            bd BD = new bd();
            resultado = BD.ejecutaCmd(SqlCmd.ToString());
            BD.cierraBD();
            return resultado;
        }



        /// <summary>
        /// Actualiza un tramite en una mesa al estado indicado
        /// </summary>
        /// <returns></returns>
        public bool reinicia(int pIdTramite, int pIdMesa, E_EstadoMesa pEstado, int Procede, int AnswerTo, bool MantenerUsuario)
        {
            bool resultado = false;
            StringBuilder SqlCmd = new StringBuilder("Update tramiteMesa");

            if (pEstado == wfiplib.E_EstadoMesa.RevPromotoriaOK)
            {
                SqlCmd.Append(" SET FechaFin=GETDATE()");
            }
            else
            {
                SqlCmd.Append(" SET FechaInicio = null");
                if (!MantenerUsuario)
                {
                    SqlCmd.Append(",IdUsuario=null");
                }
            }

            SqlCmd.Append(",Estado=" + pEstado.ToString("d"));
            SqlCmd.Append(",Procede=" + Procede.ToString("d"));
            SqlCmd.Append(",AnswerTo=" + AnswerTo.ToString("d"));
            SqlCmd.Append(" WHERE IdTramite=" + pIdTramite.ToString());
            SqlCmd.Append(" AND IdMesa=" + pIdMesa.ToString());
            bd BD = new bd();
            resultado = BD.ejecutaCmd(SqlCmd.ToString());
            BD.cierraBD();
            return resultado;
        }

        public bool reiniciaDesdePromotoria(int pIdTramite, int pIdMesa, E_EstadoMesa pEstado, bool MantenerUsuario)
        {
            bool resultado = false;
            StringBuilder SqlCmd = new StringBuilder("Update tramiteMesa");

            if (pEstado == wfiplib.E_EstadoMesa.RevPromotoriaOK)
            {
                SqlCmd.Append(" SET FechaFin=GETDATE()");
            }
            else
            {
                SqlCmd.Append(" SET FechaInicio = null");
                if (!MantenerUsuario)
                {
                    SqlCmd.Append(",IdUsuario=null");
                }
            }

            SqlCmd.Append(",Estado=" + pEstado.ToString("d"));
            SqlCmd.Append(" WHERE IdTramite=" + pIdTramite.ToString());
            SqlCmd.Append(" AND IdMesa=" + pIdMesa.ToString());
            bd BD = new bd();
            resultado = BD.ejecutaCmd(SqlCmd.ToString());
            BD.cierraBD();
            return resultado;
        }

        public bool reinicia(int pIdTramite, int pIdMesa, E_EstadoMesa pEstado, int Procede, int AnswerTo, string obsPublicas, int IdUsuario)
        {
            bool resultado = false;
            StringBuilder SqlCmd = new StringBuilder("Update tramiteMesa");

            if (pEstado == wfiplib.E_EstadoMesa.RevPromotoriaOK)
            {
                SqlCmd.Append(" SET FechaInicio = GETDATE() ");
                SqlCmd.Append(",FechaFin = GETDATE() " );
                SqlCmd.Append(",IdUsuario = " + IdUsuario.ToString());
            }
            else
            {
                SqlCmd.Append(" SET FechaInicio = null");
                SqlCmd.Append(",IdUsuario = null");
            }

            SqlCmd.Append(",ObservacionPublica = '" + obsPublicas + "'");
            SqlCmd.Append(",Estado=" + pEstado.ToString("d"));
            SqlCmd.Append(",Procede=" + Procede.ToString("d"));
            SqlCmd.Append(",AnswerTo=" + AnswerTo.ToString("d"));
            SqlCmd.Append(" WHERE IdTramite=" + pIdTramite.ToString());
            SqlCmd.Append(" AND IdMesa=" + pIdMesa.ToString());

            if (pEstado == wfiplib.E_EstadoMesa.RevPromotoriaOK)
            {
                SqlCmd.Append(" AND Estado=" + E_EstadoMesa.RevPromotoria.ToString("d"));
            }

            bd BD = new bd();
            resultado = BD.ejecutaCmd(SqlCmd.ToString());
            BD.cierraBD();
            return resultado;
        }

        public bool sendToCalidad(int pIdTramite, int pIdMesa, E_EstadoMesa pEstado, int Procede, int AnswerTo, string obsPublicas, int IdUsuario)
        {
            bool resultado = false;
            int idTramiteMesa = siguienteId();
            StringBuilder SqlCmd = new StringBuilder("INSERT INTO tramiteMesa (Id, IdTramite, IdMesa, FechaRegistro, IdUsuario, Estado, FechaInicio, FechaFin, ObservacionPublica, ObservacionPrivada, IdRechazo, Procede, AnswerTo) ");
            SqlCmd.Append("VALUES (" + idTramiteMesa.ToString() + ", " + pIdTramite.ToString() + ", " + pIdMesa.ToString() + ", GETDATE(), NULL, " + pEstado.ToString("d") + ", NULL, NULL, '" + obsPublicas + "', '" + obsPublicas + "', 0, " + Procede.ToString() + ", " + AnswerTo.ToString() + ");");
            bd BD = new bd();
            resultado = BD.ejecutaCmd(SqlCmd.ToString());
            BD.cierraBD();
            return resultado;
        }

        public string getMotivosPCI(string IdFlujo, string IdTipoTramite)
        {
            string strMotivos = "";
            string strSQL = "SELECT Id FROM cat_MotivosRechazo WHERE IdFlujo = " + IdFlujo + " AND IdTipoTramite = " + IdTipoTramite + " AND IdTipoRechazo = (SELECT Id FROM cat_TiposRechazo WHERE Nombre = 'PCI')";
            bd BD = new bd();
            DataTable datos = BD.leeDatos(strSQL);
            if (datos.Rows.Count > 0)
            {
                strMotivos = datos.Rows[0]["Id"].ToString();
            }
            datos.Dispose();
            BD.cierraBD();
            return strMotivos;
        }

        public int getStatusTramite(int IdTramite)
        {
            int intValor = -1;
            string strSQL = "SELECT Estado FROM tramite WHERE Id = " + IdTramite.ToString();
            bd BD = new bd();
            DataTable datos = BD.leeDatos(strSQL);
            if (datos.Rows.Count > 0)
                intValor = Convert.ToInt32(datos.Rows[0]["Estado"]);
            datos.Dispose();
            BD.cierraBD();
            return intValor;
        }

        public string getFolio(int IdTramite)
        {
            string strValor = "";
            string strSQL = "SELECT FolioCompuesto FROM Folio WHERE IdTramite = " + IdTramite.ToString();
            bd BD = new bd();
            DataTable datos = BD.leeDatos(strSQL);
            if (datos.Rows.Count > 0)
                strValor = datos.Rows[0]["FolioCompuesto"].ToString();
            datos.Dispose();
            BD.cierraBD();
            return strValor;
        }

        /// <summary>
        /// Realiza la cancelación del trámite
        /// </summary>
        /// <param name="Tramite">Id del Trámite</param>
        /// <param name="DataSession">Información de la sesión para el registro de la actividad.</param>
        /// <param name="statusCancelacion">Indica el status de Cancelación (existen varios estados de cancelación).</param>
        /// <returns>Regresa Verdadero [TRUE] si la cancelación fue exítosa.</returns>
        public bool CancelarTramite(wfiplib.tramiteP Tramite, wfiplib.credencial DataSession, wfiplib.E_EstadoTramite statusCancelacion, wfiplib.tramiteMesa tramiteMesa)
        {
            //bd BD = null;
            bool blnResultado = false;
            string strStatusName = "";

            try
            {
                switch (statusCancelacion)
                {
                    case E_EstadoTramite.Cancelado:
                        strStatusName = "Cancelado";
                        break;
                }

#if DEBUG
                System.Diagnostics.Debug.WriteLine("exec WFOTramiteCancelar ");
                System.Diagnostics.Debug.WriteLine("@tramiteId = " + Tramite.Id.ToString());
                System.Diagnostics.Debug.WriteLine(", @tramiteTipoId = " + ((int)tramiteMesa.IdTipoTramite).ToString());
                System.Diagnostics.Debug.WriteLine(", @flujoId = " + Tramite.IdFlujo.ToString());
                System.Diagnostics.Debug.WriteLine(", @mesaId = " + tramiteMesa.IdMesa.ToString());
                System.Diagnostics.Debug.WriteLine(", @usuarioId = " + DataSession.Id.ToString());
                System.Diagnostics.Debug.WriteLine(", @ObsPub = '" + tramiteMesa.ObservacionPublica + "'");
                System.Diagnostics.Debug.WriteLine(", @ObsPrv = '" + tramiteMesa.ObservacionPrivada + "'");

#endif

                BaseDeDatos b = new BaseDeDatos();

                b.ExecuteCommandSP("WFOTramiteCancelar");
                b.AddParameter("@tramiteId", Tramite.Id, SqlDbType.Int);
                b.AddParameter("@tramiteTipoId", (int)tramiteMesa.IdTipoTramite, SqlDbType.Int);
                b.AddParameter("@flujoId", Tramite.IdFlujo, SqlDbType.Int);
                b.AddParameter("@mesaId", tramiteMesa.IdMesa, SqlDbType.Int);
                b.AddParameter("@usuarioId", DataSession.Id, SqlDbType.Int);
                b.AddParameter("@ObsPub", tramiteMesa.ObservacionPublica, SqlDbType.NVarChar);
                b.AddParameter("@ObsPrv", tramiteMesa.ObservacionPrivada, SqlDbType.NVarChar);
                b.SelectExecuteFunctions();
                blnResultado = true;


                //string strSQL = "UPDATE tramiteMesa SET Estado = (SELECT statusMesa.Id FROM statusMesa WHERE statusMesa.Nombre = '" + strStatusName + "'), ObservacionPublica = '" + tramiteMesa.ObservacionPublica + "', ObservacionPrivada = '" + tramiteMesa.ObservacionPrivada + "', FechaFin = GETDATE() WHERE tramiteMesa.IdTramite = " + Tramite.Id.ToString() + " AND tramiteMesa.IdMesa IN (SELECT tramiteMesa.IdMesa FROM tramiteMesa WHERE NOT tramiteMesa.Estado IN (SELECT statusMesa.Id FROM statusMesa WHERE statusMesa.Nombre IN ('Registro', 'Atrapado', 'Hold', 'RevisionHold', 'ReingresoHold', 'SolicitudApoyo', 'ReingresoApoyo', 'Pausa', 'Rechazo', 'Suspendido', 'RevisionSuspencion', 'ReingresoSuspencion', 'PCI', 'ReingresoPCI', 'RevisionPCI', 'Complementario', 'Revisión con Prospecto', 'Confirmación Pendiente', 'En espera de resultados', 'Información para Citas Medicas', 'Revisión Promotoría KO', 'Revisión Promotoría OK', 'Cancelado'))); ";
                //strSQL += "UPDATE tramite SET tramite.Estado = (SELECT statusTramite.Id FROM statusTramite WHERE statusTramite.Nombre = '" + strStatusName + "'), tramite.FechaTermino = GETDATE() WHERE tramite.Id = " + Tramite.Id.ToString() + "; ";
                //strSQL += "INSERT INTO bitacora (IdFlujo, IdTipoTramite, IdTramite, IdMesa, Usuario, FechaInicio, FechaTermino, Estado, Observacion, ObservacionPrivada, IdUsuario) VALUES (" + Tramite.IdFlujo.ToString() + ", " + Tramite.IdTipoTramite.ToString("d") + ", " + Tramite.Id.ToString() + ", -1, '" + DataSession.Usuario.ToString() + "', GETDATE(), GETDATE(), (SELECT statusMesa.Id FROM statusMesa WHERE statusMesa.Nombre = '" + strStatusName + "'), '" + tramiteMesa.ObservacionPublica + "', '" + tramiteMesa.ObservacionPrivada + "', " + DataSession.Id.ToString() + ")";
                //BD = new bd();
                //blnResultado = BD.ejecutaCmd(strSQL);
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }
            finally
            {
                //BD.cierraBD();
            }

            return blnResultado;
        }

        /// <summary>
        /// Actualiza los datos de un tramite en una mesa
        /// </summary>
        /// <param name="pIdTramite"></param>
        /// <param name="pIdMesa"></param>
        /// <param name="pEstado"></param>
        /// <param name="pObservacionPublica"></param>
        /// <param name="pObservacionPrivada"></param>
        /// <returns></returns>
        public bool cambiaEstado(int pIdTramite, int pIdMesa, E_EstadoMesa pEstado, string pObservacionPublica, string pObservacionPrivada)
        {
            bool resultado = false;
            StringBuilder SqlCmd = new StringBuilder("UPDATE tramiteMesa SET");
            SqlCmd.Append(" Estado=" + pEstado.ToString("d"));
            SqlCmd.Append(",ObservacionPublica='" + pObservacionPublica + "'");
            SqlCmd.Append(",ObservacionPrivada='" + pObservacionPrivada + "'");
            SqlCmd.Append(",FechaFin=GETDATE()");
            SqlCmd.Append(" WHERE IdTramite=" + pIdTramite.ToString() + " AND IdMesa=" + pIdMesa.ToString());
            bd BD = new bd();
            resultado = BD.ejecutaCmd(SqlCmd.ToString());
            try
            {
                if (pEstado == E_EstadoMesa.CMCitaProgramada || pEstado == E_EstadoMesa.CMCitaReProgramada)
                {
                    SqlCmd = null;
                    SqlCmd = new StringBuilder("UPDATE CitasMedicas SET NOTAS  = '" + pObservacionPrivada + "' WHERE IdTramite = " + pIdTramite.ToString() + ";");
                    BD.ejecutaCmd(SqlCmd.ToString());
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            BD.cierraBD();
            return resultado;
        }

        /// <summary>
        /// Verifica si la etapa ha sido terminada. 
        /// </summary>
        /// <param name="pIdTramite"></param>
        /// <param name="pIdMesa"></param>
        /// <returns>[False] Si ya el tramite ha sido procesado por todas las etapas del trámite.</returns>
        public bool VerificaEtapa(int pIdTramite, int pIdMesa)
        {
            bool resultado = false;
            bd BD = new bd();
            StringBuilder SqlCmd = new StringBuilder("SELECT * FROM tramiteMesa");
            SqlCmd.Append(" WHERE IdTramite = " + pIdTramite.ToString());
            SqlCmd.Append(" AND IdMesa <> " + pIdMesa);
            SqlCmd.Append(" AND Estado IN(");
            SqlCmd.Append(E_EstadoMesa.Registro.ToString("d"));
            SqlCmd.Append("," + E_EstadoMesa.Atrapado.ToString("d"));
            SqlCmd.Append("," + E_EstadoMesa.Pausa.ToString("d"));
            SqlCmd.Append("," + E_EstadoMesa.ReingresoHold.ToString("d"));
            SqlCmd.Append("," + E_EstadoMesa.ReingresoSuspencion.ToString("d"));
            SqlCmd.Append("," + E_EstadoMesa.ReingresoPCI.ToString("d"));
            SqlCmd.Append("," + E_EstadoMesa.ReingresoApoyo.ToString("d"));
            SqlCmd.Append("," + E_EstadoMesa.SolicitudApoyo.ToString("d"));
            SqlCmd.Append("," + E_EstadoMesa.SendToMesa.ToString("d"));
            SqlCmd.Append("," + E_EstadoMesa.InfoCitasMedicas.ToString("d"));
            SqlCmd.Append(")");
            DataTable datos = BD.leeDatos(SqlCmd.ToString());
            if (datos.Rows.Count > 0)
            {
                resultado = true;
            }
            BD.cierraBD();
            return resultado;
        }

        public bool tienePendientes_existeSeleccion(int pIdTramite, int pIdMesa)
        {
            bool resultado = false;
            bd BD = new bd();
            // TODO: #Pendiente: Esta consulta deberá ser incluida por estapas para que funcione correctamente
            StringBuilder SqlCmd = new StringBuilder("SELECT id FROM tramiteMesa");
            SqlCmd.Append(" WHERE tramiteMesa.IdTramite = " + pIdTramite.ToString());
            SqlCmd.Append(" AND tramiteMesa.IdMesa IN (SELECT mesa.Id FROM mesa WHERE mesa.Nombre = 'SELECCIÓN') ");
            DataTable datos = BD.leeDatos(SqlCmd.ToString());
            if (datos.Rows.Count > 0)
            {
                resultado = true;
            }
            BD.cierraBD();
            return resultado;
        }

        public bool tienePendientes_gpoSeleccion(int pIdTramite, int pIdMesa)
        {
            bool resultado = false;
            bd BD = new bd();
            // TODO: #Pendiente: Esta consulta deberá ser incluida por estapas para que funcione correctamente
            StringBuilder SqlCmd = new StringBuilder("SELECT * FROM tramiteMesa");
            SqlCmd.Append(" WHERE IdTramite = " + pIdTramite.ToString());
            SqlCmd.Append(" AND (IdMesa <> " + pIdMesa.ToString() + " AND IdMesa IN ( SELECT mesa.Id FROM mesa WHERE mesa.IdEtapa IN ( ((SELECT mesa.IdEtapa FROM mesa WHERE Id = " + pIdMesa.ToString() + ")-1) , (SELECT mesa.IdEtapa FROM mesa WHERE Id = " + pIdMesa.ToString() + ")))) ");
            SqlCmd.Append(" AND Estado NOT IN(");
            SqlCmd.Append(E_EstadoMesa.Procesado.ToString("d"));
            SqlCmd.Append("," + E_EstadoMesa.Rechazo.ToString("d"));
            SqlCmd.Append("," + E_EstadoMesa.Hold.ToString("d"));
            SqlCmd.Append("," + E_EstadoMesa.Suspendido.ToString("d"));
            SqlCmd.Append("," + E_EstadoMesa.PCI.ToString("d"));
            SqlCmd.Append("," + E_EstadoMesa.CMCitaProgramada.ToString("d"));
            SqlCmd.Append("," + E_EstadoMesa.SendToMesa.ToString("d"));
            SqlCmd.Append("," + E_EstadoMesa.Procesable.ToString("d"));
            SqlCmd.Append("," + E_EstadoMesa.NoProcesable.ToString("d"));
            SqlCmd.Append("," + E_EstadoMesa.CPDES.ToString("d"));
            SqlCmd.Append(")");
            DataTable datos = BD.leeDatos(SqlCmd.ToString());
            if (datos.Rows.Count > 0)
            {
                resultado = true;
            }
            BD.cierraBD();
            return resultado;
        }

        public bool tienePendientes_gpoPladDoc(int pIdTramite, int pIdMesa)
        {
            bool resultado = false;
            bd BD = new bd();
            // TODO: #Pendiente: Esta consulta deberá ser incluida por estapas para que funcione correctamente
            StringBuilder SqlCmd = new StringBuilder("SELECT * FROM tramiteMesa");
            SqlCmd.Append(" WHERE IdTramite = " + pIdTramite.ToString());
            SqlCmd.Append(" AND (IdMesa <> " + pIdMesa.ToString() + " AND IdMesa IN ( SELECT mesa.Id FROM mesa WHERE mesa.IdEtapa IN ( ((SELECT mesa.IdEtapa+1 FROM mesa WHERE Id = " + pIdMesa.ToString() + ")) , (SELECT mesa.IdEtapa FROM mesa WHERE Id = " + pIdMesa.ToString() + ")))) ");
            SqlCmd.Append(" AND Estado NOT IN(");
            SqlCmd.Append(E_EstadoMesa.Procesado.ToString("d"));
            SqlCmd.Append("," + E_EstadoMesa.Rechazo.ToString("d"));
            SqlCmd.Append("," + E_EstadoMesa.Hold.ToString("d"));
            SqlCmd.Append("," + E_EstadoMesa.Suspendido.ToString("d"));
            SqlCmd.Append("," + E_EstadoMesa.PCI.ToString("d"));
            SqlCmd.Append("," + E_EstadoMesa.CMCitaProgramada.ToString("d"));
            SqlCmd.Append("," + E_EstadoMesa.SendToMesa.ToString("d"));
            SqlCmd.Append("," + E_EstadoMesa.Procesable.ToString("d"));
            SqlCmd.Append("," + E_EstadoMesa.NoProcesable.ToString("d"));
            SqlCmd.Append("," + E_EstadoMesa.CPDES.ToString("d"));
            SqlCmd.Append(")");
            DataTable datos = BD.leeDatos(SqlCmd.ToString());
            if (datos.Rows.Count > 0)
            {
                resultado = true;
            }
            BD.cierraBD();
            return resultado;
        }

        /// <summary>
        /// Verifica si un tramite tiene pendientes en una mesa
        /// </summary>
        /// <param name="pIdTramite"></param>
        /// <param name="pIdMesa"></param>
        /// <returns></returns>
        public bool tienePendientes(int pIdTramite, int pIdMesa)
        {
            bool resultado = false;
            bd BD = new bd();
            // TODO: #Pendiente: Esta consulta deberá ser incluida por estapas para que funcione correctamente
            StringBuilder SqlCmd = new StringBuilder("SELECT * FROM tramiteMesa");
            SqlCmd.Append(" WHERE IdTramite = " + pIdTramite.ToString());
            SqlCmd.Append(" AND (IdMesa <> " + pIdMesa.ToString() + " AND IdMesa IN ( SELECT mesa.Id FROM mesa WHERE mesa.IdEtapa = (SELECT mesa.IdEtapa FROM mesa WHERE Id = " + pIdMesa.ToString() + "))) ");
            SqlCmd.Append(" AND Estado NOT IN(");
            SqlCmd.Append(E_EstadoMesa.Procesado.ToString("d"));
            SqlCmd.Append("," + E_EstadoMesa.Rechazo.ToString("d"));
            SqlCmd.Append("," + E_EstadoMesa.Hold.ToString("d"));
            SqlCmd.Append("," + E_EstadoMesa.Suspendido.ToString("d"));
            SqlCmd.Append("," + E_EstadoMesa.PCI.ToString("d"));
            SqlCmd.Append("," + E_EstadoMesa.CMCitaProgramada.ToString("d"));
            SqlCmd.Append("," + E_EstadoMesa.SendToMesa.ToString("d"));
            SqlCmd.Append("," + E_EstadoMesa.Procesable.ToString("d"));
            SqlCmd.Append("," + E_EstadoMesa.NoProcesable.ToString("d"));
            SqlCmd.Append("," + E_EstadoMesa.CPDES.ToString("d"));
            SqlCmd.Append(")");
            DataTable datos = BD.leeDatos(SqlCmd.ToString());
            if (datos.Rows.Count > 0)
            {
                resultado = true;
            }
            BD.cierraBD();
            return resultado;
        }

        /// <summary>
        /// Verifica si un tramite tiene apoyos en una mesa
        /// </summary>
        /// <param name="pIdTramite"></param>
        /// <param name="pIdMesaPrincipal"></param>
        /// <returns></returns>
        public bool tieneApoyosPendientes(int pIdTramite, int pIdMesaPrincipal)
        {
            bool resultado = false;
            bd BD = new bd();
            StringBuilder SqlCmd = new StringBuilder("SELECT * FROM tramiteMesa");
            SqlCmd.Append(" WHERE IdTramite = " + pIdTramite.ToString());
            SqlCmd.Append(" AND Procede = " + pIdMesaPrincipal + "");
            SqlCmd.Append(" AND Estado IN (" + E_EstadoMesa.Registro.ToString("d") + ", " + E_EstadoMesa.Atrapado.ToString("d") + ", " + E_EstadoMesa.SolicitudApoyo.ToString("d") + ", " + E_EstadoMesa.ReingresoApoyo.ToString("d") + ") ");
            // SqlCmd.Append(" AND NOT Estado IN (" + E_EstadoMesa.Procesado.ToString("d") + ", " + E_EstadoMesa.Procesable.ToString("d") + ", " + E_EstadoMesa.NoProcesable.ToString("d") + ") ");
            DataTable datos = BD.leeDatos(SqlCmd.ToString());
            if (datos.Rows.Count > 0)
            {
                resultado = true;
            }
            BD.cierraBD();
            return resultado;
        }

        /// <summary>
        /// Obtine los status establecidos por las otra mesas de la misma etapa
        /// </summary>
        /// <param name="pIdtramite">Id del Trámite</param>
        /// <param name="pIdFlujo">Id del Flujo</param>
        /// <param name="pEtapa">Id de la Etapa</param>
        /// <returns>[Listado] Listado del resultado del proceso de las mesas del trámite.</returns>
        public List<E_EstadoMesa> checkStatusEtapa(int pIdtramite, int pIdFlujo, int pEtapa)
        {
            List<E_EstadoMesa> Lista = new List<E_EstadoMesa>();
            bd BD = new bd();
            StringBuilder SqlCmd = new StringBuilder("SELECT Estado FROM vw_tramiteMesa");
            SqlCmd.Append(" WHERE IdTramite=" + pIdtramite.ToString());
            SqlCmd.Append(" AND IdMesa IN (");
            SqlCmd.Append(" SELECT Id FROM mesa");
            SqlCmd.Append(" WHERE IdFlujo=" + pIdFlujo.ToString());
            SqlCmd.Append(" AND IdEtapa=" + pEtapa.ToString());
            SqlCmd.Append(")");
            DataTable datos = BD.leeDatos(SqlCmd.ToString());
            foreach (DataRow reg in datos.Rows)
            {
                if (!reg.IsNull("Estado"))
                    Lista.Add((E_EstadoMesa)reg["Estado"]);
            }
            datos.Dispose();
            BD.cierraBD();
            return Lista;
        }

        public List<E_EstadoMesa> DaEstadosMismoNivel_manualSeleccion(int pIdtramite, int pIdFlujo, int pNivel)
        {
            List<E_EstadoMesa> Lista = new List<E_EstadoMesa>();
            bd BD = new bd();

            StringBuilder SqlCmd = new StringBuilder("SELECT Estado FROM vw_tramiteMesa");
            SqlCmd.Append(" WHERE IdTramite=" + pIdtramite.ToString());
            SqlCmd.Append(" AND IdMesa IN (");
            SqlCmd.Append(" SELECT Id FROM mesa");
            SqlCmd.Append(" WHERE IdFlujo=" + pIdFlujo.ToString());
            SqlCmd.Append(" AND IdEtapa in (" + pNivel.ToString() + ", " + (pNivel-1).ToString() + ")" );
            SqlCmd.Append(")");
            DataTable datos = BD.leeDatos(SqlCmd.ToString());
            foreach (DataRow reg in datos.Rows)
            {
                if (!reg.IsNull("Estado"))
                    Lista.Add((E_EstadoMesa)reg["Estado"]);
            }
            datos.Dispose();
            BD.cierraBD();
            return Lista;
        }


        public List<E_EstadoMesa> DaEstadosMismoNivel(int pIdtramite, int pIdFlujo, int pNivel)
        {
            List<E_EstadoMesa> Lista = new List<E_EstadoMesa>();
            bd BD = new bd();



            StringBuilder SqlCmd = new StringBuilder("SELECT Estado FROM vw_tramiteMesa");
            SqlCmd.Append(" WHERE IdTramite=" + pIdtramite.ToString());
            SqlCmd.Append(" AND IdMesa IN (");
            SqlCmd.Append(" SELECT Id FROM mesa");
            SqlCmd.Append(" WHERE IdFlujo=" + pIdFlujo.ToString());
            SqlCmd.Append(" AND IdEtapa=" + pNivel.ToString());
            SqlCmd.Append(")");
            DataTable datos = BD.leeDatos(SqlCmd.ToString());
            foreach (DataRow reg in datos.Rows)
            {
                if (!reg.IsNull("Estado"))
                    Lista.Add((E_EstadoMesa)reg["Estado"]);
            }
            datos.Dispose();
            BD.cierraBD();
            return Lista;
        }

        public tramiteMesa carga(int pIdTramite)
        {
            tramiteMesa resultado = new tramiteMesa();
            bd BD = new bd();
            StringBuilder SqlCmd = new StringBuilder("SELECT * FROM vw_tramiteMesa");
            SqlCmd.Append(" WHERE IdTramite=" + pIdTramite.ToString());
            DataTable datos = BD.leeDatos(SqlCmd.ToString());
            if (datos.Rows.Count > 0)
            {
                resultado = arma(datos.Rows[0]);
            }
            datos.Dispose();
            BD.cierraBD();
            return resultado;
        }

        public tramiteMesa carga(int pIdTramite, int pIdMesa)
        {
            tramiteMesa resultado = new tramiteMesa();
            bd BD = new bd();
            StringBuilder SqlCmd = new StringBuilder("SELECT * FROM vw_tramiteMesa");
            SqlCmd.Append(" WHERE IdTramite=" + pIdTramite.ToString());
            SqlCmd.Append(" AND IdMesa=" + pIdMesa.ToString());
            DataTable datos = BD.leeDatos(SqlCmd.ToString());
            if (datos.Rows.Count > 0)
            {
                resultado = arma(datos.Rows[0]);
            }
            datos.Dispose();
            BD.cierraBD();
            return resultado;
        }

        private tramiteMesa arma(DataRow pRegistro)
        {
            tramiteMesa resultado = new tramiteMesa();
            if (!pRegistro.IsNull("Id")) resultado.Id = Convert.ToInt32(pRegistro["Id"]);
            if (!pRegistro.IsNull("IdTramite")) resultado.IdTramite = Convert.ToInt32(pRegistro["IdTramite"]);
            if (!pRegistro.IsNull("IdMesa")) resultado.IdMesa = Convert.ToInt32(pRegistro["IdMesa"]);
            if (!pRegistro.IsNull("FechaRegistro")) resultado.FechaRegistro = Convert.ToDateTime(pRegistro["FechaRegistro"]);
            if (!pRegistro.IsNull("IdUsuario")) resultado.IdUsuario = Convert.ToInt32(pRegistro["IdUsuario"]);
            if (!pRegistro.IsNull("Estado")) resultado.Estado = (E_EstadoMesa)pRegistro["Estado"];
            if (!pRegistro.IsNull("EstadoNombre")) resultado.EstadoNombre = Convert.ToString(pRegistro["EstadoNombre"]);
            if (!pRegistro.IsNull("FechaInicio")) resultado.FechaInicio = Convert.ToDateTime(pRegistro["FechaInicio"]);
            if (!pRegistro.IsNull("FechaFin")) resultado.FechaFin = Convert.ToDateTime(pRegistro["FechaFin"]);
            if (!pRegistro.IsNull("ObservacionPublica")) resultado.ObservacionPublica = (Convert.ToString(pRegistro["ObservacionPublica"])).Trim();
            if (!pRegistro.IsNull("ObservacionPrivada")) resultado.ObservacionPrivada = (Convert.ToString(pRegistro["ObservacionPrivada"])).Trim();
            if (!pRegistro.IsNull("IdRechazo")) resultado.IdRechazo = Convert.ToInt32(pRegistro["IdRechazo"]);
            if (!pRegistro.IsNull("IdFlujo")) resultado.IdFlujo = Convert.ToInt32(pRegistro["IdFlujo"]);
            if (!pRegistro.IsNull("Flujo")) resultado.Flujo = Convert.ToString(pRegistro["Flujo"]);
            if (!pRegistro.IsNull("IdTipoTramite")) resultado.IdTipoTramite = (E_TipoTramite)pRegistro["IdTipoTramite"];
            if (!pRegistro.IsNull("TramiteNombre")) resultado.TramiteNombre = Convert.ToString(pRegistro["TramiteNombre"]);
            if (!pRegistro.IsNull("IdPromotoria")) resultado.IdPromotoria = Convert.ToInt32(pRegistro["IdPromotoria"]);
            if (!pRegistro.IsNull("PromotoriaNombre")) resultado.PromotoriaNombre = Convert.ToString(pRegistro["PromotoriaNombre"]);
            if (!pRegistro.IsNull("PromotoriaClave")) resultado.PromotoriaClave = Convert.ToString(pRegistro["PromotoriaNombre"]);
            if (!pRegistro.IsNull("UsuarioNombre")) resultado.UsuarioNombre = Convert.ToString(pRegistro["UsuarioNombre"]);
            if (!pRegistro.IsNull("IdTipoTramite")) resultado.eTipoTramite = (E_TipoTramite)pRegistro["IdTipoTramite"];
            if (!pRegistro.IsNull("MesaNombre")) resultado.MesaNombre = Convert.ToString(pRegistro["MesaNombre"]);
            if (!pRegistro.IsNull("MesaIdTipo")) resultado.MesaIdTipo = Convert.ToInt32(pRegistro["MesaIdTipo"]);
            if (!pRegistro.IsNull("MesaTipoNombre")) resultado.MesaTipoNombre = Convert.ToString(pRegistro["MesaTipoNombre"]);
            if (!pRegistro.IsNull("IdEtapa")) resultado.IdEtapa = Convert.ToInt32(pRegistro["IdEtapa"]);
            if (!pRegistro.IsNull("Etapa")) resultado.Etapa = Convert.ToString(pRegistro["Etapa"]);
            if (!pRegistro.IsNull("IdMesaPadre")) resultado.IdMesaPadre = Convert.ToInt32(pRegistro["IdMesaPadre"]);
            if (!pRegistro.IsNull("MesaPadre")) resultado.MesaPadre = Convert.ToString(pRegistro["MesaPadre"]);
            if (!pRegistro.IsNull("Procede")) resultado.Procede = Convert.ToInt32(pRegistro["Procede"]);
            if (!pRegistro.IsNull("AnswerTo")) resultado.AnswerTo = Convert.ToInt32(pRegistro["AnswerTo"]);
            return resultado;
        }

        public string getPoliza(int pIdTramite)
        {
            string strValor = "";
            string strSQL = "SELECT IdSisLegados FROM tramite WHERE Id = " + pIdTramite.ToString() + " ;";
            bd BD = new bd();
            DataTable datos = BD.leeDatos(strSQL);
            if (datos.Rows.Count > 0)
                strValor = datos.Rows[0]["IdSisLegados"].ToString();
            datos.Dispose();
            BD.cierraBD();
            return strValor;
        }

        /// <summary>
        /// Obtiene si el usuario tiene algún trámite asignado.
        /// </summary>
        /// <param name="pIdMesa">Identificador de la mesa actual.</param>
        /// <param name="pIdUsuario">Identificador del usuario actual.</param>
        /// <returns></returns>
        public tramiteMesa daAsignado(int pIdMesa, int pIdUsuario)
        {
            tramiteMesa resultado = new tramiteMesa();
            bd BD = new bd();
            StringBuilder SqlCmd = new StringBuilder("SELECT TOP 1 * FROM vw_tramiteMesa");
            SqlCmd.Append(" WHERE IdTramite IN (SELECT IdTramite FROM assignTramite WHERE IdUserAssigned = " + pIdUsuario.ToString() + ") ");
            SqlCmd.Append(" AND IdMesa = " + pIdMesa.ToString() + " ");
            SqlCmd.Append(" AND Estado IN (" + E_EstadoMesa.Registro.ToString("d"));
            //SqlCmd.Append("," + E_EstadoMesa.Pausa.ToString("d"));
            SqlCmd.Append("," + E_EstadoMesa.ReingresoHold.ToString("d"));
            SqlCmd.Append("," + E_EstadoMesa.ReingresoApoyo.ToString("d"));
            SqlCmd.Append("," + E_EstadoMesa.ReingresoSuspencion.ToString("d"));
            SqlCmd.Append("," + E_EstadoMesa.ReingresoCitaMedica.ToString("d"));
            SqlCmd.Append("," + E_EstadoMesa.ReingresoPCI.ToString("d"));
            SqlCmd.Append("," + E_EstadoMesa.ResponseFromMesa.ToString("d"));
            SqlCmd.Append("," + E_EstadoMesa.RevPromotoriaKO.ToString("d"));
            SqlCmd.Append("," + E_EstadoMesa.FromMesa.ToString("d"));
            SqlCmd.Append(")");
            //SqlCmd.Append(" AND (IdUsuario IS NULL OR IdUsuario = " + pIdUsuario.ToString() + ")");
            SqlCmd.Append(" ORDER BY Prioridad, FechaRegistro");
            DataTable datos = BD.leeDatos(SqlCmd.ToString());
            if (datos.Rows.Count > 0)
            {
                resultado = arma(datos.Rows[0]);
            }
            datos.Dispose();
            BD.cierraBD();
            return resultado;
        }

        public tramiteMesa daAsignado(int pIdMesa, int pIdUsuario, ref IDbConnection pConn, ref IDbTransaction pTrans, ref wfiplib.dbSQLServer pCapaDatos)
        {
            tramiteMesa objResultado = new tramiteMesa();
            DataTable dtTramiteMesa = null;
            string strSQL = "";

            try
            {
                strSQL = "SELECT TOP 1 * FROM vw_tramiteMesa WHERE IdTramite IN (SELECT IdTramite FROM assignTramite WHERE IdUserAssigned = " + pIdUsuario.ToString() + ")  AND IdMesa = " + pIdMesa.ToString() + " AND Estado IN (SELECT statusMesa.Id FROM statusMesa WHERE statusMesa.Nombre IN ('Registro','ReingresoHold','ReingresoApoyo','ReingresoSuspencion','ReingresoPCI','Response From Mesa','Revisión Promotoría KO','FromMesa','Promotoria Reconsidera','Reingreso Cita Médica')) ORDER BY Prioridad, FechaRegistro ";
                dtTramiteMesa = pCapaDatos.ObtenerDatos(ref pConn, ref pTrans, strSQL);
                if (dtTramiteMesa.Rows.Count > 0)
                {
                    objResultado = arma(dtTramiteMesa.Rows[0]);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                dtTramiteMesa = null;
            }
            return objResultado;

        }

        /// <summary>
        /// Obtiene un trámite en una mesa cuyo estado sea Atrapado
        /// </summary>
        /// <param name="pIdMesa"></param>
        /// <param name="pIdUsuario"></param>
        /// <returns></returns>
        public tramiteMesa daAtrapado(int pIdMesa, int pIdUsuario)
        {
            tramiteMesa resultado = new tramiteMesa();
            bd BD = new bd();
            StringBuilder SqlCmd = new StringBuilder("SELECT TOP 1 * FROM vw_tramiteMesa");
            SqlCmd.Append(" WHERE IdMesa=" + pIdMesa.ToString());
            SqlCmd.Append(" AND Estado=" + E_EstadoMesa.Atrapado.ToString("d"));
            SqlCmd.Append(" AND IdUsuario=" + pIdUsuario.ToString());
            DataTable datos = BD.leeDatos(SqlCmd.ToString());
            if (datos.Rows.Count > 0)
            {
                resultado = arma(datos.Rows[0]);
            }
            datos.Dispose();
            BD.cierraBD();
            return resultado;
        }

        public tramiteMesa daAtrapado(int pIdMesa, int pIdUsuario, ref IDbConnection pConn, ref IDbTransaction pTrans, ref wfiplib.dbSQLServer pCapaDatos)
        {
            tramiteMesa objResultado = new tramiteMesa();
            DataTable dtTramiteMesa = null;
            string strSQL = "";
            try
            {
                strSQL = "SELECT TOP 1 * FROM vw_tramiteMesa WHERE IdMesa = " + pIdMesa.ToString() + " AND Estado = (SELECT statusMesa.Id FROM statusMesa WHERE statusMesa.Nombre = 'Atrapado') AND IdUsuario = " + pIdUsuario.ToString() + "  ; ";
                dtTramiteMesa = pCapaDatos.ObtenerDatos(ref pConn, ref pTrans, strSQL);
                if (dtTramiteMesa.Rows.Count > 0)
                {
                    objResultado = arma(dtTramiteMesa.Rows[0]);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                dtTramiteMesa = null;
           }
            return objResultado;
        }

        /// <summary>
        /// Actualiza el estado a Atrapado de un trámite en una mesa
        /// </summary>
        /// <param name="pIdTramite">Número de trámite</param>
        /// <param name="pIdMesa">Número de mesa</param>
        /// <param name="pIdUsuario">Identificador de usuario</param>
        /// <returns>Verdadero si el proceso es exitoso, falso si es erróneo</returns>
        public bool atrapa(int pIdTramite, int pIdMesa, int pIdUsuario)
        {
            bool resultado = false;
            StringBuilder SqlCmd = new StringBuilder("UPDATE tramiteMesa SET");
            SqlCmd.Append(" FechaInicio=GetDate()");
            SqlCmd.Append(",FechaFin= NULL");
            SqlCmd.Append(",IdUsuario=" + pIdUsuario.ToString());
            SqlCmd.Append(",Estado=" + E_EstadoMesa.Atrapado.ToString("d"));
            SqlCmd.Append(" WHERE IdTramite=" + pIdTramite.ToString());
            SqlCmd.Append(" AND IdMesa=" + pIdMesa.ToString());
            SqlCmd.Append(" AND ((Estado=" + E_EstadoMesa.Registro.ToString("d") + " AND (IdUsuario Is NULL OR IdUsuario = " + pIdUsuario.ToString() + "))");
            SqlCmd.Append(" OR (Estado = " + E_EstadoMesa.FromMesa.ToString("d") + " AND (IdUsuario Is NULL OR IdUsuario = " + pIdUsuario.ToString() + "))");
            SqlCmd.Append(" OR (Estado = " + E_EstadoMesa.PromotoriaReconsidera.ToString("d") + " AND (IdUsuario Is NULL OR IdUsuario = " + pIdUsuario.ToString() + "))");
            SqlCmd.Append(" OR (Estado=" + E_EstadoMesa.Atrapado.ToString("d") + " AND IdUsuario=" + pIdUsuario.ToString());
            SqlCmd.Append(" OR (Estado=" + E_EstadoMesa.ReingresoHold.ToString("d") + " AND IdUsuario = " + pIdUsuario.ToString() + ")");
            SqlCmd.Append(" OR (Estado=" + E_EstadoMesa.ReingresoApoyo.ToString("d") + " AND IdUsuario = " + pIdUsuario.ToString() + " )");
            SqlCmd.Append(" OR (Estado=" + E_EstadoMesa.ReingresoSuspencion.ToString("d") + " AND IdUsuario = " + pIdUsuario.ToString() + " )");
            SqlCmd.Append(" OR (Estado=" + E_EstadoMesa.ReingresoPCI.ToString("d") + " AND IdUsuario = " + pIdUsuario.ToString() + " )");
            SqlCmd.Append(" OR (Estado=" + E_EstadoMesa.ReingresoCitaMedica.ToString("d") + " AND IdUsuario = " + pIdUsuario.ToString() + " )");
            SqlCmd.Append(" OR (Estado=" + E_EstadoMesa.ResponseFromMesa.ToString("d") + " AND IdUsuario = " + pIdUsuario.ToString() + " )");
            SqlCmd.Append(" OR (Estado=" + E_EstadoMesa.RevPromotoriaKO.ToString("d") + " AND (IdUsuario IS NULL OR IdUsuario = " + pIdUsuario.ToString() + "))");
            SqlCmd.Append(" OR (Estado=" + E_EstadoMesa.CMCitaProgramada.ToString("d") + " AND (IdUsuario IS NULL OR IdUsuario = " + pIdUsuario.ToString() + "))");
            SqlCmd.Append(" OR (Estado=" + E_EstadoMesa.CMCitaReProgramada.ToString("d") + " AND (IdUsuario IS NULL OR IdUsuario = " + pIdUsuario.ToString() + "))");
            SqlCmd.Append(" OR (Estado=" + E_EstadoMesa.CMConfirmacionPendiente.ToString("d") + " AND (IdUsuario IS NULL OR IdUsuario = " + pIdUsuario.ToString() + "))");
            SqlCmd.Append(" OR (Estado=" + E_EstadoMesa.CMEnEsperaResult.ToString("d") + " AND ( IdUsuario = " + pIdUsuario.ToString() + "))");
            SqlCmd.Append(" OR (Estado=" + E_EstadoMesa.Pausa.ToString("d") + " AND IdUsuario=" + pIdUsuario.ToString() + ")))");
            bd BD = new bd();
            resultado = BD.ejecutaCmd(SqlCmd.ToString());
            BD.cierraBD();
            return resultado;
        }

        public bool atrapa(int pIdTramite, int pIdMesa, int pIdUsuario, ref IDbConnection pConn, ref IDbTransaction pTrans, ref wfiplib.dbSQLServer pCapaDatos)
        {
            bool blnResultado = false;
            string strSentenciaSQL = "";
            StringBuilder objEstado = new StringBuilder("");
            try
            {
                objEstado.Append(" ((Estado=" + E_EstadoMesa.Registro.ToString("d") + " AND (IdUsuario Is NULL OR IdUsuario = " + pIdUsuario.ToString() + "))");
                objEstado.Append(" OR (Estado = " + E_EstadoMesa.FromMesa.ToString("d") + " AND (IdUsuario Is NULL OR IdUsuario = " + pIdUsuario.ToString() + "))");
                objEstado.Append(" OR (Estado = " + E_EstadoMesa.PromotoriaReconsidera.ToString("d") + " AND (IdUsuario Is NULL OR IdUsuario = " + pIdUsuario.ToString() + "))");
                objEstado.Append(" OR (Estado=" + E_EstadoMesa.Atrapado.ToString("d") + " AND IdUsuario=" + pIdUsuario.ToString());
                objEstado.Append(" OR (Estado=" + E_EstadoMesa.ReingresoHold.ToString("d") + " AND IdUsuario = " + pIdUsuario.ToString() + ")");
                objEstado.Append(" OR (Estado=" + E_EstadoMesa.ReingresoApoyo.ToString("d") + " AND IdUsuario = " + pIdUsuario.ToString() + " )");
                objEstado.Append(" OR (Estado=" + E_EstadoMesa.ReingresoSuspencion.ToString("d") + " AND IdUsuario = " + pIdUsuario.ToString() + " )");
                objEstado.Append(" OR (Estado=" + E_EstadoMesa.ReingresoPCI.ToString("d") + " AND IdUsuario = " + pIdUsuario.ToString() + " )");
                objEstado.Append(" OR (Estado=" + E_EstadoMesa.ReingresoCitaMedica.ToString("d") + " AND IdUsuario = " + pIdUsuario.ToString() + " )");
                objEstado.Append(" OR (Estado=" + E_EstadoMesa.ResponseFromMesa.ToString("d") + " AND IdUsuario = " + pIdUsuario.ToString() + " )");
                objEstado.Append(" OR (Estado=" + E_EstadoMesa.RevPromotoriaKO.ToString("d") + " AND (IdUsuario IS NULL OR IdUsuario = " + pIdUsuario.ToString() + "))");
                objEstado.Append(" OR (Estado=" + E_EstadoMesa.Pausa.ToString("d") + " AND IdUsuario=" + pIdUsuario.ToString() + ")))");

                strSentenciaSQL = "UPDATE tramiteMesa SET FechaInicio = GETDATE(), FechaFin = NULL, IdUsuario = " + pIdUsuario.ToString() + ", Estado = (SELECT statusMesa.Id FROM statusMesa WHERE statusMesa.Nombre = 'Atrapado') WHERE IdTramite = " + pIdTramite.ToString() + " AND IdMesa = " + pIdMesa.ToString() + " AND " + objEstado.ToString() + "; ";
                if (pCapaDatos.EjecutarComando(ref pConn, ref pTrans, strSentenciaSQL) > 0)
                {
                    blnResultado = true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return blnResultado;
        }

        /// <summary>
        /// Verifica si el usuario tiene trámites asignados
        /// </summary>
        /// <param name="idUsuario">Id del Usuario.</param>
        /// <returns>Regresa la información del trámite asignado.</returns>
        public DataTable GetTramiteAsignado(int idUsuario)
        {
            DataTable dtTramitesAsignados = null;

            try
            {
                bd BD = new bd();
                StringBuilder SqlCmd = new StringBuilder("SELECT TOP 1 assignTramite.Id, assignTramite.IdUserAssign, assignTramite.IdUserAssigned, assignTramite.IdMesa, assignTramite.IdTramite, (SELECT Folio.FolioCompuesto FROM Folio WHERE Folio.IdTramite = assignTramite.IdTramite) AS Folio, assignTramite.Fecha, assignTramite.Activo FROM assignTramite");
                SqlCmd.Append(" WHERE assignTramite.IdUserAssigned=" + idUsuario.ToString());
                SqlCmd.Append(" AND assignTramite.Activo = 1 ");
                SqlCmd.Append(" ORDER BY assignTramite.Fecha");
                dtTramitesAsignados = BD.leeDatos(SqlCmd.ToString());
                BD.cierraBD();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }

            return dtTramitesAsignados;
        }

        /// <summary>
        /// Obtiene la información de la cita del trámite
        /// </summary>
        /// <param name="idTramite">Id del Trámite.</param>
        /// <returns>Regresa la información del trámite asignado.</returns>
        public DataTable GetTramiteCita(int idTramite)
        {
            DataTable dtCitaMedica = null;

            try
            {
                bd BD = new bd();
                StringBuilder SqlCmd = new StringBuilder("select * from CitasMedicas ");
                SqlCmd.Append(" WHERE IdTramite = " + idTramite.ToString());
                SqlCmd.Append(" AND Activo = 1 ");
                dtCitaMedica = BD.leeDatos(SqlCmd.ToString());
                BD.cierraBD();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }

            return dtCitaMedica;
        }

        /// <summary>
        /// Obtiene la información del Proveedor de Citas Médicas
        /// </summary>
        /// <param name="idTramite">Id del Trámite.</param>
        /// <returns>Regresa la información del proveedor asignado (Correo Electrónico).</returns>
        public string GetTramiteCitaProveedorEMail(int idTramite)
        {
            DataTable dtCitaMedica = null;
            string strMail = "";
            try
            {
                bd BD = new bd();
                StringBuilder SqlCmd = new StringBuilder("SELECT email1 + '; ' + email2 AS EMAIL FROM cat_proveedor  ");
                SqlCmd.Append(" WHERE Id_proveedor = (SELECT CitasMedicas.LaboratorioHospital FROM CitasMedicas WHERE IdTramite = " + idTramite.ToString() + " AND Activo = 1) ");
                dtCitaMedica = BD.leeDatos(SqlCmd.ToString());
                if (dtCitaMedica.Rows.Count > 0)
                {
                    strMail = dtCitaMedica.Rows[0]["EMAIL"].ToString();
                }
                dtCitaMedica.Dispose();
                BD.cierraBD();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }

            return strMail;
        }

        /// <summary>
        /// Obtiene la información para el envío de mails al proveedor
        /// </summary>
        /// <param name="idTramite">Id del Trámite.</param>
        /// <returns>Regresa la información del Correo Electrónico.</returns>
        public string GetTramiteCitaProveedorEMailContent(int idTramite)
        {
            DataTable dtCitaMedica = null;
            string strMail = "";
            try
            {
                bd BD = new bd();
                StringBuilder SqlCmd = new StringBuilder("SELECT 'Favor de confirmar cita médica para el prospecto para el día <strong>' + ");
                SqlCmd.Append(" CASE WHEN CitasMedicas.FechaSeleccionada = 1  THEN ISNULL(Fecha1, '(fecha por definir)') + '</strong>, ' ");
                SqlCmd.Append(" WHEN CitasMedicas.FechaSeleccionada = 2  THEN ISNULL(Fecha2, '(fecha por definir)') + '</strong>, ' ");
                SqlCmd.Append(" WHEN CitasMedicas.FechaSeleccionada = 3  THEN ISNULL(Fecha3, '(fecha por definir)') + '</strong>, ' ");
                SqlCmd.Append(" WHEN CitasMedicas.FechaSeleccionada = 4  THEN ISNULL(Fecha4, '(fecha por definir)') + '</strong>, ' END ");
                SqlCmd.Append(" + ' en el laboratorio/hospital <STRONG>' ");
                SqlCmd.Append(" + (SELECT '[' + cat_proveedor.proveedor + '] ' + cat_proveedor.sucursal FROM cat_proveedor WHERE cat_proveedor.Id_proveedor = CitasMedicas.LaboratorioHospital) ");
                SqlCmd.Append(" + '</STRONG>' ");
                SqlCmd.Append(" + ' <br/><br/><br/><br/>Para realizar la confirmación de las citas por favor ingresar <strong>" + System.Web.Configuration.WebConfigurationManager.AppSettings["appName"].ToString() + "</strong> a: " + System.Web.Configuration.WebConfigurationManager.AppSettings["PaginaWebPublicadaLab"].ToString() + "' AS CORREO ");
                SqlCmd.Append(" FROM CitasMedicas ");
                SqlCmd.Append(" WHERE IdTramite = " + idTramite.ToString() + " AND Activo = 1;");

                dtCitaMedica = BD.leeDatos(SqlCmd.ToString());
                if (dtCitaMedica.Rows.Count > 0)
                {
                    strMail = dtCitaMedica.Rows[0]["CORREO"].ToString();
                }
                dtCitaMedica.Dispose();
                BD.cierraBD();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }

            return strMail;
        }

        /// <summary>
        /// Indica que ya se realizó la asignación al trámite asignado
        /// </summary>
        /// <param name="IdAsignacion">Id de la Asignación.</param>
        /// <returns></returns>
        public bool SetTramiteAsignado(int IdAsignacion)
        {
            bool blnResultado = false;
            StringBuilder SqlCmd = new StringBuilder("UPDATE assignTramite SET");
            SqlCmd.Append(" Activo = 0 " );
            SqlCmd.Append(" WHERE Id = " + IdAsignacion.ToString() + "");
            bd BD = new bd();
            blnResultado = BD.ejecutaCmd(SqlCmd.ToString());
            BD.cierraBD();
            return blnResultado;
        }

        public tramiteMesa daSiguiente(int pIdMesa, int pIdUsuario)
        {
            //TODO: Da el siguiente trámite X configuración: N trámites de c/estado
            tramiteMesa resultado = new tramiteMesa();
            bd BD = new bd();
            StringBuilder SqlCmd = new StringBuilder("SELECT TOP 1 * FROM vw_tramiteMesa");
            //SqlCmd.Append(" WHERE NOT vw_tramiteMesa.IdTramite IN (SELECT assignTramite.IdTramite FROM assignTramite WHERE NOT assignTramite.IdUserAssigned = 0 AND assignTramite.IdMesa IN ( SELECT -1 UNION ALL SELECT mesa.Id FROM mesa WHERE mesa.Id IN (SELECT usuariosMesa.IdMesa FROM usuariosMesa WHERE usuariosMesa.IdUsuario = " + pIdUsuario.ToString() + ") )) ");
            SqlCmd.Append(" WHERE IdMesa = " + pIdMesa.ToString());
            SqlCmd.Append(" AND Estado IN (" + E_EstadoMesa.Registro.ToString("d"));
            SqlCmd.Append("," + E_EstadoMesa.ReingresoHold.ToString("d"));
            SqlCmd.Append("," + E_EstadoMesa.ReingresoApoyo.ToString("d"));
            SqlCmd.Append("," + E_EstadoMesa.ReingresoSuspencion.ToString("d"));
            SqlCmd.Append("," + E_EstadoMesa.ReingresoPCI.ToString("d"));
            SqlCmd.Append("," + E_EstadoMesa.ResponseFromMesa.ToString("d"));
            SqlCmd.Append("," + E_EstadoMesa.RevPromotoriaKO.ToString("d"));
            SqlCmd.Append("," + E_EstadoMesa.FromMesa.ToString("d"));
            SqlCmd.Append("," + E_EstadoMesa.PromotoriaReconsidera.ToString("d"));
            SqlCmd.Append(")");
            SqlCmd.Append(" AND (IdUsuario IS NULL OR IdUsuario = " + pIdUsuario.ToString() + ")");
            SqlCmd.Append(" ORDER BY Prioridad, FechaRegistro");
            DataTable datos = BD.leeDatos(SqlCmd.ToString());
            if (datos.Rows.Count > 0)
            {
                resultado = arma(datos.Rows[0]);
            }
            datos.Dispose();
            BD.cierraBD();
            return resultado;
        }

        public tramiteMesa daSiguiente(int pIdMesa, int pIdUsuario, ref IDbConnection pConn, ref IDbTransaction pTrans, ref wfiplib.dbSQLServer pCapaDatos)
        {
            tramiteMesa objResultado = new tramiteMesa();
            DataTable dtTramiteMesa = null;
            string strSQL = "";

            try
            {
                strSQL = "SELECT TOP 1 * FROM vw_tramiteMesa WHERE IdMesa = " + pIdMesa.ToString() + "  AND (IdUsuario IS NULL OR IdUsuario = " + pIdUsuario.ToString() + ") AND Estado IN (SELECT statusMesa.Id FROM statusMesa WHERE statusMesa.Nombre IN ('Registro','ReingresoHold','ReingresoApoyo','ReingresoSuspencion','ReingresoPCI','Reingreso Cita Médica','Response From Mesa','Revisión Promotoría KO','FromMesa','Promotoria Reconsidera'))  ORDER BY Prioridad, FechaRegistro; ";
                dtTramiteMesa = pCapaDatos.ObtenerDatos(ref pConn, ref pTrans, strSQL);
                if (dtTramiteMesa.Rows.Count > 0)
                {
                    objResultado = arma(dtTramiteMesa.Rows[0]);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                dtTramiteMesa = null;
            }
            return objResultado;
        }

        public tramiteMesa daSiguienteXTipoTramite(int pIdMesa, int pIdTipoTramite)
        {
            //TODO: Da el siguiente trámite X configuración: N trámites de c/estado
            tramiteMesa resultado = new tramiteMesa();
            bd BD = new bd();
            StringBuilder SqlCmd = new StringBuilder("SELECT TOP 1 * FROM vw_tramiteMesa");
            SqlCmd.Append(" WHERE Estado IN (" + E_EstadoMesa.Registro.ToString("d"));
            SqlCmd.Append("," + E_EstadoMesa.ReingresoHold.ToString("d"));
            SqlCmd.Append("," + E_EstadoMesa.ReingresoApoyo.ToString("d"));
            SqlCmd.Append("," + E_EstadoMesa.ReingresoSuspencion.ToString("d"));
            SqlCmd.Append("," + E_EstadoMesa.ReingresoPCI.ToString("d"));
            SqlCmd.Append("," + E_EstadoMesa.ResponseFromMesa.ToString("d"));
            SqlCmd.Append("," + E_EstadoMesa.RevPromotoriaKO.ToString("d"));
            SqlCmd.Append(")");
            SqlCmd.Append(" AND IdMesa=" + pIdMesa.ToString());
            SqlCmd.Append(" AND IdTipoTramite=" + pIdTipoTramite.ToString());
            SqlCmd.Append(" ORDER BY Prioridad, FechaRegistro");
            DataTable datos = BD.leeDatos(SqlCmd.ToString());
            if (datos.Rows.Count > 0)
            {
                resultado = arma(datos.Rows[0]);
            }
            datos.Dispose();
            BD.cierraBD();
            return resultado;
        }

        public tramiteMesa daSiguienteXTipoTramite(int pIdMesa, int pIdTipoTramite, ref IDbConnection pConn, ref IDbTransaction pTrans, ref wfiplib.dbSQLServer pCapaDatos)
        {
            tramiteMesa objResultado = new tramiteMesa();
            DataTable dtTramiteMesa = null;
            string strSQL = "";

            try
            {
                strSQL = "SELECT TOP 1 * FROM vw_tramiteMesa WHERE IdMesa = " + pIdMesa.ToString() + " AND IdTipoTramite = " + pIdTipoTramite.ToString() + " AND Estado IN (SELECT statusMesa.Id FROM statusMesa WHERE statusMesa.Nombre IN ('Registro','ReingresoHold','ReingresoApoyo','ReingresoSuspencion','ReingresoPCI','Reingreso Cita Médica','Response From Mesa','Revisión Promotoría KO','FromMesa','Promotoria Reconsidera')) ORDER BY Prioridad, FechaRegistro; ";
                dtTramiteMesa = pCapaDatos.ObtenerDatos(ref pConn, ref pTrans, strSQL);
                if (dtTramiteMesa.Rows.Count > 0)
                {
                    objResultado = arma(dtTramiteMesa.Rows[0]);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                dtTramiteMesa = null;
            }

            return objResultado;
        }

        public List<tramiteMesa> DaMesasTramiteRechazo(int pIdtramite)
        {
            List<tramiteMesa> Lista = new List<tramiteMesa>();
            bd BD = new bd();
            StringBuilder SqlCmd = new StringBuilder("SELECT * FROM vw_tramiteMesa");
            SqlCmd.Append(" WHERE IdTramite=" + pIdtramite.ToString());
            SqlCmd.Append(" AND Estado=" + E_EstadoMesa.Rechazo.ToString("d"));
            SqlCmd.Append(" ORDER BY IdMesa");
            DataTable datos = BD.leeDatos(SqlCmd.ToString());
            foreach (DataRow reg in datos.Rows)
            {
                Lista.Add(arma(reg));
            }
            datos.Dispose();
            BD.cierraBD();
            return Lista;
        }

        public List<tramiteMesa> DaMesasTramitesCitaMedicaReactiva(int pIdtramite)
        {
            List<tramiteMesa> Lista = new List<tramiteMesa>();
            bd BD = new bd();
            StringBuilder SqlCmd = new StringBuilder("SELECT * FROM vw_tramiteMesa");
            SqlCmd.Append(" WHERE IdTramite=" + pIdtramite.ToString());
            SqlCmd.Append(" AND Estado=" + E_EstadoMesa.InfoCitasMedicas.ToString("d"));
            SqlCmd.Append(" ORDER BY IdMesa");
            DataTable datos = BD.leeDatos(SqlCmd.ToString());
            foreach (DataRow reg in datos.Rows)
            {
                Lista.Add(arma(reg));
            }
            datos.Dispose();
            BD.cierraBD();
            return Lista;
        }

        /// <summary>
        /// Obtenemos la mesa de calidad para el trámite.
        /// </summary>
        /// <returns></returns>
        public DataTable DaMesasCliadad(int pIdTramite)
        {
            bd BD = new bd();
            string strSentenciaSQL = "SELECT * FROM mesa WHERE mesa.Nombre = 'CALIDAD' AND mesa.IdFlujo = (SELECT tipoTramite.IdFlujo FROM tipoTramite WHERE tipoTramite.Id = (SELECT tramite.IdTipoTramite FROM tramite WHERE tramite.Id = " + pIdTramite.ToString() + "))";
            DataTable datos = BD.leeDatos(strSentenciaSQL.ToString());
            datos.Dispose();
            BD.cierraBD();
            return datos;
        }


        public DataTable ConsultaMesaCitaMedica(int pIdTramite)
        {
            bd BD = new bd();
            StringBuilder SqlCmd = new StringBuilder("SELECT tipoTramite.IdFlujo,mesa.Id, mesa.Nombre FROM tramiteMesa");
            SqlCmd.Append(" INNER JOIN tramite ON tramite.Id = tramiteMesa.IdTramite");
            SqlCmd.Append(" INNER JOIN tipoTramite ON tipoTramite.Id = tramite.IdTipoTramite");
            SqlCmd.Append(" INNER JOIN mesa ON mesa.Id = tramiteMesa.IdMesa");
            SqlCmd.Append(" WHERE tramite.Id = "+ pIdTramite.ToString());
            SqlCmd.Append(" AND mesa.Nombre = 'CITAS MÉDICAS'");
            DataTable datos = BD.leeDatos(SqlCmd.ToString());
            BD.cierraBD();
            return datos;
        }

        public List<tramiteMesa> DaMesasTramitesPromotoriaKO(int pIdtramite)
        {
            List<tramiteMesa> Lista = new List<tramiteMesa>();
            bd BD = new bd();
            StringBuilder SqlCmd = new StringBuilder("SELECT * FROM vw_tramiteMesa");
            SqlCmd.Append(" WHERE IdTramite=" + pIdtramite.ToString());
            //SqlCmd.Append(" AND Estado=" + E_EstadoMesa.RevPromotoria.ToString("d"));
            SqlCmd.Append(" AND IdMesa in (select mesa.id from mesa where mesa.nombre = 'CALIDAD')" );
            SqlCmd.Append(" ORDER BY IdMesa");
            DataTable datos = BD.leeDatos(SqlCmd.ToString());
            foreach (DataRow reg in datos.Rows)
            {
                Lista.Add(arma(reg));
            }
            datos.Dispose();
            BD.cierraBD();
            return Lista;
        }


        public List<tramiteMesa> DaMesasTramitesRevisionPromoCitaMedica(int pIdtramite)
        {
            List<tramiteMesa> Lista = new List<tramiteMesa>();
            bd BD = new bd();
            StringBuilder SqlCmd = new StringBuilder("SELECT * FROM vw_tramiteMesa");
            SqlCmd.Append(" WHERE IdTramite=" + pIdtramite.ToString());
            SqlCmd.Append(" AND Estado=" + E_EstadoMesa.RevisionCitaMedica.ToString("d"));
            SqlCmd.Append(" ORDER BY IdMesa");
            DataTable datos = BD.leeDatos(SqlCmd.ToString());
            foreach (DataRow reg in datos.Rows)
            {
                Lista.Add(arma(reg));
            }
            datos.Dispose();
            BD.cierraBD();
            return Lista;
        }

        public string ReviewCalidadObs(int idTramite)
        {
            string observationAnswer = "SIN OBSERVACIONES";

            bd BD = new bd();
            StringBuilder SqlCmd = new StringBuilder("select ObservacionPublica from tramiteMesa where IdTramite = " + idTramite.ToString() + " and IdMesa in (select mesa.Id from mesa where Nombre = 'CALIDAD')");

            DataTable datos = BD.leeDatos(SqlCmd.ToString());
            if (datos.Rows.Count > 0)
            {
                observationAnswer = datos.Rows[0][0].ToString();
            }
            datos.Dispose();
            BD.cierraBD();

            return observationAnswer;
        }

        public bool ReviewCalidad(int idTramite)
        {
            bool answer = false;

            bd BD = new bd();
            StringBuilder SqlCmd = new StringBuilder("select count(*) from tramitemesa");
            SqlCmd.Append(" WHERE IdTramite=" + idTramite.ToString());
            SqlCmd.Append(" AND IdMesa in (select id from mesa where mesa.nombre = 'CALIDAD') ");
            SqlCmd.Append(" AND Estado=" + E_EstadoMesa.RevPromotoria.ToString("d"));

            DataTable datos = BD.leeDatos(SqlCmd.ToString());
            if (datos.Rows.Count > 0)
            {
                if (Convert.ToInt32(datos.Rows[0][0]) == 1)
                {
                    answer = true;
                }
            }
            datos.Dispose();
            BD.cierraBD();

            return answer;
        }

        public List<tramiteMesa> DaMesasTramitesRevisionPromo(int pIdtramite)
        {
            List<tramiteMesa> Lista = new List<tramiteMesa>();
            bd BD = new bd();
            StringBuilder SqlCmd = new StringBuilder("SELECT * FROM vw_tramiteMesa");
            SqlCmd.Append(" WHERE IdTramite=" + pIdtramite.ToString());
            SqlCmd.Append(" AND Estado=" + E_EstadoMesa.RevPromotoria.ToString("d"));
            SqlCmd.Append(" ORDER BY IdMesa");
            DataTable datos = BD.leeDatos(SqlCmd.ToString());
            foreach (DataRow reg in datos.Rows)
            {
                Lista.Add(arma(reg));
            }
            datos.Dispose();
            BD.cierraBD();
            return Lista;
        }

        public string daMesasNombre (int idMesa)
        {
            string answer = "";

            bd BD = new bd();
            StringBuilder SqlCmd = new StringBuilder("select mesa.nombre from mesa where mesa.id = " + idMesa.ToString());
            DataTable datos = BD.leeDatos(SqlCmd.ToString());

            if (datos.Rows.Count > 0)
            {
                answer = datos.Rows[0][0].ToString();
            }

            datos.Dispose();
            BD.cierraBD();

            return answer;
        }

        public List<tramiteMesa> DaMesasTramitesRevisionHold(int pIdtramite)
        {
            List<tramiteMesa> Lista = new List<tramiteMesa>();
            bd BD = new bd();
            StringBuilder SqlCmd = new StringBuilder("SELECT * FROM vw_tramiteMesa");
            SqlCmd.Append(" WHERE IdTramite=" + pIdtramite.ToString());
            SqlCmd.Append(" AND Estado=" + E_EstadoMesa.RevisionHold.ToString("d"));
            SqlCmd.Append(" ORDER BY IdMesa");
            DataTable datos = BD.leeDatos(SqlCmd.ToString());
            foreach (DataRow reg in datos.Rows)
            {
                Lista.Add(arma(reg));
            }
            datos.Dispose();
            BD.cierraBD();
            return Lista;
        }

        public List<tramiteMesa> DaMesasTramitesRevisionHoldSuspencion(int pIdtramite)
        {
            List<tramiteMesa> Lista = new List<tramiteMesa>();
            bd BD = new bd();
            StringBuilder SqlCmd = new StringBuilder("SELECT * FROM vw_tramiteMesa");
            SqlCmd.Append(" WHERE IdTramite=" + pIdtramite.ToString());
            SqlCmd.Append(" AND Estado IN (" + E_EstadoMesa.RevisionHold.ToString("d"));
            SqlCmd.Append(" )");
            SqlCmd.Append(" ORDER BY IdMesa");
            DataTable datos = BD.leeDatos(SqlCmd.ToString());
            foreach (DataRow reg in datos.Rows)
            {
                Lista.Add(arma(reg));
            }
            datos.Dispose();
            BD.cierraBD();
            return Lista;
        }

        public List<tramiteMesa> DaMesasTramitesRevisionCitaMedica(int pIdtramite)
        {
            List<tramiteMesa> Lista = new List<tramiteMesa>();
            bd BD = new bd();
            StringBuilder SqlCmd = new StringBuilder("SELECT * FROM vw_tramiteMesa");
            SqlCmd.Append(" WHERE IdTramite=" + pIdtramite.ToString());
            SqlCmd.Append(" AND Estado IN (" + E_EstadoMesa.RevisionCitaMedica.ToString("d"));
            SqlCmd.Append(" )");
            SqlCmd.Append(" ORDER BY IdMesa");
            DataTable datos = BD.leeDatos(SqlCmd.ToString());
            foreach (DataRow reg in datos.Rows)
            {
                Lista.Add(arma(reg));
            }
            datos.Dispose();
            BD.cierraBD();
            return Lista;
        }

        public List<tramiteMesa> DaMesasTramitesRevisonSupension(int pIdtramite)
        {
            List<tramiteMesa> Lista = new List<tramiteMesa>();
            bd BD = new bd();
            StringBuilder SqlCmd = new StringBuilder("SELECT * FROM vw_tramiteMesa");
            SqlCmd.Append(" WHERE IdTramite=" + pIdtramite.ToString());
            SqlCmd.Append(" AND Estado=" + E_EstadoMesa.RevisionSuspencion.ToString("d"));
            SqlCmd.Append(" ORDER BY IdMesa");
            DataTable datos = BD.leeDatos(SqlCmd.ToString());
            foreach (DataRow reg in datos.Rows)
            {
                Lista.Add(arma(reg));
            }
            datos.Dispose();
            BD.cierraBD();
            return Lista;
        }

        /// <summary>
        /// Obtiene las Mesas que pusieron el Trámite con el status PCI
        /// </summary>
        /// <param name="pIdtramite"></param>
        /// <returns></returns>
        public List<tramiteMesa> DaMesasTramitesRevisonPCI(int pIdtramite)
        {
            List<tramiteMesa> Lista = new List<tramiteMesa>();
            bd BD = new bd();
            StringBuilder SqlCmd = new StringBuilder("SELECT * FROM vw_tramiteMesa");
            SqlCmd.Append(" WHERE IdTramite=" + pIdtramite.ToString());
            SqlCmd.Append(" AND Estado=" + E_EstadoMesa.PCI.ToString("d"));
            SqlCmd.Append(" ORDER BY IdMesa");
            DataTable datos = BD.leeDatos(SqlCmd.ToString());
            foreach (DataRow reg in datos.Rows)
            {
                Lista.Add(arma(reg));
            }
            datos.Dispose();
            BD.cierraBD();
            return Lista;
        }

        /// <summary>
        /// Actualiza un tramite en una mesa a estado de en Revisión
        /// </summary>
        /// <param name="pIdTramite"></param>
        public void AtrapaMesasTramiteRevision(int pIdTramite)
        {
            bd BD = new bd();
            StringBuilder SqlCmd = new StringBuilder("Update tramiteMesa set FechaInicio=getdate(), Estado=" + E_EstadoMesa.RevPromotoria.ToString("d"));
            SqlCmd.Append(" WHERE IdTramite=" + pIdTramite.ToString());
            SqlCmd.Append(" AND Estado=" + E_EstadoMesa.Hold.ToString("d"));
            bool resultado = BD.ejecutaCmd(SqlCmd.ToString());
            BD.cierraBD();
        }

        /// <summary>
        /// Actualiza un tramite en una mesa a estado de en Revisión Cita Medica 
        /// </summary>
        /// <param name="pIdTramite"></param>
        public void AtrapaMesasTramiteRevisionCitaMedica(int pIdTramite)
        {
            bd BD = new bd();
            StringBuilder SqlCmd = new StringBuilder("Update tramiteMesa set FechaInicio=getdate(), Estado=" + E_EstadoMesa.RevisionCitaMedica.ToString("d"));
            SqlCmd.Append(" WHERE IdTramite=" + pIdTramite.ToString());
            SqlCmd.Append(" AND Estado=" + E_EstadoMesa.SuspensionCitaMedica.ToString("d"));
            bool resultado = BD.ejecutaCmd(SqlCmd.ToString());
            BD.cierraBD();
        }

        /// <summary>
        /// Actualiza un tramite en una mesa a estado Hold
        /// </summary>
        /// <param name="pIdTramite"></param>
        public void AtrapaMesasenHoldTramite(int pIdTramite){
            bd BD = new bd();
            StringBuilder SqlCmd = new StringBuilder("Update tramiteMesa set FechaInicio=getdate(), Estado=" + E_EstadoMesa.RevisionHold.ToString("d"));
            SqlCmd.Append(" WHERE IdTramite=" + pIdTramite.ToString());
            SqlCmd.Append(" AND Estado=" + E_EstadoMesa.Hold.ToString("d"));
            bool resultado = BD.ejecutaCmd(SqlCmd.ToString());
            BD.cierraBD();
        }

        /// <summary>
        /// Actualiza un tramite en una mesa a estado suspendido
        /// </summary>
        /// <param name="pIdTramite"></param>
        public void AtrapaMesasSuspension(int pIdTramite)
        {
            bd BD = new bd();
            StringBuilder SqlCmd = new StringBuilder("Update tramiteMesa set FechaInicio=getdate(), Estado=" + E_EstadoMesa.RevisionSuspencion.ToString("d"));
            SqlCmd.Append(" WHERE IdTramite=" + pIdTramite.ToString());
            SqlCmd.Append(" AND Estado=" + E_EstadoMesa.Suspendido.ToString("d"));
            bool resultado = BD.ejecutaCmd(SqlCmd.ToString());
            BD.cierraBD();
        }

        public List<tramiteMesa> ListaCartaObservaciones()
        {
            List<tramiteMesa> Lista = new List<tramiteMesa>();
            return Lista;
        }
        
        public DataTable daListaCancelados(int pIdUsuario)
        {
            bd BD = new bd();
            StringBuilder SqlCmd = new StringBuilder("SELECT [Folio].[FolioCompuesto],[tipoTramite].[Nombre],[tramite].[DatosHtml],[tramite].[Estado],[tramite].[FechaRegistro],[tramite].[Id] FROM [tramite],[Folio],[tipoTramite]");
            SqlCmd.Append(" WHERE [Folio].[IdTramite] = [tramite].[Id]");
            SqlCmd.Append(" AND [tipoTramite].[Id] = [tramite].[IdTipoTramite]");
            //SqlCmd.Append(" AND [tramite].[IdUsuario] = " + pIdUsuario.ToString());
            SqlCmd.Append(" AND ([tramite].[Estado] = " + E_EstadoTramite.Cancelado.ToString("d") + " OR [tramite].[Estado] = " + E_EstadoTramite.PromotoriaCancela.ToString("d") + ")");
            SqlCmd.Append(" ORDER BY [Folio].[FolioCompuesto]");
            DataTable datos = BD.leeDatos(SqlCmd.ToString());
            BD.cierraBD();
            return datos;
        }

        public DataTable daListaCitasMedicas(int pIdUsuario)
        {
            bd BD = new bd();
            string strSentenciaSQL = "SELECT Folio.FolioCompuesto, tipoTramite.Nombre, tramite.DatosHtml, tramite.Estado, (SELECT statusTramite.Nombre FROM statusTramite WHERE statusTramite.Id = tramite.Estado) AS EstadoNombre, tramite.FechaRegistro, tramite.Id FROM tramite INNER JOIN Folio ON tramite.Id = Folio.IdTramite INNER JOIN tipoTramite ON tramite.IdTipoTramite = tipoTramite.Id WHERE Folio.IdTramite = tramite.Id AND tipoTramite.Id = tramite.IdTipoTramite AND tramite.Id IN (SELECT tramiteMesa.IdTramite FROM tramiteMesa WHERE tramiteMesa.Estado IN (SELECT statusMesa.Id FROM statusMesa WHERE statusMesa.Nombre IN ('Cita Programada','Cita Reprogramada','En espera de resultados','Información para Citas Medicas','Confirmación Pendiente', 'Revisión con Prospecto'))) ORDER BY Folio.FolioCompuesto";
            DataTable datos = BD.leeDatos(strSentenciaSQL.ToString());
            BD.cierraBD();
            return datos;
        }

        public DataTable daListaAtrapados(int pIdUsuario)
        {
            bd BD = new bd();
            StringBuilder SqlCmd = new StringBuilder("SELECT [Folio].[FolioCompuesto],[vw_tramiteMesa].[Flujo],[vw_tramiteMesa].[TramiteNombre],[vw_tramiteMesa].[DatosHtml],[vw_tramiteMesa].[MesaNombre],[vw_tramiteMesa].[EstadoNombre],[vw_tramiteMesa].[FechaRegistro],[vw_tramiteMesa].[IdMesa],[vw_tramiteMesa].[IdTramite] FROM [vw_tramiteMesa], [Folio]");
            SqlCmd.Append(" WHERE [vw_tramiteMesa].[IdUsuario]=" + pIdUsuario.ToString());
            SqlCmd.Append(" AND [Folio].[IdTramite] = [vw_tramiteMesa].[IdTramite]");
            SqlCmd.Append(" AND [vw_tramiteMesa].[Estado]=" + E_EstadoMesa.Atrapado.ToString("d"));
            SqlCmd.Append(" ORDER BY [vw_tramiteMesa].[IdMesa], [vw_tramiteMesa].[IdTramite]");
            DataTable datos = BD.leeDatos(SqlCmd.ToString());
            BD.cierraBD();
            return datos;
        }

        public DataTable daListaEnEspera(int pIdUsuario)
        {
            bd BD = new bd();
            StringBuilder SqlCmd = new StringBuilder("SELECT [Folio].[FolioCompuesto],[vw_tramiteMesa].[Flujo],[vw_tramiteMesa].[TramiteNombre],[vw_tramiteMesa].[DatosHtml],[vw_tramiteMesa].[MesaNombre],[vw_tramiteMesa].[EstadoNombre],[vw_tramiteMesa].[FechaRegistro],[vw_tramiteMesa].[IdMesa],[vw_tramiteMesa].[IdTramite] FROM [vw_tramiteMesa], [Folio]");
            SqlCmd.Append(" WHERE [vw_tramiteMesa].[IdUsuario]=" + pIdUsuario.ToString());
            SqlCmd.Append(" AND [Folio].[IdTramite] = [vw_tramiteMesa].[IdTramite]");
            SqlCmd.Append(" AND [vw_tramiteMesa].[Estado]=" + E_EstadoMesa.Pausa.ToString("d"));
            SqlCmd.Append(" ORDER BY [vw_tramiteMesa].[IdMesa], [vw_tramiteMesa].[IdTramite]");
            DataTable datos = BD.leeDatos(SqlCmd.ToString());
            BD.cierraBD();
            return datos;
        }

        public DataTable daHistorialTramite(int IdTramite)
        {
            bd BD = new bd();
            // StringBuilder SqlCmd = new StringBuilder("SELECT [vw_bitacora_dos].[Id], IIF( ([vw_bitacora_dos].[Observacion] LIKE 'Cancelado por Supervisor%') AND ( [vw_bitacora_dos].[ObservacionPrivada] LIKE 'Cancelado por Supervisor%') , 'SUPERVISOR',[vw_bitacora_dos].[MesaNombre]) AS MesaNombre,[statusMesa].[Nombre],[vw_bitacora_dos].[Observacion],[vw_bitacora_dos].[ObservacionPrivada],[vw_bitacora_dos].[FechaInicio],[vw_bitacora_dos].[FechaTermino],");
            StringBuilder SqlCmd = new StringBuilder("SELECT [vw_bitacora_dos].[Id], CASE [vw_bitacora_dos].[IdMesa] WHEN -1 THEN IIF( ([vw_bitacora_dos].[Observacion] LIKE 'Cancelado por Supervisor%') AND ( [vw_bitacora_dos].[ObservacionPrivada] LIKE 'Cancelado por Supervisor%') , 'SUPERVISOR',[vw_bitacora_dos].[MesaNombre]) WHEN -2 THEN 'LABORATORIO' ELSE [vw_bitacora_dos].[MesaNombre] END AS MesaNombre, CASE [statusMesa].[Id] WHEN 26 THEN 'Regresó a Mesa' WHEN 25 THEN 'Respues de Mesa' ELSE [statusMesa].[Nombre] END AS [Nombre], [vw_bitacora_dos].[Observacion],[vw_bitacora_dos].[ObservacionPrivada],[vw_bitacora_dos].[FechaInicio],[vw_bitacora_dos].[FechaTermino],");
            SqlCmd.Append(" CONVERT(VARCHAR(50),FLOOR([vw_bitacora_dos].[Tiempo]/86400)) + '  ' +");
            SqlCmd.Append(" RIGHT('00' + Ltrim(Rtrim(CONVERT(VARCHAR(50),FLOOR(([vw_bitacora_dos].[Tiempo]/3600 )-FLOOR([vw_bitacora_dos].[Tiempo]/86400)*24)))),2) + ':' +");
            SqlCmd.Append(" RIGHT('00' + Ltrim(Rtrim(CONVERT(VARCHAR(50),FLOOR(([vw_bitacora_dos].[Tiempo]/60)-FLOOR([vw_bitacora_dos].[Tiempo]/3600)*60)))),2) + ':' +");
            SqlCmd.Append(" RIGHT('00' + Ltrim(Rtrim(CONVERT(VARCHAR(50),[vw_bitacora_dos].[Tiempo]-FLOOR([vw_bitacora_dos].[Tiempo]/60)*60))),2) AS Tiempo ");
            SqlCmd.Append(" , [vw_bitacora_dos].[IdUsuario], (SELECT usuarios.Nombre FROM usuarios WHERE usuarios.Id = vw_bitacora_dos.IdUsuario) AS usuario ");
            SqlCmd.Append(" FROM [vw_bitacora_dos],[statusMesa]");
            SqlCmd.Append(" WHERE [statusMesa].[Id] = [vw_bitacora_dos].[Estado] AND [IdTramite]=" + IdTramite);
            DataTable datos = BD.leeDatos(SqlCmd.ToString());
            BD.cierraBD();
            return datos;
        }

        /// <summary>
        /// Actualiza un tramite en una mesa a estado Registro (libera el tramite)
        /// </summary>
        /// <param name="pIdTramite"></param>
        /// <param name="pIdMesa"></param>
        /// <returns></returns>
        public bool liberaTramite(int pIdTramite, int pIdMesa)
        {
            bool resultado = false;
            StringBuilder SqlCmd = new StringBuilder("Update tramiteMesa SET");
            SqlCmd.Append(" FechaInicio=null");
            SqlCmd.Append(",IdUsuario=null");
            SqlCmd.Append(",Estado=" + wfiplib.E_EstadoMesa.Registro.ToString("d"));
            SqlCmd.Append(" WHERE IdTramite=" + pIdTramite.ToString());
            SqlCmd.Append(" AND IdMesa=" + pIdMesa.ToString());
            bd BD = new bd();
            resultado = BD.ejecutaCmd(SqlCmd.ToString());
            BD.cierraBD();
            return resultado;
        }

        /// <summary>
        /// Actualiza un tramite en una mesa si ha sido rechazado
        /// </summary>
        /// <param name="pId"></param>
        /// <param name="pIdRechazo"></param>
        public void registraIdRechazo(int pId, int pIdRechazo)
        {
            bd BD = new bd();
            BD.ejecutaCmd("Update tramiteMesa Set IdRechazo=" + pIdRechazo.ToString() + " Where Id=" + pId.ToString());
            BD.cierraBD();
        }

        #region ****************************** Mantenimiento *****************************************************

        public DataTable TramiteMesaBuscar(string folio)
        {
            BaseDeDatos b = new BaseDeDatos();

            b.ExecuteCommandQuery("SELECT " +
            "tramiteMesa.Id, tramiteMesa.IdUsuario, " +
            "(SELECT usuarios.Usuario FROM usuarios WHERE usuarios.Id = tramiteMesa.IdUsuario) AS usuario, " +
            "tramiteMesa.Estado, " +
            "(SELECT statusMesa.Nombre FROM statusMesa WHERE statusMesa.Id = tramiteMesa.Estado) AS statusTramite, " +
            "tramiteMesa.IdTramite, " +
            "(SELECT Folio.FolioCompuesto FROM Folio WHERE folio.IdTramite = tramiteMesa.IdTramite) AS FolioTramite, " +
            "tramiteMesa.IdMesa, " +
            "(SELECT mesa.Nombre FROM mesa WHERE mesa.Id = tramiteMesa.IdMesa) AS Mesa, " +
            "tramiteMesa.FechaRegistro, tramiteMesa.FechaInicio, tramiteMesa.FechaFin, tramiteMesa.ObservacionPublica, tramiteMesa.ObservacionPrivada, " +
            "tramiteMesa.IdRechazo, tramiteMesa.Procede, tramiteMesa.AnswerTo " +
            "FROM tramiteMesa " +
            "WHERE (SELECT Folio.FolioCompuesto FROM Folio WHERE folio.IdTramite = tramiteMesa.IdTramite) LIKE @folio ");
            b.AddParameter("@folio", "%" + folio + "%", SqlDbType.VarChar);
            return b.Select();
        }


        #endregion




    }

    public class tramiteMesa
    {
        private int mId = 0;
        public int Id {
            get { return mId; }
            set { mId = value; }
        }
        private int mIdTramite = 0;
        public int IdTramite {
            get { return mIdTramite; }
            set { mIdTramite = value; }
        }
        private int mIdMesa = 0;
        public int IdMesa {
            get { return mIdMesa; }
            set { mIdMesa = value; }
        }
        private DateTime mFechaRegistro = new DateTime(2000, 1, 1, 0, 0, 0);
        public DateTime FechaRegistro {
            get { return mFechaRegistro; }
            set { mFechaRegistro = value; }
        }
        private int mIdUsuario = 0;
        public int IdUsuario {
            get { return mIdUsuario; }
            set { mIdUsuario = value; }
        }
        private E_EstadoMesa mEstado = E_EstadoMesa.Registro;
        public E_EstadoMesa Estado {
            get { return mEstado; }
            set { mEstado = value; }
        }
        private string mEstadoNombre = "";
        public string EstadoNombre {
            get { return mEstadoNombre; }
            set { mEstadoNombre = value; }
        }
        private DateTime mFechaInicio = new DateTime(2000, 1, 1, 0, 0, 0);
        public DateTime FechaInicio {
            get { return mFechaInicio; }
            set { mFechaInicio = value; }
        }
        private DateTime mFechaFin = new DateTime(2000, 1, 1, 0, 0, 0);
        public DateTime FechaFin {
            get { return mFechaFin; }
            set { mFechaFin = value; }
        }
        private string mObservacionPublica = "";
        public string ObservacionPublica {
            get { return mObservacionPublica; }
            set { mObservacionPublica = value; }
        }
        private string mObservacionPrivada = "";
        public string ObservacionPrivada {
            get { return mObservacionPrivada; }
            set { mObservacionPrivada = value; }
        }
        private int mIdRechazo = 0;
        public int IdRechazo {
            get { return mIdRechazo; }
            set { mIdRechazo = value; }
        }
        private int mIdFlujo = 0;
        public int IdFlujo {
            get { return mIdFlujo; }
            set { mIdFlujo = value; }
        }
        private string mFlujo = "";
        public string Flujo {
            get { return mFlujo; }
            set { mFlujo = value; }
        }
        private E_TipoTramite mIdTipoTramite = E_TipoTramite.Ninguno;
        public E_TipoTramite IdTipoTramite {
            get { return mIdTipoTramite; }
            set { mIdTipoTramite = value; }
        }
        private string mTramiteNombre = "";
        public string TramiteNombre {
            get { return mTramiteNombre; }
            set { mTramiteNombre = value; }
        }
        private int mIdPromotoria = 0;
        public int IdPromotoria {
            get { return mIdPromotoria; }
            set { mIdPromotoria = value; }
        }
        private string mPromotoriaNombre = "";
        public string PromotoriaNombre {
            get { return mPromotoriaNombre; }
            set { mPromotoriaNombre = value; }
        }
        private string mPromotoriaClave = "";
        public string PromotoriaClave {
            get { return mPromotoriaClave; }
            set { mPromotoriaClave = value; }
        }
        private string mUsuarioNombre = "";
        public string UsuarioNombre {
            get { return mUsuarioNombre; }
            set { mUsuarioNombre = value; }
        }
        private E_TipoTramite meTipoTramite = E_TipoTramite.Ninguno;
        public E_TipoTramite eTipoTramite {
            get { return meTipoTramite; }
            set { meTipoTramite = value; }
        }
        private string mMesaNombre = "";
        public string MesaNombre {
            get { return mMesaNombre; }
            set { mMesaNombre = value; }
        }
        private int mMesaIdTipo = 0;
        public int MesaIdTipo {
            get { return mMesaIdTipo; }
            set { mMesaIdTipo = value; }
        }
        private string mMesaTipoNombre = "";
        public string MesaTipoNombre {
            get { return mMesaTipoNombre; }
            set { mMesaTipoNombre = value; }
        }
        private int mIdEtapa = 0;
        public int IdEtapa {
            get { return mIdEtapa; }
            set { mIdEtapa = value; }
        }
        private string mEtapa = "";
        public string Etapa {
            get { return mEtapa; }
            set { mEtapa = value; }
        }
        private int mIdMesaPadre = 0;
        public int IdMesaPadre {
            get { return mIdMesaPadre; }
            set { mIdMesaPadre = value; }
        }
        private string mMesaPadre = "";
        public string MesaPadre {
            get { return mMesaPadre; }
            set { mMesaPadre = value; }
        }

        private int mProcede = -1;
        public int Procede
        {
            get { return mProcede; }
            set { mProcede = value; }
        }

        private int mAnswerTo = -1;
        public int AnswerTo
        {
            get { return mAnswerTo; }
            set { mAnswerTo = value; }
        }
    }
}
