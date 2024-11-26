using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace wfiplib.Tablas.Institucional.Privado
{
    /// <summary>
    /// Procesos para el detalle de los servicios institucionales
    /// </summary>
    public class InsServicioDetalle
    {
        #region Entidades

        private string _IdInsServicios;
        private string _TipoServicio;
        private DateTime _InicioVigencia;
        private DateTime _FinVigencia;
        private string _SubGrupo; 
        private string _TipoProducto;
        private string _Accion;
        private string _NoCertificado1;
        private string _NombreAsegurado;
        private string _Parentesco;
        private string _Genero;
        private string _Sueldo;
        private string _Nacimiento1;
        private string _Nacimiento2;
        private string _Antiguedad;
        private string _TipoCarta;
        private string _NoCertificado2;
        private string _NombreAsegurado2;
        private string _TipoListado;
        private string _Nombres;
        private string _ApellidoPaterno;
        private string _ApellidoMaterno;
        private string _RFC;
        private string _CURP;
        private string _Nombre3;
        private string _RFC3;
       
        public string IdInsServicios { get => _IdInsServicios; set => _IdInsServicios = value; }
        public string TipoServicio { get => _TipoServicio; set => _TipoServicio = value; }
        public DateTime InicioVigencia { get => _InicioVigencia; set => _InicioVigencia = value; }
        public DateTime FinVigencia { get => _FinVigencia; set => _FinVigencia = value; }
        public string SubGrupo { get => _SubGrupo; set => _SubGrupo = value; }
        public string TipoProducto { get => _TipoProducto; set => _TipoProducto = value; }
        public string Accion { get => _Accion; set => _Accion = value; }
        public string NoCertificado1 { get => _NoCertificado1; set => _NoCertificado1 = value; }
        public string NombreAsegurado { get => _NombreAsegurado; set => _NombreAsegurado = value; }
        public string Parentesco { get => _Parentesco; set => _Parentesco = value; }
        public string Genero { get => _Genero; set => _Genero = value; }
        public string Sueldo { get => _Sueldo; set => _Sueldo = value; }
        public string Nacimiento1 { get => _Nacimiento1; set => _Nacimiento1 = value; }
        public string Nacimiento2 { get => _Nacimiento2; set => _Nacimiento2 = value; }
        public string Antiguedad { get => _Antiguedad; set => _Antiguedad = value; }
        public string TipoCarta { get => _TipoCarta; set => _TipoCarta = value; }
        public string NoCertificado2 { get => _NoCertificado2; set => _NoCertificado2 = value; }
        public string NombreAsegurado2 { get => _NombreAsegurado2; set => _NombreAsegurado2 = value; }
        public string TipoListado { get => _TipoListado; set => _TipoListado = value; }
        public string Nombres { get => _Nombres; set => _Nombres = value; }
        public string ApellidoPaterno { get => _ApellidoPaterno; set => _ApellidoPaterno = value; }
        public string ApellidoMaterno { get => _ApellidoMaterno; set => _ApellidoMaterno = value; }
        public string RFC { get => _RFC; set => _RFC = value; }
        public string CURP { get => _CURP; set => _CURP = value; }
        public string Nombre3 { get => _Nombre3; set => _Nombre3 = value; }
        public string RFC3 { get => _RFC3; set => _RFC3 = value; }

        #endregion 

        BaseDeDatos b = new BaseDeDatos();

        public void AgregarServicioDetalle(InsServiciosDetalleEntity serviciosdetalle)
        {
            //Cancelado, probable uso posterior
        }

        public DataTable ObtenerServiciosDetalle(int id)
        {
            string consulta = "SELECT b.Nombre AS TipoServicio, c.Nombre AS Accion, d.Nombre AS Genero, a.Nombres, a.APaterno, a.AMaterno " +
            "FROM insserviciosdetalle a LEFT JOIN insTipoServicio b ON a.IdTipoServicio = b.IdTipoServicio " +
            "LEFT JOIN insAccion c ON a.IdAccion = c.IdAccion " +
            "LEFT JOIN insGenero d ON a.IdGenero = d.IdGenero " +
            "WHERE a.IdInsServicios = @id";
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@id", id, SqlDbType.Int);
            return b.Select();
        }

    }

    public class InsServiciosDetalleEntity
    {
        public string IdInsServicios { get; set; }
        public string IdTipoServicio { get; set; }
        public string IdAccion { get; set; }
        public string Certificado { get; set; }
        public string SubGrupo { get; set; }
        public string Categoria { get; set; }
        public string Nombres { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string IdParentesco { get; set; }
        public string FNacimiento { get; set; }
        public string IDGenero { get; set; }
        public string Sueldo { get; set; }
        public string FMovimiento { get; set; }
        public string FAntiguedad { get; set; }
        public string PolizaRecAnt { get; set; }
        public string CiaAntRecAnt { get; set; }
        public string CertificadoImpreso { get; set; }
        public string IdTipoCarta { get; set; }
        public string NoCertificado2 { get; set; }
        public string Contratante { get; set; }
        public string RFC { get; set; }
        public string DomFiscal { get; set; }
        public string FormaPago { get; set; }
        public string IdServicioListado { get; set; }        
        public string Comentarios { get; set; }
        public string FLMovimiento { get; set; }
    }

        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
         
        
        
        
        
        
        
        
        
































}
