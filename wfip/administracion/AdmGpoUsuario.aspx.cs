using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace wfip.administracion
{
    public partial class AdmGpoUsuario : System.Web.UI.Page
    {
        wfiplib.Concentrado manejo_sesion = new wfiplib.Concentrado();

        protected void Page_Init(object sender, EventArgs e)
        {
            if (Session["credencial"] == null)
                Response.Redirect("~/Default.aspx");
            manejo_sesion = (wfiplib.Concentrado)Session["credencial"];
        }

        private void EnviaMsgCliente(string pMensaje)
        {
            Lt_jsMsg.Text = "<script type='text/javascript'>$(document).ready(function () { alert('" + pMensaje + "'); });</script>";
        }

        protected void BtnCerrar_Click(object sender, EventArgs e)
        {
            Response.Redirect("admsysEspera.aspx");
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Lt_jsMsg.Text = "";
            if (!IsPostBack)
            {
                PintaDatos();
            }
        }

        protected void BtnCrear_Click(object sender, EventArgs e)
        {
            wfiplib.credencial credencial = RecuperaCaptura();
            int Id = (new wfiplib.admCredencial()).Nuevo(ref credencial);
            Limpia();
            PintaDatos();
        }

        private wfiplib.credencial RecuperaCaptura()
        {
            wfiplib.credencial credencial = new wfiplib.credencial
            {
                Nombre = txNombre.Text.Trim().ToUpper(),
                Usuario = txUsuario.Text.Trim(),
                Clave = txClave.Text.Trim(),
                Aplicacion = wfiplib.E_Aplicacion.PolizaGrupal,
                Modulo = wfiplib.E_Modulo.Ninguno,
                Grupo = (wfiplib.E_CredencialGrupo)Enum.Parse(typeof(wfiplib.E_CredencialGrupo), ddlGrupo.SelectedValue),
                IdFlujo = 0
            };
            return credencial;
        }

        private void Limpia()
        {
            txNombre.Text = "";
            txUsuario.Text = "";
            txClave.Text = "";
        }

        private void PintaDatos()
        {
            RptCatalogo.DataSource = (new wfiplib.admCredencial()).DaListaUsuarios(wfiplib.E_Aplicacion.PolizaGrupal);
            RptCatalogo.DataBind();
            ddlGrupo.DataSource = (new wfiplib.Utilerias()).DaListaDeRolesUsuarios;
            ddlGrupo.DataTextField = "texto";
            ddlGrupo.DataValueField = "valor";
            ddlGrupo.DataBind();
        }

        protected void BtnModifica_Click(object sender, EventArgs e)
        {
            wfiplib.credencial credencial = RecuperaCaptura();
            credencial.Id = Convert.ToInt32(Hf_Id.Value);
            (new wfiplib.admCredencial()).modifica(credencial);
            CancelaModifica();
            PintaDatos();
        }

        protected void BtnModificaCancela_Click(object sender, EventArgs e)
        {
            CancelaModifica();
        }

        private void CancelaModifica()
        {
            Limpia();
            BtnCrear.Visible = true;
            BtnModifica.Visible = false;
            BtnModificaCancela.Visible = false;
        }

        protected void RptCatalogo_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                wfiplib.credencial registro = (wfiplib.credencial)e.Item.DataItem;
                if (registro.Estado == wfiplib.E_Estado.Inactivo)
                {
                    ((ImageButton)e.Item.FindControl("ImgBtnActivo")).ImageUrl = "~/img/inactivo.png";
                }
            }
        }

        protected void RptCatalogo_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName.Equals("activo"))
            {
                wfiplib.admCredencial admCred = new wfiplib.admCredencial();
                wfiplib.credencial credencial = admCred.carga(Convert.ToInt32(e.CommandArgument));
                if (credencial.Estado == wfiplib.E_Estado.Inactivo)
                    admCred.activa(credencial.Id.ToString());
                else
                    admCred.desactiva(credencial.Id.ToString());
                PintaDatos();
            }
            if (e.CommandName.Equals("modificar"))
            {
                wfiplib.credencial credencial = (new wfiplib.admCredencial()).carga(Convert.ToInt32(e.CommandArgument));
                PintaModificar(credencial);
            }
        }

        private void PintaModificar(wfiplib.credencial pDatos)
        {
            try
            {
                Limpia();
                Hf_Id.Value = pDatos.Id.ToString();
                txNombre.Text = pDatos.Nombre;
                txUsuario.Text = pDatos.Usuario;
                //CHECK ¿Debería el administrador ver la clave del usuario?
                txClave.Text = wfiplib.Cifrado.Desencriptar(pDatos.Clave);
                ddlGrupo.SelectedValue = pDatos.Grupo.ToString("d");

                BtnCrear.Visible = false;
                BtnModifica.Visible = true;
                BtnModificaCancela.Visible = true;
            }
            catch (Exception ex)
            {
                EnviaMsgCliente(ex.Message);
            }
        }
    }
}