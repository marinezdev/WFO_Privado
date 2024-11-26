using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace wfiplib
{
    public class admCat_Proveedor
    {
        BaseDeDatos b = new BaseDeDatos();

        public DataTable Seleccionar()
        {
            string consulta = "SELECT  c.estado, b.ciudad,  a.sucursal, a.direccion, a.zona, a.rating, a.email1, a.email2, a.combo1, a.combo1p, a.combo2, a.combo2p, a.combo3, a.combo3p, a.antigenoProstatico, a.inasistencia " + 
            "FROM cat_proveedor a, cat_ciudad b, cat_estados c " +
            "WHERE a.Id_ciudad = b.Id_ciudad " +
            "and b.Id_estado = c.Id_estado";
            b.ExecuteCommandQuery(consulta);
            return b.Select();
        }

        //public string SeleccionarIdProveedorPorSucursal(string sucursal)
        //{
        //    string consulta = "SELECT id_proveedor FROM cat_proveedor WHERE sucursal=@sucursal";
        //    b.ExecuteCommandQuery(consulta);
        //    b.AddParameter("@sucursal", sucursal.Trim(), SqlDbType.NVarChar, 50);
        //    if (b.Select().Rows.Count > 0)
        //        return b.Select().Rows[0][0].ToString();
        //    else
        //        return "0";
        //}



        public bool Agregar(string idciudad, string proveedor, string sucursal, string direccion, string zona, string rating, string email1, string email2,
            string combo1, string combo1p, string combo2, string combo2p, string combo3, string combo3p, string antigenoprostatico, string inasistencia, string estadoregistro)
        {
            string consulta = "INSERT INTO cat_proveedor (id_ciudad, proveedor, sucursal, direccion, zona, rating, email1, email2, combo1, combo1p, " +
            "combo2, combo2p, combo3, combo3p, antigenoprostatico, inasistencia, estadoregistro) " +
            "VALUES(@idciudad, @proveedor, @sucursal, @direccion, @zona, @rating, @email1, @email2, @combo1, @combo1p, " +
            "@combo2, @combo2p, @combo3, @combo3p, @antigenoprostatico, @inasistencia, @estadoregistro)";
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@idciudad", idciudad, SqlDbType.Int);
            b.AddParameter("@proveedor", proveedor, SqlDbType.VarChar, 250);
            b.AddParameter("@sucursal", sucursal, SqlDbType.VarChar, 500);
            b.AddParameter("@direccion", direccion, SqlDbType.VarChar, 350);
            b.AddParameter("@zona", zona, SqlDbType.VarChar, 50);
            b.AddParameter("@rating", rating, SqlDbType.VarChar, 50);
            b.AddParameter("@email1", email1, SqlDbType.VarChar, 250);
            b.AddParameter("@email2", email2, SqlDbType.VarChar, 250);
            b.AddParameter("@combo1", combo1, SqlDbType.VarChar, 2);
            b.AddParameter("@combo1p", combo1p, SqlDbType.VarChar, 10);
            b.AddParameter("@combo2", combo2, SqlDbType.VarChar, 2);
            b.AddParameter("@combo2p", combo2p, SqlDbType.VarChar, 10);
            b.AddParameter("@combo3", combo3, SqlDbType.VarChar, 2);
            b.AddParameter("@combo3p", combo3p, SqlDbType.VarChar, 10);
            b.AddParameter("@antigenoprostatico", antigenoprostatico, SqlDbType.VarChar, 10);
            b.AddParameter("@inasistencia", inasistencia, SqlDbType.VarChar, 10);
            b.AddParameter("@estadoregistro", estadoregistro, SqlDbType.NChar, 1);
            if (b.InsertUpdateDeleteWithTransaction() == 1)
                return true;
            else
                return false;
        }

        public int Actualizar(string idproveedor, string ciudad, string proveedor, string sucursal, string direccion, string zona, string rating, string email1, string email2,
            string combo1, string combo1p, string combo2, string combo2p, string combo3, string combo3p, string antigenoprostatico, string inasistencia, string estadoregistro)
        {
            string consulta = "UPDATE cat_proveedor SET ciudad=@ciudad, proveedor=@proveedor, sucursal=@sucursal, direccion=@direccion, zona=@zona, " +
            "rating=@rating, email1=@email1, email2=@email2, combo=@combo1, combo1p=@combo1p, combo2=@combo2, combo2p=@combo2p, combo3=@combo3, combo3p=@combo3p, " +
            "antigenoprostatico=@antigenoprostatico, inasistencia=@inasistencia, estadoregistro=@estadoregistro WHERE Id_Proveedor=@idproveedor ";
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@ciudad", ciudad, SqlDbType.Int);
            b.AddParameter("@proveedor", proveedor, SqlDbType.VarChar, 250);
            b.AddParameter("@sucursal", sucursal, SqlDbType.VarChar, 500);
            b.AddParameter("@direccion", direccion, SqlDbType.VarChar, 350);
            b.AddParameter("@zona", zona, SqlDbType.VarChar, 50);
            b.AddParameter("@rating", rating, SqlDbType.VarChar, 50);
            b.AddParameter("@email1", email1, SqlDbType.VarChar, 250);
            b.AddParameter("@email2", email2, SqlDbType.VarChar, 250);
            b.AddParameter("@combo1", combo1, SqlDbType.VarChar, 2);
            b.AddParameter("@combo1p", combo1p, SqlDbType.VarChar, 10);
            b.AddParameter("@combo2", combo2, SqlDbType.VarChar, 2);
            b.AddParameter("@combo2p", combo2p, SqlDbType.VarChar, 10);
            b.AddParameter("@combo3", combo3, SqlDbType.VarChar, 2);
            b.AddParameter("@combo3p", combo3p, SqlDbType.VarChar, 10);
            b.AddParameter("@antigenoprostatico", antigenoprostatico, SqlDbType.VarChar, 10);
            b.AddParameter("@inasistencia", inasistencia, SqlDbType.VarChar, 10);
            b.AddParameter("@estadoregistro", estadoregistro, SqlDbType.NChar, 1);
            b.AddParameter("@idproveedor", idproveedor, SqlDbType.Int);
            return b.InsertUpdateDeleteWithTransaction();
        }

        /// <summary>
        /// Actualiza el estado de un registro 
        /// Valores: A=alta, B=Baja, C=Modificación
        /// </summary>
        /// <param name="estadoregistro"></param>
        /// <param name="idlaboratorio"></param>
        /// <returns></returns>
        public int ActualizarEstadoRegistro(string estadoregistro, string idproveedor)
        {
            string consulta = "UPDATE cat_proveedor SET EstadoRegistro=@estadoregistro WHERE Id_proveedor=@idproveedor";
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@idproveedor", idproveedor, SqlDbType.Int);
            b.AddParameter("@estadoregistro", estadoregistro, SqlDbType.NChar, 1);
            return b.InsertUpdateDeleteWithTransaction();
        }

    }

    public class ProveedorP
    {
        public string Estado { get; set; }
        public string Ciudad { get; set; }
        public string Sucursal { get; set; }
        public string Direccion { get; set; }
        public string Zona { get; set; }
        public string Rating { get; set; }
        public string Email1 { get; set; }
        public string Email2 { get; set; }
        public string Combo1 { get; set; }
        public string Combo1p { get; set; }
        public string Combo2 { get; set; }
        public string Combo2p { get; set; }
        public string Combo3 { get; set; }
        public string Combo3p { get; set; }
        public string AntigenoProstatico { get; set; }
        public string Inasistencia { get; set; }
    }


}
