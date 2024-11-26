using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace wfip.laboratorios
{
    public partial class laboratorios : System.Web.UI.MasterPage
    {
        wfiplib.ConcentradoLaboratorios manejo_sesion_labs = new wfiplib.ConcentradoLaboratorios();
        wfiplib.Menu menu = new wfiplib.Menu();

        protected void Page_Load(object sender, EventArgs e)
        {
            lblTituloSistema.Text = System.Web.Configuration.WebConfigurationManager.AppSettings["appName"].ToString();
            lblSubTituloSistema.Text = System.Web.Configuration.WebConfigurationManager.AppSettings["appSubName"];

            if (!IsPostBack)
            {
                if (Session["SesionLabs"] != null)
                {
                    manejo_sesion_labs = (wfiplib.ConcentradoLaboratorios)Session["SesionLabs"];
                    //lbMstNombreUsuario.Text = manejo_sesion_labs.CPAP.nombre;
                    //if (System.Configuration.ConfigurationManager.AppSettings["MenuDinamico"].ToString() == "S")
                    //    lblMenu.Text = menu.CrearMenuHTML(manejo_sesion_labs.Credencial.IdRol);
                    //else
                    //{
                    //    if (manejo_sesion_labs.Credencial.IdRol > 0 && manejo_sesion_labs.Credencial.Modulo > 0 && manejo_sesion.Credencial.Grupo > 0)
                    //    {
                    //        lblMenu.Text = menu.MenuConstruir(manejo_sesion_labs.Credencial.Modulo, manejo_sesion.Credencial.Grupo);
                    //    }
                    //    else
                    //    {
                    //        lblMenu.Text = menu.MenuConstruir((wfiplib.E_AppRoles)Enum.Parse(typeof(wfiplib.E_AppRoles), manejo_sesion_labs.Credencial.IdRol.ToString()));
                    //    }
                    //}
                }
            }
        }

        protected void btnSalirSistema_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("Default.aspx");
        }
    }
}