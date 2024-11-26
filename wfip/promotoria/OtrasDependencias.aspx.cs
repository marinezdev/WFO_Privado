using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.IO;
using OfficeOpenXml;
using System.Text.RegularExpressions;

namespace wfip.promotoria
{
    public partial class OtrasDependencias : System.Web.UI.Page
    {
        wfiplib.Concentrado manejo_sesion = new wfiplib.Concentrado();
        wfiplib.admEntidadFederativa ef = new wfiplib.admEntidadFederativa();
        wfiplib.admAseguradosTitulares at = new wfiplib.admAseguradosTitulares();
        Mensajes mensajes = new Mensajes();


        #region Eventos ***************************************************************************************************************************************

        protected void Page_Init(object sender, EventArgs e)
        {
            Session["RegistrosTemporales"] = null;
            manejo_sesion = (wfiplib.Concentrado)Session["credencial"];
            if (Session["credencial"] == null)
                Response.Redirect("~/Default.aspx");
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

            }
        }

        protected void BtnGuardar_Click(object sender, EventArgs e)
        {
            //Guardar los datos del grid en un nuevo archivo
            /*
            "1. Renombrar archivo ordinario de acuerdo al catálogo de nomenclaturas bajo el formato siguiente:
            NNAAAAQQ_ORRETUPSSIN##.dat
            2.Renombrar archivo extraordinario de acuerdo al catálogo de nomenclaturas bajo el formato siguiente:
            NNAAAAQQ_EXRETUPSSIN##.dat
            3.Renombrar archivo de cancelación de acuerdo al catálogo de nomenclaturas bajo el formato siguiente:
            NNAAAAQQ_CARETUPSSIN##.dat

            NN = Nomenclatura de acuerdo al retenedor
            AAAA = Formato año
            QQ = Quincena 01 a 24
            OR = Ordinaria dato fijo
            EX = Extraordinaria dato fijo
            CA = Canceación dato fijo
            RET = retenedor a 4 posiciones completando con ceros a la izquierda
            UP = unidad de pago a 3 posiciones completando con ceros a la izquierda
            SSIN = Producto dato fijo
            ##=01 para Ordinadinaria dato fijo 
            ##=Consecutivo para EX y CA, campo numérico (01 - 99)"
            */

        }

        protected void BtnSubirArchivo_Click(object sender, EventArgs e)
        {
            //Subir el archivo a un datatable para procesarlo
            if (AsyncFileUpload1.HasFile)
            {
                //sólo subir el archivo
                //ExcelPackage pagina = new ExcelPackage(AsyncFileUpload1.FileContent);
                //DataTable dt = new DataTable();
                //dt = wfiplib.ExtensionPaqueteriaExcel.Excel_A_DataTable(pagina);


                DataTable dt = new DataTable();
                dt.Columns.Add("Fila");
                string carpeta = "../ArchivosExcel/";
                carpeta = Server.MapPath(carpeta);
                //string nombreArchivo = AsyncFileUpload1.FileName;
                //AsyncFileUpload1.SaveAs(carpeta + nombreArchivo);

                using (StreamReader sr = new StreamReader(carpeta + "07_18.txt"))
                {
                    while (!sr.EndOfStream)
                    {
                        string[] parts = sr.ReadLine().Replace('�', 'N').Replace('/',' ').Replace('-', ' ').Replace('_', ' ').ToUpper().Split('\t');
                        //dt.Rows.Add(parts[0].ToString().Replace(' ','*'));
                        
                        dt.Rows.Add(parts[0].ToString().Substring(0, 22).Replace(' ', '*') +//CURP
                            parts[0].ToString().Substring(22, 13).Replace(' ', '*') +       //RFC
                            parts[0].ToString().Substring(35, 14).Replace(' ', '*') +       //Identificación Nominal
                            parts[0].ToString().Substring(49, 20).Replace(' ', '*') +       //Apellido Paterno
                            parts[0].ToString().Substring(69, 20).Replace(' ', '*') +       //Apellido Materno
                            parts[0].ToString().Substring(89, 20).Replace(' ', '*') +       //Nombre del servidor público
                            parts[0].ToString().Substring(109, 40).Replace(' ', '*') +      //Domicilio particular
                            parts[0].ToString().Substring(149, 40).Replace(' ', '*') +      //Colonia o localidad
                            parts[0].ToString().Substring(189, 40).Replace(' ', '*') +      //Delegación o municipio
                            parts[0].ToString().Substring(229, 5).Replace(' ', '*') +       //Código Postal
                            parts[0].ToString().Substring(234, 2).Replace(' ', '*') +       //Entidad Federativa
                            parts[0].ToString().Substring(233, 15).Replace(' ', '*') +      //Número telefónico
                            parts[0].ToString().Substring(248, 10).Replace(' ', '*') +      //Sueldo
                            parts[0].ToString().Substring(258, 10).Replace(' ', '*') +      //compensación garantizada
                            parts[0].ToString().Substring(268, 10).Replace(' ', '*') +      //Compensación especial
                            parts[0].ToString().Substring(278, 10).Replace(' ', '*') +      //Carrera magisterial
                            parts[0].ToString().Substring(288, 6).Replace(' ', '*') +       //Nivel de la plaza
                            parts[0].ToString().Substring(294, 1).Replace(' ', '*') +       //Zona económica
                            parts[0].ToString().Substring(295, 1).Replace(' ', '*') +       //Tipo de registro
                            parts[0].ToString().Substring(296, 1).Replace(' ', '*') +       //Tipo de movimiento
                            parts[0].ToString().Substring(297, 1).Replace(' ', '*') +       //Tipo de transacción
                            parts[0].ToString().Substring(298, 1).Replace(' ', '*') +       //Situación del servidor público
                            parts[0].ToString().Substring(299, 8).Replace(' ', '*') +       //Fecha de ingreso
                            parts[0].ToString().Substring(307, 8).Replace(' ', '*') +       //Fecha de pago de la nómina
                            parts[0].ToString().Substring(315, 8).Replace(' ', '*') +       //Fecha de nacimiento
                            parts[0].ToString().Substring(323, 4).Replace(' ', '*') +       //Clave del ramo
                            parts[0].ToString().Substring(327, 3).Replace(' ', '*') +       //Unidad de pago
                            parts[0].ToString().Substring(330, 3).Replace(' ', '*') +       //Código de deducción
                            parts[0].ToString().Substring(333, 13).Replace(' ', '*') +      //Importe de deducción
                            parts[0].ToString().Substring(346, 3).Replace(' ', '*') +       //Subgrupo
                            parts[0].ToString().Substring(349, 4).Replace(' ', '*') +       //Área
                            parts[0].ToString().Substring(353, 1).Replace(' ', '*') +       //Sexo
                            parts[0].ToString().Substring(354, 2).Replace(' ', '*') +       //Edad
                            parts[0].ToString().Substring(356, 10).Replace(' ', '*') +      //Número de póliza
                            parts[0].ToString().Substring(366, 1).Replace(' ', '*') +       //Estado civil
                            parts[0].ToString().Substring(367, 2).Replace(' ', '*') +       //Lugar de nacimiento
                            parts[0].ToString().Substring(369, 6).Replace(' ', '*')         //Pocentaje de aportación
                            );
                            
                    }
                }

                gvDatos.DataSource = dt;
                gvDatos.CellPadding = 4;
                gvDatos.CellSpacing = 1;
                gvDatos.BorderStyle = BorderStyle.None;
                gvDatos.BorderWidth = 0;
                //gvDatos.Font.Name = new FontNamesConverter("Arial");
                gvDatos.GridLines = GridLines.Both;
                gvDatos.RowStyle.Wrap = false;
                gvDatos.HeaderStyle.Font.Bold = false;
                
                gvDatos.RowDataBound += GvDatos_RowDataBound;
                gvDatos.DataBind();

                BtnGuardar.Visible = true;





            }
        }

        private void GvDatos_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            string tooltip = string.Empty;

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //Sumarizar las longitudes desde el inicio
                //Validar los datos insertados

                Regex _regex = new Regex(@"/-*_");
                Match match = _regex.Match(e.Row.Cells[0].ToString());
                
                //if (!match.Success)
                //{
                //    tooltip = "Uso incorrecto de caracteres en la cadena";
                //}
                string cadena = e.Row.Cells[0].Text;
                if (cadena.Length < 378)
                {
                    //e.Row.Cells[0].BackColor = System.Drawing.Color.Red;
                    tooltip += "/Longitud incorrecta";
                }

                if (!at.Seleccionar(curp:cadena.Substring(0, 18)))    //CURP
                    tooltip += "/CURP Incorrecto";
                if (cadena.Substring(23, 13).Length != 13)              //RFC
                    tooltip += "/RFC Incorrecto";
                if (cadena.Substring(35, 14).Length != 14)              //Identificación Nominal
                    tooltip += "/Identificación Nominal Incorrecta";
                if (cadena.Substring(49, 20).Length != 20)              //Apellido Paterno
                    tooltip += "/Apellido Paterno Incorrecto";
                if (cadena.Substring(69, 20).Length != 20)              //Apellido Materno
                    tooltip += "/Apellido Materno Incorrecto";
                if (cadena.Substring(89, 20).Length != 20)              //Nombre del Servidor público
                    tooltip += "/Nombre Incorrecto";
                if (cadena.Substring(109, 40).Length != 40)             //Domicilio particular
                    tooltip += "/Domicilio Incorrecto";
                if (cadena.Substring(149, 40).Length != 40)             //Colonia o localidad
                    tooltip += "/Colonia-Localidad Incorrecto";
                if (cadena.Substring(189, 40).Length != 40)             //Delegación o municipio
                    tooltip += "/Delegación o municipio Incorrecto";
                if (cadena.Substring(229, 5).Length != 5)               //Código Postal
                    tooltip += "/Código postal Incorrecto";
                if (!ef.ValidarEntidadFederativaPorValor(cadena.Substring(234, 2)))    //Entidad federativa
                    tooltip += "/Entidad federativa Incorrecta";
                if (cadena.Substring(233, 15).Length != 15)             //Teléfono
                    tooltip += "/Teléfono Incorrecto";
                if (cadena.Substring(248, 10).Length != 10)             //Sueldo
                    tooltip += "/Sueldo Incorrecto";
                if (cadena.Substring(258, 10).Length != 10)             //Compensación garantizada
                    tooltip += "/Compensación Garantizada Incorrecta";
                if (cadena.Substring(269, 10).Length != 10)             //Compensación especial
                    tooltip += "/Compensación especial Incorrecta";
                if (cadena.Substring(278, 10).Length != 10)             //Carrera magisterial
                    tooltip += "/Carrera Magisterial Incorrecta";

                e.Row.Cells[0].ToolTip = tooltip;
                
            }
        }

        #endregion

    }
}