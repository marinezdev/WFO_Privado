using System;
using System.Web.UI;

namespace wfip
{
    public partial class Site : System.Web.UI.MasterPage
    {
        wfiplib.Concentrado manejo_sesion = new wfiplib.Concentrado();

        protected void Page_Load(object sender, EventArgs e)
        {
            lblTituloSistema.Text = System.Web.Configuration.WebConfigurationManager.AppSettings["appName"].ToString();
            lblSubTituloSistema.Text = System.Web.Configuration.WebConfigurationManager.AppSettings["appSubName"];

            manejo_sesion = (wfiplib.Concentrado)Session["credencial"];
            lbMstNombreUsuario.Text = manejo_sesion.Credencial.Nombre;

            wfiplib.Menu menu = new wfiplib.Menu();
            lblMenu.Text = menu.CrearMenuHTML(manejo_sesion.Credencial.IdRol);
        }

        protected void BtnSalirSistema_Click(object sender, ImageClickEventArgs e)
        {
            if (Session["credencial"] != null)
            {
                (new wfiplib.admCredencial()).desconecta(manejo_sesion.Credencial.Id, Session.SessionID);
            }
            Session.Clear();
            System.Web.Security.FormsAuthentication.SignOut();
            Response.Redirect("/Default.aspx");
        }
    }
}