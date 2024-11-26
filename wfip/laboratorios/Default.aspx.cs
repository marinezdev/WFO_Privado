using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace wfip.laboratorios
{
    public partial class Default : System.Web.UI.Page
    {
        wfiplib.ConcentradoLaboratorios manejo_sesion_labs = new wfiplib.ConcentradoLaboratorios();
        wfiplib.admCatProveedor cp = new wfiplib.admCatProveedor();

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void LoginButton_Click(object sender, EventArgs e)
        {
            string correo = txUsuario.Text.Trim();

            if (!String.IsNullOrEmpty(correo))
            {
                manejo_sesion_labs.Inicializar();
                if (cp.SeleccionarValidar(correo).Rows.Count > 0)
                {
                    ddlProveedor.Visible = true;
                    cp.Proveedor_DropDownList(ref ddlProveedor, correo);
                    
                    //manejo_sesion_labs.CPAP = cpa.SeleccionarPorCorreo(usuario);
                    //wfiplib.cat_proveedor_accesop m; 
                    //m = cpa.SeleccionarPorId("1");
                    Session["SesionLabs"] = manejo_sesion_labs;
                    //Response.Redirect("esperaLaboratorio.aspx");
                }
                else
                {
                    ltMsg.Text = "Credenciales de acceso No válidas.";
                }
            }

        }

        protected void ddlProveedor_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlSucursal.Visible = true;
            cp.Sucursales_DropDownList(ref ddlSucursal, ddlProveedor.SelectedValue);
        }

        protected void ddlSucursal_SelectedIndexChanged(object sender, EventArgs e)
        {
            manejo_sesion_labs.CPP = cp.SeleccionarDetallePorId(ddlSucursal.SelectedValue);
            Session["SesionLabs"] = manejo_sesion_labs;
            Response.Redirect("MisTramites.aspx");
        }
    }
}