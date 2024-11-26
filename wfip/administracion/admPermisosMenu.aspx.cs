using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace wfip.administracion
{
    public partial class admPermisosMenu : System.Web.UI.Page
    {
        wfiplib.Concentrado manejo_sesion = new wfiplib.Concentrado();
        wfiplib.admRoles roles = new wfiplib.admRoles();
        wfiplib.Menu menu = new wfiplib.Menu();
        Mensajes mensajes = new Mensajes();

        protected void Page_Init(object sender, EventArgs e)
        {
            if (Session["credencial"] == null)
                Response.Redirect("~/Default.aspx");
            manejo_sesion = (wfiplib.Concentrado)Session["credencial"];
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarRoles();
            }
        }

        protected void btnCerrar_Click(object sender, EventArgs e)
        {
            Response.Redirect("admsysEspera.aspx");
        }

        protected void ddlRoles_SelectedIndexChanged(object sender, EventArgs e)
        {
            //llenar los permisos de acuerdo al rol seleccionado
            tablaPermisosMenu.Visible = true;
            tvwMenu.Nodes.Clear();
            LlenarTreeViewMenu(int.Parse(ddlRoles.SelectedValue));

            if (tvwMenu.Nodes.Count == 0)
                tvwMenu.Nodes.Clear();

            //roles.AsociarMenuARol(int.Parse(ddlRoles.SelectedValue), int.Parse(ddlMenus.SelectedValue), 0);   
           lblVerMenu.Text = menu.CrearMenuHTML(int.Parse(ddlRoles.SelectedValue));

        }

        protected void CargarRoles()
        {
            roles.RolesObtener_DropDownList(ref ddlRoles);
        }

        protected void LlenarTreeViewMenu(int idrol)
        {
            menu.MenuLlenarPorRol_TreeView(ref tvwMenu, idrol);
        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            //guarda los cambios en los permisos del menu
            foreach (TreeNode nodo in tvwMenu.Nodes) //Padre
            {
                if (nodo.Checked == false)
                    menu.MenuActualizarPermisos(0, int.Parse(ddlRoles.SelectedValue), int.Parse(nodo.Value));
                else if (nodo.Checked == true)
                    menu.MenuActualizarPermisos(1, int.Parse(ddlRoles.SelectedValue), int.Parse(nodo.Value));

                if (nodo.ChildNodes.Count > 0)
                {
                    foreach (TreeNode nodoChild1 in nodo.ChildNodes) //Hijo 1
                    {
                        if (nodoChild1.Checked == false)
                            menu.MenuActualizarPermisos(0, int.Parse(ddlRoles.SelectedValue), int.Parse(nodoChild1.Value));
                        else if (nodoChild1.Checked == true)
                            menu.MenuActualizarPermisos(1, int.Parse(ddlRoles.SelectedValue), int.Parse(nodoChild1.Value));

                        if (nodoChild1.ChildNodes.Count > 0)
                        {
                            foreach (TreeNode nodoChild2 in nodoChild1.ChildNodes) //Hijo 2
                            {
                                if (nodoChild2.Checked == false)
                                    menu.MenuActualizarPermisos(0, int.Parse(ddlRoles.SelectedValue), int.Parse(nodoChild2.Value));
                                else if (nodoChild2.Checked == true)
                                    menu.MenuActualizarPermisos(1, int.Parse(ddlRoles.SelectedValue), int.Parse(nodoChild2.Value));

                                if (nodoChild2.ChildNodes.Count > 0)
                                {
                                    foreach (TreeNode nodoChild3 in nodoChild2.ChildNodes) //Hijo 3
                                    {
                                        if (nodoChild3.Checked == false)
                                            menu.MenuActualizarPermisos(0, int.Parse(ddlRoles.SelectedValue), int.Parse(nodoChild3.Value));
                                        else if (nodoChild3.Checked == true)
                                            menu.MenuActualizarPermisos(1, int.Parse(ddlRoles.SelectedValue), int.Parse(nodoChild3.Value));
                                    }
                                }
                            }
                        }
                    }
                }
            }
            tvwMenu.Nodes.Clear();
            LlenarTreeViewMenu(int.Parse(ddlRoles.SelectedValue));
            mensajes.MostrarMensaje(this, "Se guardaron los cambios a los permisos del menú.");
            mensajes.MostrarMensajeSM(this, "Se guardaron los cambios a los permisos del menú para el Rol seleccionado.");
        }

        protected void tvwMenu_TreeNodePopulate(object sender, TreeNodeEventArgs e)
        {
            
        }

        protected void tvwMenu_TreeNodeDataBound(object sender, TreeNodeEventArgs e)
        {

        }
    }
}