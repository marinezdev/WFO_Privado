using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace wfiplib.Tablas
{
    public abstract class AbstractasABC
    {
        public abstract DataTable Seleccionar();

        public abstract DataRow SeleccionarPorId(int id);

        public abstract string SeleccionarDato(string id);

        public abstract int Agregar();

        public abstract int Actualizar(int id);

        public abstract int Eliminar(int id);

        public virtual string SeleccionarPorNombre(int id)
        {
            return "";
        }
    }
}
