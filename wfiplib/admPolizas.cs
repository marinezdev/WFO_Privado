using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace wfiplib
{
    public class admPolizas
    {
        BaseDeDatos b = new BaseDeDatos();

        public string NombreCliente(string poliza)
        {
            string consulta = "SELECT COALESCE(nombrecliente, 'No existe') AS NombreCliente FROM polizas WHERE NoPoliza=@poliza";
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@poliza", poliza, System.Data.SqlDbType.NVarChar, 50);
            return b.SelectString();
        }
    }

    public class PolizaPropiedades
    {
        string Poliza { get; set; }
        string NombreCliente { get; set; }
    }
        



}
