using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace wfip.administracion
{
    public partial class usuariosV2_cat : System.Web.UI.Page
    {
        wfiplib.Concentrado manejo_sesion = new wfiplib.Concentrado();

        protected void Page_Init(object sender, EventArgs e)
        {
            if (Session["credencial"] == null)
                Response.Redirect("~/Default.aspx");
            manejo_sesion = (wfiplib.Concentrado)Session["credencial"];
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                PintaDatos();
        }

        private void PintaDatos()
        {
            Mensajes mensajes = new Mensajes();

            rptCatalogo.DataSource = (new wfiplib.admCredencial()).ListadoUsuario();
            rptCatalogo.DataBind();

            int intMensaje = Convert.ToInt16(Request.QueryString["Mensaje"]);
            if (intMensaje == 1)
                mensajes.MostrarMensaje(this, "El usuario se creo correctamente.");
        }

        protected void rptCatalogo_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                wfiplib.credencial registro = (wfiplib.credencial)e.Item.DataItem;
                if (registro.Estado == wfiplib.E_Estado.Inactivo)
                    ((ImageButton)e.Item.FindControl("imgBtnActivo")).ImageUrl = "~/img/inactivo.png";

                if (registro.Modulo == wfiplib.E_Modulo.Operacion || (new wfiplib.admSeguridad()).VerificaRol("OPERACIÓN MASTER", registro.IdRol))
                {
                    ((ImageButton)e.Item.FindControl("imgBtnOperacion")).Visible = true;
                }
                else
                {
                    ((ImageButton)e.Item.FindControl("imgBtnOperacion")).Visible = false;
                }
            }
        }

        protected void rptCatalogo_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            wfiplib.admCredencial admCred = new wfiplib.admCredencial();
            wfiplib.credencial credencial = admCred.carga(Convert.ToInt32(e.CommandArgument));

            switch (e.CommandName)
            {
                case "activo":
                    if (credencial.Estado == wfiplib.E_Estado.Inactivo)
                        admCred.activa(credencial.Id.ToString());
                    else
                        admCred.desactiva(credencial.Id.ToString());

                    PintaDatos();
                    break;

                case "modificar":
                    // wfiplib.credencial credencial = (new wfiplib.admCredencial()).carga(Convert.ToInt32(e.CommandArgument));
                    Response.Redirect("usuariosV2_det.aspx?Action=2&Id=" + e.CommandArgument, true);
                    break;

                case "operacion":
                    Response.Redirect("usuariosV2_WFO.aspx?id=" + e.CommandArgument, true);
                    break;
            }
        }

        protected void btnNewUser_Click(object sender, EventArgs e)
        {
            Response.Redirect("usuariosV2_det.aspx?Action=1&Id=-1", true);
        }
    }
}