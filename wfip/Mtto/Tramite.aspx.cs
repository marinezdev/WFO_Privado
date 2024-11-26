using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace wfip.Mtto
{
    public partial class Tramite : System.Web.UI.Page
    {
        wfiplib.admTramite tramite = new wfiplib.admTramite();
        wfiplib.admTramiteMesa tramitemesa = new wfiplib.admTramiteMesa();
        wfiplib.admBitacora admbitacora = new wfiplib.admBitacora();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Session["validar"].ToString()))
                Response.Redirect(".");
            if (!IsPostBack)
            {

            }
        }

        protected void BtnBuscar_Click(object sender, EventArgs e)
        {
            GVTramite.DataSource = tramite.TramiteEstatus(txtIdTramite.Text);
            GVTramite.EmptyDataText = "Ningún dato coincide con su búsqueda.";
            GVTramite.DataBind();

            GVFolio.DataSource = null;
            GVFolio.DataBind();

            GVBitacora.DataSource = null;
            GVBitacora.DataBind();
        }

        protected void GVTramite_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Accion")
            {
                string[] datos = e.CommandArgument.ToString().Split('|');
                GVFolio.DataSource = tramitemesa.TramiteMesaBuscar(datos[0]);
                GVFolio.DataBind();

                GVBitacora.DataSource = admbitacora.BitacoraTramite(datos[1]);
                GVBitacora.DataBind();

                DVDetalleBitacora.DataSource = admbitacora.BitacoraTramite(datos[1]);
                DVDetalleBitacora.DataBind();

                ViewState["Id01"] = datos[1];
            }
        }

        protected void GVBitacora_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Accion")
            {
                string id = e.CommandArgument.ToString();
                PnlDetalleBitacora.Visible = true;
            }
        }

        protected void BtnCerrar_Click(object sender, EventArgs e)
        {
            PnlDetalleBitacora.Visible = false;
        }

         protected void DVDetalleBitacora_PageIndexChanging(object sender, DetailsViewPageEventArgs e)
        {
            DVDetalleBitacora.PageIndex = e.NewPageIndex;
            DVDetalleBitacora.DataSource = admbitacora.BitacoraTramite(ViewState["Id01"].ToString());
            DVDetalleBitacora.DataBind();
        }

        protected void DVDetalleBitacora_ModeChanging(object sender, DetailsViewModeEventArgs e)
        {
            {
                if (DVDetalleBitacora.CurrentMode == DetailsViewMode.ReadOnly)
                {
                    DVDetalleBitacora.ChangeMode(DetailsViewMode.Edit);
                    DVDetalleBitacora.DataSource = admbitacora.BitacoraTramite(ViewState["Id01"].ToString());
                    DVDetalleBitacora.DataBind();
                }
                else if (DVDetalleBitacora.CurrentMode == DetailsViewMode.Edit)
                {

                    DVDetalleBitacora.ChangeMode(DetailsViewMode.ReadOnly);
                    DVDetalleBitacora.DataSource = admbitacora.BitacoraTramite(ViewState["Id01"].ToString());
                    DVDetalleBitacora.DataBind();
                }
            }
        }

        protected void DVDetalleBitacora_ItemUpdating(object sender, DetailsViewUpdateEventArgs e)
        {
            int idd = Convert.ToInt32(DVDetalleBitacora.Rows[0].Cells[1].Text);
            DropDownList estado = DVDetalleBitacora.Rows[7].Cells[1].Controls[1] as DropDownList;
            TextBox observacion = DVDetalleBitacora.Rows[8].Cells[1].Controls[1] as TextBox;
            TextBox observacionp = DVDetalleBitacora.Rows[9].Cells[1].Controls[1] as TextBox;

            admbitacora.BitacoraTramiteGuardarCambio(estado.SelectedValue, observacion.Text, observacionp.Text, idd.ToString());

            DVDetalleBitacora.ChangeMode(DetailsViewMode.ReadOnly);
            DVDetalleBitacora.DataSource = admbitacora.BitacoraTramite(ViewState["Id01"].ToString());
            DVDetalleBitacora.DataBind();
        }

        protected void DVDetalleBitacora_ItemDeleting(object sender, DetailsViewDeleteEventArgs e)
        {
            int idd = Convert.ToInt32(DVDetalleBitacora.DataKey[e.RowIndex].ToString());
            admbitacora.BitacoraTramiteEliminar(idd.ToString());
            Mensajes mensaje = new Mensajes();
            mensaje.MostrarMensaje(this, "Se eliminó el registro.");
            DVDetalleBitacora.DataSource = admbitacora.BitacoraTramite(ViewState["Id01"].ToString());
            DVDetalleBitacora.DataBind();
        }

        protected void DVDetalleBitacora_DataBound(object sender, EventArgs e)
        {
            int noRow = DVDetalleBitacora.Rows.Count - 1;//obtiene el número de registro
            if (noRow > 0)
            {
                LinkButton button = (LinkButton)(DVDetalleBitacora.Rows[noRow].Cells[0].Controls[2]);
                //Confirmación de eliminado del registro
                ((LinkButton)(button)).OnClientClick = "if(!confirm('¿Esta seguro de eliminar el registro?')){ return false; };";
            }
        }
    }
}