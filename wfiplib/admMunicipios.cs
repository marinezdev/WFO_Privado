using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;

namespace wfiplib
{
    public class admMunicipios
    {
        MunicipiosTablas mt = new MunicipiosTablas();

        public void Seleccionar_DropDownList(ref DropDownList dropdownlist)
        {
            LlenarControles.LlenarDropDownList(ref dropdownlist, mt.Seleccionar(), "Municipio", "Codigo");
        }

        public void SeleccionarPorEstado_DropDownList(ref DropDownList dropdownlist, string estado)
        {
            LlenarControles.LlenarDropDownList(ref dropdownlist, mt.SeleccionarPorEstado(estado), "Municipio", "Codigo");
        }


        internal class MunicipiosTablas
        {
            BaseDeDatos b = new BaseDeDatos();

            public DataTable Seleccionar()
            {
                string consulta = "SELECT * FROM municipios";
                b.ExecuteCommandQuery(consulta);
                return b.Select();
            }

            public DataTable SeleccionarPorEstado(string estado)
            {
                string consulta = "SELECT * FROM municipios WHERE estado=@estado";
                b.ExecuteCommandQuery(consulta);
                b.AddParameter("@estado", estado, SqlDbType.Char);
                return b.Select();
            }
        }
    }
}
