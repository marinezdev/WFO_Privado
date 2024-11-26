using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web;

namespace wfip.administracion
{
    public partial class ejmGridComboBox : System.Web.UI.Page
    {
        wfiplib.Concentrado manejo_sesion = new wfiplib.Concentrado();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["credencial"] == null)
                Response.Redirect("~/Default.aspx");
            manejo_sesion = (wfiplib.Concentrado)Session["credencial"];
        }

        protected void grdMesas_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
        {
            Session["pIdFlujo"] = e.Parameters;
            llenaGrid();
        }

        private void llenaGrid()
        {
            System.Threading.Thread.Sleep(3000);
            grdMesas.DataSource = objDsMesas;
            grdMesas.DataBind();
        }

        protected void grdMesas_PageIndexChanged(object sender, EventArgs e)
        {
            llenaGrid();
        }

        protected void grdMesas_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            ASPxGridView g = sender as ASPxGridView;
            if (Session["pIdFlujo"] != null)
            {
                wfiplib.mesa datos = new wfiplib.mesa();
                datos.IdFlujo = Convert.ToInt32(Session["pIdFlujo"]);
                datos.Nombre = e.NewValues["Nombre"].ToString();
                datos.IdTipo = Convert.ToInt32(e.NewValues["Tipo"]);
                datos.IdEtapa = Convert.ToInt32(e.NewValues["Etapa"]);
                datos.ConApoyo = wfiplib.E_Estado.Inactivo;
                datos.ConCondicion = wfiplib.E_Estado.Inactivo;
                datos.IdEstado = Convert.ToInt32(e.NewValues["IdEstado"]);
                datos.IdMesaPadre = Convert.ToInt32(e.NewValues["MesaPadre"]);
                datos.Apoyo = wfiplib.E_Estado.Inactivo;
                datos.IdUsuario = manejo_sesion.Credencial.Id;
                //(new wfiplib.admMesa()).nuevo(datos);
                g.DataSource = objDsMesas;
                g.DataBind();
            }
            g.CancelEdit();
            e.Cancel = true;
        }

        protected void grdMesas_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            ASPxGridView g = sender as ASPxGridView;
            if (Session["pIdFlujo"] != null)
            {
                wfiplib.mesa datos = new wfiplib.mesa();
                datos.Id = Convert.ToInt32(e.Keys[g.KeyFieldName]); ;
                datos.Nombre = e.NewValues["Nombre"].ToString();
                datos.IdEstado = Convert.ToInt32(e.NewValues["IdEstado"]);
                datos.IdEtapa = Convert.ToInt32(e.NewValues["Etapa"]);
                datos.IdTipo = Convert.ToInt32(e.NewValues["Tipo"]);
                datos.IdMesaPadre = Convert.ToInt32(e.NewValues["MesaPadre"]);
                //(new wfiplib.admMesa()).modificaNombreEstado(datos);
                g.DataSource = objDsMesas;
                g.DataBind();
            }
            g.CancelEdit();
            e.Cancel = true;
        }
    }
}