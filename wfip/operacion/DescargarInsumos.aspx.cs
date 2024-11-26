using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace wfip.operacion
{
    public partial class DescargarInsumos : System.Web.UI.Page
    {
        public Mensajes mensajes = new Mensajes();
        wfiplib.Concentrado manejo_sesion = new wfiplib.Concentrado();

        protected void Page_Init(object sender, EventArgs e)
        {
            if (Session["credencial"] == null)
                Response.Redirect("~/Default.aspx", true);

            manejo_sesion = (wfiplib.Concentrado)Session["credencial"];
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            string strInsumo = Request.QueryString["tipo"].ToString();
            string strArchivo = Request.QueryString["file"].ToString();
            string strRuta = "";

            try
            {
                if (strInsumo == "insumo")
                {
                    //strRuta = Server.MapPath("~") + "\\DocsInsumos\\";
                    //strRuta = HttpContext.Current.Server.MapPath("~") + "\\DocsInsumos\\";
                    wfiplib.insumos oInsumo = new wfiplib.insumos();
                    strRuta = oInsumo.CarpetaArchivada;
                    //strRuta = Server.MapPath("~") + "DocsInsumos\\";

                    if (strArchivo.Length > 0)
                    {
                        if (File.Exists(strRuta + "" + strArchivo))
                        {
                            FileInfo file = new FileInfo(strRuta + strArchivo);
                            Response.Clear();
                            Response.AddHeader("Content-Disposition", "attachment;filename=" + file.Name);
                            Response.AddHeader("Content-Length", file.Length.ToString());
                            Response.ContentType = "application/octet-stream";
                            Response.Flush();
                            Response.WriteFile(file.FullName);
                            Response.Write("<script>window.close();</script>");
                            Response.End();
                        }
                        else
                        {
                            Response.Write("<br/><strong>E0003. Archivo no encontrado [" + strRuta + strArchivo + "].</strong>");
                        }
                    }
                    else
                    {
                        Response.Write("<br/><strong>E0002. Request no encontrado [" + strArchivo.ToUpper() + "].</strong>");
                    }

                }
                else
                {
                    Response.Write("<br/><strong>E0001. Seleccionador Incorrecto [" + strInsumo.ToUpper() + "]</strong>");
                }

                
            }
            catch (Exception ex)
            {
                mensajes.MostrarMensaje(this, ex.Message);
                
            }
        }
    }
}