using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace wfiplib
{
    public class TiemposOperacion
    {
        BaseDeDatos b = new BaseDeDatos();

        public DataTable Seleccionar(string fechainicio, string fechafinal)
        {
            b.ExecuteCommandSP("Tramite_Seleccionar_TramitesProcesados");
            b.AddParameter("@FECHAINICIO", string.Format("{0:yyyy/MM/dd HH:mm:ss}", DateTime.Parse(fechainicio + " 00:00:00")), SqlDbType.DateTime);
            b.AddParameter("@FECHAFINAL", string.Format("{0:yyyy/MM/dd HH:mm:ss}", DateTime.Parse(fechafinal + " 23:59:59")), SqlDbType.DateTime);
            return b.Select();
        }
    }
}
