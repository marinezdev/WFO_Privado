using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace wfiplib
{
    public class admTipoTramiteDoctos
    {

        public bool agrega(tipoTramiteDoctos pDatos)
        {
            bool resultado = false;
            StringBuilder SqlCmd = new StringBuilder("INSERT INTO tipoTramiteDoctos (");
            SqlCmd.Append("IdFlujo");
            SqlCmd.Append(",IdTipoTramite");
            SqlCmd.Append(",IdTipoDocto");
            SqlCmd.Append(",Orden");
            SqlCmd.Append(")");

            SqlCmd.Append(" VALUES (");
            SqlCmd.Append(pDatos.IdFlujo);
            SqlCmd.Append("," + pDatos.IdTipoTramite );
            SqlCmd.Append("," + pDatos.IdTipoDocto );
            SqlCmd.Append("," + pDatos.Orden );
            SqlCmd.Append(");");
            bd BD = new bd();
            resultado = BD.ejecutaCmd(SqlCmd.ToString());

            BD.cierraBD();

            return resultado;
        }
        
        public List<tipoTramiteDoctos> daListaDisponibles(int pIdFlujo, E_TipoTramite pTipoTramite, int pIdTramite)
        {
            List<tipoTramiteDoctos> resultado = new List<tipoTramiteDoctos>();
            bd BD = new bd();
            StringBuilder SqlCmd = new StringBuilder("SELECT * FROM vw_tipoTramiteDoctos");
            SqlCmd.Append(" WHERE IdFlujo=" + pIdFlujo.ToString());
            SqlCmd.Append(" AND IdTipoTramite=" + pTipoTramite.ToString("d"));
            SqlCmd.Append(" AND IdTipoDocto NOT IN (SELECT IdTipoDocto FROM documento WHERE IdTramite=" + pIdTramite.ToString() + ")");
            SqlCmd.Append(" ORDER BY Orden");
            DataTable datos = BD.leeDatos(SqlCmd.ToString());
            if (datos.Rows.Count > 0) foreach (DataRow registro in datos.Rows) resultado.Add(armaCatDocto(registro));
            BD.cierraBD();
            return resultado;
        }

        private tipoTramiteDoctos armaCatDocto(DataRow pRegistro)
        {
            tipoTramiteDoctos resultado = new tipoTramiteDoctos();
            if (!pRegistro.IsNull("IdTipoDocto")) resultado.IdTipoDocto = Convert.ToInt32(pRegistro["IdTipoDocto"]);
            if (!pRegistro.IsNull("Nombre")) resultado.Nombre = (Convert.ToString(pRegistro["Nombre"])).Trim();
            return resultado;
        }


        public List<tipoTramiteDoctos> daDoctosPorAgregarTipoTramite(int pIdFlujo, int pTipoTramite)
        {
            List<tipoTramiteDoctos> resultado = new List<tipoTramiteDoctos>();
            bd BD = new bd();
            StringBuilder SqlCmd = new StringBuilder("SELECT IdTipoDocto, Nombre FROM cat_documento");
            SqlCmd.Append(" WHERE IdTipoDocto not in(");
            SqlCmd.Append(" select IdTipoDocto from vw_tipoTramiteDoctos where IdFlujo=" + pIdFlujo + " and IdTipoTramite=" + pTipoTramite);
            SqlCmd.Append(")");
            DataTable datos = BD.leeDatos(SqlCmd.ToString());
            if (datos.Rows.Count > 0) foreach (DataRow registro in datos.Rows) resultado.Add(armaCatDocto(registro));
            BD.cierraBD();
            return resultado;
        }

        public List<tipoTramiteDoctos> daDoctosContieneTipoTramite(int pIdFlujo, int pTipoTramite)
        {
            List<tipoTramiteDoctos> resultado = new List<tipoTramiteDoctos>();
            bd BD = new bd();
            StringBuilder SqlCmd = new StringBuilder("SELECT * FROM vw_tipoTramiteDoctos");
            SqlCmd.Append(" WHERE IdFlujo=" + pIdFlujo.ToString());
            SqlCmd.Append(" AND IdTipoTramite=" + pTipoTramite.ToString());
            SqlCmd.Append(" ORDER BY Orden");
            DataTable datos = BD.leeDatos(SqlCmd.ToString());
            if (datos.Rows.Count > 0) foreach (DataRow registro in datos.Rows) resultado.Add(armaCatDocto(registro));
            BD.cierraBD();
            return resultado;
        }

        public  bool EliminaDoctosTramite(int pIdFlujo, int pTipoTramite)
        {
            bd BD = new bd();
            StringBuilder SqlCmd = new StringBuilder("DELETE tipoTramiteDoctos");
            SqlCmd.Append(" WHERE IdFlujo=" + pIdFlujo.ToString());
            SqlCmd.Append(" AND IdTipoTramite=" + pTipoTramite.ToString());
            bool resultado = BD.ejecutaCmd(SqlCmd.ToString());
            BD.cierraBD();
            return resultado;
        }
    }
    
    public class tipoTramiteDoctos
    {
        private int mIdFlujo = 0;
        public int IdFlujo { get { return mIdFlujo; } set { mIdFlujo = value; } }
        private int mIdTipoTramite = 0;
        public int IdTipoTramite { get { return mIdTipoTramite; } set { mIdTipoTramite = value; } }
        private int mIdTipoDocto = 0;
        public int IdTipoDocto { get { return mIdTipoDocto; } set { mIdTipoDocto = value; } }
        private string mNombre = "";
        public string Nombre { get { return mNombre; } set { mNombre = value; } }
        
        private int mOrden = 0;
        public int Orden { get { return mOrden; } set { mOrden = value; } }
    }
}
