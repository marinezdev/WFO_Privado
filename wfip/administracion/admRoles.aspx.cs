using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace wfip.administracion
{
    public partial class admRoles : System.Web.UI.Page
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
            { }
        }

        protected void btnCerrar_Click(object sender, EventArgs e)
        {
            Response.Redirect("admsysEspera.aspx");
        }

        protected void rblOpciones_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rblOpciones.SelectedValue == "1")
            {
                nuevo.Visible = true;
                rol.Visible = false;
                nombreEditado.Visible = false;
                menuAsociado.Visible = false;
            }
            else if (rblOpciones.SelectedValue == "2")
            {
                nuevo.Visible = false;
                rol.Visible = true;
                nombreEditado.Visible = true;
                menuAsociado.Visible = false;
                CargarRoles();
            }
            else if (rblOpciones.SelectedValue == "3")
            {
                nuevo.Visible = false;
                rol.Visible = true;
                nombreEditado.Visible = false;
                menuAsociado.Visible = true;
                CargarRoles();
                CargarMenu();
            }
            btnAceptar.Visible = true;
        }

        protected void CargarRoles()
        {
            roles.RolesObtener_DropDownList(ref ddlRoles);
        }

        protected void CargarMenu()
        {

        }

        protected void ddlRoles_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rblOpciones.SelectedValue == "2")
            {
                nombreEditado.Visible = true;
                txtNombreEditado.Visible = true;
                txtNombreEditado.Text = ddlRoles.SelectedItem.Text;
            }
            else if (rblOpciones.SelectedValue == "2")
            {
                nombreEditado.Visible = false;
                txtNombreEditado.Text = "";
            }
        }

        protected void ddlMenu_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlMenu.SelectedValue == "3")
                ddlSub.Visible = true;
            else
                ddlSub.Visible = false;
        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            //Guardar la info
            if (rblOpciones.SelectedValue == "1")
            {
                int RolObtenido = roles.RolAgregar(txtNombre.Text);
                int opcionesDemenu = menu.ContarOpciones() + 1;
                //Agregado el nuevo rol, agregar sus permisos de menu para modificación posterior
                for (int i = 1; i < opcionesDemenu; i++)
                {
                    menu.MenuPermisosGuardar(i, RolObtenido);
                }
                mensajes.MostrarMensaje(this, "Se agregó el nuevo Rol");
            }
            else if (rblOpciones.SelectedValue == "2")
            {
                roles.EditarRol(int.Parse(ddlRoles.SelectedValue), txtNombreEditado.Text);
                mensajes.MostrarMensaje(this, "Se guardaron los cambios al Rol");
            }
            else if (rblOpciones.SelectedValue == "3")
            {
                roles.AsociarMenuARol(int.Parse(ddlRoles.SelectedValue), int.Parse(ddlMenu.SelectedValue), int.Parse(ddlSub.SelectedValue));
                mensajes.MostrarMensaje(this, "Se asoció el menú indicado al Rol seleccionado.");
            }
            nuevo.Visible = false;
            rol.Visible = false;
            nombreEditado.Visible = false;
            menuAsociado.Visible = false;
            btnAceptar.Visible = false;
        }
    }
}