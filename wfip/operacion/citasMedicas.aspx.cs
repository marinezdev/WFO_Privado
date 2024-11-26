using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace wfip.operacion
{
    public partial class citasMedicas : System.Web.UI.Page
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
            Muestradatos();
        }

        private void Muestradatos()
        {
            int IdFlujo = manejo_sesion.Credencial.IdFlujo;
            DataTable Datos = new DataTable();
            Datos = (new wfiplib.Reportes()).TramitesEsperaResultados(IdFlujo);
            dvgdTramitesEspera.DataSource = Datos;
            dvgdTramitesEspera.DataBind();
        }

        protected void UploadButton_Click(object sender, EventArgs e)
        {
            string archivo = FileUploadControl.FileName;
            if (FileUploadControl.HasFile)
            {
                try
                {
                    if (FileUploadControl.PostedFile.ContentLength < 1024000)
                    {
                        string filename = Path.GetFileName(FileUploadControl.FileName);
                        FileUploadControl.SaveAs(Server.MapPath("~/" + (new wfiplib.insumos()).CarpetaInicial + "/") + filename);
                    }
                    else
                    {
                        StatusLabel.Text = "Estatus: El archivo debe ser de menos de 1000 kb!";
                    }
                }
                catch (Exception ex)
                {
                    StatusLabel.Text = "Upload status: No se pudo subir el archivo. Error: " + ex.Message;
                }
            }
        }
    }
}