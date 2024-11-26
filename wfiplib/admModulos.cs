using System.Collections.Generic;
using System.Data;
using System.Web.UI.WebControls;

namespace wfiplib
{
    public class admModulos
    {
        bd b = new bd();
        DataTable dt;

        private List<Modulos> ListaModulos()
        {
            dt = new DataTable();
            string consulta = "SELECT * FROM modulos";
            dt = b.leeDatos(consulta);

            List<Modulos> mods = new List<Modulos>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Modulos modls = new Modulos()
                {
                    IdModulo    = int.Parse(dt.Rows[i]["IdModulo"].ToString()),
                    Nombre      = dt.Rows[i]["Nombre"].ToString(),
                    IdPertenece = int.Parse(dt.Rows[i]["IdPertenece"].ToString()),
                    IdPermisos  = int.Parse(dt.Rows[i]["IdPermisos"].ToString()),
                    Activo      = dt.Rows[i]["Activo"].ToString() == "True" ? true : false
                };
                mods.Add(modls);
            }
            return mods;
        }

        public void ObtenerModulos_TreeView(ref TreeView treeview)
        {
            LlenarControles.LlenarTreeViewModulos(ListaModulos(), null, ref treeview);
        }


    }

    /// <summary>
    /// Propiedades
    /// </summary>
    public class Modulos
    {
        public int IdModulo { get; set; }
        public string Nombre { get; set; }
        public int IdPertenece { get; set; }
        public int IdPermisos { get; set; }
        public bool Activo { get; set; }
    }
}
