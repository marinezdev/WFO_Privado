using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace wfip.administracion
{
    public partial class usuariosV2_det : System.Web.UI.Page
    {
        wfiplib.Concentrado manejo_sesion = new wfiplib.Concentrado();

        protected void Page_Init(object sender, EventArgs e)
        {
            if (Session["credencial"] == null)
                Response.Redirect("~/Default.aspx");
            manejo_sesion = (wfiplib.Concentrado)Session["credencial"];
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Mensajes message = new Mensajes();
            int intAccion = -1;
            int intIdUsuario = -1;
            
            if (!IsPostBack)
            {
                intAccion = Convert.ToInt32(Request.QueryString["Action"]);
                intIdUsuario = Convert.ToInt32(Request.QueryString["Id"]);
                cargarRoles(ref cboRol);

                switch (intAccion)
                {
                    case 1:
                        lblTitulo.Text = "Usuario Nuevo.";
                        btnEditar.Visible = false;
                        btnCrear.Visible = true;
                        
                        break;

                    case 2:
                        lblTitulo.Text = "Modificar Usuario.";
                        btnEditar.Visible = true;
                        btnCrear.Visible = false;
                        
                        break;

                    default:
                        message.MensajeConfirmacion(this, "No se ha identificado la operación de realizar...", "usuariosV2_cat.aspx", "usuariosV2_cat.aspx");
                        break;
                }
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Mensajes message = new Mensajes();
            message.MensajeConfirmacion(this, "¿Seguro de que desea cancelar la acción?", "usuariosV2_cat.aspx", "");
        }

        protected void btnCrear_Click(object sender, EventArgs e)
        {
            Mensajes message = new Mensajes();
            string strMensaje = "";
            string strNombre = "";
            string strUsuario = "";
            int intIdRol = -1;

            try
            {
                strNombre = txNombre.Text.Trim();
                strUsuario = txUsuario.Text.Trim();
                intIdRol = Convert.ToInt32(cboRol.SelectedValue);
                strMensaje = "";

                if (strNombre.Length == 0)
                    strMensaje += "Debe indicar el nombre para el Usuario. \\n";

                if (strUsuario.Length < 8)
                    strMensaje += "Debe indicar un nombre de usuario para el acceso al sistema. \\n";

                if (intIdRol == -1)
                    strMensaje += "Debe indicar un Rol para el usuario. \\n";

                if (strMensaje.Length == 0)
                {
                    wfiplib.credencial credencial = RecuperaCaptura();
                    int Id = (new wfiplib.admCredencial()).NewUser(ref credencial);

                    try
                    {
                        wfiplib.Correo correo = new wfiplib.Correo();
                        correo.ProcesarCorreo(credencial.Correo,
                            "wfo@asae.com.mx",
                            "Creación de usuario",
                            "Se envía este correo con sus datos de acceso para que pueda entrar a la aplicación:<br /> Usuario: " + credencial.Usuario + "<br />Clave de acceso: " + credencial.Clave);
                    }
                    catch (Exception)
                    { }

                    Response.Redirect("usuariosV2_cat.aspx?Mensaje=1", true);
                }
                else
                    message.MostrarMensaje(this, strMensaje);
                
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }

        private void cargarRoles(ref DropDownList objDDL)
        {
            DataTable dtRoles = (new wfiplib.admSeguridad()).cargaRoles();
            objDDL.DataSource = dtRoles;
            objDDL.DataTextField = "NOMBRE";
            objDDL.DataValueField = "ID_ROL";
            objDDL.DataBind();
            objDDL.SelectedIndex = 0;
        }

        private wfiplib.credencial RecuperaCaptura()
        {
            return new wfiplib.credencial
            {
                Nombre = txNombre.Text.Trim().ToUpper(),
                Usuario = txUsuario.Text.Trim(),
                Correo = txtCorreo.Text,
                IdRol = int.Parse(cboRol.SelectedValue)
            };
        }
    }
}