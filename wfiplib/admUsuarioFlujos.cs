using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace wfiplib
{
    public class admUsuarioFlujos
    {
        public List<usuarioFlujos> lista(int pIdUsuario, int pIdRol)
        {
            List<usuarioFlujos> resultado = new List<usuarioFlujos>();
            string strSQL = "";
            strSQL += "SELECT flujo.Id AS IdFlujo, flujo.Nombre AS FlujoNombre ";
            strSQL += "FROM DBO.SEC_ASIGNPERMISSIONS ";
            strSQL += "     INNER JOIN DBO.SEC_APLICACIONES ON DBO.SEC_APLICACIONES.ID_APLICACION = DBO.SEC_ASIGNPERMISSIONS.ID_APLICACION ";
            strSQL += "     INNER JOIN DBO.SEC_MODULOS ON DBO.SEC_MODULOS.ID_APLICACION = DBO.SEC_ASIGNPERMISSIONS.ID_APLICACION AND DBO.SEC_MODULOS.ID_MODULO = DBO.SEC_ASIGNPERMISSIONS.ID_MODULO ";
            strSQL += "     INNER JOIN DBO.SEC_PERFILES ON DBO.SEC_PERFILES.ID_APLICACION = DBO.SEC_ASIGNPERMISSIONS.ID_APLICACION AND DBO.SEC_PERFILES.ID_PERFIL = DBO.SEC_ASIGNPERMISSIONS.ID_PERFIL ";
            strSQL += "     INNER JOIN DBO.SEC_PERMISOS ON DBO.SEC_PERMISOS.ID_PERMISO = DBO.SEC_ASIGNPERMISSIONS.ID_PERMISO ";
            strSQL += "     INNER JOIN DBO.flujo ON DBO.flujo.SystemAPP = DBO.SEC_ASIGNPERMISSIONS.ID_APLICACION AND DBO.flujo.SystemMOD = DBO.SEC_ASIGNPERMISSIONS.ID_MODULO ";
            strSQL += "WHERE DBO.SEC_APLICACIONES.ACTIVO = 1 ";
            strSQL += "     AND DBO.SEC_MODULOS.ACTIVO = 1 ";
            strSQL += "     AND DBO.SEC_PERFILES.ACTIVO = 1 ";
            strSQL += "     AND DBO.SEC_PERMISOS.ACTIVO = 1 ";
            strSQL += "     AND DBO.SEC_PERFILES.ID_PERFIL = " + pIdRol.ToString() + " ";
            strSQL += "     AND DBO.SEC_ASIGNPERMISSIONS.PERMISO = 1 ";

            bd BD = new bd();
            DataTable datos = BD.leeDatos(strSQL);
            if (datos.Rows.Count > 0)
            {
                foreach (DataRow registro in datos.Rows)
                    resultado.Add(arma(registro));
            }
            datos.Dispose();
            BD.cierraBD();
            return resultado;
        }

        private usuarioFlujos arma(DataRow pRegistro)
        {
            usuarioFlujos resultado = new usuarioFlujos();
            if (!pRegistro.IsNull("IdFlujo")) resultado.IdFlujo = Convert.ToInt32(pRegistro["IdFlujo"]);
            if (!pRegistro.IsNull("FlujoNombre")) resultado.FlujoNombre = (Convert.ToString(pRegistro["FlujoNombre"])).Trim();
            return resultado;
        }

        public DataTable usuariosFlujo_Seleccionar_IdUsuario(int IdUsuario)
        {
            BaseDeDatos b = new BaseDeDatos();
            b.ExecuteCommandSP("usuariosFlujo_Seleccionar_IdUsuario");
            b.AddParameter("@IdUsuario", (int)IdUsuario, SqlDbType.Int);
            return b.Select();
        }
    }

    public class usuarioFlujos
    {
        private int mIdFlujo = 0;
        public int IdFlujo { get { return mIdFlujo; } set { mIdFlujo = value; } }
        private string mFlujoNombre = String.Empty;
        public string FlujoNombre { get { return mFlujoNombre; } set { mFlujoNombre = value; } }
    }
}
