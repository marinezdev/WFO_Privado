using System;
using System.Data;
using wfiplib;

namespace wfip.Data
{
    public class TramitesProcesar
    {
        ManejoDatos b = new ManejoDatos();

        public DataSet ProcesarTramite(wfiplib.tramiteMesa oTramiteMesa, wfiplib.E_EstadoTramite statusTramite, wfiplib.E_EstadoMesa statusMesa, string motivosRechazos)
        {
            DataSet procesamiento = null;

#if DEBUG
            System.Diagnostics.Debug.WriteLine("exec WFOTramiteProcesar ");
            System.Diagnostics.Debug.WriteLine("@IdTramite = " + oTramiteMesa.IdTramite.ToString());
            System.Diagnostics.Debug.WriteLine(", @IdMesa = " + oTramiteMesa.IdMesa.ToString());
            System.Diagnostics.Debug.WriteLine(", @IdUsuario = " + oTramiteMesa.IdUsuario.ToString());
            System.Diagnostics.Debug.WriteLine(", @IdStatusMesa = " + ((int)statusMesa).ToString());
            System.Diagnostics.Debug.WriteLine(", @ObservacionPub = '" + oTramiteMesa.ObservacionPublica + "'");
            System.Diagnostics.Debug.WriteLine(", @ObservacionPriv = '" + oTramiteMesa.ObservacionPrivada + "'");
            System.Diagnostics.Debug.WriteLine(", @MotivosRechazo = '" + motivosRechazos + "'");
#endif

            b.ExecuteCommandSP("WFOTramiteProcesar");
            b.AddParameter("@IdTramite", oTramiteMesa.IdTramite, SqlDbType.Int);
            b.AddParameter("@IdMesa", oTramiteMesa.IdMesa, SqlDbType.Int);
            b.AddParameter("@IdUsuario", oTramiteMesa.IdUsuario, SqlDbType.Int);
            b.AddParameter("@IdStatusMesa", (int)statusMesa, SqlDbType.Int);
            b.AddParameter("@ObservacionPub", oTramiteMesa.ObservacionPublica, SqlDbType.NVarChar);
            b.AddParameter("@ObservacionPriv", oTramiteMesa.ObservacionPrivada, SqlDbType.NVarChar);
            b.AddParameter("@MotivosRechazo", motivosRechazos, SqlDbType.NVarChar);
            // List<Propiedades.Flujo> resultado = new List<Propiedades.Flujo>();
            // var reader = b.ExecuteReader();
            // while (reader.Read())
            // {
            //    Propiedades.Flujo item = new Propiedades.Flujo()
            //    {
            //        Id = Convert.ToInt32(reader["Id"].ToString()),
            //        Nombre = reader["Nombre"].ToString().ToUpper(),
            //    };
            //    resultado.Add(item);
            // }
            // reader = null;
            // b.ConnectionCloseToTransaction();
            procesamiento = b.SelectExecuteFunctions();
            return procesamiento;

        }
    }
}
