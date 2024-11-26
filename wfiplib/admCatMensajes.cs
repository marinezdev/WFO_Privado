using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace wfiplib
{
    public class admCatMensajes
    {
        public string mensaje(string rolNombre)
        {
            string respuesta = "";
            bd BD = new bd();
            DataTable datos = BD.leeDatos("select top 1 id, mensaje from warningMessages where rolNombre = '" + rolNombre + "' and activo = 1 order by id desc;");
            if (datos.Rows.Count > 0)
            {
                respuesta = datos.Rows[0]["mensaje"].ToString();
            }
            datos.Dispose();
            BD.cierraBD();
            return respuesta;
        }

        public bool nuevo(Mensajes pMensaje)
        {
            bool resultado = false;
            StringBuilder SqlCmd = new StringBuilder("INSERT INTO cat_mensaje (Mensaje, Descripcion)");
            //SqlCmd.Append(",Mensaje");
            //SqlCmd.Append(",Descripcion");
            //SqlCmd.Append(")");*/
            SqlCmd.Append(" VALUES (");
            // SqlCmd += pDatos.Id_Control;
            //SqlCmd.Append(pMensaje.Id_Control.ToString());
            SqlCmd.Append("'" + pMensaje.Mensaje + "'");
            SqlCmd.Append(",'" + pMensaje.Descripcion + "'");
            SqlCmd.Append(");");
            bd BD = new bd();
            resultado = BD.ejecutaCmd(SqlCmd.ToString());

            BD.cierraBD();

            return resultado;
        }

        /// <summary>
        /// Obtiene el mensaje solicitado.
        /// </summary>
        /// <param name="pIdMensaje">Id del Mensaje.</param>
        /// <returns></returns>
        public string getMensaje(int pIdMensaje)
        {
            string respuesta = "";
            bd BD = new bd();
            DataTable datos = BD.leeDatos("SELECT mensaje FROM cat_mensaje WHERE Id_Control =" + pIdMensaje.ToString());
            if (datos.Rows.Count > 0)
            {
                respuesta = datos.Rows[0]["mensaje"].ToString();
            }
            datos.Dispose();
            BD.cierraBD();
            return respuesta;
        }

        public Mensajes carga(int pId_Control)
        {
            Mensajes respuesta = new Mensajes();
            bd BD = new bd();
            DataTable datos = BD.leeDatos("SELECT * FROM cat_mensaje WHERE Id_Control=" + pId_Control.ToString());
            if (datos.Rows.Count > 0) { respuesta = arma(datos.Rows[0]); }
            datos.Dispose();
            BD.cierraBD();
            return respuesta;
        }

        public List<Mensajes> ListaMensajes()
        {
            List<Mensajes> respuesta = new List<Mensajes>();
            bd BD = new bd();
            DataTable datos = BD.leeDatos("SELECT * FROM cat_mensaje order by Mensaje");
            foreach (DataRow reg in datos.Rows) { respuesta.Add(arma(reg)); }
            datos.Dispose();
            BD.cierraBD();
            return respuesta;
        }

        private Mensajes arma(DataRow pRegistro)
        {
            Mensajes respuesta = new Mensajes();
            if (!pRegistro.IsNull("Id_Control")) respuesta.Id_Control = Convert.ToInt32(pRegistro["Id_Control"]);
            if (!pRegistro.IsNull("Mensaje")) respuesta.Mensaje = Convert.ToString(pRegistro["Mensaje"]);
            if (!pRegistro.IsNull("Descripcion")) respuesta.Descripcion = Convert.ToString(pRegistro["Descripcion"]);
            //if (!pRegistro.IsNull("activo")) respuesta.activo = Convert.ToString(pRegistro["activo"]);
            return respuesta;
        }

        public bool Existe(string pDescripcion)
        {
            bool resultado = false;
            bd BD = new bd();
            DataTable datos = BD.leeDatos("SELECT * FROM cat_mensaje Where Nombre= '" + pDescripcion + "'");
            resultado = (datos.Rows.Count > 0);
            datos.Dispose();
            BD.cierraBD();
            return resultado;
        }

        public void modifica(Mensajes oDt)
        {
            StringBuilder SqlCmd = new StringBuilder("UPDATE cat_mensaje SET");
            SqlCmd.Append(" Mensaje='" + oDt.Mensaje + "'");
            SqlCmd.Append(",Descripcion='" + oDt.Descripcion + "'");
            SqlCmd.Append(" WHERE Id_Control=" + oDt.Id_Control);
            bd BD = new bd();
            BD.ejecutaCmd(SqlCmd.ToString());
            BD.cierraBD();
        }
    }

    public class Mensajes
    {
        private int mId_Control = 0;
        public int Id_Control { get { return mId_Control; } set { mId_Control = value; } }
        private string mMensaje = String.Empty;
        public string Mensaje { get { return mMensaje; } set { mMensaje = value; } }
        private string mDescripcion = String.Empty;
        public string Descripcion { get { return mDescripcion; } set { mDescripcion = value; } }
        //private string mactivo = String.Empty;
        //public string activo { get { return mactivo; } set { mactivo = value; } }


    }
}

