using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace wfiplib
{
    public class admMesa
    {
        public DataTable ListadoMesas(int IdFlujo)
        {
            DataTable mesa = new DataTable();
            bd BD = new bd();
            mesa = BD.leeDatos("SELECT Id,Nombre FROM mesa WHERE IdFlujo=" + IdFlujo + "  AND idMesaPadre=0");
            BD.cierraBD();
            return mesa;
        }

        public DataTable TipoTramite(int IdFlujo)
        {
            DataTable tipoTramite = new DataTable();
            bd BD = new bd();
            tipoTramite = BD.leeDatos("SELECT Id,Nombre FROM tipoTramite WHERE IdFlujo=" + IdFlujo + "  AND idEstado=1");
            BD.cierraBD();

            return tipoTramite; 
        }
        public DataTable BuscarTramites(string tramite, string rfc, string contratante, string asegurado, int IdUsuario)
        {
            DataTable dt = new DataTable();
            bd buscarTramites = new bd();
            string qBuscarTramites = " SELECT " +
                           "VTM.IdTramite," +
                           "F.FolioCompuesto," +
                           "(T3P.Nombre + ' ' + T3P.ApPaterno + ' ' + T3P.ApMaterno) AS Contratante," +
                           "(T3P.TitularNombre + ' ' + T3P.TitularApPat + ' ' + T3P.TitularApMat) AS Titular," +
                           "VTM.IdMesa," +
                           "VTM.MesaNombre," +
                           "VTM.UsuarioNombre," +
                           "CASE VTM.prioridad " +
                             "WHEN 2 THEN 'SUPERVISIÓN' " +
                             "WHEN 3 THEN 'GRANDES SUMAS' "+
                             "WHEN 4 THEN 'HOMBRES CLAVE' "+
                             "ELSE 'NORMAL'"+
                           "END AS prioridad "+
                           "FROM vw_tramiteMesa VTM, Folio F, TRAM00003P T3P " +
                           " WHERE VTM.IdTramite = F.IdTramite " +
                           " AND VTM.IdTramite = T3P.IdTramite " +
                           " AND VTM.IdFlujo IN (SELECT flujoId FROM usuariosFlujo WHERE usuarioId = " + IdUsuario + " AND activo = 1)" + 
                           " AND(F.FolioCompuesto LIKE " + tramite +
                           " OR T3P.RFC LIKE " + rfc +
                           " OR T3P.Nombre LIKE " + contratante +
                           ") ";
            dt=buscarTramites.leeDatos(qBuscarTramites);
            buscarTramites.cierraBD();
            return dt;
        }
        public DataTable TramitesEnOperacion(string tramite, string rfc, string contratante, string asegurado, int operacion, int IdUsuario)
        {
            string prioridad = string.Empty;
            string datos = string.Empty;

            //1:asignar, 2:priorizar, 3: cancelar
            //if (operacion == 2)
            //{
            //    prioridad = "AND prioridad<>3";
            //}
            DataTable dt = new DataTable();
            bd BD = new bd();
            if (operacion==2 || operacion == 3)
            {
                datos = "SELECT * FROM (" +
                         "SELECT DISTINCT " +
                            "VTM.IdTramite," +
                            "VTM.IdTipoTramite," +
                            "F.FolioCompuesto," +
                            "VTM.prioridad," +
                            "CASE VTM.prioridad " +
                                "WHEN 1 THEN 'SUPERVISOR' "+
                                "WHEN 2 THEN 'SISTEMA' "+
                                "WHEN 3 THEN 'GRANDES SUMAS' "+
                                "WHEN 4 THEN 'HOMBRES CLAVE' "+
                                "WHEN 5 THEN 'NORMAL' "+
                            "END AS prioridadDesc,"+
                            "(T3P.Nombre + ' ' + T3P.ApPaterno + ' ' + T3P.ApMaterno) AS Contratante," +
                            "(T3P.TitularNombre + ' ' + T3P.TitularApPat + ' ' + T3P.TitularApMat) AS Titular," +
                            "T3P.RFC " +
                            "FROM vw_tramiteMesa VTM, Folio F, TRAM00003P T3P " +
                            " WHERE (VTM.IdTramite = F.IdTramite AND VTM.IdPromotoria=F.IdPromotoria) " +
                            " AND VTM.IdTramite = T3P.IdTramite " +
                            " AND VTM.Estado NOT IN(2,7,9,10,17,30) " +
                            " AND VTM.IdFlujo IN (SELECT flujoId FROM usuariosFlujo WHERE usuarioId = " + IdUsuario + " AND activo = 1)" +
                         ") A " +
                         " WHERE FolioCompuesto LIKE " + tramite +
                         " AND prioridad <>2 "+
                         " AND RFC LIKE " + rfc +
                         " AND Contratante LIKE " + contratante +
                         " AND Titular LIKE " + asegurado;
            }
            else
            {
               datos= "SELECT * FROM ("+
                           "SELECT " +
                           "VT.Id AS IdTramite," +
                           "T3P.RFC,"+
                           "VT.IdTipoTramite," +
                           "F.FolioCompuesto," +
                           "(SELECT EstadoNombre FROM vw_tramiteMesa WHERE IdTramite= VT.Id AND Id=(SELECT MAX(Id) FROM vw_tramiteMesa WHERE IdTramite= VT.Id)) AS EstadoNombre," +
                           "(SELECT MesaNombre FROM vw_tramiteMesa WHERE IdTramite= VT.Id AND Id=(SELECT MAX(Id) FROM vw_tramiteMesa WHERE IdTramite= VT.Id)) AS Mesa," +
                           "(SELECT IdMesa FROM vw_tramiteMesa WHERE IdTramite= VT.Id AND Id=(SELECT MAX(Id) FROM vw_tramiteMesa WHERE IdTramite= VT.Id)) AS IdMesa,"+
                           "(SELECT UsuarioNombre FROM vw_tramiteMesa WHERE IdTramite= VT.Id AND Id=(SELECT MAX(Id) FROM vw_tramiteMesa WHERE IdTramite= VT.Id)) AS UsuarioNombre," +
                           "(T3P.Nombre + ' ' + T3P.ApPaterno + ' ' + T3P.ApMaterno) AS Contratante," +
                           "(T3P.TitularNombre + ' ' + T3P.TitularApPat + ' ' + T3P.TitularApMat) AS Titular " +
                           "FROM vw_tramite VT, Folio F, TRAM00003P T3P " +
                           " WHERE VT.Id = F.IdTramite " +
                           " AND VT.Id = T3P.IdTramite " +
                           " AND VT.Estado IN(0,1,9,10,11,14,16,19,20) " +
                           " AND VT.IdFlujo IN (SELECT flujoId FROM usuariosFlujo WHERE usuarioId = " + IdUsuario + " AND activo = 1)" +
                        ") A "+
                        " WHERE FolioCompuesto LIKE " + tramite +
                        " AND RFC LIKE " + rfc +
                        " AND Contratante LIKE " + contratante +
                        " AND Titular LIKE " + asegurado +
                        " " + prioridad;
            }

            dt = BD.leeDatos(datos);
            BD.cierraBD();
            return dt;
        }

        public bool AsignarTramites(string IdUsuario, string IdTramite, string IdUsuarioAsigna)
        {
            bool blnResultado = false;
            string strSentenciaSQL = "";
            DataTable dtAsignacionActiva = null;
            StringBuilder sentenciaSQL = new StringBuilder("");
            string[] usuarios = IdUsuario.Split(',');
            bd BD = new bd();

            strSentenciaSQL = "SELECT * FROM assignTramite WHERE assignTramite.IdTramite = " + IdTramite.ToString() + "; ";
            dtAsignacionActiva = BD.leeDatos(strSentenciaSQL);
            if (dtAsignacionActiva.Rows.Count > 0)
            {
                sentenciaSQL.Append(" DELETE FROM assignTramite WHERE assignTramite.IdTramite = " + IdTramite.ToString() + "; ");
            }
            
            sentenciaSQL.Append(" UPDATE tramite SET prioridad = " + wfiplib.E_PrioridadTramite.Supervisor.ToString("d") + " WHERE Id = " + IdTramite.ToString() + "; ");
            foreach (string usuario in usuarios)
            {
                sentenciaSQL.Append(" UPDATE tramiteMesa SET IdUsuario = " + usuario.Trim() + " WHERE IdTramite = " + IdTramite.ToString() + " AND tramiteMesa.IdMesa IN (SELECT usuariosMesa.IdMesa FROM usuariosMesa WHERE usuariosMesa.IdUsuario = " + usuario.Trim() + ") AND Estado IN (SELECT statusMesa.Id FROM statusMesa WHERE Nombre IN ('Registro', 'ReingresoHold','ReingresoApoyo','Pausa','ReingresoSuspencion','ReingresoPCI','Revisión con Prospecto','Confirmación Pendiente','En espera de resultados','Send To Mesa','Revisión Promotoría')); ");
                sentenciaSQL.Append(" UPDATE tramiteMesa SET Estado = (SELECT statusMesa.Id FROM statusMesa WHERE statusMesa.Nombre = 'Registro') WHERE IdTramite = " + IdTramite.ToString() + " AND tramiteMesa.IdMesa IN (SELECT usuariosMesa.IdMesa FROM usuariosMesa WHERE usuariosMesa.IdUsuario = " + usuario.Trim() + ") AND Estado IN (SELECT statusMesa.Id FROM statusMesa WHERE Nombre IN ('Pausa')); ");
                sentenciaSQL.Append(" INSERT INTO assignTramite (IdUserAssign, IdUserAssigned, IdMesa, IdTramite, Fecha, Activo) VALUES (" + IdUsuarioAsigna.Trim() + ", " + usuario.Trim() + ", " + -1 + ", " + IdTramite.Trim() + ", GETDATE(), 1); ");
            }
            blnResultado = BD.ejecutaCmd(sentenciaSQL.ToString());
            BD.cierraBD();
            return blnResultado;
        }
        public bool AsignacionTramites(string IdUsuario,string Id,string IdTramite)
        {
            bool resultado = false;
            bd BD = new bd();
            string qAsignar = "UPDATE tramiteMesa SET IdUsuario=" + IdUsuario + " WHERE Id=" + Id + " AND IdTramite=" + IdTramite + "; if ((select tramite.Estado from tramite where id = " + IdTramite + ") = 12 ) update tramite set Estado = 1 where id = " + IdTramite + ";";
                           
            resultado = BD.ejecutaCmd(qAsignar);
            BD.cierraBD();
            return resultado;
        }
        public DataTable ListaTramites(string idTramite)
        {
            DataTable dt = new DataTable();
            bd BD = new bd();
            string qLista = "SELECT " +
                           "VTM.Id," +
                           "VTM.IdTramite," +
                           "VTM.IdUsuario," +
                           "VTM.UsuarioNombre AS Usuario," +
                           "VTM.EstadoNombre AS statusTramite," +
                           "VTM.IdMesa," +
                           "VTM.MesaNombre AS Mesa " +
                           "FROM vw_tramiteMesa VTM WHERE idTramite=" + idTramite;
            dt = BD.leeDatos(qLista);
            BD.cierraBD();
            return dt;
        }
        public Boolean PriorizarTramites(string IdTramite, string prioridad)
        {
            bd BD = new bd();
            Boolean resultado=false;
            string qPrioriza = "UPDATE tramite SET prioridad=" + prioridad + " WHERE Id=" + IdTramite ;
            resultado = BD.ejecutaCmd(qPrioriza);
            BD.cierraBD();
            return resultado;
        }

        public Boolean CancelarTramites(string IdTramite, string IdCancelacion, wfiplib.credencial DataSession)
        {
            //bd BD = new bd();
            //Boolean resultado;
            //string qCancelacionTramiteMesa = "UPDATE tramiteMesa SET Estado=30 WHERE IdTramite =" + IdTramite +
            //                             " AND idMesa=" + IdMesa;
            //resultado = BD.ejecutaCmd(qCancelacionTramiteMesa);
            //string qCancelacionTramite = "UPDATE tramite SET Estado=7  WHERE Id =" + IdTramite;

            //resultado = BD.ejecutaCmd(qCancelacionTramite);

            //string qCancelacionMotivos = "INSERT INTO tramiteCancelacionMotivos (idTramite,idMotivoCancelacion) VALUES(" + IdTramite + "," + IdCancelacion + ")";
            //resultado = BD.ejecutaCmd(qCancelacionMotivos);
            //BD.cierraBD();
            //return resultado;

            Mensajes mensajes = new Mensajes();
            bool blnResultado = false;

            try
            {
                wfiplib.tramiteP oTramite = (new wfiplib.admTramite()).carga(Convert.ToInt32(IdTramite));
                if ((new wfiplib.admTramite()).CancelarTramite(oTramite, DataSession, E_EstadoTramite.Cancelado))
                {
                    wfiplib.admTramiteRechazo oAdmTramiteRechazo = new wfiplib.admTramiteRechazo();
                    wfiplib.tramiteRechazo oTramiteRechazo = new wfiplib.tramiteRechazo();
                    oTramiteRechazo.Id = oAdmTramiteRechazo.siguienteId();
                    oTramiteRechazo.IdTramiteMesa = -1;
                    oTramiteRechazo.IdUsuario = DataSession.Id;
                    oTramiteRechazo.ObservacionPublica = "Cancelación por Supervisor";
                    oTramiteRechazo.ObservacionPrivada = "Cancelación por Supervisor";
                    oTramiteRechazo.Estado = wfiplib.E_EstadoMesa.Cancelado;
                    oAdmTramiteRechazo.nuevo(oTramiteRechazo);
                    oAdmTramiteRechazo.nuevoMotivo(oTramiteRechazo.Id, Convert.ToInt32(IdCancelacion));
                    blnResultado = true;
                }
                else
                {
                    blnResultado = false;
                }
            }
            catch (Exception ex)
            {
                blnResultado = false;
                Console.WriteLine("Error:" + ex.Message);
            }

            return blnResultado;
        }

        public Boolean CancelarTramites(string IdTramite, string IdCancelacion, wfiplib.credencial DataSession, string observacion)
        {
            //bd BD = new bd();
            //Boolean resultado;
            //string qCancelacionTramiteMesa = "UPDATE tramiteMesa SET Estado=30 WHERE IdTramite =" + IdTramite +
            //                             " AND idMesa=" + IdMesa;
            //resultado = BD.ejecutaCmd(qCancelacionTramiteMesa);
            //string qCancelacionTramite = "UPDATE tramite SET Estado=7  WHERE Id =" + IdTramite;

            //resultado = BD.ejecutaCmd(qCancelacionTramite);

            //string qCancelacionMotivos = "INSERT INTO tramiteCancelacionMotivos (idTramite,idMotivoCancelacion) VALUES(" + IdTramite + "," + IdCancelacion + ")";
            //resultado = BD.ejecutaCmd(qCancelacionMotivos);
            //BD.cierraBD();
            //return resultado;

            Mensajes mensajes = new Mensajes();
            bool blnResultado = false;

            try
            {
                wfiplib.tramiteP oTramite = (new wfiplib.admTramite()).carga(Convert.ToInt32(IdTramite));
                if ((new wfiplib.admTramite()).CancelarTramite(oTramite, DataSession, E_EstadoTramite.Cancelado, observacion))
                {
                    wfiplib.admTramiteRechazo oAdmTramiteRechazo = new wfiplib.admTramiteRechazo();
                    wfiplib.tramiteRechazo oTramiteRechazo = new wfiplib.tramiteRechazo();
                    oTramiteRechazo.Id = oAdmTramiteRechazo.siguienteId();
                    oTramiteRechazo.IdTramiteMesa = -1;
                    oTramiteRechazo.IdUsuario = DataSession.Id;
                    oTramiteRechazo.ObservacionPublica = "Cancelación por Supervisor. " + observacion;
                    oTramiteRechazo.ObservacionPrivada = "Cancelación por Supervisor. " + observacion;
                    oTramiteRechazo.Estado = wfiplib.E_EstadoMesa.Cancelado;
                    oAdmTramiteRechazo.nuevo(oTramiteRechazo);
                    oAdmTramiteRechazo.nuevoMotivo(oTramiteRechazo.Id, Convert.ToInt32(IdCancelacion));
                    blnResultado = true;
                }
                else
                {
                    blnResultado = false;
                }
            }
            catch (Exception ex)
            {
                blnResultado = false;
                Console.WriteLine("Error:" + ex.Message);
            }

            return blnResultado;
        }

        public DataTable UsuariosDisponibles(string idMesa)
        {
            DataTable usuarios = new DataTable();
            bd BD = new bd();
            string qUsuarios = "SELECT U.id AS numUsuario,U.Nombre as usuario " +
                               "FROM Usuarios  U " +
                               "WHERE U.Id IN " +
                                    "(SELECT UM.IdUsuario FROM UsuariosMesa UM WHERE UM.IdMesa=" + idMesa +
                                      " AND U.Estado = 1 ) ORDER BY U.Nombre ";
            usuarios = BD.leeDatos(qUsuarios);
            BD.cierraBD();
            return usuarios;
        }
        public DataTable Mesas(string idMesa)
        {
            DataTable mesas = new DataTable();
            bd BD = new bd();
            string qMesas ="SELECT id AS IdMesa,Nombre AS Mesa FROM MESA " +
                           "WHERE IdEstado = 1 AND idFlujo =(SELECT idFlujo FROM mesa WHERE id=" + idMesa + ") ORDER BY id";
            mesas = BD.leeDatos(qMesas);
            BD.cierraBD();
            return mesas;
        }
        public DataTable getStatusMesas(int pIdTramite)
        {
            DataTable statusMesa = new DataTable();
            bd BD = new bd();
            string data = "SELECT Estado, (SELECT mesa.Nombre FROM mesa WHERE mesa.Id = tramiteMesa.IdMesa) AS Mesa FROM tramiteMesa WHERE IdTramite = " + pIdTramite.ToString();
            statusMesa = BD.leeDatos(data);
            BD.cierraBD();
            return statusMesa;
        }

        public DataTable MotivosRechazo(int IdMesa, int idFlujo)
        {
            DataTable motivosRechazo = new DataTable();
            bd BD = new bd();
            string qMotivos = "SELECT MR.id, MR.idTipoRechazo,TM.Nombre AS Tipo, MR.Nombre FROM cat_MotivosRechazo MR " +
                              "INNER JOIN TipoTramite TM " +
                              "ON MR.IdTipoTramite=TM.Id  AND MR.IdFlujo=TM.IdFlujo "+
                              "WHERE MR.Activo = 1 and MR.idFlujo =" + idFlujo +
                              " AND MR.IdTipoTramite IN (SELECT DISTINCT IdTipoTramite FROM vw_tramiteMesa WHERE Estado IN (1,7) AND IdMesa="+ IdMesa +")";


            motivosRechazo = BD.leeDatos(qMotivos);
            BD.cierraBD();
            return motivosRechazo;
        }
        public DataTable motivosCancelacion()
        {
            DataTable motivosCancelacion = new DataTable();
            bd BD = new bd();
            string qMotivos = "SELECT idMotivoCancelacion,MotivoCancelacion " +
                               "FROM cat_MotivosCancelacion";
            motivosCancelacion = BD.leeDatos(qMotivos);
            BD.cierraBD();
            return motivosCancelacion;
        }

        public bool nuevo(mesa pDatos)
        {
            bool resultado = false;
            StringBuilder SqlCmd = new StringBuilder("INSERT INTO mesa (");
            SqlCmd.Append("IdFlujo");
            SqlCmd.Append(",Nombre");
            SqlCmd.Append(",IdTipo");
            SqlCmd.Append(",IdEtapa");
            SqlCmd.Append(",ConApoyo");
            SqlCmd.Append(",ConCondicion");
            SqlCmd.Append(",IdEstado");
            SqlCmd.Append(",IdMesaPadre");
            SqlCmd.Append(",Apoyo");
            SqlCmd.Append(",IdUsuario");
            SqlCmd.Append(",FechaRegistro");
            SqlCmd.Append(")");
            SqlCmd.Append(" VALUES (");
            SqlCmd.Append(pDatos.IdFlujo.ToString());
            SqlCmd.Append(",'" + pDatos.Nombre + "'");
            SqlCmd.Append("," + pDatos.IdTipo.ToString("d"));
            SqlCmd.Append("," + pDatos.IdEtapa.ToString());
            SqlCmd.Append("," + pDatos.ConApoyo.ToString("d"));
            SqlCmd.Append("," + pDatos.ConCondicion.ToString("d"));
            SqlCmd.Append("," + pDatos.IdEstado.ToString());
            SqlCmd.Append("," + pDatos.IdMesaPadre.ToString());
            SqlCmd.Append("," + pDatos.Apoyo.ToString("d"));
            SqlCmd.Append("," + pDatos.IdUsuario.ToString());
            SqlCmd.Append(",GETDATE()");
            SqlCmd.Append(");");
            bd BD = new bd();
            resultado = BD.ejecutaCmd(SqlCmd.ToString());
            BD.cierraBD();
            return resultado;
        }

        public int UltimaEtapaTramite(int idFlujo)
        {
            int respuesta = -1;
            bd BD = new bd();
            DataTable datos = BD.leeDatos("SELECT MAX(mesa.IdEtapa) FROM mesa WHERE IdFlujo = " + idFlujo.ToString() + ";");
            if (datos.Rows.Count > 0)
            {
                respuesta = Convert.ToInt32(datos.Rows[0][0]);
            }
            datos.Dispose();
            BD.cierraBD();
            return respuesta;
        }

        public mesa carga(int pId)
        {
            mesa respuesta = new mesa();
            bd BD = new bd();
            DataTable datos = BD.leeDatos("SELECT * FROM vw_mesa WHERE Id=" + pId.ToString());
            if (datos.Rows.Count > 0)
            {
                respuesta = arma(datos.Rows[0]);
            }
            datos.Dispose();
            BD.cierraBD();
            return respuesta;
        }

        public List<mesa> Lista(int pIdFlujo)
        {
            List<mesa> respuesta = new List<mesa>();
            bd BD = new bd();
            DataTable datos = BD.leeDatos("SELECT * FROM vw_mesa WHERE IdFlujo=" + pIdFlujo + " ORDER BY IdEtapa,IdMesaPadre,Id");
            foreach (DataRow reg in datos.Rows) { respuesta.Add(arma(reg)); }
            datos.Dispose();
            BD.cierraBD();
            return respuesta;
        }

        public List<mesa> ListaCbo(int pIdFlujo)
        {
            List<mesa> respuesta = new List<mesa>();
            mesa oInicial = new mesa();
            oInicial.Id = 0;
            oInicial.Nombre = "SELECCIONAR";
            respuesta.Add(oInicial);

            bd BD = new bd();
            DataTable datos = BD.leeDatos("SELECT * FROM vw_mesa WHERE IdFlujo=" + pIdFlujo + " AND IdEtapa>0 ORDER BY IdEtapa,Id");
            foreach (DataRow reg in datos.Rows) { respuesta.Add(arma(reg)); }
            datos.Dispose();
            BD.cierraBD();
            return respuesta;
        }

        private mesa arma(DataRow pRegistro)
        {
            mesa respuesta = new mesa();
            if (!pRegistro.IsNull("Id")) respuesta.Id = Convert.ToInt32(pRegistro["Id"]);
            if (!pRegistro.IsNull("IdFlujo")) respuesta.IdFlujo = Convert.ToInt32(pRegistro["IdFlujo"]);
            if (!pRegistro.IsNull("Flujo")) respuesta.Flujo = Convert.ToString(pRegistro["Flujo"]);
            if (!pRegistro.IsNull("Nombre")) respuesta.Nombre = Convert.ToString(pRegistro["Nombre"]);
            if (!pRegistro.IsNull("IdTipo")) respuesta.IdTipo = Convert.ToInt32(pRegistro["IdTipo"]);
            if (!pRegistro.IsNull("Tipo")) respuesta.Tipo = Convert.ToString(pRegistro["Tipo"]);
            if (!pRegistro.IsNull("IdEtapa")) respuesta.IdEtapa = Convert.ToInt32(pRegistro["IdEtapa"]);
            if (!pRegistro.IsNull("Etapa")) respuesta.Etapa = Convert.ToString(pRegistro["Etapa"]);
            if (!pRegistro.IsNull("EtapaOrden")) respuesta.EtapaOrden = Convert.ToInt32(pRegistro["EtapaOrden"]);
            if (!pRegistro.IsNull("ConApoyo")) respuesta.ConApoyo = (E_Estado)(pRegistro["ConApoyo"]);
            if (!pRegistro.IsNull("sendToMesa")) respuesta.SendToMesa = Convert.ToBoolean(pRegistro["sendToMesa"]);
            if (!pRegistro.IsNull("ConCondicion")) respuesta.ConCondicion = (E_Estado)(pRegistro["ConCondicion"]);
            if (!pRegistro.IsNull("IdEstado")) respuesta.IdEstado = Convert.ToInt32(pRegistro["IdEstado"]);
            if (!pRegistro.IsNull("Estado")) respuesta.Estado = Convert.ToString(pRegistro["Estado"]);
            if (!pRegistro.IsNull("IdMesaPadre")) respuesta.IdMesaPadre = Convert.ToInt32(pRegistro["IdMesaPadre"]);
            if (!pRegistro.IsNull("MesaPadre")) respuesta.MesaPadre = Convert.ToString(pRegistro["MesaPadre"]);
            if (!pRegistro.IsNull("IdUsuario")) respuesta.IdUsuario = Convert.ToInt32(pRegistro["IdUsuario"]);
            if (!pRegistro.IsNull("Usuario")) respuesta.Usuario = Convert.ToString(pRegistro["Usuario"]);
            if (!pRegistro.IsNull("Apoyo")) respuesta.Apoyo = (E_Estado)(pRegistro["Apoyo"]); ;
            if (!pRegistro.IsNull("FechaRegistro")) respuesta.FechaRegistro = Convert.ToDateTime(pRegistro["FechaRegistro"]); ;
            return respuesta;
        }

        public bool Existe(int pIdFlujo, string pNombre)
        {
            bool resultado = false;
            bd BD = new bd();
            DataTable datos = BD.leeDatos("SELECT * FROM mesa Where IdFlujo=" + pIdFlujo + " AND Nombre='" + pNombre + "'");
            resultado = (datos.Rows.Count > 0);
            datos.Dispose();
            BD.cierraBD();
            return resultado;
        }

        public void modifica(mesa pMesa)
        {
            StringBuilder SqlCmd = new StringBuilder("UPDATE mesa SET");
            SqlCmd.Append(" Nombre='" + pMesa.Nombre + "'");
            SqlCmd.Append(",IdTipo=" + pMesa.IdTipo.ToString("d"));
            SqlCmd.Append(",IdEtapa=" + pMesa.IdEtapa.ToString());
            SqlCmd.Append(",ConApoyo=" + pMesa.ConApoyo.ToString("d"));
            SqlCmd.Append(",ConCondicion=" + pMesa.ConCondicion.ToString("d"));
            SqlCmd.Append(" WHERE Id=" + pMesa.Id);
            bd BD = new bd();
            BD.ejecutaCmd(SqlCmd.ToString());
            BD.cierraBD();
        }

        public void modificaNombreEstado(mesa pMesa)
        {
            StringBuilder SqlCmd = new StringBuilder("UPDATE mesa SET");
            SqlCmd.Append(" Nombre='" + pMesa.Nombre + "'");
            SqlCmd.Append(",IdEstado=" + pMesa.IdEstado.ToString());
            SqlCmd.Append(",IdTipo=" + pMesa.IdTipo.ToString());
            SqlCmd.Append(" WHERE Id=" + pMesa.Id);
            bd BD = new bd();
            BD.ejecutaCmd(SqlCmd.ToString());
            BD.cierraBD();
        }

        public string daNombre(int pId)
        {
            string resultado = string.Empty;
            String SqlCmd = "SELECT Nombre FROM mesa WHERE Id=" + pId.ToString();
            bd BD = new bd();
            DataTable datos = BD.leeDatos(SqlCmd);
            if (datos.Rows.Count > 0) { resultado = datos.Rows[0][0].ToString(); }
            datos.Dispose();
            BD.cierraBD();
            return resultado;
        }

        public mesa daPrimerMesa(int pIdFlujo)
        {
            mesa respuesta = new mesa();
            String SqlCmd = "SELECT top 1 * FROM vw_mesa WHERE IdFlujo=" + pIdFlujo.ToString() + " ORDER BY IdEtapa";
            bd BD = new bd();
            DataTable datos = BD.leeDatos(SqlCmd);
            if (datos.Rows.Count > 0) { respuesta = arma(datos.Rows[0]); }
            datos.Dispose();
            BD.cierraBD();
            return respuesta;
        }

        public DataTable MesasUsuarios_Conetados_Mesas(int usuarioId)
        {
            DataTable dt = new DataTable();
            bd data = new bd();
            //string qMesasUsuariosConetados = "EXEC Usuarios_Conetados_Mesas ";
            string qMesasUsuariosConetados = "EXEC Usuarios_Conetados_Mesas " + usuarioId.ToString();
            dt = data.leeDatos(qMesasUsuariosConetados);
            data.cierraBD();
            return dt;
        }

        public DataTable Mesa_Detalle_Selecionar_PorNombre(string NombreMesa, int usuarioId)
        {
            DataTable dt = new DataTable();
            bd data = new bd();
            string qMesaDetalle = "EXEC Mesa_Detalle_Selecionar_PorNombre '" + NombreMesa + "', " + usuarioId.ToString() + ";";
            dt = data.leeDatos(qMesaDetalle);
            data.cierraBD();
            return dt;
        }

        public DataTable Mesa_Detalle_Selecionar(int IdFlujo, int IdFluj2, string NombreMesa)
        {
            DataTable dt = new DataTable();
            bd data = new bd();
            string qMesaDetalleSelecionar = "EXEC Mesa_Detalle_Selecionar " + IdFlujo + ", " + IdFluj2 + ",'" + NombreMesa + "'";
            dt = data.leeDatos(qMesaDetalleSelecionar);
            data.cierraBD();
            return dt;
        }


        public DataTable Mesa_Productividad_Flujo(int IdFlujo, int IdFluj2, DateTime fechaDesde, DateTime fechaHasta)
        {
            DataTable dt = new DataTable();
            TimeSpan In = new TimeSpan(00, 00, 00);
            TimeSpan Fin = new TimeSpan(23, 59, 59);
            fechaDesde = fechaDesde.Date + In;
            fechaHasta = fechaHasta.Date + Fin;

            string formato = "yyyy-MM-dd HH:mm:ss";
            string fechaD = fechaDesde.ToString(formato);
            string fechaH = fechaHasta.ToString(formato);

            bd uEstatusTramite = new bd();
            string qProductividad = "" +
                "DECLARE @FECHAINI  DATETIME;" +
                "DECLARE @FECHAFIN  DATETIME;" +
                "SET @FECHAINI = TRY_CONVERT(DATETIME, '" + fechaD + "',121);" +
                "SET @FECHAFIN = TRY_CONVERT(DATETIME, '" + fechaH + "',121);" +
                "EXEC Pruductividad_Top3_IdFlujo " + IdFlujo + ", " + IdFluj2 + ",@FECHAINI, @FECHAFIN";
            dt = uEstatusTramite.leeDatos(qProductividad);
            uEstatusTramite.cierraBD();
            return dt;
            
        }

        public DataTable Mesa_Productividad_Flujo_Mesa(int IdFlujo, int IdFluj2, DateTime fechaDesde, DateTime fechaHasta, int OrdenMesa)
        {
            DataTable dt = new DataTable();
            TimeSpan In = new TimeSpan(00, 00, 00);
            TimeSpan Fin = new TimeSpan(23, 59, 59);
            fechaDesde = fechaDesde.Date + In;
            fechaHasta = fechaHasta.Date + Fin;

            string formato = "yyyy-MM-dd HH:mm:ss";
            string fechaD = fechaDesde.ToString(formato);
            string fechaH = fechaHasta.ToString(formato);

            bd uEstatusTramite = new bd();
            string qProductividad = "" +
                 "DECLARE @FECHAINI  DATETIME;" +
                "DECLARE @FECHAFIN  DATETIME;" +
                "SET @FECHAINI = TRY_CONVERT(DATETIME, '" + fechaD + "',121);" +
                "SET @FECHAFIN = TRY_CONVERT(DATETIME, '" + fechaH + "',121);" +
                "EXEC Pruductividad_Mesa_IdFlujo " + IdFlujo + ", " + IdFluj2 + ",@FECHAINI,  @FECHAFIN,'" + OrdenMesa + "'";
            dt = uEstatusTramite.leeDatos(qProductividad);
            uEstatusTramite.cierraBD();
            return dt;

        }

        public DataTable Pruductividad_Mesa_Grafica_IdFlujo(int IdFlujo, int IdFluj2, DateTime fechaDesde, DateTime fechaHasta, int OrdenMesa)
        {
            DataTable dt = new DataTable();
            TimeSpan In = new TimeSpan(00, 00, 00);
            TimeSpan Fin = new TimeSpan(23, 59, 59);
            fechaDesde = fechaDesde.Date + In;
            fechaHasta = fechaHasta.Date + Fin;

            string formato = "yyyy-MM-dd HH:mm:ss";
            string fechaD = fechaDesde.ToString(formato);
            string fechaH = fechaHasta.ToString(formato);

            bd uEstatusTramite = new bd();
            string qProductividad = "" +
                "DECLARE @FECHAINI  DATETIME;" +
                "DECLARE @FECHAFIN  DATETIME;" +
                "SET @FECHAINI = TRY_CONVERT(DATETIME, '" + fechaD + "',121);" +
                "SET @FECHAFIN = TRY_CONVERT(DATETIME, '" + fechaH + "',121);" +
                "EXEC Pruductividad_Mesa_Grafica_IdFlujo " + IdFlujo + ", " + IdFluj2 + ",@FECHAINI, @FECHAFIN,'" + OrdenMesa + "'";
            dt = uEstatusTramite.leeDatos(qProductividad);
            uEstatusTramite.cierraBD();
            return dt;

        }

        // Obtiene los resultados de las mesas de apoyo
        public List<mesa> getMesasApoyo(int pIdFlujo, int pIdTramite, int pIdMesaActual)
        {
            List<mesa> respuesta = new List<mesa>();
            bd BD = new bd();
            StringBuilder SqlCmd = new StringBuilder("SELECT * FROM tramiteMesa");
            SqlCmd.Append(" WHERE IdTramite = " + pIdTramite.ToString());
            SqlCmd.Append(" AND IdMesa = " + pIdMesaActual);
            SqlCmd.Append(" AND Estado IN (" + wfiplib.E_EstadoMesa.Procesable.ToString("d") + ", " + E_EstadoMesa.NoProcesable.ToString("d") + " ) ");
            DataTable datos = BD.leeDatos(SqlCmd.ToString());
            foreach (DataRow reg in datos.Rows)
            {
                respuesta.Add(arma(reg));
            }
            datos.Dispose();
            BD.cierraBD();
            return respuesta;
        }

        public int getStatusMesaSeleccion(int idTramite, int IdMesa)
        {
            int resultado = -1;
            bd BD = new bd();
            StringBuilder SqlCmd = new StringBuilder("SELECT Estado FROM tramiteMesa");
            SqlCmd.Append(" WHERE IdTramite = " + idTramite.ToString());
            SqlCmd.Append(" AND IdMesa = " + IdMesa);
            DataTable datos = BD.leeDatos(SqlCmd.ToString());
            foreach (DataRow reg in datos.Rows)
            {
                resultado = int.Parse(reg["Estado"].ToString());
            }
            datos.Dispose();
            BD.cierraBD();
            return resultado;
        }

        public List<mesa> daSiguienteMesa(int pIdFlujo, int pIdEtapaActual, int pIdTramite)
        {
            bool buscarMesa = true;
            bd BD = new bd();
            List<mesa> respuesta = new List<mesa>();

            StringBuilder VerificaTipoTramite = new StringBuilder("SELECT bitacora.Estado FROM bitacora WHERE IdMesa = 14 AND IdTramite = " + pIdTramite); // " AND Estado = 38");
            DataTable Validador = BD.leeDatos(VerificaTipoTramite.ToString());
            foreach (DataRow reg in Validador.Rows)
            {
                if (reg["Estado"].ToString() == "38")     // E_EstadoMesa.SuspensionCitaMedica
                {
                    buscarMesa = false;
                }
                else
                {
                    buscarMesa = true;
                }
            }
            Validador.Dispose();
            

            if (buscarMesa)
            {
                StringBuilder SqlCmd = new StringBuilder("SELECT * FROM vw_mesa");
                SqlCmd.Append(" WHERE IdFlujo=" + pIdFlujo.ToString());
                SqlCmd.Append(" AND EtapaOrden=" + (pIdEtapaActual + 1).ToString());
                SqlCmd.Append(" AND IdMesaPadre=0");
                SqlCmd.Append(" ORDER BY Id");
                DataTable datos = BD.leeDatos(SqlCmd.ToString());
                foreach (DataRow reg in datos.Rows)
                {
                    respuesta.Add(arma(reg));
                }
                datos.Dispose();
            }
            
            BD.cierraBD();
            return respuesta;
        }

        //Funciones para pintar la estructura del flujo
        public List<mesa> ListaMesasTrabajo(int pIdFlujo)
        {
            List<mesa> respuesta = new List<mesa>();
            bd BD = new bd();
            string SqlCmd = "SELECT * FROM vw_mesa WHERE IdFlujo=" + pIdFlujo;
            SqlCmd += " AND Apoyo=" + E_Estado.Inactivo.ToString("d");
            DataTable datos = BD.leeDatos(SqlCmd);
            foreach (DataRow reg in datos.Rows) { respuesta.Add(arma(reg)); }
            datos.Dispose();
            BD.cierraBD();
            return respuesta;
        }

        public List<mesa> ListaMesasRaizDelPaso(int pIdFlujo, int pPaso)
        {
            List<mesa> respuesta = new List<mesa>();
            bd BD = new bd();
            string SqlCmd = "SELECT * FROM vw_mesa WHERE IdFlujo=" + pIdFlujo.ToString() + " AND IdEtapa=" + pPaso.ToString() + " AND IdMesaPadre=0";
            DataTable datos = BD.leeDatos(SqlCmd);
            foreach (DataRow reg in datos.Rows) { respuesta.Add(arma(reg)); }
            datos.Dispose();
            BD.cierraBD();
            return respuesta;
        }

        public int getTieneIdMesaPlad(int pIdTramite)
        {
            int intResultado = -1;
            String SqlCmd = "SELECT IdMesa FROM tramitemesa WHERE IdTramite = " + pIdTramite.ToString() + " AND IdMesa IN (SELECT Id FROM mesa WHERE Nombre = 'REVISIÓN PLAD')";
            bd BD = new bd();
            DataTable datos = BD.leeDatos(SqlCmd);
            if (datos.Rows.Count > 0)
            {
                intResultado = Convert.ToInt32(datos.Rows[0][0]);
            }
            datos.Dispose();
            BD.cierraBD();
            return intResultado;
        }

        public DataTable getMesasEnviar(int pIdMesa, int pIdTramite)
        {
            DataTable dtRespuesta = null;
            bd BD = new bd();
            string SqlCmd = "SELECT * FROM vw_mesa WHERE Id IN (SELECT IdMesaDestino FROM sendToMesa WHERE IdMesaOrigen = " + pIdMesa + " AND Activo = 1) and id in (select idmesa from tramitemesa where IdTramite = " + pIdTramite.ToString() + ") ORDER BY Id DESC";
            dtRespuesta = BD.leeDatos(SqlCmd);
            BD.cierraBD();
            return dtRespuesta;
        }

        public List<mesa> ListaMesasEnviar(int pIdMesa, int pIdTramite)
        {
            List<mesa> respuesta = new List<mesa>();
            bd BD = new bd();
            string SqlCmd = "SELECT * FROM vw_mesa WHERE Id IN (SELECT IdMesaDestino FROM sendToMesa WHERE IdMesaOrigen = " + pIdMesa + " AND Activo = 1) and id in (select idmesa from tramitemesa where IdTramite = " + pIdTramite.ToString() + ") ORDER BY Id DESC";
            DataTable datos = BD.leeDatos(SqlCmd);
            foreach (DataRow reg in datos.Rows) { respuesta.Add(arma(reg)); }
            datos.Dispose();
            BD.cierraBD();
            return respuesta;
        }

        public List<mesa> ListaMesasHijas(int pId)
        {
            List<mesa> respuesta = new List<mesa>();
            bd BD = new bd();
            string SqlCmd = "SELECT * FROM vw_mesa WHERE IdMesaPadre=" + pId.ToString() + " AND IdEstado = 1 ORDER BY Id";
            DataTable datos = BD.leeDatos(SqlCmd);
            foreach (DataRow reg in datos.Rows) { respuesta.Add(arma(reg)); }
            datos.Dispose();
            BD.cierraBD();
            return respuesta;
        }

        public List<mesa> ListaMesasHijas2(int pId, wfiplib.E_TipoTramite IdTipoTramite)
        {
            List<mesa> respuesta = new List<mesa>();
            bd BD = new bd();
            string SqlCmd = "";
            if (IdTipoTramite == E_TipoTramite.indPriEmisionGMM)
                SqlCmd = "SELECT * FROM vw_mesa WHERE IdMesaPadre=" + pId.ToString() + " AND IdEstado = 1 AND NOT Nombre = 'CITAS MÉDICAS' ORDER BY Id";
            else
                SqlCmd = "SELECT * FROM vw_mesa WHERE IdMesaPadre=" + pId.ToString() + " AND IdEstado = 1 AND NOT Nombre = 'CITAS MÉDICAS' ORDER BY Id";


            DataTable datos = BD.leeDatos(SqlCmd);
            foreach (DataRow reg in datos.Rows) { respuesta.Add(arma(reg)); }
            datos.Dispose();
            BD.cierraBD();
            return respuesta;
        }

        public List<int> DaNivelesDeFlujo(int pIdFlujo)
        {
            List<int> respuesta = new List<int>();
            bd BD = new bd();
            string SqlCmd = "SELECT Distinct(IdEtapa) FROM mesa WHERE IdFlujo=" + pIdFlujo;
            DataTable datos = BD.leeDatos(SqlCmd);
            foreach (DataRow reg in datos.Rows) { respuesta.Add(Convert.ToInt32(reg["IdEtapa"])); }
            datos.Dispose();
            BD.cierraBD();
            return respuesta;
        }

        public void Actualiza(mesa pMesa)
        {
            StringBuilder SqlCmd = new StringBuilder("UPDATE mesa SET");
            SqlCmd.Append(" IdEtapa=" + pMesa.IdEtapa.ToString());
            SqlCmd.Append(",Tipo=" + pMesa.IdTipo.ToString());
            SqlCmd.Append(" WHERE Id=" + pMesa.Id);
            bd BD = new bd();
            BD.ejecutaCmd(SqlCmd.ToString());
            BD.cierraBD();
        }

        public void configura(int pIdEtapa, int pIdMesaPadre, string pConApoyo, int pId)
        {
            StringBuilder SqlCmd = new StringBuilder("UPDATE mesa SET");
            SqlCmd.Append(" IdEtapa=" + pIdEtapa.ToString());
            SqlCmd.Append(",IdMesaPadre=" + pIdMesaPadre.ToString());
            if (pIdMesaPadre > 0) SqlCmd.Append(", Apoyo=" + E_Estado.Activo.ToString("d"));
            else SqlCmd.Append(",Apoyo=" + E_Estado.Inactivo.ToString("d"));
            SqlCmd.Append(",ConApoyo=" + pConApoyo);
            SqlCmd.Append(" WHERE Id=" + pId.ToString());
            bd BD = new bd();
            BD.ejecutaCmd(SqlCmd.ToString());
            BD.cierraBD();
        }

        public void activa(string pId)
        {
            bd BD = new bd();
            BD.ejecutaCmd("UPDATE mesa SET IdEstado = " + E_Estado.Activo.ToString("d") + " WHERE Id=" + pId);
            BD.cierraBD();
        }

        public void desactiva(string pId)
        {
            bd BD = new bd();
            BD.ejecutaCmd("UPDATE mesa SET IdEstado = " + E_Estado.Inactivo.ToString("d") + " WHERE Id=" + pId);
            BD.cierraBD();
        }

        public DataTable Mesa_DropDownList()
        {
            BaseDeDatos b = new BaseDeDatos();
            b.ExecuteCommandQuery("SELECT Id, Nombre FROM mesa");
            return b.Select();
        }

    }

    public class mesa
    {
        private int mId = 0;
        public int Id { get { return mId; } set { mId = value; } }
        private int mIdFlujo = 0;
        public int IdFlujo { get { return mIdFlujo; } set { mIdFlujo = value; } }
        private string mFlujo = string.Empty;
        public string Flujo { get { return mFlujo; } set { mFlujo = value; } }
        private string mNombre = String.Empty;
        public string Nombre { get { return mNombre; } set { mNombre = value; } }
        private int mIdTipo = 0;
        public int IdTipo { get { return mIdTipo; } set { mIdTipo = value; } }
        private string mTipo = String.Empty;
        public string Tipo { get { return mTipo; } set { mTipo = value; } }
        public E_TipoMesa eTipo { get { return (E_TipoMesa)IdTipo; } }
        private int mIdEtapa = 0;
        public int IdEtapa { get { return mIdEtapa; } set { mIdEtapa = value; } }
        private string mEtapa = string.Empty;
        public string Etapa { get { return mEtapa; } set { mEtapa = value; } }
        private int mEtapaOrden = 0;
        public int EtapaOrden { get { return mEtapaOrden; } set { mEtapaOrden = value; } }
        private E_Estado mConApoyo = E_Estado.Inactivo;
        public E_Estado ConApoyo { get { return mConApoyo; } set { mConApoyo = value; } }
        private bool mSendToMesa;
        public bool SendToMesa { get { return mSendToMesa; } set { mSendToMesa = value; } }
        private E_Estado mConCondicion = E_Estado.Inactivo;
        public E_Estado ConCondicion { get { return mConCondicion; } set { mConCondicion = value; } }
        private int mIdEstado = 0;
        public int IdEstado { get { return mIdEstado; } set { mIdEstado = value; } }
        private string mEstado = string.Empty;
        public string Estado { get { return mEstado; } set { mEstado = value; } }
        public E_Estado eEstado { get { return (E_Estado)mIdEstado; } }
        private int mIdMesaPadre = 0;
        public int IdMesaPadre { get { return mIdMesaPadre; } set { mIdMesaPadre = value; } }
        private string mMesaPadre = string.Empty;
        public string MesaPadre { get { return mMesaPadre; } set { mMesaPadre = value; } }
        private E_Estado mApoyo = E_Estado.Inactivo;
        public E_Estado Apoyo { get { return mApoyo; } set { mApoyo = value; } }
        private int mIdUsuario = 0;
        public int IdUsuario { get { return mIdUsuario; } set { mIdUsuario = value; } }
        private string mUsuario = string.Empty;
        public string Usuario { get { return mUsuario; } set { mUsuario = value; } }
        private DateTime mFechaRegistro = new DateTime(2000, 1, 1, 0, 0, 0);
        public DateTime FechaRegistro { get { return mFechaRegistro; } set { mFechaRegistro = value; } }
    }

    public class csFlujoTemp
    {
        private List<FlujoTemp> mMesas = new List<FlujoTemp>();
        public List<FlujoTemp> Mesas { get { return mMesas; } set { mMesas = value; } }
    }

    [Serializable]
    public class FlujoTemp
    {
        private string mid = string.Empty;
        public string id { get { return mid; } set { mid = value; } }
        private string mname = String.Empty;
        public string name { get { return mname; } set { mname = value; } }
        private string mtipo = String.Empty;
        public string tipo
        {
            get
            {
                if (string.IsNullOrEmpty(mtipo)) mtipo = id.Substring(0, 1);
                return mtipo;
            }
        }
        private int mIdReal = 0;
        public int IdReal
        {
            get
            {
                if (mIdReal == 0) mIdReal = Convert.ToInt32(id.Substring(1));
                return mIdReal;
            }
        }
        private List<FlujoTemp> mchildren = new List<FlujoTemp>();
        public List<FlujoTemp> children { get { return mchildren; } set { mchildren = value; } }
    }
}
