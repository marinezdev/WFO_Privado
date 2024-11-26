using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;

namespace wfiplib
{
    public class admRoles
    {
        BaseDeDatos b = new BaseDeDatos();

        private DataTable RolesObtener()
        {
            string consulta = "SELECT * FROM roles";
            b.ExecuteCommandQuery(consulta);
            return b.Select();
        }

        public void RolesObtener_DropDownList(ref DropDownList ddl)
        {
            LlenarControles.LlenarDropDownList(ref ddl, RolesObtener(), "Descripcion", "IdRol");
        }

        public int RolAgregar(string descripcion)
        {
            string consulta = "INSERT INTO roles (descripcion) VALUES (@descripcion)" +
                "SELECT @@Identity";
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@descripcion", descripcion, SqlDbType.NVarChar, 50);
            return b.SelectWithReturnValue();
        }



        public int EditarRol(int idrol, string descripcion)
        {
            string consulta = "UPDATE roles SET descripcion=@descripcion WHERE IdRol=@idrol";
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@descripcion", descripcion, SqlDbType.NVarChar, 50);
            b.AddParameter("@idrol", idrol, SqlDbType.Int);
            return b.InsertUpdateDelete();
        }

        public int AsociarMenuARol(int idrol, int modulo, int grupo)
        {
            string consulta = "INSERT INTO permisosmenu (idmenu, idrol, activo) SELECT idmenu, @idrol AS idrol, 1 AS activo FROM menu WHERE modulo=@modulo AND grupo=@grupo";
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@idrol", idrol, SqlDbType.Int);
            b.AddParameter("@modulo", modulo, SqlDbType.Int);
            b.AddParameter("@grupo", grupo, SqlDbType.Int);
            return b.InsertUpdateDelete();
        }

    }
}
