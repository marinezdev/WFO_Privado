using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace wfiplib
{
    public class admExpediente
    {
        public bool Nuevo(expediente pExpediente)
        {
            bool resultado = false;
            StringBuilder SqlCmd = new StringBuilder("Insert Into expediente (IdTramite,IdArchivo,NmArchivo,NmOriginal,Activo,Fusion,Descripcion) Values(");
            SqlCmd.Append(pExpediente.IdTramite.ToString());
            SqlCmd.Append("," + pExpediente.Id.ToString());
            SqlCmd.Append(",'" + pExpediente.NmArchivo + "'");
            SqlCmd.Append(",'" + pExpediente.NmOriginal + "'");
            SqlCmd.Append("," + pExpediente.Activo.ToString("d"));
            SqlCmd.Append("," + pExpediente.Fusion.ToString("d"));
            SqlCmd.Append(",'" + pExpediente.Descripcion + "'");
            SqlCmd.Append(")");
            bd BD = new bd();
            resultado = BD.ejecutaCmd(SqlCmd.ToString());
            BD.cierraBD();
            return resultado;
        }

        public bool NuevoRes(expediente pExpediente)
        {
            bool resultado = false;
            StringBuilder SqlCmd = new StringBuilder("Insert Into expediente (IdTramite,IdArchivo,NmArchivo,NmOriginal,Activo,Fusion,Descripcion) Values(");
            SqlCmd.Append(pExpediente.IdTramite.ToString());
            SqlCmd.Append("," + pExpediente.Id_Archivo.ToString());
            SqlCmd.Append(",'" + pExpediente.NmArchivo + "'");
            SqlCmd.Append(",'" + pExpediente.NmOriginal.Replace("'","") + "'");
            SqlCmd.Append("," + pExpediente.Activo.ToString("d"));
            SqlCmd.Append("," + pExpediente.Fusion.ToString("d"));
            SqlCmd.Append(",'" + pExpediente.Descripcion + "'");
            SqlCmd.Append(")");
            bd BD = new bd();
            resultado = BD.ejecutaCmd(SqlCmd.ToString());
            BD.cierraBD();
            return resultado;
        }

        public List<expediente> DaLista(int pIdTramite)
        {
            List<expediente> resultado = new List<expediente>();
            bd BD = new bd();
            StringBuilder SqlCmd = new StringBuilder("SELECT * FROM expediente");
            SqlCmd.Append(" WHERE IdTramite = " + pIdTramite.ToString());
            SqlCmd.Append(" AND Activo = " + E_SiNo.Si.ToString("d"));
            SqlCmd.Append(" AND Fusion = " + E_SiNo.No.ToString("d"));
            SqlCmd.Append(" ORDER BY IdArchivo");
            DataTable datos = BD.leeDatos(SqlCmd.ToString());
            if (datos.Rows.Count > 0) foreach (DataRow registro in datos.Rows) resultado.Add(arma(registro));
            BD.cierraBD();
            return resultado;
        }

        public expediente carga(int pIdTramite, int pIdArchivo)
        {
            expediente resultado = new expediente();
            StringBuilder SqlCmd = new StringBuilder("SELECT * FROM expediente");
            SqlCmd.Append(" WHERE IdTramite = " + pIdTramite.ToString());
            SqlCmd.Append(" AND IdArchivo=" + pIdArchivo.ToString());
            bd BD = new bd();
            DataTable datos = BD.leeDatos(SqlCmd.ToString());
            if (datos.Rows.Count > 0) resultado = arma(datos.Rows[0]);
            datos.Dispose();
            BD.cierraBD();
            return resultado;
        }

        public expediente carga(int pIdArchivo)
        {
            expediente resultado = new expediente();
            StringBuilder SqlCmd = new StringBuilder("SELECT * FROM expediente");
            SqlCmd.Append(" WHERE IdArchivo=" + pIdArchivo.ToString());
            bd BD = new bd();
            DataTable datos = BD.leeDatos(SqlCmd.ToString());
            if (datos.Rows.Count > 0) resultado = arma(datos.Rows[0]);
            datos.Dispose();
            BD.cierraBD();
            return resultado;
        }

        public Boolean tieneFusion(int pIdTramite)
        {
            Boolean resultado = false;
            StringBuilder SqlCmd = new StringBuilder("SELECT IdArchivo FROM expediente");
            SqlCmd.Append(" WHERE IdTramite = " + pIdTramite.ToString());
            SqlCmd.Append(" AND Activo = " + E_SiNo.Si.ToString("d"));
            SqlCmd.Append(" AND Fusion = " + E_SiNo.Si.ToString("d"));
            bd BD = new bd();
            DataTable datos = BD.leeDatos(SqlCmd.ToString());
            resultado = (datos.Rows.Count > 0);
            datos.Dispose();
            BD.cierraBD();
            return resultado;
        }

        public expediente daFusion_sinTiempo(int pIdTramite)
        {
            expediente resultado = new expediente();
            bd BD = new bd();
            StringBuilder SqlCmd = new StringBuilder("SELECT IdTramite, IdArchivo, NmArchivo as NmArchivo, NmOriginal, Activo, Fusion, Descripcion FROM expediente");
            SqlCmd.Append(" WHERE IdTramite = " + pIdTramite.ToString());
            SqlCmd.Append(" AND Activo = " + E_SiNo.Si.ToString("d"));
            SqlCmd.Append(" AND Fusion = " + E_SiNo.Si.ToString("d"));
            SqlCmd.Append(" ORDER BY IdArchivo DESC");
            DataTable datos = BD.leeDatos(SqlCmd.ToString());
            if (datos.Rows.Count > 0) resultado = arma(datos.Rows[0]);
            BD.cierraBD();
            return resultado;
        }

        public expediente daFusion(int pIdTramite)
        {
            expediente resultado = new expediente();
            bd BD = new bd();
            StringBuilder SqlCmd = new StringBuilder("SELECT IdTramite, IdArchivo, (CASE WHEN ISNULL( (SELECT Id FROM tramite WHERE Id = IdTramite AND FechaRegistro BETWEEN DATEADD(DAY,-90,GETDATE())  AND GETDATE()) , '' ) = '' THEN 'Expedido.pdf' ELSE NmArchivo END) as NmArchivo, NmOriginal, Activo, Fusion, Descripcion FROM expediente");
            SqlCmd.Append(" WHERE IdTramite = " + pIdTramite.ToString());
            SqlCmd.Append(" AND Activo = " + E_SiNo.Si.ToString("d"));
            SqlCmd.Append(" AND Fusion = " + E_SiNo.Si.ToString("d"));
            SqlCmd.Append(" ORDER BY IdArchivo DESC");
            DataTable datos = BD.leeDatos(SqlCmd.ToString());
            if (datos.Rows.Count > 0) resultado = arma(datos.Rows[0]);
            BD.cierraBD();
            return resultado;
        }

        private expediente arma(DataRow pRegistro)
        {
            expediente resultado = new expediente();
            if (!pRegistro.IsNull("IdTramite")) resultado.IdTramite = Convert.ToInt32(pRegistro["IdTramite"]);
            if (!pRegistro.IsNull("IdArchivo")) resultado.Id = Convert.ToInt32(pRegistro["IdArchivo"]);
            if (!pRegistro.IsNull("NmArchivo")) resultado.NmArchivo = (Convert.ToString(pRegistro["NmArchivo"])).Trim();
            if (!pRegistro.IsNull("NmOriginal")) resultado.NmOriginal = (Convert.ToString(pRegistro["NmOriginal"])).Trim();
            if (!pRegistro.IsNull("Activo")) resultado.Activo = (E_SiNo)pRegistro["Activo"];
            if (!pRegistro.IsNull("Fusion")) resultado.Fusion = (E_SiNo)pRegistro["Fusion"];
            if (!pRegistro.IsNull("Descripcion")) resultado.Descripcion = Convert.ToString(pRegistro["Descripcion"]);
            return resultado;
        }

        public bool elimina(int pIdTramite, int pIdArchivo)
        {
            bd BD = new bd();
            StringBuilder SqlCmd = new StringBuilder("Update expediente");
            SqlCmd.Append(" SET Activo=" + E_SiNo.No.ToString("d"));
            SqlCmd.Append(" WHERE IdTramite=" + pIdTramite.ToString());
            SqlCmd.Append(" AND IdArchivo=" + pIdArchivo.ToString());
            bool resultado = BD.ejecutaCmd(SqlCmd.ToString());
            BD.cierraBD();
            return resultado;
        }

        public bool eliminaFusion(int pIdTramite)
        {
            bd BD = new bd();
            StringBuilder SqlCmd = new StringBuilder("Update expediente");
            SqlCmd.Append(" SET Activo=" + E_SiNo.No.ToString("d"));
            SqlCmd.Append(" , Fusion=" + E_SiNo.No.ToString("d"));
            SqlCmd.Append(" WHERE IdTramite=" + pIdTramite.ToString());
            SqlCmd.Append(" AND Fusion=" + E_SiNo.Si.ToString("d"));
            bool resultado = BD.ejecutaCmd(SqlCmd.ToString());
            BD.cierraBD();
            return resultado;
        }

        public Boolean existe(int pIdTramite, int pIdArchivo)
        {
            Boolean resultado = false;
            String SqlCmd = "SELECT * FROM expediente WHERE IdTramite=" + pIdTramite.ToString() + " AND IdArchivo=" + pIdArchivo.ToString();
            bd BD = new bd();
            DataTable datos = BD.leeDatos(SqlCmd);
            resultado = (datos.Rows.Count > 0);
            datos.Dispose();
            BD.cierraBD();
            return resultado;
        }

        public bool eliminaTodos(int pIdTramite)
        {
            bd BD = new bd();
            String SqlCmd = "DELETE FROM expediente WHERE IdTramite=" + pIdTramite.ToString();
            bool resultado = BD.ejecutaCmd(SqlCmd);
            BD.cierraBD();
            return resultado;
        }

        /******************************************************************************************************************************************/
        /** MODIFICACIONES NUEVAS EN LA CARGA DE ARCHIVOS **/
        public DataTable ControlArchivoNuevoID()
        {
            DataTable dt = new DataTable();
            bd Data = new bd();
            string qData = "EXEC Control_Archivos_Selecionar ";
            dt = Data.leeDatos(qData);
            Data.cierraBD();
            return dt;
        }
    }

    [Serializable]
    public class expediente
    {
        private int mId = 0;
        public int Id { get { return mId; } set { mId = value; } }
        private int mIdTramite = 0;
        public int IdTramite { get { return mIdTramite; } set { mIdTramite = value; } }
        public int mId_Archivo = 0;
        public int Id_Archivo { get { return mId_Archivo; } set { mId_Archivo = value; } }
        private string mNmArchivo = "";
        public string NmArchivo { get { return mNmArchivo; } set { mNmArchivo = value; } }
        private string mNmOriginal = "";
        public string NmOriginal { get { return mNmOriginal; } set { mNmOriginal = value; } }
        private E_SiNo mActivo = E_SiNo.Si;
        public E_SiNo Activo { get { return mActivo; } set { mActivo = value; } }
        private E_SiNo mFusion = E_SiNo.No;
        public E_SiNo Fusion { get { return mFusion; } set { mFusion = value; } }
        private string mRutaTemporal = "";
        public string RutaTemporal { get { return mRutaTemporal; } set { mRutaTemporal = value; } }
        private string mDescripcion = "";
        public string Descripcion { get { return mDescripcion; } set { mDescripcion = value; } }

        public int Tam { get; set; }

        public string CarpetaInicial = "\\uploadDocuments\\";

        //public string CarpetaArchivada = "\\DocsUp\\";
#if DEBUG
        public string CarpetaArchivada = @"C:\files\WFOPRIVADO\expedientes\";
#else
        public string CarpetaArchivada = @"F:\files\WFOPRIVADO\expedientes\";
#endif
    }
}
