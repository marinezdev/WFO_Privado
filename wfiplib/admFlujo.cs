using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace wfiplib
{
    public class admFlujo
    {
        public bool nuevo(flujo pDatos)
        {
            bool resultado = false;
            StringBuilder SqlCmd = new StringBuilder("INSERT INTO flujo (");
            SqlCmd.Append("Nombre");
            SqlCmd.Append(",IdEstado");
            SqlCmd.Append(",IdUsuario");
            SqlCmd.Append(",FechaRegistro");
            SqlCmd.Append(")");
            SqlCmd.Append(" VALUES (");
            SqlCmd.Append("'" + pDatos.Nombre + "'");
            SqlCmd.Append("," + pDatos.IdEstado.ToString("d"));
            SqlCmd.Append("," + pDatos.IdUsuario.ToString("d"));
            SqlCmd.Append(",GETDATE()");
            SqlCmd.Append(");");
            bd BD = new bd();
            resultado = BD.ejecutaCmd(SqlCmd.ToString());

            BD.cierraBD();
            return resultado;
        }

        public flujo carga(int pId)
        {
            flujo respuesta = new flujo();
            bd BD = new bd();
            DataTable datos = BD.leeDatos("SELECT * FROM vw_flujo WHERE Id=" + pId.ToString());
            if (datos.Rows.Count > 0) { respuesta = arma(datos.Rows[0]); }
            datos.Dispose();
            BD.cierraBD();
            return respuesta;
        }

        public List<flujo> Lista()
        {
            List<flujo> respuesta = new List<flujo>();
            bd BD = new bd();
            DataTable datos = BD.leeDatos("SELECT * FROM vw_flujo order by Nombre");
            foreach (DataRow reg in datos.Rows) { respuesta.Add(arma(reg)); }
            datos.Dispose();
            BD.cierraBD();
            return respuesta;
        }

        public List<flujo> ListaCbo()
        {
            List<flujo> respuesta = new List<flujo>();
            flujo oInicial = new flujo();
            oInicial.Id = 0;
            oInicial.Nombre = "SELECCIONAR";
            respuesta.Add(oInicial);

            bd BD = new bd();
            DataTable datos = BD.leeDatos("SELECT * FROM vw_flujo order by Nombre");
            foreach (DataRow reg in datos.Rows) { respuesta.Add(arma(reg)); }
            datos.Dispose();
            BD.cierraBD();
            return respuesta;
        }

        private flujo arma(DataRow pRegistro)
        {
            flujo Resultado= new flujo();
            if (!pRegistro.IsNull("Id")) Resultado.Id = Convert.ToInt32(pRegistro["Id"]);
            if (!pRegistro.IsNull("Nombre")) Resultado.Nombre = Convert.ToString(pRegistro["Nombre"]);
            if (!pRegistro.IsNull("IdEstado")) Resultado.IdEstado = Convert.ToInt32(pRegistro["IdEstado"]);
            if (!pRegistro.IsNull("Estado")) Resultado.Estado = Convert.ToString(pRegistro["Estado"]);
            if (!pRegistro.IsNull("IdUsuario")) Resultado.IdUsuario = Convert.ToInt32(pRegistro["IdUsuario"]);
            if (!pRegistro.IsNull("Usuario")) Resultado.Usuario = Convert.ToString(pRegistro["Usuario"]);
            if (!pRegistro.IsNull("FechaRegistro")) Resultado.FechaRegistro = Convert.ToDateTime(pRegistro["FechaRegistro"]);
            return Resultado;
        }

        public bool Existe(string pNombre)
        {
            bool resultado = false;
            bd BD = new bd();
            DataTable datos = BD.leeDatos("SELECT * FROM flujo Where Nombre='" + pNombre + "'");
            resultado = (datos.Rows.Count > 0);
            datos.Dispose();
            BD.cierraBD();
            return resultado;
        }

        public void modifica_ant(int Id, string pNombre)
        {
            StringBuilder SqlCmd = new StringBuilder("UPDATE flujo SET");
            SqlCmd.Append(" Nombre='" + pNombre + "'");
            SqlCmd.Append(" WHERE Id=" + Id);
            bd BD = new bd();
            BD.ejecutaCmd(SqlCmd.ToString());
            BD.cierraBD();
        }

        public void modifica(flujo pDatos)
        {
            StringBuilder SqlCmd = new StringBuilder("UPDATE flujo SET");
            SqlCmd.Append(" Nombre='" + pDatos.Nombre + "'");
            SqlCmd.Append(",IdEstado=" + pDatos.IdEstado.ToString("d"));
            SqlCmd.Append(" WHERE Id=" + pDatos.Id.ToString());
            bd BD = new bd();
            BD.ejecutaCmd(SqlCmd.ToString());
            BD.cierraBD();
        }

        public string daNombre(int pId)
        {
            string resultado = "";
            bd BD = new bd();
            DataTable datos = BD.leeDatos("SELECT Nombre FROM flujo Where Id=" + pId.ToString());
            if (datos.Rows.Count > 0) resultado = Convert.ToString(datos.Rows[0][0]);
            datos.Dispose();
            BD.cierraBD();
            return resultado;
        }

        public int daModo(int pId)
        {
            int resultado = 0;
            bd BD = new bd();
            DataTable datos = BD.leeDatos("SELECT Automatico FROM flujo Where Id=" + pId.ToString());
            if (datos.Rows.Count > 0) if (!datos.Rows[0].IsNull("Automatico")) resultado = Convert.ToInt32(datos.Rows[0][0]);
            datos.Dispose();
            BD.cierraBD();
            return resultado;
        }

        public void cambiaModo(int pId, int pModo)
        {
            StringBuilder SqlCmd = new StringBuilder("UPDATE flujo SET");
            SqlCmd.Append(" Automatico=" + pModo.ToString());
            SqlCmd.Append(" WHERE Id=" + pId);
            bd BD = new bd();
            BD.ejecutaCmd(SqlCmd.ToString());
            BD.cierraBD();
        }
    }

    public class flujo
    {
        private int mId = 0;
        public int Id { get { return mId; } set { mId = value; } }
        private string mNombre = String.Empty;
        public string Nombre { get { return mNombre; } set { mNombre = value; } }
        private int mIdEstado = 1;
        public int IdEstado { get { return mIdEstado; } set { mIdEstado = value; } }
        private string mEstado = String.Empty;
        public string Estado { get { return mEstado; } set { mEstado = value; } }
        public E_Estado eEstado { get { return (E_Estado)mIdEstado; } }
        private int mIdUsuario = 0;
        public int IdUsuario { get { return mIdUsuario; } set { mIdUsuario = value; } }
        private string mUsuario = String.Empty;
        public string Usuario { get { return mUsuario; } set { mUsuario = value; } }
        private DateTime mFechaRegistro = new DateTime(2000, 1, 1, 0, 0, 0);
        public DateTime FechaRegistro { get { return mFechaRegistro; } set { mFechaRegistro = value; } }
    }
}
