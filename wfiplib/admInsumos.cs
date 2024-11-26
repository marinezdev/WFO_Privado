using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace wfiplib
{
    public class admInsumos
    {
        public bool nuevo(insumos pExpediente)
        {
            bool resultado = false;
            string strPrivado = "0";

            if (pExpediente.Privado)
                strPrivado = "1";
            
            StringBuilder SqlCmd = new StringBuilder("Insert Into insumos (IdTramite,IdArchivo,NmArchivo,NmOriginal,Activo,Privado) Values(");
            SqlCmd.Append(pExpediente.IdTramite.ToString());
            SqlCmd.Append("," + pExpediente.Id.ToString());
            SqlCmd.Append(",'" + pExpediente.NmArchivo + "'");
            SqlCmd.Append(",'" + pExpediente.NmOriginal + "'");
            SqlCmd.Append("," + pExpediente.Activo.ToString("d"));
            SqlCmd.Append("," + strPrivado);
            SqlCmd.Append(")");
            bd BD = new bd();
            resultado = BD.ejecutaCmd(SqlCmd.ToString());
            BD.cierraBD();
            return resultado;
        }

        public List<insumos> daLista(int pIdTramite)
        {
            List<insumos> resultado = new List<insumos>();
            bd BD = new bd();
            StringBuilder SqlCmd = new StringBuilder("SELECT * FROM insumos");
            SqlCmd.Append(" WHERE IdTramite = " + pIdTramite.ToString());
            SqlCmd.Append(" AND Activo = " + E_SiNo.Si.ToString("d"));
            SqlCmd.Append(" ORDER BY IdArchivo");
            DataTable datos = BD.leeDatos(SqlCmd.ToString());
            if (datos.Rows.Count > 0) foreach (DataRow registro in datos.Rows) resultado.Add(arma(registro));
            BD.cierraBD();
            return resultado;
        }

        public Boolean tieneInsumos(int pIdTramite)
        {
            Boolean resultado = false;
            String SqlCmd = "SELECT * FROM insumos WHERE IdTramite = " + pIdTramite.ToString();
            bd BD = new bd();
            DataTable datos = BD.leeDatos(SqlCmd);
            resultado = (datos.Rows.Count > 0);
            datos.Dispose();
            BD.cierraBD();
            return resultado;
        }

        private insumos arma(DataRow pRegistro)
        {
            insumos resultado = new insumos();
            if (!pRegistro.IsNull("IdTramite")) resultado.IdTramite = Convert.ToInt32(pRegistro["IdTramite"]);
            if (!pRegistro.IsNull("IdArchivo")) resultado.Id = Convert.ToInt32(pRegistro["IdArchivo"]);
            if (!pRegistro.IsNull("NmArchivo")) resultado.NmArchivo = (Convert.ToString(pRegistro["NmArchivo"])).Trim();
            if (!pRegistro.IsNull("NmOriginal")) resultado.NmOriginal = (Convert.ToString(pRegistro["NmOriginal"])).Trim();
            if (!pRegistro.IsNull("Activo")) resultado.Activo = (E_SiNo)pRegistro["Activo"];
            if (!pRegistro.IsNull("Privado")) resultado.Privado = Convert.ToBoolean(pRegistro["Privado"]);
            return resultado;
        }

        public bool elimina(int pIdTramite, int pIdArchivo)
        {
            bd BD = new bd();
            StringBuilder SqlCmd = new StringBuilder("Update insumos");
            SqlCmd.Append(" SET Activo=" + E_SiNo.No.ToString("d"));
            SqlCmd.Append(" WHERE IdTramite=" + pIdTramite.ToString());
            SqlCmd.Append(" AND IdArchivo=" + pIdArchivo.ToString());
            bool resultado = BD.ejecutaCmd(SqlCmd.ToString());
            BD.cierraBD();
            return resultado;
        }

        public Boolean existe(int pIdTramite, int pIdArchivo)
        {
            Boolean resultado = false;
            String SqlCmd = "SELECT * FROM insumos WHERE IdTramite=" + pIdTramite.ToString() + " AND IdArchivo=" + pIdArchivo.ToString();
            bd BD = new bd();
            DataTable datos = BD.leeDatos(SqlCmd);
            resultado = (datos.Rows.Count > 0);
            datos.Dispose();
            BD.cierraBD();
            return resultado;
        }
    }

    [Serializable]
    public class insumos
    {
        private int mId = 0;
        public int Id { get { return mId; } set { mId = value; } }
        private int mIdTramite = 0;
        public int IdTramite { get { return mIdTramite; } set { mIdTramite = value; } }
        private string mNmArchivo = "";
        public string NmArchivo { get { return mNmArchivo; } set { mNmArchivo = value; } }
        private string mNmOriginal = "";
        public string NmOriginal { get { return mNmOriginal; } set { mNmOriginal = value; } }
        private E_SiNo mActivo = E_SiNo.No;
        public E_SiNo Activo { get { return mActivo; } set { mActivo = value; } }
        private string mRutaTemporal = "";
        public string RutaTemporal { get { return mRutaTemporal; } set { mRutaTemporal = value; } }
        private bool mPrivado = false;
        public bool Privado { get { return mPrivado; } set { mPrivado = value; } }
        public string CarpetaInicial = "\\uploadInsumos\\";

        //public string CarpetaArchivada = "\\DocsUp\\";
#if DEBUG
        public string CarpetaArchivada = @"C:\files\WFOPrivado\insumos\";
#else
        public string CarpetaArchivada = @"F:\files\WFOPrivado\insumos\";
#endif
    }
}
