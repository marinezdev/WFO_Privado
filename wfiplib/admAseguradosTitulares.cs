using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace wfiplib
{
    public class admAseguradosTitulares
    {
        AseguradosTitularesTablas att = new AseguradosTitularesTablas();

        /// <summary>
        /// Obtiene todos los AseguradosTitulares
        /// </summary>
        /// <returns></returns>
        public DataTable Seleccionar()
        {
            return att.Seleccionar();
        }

        /// <summary>
        /// Obtiene una fila con los datos del id proporcionado
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DataRow Seleccionar(string id)
        {
            return att.SeleccionarPorId(id).Rows[0];
        }

        /// <summary>
        /// Valida si el valor de un campo existe
        /// </summary>
        /// <param name="curp">CURP a buscar</param>
        /// <param name="id">valor null</param>
        /// <returns>Verdadero o falso</returns>
        public bool Seleccionar(string curp, string id=null)
        {
            if (att.SeleccionarPorCURP(curp).Rows.Count > 0)
                return true;
            else
                return false;
        }

        public int Agregar(string poliza, string dependencia, string apellidopaterno, string apellidomaterno, string nombres, string fechanacimiento, string rfc,
                string curp, string sexo, string entidadfederativa, string municipio, string niveltabular, string percepcionordinariabruta, string eventual, string estado)
        {
            return att.Agregar(poliza, dependencia, apellidopaterno, apellidomaterno, nombres, fechanacimiento, rfc,
                curp, sexo, entidadfederativa, municipio, niveltabular, percepcionordinariabruta, eventual, estado);
        }

        public int Actualizar(string id, string poliza, string dependencia, string apellidopaterno, string apellidomaterno, string nombres, string fechanacimiento, string rfc,
                string curp, string sexo, string entidadfederativa, string municipio, string niveltabular, string percepcionordinariabruta, string eventual, string estado)
        {
            return att.Actualizar(id, poliza, dependencia, apellidopaterno, apellidomaterno, nombres, fechanacimiento, rfc, curp, sexo, entidadfederativa, municipio, 
                niveltabular, percepcionordinariabruta, eventual, estado);
        }

        internal class AseguradosTitularesTablas
        {
            BaseDeDatos b = new BaseDeDatos();

            public DataTable Seleccionar()
            {
                string consulta = "SELECT a.Id, a.Poliza, b.Nombre AS Dependencia, a.ApellidoPaterno,	a.ApellidoMaterno,	a.Nombres,	a.FechaNacimiento,	a.RFC,	a.CURP,	a.Sexo,	a.EntidadFederativa,	a.Municipio,	a.NivelTabular,	a.PercepcionOrdinariaBruta,	a.Eventual, a.Estado " +
                "FROM aseguradostitulares a, dependencias b " +
                "WHERE a.Dependencia = b.IdDependencia";
                b.ExecuteCommandQuery(consulta);
                //b.ExecuteCommandSP("AseguradosTitulares_Seleccionar");
                return b.Select();
            }

            public DataTable SeleccionarPorId(string id)
            {
                string consulta = "SELECT * FROM aseguradostitulares WHERE id=@id";
                b.ExecuteCommandQuery(consulta);
                //b.ExecuteCommandSP("AseguradosTitulares_SeleccionarPorId");
                b.AddParameter("@id", id, SqlDbType.Int);
                return b.Select();
            }

            public DataTable SeleccionarPorPoliza(string poliza)
            {
                string consulta = "SELECT * FROM aseguradostitulares WHERE poliza=@poliza";
                b.ExecuteCommandQuery(consulta);
                b.AddParameter("@poliza", poliza, SqlDbType.NVarChar);
                return b.Select();
            }

            public DataTable SeleccionarPorDependencia(int dependencia)
            {
                string consulta = "SELECT * FROM aseguradostitulares WHERE Dependencia=@dependencia";
                b.ExecuteCommandQuery(consulta);
                b.AddParameter("@dependencia", dependencia, SqlDbType.Int);
                return b.Select();
            }

            public DataTable SeleccionarPorCURP(string curp)
            {
                string consulta = "SELECT * FROM aseguradostitulares WHERE curp=@curp";
                b.ExecuteCommandQuery(consulta);
                b.AddParameter("@curp", curp, SqlDbType.NVarChar);
                return b.Select();
            }

            public int Agregar(string poliza, string dependencia, string apellidopaterno, string apellidomaterno, string nombres, string fechanacimiento, string rfc,
                string curp, string sexo, string entidadfederativa, string municipio, string niveltabular, string percepcionordinariabruta, string eventual, string estado)
            {
                string consulta = " " +
                "IF NOT EXISTS(SELECT * FROM AseguradosTitulares WHERE nombres=@nombres AND ApellidoPaterno=@apellidopaterno AND ApellidoMaterno=@apellidomaterno AND rfc=@rfc and curp=@curp and FechaNacimiento=@fechanacimiento)" +
                "BEGIN " +
                "   INSERT INTO aseguradostitulares (poliza, dependencia, apellidopaterno, apellidomaterno," +
                "nombres, fechanacimiento, rfc, curp,sexo,	entidadfederativa, municipio,	" +
                "niveltabular, percepcionordinariabruta, eventual, estado) VALUES(@poliza, @dependencia, @apellidopaterno, @apellidomaterno," +
                "@nombres, @fechanacimiento, @rfc, @curp, @sexo, @entidadfederativa, @municipio," +
                "@niveltabular, @percepcionordinariabruta, @eventual, @estado)" +
                "END "; 
                b.ExecuteCommandQuery(consulta);
                b.AddParameter("@poliza", poliza, SqlDbType.NVarChar);
                b.AddParameter("@dependencia", dependencia, SqlDbType.NVarChar);
                b.AddParameter("@apellidopaterno", apellidopaterno, SqlDbType.NVarChar);
                b.AddParameter("@apellidomaterno", apellidomaterno, SqlDbType.NVarChar);
                b.AddParameter("@nombres", nombres, SqlDbType.NVarChar);
                b.AddParameter("@fechanacimiento", fechanacimiento, SqlDbType.NVarChar);
                b.AddParameter("@rfc", rfc, SqlDbType.NVarChar);
                b.AddParameter("@curp", curp, SqlDbType.NVarChar);
                b.AddParameter("@sexo", sexo, SqlDbType.NVarChar);
                b.AddParameter("@entidadfederativa", entidadfederativa, SqlDbType.NVarChar);
                b.AddParameter("@municipio", municipio, SqlDbType.NVarChar);
                b.AddParameter("@niveltabular", niveltabular, SqlDbType.NVarChar);
                b.AddParameter("@percepcionordinariabruta", percepcionordinariabruta, SqlDbType.NVarChar);
                b.AddParameter("@eventual", eventual, SqlDbType.NVarChar);
                b.AddParameter("@estado", estado == "1" ? true : false, SqlDbType.Bit);
                return b.InsertUpdateDeleteWithTransaction();
            }
            public int Actualizar(string id, string poliza, string dependencia, string apellidopaterno, string apellidomaterno, string nombres, string fechanacimiento, string rfc,
                string curp, string sexo, string entidadfederativa, string municipio, string niveltabular, string percepcionordinariabruta, string eventual, string estado)
            {
                string consulta = "UPDATE aseguradostitulares SET poliza=@poliza, dependencia=@dependencia,	apellidopaterno=@apellidopaterno, apellidomaterno=@apellidomaterno," +
                "nombres=@nombres, fechanacimiento=@fechanacimiento, rfc=@rfc, curp=@curp,sexo=@sexo,	entidadfederativa=@entidadfederativa, municipio=@municipio,	" +
                "niveltabular=@niveltabular, percepcionordinariabruta=@percepcionordinariabruta, eventual=@eventual, estado=@estado WHERE Id=@id";
                b.ExecuteCommandQuery(consulta);
                b.AddParameter("@id", id, SqlDbType.Int);
                b.AddParameter("@poliza", poliza, SqlDbType.NVarChar);
                b.AddParameter("@dependencia", dependencia, SqlDbType.NVarChar);
                b.AddParameter("@apellidopaterno", apellidopaterno, SqlDbType.NVarChar);
                b.AddParameter("@apellidomaterno", apellidomaterno, SqlDbType.NVarChar);
                b.AddParameter("@nombres", nombres, SqlDbType.NVarChar);
                b.AddParameter("@fechanacimiento", fechanacimiento, SqlDbType.NVarChar);
                b.AddParameter("@rfc", rfc, SqlDbType.NVarChar);
                b.AddParameter("@curp", curp, SqlDbType.NVarChar);
                b.AddParameter("@sexo", sexo, SqlDbType.NVarChar);
                b.AddParameter("@entidadfederativa", entidadfederativa, SqlDbType.NVarChar);
                b.AddParameter("@municipio", municipio, SqlDbType.NVarChar);
                b.AddParameter("@niveltabular", niveltabular, SqlDbType.NVarChar);
                b.AddParameter("@percepcionordinariabruta", percepcionordinariabruta, SqlDbType.NVarChar);
                b.AddParameter("@eventual", eventual, SqlDbType.NVarChar);
                b.AddParameter("@estado", estado == "1" ? true : false, SqlDbType.Bit);
                return b.InsertUpdateDeleteWithTransaction();
            }



        }
    }
}
