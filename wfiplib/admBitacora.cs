using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace wfiplib
{
    public class admBitacora
    {
        public bool Nuevo(bitacora pTramite)
        {
            bool resultado = false;
            StringBuilder SqlCmd = new StringBuilder("Insert Into bitacora (IdFlujo,IdTipoTramite,IdTramite,IdMesa,Usuario,IdUsuario,FechaInicio,FechaTermino,Estado,Observacion,ObservacionPrivada)");
            SqlCmd.Append(" Values(");
            SqlCmd.Append(pTramite.IdFlujo.ToString());
            SqlCmd.Append("," + pTramite.IdTipoTramite.ToString("d"));
            SqlCmd.Append("," + pTramite.IdTramite.ToString());
            SqlCmd.Append("," + pTramite.IdMesa.ToString());
            SqlCmd.Append(",'" + pTramite.Usuario + "'");
            SqlCmd.Append("," + pTramite.IdUsuario.ToString());
            SqlCmd.Append(",'" + pTramite.FechaInicio.ToString("yyyyMMdd HH:mm:ss") + "'");
            SqlCmd.Append(",getdate()");
            SqlCmd.Append("," + pTramite.Estado.ToString("d"));
            SqlCmd.Append(",'" + pTramite.Observacion + "'");
            SqlCmd.Append(",'" + pTramite.ObservacionPrivada + "'");
            SqlCmd.Append(")");
            bd BD = new bd();
            resultado = BD.ejecutaCmd(SqlCmd.ToString());
            BD.cierraBD();
            return resultado;
        }

        public List<bitacora> daLista(int pIdTramite)
        {
            List<bitacora> resultado = new List<bitacora>();
            bd BD = new bd();
            StringBuilder SqlCmd = new StringBuilder("SELECT * FROM vw_bitacora_dos");
            SqlCmd.Append(" WHERE IdTramite = " + pIdTramite.ToString());
            SqlCmd.Append(" ORDER BY FechaTermino Desc");
            DataTable datos = BD.leeDatos(SqlCmd.ToString());
            if (datos.Rows.Count > 0)
                foreach (DataRow registro in datos.Rows)
                    resultado.Add(arma(registro));
            BD.cierraBD();
            return resultado;
        }

        private bitacora arma(DataRow pRegistro)
        {
            bitacora resultado = new bitacora();
            if (!pRegistro.IsNull("Id")) resultado.Id = Convert.ToInt32(pRegistro["Id"]);
            if (!pRegistro.IsNull("IdFlujo")) resultado.IdFlujo = Convert.ToInt32(pRegistro["IdFlujo"]);
            if (!pRegistro.IsNull("IdTipoTramite")) resultado.IdTipoTramite = (E_TipoTramite)(pRegistro["IdTipoTramite"]);
            if (!pRegistro.IsNull("IdTramite")) resultado.IdTramite = Convert.ToInt32(pRegistro["IdTramite"]);
            if (!pRegistro.IsNull("IdMesa")) resultado.IdMesa = Convert.ToInt32(pRegistro["IdMesa"]);
            if (!pRegistro.IsNull("Usuario")) resultado.Usuario = Convert.ToString(pRegistro["Usuario"]);
            if (!pRegistro.IsNull("IdUsuario")) resultado.IdUsuario = Convert.ToInt32(pRegistro["IdUsuario"]);
            if (!pRegistro.IsNull("FechaInicio")) resultado.FechaInicio = Convert.ToDateTime(pRegistro["FechaInicio"]);
            if (!pRegistro.IsNull("FechaTermino")) resultado.FechaTermino = Convert.ToDateTime(pRegistro["FechaTermino"]);
            if (!pRegistro.IsNull("Estado")) resultado.Estado = (E_EstadoMesa)pRegistro["Estado"];
            if (!pRegistro.IsNull("Observacion")) resultado.Observacion = (Convert.ToString(pRegistro["Observacion"])).Trim();
            if (!pRegistro.IsNull("ObservacionPrivada")) resultado.ObservacionPrivada = (Convert.ToString(pRegistro["ObservacionPrivada"])).Trim();

            if (!pRegistro.IsNull("MesaNombre")) resultado.NombreMesa = (Convert.ToString(pRegistro["MesaNombre"])).Trim();
            if (!pRegistro.IsNull("Usuario")) resultado.NombreUsuario = (Convert.ToString(pRegistro["Usuario"])).Trim();

            return resultado;
        }

        public bitacora daUltimo(int pIdTramite)
        {
            bitacora resultado = new bitacora();
            bd BD = new bd();
            StringBuilder SqlCmd = new StringBuilder("SELECT TOP 1 * FROM vw_bitacora");
            SqlCmd.Append(" WHERE IdTramite = " + pIdTramite.ToString());
            SqlCmd.Append(" ORDER BY FechaTermino Desc");
            DataTable datos = BD.leeDatos(SqlCmd.ToString());
            if (datos.Rows.Count > 0) foreach (DataRow registro in datos.Rows) resultado = arma(registro);
            BD.cierraBD();
            return resultado;
        }

        public DataTable daProduccionMes(string pUsuario, string pFechaInicio, string pFechaTermino)
        {
            bd BD = new bd();
            StringBuilder SqlCmd = new StringBuilder("SELECT * FROM vw_bitacora");
            SqlCmd.Append(" WHERE Usuario = '" + pUsuario + "'");
            SqlCmd.Append(" AND FechaTermino >= '" + pFechaInicio + "'");
            SqlCmd.Append(" AND FechaTermino < '" + pFechaTermino + "'");
            SqlCmd.Append(" ORDER BY FechaTermino");
            DataTable datos = BD.leeDatos(SqlCmd.ToString());
            //if (datos.Rows.Count > 0) foreach (DataRow registro in datos.Rows) resultado.Add(arma(registro));
            BD.cierraBD();
            return datos;
        }

        public DataTable DaHistoria(int pId)
        {
            string SqlCmd = "Select MesaNombre,UsuarioNombre,FechaInicio,FechaTermino,Tiempo,ObservacionPrivada From vw_bitacora_dos Where IdTramite=" + pId.ToString() + " Order By FechaInicio";
            bd BD = new bd();
            DataTable datos = BD.leeDatos(SqlCmd);
            BD.cierraBD();
            return datos;
        }

        #region ****************************** Mantenimiento *****************************************************

        public int BitacoraTramiteGuardarCambio(string estado, string observacion, string observacionprivada, string id)
        {
            BaseDeDatos b = new BaseDeDatos();
            b.ExecuteCommandQuery("UPDATE bitacora SET Estado=@estado, Observacion=@observacion, ObservacionPrivada=@observacionprivada WHERE Id=@id");
            b.AddParameter("@estado", estado, SqlDbType.VarChar);
            b.AddParameter("@observacion", observacion, SqlDbType.VarChar);
            b.AddParameter("@observacionprivada", observacionprivada, SqlDbType.VarChar);
            b.AddParameter("@id", id, SqlDbType.Int);
            return b.InsertUpdateDelete();
        }

        public DataTable BitacoraTramite(string idtramite)
        {
            BaseDeDatos b = new BaseDeDatos();
            b.ExecuteCommandQuery("SELECT Id, " +
            "(SELECT Nombre FROM flujo WHERE id = idFlujo) AS Flujo, " +
            "(SELECT Nombre FROM tipoTramite WHERE Id = IdTipoTramite) AS TipoTramite, " +
            "(SELECT Nombre FROM mesa WHERE Id = IdMesa) AS Mesa, " +
            "Usuario, FechaInicio, FechaTermino, " +
            "(SELECT Nombre FROM statusMesa WHERE Id=Estado) AS Estado,  " +
            "Observacion, ObservacionPrivada " +
            "FROM bitacora " +
            "WHERE IdTramite=@idtramite ORDER BY id ASC;");
            b.AddParameter("@idtramite", idtramite, SqlDbType.Int);
            return b.Select();
        }

        public DataTable BitacoraTramiteDetalle(string id)
        {
            BaseDeDatos b = new BaseDeDatos();
            b.ExecuteCommandQuery("SELECT Id, " +
            "(SELECT Nombre FROM flujo WHERE id = idFlujo) AS Flujo, " +
            "(SELECT Nombre FROM tipoTramite WHERE Id = IdTipoTramite) AS TipoTramite, " +
            "(SELECT Nombre FROM mesa WHERE Id = IdMesa) AS Mesa, " +
            "Usuario, FechaInicio, FechaTermino, " +
            "(SELECT Nombre FROM statusMesa WHERE Id=Estado) AS Estado,  " +
            "Observacion, ObservacionPrivada " +
            "FROM bitacora " +
            "WHERE Id=@id");
            b.AddParameter("@id", id, SqlDbType.Int);
            return b.Select();
        }

        public int BitacoraTramiteEliminar(string id)
        {
            BaseDeDatos b = new BaseDeDatos();
            b.ExecuteCommandQuery("DELETE FROM bitacora WHERE Id=@id");
            b.AddParameter("@id", id, SqlDbType.Int);
            return b.InsertUpdateDelete();
        }

        #endregion

        //************* Reingresos

        public DataTable Reingresos()
        {
            BaseDeDatos b = new BaseDeDatos();
            b.ExecuteCommandQuery("DECLARE @FechaInicio DATETIME; " +
            "DECLARE @FechaTermino DATETIME; " +
            "DECLARE @_Reingresos TABLE(IdTramite INT, IdMesa INT, Mesa VARCHAR(100), TotalReingresos INT, PrimerRegistro INT); " +
            "  " +
            "SET @FechaInicio = '2019/05/14 00:00:00'; " +
            "SET @FechaTermino = '2019/06/03 23:59:29' " +
            "  " +
            "INSERT INTO @_Reingresos " +
            "SELECT BP.IdTramite, BP.IdMesa, (SELECT mesa.Nombre FROM mesa WHERE mesa.Id = BP.IdMesa) AS Mesa, COUNT(BP.Id) AS TotalReingresos, " +
            "        IIF( " +
            "                ( " +
            "                    SELECT bitacora.Id " +
            "  " +
            "                    FROM bitacora " +
            "  " +
            "                    WHERE bitacora.Id = ( " +
            "                            SELECT MIN(bitacora.Id) " +
            "  " +
            "                            FROM bitacora " +
            "  " +
            "                            WHERE IdTramite = BP.IdTramite " +
            "  " +
            "                            AND IdMesa = BP.IdMesa) " +
            "                            AND FechaTermino BETWEEN @FechaInicio AND @FechaTermino " +
            "                ) > 0, -1, 0 " +
            "        ) AS PrimerRegistro " +
            "FROM bitacora BP " +
            "WHERE " +
            "        (BP.FechaTermino BETWEEN @FechaInicio AND @FechaTermino) " +
            "        AND NOT BP.IdMesa = -1 " +
            "GROUP BY BP.IdTramite, BP.IdMesa " +
            "HAVING COUNT(BP.Id) > 1 " +
            "  " +
            "SELECT " +
            "        DISTINCT mesa.Nombre, " +
            "        ISNULL((SELECT COUNT(IdTramite) FROM @_Reingresos WHERE Mesa = mesa.Nombre),0) AS Tramites, " +
            "        ISNULL((SELECT SUM(TotalReingresos + PrimerRegistro)  FROM @_Reingresos WHERE Mesa = mesa.Nombre),0)  AS Total_Reingresos " +
            "FROM mesa");
            return b.Select();
        }




    }

    public class bitacora
    {
        private int mId = 0;
        public int Id { get { return mId; } set { mId = value; } }
        private int mIdFlujo = 0;
        public int IdFlujo { get { return mIdFlujo; } set { mIdFlujo = value; } }
        private E_TipoTramite mIdTipoTramite = E_TipoTramite.Ninguno;
        public E_TipoTramite IdTipoTramite { get { return mIdTipoTramite; } set { mIdTipoTramite = value; } }
        private int mIdTramite = 0;
        public int IdTramite { get { return mIdTramite; } set { mIdTramite = value; } }
        private int mIdMesa = 0;
        public int IdMesa { get { return mIdMesa; } set { mIdMesa = value; } }
        private string mUsuario = string.Empty;
        public string Usuario { get { return mUsuario; } set { mUsuario = value; } }
        public int IdUsuario { get; set; }
        private DateTime mFechaInicio = new DateTime(2000, 1, 1, 0, 0, 0);
        public DateTime FechaInicio { get { return mFechaInicio; } set { mFechaInicio = value; } }
        private DateTime mFechaTermino = new DateTime(2000, 1, 1, 0, 0, 0);
        public DateTime FechaTermino { get { return mFechaTermino; } set { mFechaTermino = value; } }
        private E_EstadoMesa mEstado = E_EstadoMesa.Registro;
        public E_EstadoMesa Estado { get { return mEstado; } set { mEstado = value; } }
        private string mObservacion = String.Empty;
        public string Observacion { get { return mObservacion; } set { mObservacion = value; } }
        
        private string mObservacionPrivada = "";
        public string ObservacionPrivada { get { return mObservacionPrivada; } set { mObservacionPrivada = value; } }

        private string mNombreMesa = string.Empty;
        public string NombreMesa { get { return mNombreMesa; } set { mNombreMesa = value; } }
        private string mNombreUsuario = string.Empty;
        public string NombreUsuario { get { return mNombreUsuario; } set { mNombreUsuario = value; } }

        public string TextoHtml { get { return "<strong>" + NombreMesa + "</strong><br />" + FechaInicio.ToString("dd/MM/yyyy hh:mm") + "<br/>" + FechaTermino.ToString("dd/MM/yyyy hh:mm") + "<br />" + NombreUsuario + "<br />" + Estado.ToString() + "<br />" + Observacion + "<hr />"; } }
        public string TextoHtmlPrivado { get { return "<strong>" + NombreMesa + "</strong><br />" + FechaInicio.ToString("dd/MM/yyyy hh:mm") +  "<br/>" + FechaTermino.ToString("dd/MM/yyyy hh:mm") + "<br />" + NombreUsuario + "<br />" + Estado.ToString() + "<br /><i>Observaciones privadas</i><br>" + ObservacionPrivada + "<hr />"; } }
        public string TextoHtmlPrivado2 { get { return "<strong>" + NombreMesa + "</strong><br />" + FechaInicio.ToString("dd/MM/yyyy hh:mm") + "<br/>" + FechaTermino.ToString("dd/MM/yyyy hh:mm") + "<br />" + NombreUsuario + "<br />" + Estado.ToString() + "<br /><i>Observaciones públicas </i><br>" + Observacion + "<br><i>Observaciones privadas</i><br>" + ObservacionPrivada + "<hr />"; } }
    }
}
