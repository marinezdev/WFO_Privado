using DevExpress.Web;
using System;
using System.Data;
using DevExpress.Export;
using DevExpress.XtraPrinting;

namespace wfip.supervision
{
    public partial class sprReporteProductividadPromotorias : System.Web.UI.Page
    {
        wfiplib.Concentrado manejo_sesion = new wfiplib.Concentrado();

        public object LbxPromotorias { get; private set; }

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

            }
        }

        protected void lnkExportar_Click(object sender, EventArgs e)
        {
            wfiplib.admTramite tramite = new wfiplib.admTramite();
            wfiplib.ExportarAExcel eax = new wfiplib.ExportarAExcel();

            string a = ASPxComboBoxAnn.SelectedItem.Value.ToString();
            string dEstatus = string.Empty;
            string estatus = cmbEstatus.Value.ToString();
            string[] listaEstatus = estatus.Split(';');

            string listaclaves = string.Empty;
            string[] nuevoestado = null;
            foreach (string estado in listaEstatus)
            {
                nuevoestado = estado.Split('-');
                listaclaves += nuevoestado[0] + ",";
            }

            listaclaves = listaclaves.Substring(0, listaclaves.Length - 1);
            DataSet ds = new DataSet();
            ds = tramite.ReporteProductividadPromotorias(a, listaclaves);

            eax.ExportarDataSetAExcel(this, ds);
            
        }

        protected void LbxPromotorias_Init(object sender, EventArgs e)
        {
            wfiplib.admCatPromotoria catpromotoria = new wfiplib.admCatPromotoria("");
            ASPxListBox listaUsuarios = (ASPxListBox)sender;
            foreach (DataRow promotoria in catpromotoria.Seleccionar().Rows)
            {
                listaUsuarios.Items.Add(promotoria["Nombre"].ToString(), promotoria["Id"].ToString());
            }
        }

        protected void btnFiltrar_Click(object sender, EventArgs e)
        {
            wfiplib.admTramite tramite = new wfiplib.admTramite();

            //Obtener los parámetros para llenar la tabla 
            string a = ASPxComboBoxAnn.SelectedItem.Value.ToString();

            string dEstatus = string.Empty;
            string estatus = cmbEstatus.Value.ToString();
            string[] listaEstatus = estatus.Split(';');

            string listaclaves = string.Empty;
            string[] nuevoestado = null;
            foreach (string estado in listaEstatus)
            {
                nuevoestado = estado.Split('-');
                listaclaves += nuevoestado[0] + ",";
            }

            listaclaves = listaclaves.Substring(0, listaclaves.Length - 1);

            ViewState["Datos"] = dvgdEstatusTramite.DataSource = tramite.ReporteProductividadPromotorias(a, listaclaves);
            dvgdEstatusTramite.DataBind();
        }

        protected void dvgdEstatusTramite_DataBinding(object sender, EventArgs e)
        {
            //Para reordenamiento de columnas
            if (dvgdEstatusTramite.SortCount > 0)
            {
                dvgdEstatusTramite.DataSource = ViewState["Datos"];
            }
        }
    }
}