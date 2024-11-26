using System;
using System.Configuration;
using System.Web.UI.WebControls;

namespace wfip
{
    public partial class primerAccesoPromotoria : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.Params["id"] != null) pintaDatos(Convert.ToInt32(Request.Params["id"]));
                else Response.Redirect("Default.aspx");
            }
        }

        private void pintaDatos(int IdUsuario)
        {
            wfiplib.admUsuariosPromotoriaPrimierIngreso admUsr = new wfiplib.admUsuariosPromotoriaPrimierIngreso();
            ltUsuario.Text = admUsr.daNombreUsuario(IdUsuario);
        }

        protected void LoginButton_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txUsuario.Text) && !string.IsNullOrEmpty(txClave.Text))
            {
                wfiplib.credencial credencial = new wfiplib.credencial();
                credencial.Nombre = ltUsuario.Text.Trim().ToUpper();
                credencial.Usuario = ltUsuario.Text.Trim();
                credencial.Clave = txClave.Text.Trim();
                credencial.Modulo = wfiplib.E_Modulo.Promotoria;
                credencial.Grupo = wfiplib.E_CredencialGrupo.Operador;
                int Id = (new wfiplib.admCredencial()).Nuevo(ref credencial);
                pnlRespuestaCreacion.Visible = true;
                pnlCajaCaptura.Visible = false;
                if (Id > 0)
                {
                    wfiplib.admUsuariosPromotoriaPrimierIngreso admUsr = new wfiplib.admUsuariosPromotoriaPrimierIngreso();
                    int id_Usuario = Convert.ToInt32(Request.Params["id"]);
                    admUsr.registraPrimerIngreso(id_Usuario);
                    int clavePromotoria = admUsr.daClavePromotoria(id_Usuario);
                    int IdPromotoria = (new wfiplib.admCatPromotoria(ConfigurationManager.ConnectionStrings["conecta_bd"].ConnectionString)).daIdPromotoria(clavePromotoria);
                    (new wfiplib.admCredencial()).asignaPromotoria(IdPromotoria.ToString(), Id.ToString());
                    ltAceptaCreacion.Text = "Tu registro fue correcto, ahora ingresa utilizando tu usuario y contraseña";
                }
                else { ltAceptaCreacion.Text = "Hubo un error en tu registro, por favor intenta de nuevo"; }
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e) { Response.Redirect("Default.aspx"); }

        protected void cust_txUsuario_ServerValidate(object source, ServerValidateEventArgs args)
        {
            args.IsValid = (txUsuario.Text.Length > 7);
        }

        protected void btnAceptaCreacion_Click(object sender, EventArgs e) { Response.Redirect("Default.aspx"); }
    }
}