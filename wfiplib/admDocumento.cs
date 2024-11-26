using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace wfiplib
{
    public class admDocumento
    {
        public bool nuevo(documento pDocumento)
        {
            bool resultado = false;
            StringBuilder SqlCmd = new StringBuilder("Insert Into documento (IdTramite,IdTipoDocto,IdArchivo,NmArchivo) Values(");
            SqlCmd.Append(pDocumento.IdTramite.ToString());
            SqlCmd.Append("," + pDocumento.IdTipoDocto.ToString());
            SqlCmd.Append("," + pDocumento.IdArchivo.ToString());
            SqlCmd.Append(",'" + pDocumento.NmArchivo + "'");
            SqlCmd.Append(")");
            bd BD = new bd();
            resultado = BD.ejecutaCmd(SqlCmd.ToString());
            BD.cierraBD();
            return resultado;
        }

        public List<documento> daLista(int pIdTramite)
        {
            List<documento> resultado = new List<documento>();
            bd BD = new bd();
            StringBuilder SqlCmd = new StringBuilder("SELECT * FROM vw_documento");
            SqlCmd.Append(" WHERE IdTramite = " + pIdTramite.ToString());
            SqlCmd.Append(" ORDER BY Nombre");
            DataTable datos = BD.leeDatos(SqlCmd.ToString());
            if (datos.Rows.Count > 0) foreach (DataRow registro in datos.Rows) resultado.Add(arma(registro));
            BD.cierraBD();
            return resultado;
        }

        public documento CargaDocumento(int pIdTramite, int pIdTipoDocto)
        {
            documento resultado = new documento();
            bd BD = new bd();
            StringBuilder SqlCmd = new StringBuilder("SELECT * FROM vw_documento");
            SqlCmd.Append(" WHERE IdTramite = " + pIdTramite.ToString());
            SqlCmd.Append(" AND IdTipoDocto = " + pIdTipoDocto.ToString());
            DataTable datos = BD.leeDatos(SqlCmd.ToString());
            if (datos.Rows.Count > 0) resultado = arma(datos.Rows[0]);
            BD.cierraBD();
            return resultado;
        }

        private documento arma(DataRow pRegistro)
        {
            documento resultado = new documento();
            if (!pRegistro.IsNull("IdTramite")) resultado.IdTramite = Convert.ToInt32(pRegistro["IdTramite"]);
            if (!pRegistro.IsNull("IdTipoDocto")) resultado.IdTipoDocto = Convert.ToInt32(pRegistro["IdTipoDocto"]);
            if (!pRegistro.IsNull("IdArchivo")) resultado.IdArchivo = Convert.ToInt32(pRegistro["IdArchivo"]);
            if (!pRegistro.IsNull("NmArchivo")) resultado.NmArchivo = (Convert.ToString(pRegistro["NmArchivo"])).Trim();
            if (!pRegistro.IsNull("Nombre")) resultado.Nombre = (Convert.ToString(pRegistro["Nombre"])).Trim();
            return resultado;
        }

        public bool EliminaDocto( int pIdTramite, int pIdTipoDocto)
        {
            bd BD = new bd();
            StringBuilder SqlCmd = new StringBuilder("Delete documento");
            SqlCmd.Append(" WHERE IdTramite=" + pIdTramite.ToString());
            SqlCmd.Append(" AND IdTipoDocto=" + pIdTipoDocto.ToString());
            bool resultado = BD.ejecutaCmd(SqlCmd.ToString());
            BD.cierraBD();
            return resultado;
        }
    }

    public class documento
    {
        private int mIdTramite = 0;
        public int IdTramite { get { return mIdTramite; } set { mIdTramite = value; } }
        private int mIdTipoDocto = 0;
        public int IdTipoDocto { get { return mIdTipoDocto; } set { mIdTipoDocto = value; } }
        private int mIdArchivo = 0;
        public int IdArchivo { get { return mIdArchivo; } set { mIdArchivo = value; } }
        private string mNmArchivo = "";
        public string NmArchivo { get { return mNmArchivo; } set { mNmArchivo = value; } }
        private string mNombre = "";
        public string Nombre { get { return mNombre; } set { mNombre = value; } }
    }
}
