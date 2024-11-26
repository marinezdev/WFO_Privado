using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace wfiplib
{
    public class admCatPaso
    {
        public bool nuevo(etapa pDatos)
        {
            bool resultado = false;
            pDatos.IdEtapa = daSiguienteIdPasoDelFlujo(pDatos.IdFlujo);
            pDatos.Nombre = "PASO " + pDatos.IdEtapa.ToString();
            StringBuilder SqlCmd = new StringBuilder("INSERT INTO cat_paso(Descripcion,IdFlujo,Paso,Estado) VALUES(");
            SqlCmd.Append("'" + pDatos.Nombre + "'");
            SqlCmd.Append("," + pDatos.IdFlujo.ToString("d"));
            SqlCmd.Append("," + pDatos.IdEtapa.ToString());
            SqlCmd.Append("," + pDatos.Estado.ToString("d"));
            SqlCmd.Append(");");
            bd BD = new bd();
            resultado = BD.ejecutaCmd(SqlCmd.ToString());

            BD.cierraBD();

            return resultado;
        }

        private int daSiguienteIdPasoDelFlujo(int pIdFujo)
        {
            int resultado = 1;
            string SqlCmd = "Select Max(Paso) + 1 As SiguientePaso From cat_paso Where IdFlujo = " + pIdFujo.ToString();
            bd BD = new bd();
            DataTable datos = BD.leeDatos(SqlCmd);
            if (datos.Rows.Count > 0) if (!datos.Rows[0].IsNull(0)) resultado = Convert.ToInt32(datos.Rows[0][0]);
            datos.Dispose();
            BD.cierraBD();
            return resultado;
        }

        public etapa carga(int pId)
        {
            etapa respuesta = new etapa();
            bd BD = new bd();
            DataTable datos = BD.leeDatos("SELECT * FROM cat_paso WHERE Id=" + pId.ToString());
            if (datos.Rows.Count > 0) { respuesta = arma(datos.Rows[0]); }
            datos.Dispose();
            BD.cierraBD();
            return respuesta;
        }

        public List<etapa> Lista(int pIdFlujo)
        {
            List<etapa> respuesta = new List<etapa>();
            bd BD = new bd();
            DataTable datos = BD.leeDatos("SELECT * FROM cat_paso WHERE IdFlujo=" + pIdFlujo + " ORDER BY Paso");
            foreach (DataRow reg in datos.Rows) { respuesta.Add(arma(reg)); }
            datos.Dispose();
            BD.cierraBD();
            return respuesta;
        }

        private etapa arma(DataRow pRegistro)
        {
            etapa respuesta = new etapa();
            if (!pRegistro.IsNull("Id")) respuesta.Id = Convert.ToInt32(pRegistro["Id"]);
            if (!pRegistro.IsNull("IdFlujo")) respuesta.IdFlujo = Convert.ToInt32(pRegistro["IdFlujo"]);
            if (!pRegistro.IsNull("Descripcion")) respuesta.Nombre = Convert.ToString(pRegistro["Descripcion"]);
            if (!pRegistro.IsNull("Paso")) respuesta.IdEtapa = Convert.ToInt32(pRegistro["Paso"]);
            if (!pRegistro.IsNull("Estado")) respuesta.Estado = (e_Estado)(pRegistro["Estado"]);
            return respuesta;
        }

        public void activa(string pId)
        {
            bd BD = new bd();
            BD.ejecutaCmd("UPDATE cat_paso SET Estado = " + e_Estado.Activo.ToString("d") + " WHERE Id=" + pId);
            BD.cierraBD();
        }

        public void desactiva(string pId)
        {
            bd BD = new bd();
            BD.ejecutaCmd("UPDATE cat_paso SET Estado = " + e_Estado.Inactivo.ToString("d") + " WHERE Id=" + pId);
            BD.cierraBD();
        }

        public void modifica(etapa pDatos)
        {
            StringBuilder SqlCmd = new StringBuilder("UPDATE cat_paso SET");
            SqlCmd.Append(" Descripcion='" + pDatos.Nombre+ "'");
            SqlCmd.Append(" WHERE Id=" + pDatos.Id.ToString());
            bd BD = new bd();
            BD.ejecutaCmd(SqlCmd.ToString());
            BD.cierraBD();
        }
    }

    //public class etapa
    //{
    //    private int mId = 0;
    //    public int Id { get { return mId; } set { mId = value; } }
    //    private int mIdFlujo = 0;
    //    public int IdFlujo { get { return mIdFlujo; } set { mIdFlujo = value; } }
    //    private string mDescripcion = String.Empty;
    //    public string Descripcion { get { return mDescripcion; } set { mDescripcion = value; } }
    //    private int mPaso = 0;
    //    public int Paso { get { return mPaso; } set { mPaso = value; } }
    //    private e_Estado mEstado = e_Estado.Activo;
    //    public e_Estado Estado { get { return mEstado; } set { mEstado = value; } }
    //}
}
