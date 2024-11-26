using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace wfiplib
{
    public class admTramiteRechazo
    {
        public int siguienteId()
        {
            int resultado = 0;
            bd BD = new bd();
            resultado = BD.ejecutaCmdScalar("Insert Into tramiteRechazoCtrl(Fecha) Values(GetDate()) SELECT SCOPE_IDENTITY()");
            BD.cierraBD();
            return resultado;
        }

        public bool nuevo(tramiteRechazo pDatos)
        {
            bool resultado = false;
            StringBuilder SqlCmd = new StringBuilder("Insert Into tramiteRechazo(Id,IdTramiteMesa,IdUsuario,ObservacionPublica,ObservacionPrivada,Estado,FechaRegistro)");
            SqlCmd.Append(" Values(");
            SqlCmd.Append(pDatos.Id.ToString());
            SqlCmd.Append("," + pDatos.IdTramiteMesa.ToString());
            SqlCmd.Append("," + pDatos.IdUsuario.ToString());
            SqlCmd.Append(",'" + pDatos.ObservacionPublica + "'");
            SqlCmd.Append(",'" + pDatos.ObservacionPrivada + "'");
            SqlCmd.Append("," + pDatos.Estado.ToString("d"));
            SqlCmd.Append(",GetDate()");
            SqlCmd.Append(")");
            bd BD = new bd();
            resultado = BD.ejecutaCmd(SqlCmd.ToString());
            BD.cierraBD();
            return resultado;
        }

        public void nuevoMotivo(int pId, int pIdMotivos)
        {
            bd BD = new bd();
            BD.ejecutaCmd("Insert Into tramiteRechazoMotivos(Id,IdMotivo) Values(" + pId.ToString() + "," + pIdMotivos.ToString() + ")");
            BD.cierraBD();
        }

        public void nuevoMotivos(int pId, String[] pMotivos)
        {
            bd BD = new bd();
            foreach(string IdMotivo in pMotivos)
            {
                BD.ejecutaCmd("Insert Into tramiteRechazoMotivos(Id,IdMotivo) Values(" + pId.ToString() + "," + IdMotivo + ")");
            }
            BD.cierraBD();
        }

        public tramiteRechazo carga(int pId)
        {
            tramiteRechazo resultado = new tramiteRechazo();
            bd BD = new bd();
            DataTable datos = BD.leeDatos("Select * Form tramiteRechazo Where Id=" + pId.ToString());
            if (datos.Rows.Count > 0) foreach (DataRow registro in datos.Rows) resultado = arma(registro);
            BD.cierraBD();
            datos.Dispose();
            return resultado;
        }

        private tramiteRechazo arma(DataRow pRegistro)
        {
            tramiteRechazo resultado = new tramiteRechazo();
            if (!pRegistro.IsNull("Id")) resultado.Id = Convert.ToInt32(pRegistro["Id"]);
            if (!pRegistro.IsNull("IdTramiteMesa")) resultado.IdTramiteMesa = Convert.ToInt32(pRegistro["IdTramiteMesa"]);
            if (!pRegistro.IsNull("IdUsuario")) resultado.IdUsuario = Convert.ToInt32(pRegistro["IdUsuario"]);
            if (!pRegistro.IsNull("ObservacionPublica")) resultado.ObservacionPublica = Convert.ToString(pRegistro["ObservacionPublica"]);
            if (!pRegistro.IsNull("ObservacionPrivada")) resultado.ObservacionPrivada = Convert.ToString(pRegistro["ObservacionPrivada"]);
            if (!pRegistro.IsNull("Estado")) resultado.Estado = (E_EstadoMesa)pRegistro["Estado"];
            if (!pRegistro.IsNull("FechaRegistro")) resultado.FechaRegistro = Convert.ToDateTime(pRegistro["FechaRegistro"]);
            resultado.Motivos = lstMotivos(resultado.Id);
            return resultado;
        }

        private List<string> lstMotivos(int pId)
        {
            List<string> resultado = new List<string>();
            bd BD = new bd();
            DataTable datos = BD.leeDatos("Select Nombre From cat_MotivosRechazo Where Id In(Select IdMotivo From tramiteRechazoMotivos Where Id=" + pId.ToString() +")");
            if (datos.Rows.Count > 0)
            {
                foreach (DataRow registro in datos.Rows)
                {
                    if (!registro.IsNull("Nombre")) resultado.Add(Convert.ToString(registro["Nombre"]));
                }
            }
            BD.cierraBD();
            datos.Dispose();
            return resultado;
        }
    }

    public class tramiteRechazo
    {
        private int mId = 0;
        public int Id { get { return mId; } set { mId = value; } }
        private int mIdTramiteMesa = 0;
        public int IdTramiteMesa { get { return mIdTramiteMesa; } set { mIdTramiteMesa = value; } }
        private int mIdUsuario = 0;
        public int IdUsuario { get { return mIdUsuario; } set { mIdUsuario = value; } }
        private string mObservacionPublica = "";
        public string ObservacionPublica { get { return mObservacionPublica; } set { mObservacionPublica = value; } }
        private string mObservacionPrivada = "";
        public string ObservacionPrivada { get { return mObservacionPrivada; } set { mObservacionPrivada = value; } }
        private E_EstadoMesa mEstado = E_EstadoMesa.Registro;
        public E_EstadoMesa Estado { get { return mEstado; } set { mEstado = value; } }
        private DateTime mFechaRegistro = new DateTime(1900, 1, 1, 0, 0, 0);
        public DateTime FechaRegistro { get { return mFechaRegistro; } set { mFechaRegistro = value; } }
        private List<string> mMotivos = new List<string>();
        public List<string> Motivos { get { return mMotivos; } set { mMotivos = value; } }
    }
}
