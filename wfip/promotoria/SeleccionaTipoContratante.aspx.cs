using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace wfip.promotoria
{
    public partial class SeleccionaTipoContratante : System.Web.UI.Page
    {
        wfiplib.Concentrado manejo_sesion = new wfiplib.Concentrado();

        protected void Page_Init(object sender, EventArgs e)
        {
            if (Session["credencial"] == null)
                Response.Redirect("~/Default.aspx");
            manejo_sesion = (wfiplib.Concentrado)Session["credencial"];
        }

        protected void Page_Load(object sender, EventArgs e){}

        protected void btnFisica_Click(object sender, EventArgs e)
        {
            string Tipo=Request .Params["tc"].ToString ();
            if (Tipo.Equals("vida")) { Response.Redirect("EmisionVida_Fisica.aspx"); }
            if (Tipo.Equals("gmm")) { Response.Redirect("EmisionGmm_Fisica.aspx"); }

        }

        protected void btnMoral_Click(object sender, EventArgs e)
        {
            string Tipo = Request.Params["tc"].ToString();
            if (Tipo.Equals("vida")) { Response.Redirect("EmisionVida_Moral.aspx"); }
            if (Tipo.Equals("gmm")) { Response.Redirect("EmisionGmm_Moral.aspx"); }
        }
                      
    }
}