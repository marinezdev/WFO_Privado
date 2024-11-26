using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace wfip.operacion
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        wfiplib.Concentrado manejo_sesion = new wfiplib.Concentrado();

        protected void Page_Init(object sender, EventArgs e)
        {
            if (Session["credencial"] == null)
                Response.Redirect("~/Default.aspx");
            manejo_sesion = (wfiplib.Concentrado)Session["credencial"];
        }
        private void enviaMsgCliente(string pMensaje)
        {
            //lt_jsMsg.Text = "<script type='text/javascript'>$(document).ready(function () { alert('" + pMensaje + "'); });</script>";
            System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "AlertBox", "alert('"+ pMensaje +"');", true);

        }
        protected void Page_Load(object sender, EventArgs e)
        {
            lt_jsMsg.Text = "";
            if (!IsPostBack)
            {
                if (manejo_sesion.Credencial.usuarioMesa.Count == 0)
                {
                    enviaMsgCliente("El usuario NO tiene configurada una mesa, favor de validar con el Administrador.");
                }
                else
                {
                    //int modo = (new wfiplib.admFlujo()).daModo(oCredencial.IdFlujo);
                    int modo = (new wfiplib.admFlujo()).daModo(manejo_sesion.Credencial.IdFlujo);
                    if (modo != 1)
                    {
                        int res = Convert.ToInt32(Request.QueryString["res"]);
                        if (res == 1)
                        {
                            enviaMsgCliente("Por el momento NO hay trámites para la mesa.");
                        }
                    }
                    else
                    {
                        Response.Redirect("consultaTramite2.aspx?tp=2");
                    }
                }
                Session.Contents.Remove("nota");
            }
        }
        protected void admision_Command(object sender, CommandEventArgs e)
        {
            if (e.CommandName.Equals("abreMesa"))
            {
                Response.Redirect("consultaTramite2.aspx?tp=" + e.CommandArgument);
            }
        }

        protected void revisionDocumental_Command(object sender, CommandEventArgs e)
        {
            if (e.CommandName.Equals("abreMesa"))
            {
                Response.Redirect("consultaTramite2.aspx?tp=" + e.CommandArgument);
            }
        }

        protected void revisionPlad_Command(object sender, CommandEventArgs e)
        {
            if (e.CommandName.Equals("abreMesa"))
            {
                Response.Redirect("consultaTramite2.aspx?tp=" + e.CommandArgument);
            }
        }

        protected void Seleccion_Command(object sender, CommandEventArgs e)
        {
            if (e.CommandName.Equals("abreMesa"))
            {
                Response.Redirect("consultaTramite2.aspx?tp=" + e.CommandArgument);
            }

        }

        protected void revisionTecnica_Command(object sender, CommandEventArgs e)
        {
            if (e.CommandName.Equals("abreMesa"))
            {
                Response.Redirect("consultaTramite2.aspx?tp=" + e.CommandArgument);
            }
        }

        protected void Latam_Command(object sender, CommandEventArgs e)
        {
            if (e.CommandName.Equals("abreMesa"))
            {
                Response.Redirect("consultaTramite2.aspx?tp=" + e.CommandArgument);
            }
        }

        protected void citasMedicas_Command(object sender, CommandEventArgs e)
        {
            if (e.CommandName.Equals("abreMesa"))
            {
                Response.Redirect("consultaTramite2.aspx?tp=" + e.CommandArgument);
            }
        }

        protected void Captura_Command(object sender, CommandEventArgs e)
        {
            if (e.CommandName.Equals("abreMesa"))
            {
                Response.Redirect("consultaTramite2.aspx?tp=" + e.CommandArgument);
            }
        }

        protected void Control_Command(object sender, CommandEventArgs e)
        {
            if (e.CommandName.Equals("abreMesa"))
            {
                Response.Redirect("consultaTramite2.aspx?tp=" + e.CommandArgument);
            }
        }

        protected void Ejecucion_Command(object sender, CommandEventArgs e)
        {
            if (e.CommandName.Equals("abreMesa"))
            {
                Response.Redirect("consultaTramite2.aspx?tp=" + e.CommandArgument);
            }
        }

        protected void Cobranza_Command(object sender, CommandEventArgs e)
        {
            if (e.CommandName.Equals("abreMesa"))
            {
                Response.Redirect("consultaTramite2.aspx?tp=" + e.CommandArgument);
            }
        }
    }
}