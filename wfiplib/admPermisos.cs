using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;

namespace wfiplib
{
    public class admPermisos
    {
        BaseDeDatos b = new BaseDeDatos();

        public List<PermisosEntity> PermisosObtenerPorRol(int rol)
        {
            string consulta = "SELECT * FROM permisos WHERE IdRol=@rol AND Activo=1";
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@rol", rol, System.Data.SqlDbType.Int);
            List<PermisosEntity> resultado = new List<PermisosEntity>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                PermisosEntity itm = new PermisosEntity()
                {
                    IdPermiso = int.Parse(reader["IdPermiso"].ToString()),
                    IdRol = int.Parse(reader["IdRol"].ToString())
                };
                resultado.Add(itm);
            }
            reader = null;
            b.ConnectionCloseToTransaction();
            return resultado;
        }

        public DataTable PermisosObtenerTablaPorRol(int idrol)
        {
            string consulta = "SELECT * FROM permisos WHERE IdRol=@idrol";
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@idrol", idrol, SqlDbType.Int);
            return b.Select();
        }

        public int PermisoAgregar(string descripcion, int idrol)
        {
            string consulta = "INSERT INTO permisos (IdRol, Activo, Descripcion) VALUES (@idrol, 1, @descripcion)";
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@idrol", idrol, System.Data.SqlDbType.Int);
            b.AddParameter("@descripcion", descripcion, System.Data.SqlDbType.NVarChar, 450);
            return b.InsertUpdateDelete();
        }

        public int PermisoModificar(int activo, int idrol, int idpermiso)
        {
            string consulta = "UPDATE permisos SET Activo=@activo WHERE IdRol=@idrol AND idPermiso=@idpermiso";
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@idrol", idrol, SqlDbType.Int);
            //b.AddParameter("@descripcion", descripcion, System.Data.SqlDbType.NVarChar, 450);
            b.AddParameter("@activo", activo, SqlDbType.Int);
            b.AddParameter("@idpermiso", idpermiso, SqlDbType.Int);
            return b.InsertUpdateDelete();
        }

        public void PermisosObtener_TreeView(ref TreeView treeview, int idrol)
        {
            
        }
    }

    public class PermisosEntity
    {
        public int IdPermiso { get; set; }
        public int IdRol { get; set; }
        public int Activo { get; set; }
        public string Descripcion { get; set; }
    }
}
