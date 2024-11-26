using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace wfip.promotoria
{
    public partial class Coasegurados : System.Web.UI.Page
    {
        wfiplib.Concentrado manejo_sesion = new wfiplib.Concentrado();
        wfiplib.admCoasegurados ca = new wfiplib.admCoasegurados();

        Mensajes mensajes = new Mensajes();

        public static string IdCoaseagurado = "";

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
            ca.Agregar(txtAPaterno.Text, txtAMaterno.Text, txtNombres.Text, txtFNacimiento.Text, txtCURP.Text, rblSexo.SelectedValue, txtFAfiliacion.Text, ddlTipoAsegurado.SelectedValue, txtFIColectividad.Text, Request["Id"], rblEstado.SelectedValue);
            mensajes.MostrarMensaje(this, "Se agregó el nuevo registro.");
            CargaInicial();
            LimpiarCampos();
        }

        protected void btnModifica_Click(object sender, EventArgs e)
        {
            //guardar los datos modificados
            ca.Actualizar(IdCoaseagurado, txtAPaterno.Text, txtAMaterno.Text, txtNombres.Text, txtFNacimiento.Text, txtCURP.Text, rblSexo.SelectedValue, txtFAfiliacion.Text, ddlTipoAsegurado.SelectedValue, txtFIColectividad.Text, rblEstado.SelectedValue);
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
                bool estado = (bool)DataBinder.Eval(e.Item.DataItem, "Estado");
                //wfiplib.credencial registro = (wfiplib.credencial)e.Item.DataItem;
                if (!estado)
                {
                    //((ImageButton)e.Item.FindControl("imgBtnActivo")).ImageUrl = "~/img/inactivo.png";
                    ((Image)e.Item.FindControl("ImgActivo")).ImageUrl = "~/img/inactivo.png";
                }
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
                wfiplib.admCredencial admCred = new wfiplib.admCredencial();
                wfiplib.credencial credencial = admCred.carga(Convert.ToInt32(e.CommandArgument));
                if (credencial.Estado == wfiplib.E_Estado.Inactivo) admCred.activa(credencial.Id.ToString());
                else admCred.desactiva(credencial.Id.ToString());
            }
            if (e.CommandName.Equals("modificar"))
            {
                //Editar el registro
                var detalle = ca.Seleccionar(id:Convert.ToInt32(e.CommandArgument).ToString());
                IdCoaseagurado = detalle[0].ToString();
                txtAPaterno.Text = detalle[1].ToString();
                txtAMaterno.Text = detalle[2].ToString();
                txtNombres.Text = detalle[3].ToString();
                txtFNacimiento.Text = detalle[4].ToString();
                txtCURP.Text = detalle[5].ToString();
                rblSexo.SelectedValue = detalle[6].ToString();
                txtFAfiliacion.Text = detalle[7].ToString();
                ddlTipoAsegurado.SelectedValue = detalle[8].ToString();
                txtFIColectividad.Text = detalle[9].ToString();
                rblEstado.SelectedValue = detalle[11].ToString() == "True" ? "1" : "0";
                btnCrear.Visible = false;
                btnModifica.Visible = true;
            }
        }

        protected void CargaInicial()
        {
            rptCatalogo.DataSource = ca.Seleccionar(Request["Id"]);
            rptCatalogo.DataBind();
        }

        protected void LimpiarCampos()
        {
            txtAPaterno.Text = "";
            txtAMaterno.Text = "";
            txtNombres.Text = "";
            txtFNacimiento.Text = "";
            txtCURP.Text = "";
            rblSexo.SelectedIndex = 0;
            txtFAfiliacion.Text = "";
            txtFIColectividad.Text = "";
            ddlTipoAsegurado.SelectedIndex = 0;
            rblEstado.SelectedIndex = 0;
        }


    }
}