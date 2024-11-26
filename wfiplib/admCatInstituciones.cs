using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wfiplib
{
    public class admCatInstituciones
    {
        BaseDeDatos b = new BaseDeDatos();
        public DataTable Seleccionar()
        {
            DataTable dt = new DataTable();
            b.ExecuteCommandSP("cat_instituciones_Seleccionar");
            return b.Select();

        } 
    }
}
