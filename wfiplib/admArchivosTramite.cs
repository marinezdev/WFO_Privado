using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;

namespace wfiplib
{
    public class admArchivosTramite
    {
        BaseDeDatos b = new BaseDeDatos();

        public bool SeleccionarContadosPorFolio(string folio)
        {
            string consulta = "SELECT count(*) FROM archivostramite WHERE folio=@folio";
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@folio", folio, SqlDbType.NVarChar);
            if (b.Select().Rows.Count > 0)
                return true;
            else
                return false;
        }

        private DataTable SeleccionarPorFolio(string folio)
        {
            string consulta = "SELECT * FROM archivostramite WHERE folio=@folio";
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@folio", folio, SqlDbType.NVarChar);
            return b.Select();
        }

        public void SeleccionarPorPoliza_GridView(string folio, ref GridView gridview)
        {
            LlenarControles.LlenarGridView(ref gridview, SeleccionarPorFolio(folio));
        }

        public int Agregar(string folio, string archivo)
        {
            string consulta = "INSERT INTO archivostramite VALUES(@folio, @archivo)";
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@folio", folio, SqlDbType.NVarChar);
            b.AddParameter("@archivo", archivo, SqlDbType.NVarChar);
            return b.InsertUpdateDeleteWithTransaction();
        }

        public int Quitar(string poliza, string archivo)
        {
            string consulta = "DELETE FROM archivostramite WHERE poliza=@poliza AND archivo=@archivo";
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@poliza", poliza, SqlDbType.NVarChar);
            b.AddParameter("@archivo", archivo, SqlDbType.NVarChar);
            return b.InsertUpdateDeleteWithTransaction();
        }




    }
}
