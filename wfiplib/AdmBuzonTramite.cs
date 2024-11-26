using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace wfiplib
{
    public class AdmBuzonTramite
    {
        public bool Nuevo(BuzonTramite pDatos)
        {
            bool resultado = false;
            StringBuilder SqlCmd = new StringBuilder("Insert Into buzonTramite (Buzon_Id,Tramite_Id,FechaRegistro,UsuarioRegistro_Id,ObsEntrada,Estado) Values(");
            SqlCmd.Append(pDatos.Buzon_Id.ToString());
            SqlCmd.Append("," + pDatos.Tramite_Id.ToString());
            SqlCmd.Append(",GetDate()");
            SqlCmd.Append("," + pDatos.UsuarioRegistro_Id.ToString());
            SqlCmd.Append(",'" + pDatos.ObsEntrada + "'");
            SqlCmd.Append("," + pDatos.Estado.ToString("d"));
            SqlCmd.Append(")");
            bd BD = new bd();
            resultado = BD.ejecutaCmd(SqlCmd.ToString());
            BD.cierraBD();
            return resultado;
        }

        public BuzonTramite Carga(string pId)
        {
            BuzonTramite resultado = new BuzonTramite();
            bd BD = new bd();
            DataTable datos = BD.leeDatos("Select * from vw_buzonTramite Where Id=" + pId);
            if (datos.Rows.Count > 0) { resultado = Arma(datos.Rows[0]); }
            datos.Dispose();
            BD.cierraBD();
            return resultado;
        }

        public List<CboValorTexto> CboBuzonesSalida(int pUsuarioId)
        {
            List<CboValorTexto> resultado = new List<CboValorTexto> { new CboValorTexto("0", "SELECCIONAR") };
            bd BD = new bd();
            DataTable datos = BD.leeDatos("SELECT Buzon_Id, Buzon_Nombre FROM vw_usuarioBuzon WHERE Usuario_Id = " + pUsuarioId.ToString() + " AND Tipo='SALIDA' ORDER BY Buzon_Nombre");
            if(datos.Rows.Count > 0) { foreach (DataRow registro in datos.Rows) { resultado.Add(ArmaCboBuzonUsuario(registro)); } }
            datos.Dispose();
            BD.cierraBD();
            return resultado;
        }

        public int BuzonEntrada(int pUsuarioId)
        {
            int resultado = 0;
            bd BD = new bd();
            DataTable datos = BD.leeDatos("SELECT Buzon_Id FROM vw_usuarioBuzon WHERE Usuario_Id = " + pUsuarioId.ToString() + " AND Tipo='ENTRADA' ORDER BY Buzon_Id");
            if (datos.Rows.Count > 0) { resultado = Convert.ToInt32(datos.Rows[0]["Buzon_Id"]); }
            datos.Dispose();
            BD.cierraBD();
            return resultado;
        }

        private CboValorTexto ArmaCboBuzonUsuario(DataRow pRegistro)
        {
            CboValorTexto resultado = new CboValorTexto();
            if (!pRegistro.IsNull("Buzon_Id")) { resultado.valor = Convert.ToString(pRegistro["Buzon_Id"]); }
            if (!pRegistro.IsNull("Buzon_Nombre")) { resultado.texto = Convert.ToString(pRegistro["Buzon_Nombre"]); }
            return resultado;
        }

        public List<BuzonTramite> LstTramitesEnBuzon(int pBuzonId)
        {
            List<BuzonTramite> resultado = new List<BuzonTramite>();
            bd BD = new bd();
            DataTable datos = BD.leeDatos("SELECT * FROM vw_buzonTramite WHERE Buzon_Id = " + pBuzonId.ToString() + " AND Estado = " + E_EstadoBuzon.Espera.ToString("d") + " ORDER BY Id");
            if (datos.Rows.Count > 0) { foreach (DataRow registro in datos.Rows) { resultado.Add(Arma(registro)); } }
            datos.Dispose();
            BD.cierraBD();
            return resultado;
        }

        public List<BuzonTramite> LstTramitesEnBuzonTerminado(int pBuzonId)
        {
            List<BuzonTramite> resultado = new List<BuzonTramite>();
            bd BD = new bd();
            DataTable datos = BD.leeDatos("SELECT * FROM vw_buzonTramite WHERE Buzon_Id = " + pBuzonId.ToString() + " AND Estado = " + E_EstadoBuzon.Atendido.ToString("d") + " ORDER BY Id");
            if (datos.Rows.Count > 0) { foreach (DataRow registro in datos.Rows) { resultado.Add(Arma(registro)); } }
            datos.Dispose();
            BD.cierraBD();
            return resultado;
        }

        private BuzonTramite Arma(DataRow pRegistro)
        {
            BuzonTramite resultado = new BuzonTramite();
            if (!pRegistro.IsNull("Id")) { resultado.Id = Convert.ToInt32(pRegistro["Id"]); }
            if (!pRegistro.IsNull("Buzon_Id")) { resultado.Buzon_Id = Convert.ToInt32(pRegistro["Buzon_Id"]); }
            if (!pRegistro.IsNull("Tramite_Id")) { resultado.Tramite_Id = Convert.ToInt32(pRegistro["Tramite_Id"]); }
            if (!pRegistro.IsNull("UsuarioRegistro_Id")) { resultado.UsuarioRegistro_Id = Convert.ToInt32(pRegistro["UsuarioRegistro_Id"]); }
            if (!pRegistro.IsNull("UsuarioAtiende_Id")) { resultado.UsuarioAtiende_Id = Convert.ToInt32(pRegistro["UsuarioAtiende_Id"]); }
            if (!pRegistro.IsNull("FechaRegistro")) { resultado.FechaRegistro = Convert.ToDateTime(pRegistro["FechaRegistro"]); }
            if (!pRegistro.IsNull("FechaInicio")) { resultado.FechaInicio = Convert.ToDateTime(pRegistro["FechaInicio"]); }
            if (!pRegistro.IsNull("FechaTermina")) { resultado.FechaTermina = Convert.ToDateTime(pRegistro["FechaTermina"]); }
            if (!pRegistro.IsNull("ObsEntrada")) { resultado.ObsEntrada = Convert.ToString(pRegistro["ObsEntrada"]); }
            if (!pRegistro.IsNull("ObsSalida")) { resultado.ObsSalida = Convert.ToString(pRegistro["ObsSalida"]); }
            if (!pRegistro.IsNull("Estado")) { resultado.Estado = (E_EstadoBuzon)pRegistro["Estado"]; }
            if (!pRegistro.IsNull("Buzon_Nombre")) { resultado.Buzon_Nombre = Convert.ToString(pRegistro["Buzon_Nombre"]); }
            if (!pRegistro.IsNull("Tramite_Asunto")) { resultado.Tramite_Asunto = Convert.ToString(pRegistro["Tramite_Asunto"]); }
            if (!pRegistro.IsNull("Tramite_Nombre")) { resultado.Tramite_Nombre = Convert.ToString(pRegistro["Tramite_Nombre"]); }
            if (!pRegistro.IsNull("Tramite_Fecha")) { resultado.Tramite_Fecha = Convert.ToDateTime(pRegistro["Tramite_Fecha"]); }
            if (!pRegistro.IsNull("UsuarioRegistro_Nombre")) { resultado.UsuarioRegistro_Nombre = Convert.ToString(pRegistro["UsuarioRegistro_Nombre"]); }
            if (!pRegistro.IsNull("UsuarioAtiende_Nombre")) { resultado.UsuarioAtiende_Nombre = Convert.ToString(pRegistro["UsuarioAtiende_Nombre"]); }
            return resultado;
        }

        public void Termina(BuzonTramite pDatos)
        {
            StringBuilder SqlCmd = new StringBuilder("Update buzonTramite Set");
            SqlCmd.Append(" UsuarioAtiende_Id=" + pDatos.UsuarioAtiende_Id.ToString());
            SqlCmd.Append(",FechaInicio=DATEADD(MI,-5,GETDATE())");
            SqlCmd.Append(",FechaTermina=GetDate()");
            SqlCmd.Append(",ObsSalida='" + pDatos.ObsSalida + "'");
            SqlCmd.Append(",Estado=" + pDatos.Estado.ToString("d"));
            SqlCmd.Append(" Where Id=" + pDatos.Id.ToString());
            bd BD = new bd();
            BD.ejecutaCmd(SqlCmd.ToString());
            BD.cierraBD();
        }

        public void RegistraTerminado(BuzonTramite pDatos)
        {
            StringBuilder SqlCmd = new StringBuilder("Insert Into buzonTramite (Buzon_Id,Tramite_Id,FechaRegistro,UsuarioRegistro_Id,ObsEntrada,Estado,UsuarioAtiende_Id,FechaInicio,FechaTermina,ObsSalida) Values(");
            SqlCmd.Append(pDatos.Buzon_Id.ToString());
            SqlCmd.Append("," + pDatos.Tramite_Id.ToString());
            SqlCmd.Append(",GetDate()");
            SqlCmd.Append("," + pDatos.UsuarioRegistro_Id.ToString());
            SqlCmd.Append(",'" + pDatos.ObsEntrada + "'");
            SqlCmd.Append("," + pDatos.Estado.ToString("d"));
            SqlCmd.Append("," + pDatos.UsuarioAtiende_Id.ToString());
            SqlCmd.Append(",GetDate()");
            SqlCmd.Append(",DATEADD(SS,2,GETDATE())");
            SqlCmd.Append(",'" + pDatos.ObsSalida + "'");
            SqlCmd.Append(")");
            bd BD = new bd();
            BD.ejecutaCmd(SqlCmd.ToString());
            BD.cierraBD();
        }

        public List<BuzonTramite> Bitacora(int pTramiteId)
        {
            List<BuzonTramite> resultado = new List<BuzonTramite>();
            bd BD = new bd();
            DataTable datos = BD.leeDatos("SELECT * FROM vw_buzonTramite WHERE Tramite_Id = " + pTramiteId.ToString() + " ORDER BY Id");
            if (datos.Rows.Count > 0) { foreach (DataRow registro in datos.Rows) { resultado.Add(Arma(registro)); } }
            datos.Dispose();
            BD.cierraBD();
            return resultado;
        }
    }

    public class BuzonTramite
    {
        private int _Id = 0;
        public int Id { get => _Id; set => _Id = value; }
        private int _Buzon_Id = 0;
        public int Buzon_Id { get => _Buzon_Id; set => _Buzon_Id = value; }
        private int _Tramite_Id = 0;
        public int Tramite_Id { get => _Tramite_Id; set => _Tramite_Id = value; }
        private int _UsuarioRegistro_Id = 0;
        public int UsuarioRegistro_Id { get { return _UsuarioRegistro_Id; } set { _UsuarioRegistro_Id = value; } }
        private int _UsuarioAtiende_Id = 0;
        public int UsuarioAtiende_Id { get { return _UsuarioAtiende_Id; } set { _UsuarioAtiende_Id = value; } }
        private DateTime _FechaRegistro = new DateTime(2000, 1, 1, 0, 0, 0);
        public DateTime FechaRegistro { get { return _FechaRegistro; } set { _FechaRegistro = value; } }
        private DateTime _FechaInicio = new DateTime(2000, 1, 1, 0, 0, 0);
        public DateTime FechaInicio { get { return _FechaInicio; } set { _FechaInicio = value; } }
        private DateTime _FechaTermina = new DateTime(2000, 1, 1, 0, 0, 0);
        public DateTime FechaTermina { get { return _FechaTermina; } set { _FechaTermina = value; } }
        private string _ObsEntrada = "";
        public string ObsEntrada { get { return _ObsEntrada; } set { _ObsEntrada = value; } }
        private string _ObsSalida = "";
        public string ObsSalida { get { return _ObsSalida; } set { _ObsSalida = value; } }
        public E_EstadoBuzon Estado { get; set; }

        private string _Buzon_Nombre = "";
        public string Buzon_Nombre { get { return _Buzon_Nombre; } set { _Buzon_Nombre = value; } }
        private string _Tramite_Asunto = "";
        public string Tramite_Asunto { get { return _Tramite_Asunto; } set { _Tramite_Asunto = value; } }
        private string _Tramite_Nombre = "";
        public string Tramite_Nombre { get { return _Tramite_Nombre; } set { _Tramite_Nombre = value; } }
        private DateTime _Tramite_Fecha = new DateTime(2000, 1, 1, 0, 0, 0);
        public DateTime Tramite_Fecha { get { return _Tramite_Fecha; } set { _Tramite_Fecha = value; } }
        private string _UsuarioRegistro_Nombre = "";
        public string UsuarioRegistro_Nombre { get { return _UsuarioRegistro_Nombre; } set { _UsuarioRegistro_Nombre = value; } }
        private string _UsuarioAtiende_Nombre = "";
        public string UsuarioAtiende_Nombre { get { return _UsuarioAtiende_Nombre; } set { _UsuarioAtiende_Nombre = value; } }
    }
}
