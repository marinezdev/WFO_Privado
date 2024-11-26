using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace wfiplib
{
    public class admCatEstados
    {
        BaseDeDatos b = new BaseDeDatos();

        public string SeleccionarIdEstadoPorNombre(string estado)
        {
            string consulta = "SELECT id_estado FROM cat_estados WHERE estado=@estado";
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@estado", estado.Trim(), SqlDbType.NVarChar);
            if (b.Select().Rows.Count > 0)
                return b.Select().Rows[0][0].ToString();
            else
                return "0";
        }

    }
}
