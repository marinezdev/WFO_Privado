using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace wfiplib.Tablas
{
    public class Implementacion01 : AbstractasABC
    {
        DataTable dt;

        public override DataTable Seleccionar()
        {
            dt = new DataTable();
            return dt;
        }

        public override DataRow SeleccionarPorId(int id)
        {
            dt = new DataTable();
            return dt.Rows[0];
        }

        public override string SeleccionarDato(string id)
        {
            string devuelto = string.Empty;
            return devuelto;
        }


        public override int Agregar()
        {
            int i = 0;
            return i;
        }
        public override int Actualizar(int id)
        {
            int i = 0;
            return i;
        }

        public override int Eliminar(int id)
        {
            int i = 0;
            return i;
        }



        
    }
}
