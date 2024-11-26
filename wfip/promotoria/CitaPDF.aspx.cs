using iTextSharp.text;
using iTextSharp.text.html;
using iTextSharp.text.pdf;
using System;
using System.Data;
using System.IO;
using static iTextSharp.text.Font;

namespace wfip.promotoria
{
    public partial class CitaPDF : System.Web.UI.Page
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
            Propiedades.UrlCifrardo urlCifrardo = ConsultaParametros();

            if (urlCifrardo.Result)
            {
                wfiplib.admCartas cartas = new wfiplib.admCartas();
                PDF(Int32.Parse(urlCifrardo.IdTramite));
            }

            // VALIDA EL TIPO DE TRAMITE TANTO PUBLICO COMO PRIVADO
            //if (!String.IsNullOrEmpty(Request.QueryString["Id"]))
            //{
            //    String IdTramite = Request.Params["Id"].ToString();
            //    int Id = Int32.Parse(IdTramite);
            //    PDF(Id);
            //}
            //else
            //{
            //    Response.Redirect("~/Default.aspx");
            //}
        }
        protected void PDF(int Id)
        {
            DataTable listEstados = (new wfiplib.admEmisionVG()).CitaMedica(Id);
            if (listEstados.Rows.Count > 0)
            {
                DataRow row = listEstados.Rows[0];
                DataTable listCombos = (new wfiplib.admEmisionVG()).Combo(row["combo"].ToString());
                DataTable listConsideraciones = (new wfiplib.admEmisionVG()).consideraciones(row["combo"].ToString());

                string fmt = "000000";
                int id = Int32.Parse(row["IdCitaMedica"].ToString());

                String masculino = "";
                String femenino = "";
                if (row["Sexo"].ToString() == "Masculino")
                { masculino = "X"; }
                else if (row["Sexo"].ToString() == "Femenino")
                { femenino = "X"; }

                // Medidas de la hoja A4
                var document = new Document(PageSize.A4, 30, 30, 60, 40);
                document.SetMargins(30, 30, 40, 70);
                var output = new MemoryStream();
                var writer = PdfWriter.GetInstance(document, output);

                // COLOCA ENCABEZADO Y PIE DE PAGINA
                writer.PageEvent = new PDFFooter();
                document.Open();

                PdfPTable table = new PdfPTable(10);
                table.WidthPercentage = 100;

                iTextSharp.text.Image jpg = iTextSharp.text.Image.GetInstance("c:\\logo_grap.png");
                //PdfPCell imageCell = new PdfPCell(jpg);
                //imageCell.Colspan = 10; // either 1 if you need to insert one cell
                //imageCell.Border = 0;
                //imageCell.MinimumHeight = 50f;
                //imageCell.FixedHeight = 50f;
                //imageCell.HorizontalAlignment = Element.ALIGN_RIGHT;
                //table.AddCell(imageCell);

#pragma warning disable CS0612 // 'WebColors' está obsoleto
                BaseColor ColorAzul = WebColors.GetRGBColor("#0070C0");
#pragma warning restore CS0612 // 'WebColors' está obsoleto
                Font TextTitulo = new Font(FontFamily.HELVETICA, 35, Font.BOLD, BaseColor.WHITE);
                //PdfPCell cell = new PdfPCell(new Phrase("MetLife\n", TextTitulo));
                PdfPCell cell = new PdfPCell(jpg);
                cell.MinimumHeight = 50f;
                cell.FixedHeight = 50f;
                cell.BackgroundColor = ColorAzul;
                cell.Colspan = 10;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.BorderColorLeft = BaseColor.BLACK;
                cell.BorderColorRight = BaseColor.BLACK;
                cell.BorderColorTop = BaseColor.BLACK;
                cell.BorderWidthLeft = 1.5f;
                cell.BorderWidthRight = 1.5f;
                cell.BorderWidthTop = 1.5f;
                cell.BorderWidthBottom = 0f;
                table.AddCell(cell);


                if (row["IdCitaMedica"].ToString() == "GPO ANGELES")
                { 
                
                }
                else
                {
                    Font TextCarta = new Font(FontFamily.HELVETICA, 15, Font.BOLD, BaseColor.BLACK);
                    cell = new PdfPCell(new Phrase("\nCARTA CITA MÉDICA", TextCarta));
                    cell.Colspan = 10;
                    cell.FixedHeight = 45f;
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell.BorderColorTop = BaseColor.WHITE;
                    cell.BorderColorBottom = BaseColor.WHITE;
                    cell.BorderWidth = 2;
                    cell.BorderWidthLeft = 1.5f;
                    cell.BorderWidthRight = 1.5f;
                    cell.BorderWidthTop = 0f;
                    cell.BorderWidthBottom = 0f;
                    table.AddCell(cell);

                    Font Text = new Font(FontFamily.HELVETICA, 10, Font.NORMAL, BaseColor.BLACK);
                    cell = new PdfPCell(new Phrase("Folio: " + id.ToString(fmt), Text));
                    cell.Colspan = 10;
                    cell.FixedHeight = 17f;
                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    cell.BorderColorTop = BaseColor.WHITE;
                    cell.BorderColorBottom = BaseColor.WHITE;
                    cell.BorderWidthLeft = 1.5f;
                    cell.BorderWidthRight = 1.5f;
                    cell.BorderWidthTop = 0f;
                    cell.BorderWidthBottom = 0f;
                    table.AddCell(cell);

                    Font TextBlanco = new Font(FontFamily.HELVETICA, 10, Font.BOLD, BaseColor.WHITE);
                    cell = new PdfPCell(new Phrase("Datos generales ", TextBlanco));
                    cell.FixedHeight = 15f;
                    cell.Colspan = 10;
                    cell.BackgroundColor = ColorAzul;
                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    cell.BorderWidthLeft = 1.5f;
                    cell.BorderWidthRight = 1.5f;
                    cell.BorderWidthTop = 1.5f;
                    cell.BorderWidthBottom = 1.5f;
                    table.AddCell(cell);

                    Font TextNegrita = new Font(FontFamily.HELVETICA, 10, Font.BOLD, BaseColor.BLACK);
                    cell = new PdfPCell(new Phrase("Hospital ", TextNegrita));
                    cell.Colspan = 10;
                    cell.FixedHeight = 14f;
                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    cell.BorderWidthLeft = 1.5f;
                    cell.BorderWidthRight = 1.5f;
                    cell.BorderWidthTop = 0f;
                    cell.BorderWidthBottom = 0f;
                    table.AddCell(cell);

#pragma warning disable CS0612 // 'WebColors' está obsoleto
                    BaseColor ColorAzulClaro = WebColors.GetRGBColor("#DCE6F1");
#pragma warning restore CS0612 // 'WebColors' está obsoleto
                    cell = new PdfPCell(new Phrase(row["proveedor"].ToString() + " - " + row["sucursal"].ToString(), Text));
                    cell.BackgroundColor = ColorAzulClaro;
                    cell.Colspan = 10;
                    cell.FixedHeight = 14f;
                    cell.BorderWidthLeft = 1.5f;
                    cell.BorderWidthRight = 1.5f;
                    cell.BorderWidthTop = 0f;
                    cell.BorderWidthBottom = 0f;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase("Dirección ", TextNegrita));
                    cell.Colspan = 10;
                    cell.FixedHeight = 14f;
                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    cell.BorderWidthLeft = 1.5f;
                    cell.BorderWidthRight = 1.5f;
                    cell.BorderWidthTop = 0f;
                    cell.BorderWidthBottom = 0f;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase(row["direccion"].ToString(), Text));
                    cell.BackgroundColor = ColorAzulClaro;
                    cell.Colspan = 10;
                    cell.FixedHeight = 28f;
                    cell.BorderWidthLeft = 1.5f;
                    cell.BorderWidthRight = 1.5f;
                    cell.BorderWidthTop = 0f;
                    cell.BorderWidthBottom = 0f;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase("Nombre del paciente ", TextNegrita));
                    cell.Colspan = 10;
                    cell.FixedHeight = 14f;
                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    cell.BorderWidthLeft = 1.5f;
                    cell.BorderWidthRight = 1.5f;
                    cell.BorderWidthTop = 0f;
                    cell.BorderWidthBottom = 0f;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase(row["Nombre"].ToString(), Text));
                    cell.BackgroundColor = ColorAzulClaro;
                    cell.Colspan = 8;
                    cell.BorderWidthLeft = 1.5f;
                    cell.BorderWidthRight = 0f;
                    cell.BorderWidthTop = 0f;
                    cell.BorderWidthBottom = 0f;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase(" ", Text));
                    cell.Colspan = 2;
                    cell.FixedHeight = 14f;
                    cell.BorderWidthLeft = 0f;
                    cell.BorderWidthRight = 1.5f;
                    cell.BorderWidthTop = 0f;
                    cell.BorderWidthBottom = 0f;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase("Fecha cita", TextNegrita));
                    cell.FixedHeight = 14f;
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell.BorderWidthLeft = 1.5f;
                    cell.BorderWidthRight = 0f;
                    cell.BorderWidthTop = 0f;
                    cell.BorderWidthBottom = 0f;
                    table.AddCell(cell);


                    cell = new PdfPCell(new Phrase("Hora cita", TextNegrita));
                    cell.FixedHeight = 14f;
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell.BorderWidth = 0f;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase(" ", TextNegrita));
                    cell.Colspan = 4;
                    cell.FixedHeight = 14f;
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell.BorderWidth = 0f;
                    table.AddCell(cell);


                    cell = new PdfPCell(new Phrase("Edad", TextNegrita));
                    cell.FixedHeight = 14f;
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell.BorderWidth = 0f;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase("\nSexo", TextNegrita));
                    cell.FixedHeight = 14f;
                    cell.Rowspan = 2;
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell.BorderWidth = 0f;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase("Femenino", TextNegrita));
                    cell.FixedHeight = 14f;
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell.BorderWidth = 0f;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase(femenino, TextNegrita));
                    cell.BackgroundColor = ColorAzulClaro;
                    cell.FixedHeight = 14f;
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell.BorderWidthLeft = 0f;
                    cell.BorderWidthRight = 1.5f;
                    cell.BorderWidthTop = 0f;
                    cell.BorderWidthBottom = 0f;
                    table.AddCell(cell);

                    /******************************************************************/
                    /****************** TOMA LA OPCION SELECCIONADA *******************/
                    /******************************************************************/
                    ///////////////////

                    row["FechaSeleccionada"].ToString();
                    switch (Convert.ToInt32(row["FechaSeleccionada"].ToString()))
                    {
                        case 1:
                            cell = new PdfPCell(new Phrase(row["Fecha1"].ToString(), Text));
                            break;
                        case 2:
                            cell = new PdfPCell(new Phrase(row["Fecha2"].ToString(), Text));
                            break;
                        case 3:
                            cell = new PdfPCell(new Phrase(row["Fecha3"].ToString(), Text));
                            break;
                        case 4:
                            cell = new PdfPCell(new Phrase(row["Fecha4"].ToString(), Text));
                            break;
                    }

                    cell.Colspan = 2;
                    cell.BackgroundColor = ColorAzulClaro;
                    cell.FixedHeight = 14f;
                    cell.BorderWidthLeft = 1.5f;
                    cell.BorderWidthRight = 0f;
                    cell.BorderWidthTop = 0f;
                    cell.BorderWidthBottom = 0f;
                    table.AddCell(cell);
                    /*
                    cell = new PdfPCell(new Phrase(row["Hora1"].ToString(), Text));
                    cell.BackgroundColor = ColorAzulClaro;
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell.FixedHeight = 14f;
                    cell.BorderWidthLeft = 1.5f;
                    cell.BorderWidthRight = 0f;
                    cell.BorderWidthTop = 0f;
                    cell.BorderWidthBottom = 0f;
                    table.AddCell(cell);
                    */
                    cell = new PdfPCell(new Phrase(" ", TextNegrita));
                    cell.Colspan = 4;
                    cell.FixedHeight = 14f;
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell.BorderWidth = 0f;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase(row["Edad"].ToString(), Text));
                    cell.BackgroundColor = ColorAzulClaro;
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell.FixedHeight = 14f;
                    cell.BorderWidthLeft = 0f;
                    cell.BorderWidthRight = 0f;
                    cell.BorderWidthTop = 0f;
                    cell.BorderWidthBottom = 0f;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase("Masculino", TextNegrita));
                    cell.FixedHeight = 14f;
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell.BorderWidth = 0f;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase(masculino, TextNegrita));
                    cell.BackgroundColor = ColorAzulClaro;
                    cell.FixedHeight = 14f;
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell.BorderWidthLeft = 0f;
                    cell.BorderWidthRight = 1.5f;
                    cell.BorderWidthTop = 0f;
                    cell.BorderWidthBottom = 0f;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase(" ", TextNegrita));
                    cell.Colspan = 10;
                    cell.FixedHeight = 5f;
                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    cell.BorderWidthLeft = 1.5f;
                    cell.BorderWidthRight = 1.5f;
                    cell.BorderWidthTop = 1.5f;
                    cell.BorderWidthBottom = 0f;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase("Examenes a realizar ", TextBlanco));
                    cell.FixedHeight = 14f;
                    cell.Colspan = 10;
                    cell.BackgroundColor = ColorAzul;
                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    cell.BorderWidthLeft = 1.5f;
                    cell.BorderWidthRight = 1.5f;
                    cell.BorderWidthTop = 1.5f;
                    cell.BorderWidthBottom = 0f;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase("COMBO " + row["combo"].ToString(), TextNegrita));
                    cell.Colspan = 10;
                    cell.FixedHeight = 14f;
                    cell.BorderWidthLeft = 1.5f;
                    cell.BorderWidthRight = 1.5f;
                    cell.BorderWidthTop = 0f;
                    cell.BorderWidthBottom = 0f;
                    table.AddCell(cell);

                    int color = 1;
                    int filas = 0;
                    foreach (DataRow dtRow in listCombos.Rows)
                    {

                        if (dtRow["examen"].ToString() != "")
                        {
                            cell = new PdfPCell(new Phrase("", Text));
                            if (color == 1)
                            {
                                cell.BackgroundColor = ColorAzulClaro;
                            }
                            //cell.FixedHeight = 14f;
                            cell.BorderWidthLeft = 1.5f;
                            cell.BorderWidthRight = 0f;
                            cell.BorderWidthTop = 0f;
                            cell.BorderWidthBottom = 0f;
                            table.AddCell(cell);

                            cell = new PdfPCell(new Phrase("  " + dtRow["examen"].ToString(), Text));
                            if (color == 1)
                            {
                                cell.BackgroundColor = ColorAzulClaro;
                                color = 0;
                            }
                            else
                            {
                                color = 1;
                            }
                            cell.Colspan = 9;
                            //cell.FixedHeight = 14f;
                            cell.BorderWidthLeft = 0f;
                            cell.BorderWidthRight = 1.5f;
                            cell.BorderWidthTop = 0f;
                            cell.BorderWidthBottom = 0f;
                            table.AddCell(cell);
                        }
                        filas++;

                    }
                    for (int i = filas; i <= 13; i++)
                    {
                        cell = new PdfPCell(new Phrase(" ", Text));
                        if (color == 1)
                        {
                            cell.BackgroundColor = ColorAzulClaro;
                            color = 0;
                        }
                        else
                        {
                            color = 1;
                        }
                        cell.Colspan = 10;
                        //cell.FixedHeight = 14f;
                        cell.BorderWidthLeft = 1.5f;
                        cell.BorderWidthRight = 1.5f;
                        cell.BorderWidthTop = 0f;
                        cell.BorderWidthBottom = 0f;
                        table.AddCell(cell);
                    }

                    cell = new PdfPCell(new Phrase(" NOTA: " + row["Notas"].ToString(), TextNegrita));
                    if (color == 1)
                    {
                        cell.BackgroundColor = ColorAzulClaro;
                        //    color = 0;
                    }
                    //else
                    //{
                    //    color = 1;
                    //}
                    cell.Colspan = 10;
                    cell.FixedHeight = 50f;
                    cell.BorderWidthLeft = 1.5f;
                    cell.BorderWidthRight = 1.5f;
                    cell.BorderWidthTop = 0f;
                    cell.BorderWidthBottom = 0f;
                    table.AddCell(cell);


                    cell = new PdfPCell(new Phrase("                        ** Antígeno Prostático solo aplica para hombres con Edad a partir de 46 años", Text));
                    if (color == 1)
                    {
                        cell.BackgroundColor = ColorAzulClaro;
                        color = 0;
                    }
                    else
                    {
                        color = 1;
                    }
                    cell.Colspan = 10;
                    cell.FixedHeight = 14f;
                    cell.BorderWidthLeft = 1.5f;
                    cell.BorderWidthRight = 1.5f;
                    cell.BorderWidthTop = 0f;
                    cell.BorderWidthBottom = 0f;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase(" ", Text));
                    cell.Colspan = 10;
                    cell.FixedHeight = 14f;
                    cell.BorderWidthLeft = 1.5f;
                    cell.BorderWidthRight = 1.5f;
                    cell.BorderWidthTop = 0f;
                    cell.BorderWidthBottom = 1.5f;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase(" ", TextNegrita));
                    cell.Colspan = 10;
                    cell.FixedHeight = 5f;
                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    cell.BorderWidthLeft = 1.5f;
                    cell.BorderWidthRight = 1.5f;
                    cell.BorderWidthTop = 0f;
                    cell.BorderWidthBottom = 0f;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase("Consideraciones ", TextBlanco));
                    cell.FixedHeight = 14f;
                    cell.Colspan = 10;
                    cell.BackgroundColor = ColorAzul;
                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    cell.BorderWidthLeft = 1.5f;
                    cell.BorderWidthRight = 1.5f;
                    cell.BorderWidthTop = 1.5f;
                    cell.BorderWidthBottom = 0f;
                    table.AddCell(cell);

                    color = 1;
                    filas = 0;
                    foreach (DataRow dtRow in listConsideraciones.Rows)
                    {
                        if (dtRow["consideracion"].ToString() != "")
                        {
                            cell = new PdfPCell(new Phrase(dtRow["consideracion"].ToString(), Text));
                            if (color == 1)
                            {
                                cell.BackgroundColor = ColorAzulClaro;
                                color = 0;
                            }
                            else
                            {
                                color = 1;
                            }
                            cell.Colspan = 10;
                            cell.BorderWidthLeft = 1.5f;
                            cell.BorderWidthRight = 1.5f;
                            cell.BorderWidthTop = 0f;
                            cell.BorderWidthBottom = 0f;
                            table.AddCell(cell);
                        }
                        filas++;
                    }
                    for (int i = filas; i <= 4; i++)
                    {
                        cell = new PdfPCell(new Phrase(" ", Text));
                        if (color == 1)
                        {
                            cell.BackgroundColor = ColorAzulClaro;
                            color = 0;
                        }
                        else
                        {
                            color = 1;
                        }
                        cell.Colspan = 10;
                        //cell.FixedHeight = 14f;
                        cell.BorderWidthLeft = 1.5f;
                        cell.BorderWidthRight = 1.5f;
                        cell.BorderWidthTop = 0f;
                        cell.BorderWidthBottom = 0f;
                        table.AddCell(cell);
                    }
                    cell = new PdfPCell(new Phrase(" ", Text));
                    if (color == 1)
                    {
                        cell.BackgroundColor = ColorAzulClaro;
                        color = 0;
                    }
                    else
                    {
                        color = 1;
                    }
                    cell.Colspan = 10;
                    cell.FixedHeight = 14f;
                    cell.BorderWidthLeft = 1.5f;
                    cell.BorderWidthRight = 1.5f;
                    cell.BorderWidthTop = 0f;
                    cell.BorderWidthBottom = 1.5f;
                    table.AddCell(cell);


                    //Font TextPiePagina = new Font(FontFamily.HELVETICA, 9, Font.NORMAL, BaseColor.BLACK);
                    ////cell = new PdfPCell(new Phrase("Boulevard Manuel Avila Camacho Número 32, Pisos SKL, 14 a 20 y PH, Colonia Lomas de Chapultepec, Código Postal ", TextPiePagina));
                    ////cell = new PdfPCell(new Phrase("Avenida Insurgentes Sur No. 1457 pisos 7 al 14, Colonia Insurgentes  Mixcoac, Alcaldía Benito Juárez, C.P. 03920", TextPiePagina));
                    //cell = new PdfPCell(new Phrase("MetLife México, S.A. de C.V., Avenida Insurgentes Sur No. 1457 pisos 7 al 14, Colonia Insurgentes  Mixcoac, Alcaldía Benito Juárez, C.P. 03920, en la Ciudad de México,  Tel. 5328-7000 ó lada sin costo 01-800- 00 METLIFE", TextPiePagina));
                    //cell.Colspan = 10;
                    //cell.FixedHeight = 14f;
                    //cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    //cell.BorderWidth = 0;
                    //table.AddCell(cell);
                    /*
                    cell = new PdfPCell(new Phrase("03920 Ciudad de México, CDMX", TextPiePagina));
                    cell.Colspan = 10;
                    cell.FixedHeight = 14f;
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell.BorderWidth = 0;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase("Teléfono 5328-7000, Lada sin costo 01-800-00-MetLife (638-5433) ", TextPiePagina));
                    cell.Colspan = 10;
                    cell.FixedHeight = 14f;
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell.BorderWidth = 0;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase("MetLife México, S.A. de C.V. Registro Federal de Contribuyentes: MME 920427EM3", TextPiePagina));
                    cell.Colspan = 10;
                    cell.FixedHeight = 14f;
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell.BorderWidth = 0;
                    table.AddCell(cell);
                    */

                }

                document.Add(table);
                document.Close();
                Response.ContentType = "application/pdf";
                Response.AddHeader("Content-Disposition", "");
                Response.BinaryWrite(output.ToArray());
            }
        }

        private Propiedades.UrlCifrardo ConsultaParametros()
        {
            Propiedades.UrlCifrardo urlCifrardo = new Propiedades.UrlCifrardo();
            try
            {
                string parametros = (new Application.Operacion.UrlCifrardo()).Decrypt(Request.QueryString["data"].ToString());
                string IdTramite = "";

                String[] spearator = { "," };
                String[] strlist = parametros.Split(spearator, StringSplitOptions.RemoveEmptyEntries);

                foreach (String s in strlist)
                {

                    string BusqeudaIdTramite = stringBetween(s + ".", "Id=", ".");
                    if (BusqeudaIdTramite.Length > 0)
                    {
                        IdTramite = BusqeudaIdTramite;
                    }
                }

                if (IdTramite.Length > 0)
                {
                    urlCifrardo.IdTramite = IdTramite.ToString();
                    urlCifrardo.Result = true;
                }
                else
                {
                    urlCifrardo.IdTramite = "";
                }
            }
            catch (Exception ex)
            {
                urlCifrardo.Result = false;
            }

            return urlCifrardo;
        }
        public static string stringBetween(string Source, string Start, string End)
        {
            string result = "";
            if (Source.Contains(Start) && Source.Contains(End))
            {
                int StartIndex = Source.IndexOf(Start, 0) + Start.Length;
                int EndIndex = Source.IndexOf(End, StartIndex);
                result = Source.Substring(StartIndex, EndIndex - StartIndex);
                return result;
            }

            return result;
        }

    }


    public class PDFFooter : PdfPageEventHelper
    {
        // write on top of document
        public override void OnOpenDocument(PdfWriter writer, Document document)
        {
            base.OnOpenDocument(writer, document);
        }

        // write on start of each page
        public override void OnStartPage(PdfWriter writer, Document document)
        {
            //base.OnStartPage(writer, document);
            //PdfPTable tabFot = new PdfPTable(new float[] { 1F });
            //tabFot.TotalWidth = 540F;

            //iTextSharp.text.Image jpg = iTextSharp.text.Image.GetInstance("c:\\logo_grap.jpg");
            //PdfPCell imageCell = new PdfPCell(jpg);
            //imageCell.Colspan = 10; // either 1 if you need to insert one cell
            //imageCell.Border = 0;
            //imageCell.MinimumHeight = 50f;
            //imageCell.FixedHeight = 50f;
            //imageCell.HorizontalAlignment = Element.ALIGN_RIGHT;
            //tabFot.AddCell(imageCell);
            //imageCell = null;
            //tabFot.WriteSelectedRows(0, -1, 26, document.Top + 50, writer.DirectContent);
        }

        // write on end of each page
        public override void OnEndPage(PdfWriter writer, Document document)
        {
            base.OnEndPage(writer, document);
            PdfPTable tabFot = new PdfPTable(new float[] { 1F });
            Font TextFooter = new Font(FontFamily.HELVETICA, 10, Font.NORMAL, BaseColor.BLACK);
            PdfPCell cell;
            tabFot.TotalWidth = 540F;

            //cell = new PdfPCell(new Phrase("MetLife México, S.A. de C.V., Blvd. Manuel Ávila Camacho No.32, pisos SKL, 14 a 20 y PH. Col Lomas de Chapultepec, CP. 11000, Delegación Miguel Hidalgo, Ciudad de México, Tel. 5328-7000 ó lada sin costo 01-800-00 METLIFE", TextFooter));
            cell = new PdfPCell(new Phrase("Av. de los Insurgentes Sur 1457, Insurgentes Mixcoac, 03920 Ciudad de México, CDMX, México; pisos 7 al 14, \n\n Teléfono 5328-9000, Lada sin costo 01-800-00-MetLife (638-5433)\n MetLife México, S.A. de C.V. Registro Federal de Contribuyentes: MME 920427EM3", TextFooter));
            cell.Colspan = 10;
            cell.FixedHeight = 200f;
            cell.MinimumHeight = 200f;
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.Border = 0;
            tabFot.AddCell(cell);
            cell = null;

            tabFot.WriteSelectedRows(0, -100, 30, document.Bottom, writer.DirectContent);
        }

        //write on close of document
        public override void OnCloseDocument(PdfWriter writer, Document document)
        {
            base.OnCloseDocument(writer, document);
        }
    }
}