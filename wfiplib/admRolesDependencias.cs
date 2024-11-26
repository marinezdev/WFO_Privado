using System.Data;

namespace wfiplib
{
    public class admRolesDependencias
    {
        RolesDependenciasTablas rdt = new RolesDependenciasTablas();

        public bool SeleccionarRolEnDependencia(int rol)
        {
            if (rdt.SeleccionarRolEnDependencia(rol) == "Dato vacío")
                return false;
            else
                return true;
        }

        public int Agregar(int rol, int iddependencia)
        {
            return rdt.Agregar(rol, iddependencia);
        }

        internal class RolesDependenciasTablas
        {
            BaseDeDatos b = new BaseDeDatos();
          
            public string SeleccionarRolEnDependencia(int rol)
            {
                string consulta = "SELECT IdRol FROM RolesDependencias WHERE idrol=@idrol";
                b.ExecuteCommandQuery(consulta);
                b.AddParameter("@idrol", rol, SqlDbType.Int);
                return b.SelectString();
            }

            public int Agregar(int rol, int dep)
            {
                string consulta = "INSERT INTO RolesDependencias VALUES(@rol, @dependencia)";
                b.ExecuteCommandQuery(consulta);
                b.AddParameter("@rol", rol, SqlDbType.Int);
                b.AddParameter("@dependencia", dep, SqlDbType.Int);
                return b.InsertUpdateDeleteWithTransaction();
            }
        }
    }
}
