using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace wfip.promotoria
{
    public partial class pmConsultaTramiteIns : System.Web.UI.Page
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
            {
                MostrarDatos(Convert.ToInt32(Request.Params["Id"].ToString()));
            }
        }

        protected void btnRegresar_Click(object sender, EventArgs e)
        {
            ClientScript.RegisterStartupScript(GetType(), "alertMessage", "<script>alert('Se volverá a la página MisTrámites'); window.location.href='listaMisTramites.aspx';</script>");
            //Response.Redirect("listaMisTramites.aspx", false);
        }

        private void MostrarDatos(int pId)
        {
            //Obtener los datos del registro
            wfiplib.Tablas.Institucional.Privado.InsServicios insservicios = new wfiplib.Tablas.Institucional.Privado.InsServicios();
            wfiplib.Tablas.Institucional.Privado.InsServicioDetalle insserviciodetalle = new wfiplib.Tablas.Institucional.Privado.InsServicioDetalle();

            var fila = insservicios.ObtenerServicio(pId);

            lblEncabezado.Text = "<b>Producto:</b> " + fila[1].ToString() + "<br />" +
                "<b>Ramo:</b> " + fila[2].ToString() + "<br />" +
                "<b>Clave Promotoría:</b> " + fila[3].ToString() + "<br />" +
                "<b>Región:</b> " + fila[4].ToString() + "<br />" +
                "<b>Agente:</b> " + fila[5].ToString() + "<br />" +
                "<b>Subdirección:</b> " + fila[6].ToString() + "<br />" +
                "<b>No. Poliza:</b> " + fila[7].ToString() + "<br />" +
                "<b>Gerente Comercial:</b> " + fila[8].ToString() + "<br />" +
                "<b>No. Orden:</b> " + fila[9].ToString() + "<br />" +
                "<b>Ejecutivo Comercial:</b> " + fila[10].ToString() + "<br />" +
                "<b>Contratante:</b> " + fila[11].ToString() + "<br />" +
                "<b>Fecha de Solicitud:</b> " + fila[12].ToString() + "<br />" +
                "<b>Observaciones:</b> " + fila[13].ToString();

            gvArchivos.DataSource = insserviciodetalle.ObtenerServiciosDetalle(int.Parse(fila[1].ToString()));
            gvArchivos.DataBind();
        }



    }
}