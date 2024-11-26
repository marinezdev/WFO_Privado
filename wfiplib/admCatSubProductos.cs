using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace wfiplib
{
    public class admCatSubProductos
    {
        public bool nuevo(SubProductos pDatos)
        {
            bool resultado = false;
            StringBuilder SqlCmd = new StringBuilder("INSERT INTO cat_subproducto (IdCatProducto, Nombre, Descripcion)");

            SqlCmd.Append(" VALUES (");
            SqlCmd.Append("'" + pDatos.IdCatProducto + "'");
            SqlCmd.Append(",'" + pDatos.Nombre + "'");
            SqlCmd.Append(",'" + pDatos.Descripcion + "'");
            SqlCmd.Append(");");
            bd BD = new bd();
            resultado = BD.ejecutaCmd(SqlCmd.ToString());

            BD.cierraBD();

            return resultado;
        }

        public SubProductos carga(int pIdCatSubProducto)
        {
            SubProductos respuesta = new SubProductos();
            bd BD = new bd();
            DataTable datos = BD.leeDatos("SELECT * FROM cat_Subproducto WHERE IdCatSubProducto=" + pIdCatSubProducto.ToString());
            if (datos.Rows.Count > 0) { respuesta = arma(datos.Rows[0]); }
            datos.Dispose();
            BD.cierraBD();
            return respuesta;
        }

        public List<SubProductos> ListaSubProductos()
        {
            List<SubProductos> respuesta = new List<SubProductos>();
            bd BD = new bd();
            DataTable datos = BD.leeDatos("SELECT * FROM cat_subproducto order by IdCatProducto");
            foreach (DataRow reg in datos.Rows) { respuesta.Add(arma(reg)); }
            datos.Dispose();
            BD.cierraBD();
            return respuesta;
        }

        private SubProductos arma(DataRow pRegistro)
        {
            SubProductos respuesta = new SubProductos();
            if (!pRegistro.IsNull("IdCatSubProducto")) respuesta.IdCatSubProducto = Convert.ToInt32(pRegistro["IdCatSubProducto"]);
            if (!pRegistro.IsNull("IdCatProducto")) respuesta.IdCatProducto = Convert.ToString(pRegistro["IdCatProducto"]);
            if (!pRegistro.IsNull("Nombre")) respuesta.Nombre = Convert.ToString(pRegistro["Nombre"]);
            if (!pRegistro.IsNull("Descripcion")) respuesta.Descripcion = Convert.ToString(pRegistro["Descripcion"]);
            return respuesta;
        }

        public bool Existe(string pNombre)
        {
            bool resultado = false;
            bd BD = new bd();
            DataTable datos = BD.leeDatos("SELECT * FROM cat_subproducto Where Nombre= '" + pNombre + "'");
            resultado = (datos.Rows.Count > 0);
            datos.Dispose();
            BD.cierraBD();
            return resultado;
        }

        public void modifica(SubProductos oDt)
        {
            StringBuilder SqlCmd = new StringBuilder("UPDATE cat_subproducto SET");
            SqlCmd.Append(" Nombre='" + oDt.Nombre + "'");
            SqlCmd.Append(",Descripcion='" + oDt.Descripcion + "'");
            SqlCmd.Append(" WHERE IdCatSubProducto=" + oDt.IdCatSubProducto);
            bd BD = new bd();
            BD.ejecutaCmd(SqlCmd.ToString());
            BD.cierraBD();
        }

        public DataTable cartgaCatProducto()
        {
            bd BD = new bd();
            StringBuilder SqlCmd = new StringBuilder("SELECT -1 AS IdCatProducto, '' AS Id_TipoTramite,'[Selecciona]' AS Nombre, '' AS Descripcion UNION ALL SELECT * FROM dbo.cat_producto");
            DataTable datos = BD.leeDatos(SqlCmd.ToString());
            BD.cierraBD();
            return datos;
        }
    }

    public class SubProductos
    {
        private int mIdCatSubProducto = 0;
        public int IdCatSubProducto { get { return mIdCatSubProducto; } set { mIdCatSubProducto = value; } }
        private string mIdCatProducto = String.Empty;
        public string IdCatProducto { get { return mIdCatProducto; } set { mIdCatProducto = value; } }
        private string mNombre = String.Empty;
        public string Nombre { get { return mNombre; } set { mNombre = value; } }
        private string mDescripcion = String.Empty;
        public string Descripcion { get { return mDescripcion; } set { mDescripcion = value; } }
    }
}
