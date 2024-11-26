using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace wfip.promotoria
{
    public partial class FormatoEnviosImss : System.Web.UI.Page
    {
        public DataTable dt;

        wfiplib.Concentrado manejo_sesion = new wfiplib.Concentrado();
        Mensajes mensajes = new Mensajes();

        protected void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session["RegistrosTemporales"] = null;
                GVRegistros.DataSource = null;
                GVRegistros.DataBind();
            }
            manejo_sesion = (wfiplib.Concentrado)Session["credencial"];
            if (Session["credencial"] == null)
                Response.Redirect("~/Default.aspx");
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

            }
            else
            {
                if (Session["RegistrosTemporales"] == null)
                {
                    //Nuevo
                    dt = new DataTable();
                    dt.Columns.Add("Poliza", typeof(string)).MaxLength = 10;    //Numero de Poliza
                    dt.Columns.Add("Unidad", typeof(string)).MaxLength = 10;    //Unidad de Pago

                    GVRegistros.DataSource = dt;
                    GVRegistros.DataBind();

                    Session["RegistrosTemporales"] = dt;
                }
            }
        }

        protected void BtnAgregar_Click(object sender, EventArgs e)
        {
            dt = (DataTable)Session["RegistrosTemporales"];
            dt.Rows.Add(txtNoPoliza.Text, txtUnidadPago.Text);

            GVRegistros.DataSource = dt;
            GVRegistros.DataBind();

            Session["RegistrosTemporales"] = dt;
        }

        protected void BtnAceptar_Click(object sender, EventArgs e)
        {
            mensajes.MostrarMensaje(this, "Tramite Procesado, tu número de folio es: 123456", "esperaPromotoria.aspx");
        }
    }
}