using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace wfip.Mtto
{
    public partial class Reseteo : System.Web.UI.Page
    {
        wfiplib.admCredencial admcredencial = new wfiplib.admCredencial();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Session["validar"].ToString()))
                Response.Redirect(".");
            if (!IsPostBack)
            {

            }
        }

        protected void Btnbuscar_Click(object sender, EventArgs e)
        {
            if (admcredencial.ReseteoContrasena(txtClave.Text).Tables[0].Rows.Count > 0)
            {
                GVUsuarios.DataSource = admcredencial.ReseteoContrasena(txtClave.Text).Tables[0];
                GVUsuarios.EmptyDataText = "No hay usuarios con ese dato.";
                GVUsuarios.DataBind();

                GVResultado.DataSource = admcredencial.ReseteoContrasena(txtClave.Text).Tables[1];
                GVResultado.DataBind();
            }
            else
            {
                GVUsuarios.DataSource = admcredencial.ReseteoContrasena(txtClave.Text).Tables[0];
                GVUsuarios.EmptyDataText = "No hay usuarios con ese dato.";
                GVUsuarios.DataBind();

                GVResultado.DataSource = null;
                GVResultado.DataBind();
            }
        }

        protected void GVUsuarios_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Accion")
            {
                string usuario = e.CommandArgument.ToString();

                GVUsuarios.DataSource = admcredencial.ReseteoContrasena(usuario).Tables[0];
                GVUsuarios.DataBind();

                GVResultado.DataSource = admcredencial.ReseteoContrasena(usuario).Tables[1];
                GVResultado.DataBind();
            }
        }
    }
}