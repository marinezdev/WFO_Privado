using System;
using System.Web.UI;

namespace wfip.operacion
{
    public partial class operacion : System.Web.UI.MasterPage
    {
        wfiplib.Concentrado manejo_sesion = new wfiplib.Concentrado();
        wfiplib.Menu menu = new wfiplib.Menu();

        protected void Page_Load(object sender, EventArgs e)
        {
            lblTituloSistema.Text = System.Web.Configuration.WebConfigurationManager.AppSettings["appName"].ToString();
            lblSubTituloSistema.Text = System.Web.Configuration.WebConfigurationManager.AppSettings["appSubName"];

            if (!IsPostBack)
            {
                if (Session["credencial"] != null)
                {
                    manejo_sesion = (wfiplib.Concentrado)Session["credencial"];
                    lbMstNombreUsuario.Text = manejo_sesion.Credencial.Nombre;
                    if (System.Configuration.ConfigurationManager.AppSettings["MenuDinamico"].ToString() == "S")
                        lblMenu.Text = menu.CrearMenuHTML(manejo_sesion.Credencial.IdRol);
                    else
                    {
                        if (manejo_sesion.Credencial.IdRol > 0 && manejo_sesion.Credencial.Modulo > 0 && manejo_sesion.Credencial.Grupo > 0)
                        {
                            lblMenu.Text = menu.MenuConstruir(manejo_sesion.Credencial.Modulo, manejo_sesion.Credencial.Grupo);
                        }
                        else
                        {
                            lblMenu.Text = menu.MenuConstruir((wfiplib.E_AppRoles)Enum.Parse(typeof(wfiplib.E_AppRoles), manejo_sesion.Credencial.IdRol.ToString()));
                        }
                    }
                }
            }
        }

        /*
        protected void btnSalirSistema_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                Response.Redirect("../salir.aspx", true);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        */
    }
}