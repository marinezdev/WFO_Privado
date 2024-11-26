using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace wfiplib
{

    public class admCatPromotoria
    {
        private string mCadenaConexionDB = "";
        BaseDeDatos b = new BaseDeDatos();

        public admCatPromotoria(string CadenaConexionDB)
        {
            mCadenaConexionDB = CadenaConexionDB;
        }
        
        public bool nuevo(Promotoria pDatos)
        {
            bool resultado = false;
            StringBuilder SqlCmd = new StringBuilder("INSERT INTO cat_promotorias (");
            SqlCmd.Append("FechaRegistro");
            SqlCmd.Append(",Nombre");
            SqlCmd.Append(",Rfc");
            SqlCmd.Append(",Clave");
            SqlCmd.Append(",Direccion");
            SqlCmd.Append(",Ciudad");
            SqlCmd.Append(",Cp");
            SqlCmd.Append(",Correo");
            SqlCmd.Append(",Telefono");
            SqlCmd.Append(",Extencion");
            SqlCmd.Append(",Activo");
            SqlCmd.Append(")");

            SqlCmd.Append(" VALUES (");
            SqlCmd.Append("getdate()");
            SqlCmd.Append(",'" + pDatos.Nombre + "'");
            SqlCmd.Append(",'" + pDatos.Rfc + "'");
            SqlCmd.Append(",'" + pDatos.Clave + "'");
            SqlCmd.Append(",'" + pDatos.Direccion + "'");
            SqlCmd.Append(",'" + pDatos.Ciudad + "'");
            SqlCmd.Append(",'" + pDatos.Cp + "'");
            SqlCmd.Append(",'" + pDatos.Correo + "'");
            SqlCmd.Append(",'" + pDatos.Telefono + "'");
            SqlCmd.Append(",'" + pDatos.Extencion + "'");
            SqlCmd.Append("," + pDatos.Activo);
            SqlCmd.Append(");");
            bd BD = new bd();
            resultado = BD.ejecutaCmd(SqlCmd.ToString());

            BD.cierraBD();

            return resultado;
        }

        public Promotoria carga(int pId)
        {
            Promotoria respuesta = new Promotoria();
            bd BD = new bd();
            DataTable datos = BD.leeDatos("SELECT * FROM cat_promotorias WHERE Id=" + pId.ToString());
            if (datos.Rows.Count > 0) { respuesta = arma(datos.Rows[0]); }
            datos.Dispose();
            BD.cierraBD();
            return respuesta;
        }


        public List<Promotoria> ListaPromotorias()
        {
            List<Promotoria> respuesta = new List<Promotoria>();
            bd BD = new bd();
            DataTable datos = BD.leeDatos("SELECT * FROM cat_promotorias order by Nombre");
            foreach (DataRow reg in datos.Rows) { respuesta.Add(arma(reg)); }
            datos.Dispose();
            BD.cierraBD();
            return respuesta;
        }

        private Promotoria arma(DataRow pRegistro)
        {
            Promotoria respuesta = new Promotoria();
            if (!pRegistro.IsNull("Id")) respuesta.Id = Convert.ToInt32(pRegistro["Id"]);
            if (!pRegistro.IsNull("FechaRegistro")) respuesta.FechaRegistro = Convert.ToDateTime(pRegistro["FechaRegistro"]);
            if (!pRegistro.IsNull("Nombre")) respuesta.Nombre = Convert.ToString(pRegistro["Nombre"]);
            if (!pRegistro.IsNull("Rfc")) respuesta.Rfc = Convert.ToString(pRegistro["Rfc"]);
            if (!pRegistro.IsNull("Clave")) respuesta.Clave = Convert.ToString(pRegistro["Clave"]);
            if (!pRegistro.IsNull("Direccion")) respuesta.Direccion = Convert.ToString(pRegistro["Direccion"]);
            if (!pRegistro.IsNull("Ciudad")) respuesta.Ciudad = Convert.ToString(pRegistro["Ciudad"]);
            if (!pRegistro.IsNull("Cp")) respuesta.Cp = Convert.ToString(pRegistro["Cp"]);
            if (!pRegistro.IsNull("Correo")) respuesta.Correo = Convert.ToString(pRegistro["Correo"]);
            if (!pRegistro.IsNull("Telefono")) respuesta.Telefono = Convert.ToString(pRegistro["Telefono"]);
            if (!pRegistro.IsNull("Extencion")) respuesta.Extencion = Convert.ToString(pRegistro["Extencion"]);
            if (!pRegistro.IsNull("Activo")) respuesta.Activo = Convert.ToInt32(pRegistro["Activo"]);

            return respuesta;
        }

        public bool Existe(string pRfc)
        {
            bool resultado = false;
            bd BD = new bd();
            DataTable datos = BD.leeDatos("SELECT * FROM cat_promotorias Where Rfc= '" + pRfc + "'");
            resultado = (datos.Rows.Count > 0);
            datos.Dispose();
            BD.cierraBD();
            return resultado;
        }

        public bool AsignarUsuarioPromotoria(int pIdUsuario, int pIdPromotoria)
        {
            IDbConnection _conn = null;
            IDbTransaction _trans = null;
            wfiplib.dbSQLServer cDatos = new wfiplib.dbSQLServer();
            bool blnResultado = false;

            try
            {
                _conn = cDatos.EstablecerConexion(mCadenaConexionDB);
                _trans = cDatos.getTransaccion(_conn);
                cDatos.EjecutarComando(ref _conn, ref _trans, "DELETE FROM promotoriaUsuario WHERE idUsuario = " + pIdUsuario.ToString() + "; ");
                cDatos.EjecutarComando(ref _conn, ref _trans, "INSERT INTO promotoriaUsuario (idPromotoria, idUsuario) VALUES (" + pIdPromotoria.ToString() + ", " + pIdUsuario.ToString() + ") ; ");
                cDatos.EjecutarComando(ref _conn, ref _trans, "UPDATE USUARIOS SET IdPerfil = 4 WHERE Id = " + pIdUsuario.ToString() + " ; ");
                cDatos.TransaccionCommit(ref _trans);
                blnResultado = true;
            }
            catch (Exception)
            {
                cDatos.TransaccionRollback(ref _trans);
                blnResultado = false;
            }
            finally
            {
                _trans = null;
                _conn = null;
                cDatos = null;
            }

            return blnResultado;
        }

        public void modifica(Promotoria oDt)
        {
            StringBuilder SqlCmd = new StringBuilder("UPDATE cat_promotorias SET");
            SqlCmd.Append(" Nombre='" + oDt.Nombre + "'");
            SqlCmd.Append(",Rfc='" + oDt.Rfc + "'");
            SqlCmd.Append(",Clave='" + oDt.Clave + "'");
            SqlCmd.Append(",Direccion='" + oDt.Direccion + "'");
            SqlCmd.Append(",Ciudad='" + oDt.Ciudad + "'");
            SqlCmd.Append(",Cp='" + oDt.Cp + "'");
            SqlCmd.Append(",Correo='" + oDt.Correo + "'");
            SqlCmd.Append(",Telefono='" + oDt.Telefono + "'");
            SqlCmd.Append(",Extencion='" + oDt.Extencion + "'");
            SqlCmd.Append(",Activo=" + oDt.Activo );
            SqlCmd.Append(" WHERE Id=" + oDt.Id);
            bd BD = new bd();
            BD.ejecutaCmd(SqlCmd.ToString());
            BD.cierraBD();
        }

        public List<CboValorTexto> cargaComboPromotorias()
        {
            List<CboValorTexto> respuesta = new List<CboValorTexto>();
            respuesta.Add(new CboValorTexto("0", "SELECCIONAR"));
            bd BD = new bd();
            DataTable datos = BD.leeDatos("SELECT * FROM cat_promotorias order by Nombre");
            foreach (DataRow reg in datos.Rows) { respuesta.Add(armaVelorTexto(reg)); }
            datos.Dispose();
            BD.cierraBD();
            return respuesta;
        }

        private CboValorTexto armaVelorTexto(DataRow pRegistro)
        {
            CboValorTexto respuesta = new CboValorTexto();
            if (!pRegistro.IsNull("Id")) respuesta.valor = Convert.ToString(pRegistro["Id"]);
            if (!pRegistro.IsNull("Nombre")) respuesta.texto = Convert.ToString(pRegistro["Nombre"]);
            return respuesta;
        }

        public int daIdPromotoria(int pClave)
        {
            int resultado = 0;
            bd BD = new bd();
            DataTable datos = BD.leeDatos("Select Id From cat_promotorias Where Clave=" + pClave.ToString());
            if(datos.Rows.Count > 0) resultado = Convert.ToInt32(datos.Rows[0][0]);
            datos.Dispose();
            BD.cierraBD();
            return resultado;
        }

        public string daNombre(int pId)
        {
            //string resultado = "";
            //bd BD = new bd();
            //DataTable datos = BD.leeDatos("Select Nombre From cat_promotorias Where Id=" + pId.ToString());
            //if (datos.Rows.Count > 0) resultado = Convert.ToString(datos.Rows[0][0]);
            //datos.Dispose();
            //BD.cierraBD();
            //return resultado;

            string consulta = "SELECT Nombre FROM cat_promotorias WHERE Id=@pId";
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@pId", pId, SqlDbType.Int);
            if (b.Select().Rows.Count > 0)
                return b.SelectString();
            else return "";
        }

        /// <summary>
        /// Obtiene las promotorías para mostrarlas en una lista desplegable
        /// </summary>
        /// <returns>DataTable con los registros obtenidos</returns>
        public DataTable Seleccionar()
        {
            b.ExecuteCommandQuery("SELECT Id, clave + '-' + nombre AS Nombre FROM cat_promotorias");
            return b.Select();
        }

    }

    public class admCatPromotorias
    {
        public DataTable Promotorias_Selecionar()
        {
            DataTable dt = new DataTable();
            bd ExportarExcel = new bd();
            string qExportarExcel = "EXEC Promotorias_Selecionar ";
            dt = ExportarExcel.leeDatos(qExportarExcel);
            ExportarExcel.cierraBD();
            return dt;
        }

        public DataTable Tramites_Selecionar_PorPromotorias(DateTime fechaDesde, DateTime fechaHasta, string promotorias)
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
                "EXECUTE Tramites_Selecionar_PorPromotorias @FECHAINI, @FECHAFIN, '" + promotorias +"'";
            dt = data.leeDatos(q);
            data.cierraBD();
            return dt;
        }
    }

    public class Promotoria
    {
        private int mId = 0;
        public int Id { get { return mId; } set { mId = value; } }
        private DateTime mFechaRegistro = new DateTime(2000, 1, 1, 0, 0, 0);
        public DateTime FechaRegistro { get { return mFechaRegistro; } set { mFechaRegistro = value; } }
        private string mNombre = String.Empty;
        public string Nombre { get { return mNombre; } set { mNombre = value; } }
        private string mRfc = String.Empty;
        public string Rfc { get { return mRfc; } set { mRfc = value; } }
        private string mClave = String.Empty;
        public string Clave { get { return mClave; } set { mClave = value; } }
        private string mDireccion = String.Empty;
        public string Direccion { get { return mDireccion; } set { mDireccion = value; } }
        private string mCiudad = String.Empty;
        public string Ciudad { get { return mCiudad; } set { mCiudad = value; } }
        private string mCp = String.Empty;
        public string Cp { get { return mCp; } set { mCp = value; } }
        private string mCorreo = String.Empty;
        public string Correo { get { return mCorreo; } set { mCorreo = value; } }
        private string mTelefono = String.Empty;
        public string Telefono { get { return mTelefono; } set { mTelefono = value; } }
        private string mExtencion = String.Empty;
        public string Extencion { get { return mExtencion; } set { mExtencion = value; } }
        private int mActivo = 1;
        public int Activo { get { return mActivo; } set { mActivo = value; } }
    }
}
