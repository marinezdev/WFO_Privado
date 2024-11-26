using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace wfiplib
{
    public class admCatAreas
    {
        public bool nuevo(Area pDatos)
        {
            bool resultado = false;
            StringBuilder SqlCmd = new StringBuilder("INSERT INTO cat_areas (");
            SqlCmd.Append("FechaRegistro");
            SqlCmd.Append(",Nombre");
            SqlCmd.Append(",Activo");
            SqlCmd.Append(")");

            SqlCmd.Append(" VALUES (");
            SqlCmd.Append("getdate()");
            SqlCmd.Append(",'" + pDatos.Nombre + "'");
            SqlCmd.Append("," + pDatos.Activo);
            SqlCmd.Append(");");
            bd BD = new bd();
            resultado = BD.ejecutaCmd(SqlCmd.ToString());

            BD.cierraBD();

            return resultado;
        }

        public Area carga(int pId)
        {
            Area respuesta = new Area();
            bd BD = new bd();
            DataTable datos = BD.leeDatos("SELECT * FROM cat_areas WHERE Id=" + pId.ToString());
            if (datos.Rows.Count > 0) { respuesta = arma(datos.Rows[0]); }
            datos.Dispose();
            BD.cierraBD();
            return respuesta;
        }

        public List<Area> ListaAreas()
        {
            List<Area> respuesta = new List<Area>();
            bd BD = new bd();
            DataTable datos = BD.leeDatos("SELECT * FROM cat_areas order by Nombre");
            foreach (DataRow reg in datos.Rows) { respuesta.Add(arma(reg)); }
            datos.Dispose();
            BD.cierraBD();
            return respuesta;
        }

        private Area arma(DataRow pRegistro)
        {
            Area respuesta = new Area();
            if (!pRegistro.IsNull("Id")) respuesta.Id = Convert.ToInt32(pRegistro["Id"]);
            if (!pRegistro.IsNull("FechaRegistro")) respuesta.FechaRegistro = Convert.ToDateTime(pRegistro["FechaRegistro"]);
            if (!pRegistro.IsNull("Nombre")) respuesta.Nombre = Convert.ToString(pRegistro["Nombre"]);
            if (!pRegistro.IsNull("Activo")) respuesta.Activo = Convert.ToInt32(pRegistro["Activo"]);

            return respuesta;
        }

        public bool Existe(string pNombre)
        {
            bool resultado = false;
            bd BD = new bd();
            DataTable datos = BD.leeDatos("SELECT * FROM cat_areas Where Nombre= '" + pNombre + "'");
            resultado = (datos.Rows.Count > 0);
            datos.Dispose();
            BD.cierraBD();
            return resultado;
        }

        public void modifica(Area oDt)
        {
            StringBuilder SqlCmd = new StringBuilder("UPDATE cat_areas SET");
            SqlCmd.Append(" Nombre='" + oDt.Nombre + "'");
            SqlCmd.Append(",Activo=" + oDt.Activo);
            SqlCmd.Append(" WHERE Id=" + oDt.Id);
            bd BD = new bd();
            BD.ejecutaCmd(SqlCmd.ToString());
            BD.cierraBD();
        }
    }

    public class Area
    {
        private int mId = 0;
        public int Id { get { return mId; } set { mId = value; } }
        private DateTime mFechaRegistro = new DateTime(2000, 1, 1, 0, 0, 0);
        public DateTime FechaRegistro { get { return mFechaRegistro; } set { mFechaRegistro = value; } }
        private string mNombre = String.Empty;
        public string Nombre { get { return mNombre; } set { mNombre = value; } }
        private int mActivo = 1;
        public int Activo { get { return mActivo; } set { mActivo = value; } }
    }
}
