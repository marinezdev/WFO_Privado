using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace wfiplib
{
    public class admTarifaPlanBasicoTitulares
    {
        BaseDeDatos b = new BaseDeDatos();

        public DataTable VerificarTarifaQuincenalTitulares(string nivel, int sumasegurada)
        {
            string consulta = "SELECT * FROM TarifaPlanBasicoTitulares WHERE nivel=@nivel AND sumaasegurada=@sumasegurada";
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@nivel", nivel, SqlDbType.Char);
            b.AddParameter("@sumaasegurada", sumasegurada, SqlDbType.Int);
            return b.Select();
        }
    }
}
