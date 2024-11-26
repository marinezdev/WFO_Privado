using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using w = wfiplib.Tablas.Institucional.Privado;

namespace wfiplib.Tablas
{
    public class Comun
    {
        public w.InsServicios insservicios;
        public w.InsServicioDetalle insserviciodetalle;
        public w.InsServiciosEntity insserviciosEntity;
        public w.InsServiciosDetalleEntity insserviciosdetalleEntity;

        public Comun()
        {
            insservicios = new w.InsServicios();
            insserviciodetalle = new w.InsServicioDetalle();
            insserviciosEntity = new w.InsServiciosEntity();
            insserviciosdetalleEntity = new w.InsServiciosDetalleEntity();
        }
            

    }
}
