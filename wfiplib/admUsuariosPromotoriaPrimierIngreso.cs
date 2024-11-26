using System;
using System.Data;
using System.Text;

namespace wfiplib
{
    public class admUsuariosPromotoriaPrimierIngreso
    {
        public int existe(string pUsuario)
        {
            BaseDeDatos b = new BaseDeDatos();
            int resultado = 0;
            string consulta = "SELECT id_Usuario FROM UsuariosPromotoriaPrimerIngreso WHERE usuario=@pusuario AND primerIngreso=0";
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@pusuario", pUsuario, SqlDbType.NVarChar);
            if (b.Select().Rows.Count > 0)
                resultado = int.Parse(b.SelectString());

            //StringBuilder SqlCmd = new StringBuilder("SELECT id_Usuario FROM UsuariosPromotoriaPrimerIngreso WHERE usuario='" + pUsuario + "' AND primerIngreso=0");
            //bd BD = new bd();
            //DataTable datos = BD.leeDatos(SqlCmd.ToString());
            //if (datos.Rows.Count > 0)
            //    resultado = Convert.ToInt32(datos.Rows[0][0]); 
            //datos.Dispose();
            //BD.cierraBD();
            return resultado;
        }

        public string daNombreUsuario(int pIdUsuario)
        {
            BaseDeDatos b = new BaseDeDatos();
            string resultado = "";
            string consulta = "SELECT usuario FROM UsuariosPromotoriaPrimerIngreso WHERE id_Usuario=@pidusuario";
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@pidusuario", pIdUsuario, SqlDbType.Int);
            if (b.Select().Rows.Count > 0)
                resultado = b.SelectString();
            //StringBuilder SqlCmd = new StringBuilder("SELECT usuario FROM UsuariosPromotoriaPrimerIngreso WHERE id_Usuario=" + pIdUsuario.ToString());
            //bd BD = new bd();
            //DataTable datos = BD.leeDatos(SqlCmd.ToString());
            //if (datos.Rows.Count > 0) { resultado = Convert.ToString(datos.Rows[0][0]); }
            //datos.Dispose();
            //BD.cierraBD();
            return resultado;
        }

        public int daClavePromotoria(int pIdUsuario)
        {
            int resultado = 0;
            StringBuilder SqlCmd = new StringBuilder("SELECT ClavePromotoria FROM UsuariosPromotoriaPrimerIngreso WHERE id_Usuario=" + pIdUsuario.ToString());
            bd BD = new bd();
            DataTable datos = BD.leeDatos(SqlCmd.ToString());
            if (datos.Rows.Count > 0) { resultado = Convert.ToInt32(datos.Rows[0][0]); }
            datos.Dispose();
            BD.cierraBD();
            return resultado;
        }

        public void registraPrimerIngreso(int pIdUsuario)
        {
            StringBuilder SqlCmd = new StringBuilder("UPDATE UsuariosPromotoriaPrimerIngreso SET primerIngreso = 1 WHERE id_Usuario=" + pIdUsuario.ToString());
            bd BD = new bd();
            BD.ejecutaCmd(SqlCmd.ToString());
        }
    }
}
