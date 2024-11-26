using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace wfip.promotoria
{
    public partial class Polizas : System.Web.UI.Page
    {
        wfiplib.Concentrado manejo_sesion = new wfiplib.Concentrado();
        wfiplib.admDependencias dep = new wfiplib.admDependencias();

        Mensajes mensajes = new Mensajes();

        public static string IdDependencia = "";

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
                CargaInicial();
            }
        }

        protected void btnCrear_Click(object sender, EventArgs e)
        {
            //Guardar los datos nuevos
            dep.Agregar(txtNombre.Text, txtRetenedor.Text);
            mensajes.MostrarMensaje(this, "Se agregó el nuevo registro.");
            CargaInicial();
            LimpiarCampos();
        }

        protected void btnModifica_Click(object sender, EventArgs e)
        {
            //guardar los datos modificados
            dep.Actualizar(int.Parse(IdDependencia), txtNombre.Text, txtRetenedor.Text);
            mensajes.MostrarMensaje(this, "Se guardaron los cambios exitosamente.");
            btnModifica.Visible = false;
            btnCrear.Visible = true;
            CargaInicial();
            LimpiarCampos();
        }

        protected void btnModificaCancela_Click(object sender, EventArgs e)
        {

        }

        private void cancelaModifica()
        {
        }

        protected void rptCatalogo_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                //wfiplib.credencial registro = (wfiplib.credencial)e.Item.DataItem;
                //if (registro.Estado == wfiplib.E_Estado.Inactivo)
                //{
                //    ((ImageButton)e.Item.FindControl("imgBtnActivo")).ImageUrl = "~/img/inactivo.png";
                //}
                //if (registro.Modulo == wfiplib.E_Modulo.Operacion)
                //{
                //    ((ImageButton)e.Item.FindControl("imgBtnOperacion")).Visible = true;
                //}
                //else
                //{
                //    ((ImageButton)e.Item.FindControl("imgBtnOperacion")).Visible = false;
                //}
            }
        }

        protected void rptCatalogo_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName.Equals("activo"))
            {
            }
            if (e.CommandName.Equals("modificar"))
            {
                //Editar el registro
                var detalle = dep.Seleccionar(Convert.ToInt32(e.CommandArgument).ToString());
                IdDependencia = detalle[0].ToString();
                txtNombre.Text = detalle[1].ToString();
                txtRetenedor.Text = detalle[2].ToString();
                btnCrear.Visible = false;
                btnModifica.Visible = true;
            }
        }

        protected void CargaInicial()
        {
            rptCatalogo.DataSource = dep.Seleccionar();
            rptCatalogo.DataBind();
        }

        protected void LimpiarCampos()
        {
            txtNombre.Text = "";
            txtRetenedor.Text = "";
        }

    }
}