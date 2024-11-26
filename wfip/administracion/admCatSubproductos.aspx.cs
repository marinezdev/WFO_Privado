using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;


namespace wfip.administracion
{
    public partial class admCatSubproductos : System.Web.UI.Page
    {
    //    wfiplib.Concentrado manejo_sesion = new wfiplib.Concentrado();
        
        protected void Page_Init(object sender, EventArgs e)
        {
    //        if (Session["credencial"] == null)
    //            Response.Redirect("~/Default.aspx");
    //        manejo_sesion = (wfiplib.Concentrado)Session["credencial"];
        }

        protected void Page_Load(object sender, EventArgs e)
        {
    //        DataTable listProductos = (new wfiplib.admCatSubProductos()).cartgaCatProducto();
    //        ddlSelSubProducto.DataSource = listProductos;
    //        ddlSelSubProducto.DataBind();
    //        ddlSelSubProducto.DataTextField = "Nombre";
    //        ddlSelSubProducto.DataValueField = "idCatProducto";
    //        ddlSelSubProducto.DataBind();

    //        if (!IsPostBack)
    //        {
    //            ListaSubProductos();
    //        }

        }

        private void ListaSubProductos()
        {
    //        List<wfiplib.SubProductos> lista = (new wfiplib.admCatSubProductos()).ListaSubProductos();
    //        rptSubProductos.DataSource = lista;
    //        rptSubProductos.DataBind();
        }

        protected void ddlSelProducto_SelectedIndexChanged(object sender, EventArgs e)
        {
    //        if (!ddlSelSubProducto.SelectedValue.Equals("0"))
    //        {
    //            ltNomProducto.Text = ddlSelSubProducto.SelectedItem.Text;
    //            pintaProducto(ddlSelSubProducto.SelectedValue);
    //        }
    //        else ltNomProducto.Text = "";
        }

        private void pintaProducto(string pIdCatProducto)
        {
    //        /*List<wfiplib.admCatSubProductos> datos = (new wfiplib.admCatSubProductos()).(ddlSelProducto);
    //        if (datos.Count > 0)
    //        {
    //            rptSubProductos.DataSource = datos;
    //            rptSubProductos.DataBind();
    //            rptSubProductos.Visible = true;
    //        }
    //        else rptSubProductos.Visible = false;*/
        }

        protected void rptSubProductos_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
    //        if (e.CommandName.Equals("Editar"))
    //        {
    //            hdIdSubProductos.Value = e.CommandArgument.ToString();
    //            wfiplib.admCatSubProductos adm = new wfiplib.admCatSubProductos();
    //            wfiplib.SubProductos oSPdt = adm.carga(Convert.ToInt32(hdIdSubProductos.Value));
    //            txNombre.Text = oSPdt.Nombre;
    //            TxIdCatProducto.Text = oSPdt.IdCatProducto;
    //            txDescripcion.Text = oSPdt.Descripcion;
    //            btnGuardar.Visible = false;
    //            btnModificar.Visible = true;
    //            btnModCancela.Visible = true;
    //        }
        }

        protected void btnModificar_Click(object sender, EventArgs e)
        {
    //        wfiplib.admCatSubProductos adm = new wfiplib.admCatSubProductos();
    //        wfiplib.SubProductos oSPdt = RecuperaDatos();
    //        oSPdt.IdCatSubProducto = Convert.ToInt32(hdIdSubProductos.Value);
    //        adm.modifica(oSPdt);
    //        this.Limpiar();
    //        this.ListaSubProductos();
    //        btnModificar.Visible = false;
    //        btnModCancela.Visible = false;
    //        btnGuardar.Visible = true;
        }

        protected void btnModCancela_Click(object sender, EventArgs e)
        {
    //        this.Limpiar();
    //        btnModificar.Visible = false;
    //        btnModCancela.Visible = false;
    //        btnGuardar.Visible = true;
        }
    
        protected void btnGuardar_Click(object sender, EventArgs e)
        {
    //        ltMsg.Text = "";
    //        wfiplib.admCatSubProductos adm = new wfiplib.admCatSubProductos();
    //        wfiplib.SubProductos oSPdt = RecuperaDatos();

    //        bool Resultado = adm.nuevo(oSPdt);
    //        this.Limpiar();
    //        this.ListaSubProductos();

        }

        private void Limpiar()
        {
    //        txNombre.Text = String.Empty;
    //        txDescripcion.Text = String.Empty;
        }

    //    private wfiplib.SubProductos RecuperaDatos()
    //    {
    //        wfiplib.SubProductos oSPdt = new wfiplib.SubProductos();
    //        oSPdt.Nombre = txNombre.Text.ToUpper();
    //        oSPdt.Descripcion = txDescripcion.Text.ToUpper();
    //        return oSPdt;
    //    }

        protected void btnCerrar_Click(object sender, EventArgs e)
        {
            Response.Redirect("admsysEspera.aspx");
        }
    }
}