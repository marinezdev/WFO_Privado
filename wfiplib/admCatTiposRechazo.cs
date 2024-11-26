using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace wfiplib
{
    public class admCatTiposRechazo
    {
        public bool nuevo( catTiposRechazo pDatos)
        {
            bool resultado = false;
            StringBuilder SqlCmd = new StringBuilder("INSERT INTO cat_TiposRechazo (");
            SqlCmd.Append("FechaRegistro");
            SqlCmd.Append(",Nombre");
            SqlCmd.Append(")");

            SqlCmd.Append(" VALUES (");
            SqlCmd.Append("getdate()");
            SqlCmd.Append(",'" + pDatos.Nombre + "'");
            SqlCmd.Append(");");
            bd BD = new bd();
            resultado = BD.ejecutaCmd(SqlCmd.ToString());

            BD.cierraBD();

            return resultado;
        }
        private catTiposRechazo arma(DataRow pRegistro)
        {
            catTiposRechazo respuesta = new catTiposRechazo();
            if (!pRegistro.IsNull("Id")) respuesta.Id = Convert.ToInt32(pRegistro["Id"]);
            if (!pRegistro.IsNull("FechaRegistro")) respuesta.FechaRegistro = Convert.ToDateTime(pRegistro["FechaRegistro"]);
            if (!pRegistro.IsNull("Nombre")) respuesta.Nombre = Convert.ToString(pRegistro["Nombre"]);
            return respuesta;
        }

        public List<catTiposRechazo> Lista()
        {
            List<catTiposRechazo> respuesta = new List<catTiposRechazo>();
            bd BD = new bd();
            DataTable datos = BD.leeDatos("SELECT * FROM cat_TiposRechazo order by Id");
            foreach (DataRow reg in datos.Rows) { respuesta.Add(arma(reg)); }
            datos.Dispose();
            BD.cierraBD();
            return respuesta;
        }

        public List<catTiposRechazo> ListaCbo()
        {
            List<catTiposRechazo> respuesta = new List<catTiposRechazo>();
            catTiposRechazo oInicial = new catTiposRechazo();
            oInicial.Id = 0;
            oInicial.Nombre = "SELECCIONAR";
            respuesta.Add(oInicial);

            bd BD = new bd();
            DataTable datos = BD.leeDatos("SELECT * FROM cat_TiposRechazo order by Id");
            foreach (DataRow reg in datos.Rows) { respuesta.Add(arma(reg)); }
            datos.Dispose();
            BD.cierraBD();
            return respuesta;
        }
    }
    
    public class catTiposRechazo {
        private int mId = 0;
        public int Id { get { return mId; } set { mId = value; } }
        private DateTime mFechaRegistro = new DateTime(2000, 1, 1, 0, 0, 0);
        public DateTime FechaRegistro { get { return mFechaRegistro; } set { mFechaRegistro = value; } }
        private string mNombre = String.Empty;
        public string Nombre { get { return mNombre; } set { mNombre = value; } }
    }
}
