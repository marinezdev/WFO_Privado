using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace wfiplib.Tablas.Institucional.Privado
{
    public class InsTramiteMesa
    {
        BaseDeDatos b = new BaseDeDatos();

        admTramiteMesa admtramitemesa = new admTramiteMesa();

        private int TramiteMesaAgregar(int idtramite, int idmesa)
        {
            string consulta = "INSERT INTO tramiteMesa (Id, IdTramite, IdMesa, FechaRegistro, Estado, IdRechazo) VALUES (@id, @idtramite, @idmesa, getdate(), 0, 0)";
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@id", admtramitemesa.siguienteId(), SqlDbType.Int);
            b.AddParameter("@idtramite", idtramite, SqlDbType.Int);
            b.AddParameter("@idmesa", idmesa, SqlDbType.Int);
            return b.InsertUpdateDelete();
        }

        public bool RegistraPaso(int pIdTramite, wfiplib.E_EstadoMesa statusMesa)
        {
            bool resultado = false;
            try
            {
                wfiplib.tramiteP oTramite = (new wfiplib.admTramite()).carga(pIdTramite);

                List<wfiplib.mesa> lstSiguientesMesas = (new wfiplib.admMesa()).daSiguienteMesa(oTramite.IdFlujo, 0, oTramite.Id);
                if (lstSiguientesMesas.Count > 0)
                {
                    wfiplib.tramiteMesa siguienteMesa = new wfiplib.tramiteMesa();
                    siguienteMesa.IdTramite = oTramite.Id;
                    siguienteMesa.Estado = wfiplib.E_EstadoMesa.Registro;

                    wfiplib.admTramiteMesa oAdmTramiteMesa = new wfiplib.admTramiteMesa();
                    foreach (wfiplib.mesa oMesa in lstSiguientesMesas)
                    {
                        siguienteMesa.IdMesa = oMesa.Id;
                        TramiteMesaAgregar(pIdTramite, siguienteMesa.IdMesa);
                    }
                    resultado = true;
                }
            }
            catch
            {
                resultado = false;
            }
            return resultado;
        }
    }
}
