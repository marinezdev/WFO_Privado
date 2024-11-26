using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace wfiplib
{
    public class admCatMotivoRechazo 
    {
        public bool nuevo(catMotivoRechazo pDatos)
        {
            bool resultado = false;
            StringBuilder SqlCmd = new StringBuilder("INSERT INTO cat_MotivosRechazo (");
            SqlCmd.Append("FechaRegistro");
            SqlCmd.Append(",IdFlujo");
            SqlCmd.Append(",IdTipoTramite");
            SqlCmd.Append(",IdTipoRechazo");
            SqlCmd.Append(",Nombre");
            SqlCmd.Append(",Activo");
            SqlCmd.Append(")");
            SqlCmd.Append(" VALUES (");
            SqlCmd.Append("getdate()");
            SqlCmd.Append("," + pDatos.IdFlujo.ToString () );
            SqlCmd.Append("," + pDatos.IdTipoTramite.ToString () );
            SqlCmd.Append("," + pDatos.IdTipoRechazo.ToString());
            SqlCmd.Append(",'" + pDatos.Nombre + "'");
            SqlCmd.Append(",1");
            SqlCmd.Append(");");
            bd BD = new bd();
            resultado = BD.ejecutaCmd(SqlCmd.ToString());
            BD.cierraBD();
            return resultado;
        }

        private catMotivoRechazo arma(DataRow pRegistro)
        {
            catMotivoRechazo respuesta = new catMotivoRechazo();
            if (!pRegistro.IsNull("Id")) respuesta.Id = Convert.ToInt32(pRegistro["Id"]);
            if (!pRegistro.IsNull("FechaRegistro")) respuesta.FechaRegistro = Convert.ToDateTime(pRegistro["FechaRegistro"]);
            if (!pRegistro.IsNull("IdFlujo")) respuesta.IdFlujo = Convert.ToInt32(pRegistro["IdFlujo"]);
            if (!pRegistro.IsNull("IdTipoTramite")) respuesta.IdTipoTramite = Convert.ToInt32(pRegistro["IdTipoTramite"]);
            if (!pRegistro.IsNull("IdTipoRechazo")) respuesta.IdTipoRechazo = Convert.ToInt32(pRegistro["IdTipoRechazo"]);
            if (!pRegistro.IsNull("Nombre")) respuesta.Nombre = Convert.ToString(pRegistro["Nombre"]);
            if (!pRegistro.IsNull("Activo")) respuesta.Activo = Convert.ToInt32(pRegistro["Activo"]);
            return respuesta;
        }

        public List<catMotivoRechazo> Lista(int pIdFlujo, int pIdTipoTramite, E_EstadoMesa pTipoRechazo)
        {
            List<catMotivoRechazo> respuesta = new List<catMotivoRechazo>();
            bd BD = new bd();
            DataTable datos = BD.leeDatos("SELECT * FROM cat_MotivosRechazo where IdFlujo=" + pIdFlujo.ToString() + " and IdTipoTramite=" + pIdTipoTramite.ToString() + " and IdTipoRechazo=" + pTipoRechazo.ToString("d") + " order by Nombre");
            foreach (DataRow reg in datos.Rows) { respuesta.Add(arma(reg)); }
            datos.Dispose();
            BD.cierraBD();
            return respuesta;
        }

        public List<catMotivoRechazo> ListaSoloActivos(string pIdFlujo, string pIdTipoTramite, string pTipoRechazo)
        {
            List<catMotivoRechazo> respuesta = new List<catMotivoRechazo>();
            bd BD = new bd();
            DataTable datos = BD.leeDatos("Select * From cat_MotivosRechazo Where IdFlujo=" + pIdFlujo + " And IdTipoTramite=" + pIdTipoTramite + " And IdTipoRechazo=" + pTipoRechazo + " And Activo=1" + " Order By Nombre");
            foreach (DataRow reg in datos.Rows) { respuesta.Add(arma(reg)); }
            datos.Dispose();
            BD.cierraBD();
            return respuesta;
        }

        public bool Existe(catMotivoRechazo pDatos)
        {
            bool resultado = false;
            StringBuilder SqlCdm = new StringBuilder("SELECT * FROM cat_MotivosRechazo");
            SqlCdm.Append(" WHERE IdFlujo=" + pDatos.IdFlujo.ToString());
            SqlCdm.Append(" AND IdTipoTramite=" + pDatos.IdTipoTramite.ToString());
            SqlCdm.Append(" AND IdTipoRechazo=" + pDatos.IdTipoRechazo.ToString());
            SqlCdm.Append(" AND Nombre='" + pDatos.Nombre +"'");
            bd BD = new bd();
            DataTable datos = BD.leeDatos(SqlCdm.ToString());
            resultado = (datos.Rows.Count > 0);
            datos.Dispose();
            BD.cierraBD();
            return resultado;
        }

        public catMotivoRechazo carga(int pId)
        {
            catMotivoRechazo resultado = new catMotivoRechazo();
            string SqlCdm = "SELECT * FROM cat_MotivosRechazo where Id=" + pId.ToString ();
            bd BD = new bd();
            DataTable datos = BD.leeDatos(SqlCdm);
            if (datos.Rows.Count > 0){resultado = arma(datos.Rows[0]);}
            datos.Dispose();
            BD.cierraBD();
            return resultado;
        }

        public bool Modifica(string pId, string pNombre)
        {
            bool resultado = false;
            string SqlCdm = "Update cat_MotivosRechazo set Nombre ='" + pNombre + "'";
            SqlCdm += " where Id=" + pId ;
            bd BD = new bd();
            resultado = BD.ejecutaCmd(SqlCdm);
            BD.cierraBD();

            return resultado;
        }

        public void cambiaActivo(int pId, int pActivo)
        {
            bd BD = new bd();
            BD.ejecutaCmd("UPDATE cat_MotivosRechazo SET Activo = " + pActivo.ToString() + " WHERE Id=" + pId);
            BD.cierraBD();
        }
    }

    public class catMotivoRechazo {
        private int mId = 0;
        public int Id { get { return mId; } set { mId = value; } }
        private DateTime mFechaRegistro = new DateTime(2000, 1, 1, 0, 0, 0);
        public DateTime FechaRegistro { get { return mFechaRegistro; } set { mFechaRegistro = value; } }
        private int mIdFlujo = 0;
        public int IdFlujo { get { return mIdFlujo; } set { mIdFlujo = value; } }
        private int mIdTipoTramite = 0;
        public int IdTipoTramite { get { return mIdTipoTramite; } set { mIdTipoTramite = value; } }
        private int mIdTipoRechazo = 0;
        public int IdTipoRechazo { get { return mIdTipoRechazo; } set { mIdTipoRechazo = value; } }
        private string mNombre = String.Empty;
        public string Nombre { get { return mNombre; } set { mNombre = value; } }
        private int mActivo = 0;
        public int Activo { get { return mActivo; } set { mActivo = value; } }
    }
}
