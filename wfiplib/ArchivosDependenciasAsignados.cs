using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace wfiplib
{
    public class ArchivosDependenciasAsignados
    {
        BaseDeDatos b = new BaseDeDatos();

        public string Seleccionar(string usuario)
        {
            string consulta = "SELECT folio FROM ArchivosDependenciasAsignados WHERE IdUsuario=@usuario";
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@usuario", usuario, SqlDbType.NVarChar);
            return b.SelectString();
        }

        public bool SeleccionarTramiteYaAsignadoAnteriormente(string folio)
        {
            string consulta = "SELECT Folio FROM ArchivosDependenciasAsignados WHERE Folio=@folio";
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@folio", folio, SqlDbType.NVarChar);
            if (b.Select().Rows.Count > 0)
                return true;
            else
                return false;
        }

        public int Agregar(string folio, string usuario)
        {
            string consulta = "INSERT INTO ArchivosDependenciasAsignados VALUES (@folio, @usuario)";
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@folio", folio, SqlDbType.NVarChar);
            b.AddParameter("@usuario", usuario, SqlDbType.NVarChar);
            return b.InsertUpdateDeleteWithTransaction();
        }

        public int Eliminar(string folio)
        {
            string consulta = "DELETE FROM ArchivosDependenciasAsignados WHERE folio=@folio";
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@folio", folio, SqlDbType.NVarChar);
            return b.InsertUpdateDeleteWithTransaction();
        }

       
    }

    public class ArchivosDependenciasAsignadosPropiedades
    {
        public string Folio { get; set; }
        public string IdUsuario { get; set; }
    }
}
