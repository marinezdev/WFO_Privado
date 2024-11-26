using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace wfiplib
{
    public class UsuariosMtto
    {
        BaseDeDatos b = new BaseDeDatos();

        public bool Validar(string clave, string contra)
        {
            string consulta = "SELECT * FROM usuariosmtto WHERE clave=@clave ANd contra=@contra";
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@clave", clave, System.Data.SqlDbType.VarChar);
            b.AddParameter("@contra", contra, System.Data.SqlDbType.VarChar);
            if (b.Select().Rows.Count > 0)
                return true;
            else
                return false;
        }
    }
}
