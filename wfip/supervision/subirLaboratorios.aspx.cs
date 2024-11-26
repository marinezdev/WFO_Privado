using System;
using System.IO;

namespace wfip.supervision
{
    public partial class subirLaboratorios : System.Web.UI.Page
    {
        wfiplib.Concentrado manejo_sesion = new wfiplib.Concentrado();
        wfiplib.admCat_Proveedor prov = new wfiplib.admCat_Proveedor();
        wfiplib.admCatCiudad cd = new wfiplib.admCatCiudad();

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

        protected void UploadButton_Click(object sender, EventArgs e)
        {
            string archivo = FileUploadControl.FileName;
            int totalProveedores = 0;
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
                                if (string.Equals("accion", campos[0]) 
                                    && string.Equals("estado", campos[1]) 
                                    && string.Equals("ciudad", campos[2])
                                    && string.Equals("proveedor", campos[3]) 
                                    && string.Equals("sucursal", campos[4])
                                    && string.Equals("direccion", campos[5]) 
                                    && string.Equals("zona", campos[6])
                                    && string.Equals("reaking", campos[7]) 
                                    && string.Equals("email1", campos[8])
                                    && string.Equals("email2", campos[9]) 
                                    && string.Equals("combo1", campos[10])
                                    && string.Equals("combo1p", campos[11]) 
                                    && string.Equals("combo2", campos[12])
                                    && string.Equals("combo2p", campos[13]) 
                                    && string.Equals("combo3", campos[14])
                                    && string.Equals("combo3p", campos[15]) 
                                    && string.Equals("antigenoprostatico", campos[16])
                                    && string.Equals("inasistencia", campos[17]))
                                {
                                    bool resultadoProveedores = false;
                                    for (int registros = 1; registros <= informacion.Length - 1; registros++)
                                    {
                                        string[] cadena = informacion[registros].Split(',');
                                        //resultadoProveedores = prov.Agregar(
                                        //    cd.SeleccionarIdCiudadPorCiudad(cadena[2].ToString()), 
                                        //    prov.SeleccionarIdProveedorPorSucursal(cadena[4].ToString()), 
                                        //    cadena[4].ToString(), 
                                        //    cadena[5].ToString(), 
                                        //    cadena[6].ToString(), 
                                        //    cadena[7].ToString(), 
                                        //    cadena[8].ToString(), 
                                        //    cadena[9].ToString(), 
                                        //    cadena[10].ToString(), 
                                        //    cadena[11].ToString(), 
                                        //    cadena[12].ToString(), 
                                        //    cadena[13].ToString(), 
                                        //    cadena[14].ToString(), 
                                        //    cadena[15].ToString(), 
                                        //    cadena[16].ToString(), 
                                        //    cadena[17].ToString(),
                                        //    cadena[0].ToString()
                                        //    );

                                        resultadoProveedores = prov.Agregar(
                                            cd.SeleccionarIdCiudadPorCiudad(cadena[2].ToString()), 
                                            cadena[3].ToString(), 
                                            cadena[4].ToString(), 
                                            cadena[5].ToString(), 
                                            cadena[6].ToString(), 
                                            cadena[7].ToString(), 
                                            cadena[8].ToString(), 
                                            cadena[9].ToString(), 
                                            cadena[10].ToString(), 
                                            cadena[11].ToString(), 
                                            cadena[12].ToString(), 
                                            cadena[13].ToString(), 
                                            cadena[14].ToString(), 
                                            cadena[15].ToString(), 
                                            cadena[16].ToString(), 
                                            cadena[17].ToString(),
                                            cadena[0].ToString()
                                            );

                                        if (resultadoProveedores)
                                        {
                                            totalProveedores++;
                                        }
                                    }
                                    StatusLabel.Text = "Proveedores Procesados: " + totalProveedores;
                                }
                                else
                                    StatusLabel.Text = "El Layout no se puede cargar, verifíque su estructura.";
                            }
                        }
                        else
                            StatusLabel.Text = "Verifíque el tamaño del archivo.";
                    }
                    else
                        StatusLabel.Text = "El Layout debe tener formato CSV.";
                }
                catch (Exception ex)
                {
                    StatusLabel.Text = "No se pudo subir el archivo. Error: " + ex.Message;
                }
            }
        }


    }
}