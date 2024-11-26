using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace wfip
{
    public partial class Archivos2 : System.Web.UI.Page
    {
        wfiplib.Archivos archivos = new wfiplib.Archivos();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GVArchivos.DataSource = archivos.Seleccionar();
                GVArchivos.DataBind();
            }
        }

        protected void GVArchivos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //Obtiene los archivos de una base de datos
            string[] args = e.CommandArgument.ToString().Split(',');
            string ToSaveFileTo = Server.MapPath("~\\" + (new wfiplib.insumos()).CarpetaInicial + "\\" + args[0]);
            byte[] fileData = archivos.Archivo(args[1]);
            Response.ContentType = "application/vnd.ms-excel";
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + ToSaveFileTo);
            Response.BinaryWrite(fileData);
            Response.End();
        }
    }
}