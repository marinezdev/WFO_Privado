using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;

namespace wfiplib
{
    public class admCatProveedor
    {
        BaseDeDatos b = new BaseDeDatos();

        public catProveedorP SeleccionarDetallePorId(string id)
        {
            string consulta = "SELECT * FROM cat_proveedor WHERE Id_proveedor = @proveedor";
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@proveedor", int.Parse(id), SqlDbType.Int);
            var reader = b.ExecuteReader();
            catProveedorP item = new catProveedorP();
            while (reader.Read())
            {
                item.Id_proveedor = int.Parse(reader["Id_proveedor"].ToString());
                item.Id_ciudad = reader["Id_ciudad"].ToString();
                item.proveedor = reader["proveedor"].ToString();
                item.sucursal = reader["sucursal"].ToString();
                item.direccion = reader["direccion"].ToString();
                item.zona = reader["zona"].ToString();
                item.rating = reader["rating"].ToString();
                item.email1 = reader["email1"].ToString();
                item.email2 = reader["email2"].ToString();
                item.combo1 = reader["combo1"].ToString();
                item.combo1p = reader["combo1p"].ToString();
                item.combo2 = reader["combo2"].ToString();
                item.combo2p = reader["combo2p"].ToString();
                item.combo3 = reader["combo3"].ToString();
                item.combo3p = reader["combo3p"].ToString();
                item.antigenoprostatico = reader["antigenoprostatico"].ToString();
                item.inasistencia = reader["inasistencia"].ToString();
                item.estadoregistro = reader["estadoregistro"].ToString();
            }
            reader = null;
            b.ConnectionCloseToTransaction();
            return item;
        }

        public DataTable SeleccionarValidar(string correo)
        {
            string consulta = "SELECT email1 FROM cat_proveedor WHERE email1 LIKE @correo OR email2 LIKE @correo";
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@correo", correo, SqlDbType.VarChar, 250);
            return b.Select(); 
        }

        private DataTable SeleccionarProveedor(string correo)
        {
            string consulta = "SELECT DISTINCT proveedor FROM cat_proveedor WHERE email1 LIKE @correo OR email2 LIKE @correo";
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@correo", correo, SqlDbType.VarChar, 250);
            return b.Select();
        }

        public void Proveedor_DropDownList(ref DropDownList dropdownlist, string correo)
        {
            LlenarControles.LlenarDropDownList(ref dropdownlist, SeleccionarProveedor(correo), "proveedor", "proveedor", "Seleccionar Proveedor");
        }

        private DataTable SeleccionarSucursales(string proveedor)
        {
            //string consulta = "SELECT id_proveedor, sucursal FROM cat_proveedor WHERE proveedor=@proveedor";
            string consulta = "SELECT id_proveedor, (SELECT cat_ciudad.ciudad FROM cat_ciudad WHERE cat_ciudad.Id_ciudad = cat_proveedor.Id_ciudad) + ' - ' + sucursal AS sucursal FROM cat_proveedor WHERE proveedor = @proveedor ORDER BY sucursal";
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@proveedor", proveedor, SqlDbType.VarChar, 250);
            return b.Select();
        }

        public void Sucursales_DropDownList(ref DropDownList dropdownlist, string proveedor)
        {
            LlenarControles.LlenarDropDownList(ref dropdownlist, SeleccionarSucursales(proveedor), "sucursal", "id_proveedor", "Seleccionar Sucursal");
        }

        public DataTable LaboratorioConCitasMedicas(DateTime fechaDesde, DateTime fechaHasta)
        {
            TimeSpan In = new TimeSpan(00, 00, 00);
            TimeSpan Fin = new TimeSpan(23, 59, 59);
            fechaDesde = fechaDesde.Date + In;
            fechaHasta = fechaHasta.Date + Fin;

            string formato = "yyyy-MM-dd HH:mm:ss";
            string fechaD = fechaDesde.ToString(formato);
            string fechaH = fechaHasta.ToString(formato);

            DataTable dt = new DataTable();
            bd data = new bd();
            string qLaboratorioConCitasMedicas = "" +
                "DECLARE @FECHAINI  DATETIME;" +
                "DECLARE @FECHAFIN  DATETIME;" +
                "SET @FECHAINI = TRY_CONVERT(DATETIME, '" + fechaD + "',121);" +
                "SET @FECHAFIN = TRY_CONVERT(DATETIME, '" + fechaH + "',121);" +
                "EXEC Laboratorios @FECHAINI, @FECHAFIN";
            dt = data.leeDatos(qLaboratorioConCitasMedicas);
            data.cierraBD();
            return dt;
        }

        public DataTable consultaLaboratorios(int Id_proveedor, int Combo)
        {

            DataTable dt = new DataTable();
            bd data = new bd();
            string qLaboratorioConCitasMedicas = "EXEC Laboratorios_Selecionar_PorId " + "'" + Id_proveedor + "','" + Combo + "'";
            dt = data.leeDatos(qLaboratorioConCitasMedicas);
            data.cierraBD();
            return dt;
        }
    }

    public class catProveedorP
    {
        public int Id_proveedor { get; set; }
        public string Id_ciudad { get; set; }
        public string proveedor { get; set; }
        public string sucursal { get; set; }
        public string direccion { get; set; }
        public string zona { get; set; }
        public string rating { get; set; }
        public string email1 { get; set; }
        public string email2 { get; set; }
        public string combo1 { get; set; }
        public string combo1p { get; set; }
        public string combo2 { get; set; }
        public string combo2p { get; set; }
        public string combo3 { get; set; }
        public string combo3p { get; set; }
        public string antigenoprostatico { get; set; }
        public string inasistencia { get; set; }
        public string estadoregistro { get; set; }
    }


}
