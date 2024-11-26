using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace wfip.administracion
{
    public partial class admPermisos : System.Web.UI.Page
    {
        wfiplib.Concentrado manejo_sesion = new wfiplib.Concentrado();
        wfiplib.admRoles roles = new wfiplib.admRoles();
        wfiplib.Menu menu = new wfiplib.Menu();
        wfiplib.admPermisos permisos = new wfiplib.admPermisos();
        Mensajes mensajes = new Mensajes();

        public DataTable dt;

        protected void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session["RegistrosTemporales"] = null;
                gvPermisos.DataSource = null;
                gvPermisos.DataBind();
            }
            manejo_sesion = (wfiplib.Concentrado)Session["credencial"];
            if (Session["credencial"] == null)
                Response.Redirect("~/Default.aspx");
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarRoles();
            }
            else
            {
                if (Session["RegistrosTemporales"] == null)
                {
                    //Nuevo
                    dt = new DataTable();
                    dt.Columns.Add("Activo");       //Estado del permiso
                    dt.Columns.Add("Descripcion");  //Descripción del permiso

                    CargarTabla(gvPermisos, dt);

                    Session["RegistrosTemporales"] = dt;
                }
            }
        }

        protected void btnCerrar_Click(object sender, EventArgs e)
        {
            Response.Redirect("admsysEspera.aspx");
        }

        protected void ddlRoles_SelectedIndexChanged(object sender, EventArgs e)
        {
            //llenar los permisos de acuerdo al rol seleccionado
            if (rblPermisos.SelectedValue == "1")
            {
                mostrar.Visible = true;
                agregar1.Visible = false;
                agregar2.Visible = false;
                //agregar3.Visible = false;
                CargarPermisos();
            }
            else if (rblPermisos.SelectedValue == "2")
            {
                mostrar.Visible = false;
                agregar1.Visible = true;
                agregar2.Visible = true;
                //agregar3.Visible = true;
            }
        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            permisos.PermisoAgregar(txtDescripcion.Text, int.Parse(ddlRoles.SelectedValue));
            rblPermisos.SelectedIndex = -1;
            txtDescripcion.Text = "";
            ddlRoles.SelectedIndex = 0;
            mensajes.MostrarMensaje(this, "Se guardó el permiso.");
        }

        protected void rblPermisos_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarRoles();
        }

        protected void gvPermisos_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvPermisos.EditIndex = -1;
            CargarTabla(gvPermisos, (DataTable)Session["RegistrosTemporales"]);
            ProcesarEncabezadoFijo();
        }

        protected void gvPermisos_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvPermisos.EditIndex = e.NewEditIndex;
            dt = (DataTable)Session["RegistrosTemporales"];
            CargarTabla(gvPermisos, dt);
        }

        protected void gvPermisos_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            DataTable dt = (DataTable)Session["RegistrosTemporales"];
            gvPermisos.EditIndex = e.RowIndex;
            int idpermiso = System.Int32.Parse(gvPermisos.DataKeys[e.RowIndex].Value.ToString());

            //Actualización del campo
            CheckBox resNo = (CheckBox)gvPermisos.Rows[e.RowIndex].FindControl("chkActivoE");
            permisos.PermisoModificar(resNo.Checked == true ? 1 : 0, int.Parse(ddlRoles.SelectedValue), idpermiso);

            mensajes.MostrarMensaje(this, "Se modificó el permiso para el Rol seleccionado");

            gvPermisos.EditIndex = -1;
            CargarPermisos();
        }

        protected void gvPermisos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string currentCommand = e.CommandName;
            if (currentCommand == "Update")
            {
                ProcesarEncabezadoFijo();
            }
        }



        protected void CargarRoles()
        {
            roles.RolesObtener_DropDownList(ref ddlRoles);
        }

        protected void CargarPermisos()
        {
            //Obtener los permisos del rol seleccionado
            gvPermisos.DataSource = Session["RegistrosTemporales"] = permisos.PermisosObtenerTablaPorRol(int.Parse(ddlRoles.SelectedValue));
            gvPermisos.EmptyDataText = "No hay permisos para el rol indicado.";
            gvPermisos.DataBind();
        }

        private void CargarTabla(GridView gridview, DataTable dt)
        {
            gridview.DataSource = dt;
            gridview.DataBind();
        }

        protected void ProcesarEncabezadoFijo()
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "Key", "<script>MakeStaticHeader('" + gvPermisos.ClientID + "', 300, 1170, 80, true); </script>", false);
        }




    }
}