using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace wfiplib
{
    public class admChecks
    {
        public bool Nuevo(Checks pChecks)
        {
            bool resultado = false;
            StringBuilder SqlCmd = new StringBuilder("INSERT INTO dbo.DocChecEmi(IdTramite,IdDocRequerido) Values(");
            SqlCmd.Append(pChecks.IdTramite.ToString());
            SqlCmd.Append("," + pChecks.IdDocRequerido.ToString());
            SqlCmd.Append(")");
            bd BD = new bd();
            resultado = BD.ejecutaCmd(SqlCmd.ToString());
            BD.cierraBD();
            return resultado;
        }
    }
}
