using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace wfip.Mtto
{
    public partial class Default : System.Web.UI.Page
    {
        wfiplib.UsuariosMtto usuariosmtto = new wfiplib.UsuariosMtto();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

            }
        }

        protected void BtnAceptar_Click(object sender, EventArgs e)
        {
            if (usuariosmtto.Validar(txtClave.Text, txtContra.Text))
            {
                Session["validar"] = true;
                Response.Redirect("Tramite.aspx");
            }
            else
                lblMensajes.Text = "Clave o contraseña incorrectos.";
        }
    }
}