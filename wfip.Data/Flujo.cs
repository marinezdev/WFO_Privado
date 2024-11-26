using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wfip.Data
{
    public class Flujo
    {
        ManejoDatos b = new ManejoDatos();

        public List<Propiedades.Flujo> Flujo_Selecionar_IdUsuario(int IdUsuario)
        {
            b.ExecuteCommandSP("Flujo_Selecionar_IdUsuario");
            b.AddParameter("@IdUsuario", IdUsuario, SqlDbType.Int);
            List<Propiedades.Flujo> resultado = new List<Propiedades.Flujo>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                Propiedades.Flujo item = new Propiedades.Flujo()
                {
                    Id = Convert.ToInt32(reader["Id"].ToString()),
                    Nombre = reader["Nombre"].ToString().ToUpper(),
                };
                resultado.Add(item);
            }
            reader = null;
            b.ConnectionCloseToTransaction();
            return resultado;
        }


    }
}
