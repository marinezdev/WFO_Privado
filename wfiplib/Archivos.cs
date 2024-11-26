using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace wfiplib
{
    public class Archivos
    {
        BaseDeDatos b = new BaseDeDatos();

        public DataTable Seleccionar()
        {
            string consulta = "SELECT * FROM wfip_archivos.dbo.archivos";
            b.ExecuteCommandQuery(consulta);
            return b.Select();
        }

        public byte[] Archivo(string idarchivo)
        {
            byte[] bytes = null;
            string consulta = "SELECT archivo FROM wfip_archivos.dbo.archivos WHERE idarchivo=@idarchivo";
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@idarchivo", idarchivo, SqlDbType.Int);
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                bytes = (byte[])reader["archivo"];
            }
            reader = null;
            b.CloseConnection();
            return bytes;
        }


    }
}
