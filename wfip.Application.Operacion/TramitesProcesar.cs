using System;
using System.Data;
using wfiplib;

namespace wfip.Application.Operacion
{
    public class TramitesProcesar
    {
        Data.TramitesProcesar _procesar = new Data.TramitesProcesar();

        public DataSet ProcesarTramite(wfiplib.tramiteMesa oTramiteMesa, wfiplib.E_EstadoTramite statusTramite, wfiplib.E_EstadoMesa statusMesa, string motivosRechazos)
        {
            return _procesar.ProcesarTramite(oTramiteMesa, statusTramite, statusMesa, motivosRechazos);
        }
    }
}
