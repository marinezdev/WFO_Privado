using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace wfiplib
{
    public class admCoasegurados
    {
        CoaseguradosTablas cat = new CoaseguradosTablas();

        public DataTable Seleccionar(string titular)
        {
            return cat.SeleccionarCoAsegurados(titular);
        }

        public DataRow Seleccionar(string id, string titular=null)
        {
            return cat.SeleccionarDetalle(id).Rows[0];
        }

        public int Agregar(string apellidopaterno, string apellidomaterno, string nombres, string fechanacimiento, string curp, string sexo,
                string fechaafiliacion, string tipo, string fechaingresocolectividad, string aseguradotitular, string estado)
        {
            return cat.Agregar(apellidopaterno, apellidomaterno, nombres, fechanacimiento, curp, sexo,
                fechaafiliacion, tipo, fechaingresocolectividad, aseguradotitular, estado);
        }

        public int Actualizar(string id, string apellidopaterno, string apellidomaterno, string nombres, string fechanacimiento, string curp, string sexo,
                string fechaafiliacion, string tipo, string fechaingresocolectividad, string estado)
        {
            return cat.Actualizar(id, apellidopaterno, apellidomaterno, nombres, fechanacimiento, curp, sexo,
                fechaafiliacion, tipo, fechaingresocolectividad, estado);
        }


        internal class CoaseguradosTablas
        {
            BaseDeDatos b = new BaseDeDatos();

            public DataTable Seleccionar()
            {
                string consulta = "";
                b.ExecuteCommandQuery(consulta);
                return b.Select();
            }

            public DataTable SeleccionarCoAsegurados(string titular)
            {
                string consulta = "SELECT a.Id, a.ApellidoPaterno, a.ApellidoMaterno, a.Nombres, a.FechaNacimiento, a.CURP, a.sexo, a.FechaAfiliacion, a.Tipo, a.FechaIngresoColectividad, a.AseguradoTitular,  b.Nombres + ' ' + b.ApellidoPaterno + ' ' + b.ApellidoMaterno  AS NombreTitular, a.Estado " +
                "FROM coasegurados a, AseguradosTitulares b " +
                "WHERE a.curptitular=@titular " + 
                "AND a.CURPTitular = b.CURP";
                b.ExecuteCommandQuery(consulta);
                b.AddParameter("@titular", titular, SqlDbType.NVarChar);
                return b.Select();
            }

            public DataTable SeleccionarDetalle(string id)
            {
                string consulta = "SELECT * FROM coasegurados WHERE Id=@id";
                b.ExecuteCommandQuery(consulta);
                b.AddParameter("@id", id, SqlDbType.Int);
                return b.Select();
            }

            public int Agregar(string apellidopaterno, string apellidomaterno, string nombres, string fechanacimiento, string curp, string sexo, 
                string fechaafiliacion, string tipo, string fechaingresocolectividad, string aseguradotitular, string estado)
            {
                string consulta = "INSERT INTO coasegurados (ApellidoPaterno, ApellidoMaterno, Nombres, FechaNacimiento, CURP, Sexo, FechaAfiliacion, Tipo, " +
                "FechaIngresoColectividad, AseguradoTitular, Estado) VALUES(" +
                "@apellidopaterno, @apellidomaterno, @nombres, @fechanacimiento, @curp, @sexo, " +
                "@fechaafiliacion, @tipo, @fechaingresocolectividad, @aseguradotitular, @estado)";
                b.ExecuteCommandQuery(consulta);
                b.AddParameter("@apellidopaterno", apellidopaterno, SqlDbType.NVarChar);
                b.AddParameter("@apellidomaterno", apellidomaterno, SqlDbType.NVarChar);
                b.AddParameter("@nombres", nombres, SqlDbType.NVarChar);
                b.AddParameter("@fechanacimiento", fechanacimiento, SqlDbType.NVarChar);
                b.AddParameter("@curp", curp, SqlDbType.NVarChar);
                b.AddParameter("@sexo", sexo, SqlDbType.NVarChar);
                b.AddParameter("@fechaafiliacion", fechaafiliacion, SqlDbType.NVarChar);
                b.AddParameter("@tipo", tipo, SqlDbType.NVarChar);
                b.AddParameter("@fechaingresocolectividad", fechaingresocolectividad, SqlDbType.NVarChar);
                b.AddParameter("@aseguradotitular", aseguradotitular, SqlDbType.NVarChar);
                b.AddParameter("@estado", estado == "1" ? true : false, SqlDbType.Bit);
                return b.InsertUpdateDeleteWithTransaction();
            }

            public int Actualizar(string id, string apellidopaterno, string apellidomaterno, string nombres, string fechanacimiento, string curp, string sexo,
                string fechaafiliacion, string tipo, string fechaingresocolectividad, string estado)
            {
                string consulta = "UPDATE coasegurados SET ApellidoPaterno=@apellidopaterno, ApellidoMaterno=@apellidomaterno, Nombres=@nombres, " +
                "FechaNacimiento=@fechanacimiento, CURP=@curp, Sexo=@sexo, FechaAfiliacion=@fechaafiliacion, Tipo=@tipo, " +
                "FechaIngresoColectividad=@fechaingresocolectividad, estado=@estado WHERE Id=@id";
                b.ExecuteCommandQuery(consulta);
                b.AddParameter("@apellidopaterno", apellidopaterno, SqlDbType.NVarChar);
                b.AddParameter("@apellidomaterno", apellidomaterno, SqlDbType.NVarChar);
                b.AddParameter("@nombres", nombres, SqlDbType.NVarChar);
                b.AddParameter("@fechanacimiento", fechanacimiento, SqlDbType.NVarChar);
                b.AddParameter("@curp", curp, SqlDbType.NVarChar);
                b.AddParameter("@sexo", sexo, SqlDbType.NVarChar);
                b.AddParameter("@fechaafiliacion", fechaafiliacion, SqlDbType.NVarChar);
                b.AddParameter("@tipo", tipo, SqlDbType.NVarChar);
                b.AddParameter("@fechaingresocolectividad", fechaingresocolectividad, SqlDbType.NVarChar);
                b.AddParameter("@estado", estado == "1" ? true : false, SqlDbType.Bit);
                b.AddParameter("@id", id, SqlDbType.Int);
                return b.InsertUpdateDeleteWithTransaction();
            }

        }
    }
}
