using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace wfiplib
{
    public class admCatAgentes
    {
        public bool agregarAgentes(string datos)
        {
            bool resultado = false;
                string[] campos = datos.Split(',');
                string clave = campos[0].Trim();
                string qInsertaAgente = "IF NOT EXISTS(" +
                                       "SELECT CLAVE FROM AgentesPromotoria WHERE LTRIM(RTRIM(CLAVE))='" + clave + "') " +
                                       "BEGIN " +
                                       "INSERT INTO AgentesPromotoria VALUES('" +
                                        campos[0] + "','" + campos[1] + "','" +
                                        campos[3] + "','" + campos[8] + "','" +
                                        campos[9] + "','" + campos[5] + "','" +
                                        campos[6] + "') " +
                                       "END";

                bd BD = new bd();
                resultado = BD.ejecutaCmd(qInsertaAgente);
                BD.cierraBD();
            
            return resultado;

        }
        public bool agregarPromotorias(string datos)
        {
           
                bool resultado = false;
                string[] campos = datos.Split(',');
                string claveP = campos[8].Trim();
                string qInsertaPromotoria = "IF NOT EXISTS(" +
                                       "SELECT Clave FROM cat_promotorias WHERE LTRIM(RTRIM(CLAVE))='" + claveP + "') " +
                                       "BEGIN " +
                                       "INSERT INTO cat_promotorias (Nombre,Rfc,Clave,Activo,clave_region,clave_ejecutivo,ejecutivo,clave_gerente)" +
                                       " VALUES('" +
                                        campos[9] + "','" + campos[4] + "','" + campos[8] + "','" + 1 + "','000','000','" + campos[1] + "','000') " +
                                       "END";
                bd BD = new bd();
                resultado = BD.ejecutaCmd(qInsertaPromotoria);
                BD.cierraBD();
            return resultado;
        }
        public bool nuevo(Agente pDatos)
        {
            bool resultado = false;
            StringBuilder SqlCmd = new StringBuilder("INSERT INTO cat_agentes (");
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

        public Agente carga(int pId)
        {
            Agente respuesta = new Agente();
            bd BD = new bd();
            DataTable datos = BD.leeDatos("SELECT * FROM cat_agentes WHERE Id=" + pId.ToString());
            if (datos.Rows.Count > 0) { respuesta = arma(datos.Rows[0]); }
            datos.Dispose();
            BD.cierraBD();
            return respuesta;
        }

        public List<Agente> ListaAgentes()
        {
            List<Agente> respuesta = new List<Agente>();
            bd BD = new bd();
            DataTable datos = BD.leeDatos("SELECT * FROM cat_agentes order by Nombre");
            foreach (DataRow reg in datos.Rows) { respuesta.Add(arma(reg)); }
            datos.Dispose();
            BD.cierraBD();
            return respuesta;
        }

        private Agente arma(DataRow pRegistro)
        {
            Agente respuesta = new Agente();
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
            DataTable datos = BD.leeDatos("SELECT * FROM cat_agentes Where Rfc= '" + pRfc + "'");
            resultado = (datos.Rows.Count > 0);
            datos.Dispose();
            BD.cierraBD();
            return resultado;
        }

        public void modifica(Agente oDt)
        {
            StringBuilder SqlCmd = new StringBuilder("UPDATE cat_agentes SET");
            SqlCmd.Append(" Nombre='" + oDt.Nombre + "'");
            SqlCmd.Append(",Rfc='" + oDt.Rfc + "'");
            SqlCmd.Append(",Clave='" + oDt.Clave + "'");
            SqlCmd.Append(",Direccion='" + oDt.Direccion + "'");
            SqlCmd.Append(",Ciudad='" + oDt.Ciudad + "'");
            SqlCmd.Append(",Cp='" + oDt.Cp + "'");
            SqlCmd.Append(",Correo='" + oDt.Correo + "'");
            SqlCmd.Append(",Telefono='" + oDt.Telefono + "'");
            SqlCmd.Append(",Extencion='" + oDt.Extencion + "'");
            SqlCmd.Append(" WHERE Id=" + oDt.Id);
            bd BD = new bd();
            BD.ejecutaCmd(SqlCmd.ToString());
            BD.cierraBD();
        }
    }

    public class Agente
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
