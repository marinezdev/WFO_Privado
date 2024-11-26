using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace wfiplib
{
    public class admCredencial
    {
        BaseDeDatos b = new BaseDeDatos();

        string nuevaClave = "MtL#" + DateTime.Now.ToString("yy") + "01" + DateTime.Now.ToString("ss");
        
        public int NewUser(ref credencial pCredencial)
        {
            int intIdUsuario = 0;
            string strSQL = "";
            intIdUsuario = daSiguienteIdentificador();

            if (intIdUsuario > 0)
            {
                BaseDeDatos dbUsers = new BaseDeDatos();
                try
                {
                    //strSQL += "INSERT INTO usuarios (Id, Usuario, Clave, Nombre, Conectado, ConectadoFecha, FechaRegistro, FechaCambioClave, IdRol, Correo, Estado) ";
                    strSQL += "INSERT INTO usuarios (Id, Usuario, Clave, Nombre, Conectado, FechaRegistro, FechaCambioClave, IdRol, Correo, Estado) ";
                    //strSQL += "VALUES (@Id, @Usuario, @Clave, @Nombre, @Conectado, @ConectadoFecha, @FechaRegistro, @FechaCambioClave, @IdRol, @Correo, @Estado); ";
                    strSQL += "VALUES (@Id, @Usuario, @Clave, @Nombre, @Conectado,  @FechaRegistro, @FechaCambioClave, @IdRol, @Correo, @Estado); ";

                    pCredencial.Id = intIdUsuario;
                    pCredencial.Estado = wfiplib.E_Estado.Activo;
                    pCredencial.Clave = nuevaClave;
                    pCredencial.Conectado = "N";
                    pCredencial.ConectadoFecha = DateTime.Now;
                    pCredencial.FechaRegistro = DateTime.Now;
                    pCredencial.FechaCambioClave = DateTime.Now;

                    dbUsers.ExecuteCommandQuery(strSQL);
                    dbUsers.AddParameter("@Id", pCredencial.Id, SqlDbType.Int);
                    dbUsers.AddParameter("@Usuario", pCredencial.Usuario, SqlDbType.VarChar);
                    dbUsers.AddParameter("@Clave", Cifrado.Encriptar(pCredencial.Clave), SqlDbType.VarChar);
                    dbUsers.AddParameter("@Nombre", pCredencial.Nombre, SqlDbType.VarChar);
                    dbUsers.AddParameter("@Conectado", pCredencial.Conectado, SqlDbType.Char);
                    //dbUsers.AddParameter("@ConectadoFecha", pCredencial.ConectadoFecha, SqlDbType.DateTime);
                    dbUsers.AddParameter("@FechaRegistro", pCredencial.FechaRegistro, SqlDbType.DateTime);
                    dbUsers.AddParameter("@FechaCambioClave", pCredencial.FechaCambioClave, SqlDbType.DateTime);
                    dbUsers.AddParameter("@IdRol", pCredencial.IdRol, SqlDbType.Int);
                    dbUsers.AddParameter("@Correo", pCredencial.Correo, SqlDbType.VarChar);
                    dbUsers.AddParameter("@Estado", pCredencial.Estado.ToString("d"), SqlDbType.Int);

                    dbUsers.ConnectionOpenToTransaction();
                    dbUsers.BeginTransaction();
                    dbUsers.ExecuteNonQueryToTransaction();         // intIdUsuario = dbUsers.ExecuteScalarToTransactionWithReturnValue();

                    dbUsers.CommitTransaction();
                    dbUsers.CloseConnection();

                    if (pCredencial.IdRol == int.Parse(wfiplib.E_AppRoles.Promotoria.ToString("d")) || pCredencial.IdRol == int.Parse(wfiplib.E_AppRoles.SuperPromotoria.ToString("d")))
                    {
                        //TODO: Este update no debería de funcionar así...
                        strSQL = "UPDATE USUARIOS SET Modulo = 1, IdFlujo = 0, Aplicacion = 2, IdPerfil = 4 WHERE Id = @IdUsuario;";
                        dbUsers.ExecuteCommandQuery(strSQL);
                        dbUsers.AddParameter("@IdUsuario", intIdUsuario, SqlDbType.Int);

                        dbUsers.ConnectionOpenToTransaction();
                        dbUsers.BeginTransaction();
                        dbUsers.ExecuteNonQueryToTransaction();         // intIdUsuario = dbUsers.ExecuteScalarToTransactionWithReturnValue();

                        dbUsers.CommitTransaction();
                        dbUsers.CloseConnection();
                    }

                    //dbUsers.CommitTransaction();
                    //dbUsers.CloseConnection();
                }
                catch(Exception ex)
                {
                    Console.WriteLine("Error:" + ex.Message);
                    try
                    {
                        dbUsers.RollBackTransaction();
                        dbUsers.CloseConnection();
                    }
                    catch (Exception)
                    { }
                    intIdUsuario = -1;
                }
            }
            return intIdUsuario;
        }

        public int Nuevo(ref credencial pCredencial)
        {
            int nuevoId = 0;
            nuevoId = daSiguienteIdentificador();
            pCredencial.Clave = nuevaClave;
            if (nuevoId > 0)
            {
                StringBuilder SqlCmd = new StringBuilder("INSERT INTO usuarios (Id,Usuario,Clave,Nombre,Aplicacion,Grupo,Modulo,Estado,Conectado,ConectadoFecha,FechaRegistro,IdFlujo, FechaCambioClave, Correo)");
                SqlCmd.Append(" VALUES(");
                SqlCmd.Append(nuevoId.ToString());
                SqlCmd.Append(",'" + pCredencial.Usuario + "'");
                SqlCmd.Append(",'" + Cifrado.Encriptar(nuevaClave) + "'"); //MtL# + YY + "10" + MM
                SqlCmd.Append(",'" + pCredencial.Nombre + "'");
                SqlCmd.Append("," + pCredencial.Aplicacion.ToString("d"));
                SqlCmd.Append("," + pCredencial.Grupo.ToString("d"));
                SqlCmd.Append("," + pCredencial.Modulo.ToString("d"));
                SqlCmd.Append(",1,'N',NULL");
                SqlCmd.Append(",GetDate()");
                SqlCmd.Append("," + pCredencial.IdFlujo.ToString());
                SqlCmd.Append(",GetDate()");
                SqlCmd.Append(",'" + pCredencial.Correo.ToString() + "'");
                SqlCmd.Append(")");
                bd BD = new bd();
                BD.ejecutaCmd(SqlCmd.ToString());
                BD.cierraBD();
            }
            return nuevoId;
        }

        public void GuardarClave(credencial pCredencial, string pClave)
        {
            StringBuilder SqlCmd = new StringBuilder("INSERT INTO claves (IdUsuario, Clave) ");
            SqlCmd.Append(" VALUES(");
            SqlCmd.Append(pCredencial.Id);
            SqlCmd.Append(",'" + Cifrado.Encriptar(pClave) + "'");
            SqlCmd.Append(")");
            bd BD = new bd();
            BD.ejecutaCmd(SqlCmd.ToString());
            BD.cierraBD();
        }

        private int daSiguienteIdentificador()
        {
            int resultado = 0;
            String SqlCmd = "INSERT INTO usrctrl(Fecha) VALUES(GetDate()) SELECT SCOPE_IDENTITY()";
            bd BD = new bd();
            resultado = BD.ejecutaCmdScalar(SqlCmd);
            BD.cierraBD();
            return resultado;
        }

        public Boolean DesencriptarALLUsers(string Tabla, string colEncriptada, string colDesencriptada)
        {
            Boolean resultado = false;
            int id = 0;
            string strContraseña = "";
            string strHacking = "";
            String SqlCmd = "SELECT id, " + colEncriptada + " from " + Tabla + " ;";
            bd BD = new bd();
            DataTable datos = BD.leeDatos(SqlCmd);
            if (datos.Rows.Count > 0)
            {
                foreach (DataRow row in datos.Rows)
                {
                    id = (int)row[0];
                    strContraseña = (String)row[1];
                    strHacking = Cifrado.Desencriptar(strContraseña);
                    // BD.ejecutaCmd("UPDATE export_UsuariosWFONew SET Desifrado = '" + strHacking + "' WHERE Id = " + id.ToString() + "; ");
                }
            }
            resultado = true;
            datos.Dispose();
            BD.cierraBD();
            return resultado;
        }

        public Boolean usuarioClaveCorrecto(String pUsuario, String pClave)
        {
            Boolean resultado = false;
            String SqlCmd = "SELECT Clave FROM usuarios WHERE Usuario = '" + pUsuario + "'";
            bd BD = new bd();
            DataTable datos = BD.leeDatos(SqlCmd);
            if (datos.Rows.Count > 0)
            {
                if (!datos.Rows[0].IsNull(0))
                    resultado = ((String)datos.Rows[0][0]).Equals(Cifrado.Encriptar(pClave));
            }
            datos.Dispose();
            BD.cierraBD();
            return resultado;
        }

        /// <summary>
        ///Obtiene las claves que haya usado el usuario a través del tiempo para validarlas
        /// </summary>
        /// <param name="pId">Id de usuario</param>
        /// <returns>Lista con las claves del usuario</returns>
        public List<ClavesUsuario> ObtenerClaves(int pId)
        {
            List<ClavesUsuario> resultado = new List<ClavesUsuario>();
            //bd BD = new bd();
            //StringBuilder SqlCmd = new StringBuilder("SELECT clave FROM claves WHERE idusuario=" + pId);
            //DataTable datos = BD.leeDatos(SqlCmd.ToString());
            //if (datos.Rows.Count > 0)
            //{
            //    foreach (DataRow registro in datos.Rows)
            //    {
            //        ClavesUsuario itm = new ClavesUsuario
            //        {
            //            Clave = registro["clave"].ToString()
            //        };
            //        resultado.Add(itm);
            //    }
            //}
                    
            //BD.cierraBD();
            //return resultado;

            string consulta = "SELECT clave FROM claves WHERE idusuario=@pId";
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@pId", pId, SqlDbType.Int);
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                ClavesUsuario items = new ClavesUsuario()
                {
                    Clave = reader[0].ToString()
                };
                resultado.Add(items);
            }
            reader = null;
            //b.ExecuteReader().Close();
            b.ConnectionCloseToTransaction();
            return resultado;

        }

        public bool ValidaUsuario(string pUsuario)
        {
            bd BD = new bd();
            StringBuilder SqlCmd = new StringBuilder("SELECT usuario FROM usuarios WHERE usuario='" + pUsuario + "'");
            DataTable datos = BD.leeDatos(SqlCmd.ToString());
            BD.cierraBD();
            if (datos.Rows.Count > 0)
                return true;
            else
                return false;

            
        }

        public bool ValidaCorreo(string pCorreo)
        {
            bd BD = new bd();
            StringBuilder SqlCmd = new StringBuilder("SELECT correo FROM usuarios WHERE correo='" + pCorreo + "'");
            DataTable datos = BD.leeDatos(SqlCmd.ToString());
            BD.cierraBD();
            if (datos.Rows.Count > 0)
                return true;
            else
                return false;
        }

        public string CreaNuevaClave(string pUsuario, string pCorreo)
        {
            bd BD = new bd();
            StringBuilder SqlCmd = new StringBuilder("UPDATE usuarios SET clave='" + Cifrado.Encriptar(nuevaClave) + "' WHERE usuario='"+ pUsuario +"' AND correo='"+ pCorreo +"'");
            DataTable datos = BD.leeDatos(SqlCmd.ToString());
            BD.cierraBD();
            return nuevaClave;
        }


        /// <summary>
        /// Obtiene la contraseña del usuario si la ha olvidado
        /// </summary>
        /// <param name="pUsuario">Clave del usuario.</param>
        /// <param name="pCorreo">Correo electrónico asignado a la cuenta de usuario.</param>
        /// <returns>Contraseña sin cifrar.</returns>
        public bool usuarioClaveRecuperada(string pUsuario, string pCorreo, ref string resultado)
        {
            BaseDeDatos b = new BaseDeDatos();

            bool regresar = false;
            string consulta = "SELECT Clave FROM usuarios WHERE Usuario = @Usuario AND Correo = @Correo;";
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@Usuario", pUsuario, SqlDbType.NVarChar);
            b.AddParameter("@Correo", pCorreo, SqlDbType.NVarChar);
            if (b.Select().Rows.Count > 0)
            {
                resultado = Cifrado.Desencriptar(b.SelectString());
                regresar = true;
            }
            b.CloseConnection();
            return regresar;

            //String SqlCmd = "SELECT Clave FROM usuarios WHERE Correo= '" + pUsuario + "'";
            //bd BD = new bd();
            //DataTable datos = BD.leeDatos(SqlCmd);
            //if (datos.Rows.Count > 0)
            //{
            //    if (!datos.Rows[0].IsNull(0))
            //        resultado = Cifrado.Desencriptar(datos.Rows[0][0].ToString());
            //    regresar = true;
            //}
            //datos.Dispose();
            //BD.cierraBD();
            //return regresar;
        }


        public Boolean estaConectado(String pUsuario)
        {
            Boolean resultado = false;
            String SqlCmd = "SELECT Conectado FROM usuarios WHERE Usuario = '" + pUsuario + "'";
            bd BD = new bd();
            DataTable datos = BD.leeDatos(SqlCmd);
            if (datos.Rows.Count > 0) { if (!datos.Rows[0].IsNull(0)) { resultado = !((String)datos.Rows[0][0]).Equals("N"); } }
            datos.Dispose();
            BD.cierraBD();
            return resultado;
        }

        public Boolean existe(String pUsuario)
        {
            Boolean resultado = false;
            String SqlCmd = "SELECT Id FROM usuarios WHERE Usuario = '" + pUsuario + "'";
            bd BD = new bd();
            DataTable datos = BD.leeDatos(SqlCmd);
            resultado = (datos.Rows.Count > 0);
            datos.Dispose();
            BD.cierraBD();
            return resultado;
        }

        public bool registraIdSession(int pId, string pIdSession)
        {
            //Boolean resultado = false;
            //String SqlCmd = "UPDATE usuarios SET Conectado = '" + pIdSession + "', ConectadoFecha = GetDate() WHERE Id=" + pId.ToString();
            //String LogUsuario = "INSERT INTO logUsuarios (idUsuario,idSesion,inicioSesion,FinSesion) VALUES (" + pId.ToString() + ",'" + pIdSession + "',GETDATE(),NULL)";
            //bd BD = new bd();
            //BD.ejecutaCmd(LogUsuario);
            //resultado = BD.ejecutaCmd(SqlCmd);
            //BD.cierraBD();
            //return resultado;

            string consulta = "UPDATE usuarios SET Conectado=@pIdSession, ConectadoFecha=GetDate() WHERE Id=@pId; " +
            "INSERT INTO logUsuarios (idUsuario,idSesion,inicioSesion,FinSesion) VALUES (@pId, @pIdSession,GETDATE(),NULL)";
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@pId", pId, SqlDbType.Int);
            b.AddParameter("@pIdSession", pIdSession, SqlDbType.NVarChar);
            if (b.InsertUpdateDelete() > 0)
                return true;
            else
                return false;
        }

        public Boolean desconecta(int pId, string pIdSession)
        {
            Boolean resultado = false;
            String SqlCmd = "UPDATE usuarios SET Conectado = 'N', ConectadoFecha = NULL WHERE Id=" + pId.ToString();
            String LogUsuario = "UPDATE logUsuarios SET FinSesion=GETDATE() WHERE FinSesion IS NULL AND IdUsuario=" + pId.ToString();
            bd BD = new bd();
            BD.ejecutaCmd(LogUsuario);
            resultado = BD.ejecutaCmd(SqlCmd);
            BD.cierraBD();
            return resultado;
        }

        public credencial carga(string pUsuario)
        {
            credencial resultado = null;
            String SqlCmd = "SELECT * FROM usuarios WHERE Id = '" + pUsuario + "'";
            bd BD = new bd();
            DataTable datos = BD.leeDatos(SqlCmd);
            if (datos.Rows.Count > 0)
            {
                resultado = arma(datos.Rows[0]);
                if (resultado.Modulo == E_Modulo.Promotoria)
                    resultado.IdPromotoria = daPromotoria(resultado.Id);
            }
            datos.Dispose();
            BD.cierraBD();
            return resultado;
        }

        public credencial cargaPorNombre(string pUsuario)
        {
            credencial resultado = null;
            String SqlCmd = "SELECT * FROM usuarios WHERE Usuario = '" + pUsuario + "'";
            bd BD = new bd();
            DataTable datos = BD.leeDatos(SqlCmd);
            if (datos.Rows.Count > 0)
            {
                resultado = arma(datos.Rows[0]);
                if (resultado.Modulo == E_Modulo.Promotoria)
                    resultado.IdPromotoria = daPromotoria(resultado.Id);
            }
            datos.Dispose();
            BD.cierraBD();
            return resultado;
        }

        public credencial carga(int pId)
        {
            credencial resultado = null;
            String SqlCmd = "SELECT * FROM usuarios WHERE Id = " + pId.ToString();
            bd BD = new bd();
            DataTable datos = BD.leeDatos(SqlCmd);
            if (datos.Rows.Count > 0)
            {
                resultado = arma(datos.Rows[0]);
                if (resultado.Modulo == E_Modulo.Promotoria)
                    resultado.IdPromotoria = daPromotoria(resultado.Id);
            }
            datos.Dispose();
            BD.cierraBD();
            return resultado;
        }

        public string DaNombre(String pUsuario)
        {
            string resultado = string.Empty;
            String SqlCmd = "SELECT Nombre FROM usuarios WHERE Usuario = '" + pUsuario + "'"; ;
            bd BD = new bd();
            DataTable datos = BD.leeDatos(SqlCmd);
            if (datos.Rows.Count > 0) { resultado = datos.Rows[0][0].ToString(); }
            datos.Dispose();
            BD.cierraBD();
            return resultado;
        }

        /// <summary>
        /// Obtiene el listado de usuarios
        /// </summary>
        /// <returns>Regresa el Listado de Usuarios</returns>
        public List<credencial> ListadoUsuario()
        {
            List<credencial> resultado = new List<credencial>();
            bd BD = new bd();
            StringBuilder SqlCmd = new StringBuilder("SELECT * FROM usuarios WHERE Id > 0 ORDER BY Nombre");
            DataTable datos = BD.leeDatos(SqlCmd.ToString());
            if (datos.Rows.Count > 0)
            {
                foreach (DataRow registro in datos.Rows)
                {
                    resultado.Add(arma(registro));
                }
            }

            BD.cierraBD();
            return resultado;
        }

        public List<credencial> DaListaUsuarios(E_Aplicacion pAplicacion)
        {
            List<credencial> resultado = new List<credencial>();
            bd BD = new bd();
            StringBuilder SqlCmd = new StringBuilder("SELECT * FROM usuarios WHERE Id > 0 AND Aplicacion = " + pAplicacion.ToString("d") + " ORDER BY Nombre");
            DataTable datos = BD.leeDatos(SqlCmd.ToString());
            if (datos.Rows.Count > 0) foreach (DataRow registro in datos.Rows) resultado.Add(arma(registro));
            BD.cierraBD();
            return resultado;
        }

        private credencial arma(DataRow pRegistro)
        {
            credencial resultado = new credencial();
            if (!pRegistro.IsNull("Id")) resultado.Id = Convert.ToInt32(pRegistro["Id"]);
            if (!pRegistro.IsNull("Usuario")) resultado.Usuario = (Convert.ToString(pRegistro["Usuario"])).Trim();
            if (!pRegistro.IsNull("Clave")) resultado.Clave = (Convert.ToString(pRegistro["Clave"])).Trim();
            if (!pRegistro.IsNull("Nombre")) resultado.Nombre = (Convert.ToString(pRegistro["Nombre"])).Trim();
            if (!pRegistro.IsNull("Grupo")) resultado.Grupo = (E_CredencialGrupo)(pRegistro["Grupo"]);
            if (!pRegistro.IsNull("Estado")) resultado.Estado = (E_Estado)pRegistro["Estado"];
            if (!pRegistro.IsNull("FechaRegistro")) resultado.FechaRegistro = Convert.ToDateTime(pRegistro["FechaRegistro"]);
            if (!pRegistro.IsNull("Modulo")) resultado.Modulo = (E_Modulo)pRegistro["Modulo"];
            if (!pRegistro.IsNull("IdFlujo")) resultado.IdFlujo = Convert.ToInt32(pRegistro["IdFlujo"]);
            if (!pRegistro.IsNull("Aplicacion")) resultado.Aplicacion = (E_Aplicacion)pRegistro["Aplicacion"];
            if (!pRegistro.IsNull("FechaCambioClave")) resultado.FechaCambioClave = Convert.ToDateTime(pRegistro["FechaCambioClave"]);
            if (!pRegistro.IsNull("correo")) resultado.Correo = Convert.ToString(pRegistro["Correo"]);
            if (!pRegistro.IsNull("IdRol")) resultado.IdRol = Convert.ToInt32(pRegistro["IdRol"]);
            if (!pRegistro.IsNull("IdPerfil")) resultado.IdPerfil = Convert.ToInt32(pRegistro["IdPerfil"]);
            resultado.usuarioMesa = (new admUsuarioMesa()).lista(resultado.Id);
            return resultado;
        }

        public void modifica(credencial pCredencial)
        {
            StringBuilder SqlCmd = new StringBuilder("UPDATE usuarios SET");
            SqlCmd.Append(" Usuario='" + pCredencial.Usuario + "'");
            SqlCmd.Append(",Clave='" + Cifrado.Encriptar(pCredencial.Clave) + "'");
            SqlCmd.Append(",Nombre='" + pCredencial.Nombre + "'");
            SqlCmd.Append(",Grupo=" + pCredencial.Grupo.ToString("d"));
            SqlCmd.Append(",Modulo=" + pCredencial.Modulo.ToString("d"));
            SqlCmd.Append(",Correo='" + pCredencial.Correo + "'");
            SqlCmd.Append(",IdRol=" + pCredencial.IdRol);
            SqlCmd.Append(" WHERE Id=" + pCredencial.Id.ToString());
            bd BD = new bd();
            BD.ejecutaCmd(SqlCmd.ToString());
            BD.cierraBD();
        }

        public void activa(string pId)
        {
            bd BD = new bd();
            BD.ejecutaCmd("UPDATE usuarios SET Conectado = 'N', ConectadoFecha = NULL, Estado = " + E_Estado.Activo.ToString("d") + " WHERE Id=" + pId);
            BD.cierraBD();
        }

        public void desactiva(string pId)
        {
            bd BD = new bd();
            BD.ejecutaCmd("UPDATE usuarios SET Estado = " + E_Estado.Inactivo.ToString("d") + " WHERE Id=" + pId);
            BD.cierraBD();
        }

        public void DesactivaPorUsuario(string pUsuario)
        {
            bd BD = new bd();
            BD.ejecutaCmd("UPDATE usuarios SET Estado = " + E_Estado.Inactivo.ToString("d") + " WHERE usuario='" + pUsuario + "'");
            BD.cierraBD();
        }

        public void modificaClave(credencial pCredencial)
        {
            String SqlCmd = "UPDATE usuarios SET Clave='" + Cifrado.Encriptar(pCredencial.Clave) + "', FechaCambioClave=GetDate() WHERE Id=" + pCredencial.Id.ToString();
            bd BD = new bd();
            BD.ejecutaCmd(SqlCmd);
            BD.cierraBD();
        }

        public string daUsuarioClaveDeId(string pId)
        {
            string resultado = "";
            String SqlCmd = "SELECT CONCAT(Usuario, ' - ', Clave) AS UsrClv FROM usuarios WHERE Id = " + pId;
            bd BD = new bd();
            DataTable datos = BD.leeDatos(SqlCmd);
            if (datos.Rows.Count > 0) { resultado = Convert.ToString(datos.Rows[0][0]).Trim(); }
            datos.Dispose();
            BD.cierraBD();
            return resultado;
        }

        public List<CboValorTexto> usuariosSinPromotoria()
        {
            List<CboValorTexto> resultado = new List<CboValorTexto>();
            string SqlCmd = "SELECT Id, Nombre FROM usuarios WHERE Modulo=" + E_Modulo.Promotoria.ToString("d") + " AND Id Not In (SELECT IdUsuario FROM promotoriaUsuario)";
            bd BD = new bd();
            DataTable datos = BD.leeDatos(SqlCmd);
            if (datos.Rows.Count > 0) foreach (DataRow registro in datos.Rows) resultado.Add(armaUsrValorTexto(registro));
            BD.cierraBD();
            return resultado;
        }

        public List<CboValorTexto> usuariosConPromotoria(string pIdPromotoria)
        {
            List<CboValorTexto> resultado = new List<CboValorTexto>();
            string SqlCmd = "SELECT Id, Nombre FROM usuarios WHERE Modulo=" + E_Modulo.Promotoria.ToString("d") + " AND Id In (SELECT IdUsuario FROM promotoriaUsuario WHERE idPromotoria = " + pIdPromotoria + ")";
            bd BD = new bd();
            DataTable datos = BD.leeDatos(SqlCmd);
            if (datos.Rows.Count > 0) foreach (DataRow registro in datos.Rows) resultado.Add(armaUsrValorTexto(registro));
            BD.cierraBD();
            return resultado;
        }

        private CboValorTexto armaUsrValorTexto(DataRow pRegistro)
        {
            CboValorTexto resultado = new CboValorTexto();
            if (!pRegistro.IsNull("Id")) resultado.valor = Convert.ToString(pRegistro["Id"]);
            if (!pRegistro.IsNull("Nombre")) resultado.texto = Convert.ToString(pRegistro["Nombre"]);
            return resultado;
        }

        public void asignaPromotoria(string pIdPromotoria, string pIdUsuario)
        {
            string SqlCmd = "INSERT INTO promotoriaUsuario(idPromotoria,idUsuario) VALUES(" + pIdPromotoria + "," + pIdUsuario + ")";
            bd BD = new bd();
            BD.ejecutaCmd(SqlCmd);
            BD.cierraBD();
        }

        public void quitaPromotoria(string pIdUsuario)
        {
            string SqlCmd = "DELETE promotoriaUsuario WHERE idUsuario=" + pIdUsuario;
            bd BD = new bd();
            BD.ejecutaCmd(SqlCmd);
            BD.cierraBD();
        }

        public int daPromotoria(int IdUsr)
        {
            int resultado = 0;
            String SqlCmd = "SELECT * FROM promotoriaUsuario WHERE IdUsuario=" + IdUsr.ToString();
            bd BD = new bd();
            DataTable Datos = BD.leeDatos(SqlCmd);
            if (Datos.Rows.Count > 0) { resultado = Convert.ToInt32(Datos.Rows[0]["IdPromotoria"]); }
            BD.cierraBD();
            return resultado;
        }

        public List<credencial> daListaUsuariosCbo(E_CredencialGrupo pGrupo, E_Modulo pModulo)
        {
            List<credencial> resultado = new List<credencial>();
            credencial oSeleccinar = new credencial();
            oSeleccinar.Usuario = "";
            oSeleccinar.Nombre = "SELECCIONAR";
            resultado.Add(oSeleccinar);

            bd BD = new bd();
            StringBuilder SqlCmd = new StringBuilder("SELECT * FROM usuarios");
            SqlCmd.Append(" WHERE Grupo=" + pGrupo.ToString("d"));
            SqlCmd.Append(" AND Modulo=" + pModulo.ToString("d"));
            SqlCmd.Append(" AND Estado=" + E_Estado.Activo.ToString("d"));
            SqlCmd.Append(" ORDER BY Nombre");
            DataTable datos = BD.leeDatos(SqlCmd.ToString());
            if (datos.Rows.Count > 0) foreach (DataRow registro in datos.Rows) resultado.Add(arma(registro));
            BD.cierraBD();
            return resultado;
        }

        public List<credencial> daListaUsuariosCboReporte(E_CredencialGrupo pGrupo, E_Modulo pModulo, int pIdFlujo)
        {
            List<credencial> resultado = new List<credencial>();
            credencial oSeleccinar = new credencial();
            oSeleccinar.Usuario = "";
            oSeleccinar.Nombre = "SELECCIONAR";
            resultado.Add(oSeleccinar);

            bd BD = new bd();
            StringBuilder SqlCmd = new StringBuilder("SELECT * FROM usuarios");
            SqlCmd.Append(" WHERE Grupo=" + pGrupo.ToString("d"));
            SqlCmd.Append(" AND Modulo=" + pModulo.ToString("d"));
            SqlCmd.Append(" AND IdFlujo=" + pIdFlujo.ToString());
            SqlCmd.Append(" AND Estado=" + E_Estado.Activo.ToString("d"));
            SqlCmd.Append(" ORDER BY Nombre");
            DataTable datos = BD.leeDatos(SqlCmd.ToString());
            if (datos.Rows.Count > 0) foreach (DataRow registro in datos.Rows) resultado.Add(arma(registro));
            BD.cierraBD();
            return resultado;
        }

        public List<credencial> daListaUsuariosReporte(E_CredencialGrupo pGrupo, E_Modulo pModulo, int pIdFlujo)
        {
            List<credencial> resultado = new List<credencial>();
            bd BD = new bd();
            StringBuilder SqlCmd = new StringBuilder("SELECT * FROM usuarios");
            SqlCmd.Append(" WHERE Grupo=" + pGrupo.ToString("d"));
            SqlCmd.Append(" AND Modulo=" + pModulo.ToString("d"));
            SqlCmd.Append(" AND IdFlujo=" + pIdFlujo.ToString());
            SqlCmd.Append(" AND Estado=" + E_Estado.Activo.ToString("d"));
            SqlCmd.Append(" ORDER BY Nombre");
            DataTable datos = BD.leeDatos(SqlCmd.ToString());
            if (datos.Rows.Count > 0) foreach (DataRow registro in datos.Rows) resultado.Add(arma(registro));
            BD.cierraBD();
            return resultado;
        }

        public DataTable UsuariosParaChart()
        {
            BaseDeDatos b = new BaseDeDatos();
            string consulta = "SELECT idrol, COUNT(idrol) AS RolesPorUsuario FROM usuarios WHERE idrol>0 GROUP BY idrol";
            b.ExecuteCommandQuery(consulta);
            return b.Select();
        }

        #region ****************************** Mantenimiento *****************************************************

        public DataSet ReseteoContrasena(string usuario)
        {
            BaseDeDatos b = new BaseDeDatos();
            b.ExecuteCommandQuery("DECLARE @USUARIO VARCHAR(MAX); DECLARE @COUNT INT; SET @USUARIO = @usr;" +
            "SET @COUNT = (SELECT COUNT(Id) FROM USUARIOS WHERE Usuario LIKE @USUARIO); " +
            "IF @COUNT = 1 " +
            "   BEGIN " +
            "      UPDATE usuarios SET Clave = 'euQ9XAd8hjPBMhd/h2omUA==' , Conectado = 'N', ConectadoFecha = NULL, Estado = 1 WHERE Id = (SELECT Id FROM USUARIOS WHERE Usuario LIKE @usuario) " +
            "      (SELECT Id, Usuario, Correo, Nombre, Estado, Conectado, ConectadoFecha FROM USUARIOS WHERE Usuario LIKE @USUARIO); " +
            "      SELECT 'Se reseteó la contraseña del usuario.' AS Mensaje; " +
            "   END " +
            "ELSE " +
            "   BEGIN " +
            "      (SELECT Id, Usuario, Correo, Nombre, Estado, Conectado, ConectadoFecha FROM USUARIOS WHERE Usuario LIKE @USUARIO); " +
            "      SELECT 'Encontrados los anteriores, elija el que desee resetear la contraseña.' AS Mensaje; " +
            "   END");
            b.AddParameter("@usr", "%" + usuario + "%", SqlDbType.VarChar);
            return b.SelectExecuteFunctions();
        }

        public DataSet Desconectar(string usuario)
        {
            BaseDeDatos b = new BaseDeDatos();
            b.ExecuteCommandQuery("DECLARE @USUARIO VARCHAR(MAX); " +
            "DECLARE @COUNT INT; " +
            "SET @USUARIO = @usr; " +
            "SET @COUNT = (SELECT COUNT(Id) FROM USUARIOS WHERE Usuario LIKE @USUARIO); " +
            "IF @COUNT = 1 " +
            "   BEGIN " +
            "       UPDATE usuarios SET Conectado = 'N', ConectadoFecha = NULL, Estado = 1 WHERE usuarios.Id = (SELECT Id FROM USUARIOS WHERE Usuario LIKE @USUARIO) " +
            "       (SELECT Id, Usuario, Correo, Nombre, Estado, Conectado, ConectadoFecha FROM USUARIOS WHERE Usuario LIKE @USUARIO); " +
            "       SELECT 'Se desbloqueó el usuario.' AS Mensaje; " +
            "   END " +
            "ELSE " +
            "   BEGIN " +
            "       (SELECT Id, Usuario, Correo, Nombre, Estado, Conectado, ConectadoFecha FROM USUARIOS WHERE Usuario LIKE @USUARIO); " +
            "       SELECT 'Encontrados los anteriores, elija el que desee desbloquear.' AS Mensaje; " +
            "   END");
            b.AddParameter("@usr", "%" + usuario + "%", SqlDbType.VarChar);
            //return b.Select();
            return b.SelectExecuteFunctions();
        }

        public int ReiniciarSupervisor()
        {
            b.ExecuteCommandQuery("UPDATE usuarios SET conectado='N', ConectadoFecha=NULL WHERE id=155");
            return b.InsertUpdateDelete();
        }

        #endregion


    }

    [Serializable]
    public class credencial
    {
        public int Id { get; set; }
        public string Usuario { get; set; }
        public string Clave { get; set; }
        public string Nombre { get; set; }
        public E_CredencialGrupo Grupo { get; set; }
        public E_Estado Estado { get; set; }
        public string Conectado { get; set; }
        public DateTime ConectadoFecha { get; set; }
        private DateTime mFechaRegistro = new DateTime(2000, 1, 1, 0, 0, 0);
        public DateTime FechaRegistro { get { return mFechaRegistro; } set { mFechaRegistro = value; } }
        private E_Modulo mModulo = E_Modulo.Ninguno;
        public E_Modulo Modulo { get { return mModulo; } set { mModulo = value; } }
        public List<usuarioMesa> usuarioMesa { get; set; }
        private int mIdPromotoria = 0;
        public int IdPromotoria { get { return mIdPromotoria; } set { mIdPromotoria = value; } }
        private int mIdFlujo = 0;
        public int IdFlujo { get { return mIdFlujo; } set { mIdFlujo = value; } }
        private string mFlujoNombre = "";
        public string FlujoNombre
        {
            get
            {
                if (string.IsNullOrEmpty(mFlujoNombre))
                {
                    if (mIdFlujo > 0) mFlujoNombre = (new admFlujo()).daNombre(mIdFlujo);
                }
                return mFlujoNombre;
            }
        }
        private E_Aplicacion mAplicacion = E_Aplicacion.Ninguno;
        public E_Aplicacion Aplicacion { get { return mAplicacion; } set { mAplicacion = value; } }
        public DateTime FechaCambioClave { get; set; }
        public List<ClavesUsuario> ClavesDeUsuario { get; set; }
        public string Correo { get; set; }
        public int IdRol { get; set; }
        public int IdPerfil { get; set;  }

    }

    public class ClavesUsuario
    {
        public int Id { get; set; }
        public string Clave { get; set; }
    }
}
