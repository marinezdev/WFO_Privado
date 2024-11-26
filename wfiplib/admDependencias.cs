using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;

namespace wfiplib
{
    public class admDependencias
    {
        DependenciasTablas sp = new DependenciasTablas();

        /// <summary>
        /// Obtener todos los registros
        /// </summary>
        /// <returns></returns>
        public DataTable Seleccionar()
        {
            return sp.Seleccionar();
        }

        public void SeleccionarDependencias_DropDrownList(ref DropDownList dropdownlist)
        {
            LlenarControles.LlenarDropDownList(ref dropdownlist, sp.SeleccionarDependencias(), "Nombre", "IdDependencia");
        }

        /// <summary>
        /// Obtener fila de datos por id del registro
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DataRow Seleccionar(string id)
        {
            return sp.SeleccionarPorId(id).Rows[0];
        }

        /// <summary>
        /// Obtener un valor de un campo buscandolo por su mismo
        /// </summary>
        /// <param name="nombre"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Seleccionar(string nombre, string id=null)
        {
            if (sp.SeleccionarValidar(nombre).Rows.Count > 0)
                return true;
            else
                return false;
        }

        public int Agregar(string nombre, string retenedor)
        {
            return sp.Agregar(nombre, retenedor);
        }

        public int Actualizar(int id, string nombre, string retenedor)
        {
            return sp.Actualizar(id, nombre, retenedor);
        }

        internal class DependenciasTablas //Procesos de acceso a datos
        {
            BaseDeDatos b = new BaseDeDatos();

            public DataTable Seleccionar()
            {
                string consulta = "SELECT * FROM dependencias";
                b.ExecuteCommandQuery(consulta);
                return b.Select();
            }

            public DataTable SeleccionarDependencias()
            {
                string consulta = "SELECT IdDependencia, Nombre FROM Dependencias";
                b.ExecuteCommandQuery(consulta);
                return b.Select();
            }

            public DataTable SeleccionarPorId(string id)
            {
                string consulta = "SELECT * FROM dependencias WHERE iddependencia=@id";
                b.ExecuteCommandQuery(consulta);
                b.AddParameter("@id", id, SqlDbType.Int);
                return b.Select();
            }

            public DataTable SeleccionarValidar(string nombre)
            {
                string consulta = "SELECT nombre FROM dependencias WHERE nombre=@nombre";
                b.ExecuteCommandQuery(consulta);
                b.AddParameter("@nombre", nombre, SqlDbType.NVarChar);
                return b.Select();
            }

            public int Agregar(string nombre, string retenedor)
            {
                string consulta = "INSERT INTO dependencias (Nombre, Retenedor) VALUES(@nombre, @retenedor)";
                b.ExecuteCommandQuery(consulta);
                b.AddParameter("@nombre", nombre, SqlDbType.NVarChar);
                b.AddParameter("@retenedor", retenedor, SqlDbType.NVarChar);
                return b.InsertUpdateDeleteWithTransaction();
            }

            public int Actualizar(int id, string nombre, string retenedor)
            {
                string consulta = "UPDATE depedencias SET Nombre=@nombre, Retenedor=@retenedor WHERE iddependencia=@id";
                b.ExecuteCommandQuery(consulta);
                b.AddParameter("@nombre", nombre, SqlDbType.NVarChar);
                b.AddParameter("@retenedor", retenedor, SqlDbType.NVarChar);
                b.AddParameter("@id", id, SqlDbType.Int);
                return b.InsertUpdateDeleteWithTransaction();
            }

        }


    }
}
