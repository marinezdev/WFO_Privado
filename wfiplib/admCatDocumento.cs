using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace wfiplib
{
    public class admCatDocumento
    {
        public bool nuevo(catDocumento pDatos)
        {
            bool resultado = false;
            StringBuilder SqlCmd = new StringBuilder("INSERT INTO cat_documento (");
            SqlCmd.Append("Nombre");
            SqlCmd.Append(")");

            SqlCmd.Append(" VALUES (");
            SqlCmd.Append("'" + pDatos.Nombre + "'");
            SqlCmd.Append(");");
            bd BD = new bd();
            resultado = BD.ejecutaCmd(SqlCmd.ToString());

            BD.cierraBD();

            return resultado;
        }

        public catDocumento carga(int pIdTipoDocto)
        {
            catDocumento respuesta = new catDocumento();
            bd BD = new bd();
            DataTable datos = BD.leeDatos("SELECT * FROM cat_documento WHERE IdTipoDocto=" + pIdTipoDocto.ToString());
            if (datos.Rows.Count > 0) { respuesta = arma(datos.Rows[0]); }
            datos.Dispose();
            BD.cierraBD();
            return respuesta;
        }

        public List<catDocumento> ListaDocumentos()
        {
            List<catDocumento> respuesta = new List<catDocumento>();
            bd BD = new bd();
            DataTable datos = BD.leeDatos("SELECT * FROM cat_documento order by Nombre");
            foreach (DataRow reg in datos.Rows) { respuesta.Add(arma(reg)); }
            datos.Dispose();
            BD.cierraBD();
            return respuesta;
        }

        private catDocumento arma(DataRow pRegistro)
        {
            catDocumento  respuesta = new catDocumento ();
            if (!pRegistro.IsNull("IdTipoDocto")) respuesta.IdTipoDocto = Convert.ToInt32(pRegistro["IdTipoDocto"]);
            if (!pRegistro.IsNull("Nombre")) respuesta.Nombre = Convert.ToString(pRegistro["Nombre"]);
            return respuesta;
        }

        public bool Existe(string pNombre)
        {
            bool resultado = false;
            bd BD = new bd();
            DataTable datos = BD.leeDatos("SELECT * FROM cat_documento Where Nombre= '" + pNombre + "'");
            resultado = (datos.Rows.Count > 0);
            datos.Dispose();
            BD.cierraBD();
            return resultado;
        }

        public void modifica(catDocumento  oDt)
        {
            StringBuilder SqlCmd = new StringBuilder("UPDATE cat_documento SET");
            SqlCmd.Append(" Nombre='" + oDt.Nombre + "'");
            SqlCmd.Append(" WHERE IdTipoDocto=" + oDt.IdTipoDocto);
            bd BD = new bd();
            BD.ejecutaCmd(SqlCmd.ToString());
            BD.cierraBD();
        }
    }

    public class catDocumento
    {
        private int mIdTipoDocto = 0;
        public int IdTipoDocto { get { return mIdTipoDocto; } set { mIdTipoDocto = value; } }
        private string mNombre = "";
        public string Nombre { get { return mNombre; } set { mNombre = value; } }
    }
}
