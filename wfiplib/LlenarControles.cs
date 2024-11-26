using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace wfiplib
{
    /// <summary>
    /// Llenado de controles de todo tipo
    /// </summary>
    public static class LlenarControles
    {
        /// <summary>
        /// Llena un treeview de forma recursiva e infinita para modulos
        /// </summary>
        /// <param name="lista">List<> con los datos</param>
        /// <param name="nodoPadre">Al inicio con null</param>
        public static void LlenarTreeViewModulos(IEnumerable<Modulos> lista, TreeNode nodoPadre, ref TreeView treeview)
        {
            var nodos = lista.Where(nodosObtenidos => nodoPadre == null ? nodosObtenidos.IdPertenece == 0 : nodosObtenidos.IdPertenece == int.Parse(nodoPadre.Value));
            foreach (var node in nodos)
            {
                TreeNode nuevoNodo = new TreeNode(node.Nombre, node.IdModulo.ToString());
                if (nodoPadre == null)
                {
                    nuevoNodo.Checked = node.Activo;
                    treeview.Nodes.Add(nuevoNodo);
                }
                else
                {
                    nuevoNodo.Checked = node.Activo;
                    nodoPadre.ChildNodes.Add(nuevoNodo);
                }
                LlenarTreeViewModulos(lista, nuevoNodo, ref treeview);
            }
        }

        /// <summary>
        /// Llena un treeview de forma recursiva e infita para menú
        /// </summary>
        /// <param name="lista">List<> con los datos</param>
        /// <param name="nodoPadre">Al inicio con null</param>
        /// <param name="treeview">control referenciado</param>
        public static void LlenarTreeViewMenu(IEnumerable<MenuPropiedades> lista, TreeNode nodoPadre, ref TreeView treeview)
        {
            var nodos = lista.Where(nodosInternos => nodoPadre == null ? nodosInternos.PerteneceA == 0 : nodosInternos.PerteneceA == int.Parse(nodoPadre.Value));
            foreach (var node in nodos)
            {
                TreeNode nuevoNodo = new TreeNode(node.Descripcion, node.IdMenu.ToString());
                if (nodoPadre == null)
                {
                    nuevoNodo.Checked = node.Activo;
                    treeview.Nodes.Add(nuevoNodo);
                }
                else
                {
                    nuevoNodo.Checked = node.Activo;
                    nodoPadre.ChildNodes.Add(nuevoNodo);
                }
                LlenarTreeViewMenu(lista, nuevoNodo, ref treeview);
            }
        }

        public static void LlenarTreeViewPermisos(IEnumerable<PermisosEntity> lista, TreeNode nodoPadre, ref TreeView treeview)
        {
            //var nodos = lista.Where(nodosInternos => nodoPadre == null ? nodosInternos.PerteneceA == 0 : nodosInternos.PerteneceA == int.Parse(nodoPadre.Value));
            //foreach (var node in nodos)
            //{
            //    TreeNode nuevoNodo = new TreeNode(node.Descripcion, node.IdMenu.ToString());
            //    if (nodoPadre == null)
            //    {
            //        nuevoNodo.Checked = node.Activo;
            //        treeview.Nodes.Add(nuevoNodo);
            //    }
            //    else
            //    {
            //        nuevoNodo.Checked = node.Activo;
            //        nodoPadre.ChildNodes.Add(nuevoNodo);
            //    }
            //    LlenarTreeViewPermisos(lista, nuevoNodo, ref treeview);
            //}
        }

        /// <summary>
        /// LLena un treview con hasta 11 niveles de profundidad
        /// </summary>
        /// <param name="treeview">Nombre del treeview a llenar</param>
        /// <param name="dt">DataTable con los datos o función de llenado</param>
        private static void LLenarTreeViewDataTable(TreeView treeview, DataTable dt)
        {
            foreach (DataRow dr1 in dt.Rows)
            {
                TreeNode tn = new TreeNode();
                if (dr1["IdPertenece"].ToString() == "0")
                {
                    tn.Text = dr1["Nombre"].ToString();
                    treeview.Nodes.Add(tn);

                    foreach (DataRow dr2 in dt.Rows)
                    {
                        if (dr2["IdPertenece"].ToString() == dr1["IdModulo"].ToString())
                        {
                            TreeNode childnodes1 = new TreeNode();
                            childnodes1.Text = dr2["Nombre"].ToString();
                            tn.ChildNodes.Add(childnodes1);

                            foreach (DataRow dr3 in dt.Rows)
                            {
                                if (dr3["IdPertenece"].ToString() == dr2["IdModulo"].ToString())
                                {
                                    TreeNode childnodes2 = new TreeNode();
                                    childnodes2.Text = dr3["Nombre"].ToString();
                                    childnodes1.ChildNodes.Add(childnodes2);

                                    foreach (DataRow dr4 in dt.Rows)
                                    {
                                        if (dr4["IdPertenece"].ToString() == dr3["IdModulo"].ToString())
                                        {
                                            TreeNode childnodes3 = new TreeNode();
                                            childnodes3.Text = dr4["Nombre"].ToString();
                                            childnodes2.ChildNodes.Add(childnodes3);

                                            foreach (DataRow dr5 in dt.Rows)
                                            {
                                                if (dr5["IdPertenece"].ToString() == dr4["IdModulo"].ToString())
                                                {
                                                    TreeNode childnodes4 = new TreeNode();
                                                    childnodes4.Text = dr5["Nombre"].ToString();
                                                    childnodes3.ChildNodes.Add(childnodes4);

                                                    foreach (DataRow dr6 in dt.Rows)
                                                    {
                                                        if (dr6["IdPertenece"].ToString() == dr5["IdModulo"].ToString())
                                                        {
                                                            TreeNode childnodes5 = new TreeNode();
                                                            childnodes5.Text = dr6["Nombre"].ToString();
                                                            childnodes4.ChildNodes.Add(childnodes5);

                                                            foreach (DataRow dr7 in dt.Rows)
                                                            {
                                                                if (dr7["IdPertenece"].ToString() == dr6["IdModulo"].ToString())
                                                                {
                                                                    TreeNode childnodes6 = new TreeNode();
                                                                    childnodes6.Text = dr7["Nombre"].ToString();
                                                                    childnodes5.ChildNodes.Add(childnodes6);

                                                                    foreach (DataRow dr8 in dt.Rows)
                                                                    {
                                                                        if (dr8["IdPertenece"].ToString() == dr7["IdModulo"].ToString())
                                                                        {
                                                                            TreeNode childnodes7 = new TreeNode();
                                                                            childnodes7.Text = dr8["Nombre"].ToString();
                                                                            childnodes6.ChildNodes.Add(childnodes7);

                                                                            foreach (DataRow dr9 in dt.Rows)
                                                                            {
                                                                                if (dr9["IdPertenece"].ToString() == dr8["IdModulo"].ToString())
                                                                                {
                                                                                    TreeNode childnodes8 = new TreeNode();
                                                                                    childnodes8.Text = dr9["Nombre"].ToString();
                                                                                    childnodes7.ChildNodes.Add(childnodes8);

                                                                                    foreach (DataRow dr10 in dt.Rows)
                                                                                    {
                                                                                        if (dr10["IdPertenece"].ToString() == dr9["IdModulo"].ToString())
                                                                                        {
                                                                                            TreeNode childnodes9 = new TreeNode();
                                                                                            childnodes9.Text = dr10["Nombre"].ToString();
                                                                                            childnodes8.ChildNodes.Add(childnodes9);

                                                                                            foreach (DataRow dr11 in dt.Rows)
                                                                                            {
                                                                                                if (dr11["IdPertenece"].ToString() == dr10["IdModulo"].ToString())
                                                                                                {
                                                                                                    TreeNode childnodes10 = new TreeNode();
                                                                                                    childnodes10.Text = dr11["Nombre"].ToString();
                                                                                                    childnodes9.ChildNodes.Add(childnodes10);
                                                                                                }
                                                                                            }
                                                                                        }
                                                                                    }
                                                                                }
                                                                            }
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        public static void LlenarDropDownList(ref DropDownList dropdownlist, DataTable datatable, string nombre, string valor)
        {
            dropdownlist.DataSource = datatable;
            dropdownlist.DataTextField = nombre;
            dropdownlist.DataValueField = valor;
            dropdownlist.DataBind();
            dropdownlist.Items.Insert(0, new ListItem("SELECCIONAR", "0"));
        }

        public static void LlenarDropDownList(ref DropDownList dropdownlist, DataTable datatable, string nombre, string valor, string titulo)
        {
            dropdownlist.DataSource = datatable;
            dropdownlist.DataTextField = nombre;
            dropdownlist.DataValueField = valor;
            dropdownlist.DataBind();
            dropdownlist.Items.Insert(0, new ListItem(titulo, "0"));
        }
        public static void LlenarGridView(ref GridView gridview, DataTable datatable)
        {
            gridview.DataSource = datatable;
            gridview.EmptyDataText = "No hay datos que mostrar.";
            gridview.DataBind();
        }

        public static void LlenarRepeater(ref Repeater repeater, DataTable datatable)
        {
            repeater.DataSource = datatable;
            repeater.DataBind();
        }

    }

}
