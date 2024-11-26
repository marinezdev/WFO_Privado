using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wfip.Data
{
    public class Mesa
    {
        ManejoDatos b = new ManejoDatos();
        public List<Propiedades.Mesa> UsuariosMesa_Seleccionar_IdFlujo_IdUsuario(int IdFlujo, int IdUsuario)
        {
            b.ExecuteCommandSP("UsuariosMesa_Seleccionar_IdFlujo_IdUsuario");
            b.AddParameter("@IdFlujo", IdFlujo, SqlDbType.Int);
            b.AddParameter("@IdUsuario", IdUsuario, SqlDbType.Int);
            List<Propiedades.Mesa> resultado = new List<Propiedades.Mesa>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                Propiedades.Mesa item = new Propiedades.Mesa()
                {
                    Id = Convert.ToInt32(reader["Id"].ToString()),
                    Nombre = reader["Nombre"].ToString().ToUpper(),
                    Imagen = reader["Imagen"].ToString(),
                    //Totales = Convert.ToInt32(reader["Totales"].ToString()),
                };
                resultado.Add(item);
            }
            reader = null;
            b.ConnectionCloseToTransaction();
            return resultado;
        }
    }
}
