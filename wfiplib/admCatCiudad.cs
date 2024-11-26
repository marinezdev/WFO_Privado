using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace wfiplib
{
    public class admCatCiudad
    {
        BaseDeDatos b = new BaseDeDatos();

        public string SeleccionarIdCiudadPorCiudad(string ciudad)
        {
            string consulta = "SELECT Id_ciudad FROM cat_ciudad WHERE ciudad=@ciudad";
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@ciudad", ciudad.Trim(), SqlDbType.NVarChar);
            if (b.Select().Rows.Count > 0)
                return b.Select().Rows[0][0].ToString();
            else
                return "0";
        }
    }
}
