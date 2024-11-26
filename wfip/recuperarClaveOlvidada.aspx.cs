using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using w = wfiplib;


namespace wfip
{
    public partial class recuperarClaveOlvidada : System.Web.UI.Page
    {
        w.admCredencial credencial = new w.admCredencial();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

            }
        }

        protected void txtUsuario_TextChanged(object sender, EventArgs e)
        {
            if (!ValidarUsuario(txtUsuario.Text))
                lblUsuarioVal.Text = "Error en el usuario.";
        }

        protected void txtCorreo_TextChanged(object sender, EventArgs e)
        {
            if (!ValidarCorreo(txtCorreo.Text))
                lblCorreoVal.Text = "Error en el correo.";
        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            //Asignar la nueva clave.
            string clavenueva = (new w.admCredencial()).CreaNuevaClave(txtUsuario.Text, txtCorreo.Text);
            lblMensajes.Text = "Tu nueva contraseña es: " + clavenueva + " (esta contraseña es sólo para accesar, debes cambiarla de nuevo cuando entres.";
        }
        
        
        
        #region Web Services***********************************************************************************************************************************

        [WebMethod()]
        public bool ValidarUsuario(string pUsuario)
        {
            return (new w.admCredencial()).ValidaUsuario(pUsuario);
        }

        [WebMethod()]
        public bool ValidarCorreo(string pCorreo)
        {
            return (new w.admCredencial()).ValidaCorreo(pCorreo);
        }


        #endregion
    }
}