using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace wfip.administracion
{
    public partial class pruCfgMesa : System.Web.UI.Page
    {
        wfiplib.Concentrado manejo_sesion = new wfiplib.Concentrado();

        protected void Page_Init(object sender, EventArgs e)
        {
            if (Session["credencial"] == null) Response.Redirect("~/Default.aspx");
            manejo_sesion = (wfiplib.Concentrado)Session["credencial"];
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        private void pintaMesas()
        {
            
        }

        

        protected void BtnSerializa_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(hfOrdenResultado.Value)) { ltResultadoTexto.Text = hfOrdenResultado.Value; }
            else ltResultadoTexto.Text = "NO HAY DATOS";
        }
    }

}