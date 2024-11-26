using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Globalization;

namespace wfiplib
{
    public class admSeguridad
    {
        /// <summary>
        /// Obtine los tipos de trámites que puede atender el operador.
        /// </summary>
        /// <param name="IdPerfil">Id del Perfil que tiene el usuario operador.</param>
        /// <returns>Lista de los tipos de trámites.</returns>
        public List<TipoTramite> getAssignTiposTramites(int IdPerfil)
        {
            List<TipoTramite> respuesta = new List<TipoTramite>();
            bd BD = new bd();
            string SqlCmd = "SELECT * FROM vw_tipoTramite WHERE vw_tipoTramite.Id IN (SELECT SEC_MODULOS.ID_ITEM FROM SEC_MODULOS WHERE SEC_MODULOS.ID_MODULO IN (SELECT SEC_ASIGNPERMISSIONS.ID_MODULO FROM SEC_ASIGNPERMISSIONS WHERE SEC_ASIGNPERMISSIONS.ID_PERFIL = " + IdPerfil.ToString() + " AND SEC_ASIGNPERMISSIONS.PERMISO = 1 AND SEC_ASIGNPERMISSIONS.ACTIVO = 1) AND SEC_MODULOS.ACTIVO = 1 AND SEC_MODULOS.CATEGORIA = 'WORKFLOW - TIPOTRAMITE') AND vw_tipoTramite.IdEstado = 1; ";
            DataTable datos = BD.leeDatos(SqlCmd);
            foreach (DataRow reg in datos.Rows)
            {
                respuesta.Add(armaTipoTramite(reg));
            }
            datos.Dispose();
            BD.cierraBD();
            return respuesta;
        }

        /// <summary>
        /// Obtine las mesas que puede atender el operador.
        /// </summary>
        /// <param name="IdUsuario">Id del Usuario Operador.</param>
        /// <param name="IdFlujo">Id del Flujo sobre el que se realizaran las operaciones.</param>
        /// <returns>Lista de las mesas asignadas al operador dependiendo del flujo a operar.</returns>
        public List<mesa> getAssignMesas(int IdUsuario, int IdFlujo)
        {
            List<mesa> respuesta = new List<mesa>();
            bd BD = new bd();
            string SqlCmd = "SELECT * FROM vw_mesa WHERE vw_mesa.Id IN (SELECT IdMesa FROM usuariosMesa WHERE usuariosMesa.IdUsuario = " + IdUsuario + " AND usuariosMesa.IdFlujo = " + IdFlujo.ToString() + ") AND vw_mesa.IdEstado = 1 order by EtapaOrden, Id asc ;";
            DataTable datos = BD.leeDatos(SqlCmd);
            foreach (DataRow reg in datos.Rows)
            {
                respuesta.Add(armaMesa(reg));
            }
            datos.Dispose();
            BD.cierraBD();
            return respuesta;
        }

        /// <summary>
        /// Obtine las mesas que puede atender el operador.
        /// </summary>
        /// <param name="IdUsuario">Id del Usuario Operador.</param>
        /// <returns>Lista de las mesas asignadas al operador dependiendo del flujo a operar.</returns>
        public List<mesa> getAssignMesas(int IdUsuario)
        {
            List<mesa> respuesta = new List<mesa>();
            bd BD = new bd();
            string SqlCmd = "SELECT * FROM vw_mesa WHERE vw_mesa.Id IN (SELECT IdMesa FROM usuariosMesa WHERE usuariosMesa.IdUsuario = " + IdUsuario + " ) AND vw_mesa.IdEstado = 1 ;";
            DataTable datos = BD.leeDatos(SqlCmd);
            foreach (DataRow reg in datos.Rows)
            {
                respuesta.Add(armaMesa(reg));
            }
            datos.Dispose();
            BD.cierraBD();
            return respuesta;
        }

        /// <summary>
        /// Crea un nuevo perfil en la base de datos.
        /// </summary>
        /// <returns>Regresa el ID del perfil creado.</returns>
        public bool CreateProfile(int IdUsuarioOwner, int IdUsuario)
        {
            //BaseDeDatos dbWorkFlow = new BaseDeDatos();
            StringBuilder strSQL_Permissions = new StringBuilder("");
            DataTable dtServicios = null;
            DataTable dtTipoTram = null;
            DataTable dtMesas = null;
            bool blnResultado = false;
            string strClave = "";
            String strSQLTemp = "";
            int intIdValor = -1;
            
            try
            {
                strClave = DateTime.Now.ToString("yyyyMMddHHmmss|" + IdUsuarioOwner.ToString() + "|" + IdUsuario.ToString(), CultureInfo.InvariantCulture);
                strSQL_Permissions.Append("DELETE FROM usuariosMesa WHERE IdUsuario = " + IdUsuario.ToString() + "; ");
                strSQL_Permissions.Append("DELETE FROM SEC_ASIGNPERMISSIONS WHERE ID_PERFIL = (SELECT IdPerfil FROM usuarios WHERE Id = " + IdUsuario.ToString() + "); ");
                strSQL_Permissions.Append("DELETE FROM SEC_PERFILES WHERE ID_PERFIL = (SELECT IdPerfil FROM usuarios WHERE Id = " + IdUsuario.ToString() + "); ");
                strSQL_Permissions.Append("INSERT INTO SEC_PERFILES (ID_APLICACION, NOMBRE, DESCRIPCION, ACTIVO) VALUES ((SELECT ID_APLICACION FROM SEC_APLICACIONES WHERE NOMBRE = 'WFO METLIFE'), '" + strClave + "', 'CREACIÓN DEL PERFIL PARA WORK FLOW OPERACIONAL " + IdUsuarioOwner.ToString() + "|" + IdUsuario.ToString() + "', 1); ");

                // Recorremos los tipos de servicios seleccionados
                dtServicios = loadPermissionDB(IdUsuarioOwner, IdUsuario, "tmpPermisosServicios");
                if (dtServicios.Rows.Count > 0)
                {
                    for (int i = 0; i <= (dtServicios.Rows.Count - 1); i++)
                    {
                        intIdValor = Convert.ToInt32(dtServicios.Rows[i]["idNewServicio"]);
                        strSQLTemp = "INSERT INTO SEC_ASIGNPERMISSIONS (ID_APLICACION, ID_MODULO, ID_PERFIL, ID_PERMISO, PERMISO, ACTIVO) ";
                        strSQLTemp += "VALUES ((SELECT ID_APLICACION FROM SEC_APLICACIONES WHERE NOMBRE = 'WFO METLIFE'), (SELECT ID_MODULO FROM SEC_MODULOS WHERE TABLA = 'cat_TiposServicios' AND ID_ITEM = " + intIdValor.ToString() + ") , (SELECT ID_PERFIL FROM SEC_PERFILES WHERE NOMBRE = '" + strClave + "'), (SELECT ID_PERMISO FROM SEC_PERMISOS WHERE NOMBRE = 'EJECUTAR'), 1, 1); ";
                        strSQL_Permissions.Append(strSQLTemp);
                    }
                }

                // Recorremos los tipos de trámite seleccionados
                dtTipoTram = loadPermissionDB(IdUsuarioOwner, IdUsuario, "tmpPermisosTipoTramites");
                if (dtTipoTram.Rows.Count > 0)
                {
                    for (int i = 0; i <= (dtTipoTram.Rows.Count - 1); i++)
                    {
                        intIdValor = Convert.ToInt32(dtTipoTram.Rows[i]["idNewTipoTram"]);
                        strSQLTemp = "INSERT INTO SEC_ASIGNPERMISSIONS (ID_APLICACION, ID_MODULO, ID_PERFIL, ID_PERMISO, PERMISO, ACTIVO) ";
                        strSQLTemp += "VALUES ((SELECT ID_APLICACION FROM SEC_APLICACIONES WHERE NOMBRE = 'WFO METLIFE'), (SELECT ID_MODULO FROM SEC_MODULOS WHERE TABLA = 'tipoTramite' AND ID_ITEM = " + intIdValor.ToString() + "), (SELECT ID_PERFIL FROM SEC_PERFILES WHERE NOMBRE = '" + strClave + "'), (SELECT ID_PERMISO FROM SEC_PERMISOS WHERE NOMBRE = 'EJECUTAR'), 1, 1); ";
                        strSQL_Permissions.Append(strSQLTemp);
                    }
                }

                // Recorremos las mesas asignadas al usuario.
                dtMesas = loadPermissionDB("SELECT DISTINCT idNewMesa FROM tmpPermisosMesa WHERE idOwner = " + IdUsuarioOwner.ToString()+ " AND idNewUsuario = " + IdUsuario.ToString() + ";");
                if (dtMesas.Rows.Count > 0)
                {
                    for (int i = 0; i <= (dtMesas.Rows.Count - 1); i++)
                    {
                        intIdValor = Convert.ToInt32(dtMesas.Rows[i]["idNewMesa"]);
                        strSQLTemp = "INSERT INTO usuariosMesa (IdUsuario, IdFlujo, IdMesa, IdTipoTramite, Condicion, Activo) ";
                        strSQLTemp += "VALUES (" + IdUsuario.ToString() + ", (SELECT IdFlujo FROM mesa WHERE Id = " + intIdValor.ToString() + "), " + intIdValor.ToString() + ", 0, '', 1); ";

                        // TODO: ### Pendiente: Realizar correctamente esta función para que se pongan los permisos las tablas de permisos
                        //strSQLTemp = "INSERT INTO SEC_ASIGNPERMISSIONS (ID_APLICACION, ID_MODULO, ID_PERFIL, ID_PERMISO, PERMISO, ACTIVO) ";
                        //strSQLTemp += "VALUES ((SELECT ID_APLICACION FROM SEC_APLICACIONES WHERE NOMBRE = 'WFO METLIFE'), (SELECT ID_MODULO FROM SEC_MODULOS WHERE TABLA = 'tipoTramite' AND ID_ITEM = " + intIdValor.ToString() + " AND idOwner = " + IdUsuarioOwner.ToString() + " AND idNewUsuario = " + IdUsuario.ToString() + "), (SELECT ID_PERFIL FROM SEC_PERFILES WHERE NOMBRE = '" + strClave + "'), (SELECT ID_PERMISO FROM SEC_PERMISOS WHERE NOMBRE = 'EJECUTAR'), 1, 1); ";
                        strSQL_Permissions.Append(strSQLTemp);
                    }
                }

                strSQL_Permissions.Append("UPDATE usuarios SET IdPerfil = (SELECT ID_PERFIL FROM SEC_PERFILES WHERE NOMBRE = '" + strClave + "') WHERE Id = " + IdUsuario.ToString() + "; ");
                strSQL_Permissions.Append("SELECT * FROM SEC_ASIGNPERMISSIONS WHERE ID_PERFIL IN (SELECT SEC_PERFILES.ID_PERFIL FROM SEC_PERFILES WHERE NOMBRE = '" + strClave + "'); ");
                strSQL_Permissions.Append("SELECT * FROM SEC_PERFILES WHERE NOMBRE = '" + strClave + "';");

                bd BD = new bd();
                BD.ejecutaCmdScalar(strSQL_Permissions.ToString());
                BD.cierraBD();

                blnResultado = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error:" + ex.Message);
                //dbWorkFlow.RollBackTransaction();
                blnResultado = false;
            }

            return blnResultado;
        }

        /// <summary>
        /// Borramos los registramos de la información de las Mesas que se desean activar al Usuario seleccionado.
        /// </summary>
        /// <param name="IdUsuarioOwner">Id del usuario logueado en el sistema.</param>
        /// <param name="IdUsuario">Id del usuario que se le otorgan permisos.</param>
        /// <returns>Regresa verdadero si se eliminió correctamente la asignación de la mesa.</returns>
        public bool registraMesaKO(int IdUsuarioOwner, int IdUsuario)
        {
            BaseDeDatos dbUsers = new BaseDeDatos();
            bool blnResultado = false;
            string strSQL = "";

            try
            {
                strSQL += "DELETE FROM tmpPermisosMesa WHERE idOwner = @idOwner AND idNewUsuario = @idNewUsuario; ";
                dbUsers.ExecuteCommandQuery(strSQL);
                dbUsers.AddParameter("@idOwner", IdUsuarioOwner, SqlDbType.Int);
                dbUsers.AddParameter("@idNewUsuario", IdUsuario, SqlDbType.Int);
                dbUsers.ConnectionOpenToTransaction();
                dbUsers.BeginTransaction();
                dbUsers.ExecuteNonQueryToTransaction();
                dbUsers.CommitTransaction();
                blnResultado = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error:" + ex.Message);
                dbUsers.RollBackTransaction();
                blnResultado = false;
            }
            finally
            {
                dbUsers.CloseConnection();
            }

            return blnResultado;
        }

        /// <summary>
        /// Registramos la información de las mesas que se le asignarán al usuario
        /// </summary>
        /// <param name="IdUsuarioOwner">Id del usuario logueado en el sistema.</param>
        /// <param name="IdUsuario">Id del usuario que se le otorgan permisos.</param>
        /// <param name="IdServicio">Id del servicio que se activará.</param>
        /// <param name="IdTipoTramite">Id del tipo de trámite que se activará.</param>
        /// <param name="IdMesa">Id de la Mesa que se activará.</param>
        /// <returns>Regresa verdadero si se asignó correctamente la mesa.</returns>
        public bool registraMesa(int IdUsuarioOwner, int IdUsuario, int IdTipoTramite, int IdMesa, int IdTipoMovimineto)
        {
            BaseDeDatos dbUsers = new BaseDeDatos();
            const int intProcesado = 0;
            bool blnResultado = false;
            string strSQL = "";

            try
            {
                strSQL += "INSERT INTO tmpPermisosMesa(idOwner, idNewUsuario, idNewRol, idNewTipoTram, idNewTipoMov, idNewMesa, procesado) ";
                strSQL += "VALUES (@idOwner, @idNewUsuario, (SELECT usuarios.IdRol FROM usuarios WHERE usuarios.Id = @idNewUsuario), @idNewTipoTram, @idNewTipoMov, @idNewMesa, @procesado)";
                dbUsers.ExecuteCommandQuery(strSQL);
                dbUsers.AddParameter("@idOwner", IdUsuarioOwner, SqlDbType.Int);
                dbUsers.AddParameter("@idNewUsuario", IdUsuario, SqlDbType.Int);
                dbUsers.AddParameter("@idNewTipoTram", IdTipoTramite, SqlDbType.Int);
                dbUsers.AddParameter("@idNewTipoMov", IdTipoMovimineto, SqlDbType.Int);
                dbUsers.AddParameter("@idNewMesa", IdMesa, SqlDbType.Int);
                dbUsers.AddParameter("@procesado", intProcesado, SqlDbType.Bit);
                dbUsers.ConnectionOpenToTransaction();
                dbUsers.BeginTransaction();
                dbUsers.ExecuteNonQueryToTransaction();
                dbUsers.CommitTransaction();
                blnResultado = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error:" + ex.Message);
                dbUsers.RollBackTransaction();
                blnResultado = false;
            }
            finally
            {
                dbUsers.CloseConnection();
            }

            return blnResultado;
        }

        /// <summary>
        /// Borramos los registramos de la información de los Serivicios que se desean activar al Usuario seleccionado.
        /// </summary>
        /// <param name="IdUsuarioOwner">Id del usuario logueado en el sistema.</param>
        /// <param name="IdUsuario">Id del usuario que se le otorgan permisos.</param>
        /// <returns>Regresa verdadero si se eliminió correctamente la asignación de el servicio.</returns>
        public bool registraTipoTramiteKO(int IdUsuarioOwner, int IdUsuario)
        {
            BaseDeDatos dbUsers = new BaseDeDatos();
            bool blnResultado = false;
            string strSQL = "";

            try
            {
                strSQL += "DELETE FROM tmpPermisosTipoTramites WHERE idOwner = @idOwner AND idNewUsuario = @idNewUsuario; ";
                dbUsers.ExecuteCommandQuery(strSQL);
                dbUsers.AddParameter("@idOwner", IdUsuarioOwner, SqlDbType.Int);
                dbUsers.AddParameter("@idNewUsuario", IdUsuario, SqlDbType.Int);
                dbUsers.ConnectionOpenToTransaction();
                dbUsers.BeginTransaction();
                dbUsers.ExecuteNonQueryToTransaction();
                dbUsers.CommitTransaction();
                blnResultado = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error:" + ex.Message);
                dbUsers.RollBackTransaction();
                blnResultado = false;
            }
            finally
            {
                dbUsers.CloseConnection();
            }

            return blnResultado;
        }

        /// <summary>
        /// Registramos la información de los Serivicios que se desean activar al Usuario seleccionado.
        /// </summary>
        /// <param name="IdUsuarioOwner">Id del usuario logueado en el sistema.</param>
        /// <param name="IdUsuario">Id del usuario que se le otorgan permisos.</param>
        /// <param name="IdServicio">Id del servicio que se activará.</param>
        /// <param name="IdTipoTramite">Id del tipo de trámite que se activará.</param>
        /// <returns>Regresa verdadero si se asignó correctamente el servicio.</returns>
        public bool registraTipoTramite(int IdUsuarioOwner, int IdUsuario, int IdServicio, int IdTipoTramite)
        {
            BaseDeDatos dbUsers = new BaseDeDatos();
            const int intProcesado = 0;
            bool blnResultado = false;
            string strSQL = "";

            try
            {
                strSQL += "INSERT INTO tmpPermisosTipoTramites(idOwner, idNewUsuario, idNewServicio, idNewRol, idNewTipoTram, procesado) ";
                strSQL += "VALUES (@idOwner, @idNewUsuario, @idNewServicio, (SELECT usuarios.IdRol FROM usuarios WHERE usuarios.Id = @idNewUsuario), @idNewTipoTram, @procesado) ;";

                dbUsers.ExecuteCommandQuery(strSQL);
                dbUsers.AddParameter("@idOwner", IdUsuarioOwner, SqlDbType.Int);
                dbUsers.AddParameter("@idNewUsuario", IdUsuario, SqlDbType.Int);
                dbUsers.AddParameter("@idNewServicio", IdServicio, SqlDbType.Int);
                dbUsers.AddParameter("@idNewTipoTram", IdTipoTramite, SqlDbType.Int);
                dbUsers.AddParameter("@procesado", intProcesado, SqlDbType.Bit);
                dbUsers.ConnectionOpenToTransaction();
                dbUsers.BeginTransaction();
                dbUsers.ExecuteNonQueryToTransaction();
                dbUsers.CommitTransaction();
                blnResultado = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error:" + ex.Message);
                dbUsers.RollBackTransaction();
                blnResultado = false;
            }
            finally
            {
                dbUsers.CloseConnection();
            }

            return blnResultado;
        }

        /// <summary>
        /// Se realiza el regisgtro de que el tipo de trámite ya ha sido procesado
        /// </summary>
        /// <param name="IdUsuarioOwner">Id del usuario logueado en el sistema.</param>
        /// <param name="IdUsuario">Id del usuario que se le otorgan permisos.</param>
        /// <param name="IdTipoTramite">Id del Tipo de Trámite que se activará.</param>
        /// <returns>Regresa verdadero si se registró correctamente el tipo de trámite.</returns>
        public bool registraTipoTramiteProcesado(int IdUsuarioOwner, int IdUsuario, int IdTipoTramite)
        {
            BaseDeDatos dbUsers = new BaseDeDatos();
            const int intProcesado = 1;
            bool blnResultado = false;
            string strSQL = "";

            try
            {
                strSQL += "UPDATE tmpPermisosTipoTramites SET procesado = @procesado WHERE idOwner = @idOwner AND idNewUsuario = @idNewUsuario AND idNewRol = (SELECT usuarios.IdRol FROM usuarios WHERE usuarios.Id = @idNewUsuario) AND idNewTipoTram = @idNewTipoTram ;";
                dbUsers.ExecuteCommandQuery(strSQL);
                dbUsers.AddParameter("@procesado", intProcesado, SqlDbType.Bit);
                dbUsers.AddParameter("@idOwner", IdUsuarioOwner, SqlDbType.Int);
                dbUsers.AddParameter("@idNewUsuario", IdUsuario, SqlDbType.Int);
                dbUsers.AddParameter("@idNewTipoTram", IdTipoTramite, SqlDbType.Int);
                dbUsers.ConnectionOpenToTransaction();
                dbUsers.BeginTransaction();
                dbUsers.ExecuteNonQueryToTransaction();
                dbUsers.CommitTransaction();
                blnResultado = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error:" + ex.Message);
                dbUsers.RollBackTransaction();
                blnResultado = false;
            }
            finally
            {
                dbUsers.CloseConnection();
            }

            return blnResultado;
        }

        /// <summary>
        /// Borramos los registramos de la información de los Serivicios que se desean activar al Usuario seleccionado.
        /// </summary>
        /// <param name="IdUsuarioOwner">Id del usuario logueado en el sistema.</param>
        /// <param name="IdUsuario">Id del usuario que se le otorgan permisos.</param>
        /// <returns>Regresa verdadero si se eliminió correctamente la asignación de el servicio.</returns>
        public bool registrarServiciosKO(int IdUsuarioOwner, int IdUsuario)
        {
            BaseDeDatos dbUsers = new BaseDeDatos();
            bool blnResultado = false;
            string strSQL = "";

            try
            {
                strSQL += "DELETE FROM tmpPermisosServicios WHERE idOwner = @idOwner AND idNewUsuario = @idNewUsuario; ";
                dbUsers.ExecuteCommandQuery(strSQL);
                dbUsers.AddParameter("@idOwner", IdUsuarioOwner, SqlDbType.Int);
                dbUsers.AddParameter("@idNewUsuario", IdUsuario, SqlDbType.Int);
                dbUsers.ConnectionOpenToTransaction();
                dbUsers.BeginTransaction();
                dbUsers.ExecuteNonQueryToTransaction();
                dbUsers.CommitTransaction();
                blnResultado = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error:" + ex.Message);
                dbUsers.RollBackTransaction();
                blnResultado = false;
            }
            finally
            {
                dbUsers.CloseConnection();
            }

            return blnResultado;
        }

        /// <summary>
        /// Registramos la información de los Serivicios que se desean activar al Usuario seleccionado.
        /// </summary>
        /// <param name="IdUsuarioOwner">Id del usuario logueado en el sistema.</param>
        /// <param name="IdUsuario">Id del usuario que se le otorgan permisos.</param>
        /// <param name="IdServicio">Id del servicio que se activará.</param>
        /// <returns>Regresa verdadero si se asignó correctamente el servicio.</returns>
        public bool registraServicios(int IdUsuarioOwner, int IdUsuario, int IdServicio)
        {
            BaseDeDatos dbUsers = new BaseDeDatos();
            const int intProcesado = 0;
            bool blnResultado = false;
            string strSQL = "";

            try
            {
                strSQL += "INSERT INTO  tmpPermisosServicios (idOwner, idNewUsuario, idNewServicio, idNewRol, procesado) ";
                strSQL += "VALUES (@idOwner, @idNewUsuario, @idNewServicio, (SELECT usuarios.IdRol FROM usuarios WHERE usuarios.Id = @idNewUsuario), @procesado); ";
                dbUsers.ExecuteCommandQuery(strSQL);
                dbUsers.AddParameter("@idOwner", IdUsuarioOwner, SqlDbType.Int);
                dbUsers.AddParameter("@idNewUsuario", IdUsuario, SqlDbType.Int);
                dbUsers.AddParameter("@idNewServicio", IdServicio, SqlDbType.Int);
                dbUsers.AddParameter("@procesado", intProcesado, SqlDbType.Bit);
                dbUsers.ConnectionOpenToTransaction();
                dbUsers.BeginTransaction();
                dbUsers.ExecuteNonQueryToTransaction();
                dbUsers.CommitTransaction();
                blnResultado = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error:" + ex.Message);
                dbUsers.RollBackTransaction();
                blnResultado = false;
            }
            finally
            {
                dbUsers.CloseConnection();
            }
          
            return blnResultado;
        }

        /// <summary>
        /// Se realiza el regisgtro de que el servicio ya ha sido procesado
        /// </summary>
        /// <param name="IdUsuarioOwner">Id del usuario logueado en el sistema.</param>
        /// <param name="IdUsuario">Id del usuario que se le otorgan permisos.</param>
        /// <param name="IdServicio">Id del servicio que se activará.</param>
        /// <returns>Regresa verdadero si se registró correctamente el servicio.</returns>
        public bool registraServiciosProcesado(int IdUsuarioOwner, int IdUsuario, int IdServicio)
        {
            BaseDeDatos dbUsers = new BaseDeDatos();
            const int intProcesado = 1;
            bool blnResultado = false;
            string strSQL = "";

            try
            {
                strSQL += "UPDATE tmpPermisosServicios SET procesado = @procesado WHERE idOwner = @idOwner AND idNewUsuario = @idNewUsuario AND idNewRol = (SELECT usuarios.IdRol FROM usuarios WHERE usuarios.Id = @idNewUsuario) AND idNewServicio = @idNewServicio ;";
                dbUsers.ExecuteCommandQuery(strSQL);
                dbUsers.AddParameter("@procesado", intProcesado, SqlDbType.Bit);
                dbUsers.AddParameter("@idOwner", IdUsuarioOwner, SqlDbType.Int);
                dbUsers.AddParameter("@idNewUsuario", IdUsuario, SqlDbType.Int);
                dbUsers.AddParameter("@idNewServicio", IdServicio, SqlDbType.Int);
                dbUsers.ConnectionOpenToTransaction();
                dbUsers.BeginTransaction();
                dbUsers.ExecuteNonQueryToTransaction();
                dbUsers.CommitTransaction();
                blnResultado = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error:" + ex.Message);
                dbUsers.RollBackTransaction();
                blnResultado = false;
            }
            finally
            {
                dbUsers.CloseConnection();
            }

            return blnResultado;
        }

        /// <summary>
        /// Obtine los tipos de servicios existentes
        /// </summary>
        /// <returns>Lista de los servicios</returns>
        public List<TipoServicio> getTipoServicios()
        {
            List<TipoServicio> respuesta = new List<TipoServicio>();
            bd BD = new bd();
            string SqlCmd = "SELECT * FROM cat_TiposServicios WHERE Activo = 1;";
            DataTable datos = BD.leeDatos(SqlCmd);
            foreach (DataRow reg in datos.Rows)
            {
                respuesta.Add(armaTipoServicio(reg));
            }
            datos.Dispose();
            BD.cierraBD();
            return respuesta;
        }

        /// <summary>
        /// Obtine las mesas
        /// </summary>
        /// <returns>Lista de las Mesas</returns>
        public List<mesa> getMesas(int IdTipoTramite)
        {
            List<mesa> respuesta = new List<mesa>();
            bd BD = new bd();
            string SqlCmd = "SELECT * FROM vw_mesa WHERE vw_mesa.IdEstado = 1 AND vw_mesa.IdFlujo = (SELECT tipoTramite.IdFlujo FROM tipoTramite WHERE tipoTramite.Id = " + IdTipoTramite.ToString() + ");";
            DataTable datos = BD.leeDatos(SqlCmd);
            foreach (DataRow reg in datos.Rows)
            {
                respuesta.Add(armaMesa(reg));
            }
            datos.Dispose();
            BD.cierraBD();
            return respuesta;
        }

        /// <summary>
        /// Obtine los movimientos de los Tipos de Trámite
        /// </summary>
        /// <returns>Lista de los Movimientos de los Tipos de Servicios</returns>
        public List<TipoServicio> getTipoTramiteMov()
        {
            List<TipoServicio> respuesta = new List<TipoServicio>();
            bd BD = new bd();
            string SqlCmd = "SELECT * FROM cat_TipoTramitesMov WHERE Activo = 1;";
            DataTable datos = BD.leeDatos(SqlCmd);
            foreach (DataRow reg in datos.Rows)
            {
                respuesta.Add(armaTipoTramiteMov(reg));
            }
            datos.Dispose();
            BD.cierraBD();
            return respuesta;
        }

        /// <summary>
        /// Obtine los tipos de trámites existentes
        /// </summary>
        /// <param name="IdServicio">Indica el Servicio que se consultará.</param>
        /// <returns>Lista de los trámites</returns>
        public List<TipoTramite> getTipoTramites(int IdServicio)
        {
            List<TipoTramite> respuesta = new List<TipoTramite>();
            bd BD = new bd();
            string SqlCmd = "SELECT * FROM vw_tipoTramite WHERE Id IN (SELECT Id_TipoTramite FROM AsignarTipoTramServicios WHERE Id_Servicio = " + IdServicio.ToString() +  ") ORDER BY Nombre;";
            DataTable datos = BD.leeDatos(SqlCmd);
            foreach (DataRow reg in datos.Rows)
            {
                respuesta.Add(armaTipoTramite(reg));
            }
            datos.Dispose();
            BD.cierraBD();
            return respuesta;
        }

        /// <summary>
        /// Seguridad. Obtiene el Servicio que se asignará al usuario para los permisos
        /// </summary>
        /// <returns>[int] Regresa el Id del Servicio</returns>
        public int getIdServicio(int IdUSuarioOwner, int IdUsuario)
        {
            bd BD = new bd();
            int intResultado = -1;
            StringBuilder SqlCmd = new StringBuilder("SELECT TOP 1 idOwner, idNewUsuario, idNewServicio, idNewRol, procesado FROM tmpPermisosServicios WHERE idOwner = " + IdUSuarioOwner.ToString() + " AND idNewUsuario = " + IdUsuario.ToString() + " AND procesado = 0 ORDER BY idOwner, idNewUsuario, idNewServicio;");
            DataTable datos = BD.leeDatos(SqlCmd.ToString());
            if (datos.Rows.Count > 0)
            {
                intResultado = Convert.ToInt32(datos.Rows[0]["idNewServicio"]);
            }
            BD.cierraBD();
            return intResultado;
        }

        /// <summary>
        /// Seguridad. Obtiene el Tipo de Trámite que se asignará al usuario para los permisos
        /// </summary>
        /// <returns>[int] Regresa el Id del Tipo de Trámite</returns>
        public int getIdTipoTramite(int IdUSuarioOwner, int IdUsuario)
        {
            bd BD = new bd();
            int intResultado = -1;
            StringBuilder SqlCmd = new StringBuilder("SELECT TOP 1 idOwner, idNewUsuario, idNewServicio, idNewRol, idNewTipoTram, procesado FROM tmpPermisosTipoTramites WHERE idOwner = " + IdUSuarioOwner.ToString() + " AND idNewUsuario = " + IdUsuario.ToString() + " AND procesado = 0 ORDER BY idOwner, idNewUsuario, idNewServicio, idNewTipoTram;");
            DataTable datos = BD.leeDatos(SqlCmd.ToString());
            if (datos.Rows.Count > 0)
            {
                intResultado = Convert.ToInt32(datos.Rows[0]["idNewTipoTram"]);
            }
            BD.cierraBD();
            return intResultado;
        }

        /// <summary>
        /// Seguridad. Listado de todos los Perfiles
        /// </summary>
        /// <returns>[DataTable] Listado de Perfiles</returns>
        public DataTable cargaRoles()
        {
            bd BD = new bd();
            StringBuilder SqlCmd = new StringBuilder("SELECT -1 AS ID_ROL, '[SELECCIONAR]' AS NOMBRE UNION ALL SELECT ID_ROL, NOMBRE FROM SEC_ROLES WHERE ACTIVO = 1;");
            DataTable datos = BD.leeDatos(SqlCmd.ToString());
            BD.cierraBD();
            return datos;
        }

        /// <summary>
        /// Función para verificar si el Rol del Usaurio pertenece a la categoría indicada.
        /// </summary>
        /// <param name="RolName">Nombre del Rol que se desea Verificar.</param>
        /// <param name="IdUserRol">Id del Rol Asignado al Usuario</param>
        /// <returns>Regresa Verdadero si es concordante la informaicón.</returns>
        public Boolean VerificaRol(string RolName, int IdUserRol)
        {
            Boolean blnRespuesta = false;
            int intRol = -1;
            bd BD = new bd();
            string SqlCmd = "SELECT ID_ROL FROM SEC_ROLES WHERE NOMBRE = '" + RolName + "';";
            DataTable datos = BD.leeDatos(SqlCmd);
            if (datos.Rows.Count > 0)
            {
                 intRol = Convert.ToInt32(datos.Rows[0][0]);
                if (intRol == IdUserRol)
                    blnRespuesta = true;
            }
            datos.Dispose();
            BD.cierraBD();
            return blnRespuesta;
        }

        /// <summary>
        /// Ejecuta una sentencia SQL para obtener un valor determinado.
        /// </summary>
        /// <param name="SentenciaSQL">Sentencia SQL a ejecutar.</param>
        /// <returns>Regresa el valor regresado por la base de datos.</returns>
        public string getData(string SentenciaSQL)
        {
            bd BD = new bd();
            string strResultado = "(NO DATA)";
            StringBuilder SqlCmd = new StringBuilder(SentenciaSQL);
            DataTable datos = BD.leeDatos(SqlCmd.ToString());
            if (datos.Rows.Count > 0)
            {
                strResultado = Convert.ToString(datos.Rows[0][0]);
            }
            BD.cierraBD();
            return strResultado;
        }

        /// <summary>
        /// Arma la estructura del Tipo de Servicios
        /// </summary>
        /// <param name="pRegistro">Información proveniente de la base de datos</param>
        /// <returns>Objeto con la información del Tipo de Servicios</returns>
        private TipoServicio armaTipoServicio(DataRow pRegistro)
        {
            TipoServicio Resultado = new TipoServicio();
            if (!pRegistro.IsNull("Id")) Resultado.Id = Convert.ToInt32(pRegistro["Id"]);
            if (!pRegistro.IsNull("Nombre")) Resultado.Nombre = Convert.ToString(pRegistro["Nombre"]);
            if (!pRegistro.IsNull("Descripcion")) Resultado.Descripcion = Convert.ToString(pRegistro["Descripcion"]);
            if (!pRegistro.IsNull("Activo")) Resultado.Activo = Convert.ToBoolean(pRegistro["Activo"]);
            return Resultado;
        }

        /// <summary>
        /// Arma la estructura de los Movimientos del Tipo de Trámite
        /// </summary>
        /// <param name="pRegistro">Información proveniente de la base de datos</param>
        /// <returns>Objeto con la información de los Movimientos de Tipos de Trámites</returns>
        private TipoServicio armaTipoTramiteMov(DataRow pRegistro)
        {
            TipoServicio Resultado = new TipoServicio();
            if (!pRegistro.IsNull("Id")) Resultado.Id = Convert.ToInt32(pRegistro["Id"]);
            if (!pRegistro.IsNull("Nombre")) Resultado.Nombre = Convert.ToString(pRegistro["Nombre"]);
            if (!pRegistro.IsNull("Descripcion")) Resultado.Descripcion = Convert.ToString(pRegistro["Descripcion"]);
            if (!pRegistro.IsNull("Activo")) Resultado.Activo = Convert.ToBoolean(pRegistro["Activo"]);
            return Resultado;
        }

        /// <summary>
        /// Arma la estructura del Tipo de Trámite
        /// </summary>
        /// <param name="pRegistro">Información proveniente de la base de datos</param>
        /// <returns>Objeto con la información del Tipo de Trámite</returns>
        private TipoTramite armaTipoTramite(DataRow pRegistro)
        {
            TipoTramite Resultado = new TipoTramite();
            if (!pRegistro.IsNull("Id")) Resultado.Id = Convert.ToInt32(pRegistro["Id"]);
            if (!pRegistro.IsNull("IdFlujo")) Resultado.IdFlujo = Convert.ToInt32(pRegistro["IdFlujo"]);
            if (!pRegistro.IsNull("Flujo")) Resultado.Flujo = Convert.ToString(pRegistro["Flujo"]);
            if (!pRegistro.IsNull("Nombre")) Resultado.Nombre = Convert.ToString(pRegistro["Nombre"]);
            if (!pRegistro.IsNull("IdEstado")) Resultado.IdEstado = Convert.ToInt32(pRegistro["IdEstado"]);
            if (!pRegistro.IsNull("Estado")) Resultado.Estado = Convert.ToString(pRegistro["Estado"]);
            if (!pRegistro.IsNull("IdUsuario")) Resultado.IdUsuario = Convert.ToInt32(pRegistro["IdUsuario"]);
            if (!pRegistro.IsNull("Usuario")) Resultado.Usuario = Convert.ToString(pRegistro["Usuario"]);
            if (!pRegistro.IsNull("FechaRegistro")) Resultado.FechaRegistro = Convert.ToDateTime(pRegistro["FechaRegistro"]);
            if (!pRegistro.IsNull("Tabla")) Resultado.Tabla = Convert.ToString(pRegistro["Tabla"]);
            return Resultado;
        }

        /// <summary>
        /// Arma la estructura de la Mesa
        /// </summary>
        /// <param name="pRegistro">Información proveniente de la base de datos</param>
        /// <returns>Objeto con información de la Mesa</returns>
        private mesa armaMesa(DataRow pRegistro)
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
            if (!pRegistro.IsNull("Apoyo")) respuesta.Apoyo = (E_Estado)(pRegistro["Apoyo"]); 
            if (!pRegistro.IsNull("FechaRegistro")) respuesta.FechaRegistro = Convert.ToDateTime(pRegistro["FechaRegistro"]); ;
            return respuesta;
        }

        /// <summary>
        /// Seguridad. Obtiene los Servicios establecidos para el usuario
        /// </summary>
        /// <param name="IdUsuarioOwner">Id del Usuario que configura los permisos.</param>
        /// <param name="IdUsuario">Id del Usuario al que se le configuran los permisos.</param>
        /// <param name="Tabla">Nombre de la tabla para obtener la información de la base de datos.</param>
        /// <returns>[DataTable] Listado de Servicios configurados</returns>
        public DataTable loadPermissionDB(int IdUsuarioOwner, int IdUsuario, string Tabla)
        {
            bd BD = new bd();
            StringBuilder SqlCmd = new StringBuilder("SELECT * FROM " + Tabla + " WHERE idOwner = " + IdUsuarioOwner.ToString() + " AND idNewUsuario = " + IdUsuario.ToString());
            DataTable datos = BD.leeDatos(SqlCmd.ToString());
            BD.cierraBD();
            return datos;
        }

        public DataTable loadPermissionDB(string SentenciaSQL)
        {
            bd BD = new bd();
            StringBuilder SqlCmd = new StringBuilder(SentenciaSQL);
            DataTable datos = BD.leeDatos(SqlCmd.ToString());
            BD.cierraBD();
            return datos;
        }

    }

    [Serializable]
    public class Perfiles
    {
        public int IdPerfil { get; set; }
        public int IdAplicacion { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public bool Activo { get; set; }
    }
}
