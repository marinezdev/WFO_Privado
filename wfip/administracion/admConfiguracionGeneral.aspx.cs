using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace wfip.administracion
{
    public partial class admConfiguracionGeneral : System.Web.UI.Page
    {
        wfiplib.Concentrado manejo_sesion = new wfiplib.Concentrado();
        wfiplib.admConfiguracionGeneral cg = new wfiplib.admConfiguracionGeneral();

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
                CargarValoresIniciales();
            }
        }

        protected void btnCerrar_Click(object sender, EventArgs e)
        {
            Response.Redirect("admsysEspera.aspx");
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            ActualizarValoresEstados();
        }

        protected void CargarValoresIniciales()
        {
            List<wfiplib.ConfiguracionGeneralEntity> cge = new List<wfiplib.ConfiguracionGeneralEntity>();
            cge = cg.ListaconfiguracionSistema();
            //Cargar los valores para cada campo
            txtDiasCaducidad.Text = cge[0].Estado.ToString();
            txtDiasPreviosAviso.Text = cge[1].Estado.ToString();
            ddlClaveAleatoria.SelectedValue = cge[2].Estado.ToString();
            rblClaveAlta.SelectedValue = cge[3].Estado.ToString();
            rblTipoGeneracionClave.SelectedValue = cge[4].Estado.ToString();
            ckhInicioSesionPrimeraVez.Checked = cge[5].Estado.ToString() == "1" ? true : false;
            txtIntentos.Text = cge[6].Estado.ToString();
        }

        protected void ActualizarValoresEstados()
        {
            Dictionary<int, int> valores = new Dictionary<int, int>();
            valores.Add(1, int.Parse(txtDiasCaducidad.Text));
            valores.Add(2, int.Parse(txtDiasPreviosAviso.Text));
            valores.Add(3, int.Parse(ddlClaveAleatoria.SelectedValue));
            valores.Add(4, int.Parse(rblClaveAlta.SelectedValue));
            valores.Add(5, int.Parse(rblTipoGeneracionClave.SelectedValue));
            valores.Add(6, ckhInicioSesionPrimeraVez.Checked == true ? 1 : 2);
            valores.Add(7, int.Parse(txtIntentos.Text));

            wfiplib.admConfiguracionGeneral cg = new wfiplib.admConfiguracionGeneral();
            foreach (KeyValuePair<int, int> par in valores)
            {
                cg.CambiarValoresConfiguracion(par.Value.ToString(), par.Key.ToString());
            }

            //Limpia campos
            txtDiasCaducidad.Text = "";
            txtDiasPreviosAviso.Text = "";
            ddlClaveAleatoria.SelectedIndex = 0;
            rblClaveAlta.SelectedIndex = 0;
            rblTipoGeneracionClave.SelectedIndex = 0;
            ckhInicioSesionPrimeraVez.Checked = false;
            txtIntentos.Text = "";

            ClientScript.RegisterStartupScript(GetType(), "Aviso", "<script>alert('Se guardaron los datos exitosamente.');</script>");
        }


    }
}