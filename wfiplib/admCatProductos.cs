using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace wfiplib
{
    public class admCatProductos
    {
        public bool nuevo(Productos pDatos)
        {
            bool resultado = false;
            StringBuilder SqlCmd = new StringBuilder("INSERT INTO cat_producto (Id_TipoTramite, Nombre, Descripcion) VALUES(");
           
            SqlCmd.Append("'" + pDatos.Id_TipoTramite + "'");
            SqlCmd.Append(",'" + pDatos.Nombre + "'");
            SqlCmd.Append(",'" + pDatos.Descripcion + "'");
            SqlCmd.Append(");");
            bd BD = new bd();
            resultado = BD.ejecutaCmd(SqlCmd.ToString());

            BD.cierraBD();

            return resultado;
        }

        public Productos carga(int pIdCatProducto)
        {
            Productos respuesta = new Productos();
            bd BD = new bd();
            DataTable datos = BD.leeDatos("SELECT * FROM cat_producto WHERE IdCatProducto=" + pIdCatProducto.ToString());
            if (datos.Rows.Count > 0) { respuesta = arma(datos.Rows[0]); }
            datos.Dispose();
            BD.cierraBD();
            return respuesta;
        }

        public List<Productos> ListaProducto()
        {
            List<Productos> respuesta = new List<Productos>();
            bd BD = new bd();
            DataTable datos = BD.leeDatos("SELECT * FROM cat_producto order by Nombre");
            foreach (DataRow reg in datos.Rows) { respuesta.Add(arma(reg)); }
            datos.Dispose();
            BD.cierraBD();
            return respuesta;
        }

        private Productos arma(DataRow pRegistro)
        {
            Productos respuesta = new Productos();
            if (!pRegistro.IsNull("IdCatProducto")) respuesta.idCatProducto = Convert.ToInt32(pRegistro["IdCatProducto"]);
            if (!pRegistro.IsNull("Id_TipoTramite")) respuesta.Id_TipoTramite = Convert.ToString(pRegistro["Id_TipoTramite"]);
            if (!pRegistro.IsNull("Nombre")) respuesta.Nombre = Convert.ToString(pRegistro["Nombre"]);
            if (!pRegistro.IsNull("Descripcion")) respuesta.Descripcion = Convert.ToString(pRegistro["Descripcion"]);
            return respuesta;
        }

        public bool Existe(string pNombre)
        {
            bool resultado = false;
            bd BD = new bd();
            DataTable datos = BD.leeDatos("SELECT * FROM cat_producto Where Nombre= '" + pNombre + "'");
            resultado = (datos.Rows.Count > 0);
            datos.Dispose();
            BD.cierraBD();
            return resultado;
        }

        public void modifica(Productos oDt)
        {
            StringBuilder SqlCmd = new StringBuilder("UPDATE cat_producto SET");
            SqlCmd.Append(" Nombre='" + oDt.Nombre + "'");
            SqlCmd.Append(",Id_TipoTramite='" + oDt.Id_TipoTramite + "'");
            SqlCmd.Append(",Descripcion='" + oDt.Descripcion + "'");
            SqlCmd.Append(" WHERE idCatProducto=" + oDt.idCatProducto);
            bd BD = new bd();
            BD.ejecutaCmd(SqlCmd.ToString());
            BD.cierraBD();
        }
    }

    public class Productos
    {
        private int midCatProducto = 0;
        public int idCatProducto { get { return midCatProducto; } set { midCatProducto = value; } }
        private string mId_TipoTramite = String.Empty;
        public string Id_TipoTramite { get { return mId_TipoTramite; } set { mId_TipoTramite = value; } }
        private string mNombre = String.Empty;
        public string Nombre { get { return mNombre; } set { mNombre = value; } }
        private string mDescripcion = String.Empty;
        public string Descripcion { get { return mDescripcion; } set { mDescripcion = value; } }


    }
}

