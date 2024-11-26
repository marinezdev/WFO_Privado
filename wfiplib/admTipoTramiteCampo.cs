using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace wfiplib
{
    public class admTipoTramiteCampo
    {
        public List<tipoTramiteCampo> Lista(int pIdFlujo, int pIdTipoTramite)
        {
            List<tipoTramiteCampo> respuesta = new List<tipoTramiteCampo>();
            bd BD = new bd();
            DataTable datos = BD.leeDatos("SELECT * FROM tipoTramiteCampo WHERE IdFlujo=" + pIdFlujo + " AND IdTipoTramite=" + pIdTipoTramite + " ORDER BY Orden");
            foreach (DataRow reg in datos.Rows) { respuesta.Add(arma(reg)); }
            datos.Dispose();
            BD.cierraBD();
            return respuesta;
        }

        private tipoTramiteCampo arma(DataRow pRegistro)
        {
            tipoTramiteCampo respuesta = new tipoTramiteCampo();
            if (!pRegistro.IsNull("IdFlujo")) respuesta.IdFlujo = Convert.ToInt32(pRegistro["IdFlujo"]);
            if (!pRegistro.IsNull("IdTipoTramite")) respuesta.IdTipoTramite = Convert.ToInt32(pRegistro["IdTipoTramite"]);
            if (!pRegistro.IsNull("IdCampo")) respuesta.IdCampo = Convert.ToInt32(pRegistro["IdCampo"]);
            if (!pRegistro.IsNull("Campo")) respuesta.Campo = Convert.ToString(pRegistro["Campo"]);
            if (!pRegistro.IsNull("TipoDato")) respuesta.Tipo = (E_TipoCampo)(pRegistro["TipoDato"]);
            if (!pRegistro.IsNull("Longitud")) respuesta.Longitud = Convert.ToInt32(pRegistro["Longitud"]);
            if (!pRegistro.IsNull("Requerido")) respuesta.Requerido = Convert.ToInt32(pRegistro["Requerido"]);
            if (!pRegistro.IsNull("Lista")) respuesta.Lista = Convert.ToInt32(pRegistro["Lista"]);
            if (!pRegistro.IsNull("Grupo")) respuesta.Grupo = Convert.ToString(pRegistro["Grupo"]);
            if (!pRegistro.IsNull("Orden")) respuesta.Orden = Convert.ToInt32(pRegistro["Orden"]);
            if (!pRegistro.IsNull("Descripcion")) respuesta.Descripcion = Convert.ToString(pRegistro["Descripcion"]);
            return respuesta;
        }

        public bool Guardar(List<tipoTramiteCampo> lista)
        {
            bool resultado = false;

            Eliminar(lista[0].IdFlujo);
            int contador = 0;
            foreach (tipoTramiteCampo odt in lista) {
                contador += 1;
                odt.Orden = contador;
                resultado = Nuevo(odt);
            }

            return resultado;
        }

        private bool Nuevo(tipoTramiteCampo pDatos)
        {
            bool resultado = false;
            StringBuilder SqlCmd = new StringBuilder("INSERT INTO tipoTramiteCampo (");
            SqlCmd.Append("IdFlujo");
            SqlCmd.Append(",IdTipoTramite");
            SqlCmd.Append(",Campo");
            SqlCmd.Append(",Tipo");
            SqlCmd.Append(",Orden");
            SqlCmd.Append(",Descripcion");
            SqlCmd.Append(")");
            SqlCmd.Append(" VALUES (");
            SqlCmd.Append("," + pDatos.IdFlujo);
            SqlCmd.Append("," + pDatos.IdTipoTramite);
            SqlCmd.Append(",'" + pDatos.Campo + "'");
            SqlCmd.Append("," + pDatos.Tipo.ToString("d"));
            SqlCmd.Append("," + pDatos.Orden);
            SqlCmd.Append(",'" + pDatos.Descripcion + "'");
            SqlCmd.Append(");");
            bd BD = new bd();
            resultado = BD.ejecutaCmd(SqlCmd.ToString());

            BD.cierraBD();

            return resultado;
        }

        private void Eliminar(int pIdFlujo)
        {
            StringBuilder SqlCmd = new StringBuilder("delete tipoTramiteCampo ");
            SqlCmd.Append(" WHERE IdFlujo=" + pIdFlujo);
            bd BD = new bd();
            BD.ejecutaCmd(SqlCmd.ToString());
            BD.cierraBD();
        }

        public List<tipoTramiteCampo> ListaPorGrupo(int pIdFlujo, E_TipoTramite pIdTipoTramite,int pgrupo)
        {
            List<tipoTramiteCampo> respuesta = new List<tipoTramiteCampo>();
            bd BD = new bd();
            string SqlCmd = "SELECT * FROM tipoTramiteCampo WHERE IdFlujo=" + pIdFlujo + " AND IdTipoTramite=" + pIdTipoTramite.ToString ("d") + " AND Grupo=" + pgrupo + " ORDER BY Orden";
            DataTable datos = BD.leeDatos(SqlCmd);
            foreach (DataRow reg in datos.Rows) { respuesta.Add(arma(reg)); }
            datos.Dispose();
            BD.cierraBD();
            return respuesta;
        }
    }

    public class tipoTramiteCampo
    {
        private int mIdFlujo = 0;
        public int IdFlujo { get { return mIdFlujo; } set { mIdFlujo = value; } }
        private int mIdTipoTramite = 0;
        public int IdTipoTramite { get { return mIdTipoTramite; } set { mIdTipoTramite = value; } }
        private int mIdCampo = 0;
        public int IdCampo { get { return mIdCampo; } set { mIdCampo = value; } }
        private string mCampo = string.Empty;
        public string Campo { get { return mCampo; } set { mCampo = value; } }
        private E_TipoCampo mTipo = E_TipoCampo.Ninguno;
        public E_TipoCampo Tipo { get { return mTipo; } set { mTipo = value; } }
        private int mLongitud = 0;
        public int Longitud { get { return mLongitud; } set { mLongitud = value; } }
        private int mRequerido = 0;
        public int Requerido { get { return mRequerido; } set { mRequerido = value; } }
        private int mLista = 0;
        public int Lista { get { return mLista; } set { mLista = value; } }
        private string mGrupo = string.Empty;
        public string Grupo { get { return mGrupo; } set { mGrupo = value; } }
        private int mOrden = 0;
        public int Orden { get { return mOrden; } set { mOrden = value; } }
        private string mDescripcion = string.Empty;
        public string Descripcion { get { return mDescripcion; } set { mDescripcion = value; } }
    }
}
