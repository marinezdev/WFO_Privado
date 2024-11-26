using System;
using System.Web.UI.WebControls;

namespace wfip
{
    public partial class enconstruccion : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Form.FindControl("BtnSalirSistema").Visible = false;
        }
    }
}