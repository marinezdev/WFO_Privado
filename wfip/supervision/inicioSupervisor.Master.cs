using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace wfip.supervision
{
    public partial class inicioSupervisorM : System.Web.UI.MasterPage
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
                            lblMenu.Text = menu.MenuConstruir(manejo_sesion.Credencial.Modulo, wfiplib.E_CredencialGrupo.SupervisorRes);
                        }
                        else
                        {
                            if (manejo_sesion.Credencial.IdRol == 3)
                            {
                                lblMenu.Text = menu.MenuConstruir((wfiplib.E_AppRoles)Enum.Parse(typeof(wfiplib.E_AppRoles), "99"));
                            }
                            else
                            {
                                lblMenu.Text = menu.MenuConstruir((wfiplib.E_AppRoles)Enum.Parse(typeof(wfiplib.E_AppRoles), manejo_sesion.Credencial.IdRol.ToString()));
                            }
                        }
                    }
                }
            }
        }
        /*
        private DataTable GetData()
        {
            int idModulo = 3;
            int grupo = 20;
            DataTable dt = new DataTable();
            dt = (new wfiplib.Utilerias()).Menu(idModulo,grupo);
            return dt;
        }
        private void PopulateMenu(DataTable dt)
        {
            string currentPage = Path.GetFileName(Request.Url.AbsolutePath);
            DataView view = new DataView(dt);
            view.RowFilter = "idMenuParent=0";
            foreach (DataRowView row in view)
            {
                MenuItem menuItem = new MenuItem
                {
                    Value = row["IdMenu"].ToString(),
                    Text = row["Nombre"].ToString(),
                    NavigateUrl = row["Url"].ToString(),
                    Selected = row["Url"].ToString().EndsWith(currentPage, StringComparison.CurrentCultureIgnoreCase)
                };
                Menu1.Items.Add(menuItem);
                AddChildItems(dt, menuItem);
            }
        }
        private void AddChildItems(DataTable table, MenuItem menuItem)
        {
            DataView viewItem = new DataView(table);
            viewItem.RowFilter = "idMenuParent=" + menuItem.Value;
            foreach (System.Data.DataRowView childView in viewItem)
            {
                MenuItem childmenuItem = new MenuItem
                {
                    Value = childView["IdMenu"].ToString(),
                    Text = childView["Nombre"].ToString(),
                    NavigateUrl = childView["Url"].ToString(),
                };
                menuItem.ChildItems.Add(childmenuItem);
                AddChildItems(table, childmenuItem);
            }
        }
        */
        /*
        protected void btnSalirSistema_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("../salir.aspx");
        }
        */
    }
}