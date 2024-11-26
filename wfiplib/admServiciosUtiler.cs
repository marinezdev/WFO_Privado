using System.Data;
using System.Text;

namespace wfiplib
{
    public class admServiciosUtiler
    {
        public DataTable buscaRFCAntecedente(string pRfc, int pIdTramite)
        {
            StringBuilder SqlCmd = new StringBuilder("SELECT IdTramite, NumPoliza, Nombre + ' ' + ApPaterno + ' ' + ApMaterno AS Nombre, RFC FROM TRAM00001 WHERE IdTramite != " + pIdTramite.ToString() + " AND RFC='" + pRfc + "'");
            SqlCmd.Append(" UNION");
            SqlCmd.Append(" SELECT IdTramite, NumPoliza, Nombre + ' ' + ApPaterno + ' ' + ApMaterno AS Nombre, RFC FROM TRAM00002 WHERE IdTramite != " + pIdTramite.ToString() + " AND RFC='" + pRfc + "'");
            SqlCmd.Append(" ORDER BY IdTramite DESC");
            bd BD = new bd();
            DataTable resultado = BD.leeDatos(SqlCmd.ToString());
            BD.cierraBD();
            return resultado;
        }

        public bool buscaRFCAntecedenteEmision(string pRfc)
        {
            bool resultado = false;
            StringBuilder SqlCmd = new StringBuilder("SELECT IdTramite, Nombre + ' ' + ApPaterno + ' ' + ApMaterno AS Nombre, RFC FROM TRAM00001 WHERE RFC='" + pRfc + "'");
            SqlCmd.Append(" UNION");
            SqlCmd.Append(" SELECT IdTramite, Nombre + ' ' + ApPaterno + ' ' + ApMaterno AS Nombre, RFC FROM TRAM00002 WHERE RFC='" + pRfc + "'");
            SqlCmd.Append(" UNION");
            SqlCmd.Append(" SELECT IdTramite, Nombre + ' ' + ApPaterno + ' ' + ApMaterno AS Nombre, RFC FROM TRAM00003P WHERE RFC='" + pRfc + "'");
            SqlCmd.Append(" ORDER BY IdTramite DESC");
            bd BD = new bd();
            DataTable datos = BD.leeDatos(SqlCmd.ToString());
            if (datos.Rows.Count > 0) { resultado = true; }
            BD.cierraBD();
            return resultado;
        }

        public bool buscaRFCAntecedenteEmision2(string pRfc)
        {
            bool resultado = false;
            StringBuilder SqlCmd = new StringBuilder("SELECT IdTramite, Nombre + ' ' + ApPaterno + ' ' + ApMaterno AS Nombre, RFC FROM TRAM00001 WHERE RFC='" + pRfc + "'");
            SqlCmd.Append(" UNION");
            SqlCmd.Append(" SELECT IdTramite, Nombre + ' ' + ApPaterno + ' ' + ApMaterno AS Nombre, RFC FROM TRAM00002 WHERE RFC='" + pRfc + "'");
            SqlCmd.Append(" UNION");
            SqlCmd.Append(" SELECT IdTramite, Nombre + ' ' + ApPaterno + ' ' + ApMaterno AS Nombre, RFC FROM TRAM00003P WHERE RFC='" + pRfc + "'");
            SqlCmd.Append(" ORDER BY IdTramite DESC");
            bd BD = new bd();
            DataTable datos = BD.leeDatos(SqlCmd.ToString());
            if (datos.Rows.Count > 1) { resultado = true; }
            BD.cierraBD();
            return resultado;
        }


        public bool buscaRFCAntecedente(string pRfc)
        {
            bool resultado = false;
            StringBuilder SqlCmd = new StringBuilder("SELECT IdTramite, NumPoliza, Nombre + ' ' + ApPaterno + ' ' + ApMaterno AS Nombre, RFC FROM TRAM00001 WHERE RFC='" + pRfc + "'");
            SqlCmd.Append(" UNION");
            SqlCmd.Append(" SELECT IdTramite, NumPoliza, Nombre + ' ' + ApPaterno + ' ' + ApMaterno AS Nombre, RFC FROM TRAM00002 WHERE RFC='" + pRfc + "'");
            SqlCmd.Append(" ORDER BY IdTramite DESC");
            bd BD = new bd();
            DataTable datos = BD.leeDatos(SqlCmd.ToString());
            if (datos.Rows.Count > 0) { resultado = true; }
            BD.cierraBD();
            return resultado;
        }
    }
}
