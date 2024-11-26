using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace wfip.administracion
{
    public partial class admUsuario : System.Web.UI.Page
    {
        wfiplib.Concentrado manejo_sesion = new wfiplib.Concentrado();
        wfiplib.Correo correo = new wfiplib.Correo();

        protected void Page_Init(object sender, EventArgs e)
        {
            if (Session["credencial"] == null)
                Response.Redirect("~/Default.aspx");
            manejo_sesion = (wfiplib.Concentrado)Session["credencial"];
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            lt_jsMsg.Text = "";
            if (!IsPostBack)
            {
                PintaDatos();
            }
        }

        private void enviaMsgCliente(string pMensaje)
        {
            lt_jsMsg.Text = "<script type='text/javascript'>$(document).ready(function () { alert('" + pMensaje + "'); });</script>";
        }

        protected void btnCerrar_Click(object sender, EventArgs e)
        {
            Response.Redirect("admsysEspera.aspx", true);
        }

        protected void btnCrear_Click(object sender, EventArgs e)
        {
            wfiplib.credencial credencial = RecuperaCaptura();
            int Id = (new wfiplib.admCredencial()).Nuevo(ref credencial);
            //enviar correo al susuario que se ha creado
            correo.ProcesarCorreo(credencial.Correo, 
                "wfo@asae.com.mx", 
                "Creación de usuario", 
                "Se envía este correo con sus datos de acceso para que pueda entrar a la aplicación:<br /> Usuario: " + credencial.Usuario + "<br />Clave de acceso: " + credencial.Clave);
            limpia();
            PintaDatos();
        }

        private wfiplib.credencial RecuperaCaptura()
        {
            wfiplib.credencial credencial = new wfiplib.credencial
            {
                Nombre = txNombre.Text.Trim().ToUpper(),
                Usuario = txUsuario.Text.Trim(),
                Clave = txClave.Text.Trim(),
                Aplicacion = wfiplib.E_Aplicacion.Fto,
                Modulo = (wfiplib.E_Modulo)Enum.Parse(typeof(wfiplib.E_Modulo), ddlModulo.Text),
                Grupo = (wfiplib.E_CredencialGrupo)Enum.Parse(typeof(wfiplib.E_CredencialGrupo), ddlGrupo.SelectedValue),
                IdFlujo = Convert.ToInt32(ddlFto.SelectedValue),
                Correo = txtCorreo.Text,
                IdRol = int.Parse(ddlRol.SelectedValue)
            };
            return credencial;
        }

        private void limpia()
        {
            txNombre.Text = "";
            txUsuario.Text = "";
            txClave.Text = "";
            ddlModulo.SelectedIndex = 0;
            ddlGrupo.SelectedIndex = 0;
            txtCorreo.Text = "";
            ddlRol.SelectedIndex = 0;
        }

        private void PintaDatos()
        {
            rptCatalogo.DataSource = (new wfiplib.admCredencial()).DaListaUsuarios(wfiplib.E_Aplicacion.Fto);
            rptCatalogo.DataBind();
            ddlModulo.DataSource = Enum.GetValues(typeof(wfiplib.E_Modulo));
            ddlModulo.DataBind();
            ddlGrupo.DataSource = (new wfiplib.Utilerias()).DaListaDeRolesUsuarios;
            ddlGrupo.DataTextField = "texto";
            ddlGrupo.DataValueField = "valor";
            ddlGrupo.DataBind();
            ddlFto.DataSource = (new wfiplib.admFlujo()).ListaCbo();
            ddlFto.DataValueField = "Id";
            ddlFto.DataTextField = "Nombre";
            ddlFto.DataBind();
            (new wfiplib.admRoles()).RolesObtener_DropDownList(ref ddlRol);
        }

        protected void btnModifica_Click(object sender, EventArgs e)
        {
            wfiplib.credencial credencial = RecuperaCaptura();
            credencial.Id = Convert.ToInt32(hf_Id.Value);
            (new wfiplib.admCredencial()).modifica(credencial);
            cancelaModifica();
            PintaDatos();
        }

        protected void btnModificaCancela_Click(object sender, EventArgs e)
        {
            cancelaModifica();
        }

        private void cancelaModifica()
        {
            limpia();
            btnCrear.Visible = true;
            btnModifica.Visible = false;
            btnModificaCancela.Visible = false;
        }

        protected void rptCatalogo_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                wfiplib.credencial registro = (wfiplib.credencial)e.Item.DataItem;
                if (registro.Estado == wfiplib.E_Estado.Inactivo)
                {
                    ((ImageButton)e.Item.FindControl("imgBtnActivo")).ImageUrl = "~/img/inactivo.png";
                }
                if (registro.Modulo == wfiplib.E_Modulo.Operacion)
                {
                    ((ImageButton)e.Item.FindControl("imgBtnOperacion")).Visible = true;
                }
                else
                {
                    ((ImageButton)e.Item.FindControl("imgBtnOperacion")).Visible = false;
                }
            }
        }

        protected void rptCatalogo_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName.Equals("activo"))
            {
                wfiplib.admCredencial admCred = new wfiplib.admCredencial();
                wfiplib.credencial credencial = admCred.carga(Convert.ToInt32(e.CommandArgument));
                if (credencial.Estado == wfiplib.E_Estado.Inactivo) admCred.activa(credencial.Id.ToString());
                else admCred.desactiva(credencial.Id.ToString());
                PintaDatos();
            }
            if (e.CommandName.Equals("modificar"))
            {
                wfiplib.credencial credencial = (new wfiplib.admCredencial()).carga(Convert.ToInt32(e.CommandArgument));
                pintaModificar(credencial);
            }
            if (e.CommandName.Equals("operacion"))
            {
                Response.Redirect("admUsuarioMesa.aspx?id=" + e.CommandArgument);
            }
        }

        private void pintaModificar(wfiplib.credencial pDatos)
        {
            try
            {
                limpia();
                hf_Id.Value = pDatos.Id.ToString();
                txNombre.Text = pDatos.Nombre;
                txUsuario.Text = pDatos.Usuario;
                //CHECK ¿Debería el administrador ver la clave del usuario?
                txClave.Text = wfiplib.Cifrado.Desencriptar(pDatos.Clave);
                ddlModulo.Text = pDatos.Modulo.ToString();
                ddlGrupo.SelectedItem.Text = pDatos.Grupo.ToString();
                txtCorreo.Text = pDatos.Correo;
                ddlRol.SelectedValue = pDatos.IdRol.ToString();

                btnCrear.Visible = false;
                btnModifica.Visible = true;
                btnModificaCancela.Visible = true;
            }
            catch (Exception ex)
            {
                enviaMsgCliente(ex.Message);
            }
        }
    }
}