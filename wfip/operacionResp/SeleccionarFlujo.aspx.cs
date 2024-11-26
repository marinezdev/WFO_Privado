using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace wfip.operacionResp
{
    public partial class SeleccionarFlujo : System.Web.UI.Page
    {
        wfiplib.Concentrado manejo_sesion = new wfiplib.Concentrado();
        Application.Operacion.Operacion operacion = new Application.Operacion.Operacion(); 

        protected void Page_Init(object sender, EventArgs e)
        {
            if (Session["credencial"] == null)
                Response.Redirect("../Default.aspx", true);
            manejo_sesion = (wfiplib.Concentrado)Session["credencial"];
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                List<Propiedades.Flujo> flujos = operacion.flujo.Flujo_Selecionar_IdUsuario(manejo_sesion.Credencial.Id);

                ListFlujos.DataSource = flujos;
                ListFlujos.DataTextField = "Nombre";
                ListFlujos.DataValueField = "Id";
                ListFlujos.DataBind();
                ListFlujos.SelectedIndex = 0;
                ListFlujos.Focus();
            }
        }

        protected void ListFlujos_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<Propiedades.Mesa> mesas = operacion.mesa.UsuariosMesa_Seleccionar_IdFlujo_IdUsuario(Convert.ToInt32(ListFlujos.SelectedValue), manejo_sesion.Credencial.Id);
            string dtMesas = "";

            foreach (var mesa in mesas)
            {
                dtMesas += "<div class='control-label col-md-2 col-sm-2 col-xs-6'>" +
                            "<div class='x_panel text-center'>" +
                                "<a href = 'TramiteProcesar.aspx?IdM=" + mesa.Id.ToString() + "&IdF=" + ListFlujos.SelectedValue + "' >" +
                                    "<img src='" + mesa.Imagen.ToString() + "' class='img-thumbnail'/>" +
                                    "<div class='form-group text-center'>" +
                                        "<h5><small style='color: #003657;'><strong>" + mesa.Nombre + "</strong></small></h5>" +
                                        //"<h5><small style='color: #003657;'>Tramites: <strong>" + mesa.Totales.ToString() + "</strong></small></h5>" +
                                        "<hr />" +
                                    "</div>" +
                                "</a>" +
                            "</div>" +
                        "</div>";
            }

            MesasLiteral.Text = dtMesas;
        }

    }
}