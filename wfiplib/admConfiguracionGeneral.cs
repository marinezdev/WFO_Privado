using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace wfiplib
{
    public class admConfiguracionGeneral
    {
        private readonly BaseDeDatos b = new BaseDeDatos();

        public List<ConfiguracionGeneralEntity> ListaconfiguracionSistema()
        {
            string consulta = "SELECT * FROM configuraciongeneral";
            b.ExecuteCommandQuery(consulta);
            List<ConfiguracionGeneralEntity> resultado = new List<ConfiguracionGeneralEntity>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                ConfiguracionGeneralEntity itm = new ConfiguracionGeneralEntity()
                {
                    Id = int.Parse(reader["Id"].ToString()),
                    Descripcion = reader["Descripcion"].ToString(),
                    Estado = int.Parse(reader["Estado"].ToString()),
                };
                resultado.Add(itm);
            }
            reader = null;
            b.ConnectionCloseToTransaction();
            return resultado;
        }

        public int CambiarValoresConfiguracion(string estado, string id)
        {
            string consulta = "UPDATE configuraciongeneral SET Estado=@estado WHERE Id=@Id";
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@estado", estado, System.Data.SqlDbType.Int);
            b.AddParameter("@id", id, System.Data.SqlDbType.Int);
            return b.InsertUpdateDelete();
        }



    }

    public class ConfiguracionGeneralEntity
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public int Estado { get; set; }
    }

}
