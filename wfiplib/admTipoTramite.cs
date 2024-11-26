using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace wfiplib
{
    public class admTipoTramite
    {

        public bool nuevo(TipoTramite pDatos)
        {
            bool resultado = false;
            StringBuilder SqlCmd = new StringBuilder("INSERT INTO tipoTramite (");
            SqlCmd.Append("IdFlujo");
            SqlCmd.Append(",Nombre");
            SqlCmd.Append(",FechaRegistro");
            SqlCmd.Append(",IdUsuario");
            SqlCmd.Append(",IdEstado");
            SqlCmd.Append(",Tabla");
            SqlCmd.Append(")");
            SqlCmd.Append(" VALUES (");
            SqlCmd.Append(pDatos.IdFlujo.ToString());
            SqlCmd.Append(",'" + pDatos.Nombre + "'");
            SqlCmd.Append(",GETDATE()");
            SqlCmd.Append("," + pDatos.IdUsuario.ToString("d"));
            SqlCmd.Append("," + pDatos.IdEstado.ToString("d"));
            SqlCmd.Append(",'" + pDatos.Tabla + "'");
            SqlCmd.Append(");");
            bd BD = new bd();
            resultado = BD.ejecutaCmd(SqlCmd.ToString());

            BD.cierraBD();
            return resultado;
        }

        public TipoTramite carga(int pIdFlujo, E_TipoTramite pIdTipoTramite)
        {
            TipoTramite respuesta = new TipoTramite();
            bd BD = new bd();
            string SqlCmd = "SELECT * FROM tipoTramite WHERE IdFlujo=" + pIdFlujo.ToString();
            SqlCmd += "  and IdTipoTramite=" + pIdTipoTramite.ToString("d");
            DataTable datos = BD.leeDatos(SqlCmd);
            if (datos.Rows.Count > 0) { respuesta = arma(datos.Rows[0]); }
            datos.Dispose();
            BD.cierraBD();
            return respuesta;
        }

        public string DaNombre(E_TipoTramite pIdTipoTramite)
        {
            string  respuesta = string .Empty;
            bd BD = new bd();
            string SqlCmd = "SELECT Flujo + '-' + Nombre FROM vw_tipoTramite WHERE Id=" + pIdTipoTramite.ToString("d");
            DataTable datos = BD.leeDatos(SqlCmd);
            if (datos.Rows.Count > 0) { respuesta = Convert.ToString (datos.Rows[0][0]); }
            datos.Dispose();
            BD.cierraBD();
            return respuesta;
        }

        private TipoTramite arma(DataRow pRegistro)
        {
            TipoTramite Resultado = new TipoTramite();
            if (!pRegistro.IsNull("Id")) Resultado.Id = Convert.ToInt32(pRegistro["Id"]);
            if (!pRegistro.IsNull("IdFlujo")) Resultado.IdFlujo = Convert.ToInt32(pRegistro["IdFlujo"]);
            if (!pRegistro.IsNull("Flujo")) Resultado.Flujo = Convert.ToString(pRegistro["Flujo"]);
            if (!pRegistro.IsNull("Nombre")) Resultado.Nombre = Convert.ToString(pRegistro["Nombre"]);
            if (!pRegistro.IsNull("IdEstado")) Resultado.IdEstado = Convert.ToInt32(pRegistro["IdEstado"]);
            if (!pRegistro.IsNull("Estado")) Resultado.Estado = Convert.ToString(pRegistro["Estado"]);
            if (!pRegistro.IsNull("IdUsuario")) Resultado.IdUsuario = Convert.ToInt32(pRegistro["IdUsuario"]);
            if (!pRegistro.IsNull("Usuario")) Resultado.Usuario = Convert.ToString(pRegistro["Usuario"]);
            if (!pRegistro.IsNull("FechaRegistro")) Resultado.FechaRegistro = Convert.ToDateTime(pRegistro["FechaRegistro"]);
            if (!pRegistro.IsNull("Tabla")) Resultado.Tabla = Convert.ToString(pRegistro["Tabla"]);
            return Resultado;
        }

        public List<TipoTramite> ListaCbo(int pIdFlujo)
        {
            List<TipoTramite> respuesta = new List<TipoTramite>();
            TipoTramite oInicial = new TipoTramite();
            oInicial.Id = 0;
            oInicial.Nombre = "TODOS";
            respuesta.Add(oInicial);

            bd BD = new bd();
            DataTable datos = BD.leeDatos("SELECT * FROM vw_tipoTramite WHERE IdFlujo=" + pIdFlujo + " ORDER BY Nombre");
            foreach (DataRow reg in datos.Rows) { respuesta.Add(arma(reg)); }
            datos.Dispose();
            BD.cierraBD();
            return respuesta;
        }

        public List<TipoTramite> Lista(int pIdFlujo)
        {
            List<TipoTramite> respuesta = new List<TipoTramite>();
            bd BD = new bd();
            DataTable datos = BD.leeDatos("SELECT * FROM vw_tipoTramite WHERE IdFlujo=" + pIdFlujo + " ORDER BY Nombre");
            foreach (DataRow reg in datos.Rows) { respuesta.Add(arma(reg)); }
            datos.Dispose();
            BD.cierraBD();
            return respuesta;
        }

        public void modifica(TipoTramite pDatos)
        {
            StringBuilder SqlCmd = new StringBuilder("Update tipoTramite");
            SqlCmd.Append(" Set Nombre='" + pDatos.Nombre + "'");
            SqlCmd.Append(",IdEstado=" + pDatos.IdEstado.ToString("d"));
            SqlCmd.Append(",Tabla='" + pDatos.Tabla + "'");
            SqlCmd.Append(" Where Id=" + pDatos.Id .ToString());
            bd BD = new bd();
            BD.ejecutaCmd(SqlCmd.ToString());
            BD.cierraBD();
        }
    }

    public class TipoTramite {
        private int mId = 0;
        public int Id { get { return mId; } set { mId = value; } }
        private int mIdFlujo = 0;
        public int IdFlujo { get { return mIdFlujo; } set { mIdFlujo = value; } }
        private string mFlujo = String.Empty;
        public string Flujo { get { return mFlujo; } set { mFlujo = value; } }
        private string mNombre = String.Empty;
        public string Nombre { get { return mNombre; } set { mNombre = value; } }
        private int mIdUsuario = 0;
        public int IdUsuario { get { return mIdUsuario; } set { mIdUsuario = value; } }
        private string mUsuario = String.Empty;
        public string Usuario { get { return mUsuario; } set { mUsuario = value; } }
        private int mIdEstado = 1;
        public int IdEstado { get { return mIdEstado; } set { mIdEstado = value; } }
        private string mEstado = String.Empty;
        public string Estado { get { return mEstado; } set { mEstado = value; } }
        public E_Estado eEstado { get { return (E_Estado)mIdEstado; } }
        private string mTabla = String.Empty;
        public string Tabla { get { return mTabla; } set { mTabla = value; } }
        private DateTime mFechaRegistro = new DateTime(2000, 1, 1, 0, 0, 0);
        public DateTime FechaRegistro { get { return mFechaRegistro; } set { mFechaRegistro = value; } }

        public static object Text { get; set; }
    }
}
