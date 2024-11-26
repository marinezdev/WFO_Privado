using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace wfiplib
{
    public class admStatusTramite
    {
        public DataTable statusTramite_Seleccionar()
        {
            DataTable dt = new DataTable();
            bd ExportarExcel = new bd();
            string qExportarExcel = "EXEC statusTramite_Seleccionar";
            dt = ExportarExcel.leeDatos(qExportarExcel);
            ExportarExcel.cierraBD();
            return dt;
        }
    }
}
