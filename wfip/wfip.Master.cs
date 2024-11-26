using System;
using System.Web.UI;

namespace wfip
{
    public partial class wfip : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (!IsPostBack)
            //{
            //    if (Session["credencial"] != null)
            //    {
            //        wfiplib.credencial oCredencial = (wfiplib.credencial)Session["credencial"];
            //        lbMstNombreUsuario.Text = oCredencial.Nombre;

            //        String txtArchivoMenu = Server.MapPath(".") + "\\menushtml\\" + oCredencial.Grupo.ToString() + ".html";
            //        System.IO.StreamReader objReader = System.IO.File.OpenText(txtArchivoMenu);
            //        lt_strMenu.Text = objReader.ReadToEnd();
            //        objReader.Close();
            //    }
            //}
        }

        protected void btnSalirSistema_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("salir.aspx");
        }
    }
}