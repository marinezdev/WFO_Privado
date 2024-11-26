using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace wfip.promotoria
{
    public partial class Asegurados : System.Web.UI.Page
    {
        wfiplib.Concentrado manejo_sesion = new wfiplib.Concentrado();
        wfiplib.admDependencias dep = new wfiplib.admDependencias();
        wfiplib.admAseguradosTitulares at = new wfiplib.admAseguradosTitulares();
        wfiplib.admMunicipios mun = new wfiplib.admMunicipios();
        wfiplib.admEntidadFederativa ef = new wfiplib.admEntidadFederativa();

        Mensajes mensajes = new Mensajes();

        public static string IdAseguradoTitular = "";

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
            //Crea uno nuevo
            int obtenido = at.Agregar(txtPoliza.Text, ddlDependencias.SelectedValue, txtAPaterno.Text, txtAMaterno.Text, txtNombres.Text, txtFNacimiento.Text,
                txtRFC.Text, txtCURP.Text, rblSexo.SelectedValue, ddlEntidadFederativa.SelectedValue, ddlMunicipio.SelectedValue, ddlNivelTabular.SelectedValue, txtPercepcion.Text, rblEventual.SelectedValue, rblEstado.SelectedValue);
            if (obtenido == 1)
                mensajes.MostrarMensaje(this, "Se guardó el nuevo registro.");
            else
                mensajes.MostrarMensaje(this, "el registro que intentó guardar ya existe o hubo un error, revise los datos a capturar.");
            LimpiarCampos();
            CargaInicial();
        }

        protected void btnModifica_Click(object sender, EventArgs e)
        {
            //guarda las modificaciones al registro
            at.Actualizar(IdAseguradoTitular, txtPoliza.Text, ddlDependencias.SelectedValue, txtAPaterno.Text, txtAMaterno.Text, txtNombres.Text, txtFNacimiento.Text,
                txtRFC.Text, txtCURP.Text, rblSexo.SelectedValue, ddlEntidadFederativa.SelectedValue, ddlMunicipio.SelectedValue, ddlNivelTabular.SelectedValue, txtPercepcion.Text, rblEventual.SelectedValue, rblEstado.SelectedValue);
            mensajes.MostrarMensaje(this, "Se guardaron los cambios al registro.");
            LimpiarCampos();
            CargaInicial();
            btnModifica.Visible = false;
            btnCrear.Visible = true;
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
                    ((ImageButton)e.Item.FindControl("imgBtnActivo")).ImageUrl = "~/img/inactivo.png";
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
                var detalle = at.Seleccionar(Convert.ToInt32(e.CommandArgument).ToString());
                IdAseguradoTitular = detalle[0].ToString();
                ddlDependencias.SelectedValue = detalle[2].ToString();
                txtPoliza.Text = detalle[1].ToString();
                txtAPaterno.Text = detalle[3].ToString();
                txtAMaterno.Text = detalle[4].ToString();
                txtNombres.Text = detalle[5].ToString();
                txtFNacimiento.Text = detalle[6].ToString();
                txtRFC.Text = detalle[7].ToString();
                txtCURP.Text = detalle[8].ToString();
                rblSexo.SelectedValue = detalle[9].ToString();                                
                ddlEntidadFederativa.SelectedValue = detalle[10].ToString().Trim();
                //Precargar los municipios
                mun.SeleccionarPorEstado_DropDownList(ref ddlMunicipio, ddlEntidadFederativa.SelectedValue);
                ddlMunicipio.Visible = true;
                ddlMunicipio.SelectedValue = detalle[11].ToString().Trim();
                ddlNivelTabular.SelectedValue = detalle[12].ToString();
                txtPercepcion.Text = detalle[13].ToString();
                rblEventual.SelectedValue = detalle[14].ToString();
                rblEstado.SelectedValue = detalle[15].ToString() == "True" ? "1" : "0";
                btnCrear.Visible = false;
                btnModifica.Visible = true;
            }
            if (e.CommandName.Equals("operacion"))
            {
                Response.Redirect("InsCoasegurados.aspx?id=" + e.CommandArgument);
            }
        }

        protected void CargaInicial()
        {
            dep.SeleccionarDependencias_DropDrownList(ref ddlDependencias);
            rptCatalogo.DataSource = at.Seleccionar();
            rptCatalogo.DataBind();
            mun.SeleccionarPorEstado_DropDownList(ref ddlMunicipio, ddlEntidadFederativa.SelectedValue);
            ef.SeleccionarDependencias_DropDrownList(ref ddlEntidadFederativa);
        }

        protected void ddlEntidadFederativa_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlMunicipio.Visible = true;
            mun.SeleccionarPorEstado_DropDownList(ref ddlMunicipio, ddlEntidadFederativa.SelectedValue);
        }

        protected void LimpiarCampos()
        {
            txtPoliza.Text = "";
            ddlDependencias.SelectedIndex = -1;
            txtAPaterno.Text = "";
            txtAMaterno.Text = "";
            txtNombres.Text = "";
            txtFNacimiento.Text = "";
            txtRFC.Text = "";
            txtCURP.Text = ""; 
            rblSexo.SelectedIndex = 0;
            ddlEntidadFederativa.SelectedIndex = 0;
            //ddlMunicipio.SelectedIndex = 0;
            ddlNivelTabular.SelectedIndex = 0;
            txtPercepcion.Text = "";
            rblEventual.SelectedIndex = 0;
        }



    }
}