using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace wfip.administracion
{
    public partial class admUsuarioMesa : System.Web.UI.Page
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
            lt_jsMsg.Text = "";
            if (!IsPostBack)
            {
                pintaDatos();
            }
        }

        private void enviaMsgCliente(string pMensaje)
        {
            lt_jsMsg.Text = "<script type='text/javascript'>$(document).ready(function () { alert('" + pMensaje + "'); });</script>";
        }

        protected void btnCerrar_Click(object sender, EventArgs e)
        {
            Response.Redirect("admUsuario.aspx");
        }

        private void pintaDatos()
        {
            try
            {
                ddlFlujo.DataSource = (new wfiplib.admFlujo()).ListaCbo();
                ddlFlujo.DataValueField = "Id";
                ddlFlujo.DataTextField = "Nombre";
                ddlFlujo.DataBind();

                hf_Id.Value = Request.QueryString["id"].ToString();
                int idUsr = Convert.ToInt32(hf_Id.Value);
                wfiplib.credencial oCredencial = (new wfiplib.admCredencial()).carga(idUsr);
                lbNombre.Text = oCredencial.Nombre;
                lbUsuario.Text = oCredencial.Usuario;
                lbModulo.Text = oCredencial.Modulo.ToString();
                lbGrupo.Text = oCredencial.Grupo.ToString();

                Session["usrMesas"] = oCredencial.usuarioMesa;
                rptMesas.DataSource = oCredencial.usuarioMesa;
                rptMesas.DataBind();
            }
            catch (Exception ex) { enviaMsgCliente(ex.Message); }
        }

        protected void ddlFlujo_SelectedIndexChanged(object sender, EventArgs e)
        {
            int idFlujo = Convert.ToInt32(ddlFlujo.SelectedValue);
            llenaFlujo(idFlujo);
        }

        private void llenaFlujo(int pIdFlujo)
        {
            ddlMesa.DataSource = (new wfiplib.admMesa()).ListaCbo(pIdFlujo);
            ddlMesa.DataValueField = "Id";
            ddlMesa.DataTextField = "Nombre";
            ddlMesa.DataBind();

            ddlTipoTrámite.DataSource = (new wfiplib.admTipoTramite()).ListaCbo(pIdFlujo);
            ddlTipoTrámite.DataValueField = "Id";
            ddlTipoTrámite.DataTextField = "Nombre";
            ddlTipoTrámite.DataBind();
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            int credencialId = Convert.ToInt32(hf_Id.Value);
            List<wfiplib.usuarioMesa> LstUsrMesas = new List<wfiplib.usuarioMesa>();
            if (Session["usrMesas"] != null)
            {
                LstUsrMesas = (List<wfiplib.usuarioMesa>)Session["usrMesas"];
            }
            (new wfiplib.admUsuarioMesa()).Guardar(credencialId, LstUsrMesas);
            Response.Redirect("admUsuario.aspx");
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            if (ddlFlujo.SelectedIndex > 0 && ddlMesa.SelectedIndex > 0)
            {
                List<wfiplib.usuarioMesa> LstUsrMesas = new List<wfiplib.usuarioMesa>();
                if (Session["usrMesas"] != null) { LstUsrMesas = (List<wfiplib.usuarioMesa>)Session["usrMesas"]; }

                wfiplib.usuarioMesa oUsrMesa = recuperaCaptura();

                if (!existe(LstUsrMesas, oUsrMesa.IdMesa))
                {
                    LstUsrMesas.Add(oUsrMesa);

                    Session["usrMesas"] = LstUsrMesas;
                    rptMesas.DataSource = LstUsrMesas;
                    rptMesas.DataBind();
                    Limpia();
                }
            }
        }

        private bool existe(List<wfiplib.usuarioMesa> pLstUsrMesas, int pIdMesa)
        {
            bool resultado = false;
            foreach (wfiplib.usuarioMesa oUsrMesa in pLstUsrMesas)
            {
                if (oUsrMesa.IdMesa == pIdMesa) { resultado = true; }
            }
            return resultado;
        }

        private wfiplib.usuarioMesa recuperaCaptura()
        {
            wfiplib.usuarioMesa resultado = new wfiplib.usuarioMesa();
            resultado.IdUsuario = Convert.ToInt32(hf_Id.Value);
            if (ddlFlujo.SelectedIndex > 0) { resultado.IdFlujo = Convert.ToInt32(ddlFlujo.SelectedValue); resultado.FlujoNombre = ddlFlujo.SelectedItem.Text; }
            if (ddlMesa.SelectedIndex > 0) { resultado.IdMesa = Convert.ToInt32(ddlMesa.SelectedValue); resultado.MesaNombre = ddlMesa.SelectedItem.Text; }
            if (ddlTipoTrámite.SelectedIndex > 0) { resultado.IdTipoTramite = Convert.ToInt32(ddlTipoTrámite.SelectedValue); resultado.TramiteNombre = ddlTipoTrámite.SelectedItem.Text; }
            return resultado;
        }

        private void Limpia()
        {
            //ddlFlujo.SelectedIndex=0;
            ddlMesa.SelectedIndex=0;
            ddlTipoTrámite.SelectedIndex=0;
        }

        protected void rptMesas_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName.Equals("eliminar"))
            {
                int idMesa= Convert.ToInt32(e.CommandArgument);
                List<wfiplib.usuarioMesa> LstUsrMesas = new List<wfiplib.usuarioMesa>();
                List<wfiplib.usuarioMesa> LstUsrMesasTmp = new List<wfiplib.usuarioMesa>();
                if (Session["usrMesas"] != null) { LstUsrMesas = (List<wfiplib.usuarioMesa>)Session["usrMesas"]; }

                foreach (wfiplib.usuarioMesa oUsrMesa in LstUsrMesas)
                {
                    if (oUsrMesa.IdMesa != idMesa) { LstUsrMesasTmp.Add(oUsrMesa); }
                }
                Session["usrMesas"] = LstUsrMesasTmp;
                rptMesas.DataSource = LstUsrMesasTmp;
                rptMesas.DataBind();

            }
        }
    }
}