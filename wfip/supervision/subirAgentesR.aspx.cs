using DevExpress.Web;
using System.Drawing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web.Internal;
using System.Data;
namespace wfip.supervision
{
    public partial class subirAgentesR : System.Web.UI.Page
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            if (Session["credencial"] == null)
                Response.Redirect("~/Default.aspx");
        }
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void UploadButton_Click(object sender, EventArgs e)
        {
            string archivo = FileUploadControl.FileName;
            int totalAgentes = 0;
            int totalPromotorias = 0;
            if (FileUploadControl.HasFile)
            {
                try
                {

                    if (FileUploadControl.PostedFile.ContentType == "application/vnd.ms-excel" || FileUploadControl.PostedFile.ContentType == "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
                    {
                        if (FileUploadControl.PostedFile.ContentLength < 5024000)
                        {
                            string filename = Path.GetFileName(FileUploadControl.FileName);
                            FileUploadControl.SaveAs(Server.MapPath("~/DocsUp/") + filename);
                            using (StreamReader sr = new StreamReader(Server.MapPath("~/DocsUp/") + filename, System.Text.Encoding.Default, false))
                            {
                                StatusLabel.Text = "Procesando el Layout..";
                                string datos = sr.ReadToEnd();
                                sr.Close();
                                Info.Text = datos;
                                string[] informacion = datos.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
                                string[] campos = informacion[0].Split(',');
                                if (string.Equals("CLAVE", campos[0]) && string.Equals("DESCRIPCION", campos[1])
                                    && string.Equals("TIPO_PERSONA", campos[2]) && string.Equals("PROMOTORIA",
                                    campos[3]) && string.Equals("DESC_PROMOTORIA", campos[4]) && string.Equals("NOMBRE_DE_REGION", campos[5])
                                    && string.Equals("RES_CP", campos[6]) && string.Equals("NOM_COCO", campos[7]))
                                {
                                    bool resultadoAgentes = false;
                                    for (int registros = 1; registros <= informacion.Length - 1; registros++)
                                    {
                                        resultadoAgentes = new wfiplib.admCatAgentes().agregarAgentes(informacion[registros]);
                                        if (resultadoAgentes)
                                        {
                                            totalAgentes++;
                                        }
                                    }
                                    bool resultadoPromotorias = false;
                                    for (int registros = 1; registros <= informacion.Length - 1; registros++)
                                    {
                                        resultadoPromotorias = new wfiplib.admCatAgentes().agregarPromotorias(informacion[registros]);
                                        if (resultadoPromotorias)
                                        {
                                            totalPromotorias++;
                                        }
                                    }

                                    StatusLabel.Text = "Agentes Procesados: " + totalAgentes + " Promotorias Procesadas: " + totalPromotorias;
                                }
                                else StatusLabel.Text = "El Layout no se puede cargar, verifique su estructura";

                            }

                        }
                        else
                            StatusLabel.Text = "Verifique el tamaño del archivo";
                    }
                    else
                        StatusLabel.Text = "El Layout debe tener formato CSV";
                }
                catch (Exception ex)
                {
                    StatusLabel.Text = "No se pudo subir el archivo. Error: " + ex.Message;
                }
            }
        }
    }
}