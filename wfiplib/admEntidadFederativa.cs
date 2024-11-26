using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;

namespace wfiplib
{
    public class admEntidadFederativa
    {
        EntidadFederativaTablas ef = new EntidadFederativaTablas();

        public void SeleccionarDependencias_DropDrownList(ref DropDownList dropdownlist)
        {
            LlenarControles.LlenarDropDownList(ref dropdownlist,  ef.Seleccionar(), "Nombre", "Nombre");
        }

        public bool ValidarEntidadFederativaPorValor(string valor)
        {
            if (ef.SeleccionarValidarValor(valor).Rows.Count > 0)
                return true;
            else
                return false;
        }

        internal class EntidadFederativaTablas
        {
            BaseDeDatos b = new BaseDeDatos();

            public DataTable Seleccionar()
            {
                string consulta = "SELECT valor, nombre FROM entidadfederativa";
                b.ExecuteCommandQuery(consulta);
                return b.Select();
            }

            public DataTable SeleccionarValidarValor(string valor)
            {
                string consulta = "SELECT valor FROM entidadfederativa WHERE valor=@valor";
                b.ExecuteCommandQuery(consulta);
                b.AddParameter("@valor", valor, SqlDbType.NChar);
                return b.Select();
            }

        }
    }
}
