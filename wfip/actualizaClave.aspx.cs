using System;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;

namespace wfip
{
    public partial class actualizaClave : System.Web.UI.Page
    {
        public static List<wfiplib.ClavesUsuario> clavesusuario;
        wfiplib.Concentrado manejo_sesion = new wfiplib.Concentrado();

        protected void Page_Init(object sender, EventArgs e)
        {
            if (Session["credencial"] == null)
                Response.Redirect("Default.aspx");
            manejo_sesion = (wfiplib.Concentrado)Session["credencial"];
            clavesusuario = new List<wfiplib.ClavesUsuario>();
            clavesusuario = manejo_sesion.Credencial.ClavesDeUsuario;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                Session.Contents.Remove("nota");
            pintaDatos();
            
        }
        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            string regreso = "espera.aspx";
            if (!string.IsNullOrEmpty(Request.Params["bk"]))
            {
                regreso = Request.Params["bk"];
            }
            Response.Redirect(regreso);
        }

        private void pintaDatos()
        {
            //wfiplib.credencial oCredencial = (wfiplib.credencial)Session["credencial"];
            lbUsrActual.Text = manejo_sesion.Credencial.Usuario;
            //bool checar = VerificarClave(oCredencial.Usuario);
        }

        protected void btnContinuar_Click(object sender, EventArgs e)
        {
            //wfiplib.credencial oCred = (wfiplib.credencial)Session["credencial"];
            wfiplib.admCredencial oAdmCred = new wfiplib.admCredencial();
            if (oAdmCred.usuarioClaveCorrecto(manejo_sesion.Credencial.Usuario, txClaveActual.Text.Trim()))
            {
                manejo_sesion.Credencial.Clave = txNuevaClave.Text.Trim();
                oAdmCred.modificaClave(manejo_sesion.Credencial);
                oAdmCred.desconecta(manejo_sesion.Credencial.Id, Session.SessionID);
                oAdmCred.GuardarClave(manejo_sesion.Credencial, txNuevaClave.Text); //Agrega la nueva clave para el usuario para seguimiento.
                Session.Clear();
                Response.Redirect("actualizaClaveConfirma.aspx?id=" + manejo_sesion.Credencial.Id.ToString());
            }
            else
                ltMsg.Text = "LA CLAVE ACTUAL NO ES CORRECTA";
        }

        protected void txNuevaClave_TextChanged(object sender, EventArgs e)
        {
            if (VerificarClave(txNuevaClave.Text))
                cv_txNuevaClave.Text = "Pruebe una clave diferente, ya existe una clave semejante";
            else
                txConfimaClave.Focus();
        }

        [WebMethod()]
        public static bool VerificarClave(string pClave)
        {
            bool volver = false;
            for (int i = 0; i < clavesusuario.Count; i++)
            {
                if (clavesusuario[i].Clave.Contains(pClave))
                {
                    volver = true;
                    break;
                }
            }

            return volver;
        }


    }
}