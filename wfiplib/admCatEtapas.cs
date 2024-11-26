using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace wfiplib
{
    public class admCatEtapas
    {
        public bool nuevo(etapa pDatos)
        {
            bool resultado = false;
            pDatos.IdEtapa = daSiguienteIdEtapaDelFlujo(pDatos.IdFlujo);
            StringBuilder SqlCmd = new StringBuilder("INSERT INTO cat_etapas(IdFlujo,Nombre,IdEtapa,IdUsuario,IdEstado,FechaRegistro) VALUES(");
            SqlCmd.Append(pDatos.IdFlujo.ToString("d"));
            SqlCmd.Append(",'" + pDatos.Nombre + "'");
            SqlCmd.Append("," + pDatos.IdEtapa.ToString());
            SqlCmd.Append("," + pDatos.IdUsuario.ToString());
            SqlCmd.Append("," + pDatos.IdEstado.ToString());
            SqlCmd.Append(",GETDATE()");
            SqlCmd.Append(");");
            bd BD = new bd();
            resultado = BD.ejecutaCmd(SqlCmd.ToString());

            BD.cierraBD();

            return resultado;
        }

        private int daSiguienteIdEtapaDelFlujo(int pIdFujo)
        {
            int resultado = 1;
            string SqlCmd = "Select Max(IdEtapa) + 1 As SiguientePaso From cat_etapas Where IdFlujo = " + pIdFujo.ToString();
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
            DataTable datos = BD.leeDatos("SELECT * FROM cat_etapas WHERE Id=" + pId.ToString());
            if (datos.Rows.Count > 0) { respuesta = arma(datos.Rows[0]); }
            datos.Dispose();
            BD.cierraBD();
            return respuesta;
        }

        public List<etapa> Lista(int pIdFlujo)
        {
            List<etapa> respuesta = new List<etapa>();
            bd BD = new bd();
            DataTable datos = BD.leeDatos("SELECT * FROM vw_cat_etapas WHERE IdFlujo=" + pIdFlujo + " ORDER BY IdEtapa");
            foreach (DataRow reg in datos.Rows) { respuesta.Add(arma(reg)); }
            datos.Dispose();
            BD.cierraBD();
            return respuesta;
        }

        private etapa arma(DataRow pRegistro)
        {
            etapa Resultado = new etapa();
            if (!pRegistro.IsNull("Id")) Resultado.Id = Convert.ToInt32(pRegistro["Id"]);
            if (!pRegistro.IsNull("IdFlujo")) Resultado.IdFlujo = Convert.ToInt32(pRegistro["IdFlujo"]);
            if (!pRegistro.IsNull("Flujo")) Resultado.Flujo = Convert.ToString(pRegistro["Flujo"]);
            if (!pRegistro.IsNull("Nombre")) Resultado.Nombre = Convert.ToString(pRegistro["Nombre"]);
            if (!pRegistro.IsNull("IdEtapa")) Resultado.IdEtapa = Convert.ToInt32(pRegistro["IdEtapa"]);
            if (!pRegistro.IsNull("IdUsuario")) Resultado.IdUsuario = Convert.ToInt32(pRegistro["IdUsuario"]);
            if (!pRegistro.IsNull("Usuario")) Resultado.Usuario = Convert.ToString(pRegistro["Usuario"]);
            if (!pRegistro.IsNull("IdEstado")) Resultado.IdEstado = Convert.ToInt32(pRegistro["IdEstado"]);
            if (!pRegistro.IsNull("Estado")) Resultado.Estado = Convert.ToString(pRegistro["Estado"]);
            if (!pRegistro.IsNull("FechaRegistro")) Resultado.FechaRegistro = Convert.ToDateTime(pRegistro["FechaRegistro"]);
            return Resultado;
        }

        public void activa(string pId)
        {
            bd BD = new bd();
            BD.ejecutaCmd("UPDATE cat_etapas SET IdEstado = " + E_Estado.Activo.ToString("d") + " WHERE Id=" + pId);
            BD.cierraBD();
        }

        public void desactiva(string pId)
        {
            bd BD = new bd();
            BD.ejecutaCmd("UPDATE cat_etapas SET IdEstado = " + E_Estado.Inactivo.ToString("d") + " WHERE Id=" + pId);
            BD.cierraBD();
        }

        public void modifica(etapa pDatos)
        {
            StringBuilder SqlCmd = new StringBuilder("UPDATE cat_etapas SET");
            SqlCmd.Append(" Nombre='" + pDatos.Nombre + "'");
            SqlCmd.Append(",IdEstado=" + pDatos.IdEstado.ToString("d"));
            SqlCmd.Append(" WHERE Id=" + pDatos.Id.ToString());
            bd BD = new bd();
            BD.ejecutaCmd(SqlCmd.ToString());
            BD.cierraBD();
        }

        public int daIdPrimerEtapa(int pIdFlujo)
        {
            int resultado = 0;
            bd BD = new bd();
            DataTable datos = BD.leeDatos("select Id from cat_etapas where IdFlujo=" + pIdFlujo + " AND IdEtapa=1");
            if (datos.Rows.Count > 0) resultado = Convert.ToInt32(datos.Rows[0]["Id"]);
            datos.Dispose();
            BD.cierraBD();
            return resultado;
        }
    }

    public class etapa
    {
        private int mId = 0;
        public int Id { get { return mId; } set { mId = value; } }
        private int mIdFlujo = 0;
        public int IdFlujo { get { return mIdFlujo; } set { mIdFlujo = value; } }
        private string mFlujo = String.Empty;
        public string Flujo { get { return mFlujo; } set { mFlujo = value; } }
        private string mNombre = String.Empty;
        public string Nombre { get { return mNombre; } set { mNombre = value; } }
        private int mIdEtapa = 0;
        public int IdEtapa { get { return mIdEtapa; } set { mIdEtapa = value; } }
        private int mIdUsuario = 0;
        public int IdUsuario { get { return mIdUsuario; } set { mIdUsuario = value; } }
        private string mUsuario = String.Empty;
        public string Usuario { get { return mUsuario; } set { mUsuario = value; } }
        private int mIdEstado = 1;
        public int IdEstado { get { return mIdEstado; } set { mIdEstado = value; } }
        private string mEstado = String.Empty;
        public string Estado { get { return mEstado; } set { mEstado = value; } }
        public E_Estado eEstado { get { return (E_Estado)mIdEstado; } }
        private DateTime mFechaRegistro = new DateTime(2000, 1, 1, 0, 0, 0);
        public DateTime FechaRegistro { get { return mFechaRegistro; } set { mFechaRegistro = value; } }
    }
}
