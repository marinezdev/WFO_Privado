using DevExpress.Web;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace wfip.supervision
{
    public partial class asignarTramites : System.Web.UI.Page
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
            //GridTramites.SettingsEditing.Mode = GridViewEditingMode.EditFormAndDisplayRow;
            Muestradatos();
        }
        private void Muestradatos()
        {
            try
            {
                string tramite = string.Empty;
                string rfc = string.Empty;
                string contratante = string.Empty;
                string asegurado = string.Empty;
                int IdFlujo = manejo_sesion.Credencial.IdFlujo;
              
                DataTable dtU = new DataTable();

                if (!string.IsNullOrEmpty(txtTramite.Text)) tramite = "'%" + txtTramite.Text.Trim() + "%'";
                else tramite = "'%'";
                if (!string.IsNullOrEmpty(txtRFC.Text)) rfc = "'%" + txtRFC.Text.Trim() + "%'";
                else rfc = "'%'";
                if (!string.IsNullOrEmpty(txtContratante.Text)) contratante = "'%" + txtContratante.Text.Trim() + "%'";
                else contratante = "'%'";
                if (!string.IsNullOrEmpty(txtAsegurado.Text)) asegurado = "'%" + txtAsegurado.Text.Trim() + "%'";
                else asegurado = "'%'";
                DataTable dtT = new DataTable();
                dtT = (new wfiplib.admMesa()).TramitesEnOperacion(tramite, rfc, contratante, asegurado, 1, manejo_sesion.Credencial.Id);
                GridTramite.DataSource = dtT;
                GridTramite.DataBind();
            }
            catch
            {
            }
        }
      
        protected void btnFiltroTramites_Click(object sender, EventArgs e)
        {
            //Muestradatos();
        }

        //protected void GridTramites_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        //{
        //    int idTramite = (int)e.Keys[0];
        //    int idMesaAnterior = (int)e.Keys[1]; 
        //    string idMesaNueva =e.NewValues["Mesa"].ToString();
        //    string idUsuario=e.NewValues["UsuarioNombre"].ToString();
        //    bool resultado = (new wfiplib.admMesa()).AsignacionTramites(idUsuario, idMesaAnterior.ToString(), idMesaNueva, idTramite.ToString());
        //    e.Cancel = true;
        //    GridTramites.CancelEdit();
        //    Muestradatos();
        //    DetalleTramites.Update();

        //}

        
        protected void detailGrid_BeforePerformDataSelect(object sender, EventArgs e)
        {

        }

        protected void detailGrid_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            ASPxGridView gridDetalle = (ASPxGridView)sender;
            int idTramite = int.Parse(gridDetalle.GetMasterRowFieldValues("IdTramite").ToString());
            string idUsuario = e.NewValues["Usuario"].ToString();
            int id = (int)e.Keys[0];
            bool resultado = (new wfiplib.admMesa()).AsignacionTramites(idUsuario, id.ToString(), idTramite.ToString());
            e.Cancel = true;
            gridDetalle.CancelEdit();
            
        }

        protected void detailGrid_Init(object sender, EventArgs e)
        {
            ASPxGridView gridDetalle = (ASPxGridView)sender;
            gridDetalle.SettingsEditing.Mode = GridViewEditingMode.EditFormAndDisplayRow;
            int idTramite = int.Parse(gridDetalle.GetMasterRowFieldValues("IdTramite").ToString());
            DataTable dtD = (new wfiplib.admMesa()).ListaTramites(idTramite.ToString());
            gridDetalle.DataSource = dtD;
        }

        protected void detailGrid_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
        {

            string IdMesa = string.Empty;
            ASPxGridView gridDetalle = (ASPxGridView)sender;

            if (gridDetalle.IsEditing && e.Column.FieldName == "Usuario")
            {
                object val = gridDetalle.GetRowValuesByKeyValue(e.KeyValue, "IdMesa");
                if (val != DBNull.Value) IdMesa = val.ToString();
                ASPxComboBox cmbUsuarios = e.Editor as ASPxComboBox;
                cmbUsuarios.DataSource = (new wfiplib.admMesa()).UsuariosDisponibles(IdMesa);
                cmbUsuarios.DataBind();
               
            }
        }
    }
}