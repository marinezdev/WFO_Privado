using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace wfiplib
{
    public class admUsuarioMesa
    {
        public List<usuarioMesa> lista(int pIdUsuario)
        {
            List<usuarioMesa> resultado = new List<usuarioMesa>();
            StringBuilder SqlCmd = new StringBuilder("SELECT * FROM vw_usuarioMesa");
            SqlCmd.Append(" WHERE IdUsuario = " + pIdUsuario.ToString());

            bd BD = new bd();
            DataTable datos = BD.leeDatos(SqlCmd.ToString());
            if (datos.Rows.Count > 0) foreach (DataRow registro in datos.Rows) resultado.Add(arma(registro));
            datos.Dispose();
            BD.cierraBD();
            return resultado;
        }

        public bool VerificaTramiteCancelado(int IdTramite)
        {
            bool blnTramiteCancelado = false;
            //string SqlCmd = "SELECT COUNT(id) FROM tramite WHERE id = " + IdTramite.ToString() + " AND estado IN (SELECT statustramite.id FROM statustramite WHERE statustramite.nombre in ('Hold','Ejecucion','Rechazo','Suspendido','PCI','Promotoría Cancela','Caducado','Suspensión Cita Médica'))";
            string SqlCmd = "SELECT COUNT(id) FROM tramite WHERE id = " + IdTramite.ToString() + " AND estado IN (SELECT statustramite.id FROM statustramite WHERE statustramite.nombre in ('Rechazo','Cancelado','Promotoría Cancela','Caducado'))";
            bd BD = new bd();
            DataTable datos = BD.leeDatos(SqlCmd);
            if (datos.Rows.Count > 0)
            {
                blnTramiteCancelado = Convert.ToBoolean(datos.Rows[0][0]);
            }
            datos.Dispose();
            BD.cierraBD();
            return blnTramiteCancelado;
        }

        public int DaIdTipoTramite(int pIdUsuario, int pIdMesa)
        {
            int resultado = 0;
            String SqlCmd = "SELECT IdTipoTramite FROM usuariosMesa WHERE IdUsuario = " + pIdUsuario;
            bd BD = new bd();
            DataTable datos = BD.leeDatos(SqlCmd);
            if (datos.Rows.Count > 0) { resultado = Convert.ToInt32(datos.Rows[0][0]); }
            datos.Dispose();
            BD.cierraBD();
            return resultado;
        }

        public int DaIdFlujo(int pIdTramite, int pIdTipoTramite)
        {
            int resultado = 0;
            String SqlCmd = "select idflujo from tipoTramite where id = " + pIdTipoTramite.ToString();
            bd BD = new bd();
            DataTable datos = BD.leeDatos(SqlCmd);
            if (datos.Rows.Count > 0)
            {
                resultado = Convert.ToInt32(datos.Rows[0][0]);
            }
            datos.Dispose();
            BD.cierraBD();
            return resultado;
        }

        public int DaIdTipoTramite(int pIdTramite)
        {
            int resultado = 0;
            String SqlCmd = "SELECT IdTipoTramite FROM tramite WHERE id = " + pIdTramite.ToString();
            bd BD = new bd();
            DataTable datos = BD.leeDatos(SqlCmd);
            if (datos.Rows.Count > 0) { resultado = Convert.ToInt32(datos.Rows[0][0]); }
            datos.Dispose();
            BD.cierraBD();
            return resultado;
        }

        private usuarioMesa arma(DataRow pRegistro)
        {
            usuarioMesa resultado = new usuarioMesa();
            if (!pRegistro.IsNull("IdUsuario")) resultado.IdUsuario = Convert.ToInt32(pRegistro["IdUsuario"]);
            if (!pRegistro.IsNull("IdFlujo")) resultado.IdFlujo = Convert.ToInt32(pRegistro["IdFlujo"]);
            if (!pRegistro.IsNull("IdMesa")) resultado.IdMesa = Convert.ToInt32(pRegistro["IdMesa"]);
            if (!pRegistro.IsNull("IdTipoTramite")) resultado.IdTipoTramite = Convert.ToInt32(pRegistro["IdTipoTramite"]);
            if (!pRegistro.IsNull("FlujoNombre")) resultado.FlujoNombre = (Convert.ToString(pRegistro["FlujoNombre"])).Trim();
            if (!pRegistro.IsNull("MesaNombre")) resultado.MesaNombre = (Convert.ToString(pRegistro["MesaNombre"])).Trim();
            if (!pRegistro.IsNull("TramiteNombre")) resultado.TramiteNombre = (Convert.ToString(pRegistro["TramiteNombre"])).Trim();
            if (!pRegistro.IsNull("Condicion")) resultado.Condicion = (Convert.ToString(pRegistro["Condicion"])).Trim();
            return resultado;
        }

        public bool Guardar(int pIdUsuario, List<usuarioMesa> pUsuarioMesa)
        {
            bool resultado = false;
            Eliminar(pIdUsuario);
            foreach (usuarioMesa oUsrMesa in pUsuarioMesa) { resultado = Registrar(oUsrMesa); }
            return resultado;
        }

        public void Eliminar(int pIdUsuario)
        {
            StringBuilder SqlCmd = new StringBuilder("delete usuariosMesa WHERE IdUsuario=" + pIdUsuario);
            bd BD = new bd();
            BD.ejecutaCmd(SqlCmd.ToString());
            BD.cierraBD();
        }

        private bool Registrar(usuarioMesa pDatos)
        {
            bool resultado = false;
            StringBuilder SqlCmd = new StringBuilder("INSERT INTO usuariosMesa (");
            SqlCmd.Append("IdUsuario");
            SqlCmd.Append(",IdFlujo");
            SqlCmd.Append(",IdMesa");
            SqlCmd.Append(",IdTipoTramite");
            SqlCmd.Append(",Condicion");
            SqlCmd.Append(")");
            SqlCmd.Append(" VALUES (");
            SqlCmd.Append(pDatos.IdUsuario);
            SqlCmd.Append("," + pDatos.IdFlujo);
            SqlCmd.Append("," + pDatos.IdMesa);
            SqlCmd.Append("," + pDatos.IdTipoTramite);
            SqlCmd.Append(",'" + pDatos.Condicion + "'");
            SqlCmd.Append(")");
            bd BD = new bd();
            resultado = BD.ejecutaCmd(SqlCmd.ToString());

            BD.cierraBD();

            return resultado;
        }
    }

    public class usuarioMesa
    {
        private int mIdUsuario = 0;
        public int IdUsuario { get { return mIdUsuario; } set { mIdUsuario = value; } }
        private int mIdFlujo = 0;
        public int IdFlujo { get { return mIdFlujo; } set { mIdFlujo = value; } }
        private int mIdMesa = 0;
        public int IdMesa { get { return mIdMesa; } set { mIdMesa = value; } }
        private int mIdTipoTramite = 0;
        public int IdTipoTramite { get { return mIdTipoTramite; } set { mIdTipoTramite = value; } }
        private string mFlujoNombre = String.Empty;
        public string FlujoNombre { get { return mFlujoNombre; } set { mFlujoNombre = value; } }
        private string mMesaNombre = String.Empty;
        public string MesaNombre { get { return mMesaNombre; } set { mMesaNombre = value; } }
        private string mTramiteNombre = String.Empty;
        public string TramiteNombre { get { return mTramiteNombre; } set { mTramiteNombre = value; } }
        private string mCondicion = String.Empty;
        public string Condicion { get { return mCondicion; } set { mCondicion = value; } }
    }
}
