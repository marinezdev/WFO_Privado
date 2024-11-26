using System;

namespace wfiplib
{
    public class AdmBuzon
    {
    }

    public class Buzon
    {
        private int mId = 0;
        public int Id { get { return mId; } set { mId = value; } }
        private string mNombre = String.Empty;
        public string Nombre { get { return mNombre; } set { mNombre = value; } }
        private DateTime mFechaRegistro = new DateTime(2000, 1, 1, 0, 0, 0);
        public DateTime FechaRegistro { get { return mFechaRegistro; } set { mFechaRegistro = value; } }
        private E_Estado _Estado = E_Estado.Activo;
        public E_Estado Estado { get { return _Estado; } set { _Estado = value; }  }
    }
}
