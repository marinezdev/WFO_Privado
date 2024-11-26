using iTextSharp.text;
using iTextSharp.text.html;
using iTextSharp.text.pdf;
using System;
using System.Data;
using System.IO;
using static iTextSharp.text.Font;

namespace wfip.promotoria
{
    public partial class Carta : System.Web.UI.Page
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
            // VALIDA EL TIPO DE TRAMITE TANTO PUBLICO COMO PRIVADO
            if (!String.IsNullOrEmpty(Request.QueryString["Id"]))
            {
                String IdTramite = Request.Params["Id"].ToString();
                int Id = Int32.Parse(IdTramite);
                CartaPDF(Id);
            }
            else
            {
                Response.Redirect("~/Default.aspx");
            }
        }

        private void MuestraPDF()
        {
            string strDoctoWeb = "";
            string strDoctoServer = "";
            try
            {
                strDoctoWeb = "..\\DocsTemplate\\Hold_Example.pdf";
                strDoctoServer = Server.MapPath("~") + "\\DocsTemplate\\Hold_Example.pdf";
                if (File.Exists(strDoctoServer))
                {
                    ltMuestraPdf.Text = "<embed src='" + strDoctoWeb + "' style='width:100%; height:100%' type='application/pdf'>";
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        protected void CartaPDF(int Id)
        {
            MuestraPDF();

            //DataTable listEstados = (new wfiplib.admEmisionVG()).CitaMedica(Id);
            //if (listEstados.Rows.Count > 0)
            //{
            //    DataRow row = listEstados.Rows[0];
            //    DataTable listCombos = (new wfiplib.admEmisionVG()).Combo(row["combo"].ToString());
            //    DataTable listConsideraciones = (new wfiplib.admEmisionVG()).consideraciones(row["combo"].ToString());
            //    //DataRow rowCom = listCombos.Rows[0];

            //    string fmt = "000000";
            //    int id = Int32.Parse(row["IdCitaMedica"].ToString());

            //    String masculino = "";
            //    String femenino = "";
            //    if (row["Sexo"].ToString() == "Masculino")
            //    { masculino = "X"; }
            //    else if (row["Sexo"].ToString() == "Femenino")
            //    { femenino = "X"; }


            //    // Medidas de la hoja A4
            //    var document = new Document(PageSize.A4, 30, 30, 60, 40);
            //    var output = new MemoryStream();
            //    var writer = PdfWriter.GetInstance(document, output);

            //    document.Open();
            //    // TABLA DE CONTENIDO


            //    PdfPTable table = new PdfPTable(10);
            //    table.WidthPercentage = 100;

            //    BaseColor ColorAzul = WebColors.GetRGBColor("#0070C0");
            //    Font TextTitulo = new Font(FontFamily.HELVETICA, 35, Font.BOLD, BaseColor.WHITE);
            //    PdfPCell cell = new PdfPCell(new Phrase("MetLife\n", TextTitulo));
            //    cell.MinimumHeight = 45f;
            //    cell.BackgroundColor = ColorAzul;
            //    cell.Colspan = 10;
            //    cell.HorizontalAlignment = Element.ALIGN_LEFT;
            //    cell.BorderColorLeft = BaseColor.BLACK;
            //    cell.BorderColorRight = BaseColor.BLACK;
            //    cell.BorderColorTop = BaseColor.BLACK;
            //    cell.BorderWidthLeft = 1.5f;
            //    cell.BorderWidthRight = 1.5f;
            //    cell.BorderWidthTop = 1.5f;
            //    cell.BorderWidthBottom = 0f;
            //    table.AddCell(cell);

            //    Font TextCarta = new Font(FontFamily.HELVETICA, 15, Font.BOLD, BaseColor.BLACK);
            //    cell = new PdfPCell(new Phrase("\nCARTA CITA MÉDICA", TextCarta));
            //    cell.Colspan = 10;
            //    cell.FixedHeight = 45f;
            //    cell.HorizontalAlignment = Element.ALIGN_CENTER;
            //    cell.BorderColorTop = BaseColor.WHITE;
            //    cell.BorderColorBottom = BaseColor.WHITE;
            //    cell.BorderWidth = 2;
            //    cell.BorderWidthLeft = 1.5f;
            //    cell.BorderWidthRight = 1.5f;
            //    cell.BorderWidthTop = 0f;
            //    cell.BorderWidthBottom = 0f;
            //    table.AddCell(cell);

            //    Font Text = new Font(FontFamily.HELVETICA, 10, Font.NORMAL, BaseColor.BLACK);
            //    cell = new PdfPCell(new Phrase("Folio: " + id.ToString(fmt), Text));
            //    cell.Colspan = 10;
            //    cell.FixedHeight = 17f;
            //    cell.HorizontalAlignment = Element.ALIGN_LEFT;
            //    cell.BorderColorTop = BaseColor.WHITE;
            //    cell.BorderColorBottom = BaseColor.WHITE;
            //    cell.BorderWidthLeft = 1.5f;
            //    cell.BorderWidthRight = 1.5f;
            //    cell.BorderWidthTop = 0f;
            //    cell.BorderWidthBottom = 0f;
            //    table.AddCell(cell);

            //    Font TextBlanco = new Font(FontFamily.HELVETICA, 10, Font.BOLD, BaseColor.WHITE);
            //    cell = new PdfPCell(new Phrase("Datos generales ", TextBlanco));
            //    cell.FixedHeight = 15f;
            //    cell.Colspan = 10;
            //    cell.BackgroundColor = ColorAzul;
            //    cell.HorizontalAlignment = Element.ALIGN_LEFT;
            //    cell.BorderWidthLeft = 1.5f;
            //    cell.BorderWidthRight = 1.5f;
            //    cell.BorderWidthTop = 1.5f;
            //    cell.BorderWidthBottom = 1.5f;
            //    table.AddCell(cell);

            //    Font TextNegrita = new Font(FontFamily.HELVETICA, 10, Font.BOLD, BaseColor.BLACK);
            //    cell = new PdfPCell(new Phrase("Hospital ", TextNegrita));
            //    cell.Colspan = 10;
            //    cell.FixedHeight = 14f;
            //    cell.HorizontalAlignment = Element.ALIGN_LEFT;
            //    cell.BorderWidthLeft = 1.5f;
            //    cell.BorderWidthRight = 1.5f;
            //    cell.BorderWidthTop = 0f;
            //    cell.BorderWidthBottom = 0f;
            //    table.AddCell(cell);

            //    BaseColor ColorAzulClaro = WebColors.GetRGBColor("#DCE6F1");
            //    cell = new PdfPCell(new Phrase(row["proveedor"].ToString(), Text));
            //    cell.BackgroundColor = ColorAzulClaro;
            //    cell.Colspan = 10;
            //    cell.FixedHeight = 14f;
            //    cell.BorderWidthLeft = 1.5f;
            //    cell.BorderWidthRight = 1.5f;
            //    cell.BorderWidthTop = 0f;
            //    cell.BorderWidthBottom = 0f;
            //    table.AddCell(cell);

            //    cell = new PdfPCell(new Phrase("Dirección ", TextNegrita));
            //    cell.Colspan = 10;
            //    cell.FixedHeight = 14f;
            //    cell.HorizontalAlignment = Element.ALIGN_LEFT;
            //    cell.BorderWidthLeft = 1.5f;
            //    cell.BorderWidthRight = 1.5f;
            //    cell.BorderWidthTop = 0f;
            //    cell.BorderWidthBottom = 0f;
            //    table.AddCell(cell);

            //    cell = new PdfPCell(new Phrase(row["direccion"].ToString(), Text));
            //    cell.BackgroundColor = ColorAzulClaro;
            //    cell.Colspan = 10;
            //    cell.FixedHeight = 28f;
            //    cell.BorderWidthLeft = 1.5f;
            //    cell.BorderWidthRight = 1.5f;
            //    cell.BorderWidthTop = 0f;
            //    cell.BorderWidthBottom = 0f;
            //    table.AddCell(cell);

            //    cell = new PdfPCell(new Phrase("Nombre del paciente ", TextNegrita));
            //    cell.Colspan = 10;
            //    cell.FixedHeight = 14f;
            //    cell.HorizontalAlignment = Element.ALIGN_LEFT;
            //    cell.BorderWidthLeft = 1.5f;
            //    cell.BorderWidthRight = 1.5f;
            //    cell.BorderWidthTop = 0f;
            //    cell.BorderWidthBottom = 0f;
            //    table.AddCell(cell);

            //    cell = new PdfPCell(new Phrase(row["Nombre"].ToString(), Text));
            //    cell.BackgroundColor = ColorAzulClaro;
            //    cell.Colspan = 8;
            //    cell.BorderWidthLeft = 1.5f;
            //    cell.BorderWidthRight = 0f;
            //    cell.BorderWidthTop = 0f;
            //    cell.BorderWidthBottom = 0f;
            //    table.AddCell(cell);

            //    cell = new PdfPCell(new Phrase(" ", Text));
            //    cell.Colspan = 2;
            //    cell.FixedHeight = 14f;
            //    cell.BorderWidthLeft = 0f;
            //    cell.BorderWidthRight = 1.5f;
            //    cell.BorderWidthTop = 0f;
            //    cell.BorderWidthBottom = 0f;
            //    table.AddCell(cell);

            //    cell = new PdfPCell(new Phrase("Fecha cita", TextNegrita));
            //    cell.FixedHeight = 14f;
            //    cell.HorizontalAlignment = Element.ALIGN_CENTER;
            //    cell.BorderWidthLeft = 1.5f;
            //    cell.BorderWidthRight = 0f;
            //    cell.BorderWidthTop = 0f;
            //    cell.BorderWidthBottom = 0f;
            //    table.AddCell(cell);


            //    cell = new PdfPCell(new Phrase("Hora cita", TextNegrita));
            //    cell.FixedHeight = 14f;
            //    cell.HorizontalAlignment = Element.ALIGN_CENTER;
            //    cell.BorderWidth = 0f;
            //    table.AddCell(cell);

            //    cell = new PdfPCell(new Phrase(" ", TextNegrita));
            //    cell.Colspan = 4;
            //    cell.FixedHeight = 14f;
            //    cell.HorizontalAlignment = Element.ALIGN_CENTER;
            //    cell.BorderWidth = 0f;
            //    table.AddCell(cell);


            //    cell = new PdfPCell(new Phrase("Edad", TextNegrita));
            //    cell.FixedHeight = 14f;
            //    cell.HorizontalAlignment = Element.ALIGN_CENTER;
            //    cell.BorderWidth = 0f;
            //    table.AddCell(cell);

            //    cell = new PdfPCell(new Phrase("\nSexo", TextNegrita));
            //    cell.FixedHeight = 14f;
            //    cell.Rowspan = 2;
            //    cell.HorizontalAlignment = Element.ALIGN_CENTER;
            //    cell.BorderWidth = 0f;
            //    table.AddCell(cell);

            //    cell = new PdfPCell(new Phrase("Femenino", TextNegrita));
            //    cell.FixedHeight = 14f;
            //    cell.HorizontalAlignment = Element.ALIGN_CENTER;
            //    cell.BorderWidth = 0f;
            //    table.AddCell(cell);

            //    cell = new PdfPCell(new Phrase(femenino, TextNegrita));
            //    cell.BackgroundColor = ColorAzulClaro;
            //    cell.FixedHeight = 14f;
            //    cell.HorizontalAlignment = Element.ALIGN_CENTER;
            //    cell.BorderWidthLeft = 0f;
            //    cell.BorderWidthRight = 1.5f;
            //    cell.BorderWidthTop = 0f;
            //    cell.BorderWidthBottom = 0f;
            //    table.AddCell(cell);

            //    ///////////////////
            //    cell = new PdfPCell(new Phrase(row["Fecha1"].ToString(), Text));
            //    cell.BackgroundColor = ColorAzulClaro;
            //    cell.FixedHeight = 14f;
            //    cell.BorderWidthLeft = 1.5f;
            //    cell.BorderWidthRight = 0f;
            //    cell.BorderWidthTop = 0f;
            //    cell.BorderWidthBottom = 0f;
            //    table.AddCell(cell);

            //    cell = new PdfPCell(new Phrase(row["Hora1"].ToString(), Text));
            //    cell.BackgroundColor = ColorAzulClaro;
            //    cell.HorizontalAlignment = Element.ALIGN_CENTER;
            //    cell.FixedHeight = 14f;
            //    cell.BorderWidthLeft = 1.5f;
            //    cell.BorderWidthRight = 0f;
            //    cell.BorderWidthTop = 0f;
            //    cell.BorderWidthBottom = 0f;
            //    table.AddCell(cell);

            //    cell = new PdfPCell(new Phrase(" ", TextNegrita));
            //    cell.Colspan = 4;
            //    cell.FixedHeight = 14f;
            //    cell.HorizontalAlignment = Element.ALIGN_CENTER;
            //    cell.BorderWidth = 0f;
            //    table.AddCell(cell);

            //    cell = new PdfPCell(new Phrase(row["Edad"].ToString(), Text));
            //    cell.BackgroundColor = ColorAzulClaro;
            //    cell.HorizontalAlignment = Element.ALIGN_CENTER;
            //    cell.FixedHeight = 14f;
            //    cell.BorderWidthLeft = 0f;
            //    cell.BorderWidthRight = 0f;
            //    cell.BorderWidthTop = 0f;
            //    cell.BorderWidthBottom = 0f;
            //    table.AddCell(cell);

            //    cell = new PdfPCell(new Phrase("Masculino", TextNegrita));
            //    cell.FixedHeight = 14f;
            //    cell.HorizontalAlignment = Element.ALIGN_CENTER;
            //    cell.BorderWidth = 0f;
            //    table.AddCell(cell);

            //    cell = new PdfPCell(new Phrase(masculino, TextNegrita));
            //    cell.BackgroundColor = ColorAzulClaro;
            //    cell.FixedHeight = 14f;
            //    cell.HorizontalAlignment = Element.ALIGN_CENTER;
            //    cell.BorderWidthLeft = 0f;
            //    cell.BorderWidthRight = 1.5f;
            //    cell.BorderWidthTop = 0f;
            //    cell.BorderWidthBottom = 0f;
            //    table.AddCell(cell);

            //    cell = new PdfPCell(new Phrase(" ", TextNegrita));
            //    cell.Colspan = 10;
            //    cell.FixedHeight = 5f;
            //    cell.HorizontalAlignment = Element.ALIGN_LEFT;
            //    cell.BorderWidthLeft = 1.5f;
            //    cell.BorderWidthRight = 1.5f;
            //    cell.BorderWidthTop = 1.5f;
            //    cell.BorderWidthBottom = 0f;
            //    table.AddCell(cell);

            //    cell = new PdfPCell(new Phrase("Examenes a realizar ", TextBlanco));
            //    cell.FixedHeight = 14f;
            //    cell.Colspan = 10;
            //    cell.BackgroundColor = ColorAzul;
            //    cell.HorizontalAlignment = Element.ALIGN_LEFT;
            //    cell.BorderWidthLeft = 1.5f;
            //    cell.BorderWidthRight = 1.5f;
            //    cell.BorderWidthTop = 1.5f;
            //    cell.BorderWidthBottom = 0f;
            //    table.AddCell(cell);

            //    cell = new PdfPCell(new Phrase("COMBO " + row["combo"].ToString(), TextNegrita));
            //    cell.Colspan = 10;
            //    cell.FixedHeight = 14f;
            //    cell.BorderWidthLeft = 1.5f;
            //    cell.BorderWidthRight = 1.5f;
            //    cell.BorderWidthTop = 0f;
            //    cell.BorderWidthBottom = 0f;
            //    table.AddCell(cell);

            //    int color = 1;
            //    int filas = 0;
            //    foreach (DataRow dtRow in listCombos.Rows)
            //    {

            //        if (dtRow["examen"].ToString() != "")
            //        {
            //            cell = new PdfPCell(new Phrase("", Text));
            //            if (color == 1)
            //            {
            //                cell.BackgroundColor = ColorAzulClaro;
            //            }
            //            //cell.FixedHeight = 14f;
            //            cell.BorderWidthLeft = 1.5f;
            //            cell.BorderWidthRight = 0f;
            //            cell.BorderWidthTop = 0f;
            //            cell.BorderWidthBottom = 0f;
            //            table.AddCell(cell);

            //            cell = new PdfPCell(new Phrase(" - " + dtRow["examen"].ToString(), Text));
            //            if (color == 1)
            //            {
            //                cell.BackgroundColor = ColorAzulClaro;
            //                color = 0;
            //            }
            //            else
            //            {
            //                color = 1;
            //            }
            //            cell.Colspan = 9;
            //            //cell.FixedHeight = 14f;
            //            cell.BorderWidthLeft = 0f;
            //            cell.BorderWidthRight = 1.5f;
            //            cell.BorderWidthTop = 0f;
            //            cell.BorderWidthBottom = 0f;
            //            table.AddCell(cell);
            //        }
            //        filas++;

            //    }
            //    for (int i = filas; i <= 15; i++)
            //    {
            //        cell = new PdfPCell(new Phrase(" ", Text));
            //        if (color == 1)
            //        {
            //            cell.BackgroundColor = ColorAzulClaro;
            //            color = 0;
            //        }
            //        else
            //        {
            //            color = 1;
            //        }
            //        cell.Colspan = 10;
            //        //cell.FixedHeight = 14f;
            //        cell.BorderWidthLeft = 1.5f;
            //        cell.BorderWidthRight = 1.5f;
            //        cell.BorderWidthTop = 0f;
            //        cell.BorderWidthBottom = 0f;
            //        table.AddCell(cell);
            //    }

            //    cell = new PdfPCell(new Phrase(" NOTA: " + row["Notas"].ToString(), TextNegrita));
            //    if (color == 1)
            //    {
            //        cell.BackgroundColor = ColorAzulClaro;
            //        color = 0;
            //    }
            //    else
            //    {
            //        color = 1;
            //    }
            //    cell.Colspan = 10;
            //    cell.FixedHeight = 50f;
            //    cell.BorderWidthLeft = 1.5f;
            //    cell.BorderWidthRight = 1.5f;
            //    cell.BorderWidthTop = 0f;
            //    cell.BorderWidthBottom = 0f;
            //    table.AddCell(cell);

            //    cell = new PdfPCell(new Phrase(" ", Text));
            //    cell.Colspan = 10;
            //    cell.FixedHeight = 14f;
            //    cell.BorderWidthLeft = 1.5f;
            //    cell.BorderWidthRight = 1.5f;
            //    cell.BorderWidthTop = 0f;
            //    cell.BorderWidthBottom = 1.5f;
            //    table.AddCell(cell);

            //    cell = new PdfPCell(new Phrase(" ", TextNegrita));
            //    cell.Colspan = 10;
            //    cell.FixedHeight = 5f;
            //    cell.HorizontalAlignment = Element.ALIGN_LEFT;
            //    cell.BorderWidthLeft = 1.5f;
            //    cell.BorderWidthRight = 1.5f;
            //    cell.BorderWidthTop = 0f;
            //    cell.BorderWidthBottom = 0f;
            //    table.AddCell(cell);

            //    cell = new PdfPCell(new Phrase("Consideraciones ", TextBlanco));
            //    cell.FixedHeight = 14f;
            //    cell.Colspan = 10;
            //    cell.BackgroundColor = ColorAzul;
            //    cell.HorizontalAlignment = Element.ALIGN_LEFT;
            //    cell.BorderWidthLeft = 1.5f;
            //    cell.BorderWidthRight = 1.5f;
            //    cell.BorderWidthTop = 1.5f;
            //    cell.BorderWidthBottom = 0f;
            //    table.AddCell(cell);

            //    color = 1;
            //    filas = 0;
            //    foreach (DataRow dtRow in listConsideraciones.Rows)
            //    {
            //        if (dtRow["consideracion"].ToString() != "")
            //        {
            //            cell = new PdfPCell(new Phrase(dtRow["consideracion"].ToString(), Text));
            //            if (color == 1)
            //            {
            //                cell.BackgroundColor = ColorAzulClaro;
            //                color = 0;
            //            }
            //            else
            //            {
            //                color = 1;
            //            }
            //            cell.Colspan = 10;
            //            cell.BorderWidthLeft = 1.5f;
            //            cell.BorderWidthRight = 1.5f;
            //            cell.BorderWidthTop = 0f;
            //            cell.BorderWidthBottom = 0f;
            //            table.AddCell(cell);
            //        }
            //        filas++;
            //    }
            //    for (int i = filas; i <= 4; i++)
            //    {
            //        cell = new PdfPCell(new Phrase(" ", Text));
            //        if (color == 1)
            //        {
            //            cell.BackgroundColor = ColorAzulClaro;
            //            color = 0;
            //        }
            //        else
            //        {
            //            color = 1;
            //        }
            //        cell.Colspan = 10;
            //        //cell.FixedHeight = 14f;
            //        cell.BorderWidthLeft = 1.5f;
            //        cell.BorderWidthRight = 1.5f;
            //        cell.BorderWidthTop = 0f;
            //        cell.BorderWidthBottom = 0f;
            //        table.AddCell(cell);
            //    }
            //    cell = new PdfPCell(new Phrase(" ", Text));
            //    if (color == 1)
            //    {
            //        cell.BackgroundColor = ColorAzulClaro;
            //        color = 0;
            //    }
            //    else
            //    {
            //        color = 1;
            //    }
            //    cell.Colspan = 10;
            //    cell.FixedHeight = 14f;
            //    cell.BorderWidthLeft = 1.5f;
            //    cell.BorderWidthRight = 1.5f;
            //    cell.BorderWidthTop = 0f;
            //    cell.BorderWidthBottom = 1.5f;
            //    table.AddCell(cell);


            //    Font TextPiePagina = new Font(FontFamily.HELVETICA, 9, Font.NORMAL, BaseColor.BLACK);
            //    cell = new PdfPCell(new Phrase("Boulevard Manuel Avila Camacho Número 32, Pisos SKL, 14 a 20 y PH, Colonia Lomas de Chapultepec, Código Postal ", TextPiePagina));
            //    cell.Colspan = 10;
            //    cell.FixedHeight = 14f;
            //    cell.HorizontalAlignment = Element.ALIGN_CENTER;
            //    cell.BorderWidth = 0;
            //    table.AddCell(cell);

            //    cell = new PdfPCell(new Phrase("11000, Delegación Miguel Hidalgo, México, Distrito Federal ", TextPiePagina));
            //    cell.Colspan = 10;
            //    cell.FixedHeight = 14f;
            //    cell.HorizontalAlignment = Element.ALIGN_CENTER;
            //    cell.BorderWidth = 0;
            //    table.AddCell(cell);

            //    cell = new PdfPCell(new Phrase("Teléfono 5328-7000, Lada sin costo 01-800-00-MetLife (638-5433) ", TextPiePagina));
            //    cell.Colspan = 10;
            //    cell.FixedHeight = 14f;
            //    cell.HorizontalAlignment = Element.ALIGN_CENTER;
            //    cell.BorderWidth = 0;
            //    table.AddCell(cell);

            //    cell = new PdfPCell(new Phrase("MetLife México, S.A. de C.V. Registro Federal de Contribuyentes: MME 920427EM3", TextPiePagina));
            //    cell.Colspan = 10;
            //    cell.FixedHeight = 14f;
            //    cell.HorizontalAlignment = Element.ALIGN_CENTER;
            //    cell.BorderWidth = 0;
            //    table.AddCell(cell);


            //    document.Add(table);
            //    document.Close();

            //    Response.ContentType = "application/pdf";
            //    Response.AddHeader("Content-Disposition", "");
            //    Response.BinaryWrite(output.ToArray());
            //}
            //else
            //{

            //}
        }
    }
}