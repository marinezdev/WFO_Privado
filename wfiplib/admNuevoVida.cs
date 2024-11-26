using System;
using System.Data;
using System.Text;

namespace wfiplib
{
    public class admNuevoVida
    {
        public bool nuevo(nuevoVida pEmision)
        {
            bool resultado = false;
            StringBuilder SqlCmd = new StringBuilder("Insert Into nuevoVida (Id,NumTramite,TipoSeguro,Producto,NumSolCPDES,NumSolicitud,EstatusCPDES,IdTipoContratante,TipoContratante,ApPaterno,ApMaterno,Nombre,FhNacimiento,RFC,CURP)");
            SqlCmd.Append(" Values(");
            SqlCmd.Append(pEmision.Id);
            SqlCmd.Append(",'" + pEmision.NumTramite + "'");
            SqlCmd.Append(",'" + pEmision.TipoSeguro + "'");
            SqlCmd.Append(",'" + pEmision.Producto + "'");
            SqlCmd.Append(",'" + pEmision.NumSolCPDES + "'");
            SqlCmd.Append(",'" + pEmision.NumSolicitud + "'");
            SqlCmd.Append(",'" + pEmision.EstatusCPDES + "'");
            SqlCmd.Append("," + pEmision.IdTipoContratante.ToString());
            SqlCmd.Append(",'" + pEmision.TipoContratante + "'");
            SqlCmd.Append(",'" + pEmision.ApPaterno + "'");
            SqlCmd.Append(",'" + pEmision.ApMaterno + "'");
            SqlCmd.Append(",'" + pEmision.Nombre + "'");
            SqlCmd.Append(",'" + pEmision.FhNacimiento.ToString("yyyyMMdd") + "'");
            SqlCmd.Append(",'" + pEmision.RFC + "'");
            SqlCmd.Append(",'" + pEmision.CURP + "'");
            SqlCmd.Append(")");
            bd BD = new bd();
            resultado = BD.ejecutaCmd(SqlCmd.ToString());
            BD.cierraBD();
            return resultado;
        }

        public nuevoVida carga(int pId)
        {
            nuevoVida resultado = null;
            String SqlCmd = "SELECT * FROM nuevoVida WHERE Id = " + pId.ToString();
            bd BD = new bd();
            DataTable datos = BD.leeDatos(SqlCmd);
            if (datos.Rows.Count > 0) { resultado = arma(datos.Rows[0]); }
            datos.Dispose();
            BD.cierraBD();
            return resultado;
        }

        private nuevoVida arma(DataRow pRegistro)
        {
            nuevoVida resultado = new nuevoVida();
            if (!pRegistro.IsNull("Id")) resultado.Id = Convert.ToInt32(pRegistro["Id"]);
            if (!pRegistro.IsNull("NumTramite")) resultado.NumTramite = Convert.ToString(pRegistro["NumTramite"]);
            if (!pRegistro.IsNull("TipoSeguro")) resultado.TipoSeguro = Convert.ToString(pRegistro["TipoSeguro"]);
            if (!pRegistro.IsNull("Producto")) resultado.Producto = Convert.ToString(pRegistro["Producto"]);
            if (!pRegistro.IsNull("NumSolCPDES")) resultado.NumSolCPDES = Convert.ToString(pRegistro["NumSolCPDES"]);
            if (!pRegistro.IsNull("NumSolicitud")) resultado.NumSolicitud = Convert.ToString(pRegistro["NumSolicitud"]);
            if (!pRegistro.IsNull("EstatusCPDES")) resultado.EstatusCPDES = Convert.ToString(pRegistro["EstatusCPDES"]);
            if (!pRegistro.IsNull("IdTipoContratante")) resultado.IdTipoContratante = Convert.ToInt32(pRegistro["IdTipoContratante"]);
            if (!pRegistro.IsNull("TipoContratante")) resultado.TipoContratante = Convert.ToString(pRegistro["TipoContratante"]);
            if (!pRegistro.IsNull("ApPaterno")) resultado.ApPaterno = Convert.ToString(pRegistro["ApPaterno"]);
            if (!pRegistro.IsNull("ApMaterno")) resultado.ApMaterno = Convert.ToString(pRegistro["ApMaterno"]);
            if (!pRegistro.IsNull("Nombre")) resultado.Nombre = Convert.ToString(pRegistro["Nombre"]);
            if (!pRegistro.IsNull("RFC")) resultado.RFC = Convert.ToString(pRegistro["RFC"]);
            if (!pRegistro.IsNull("CURP")) resultado.CURP = Convert.ToString(pRegistro["CURP"]);
            if (!pRegistro.IsNull("FhNacimiento")) resultado.FhNacimiento = Convert.ToDateTime(pRegistro["FhNacimiento"]);
            return resultado;
        }

        public bool elimina(int pIdTramite)
        {
            bool resultado = false;
            bd BD = new bd();
            resultado = BD.ejecutaCmd("DELETE From nuevoVida WHERE Id=" + pIdTramite.ToString());
            BD.cierraBD();
            return resultado;
        }
    }

    [Serializable]
    public class nuevoVida
    {
        private int mId = 0;
        public int Id { get { return mId; } set { mId = value; } }
        private string mNumTramite = "";
        public string NumTramite { get { return mNumTramite; } set { mNumTramite = value; } }
        private string mTipoSeguro = "";
        public string TipoSeguro { get { return mTipoSeguro; } set { mTipoSeguro = value; } }
        private string mProducto = "";
        public string Producto { get { return mProducto; } set { mProducto = value; } }
        private string mNumSolCPDES = "";
        public string NumSolCPDES { get { return mNumSolCPDES; } set { mNumSolCPDES = value; } }
        private string mNumSolicitud = "";
        public string NumSolicitud { get { return mNumSolicitud; } set { mNumSolicitud = value; } }
        private string mEstatusCPDES = "";
        public string EstatusCPDES { get { return mEstatusCPDES; } set { mEstatusCPDES = value; } }
        private int mIdTipoContratante = 0;
        public int IdTipoContratante { get { return mIdTipoContratante; } set { mIdTipoContratante = value; } }
        private string mTipoContratante = "";
        public string TipoContratante { get { return mTipoContratante; } set { mTipoContratante = value; } }
        private string mApPaterno = "";
        public string ApPaterno { get { return mApPaterno; } set { mApPaterno = value; } }
        private string mApMaterno = "";
        public string ApMaterno { get { return mApMaterno; } set { mApMaterno = value; } }
        private string mNombre = "";
        public string Nombre { get { return mNombre; } set { mNombre = value; } }
        private string mRFC = "";
        public string RFC { get { return mRFC; } set { mRFC = value; } }
        private string mCURP = "";
        public string CURP { get { return mCURP; } set { mCURP = value; } }
        private DateTime mFhNacimiento = new DateTime(1900, 1, 1, 0, 0, 0);
        public DateTime FhNacimiento { get { return mFhNacimiento; } set { mFhNacimiento = value; } }

        private string mContratante = "";
        public string Contratante { get {
            if (string.IsNullOrEmpty(mContratante))
            {
                if (mIdTipoContratante == 1) mContratante = mApPaterno + " " + mApMaterno + " " + mNombre;
                else mContratante = mNombre;
            }
            return mContratante;
        }}

        public string CabeceraHtml { get {
                string cadena = "<table style ='width :100%;border: 1px solid #6387A6;border-radius: 7px 7px 7px 7px;-moz-border-radius: 7px 7px 7px 7px;'>";
                cadena += "<tr><td style='width:150px'>Número trámite:</td><td><b>" + Id.ToString().PadLeft(5, '0') + "</b></td></tr>";
                cadena += "<tr><td>Número de SOLICITUD:</td><td><b>" + NumSolicitud + "</b></td></tr>";
                cadena += "<tr><td>Nombre(s):</td><td><b>" + mNombre + " " + mApPaterno + " " + mApMaterno + "</b></td></tr>";
                cadena += "</table>";
                return cadena;
        }}

        public string DatosHtml { get {
            string cadena = "Número de Trámite: <strong>" + mNumTramite + "</strong><br />";
            cadena += "Tipo de Seguro: <strong>" + mTipoSeguro + "</strong><br />";
            cadena+="Tipo de Producto: <strong>" + mProducto + "</strong><br />";
            cadena+="Número de Solicitud CP DES: <strong>" + mNumSolCPDES + "</strong><br />";
            cadena+="Número de Solicitud: <strong>" + mNumSolicitud + "</strong><hr />";

            cadena+="Tipo de Contratante: <strong>" + mTipoContratante + "</strong><br />";
            cadena+="Nombre del Contratante: <strong>" + Contratante + "</strong><br />";
            cadena+="Fecha de Nacimiento o Constitución:";
            if(!mFhNacimiento.Equals(new DateTime(1900, 1, 1, 0, 0, 0))) { cadena+=" <strong>" + mFhNacimiento + "</strong>"; }
            cadena += "<br />";
            cadena+="RFC: <strong>" + mRFC + "</strong><br />";
            cadena+="CURP: <strong>" + mCURP+ "</strong><br />";
            cadena+="Estatus CPDES: <strong>" + mEstatusCPDES + "</strong><hr />";
            return cadena;
        }}
    }
}
