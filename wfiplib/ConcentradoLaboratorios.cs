using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace wfiplib
{
    public class ConcentradoLaboratorios
    {
        //private List<cat_proveedor_accesop> _cpa;
        private catProveedorP _cpp;

        //public List<cat_proveedor_accesop> CPA
        //{
        //    get { return _cpa; }
        //    set { _cpa = value; }
        //}

        /// <summary>
        /// Propiedades del proveedor
        /// </summary>
        public catProveedorP CPP
        {
            get { return _cpp; }
            set { _cpp = value; }
        }

        public void Inicializar()
        {
            //_cpa = new List<cat_proveedor_accesop>();
            _cpp = new catProveedorP();
        }
    }
}
