using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace wfiplib
{
    public class admEmisionVG
    {
        /// <summary>
        /// Agrega una nueva cita Médica
        /// </summary>
        /// <param name="pServicioVida">Información para la Cita Médica</param>
        /// <returns>Boolean. Si se insertó o no la información para la cita médica</returns>
        /// <remarks>Agrega la Cita Médica desde la Promotoría una vez que la Mesa de Selección indicó que requiere cita Médica</remarks>
        public bool newCitaMedica(EmisionVG pCitaMedica)
        {
            bool blnResultado = false;

            if (pCitaMedica.Edad == "")
            {
                pCitaMedica.Edad = "0";
            }

            StringBuilder SqlCmdCitas = new StringBuilder("INSERT INTO CitasMedicas ([IdTramite],[Sexo],[Edad],[SumaAsegurada],[SumaPolizas],[PrimaTotal],[IdMoneda],[TempoLife],[Combo],[Cel],[Estado],[CelAgentePromotor],[Ciudad],[Correo],[LaboratorioHospital],[Direccion],[Fecha1],[Fecha2],[Fecha3],[Notas],[FechaSeleccionada],[CMGenerada],[Activo]");
            SqlCmdCitas.Append(") Values(");
            SqlCmdCitas.Append(pCitaMedica.IdTramite.ToString());
            SqlCmdCitas.Append(",'" + pCitaMedica.SexoCitaMedica + "'");
            SqlCmdCitas.Append("," + pCitaMedica.Edad);
            SqlCmdCitas.Append(",'" + pCitaMedica.SumaAsegurada + "'");
            SqlCmdCitas.Append(",'" + pCitaMedica.SumaPolizas + "'");
            SqlCmdCitas.Append(",'" + pCitaMedica.PrimaTotal + "'");
            SqlCmdCitas.Append(",'" + pCitaMedica.IdMoneda + "'");
            SqlCmdCitas.Append(",'" + pCitaMedica.TempoLife + "'");
            SqlCmdCitas.Append("," + pCitaMedica.Combo);
            SqlCmdCitas.Append(",'" + pCitaMedica.Cel + "'");
            SqlCmdCitas.Append(",'" + pCitaMedica.Estado + "'");
            SqlCmdCitas.Append(",'" + pCitaMedica.CelAgentePromotor + "'");
            SqlCmdCitas.Append(",'" + pCitaMedica.Ciudad + "'");
            SqlCmdCitas.Append(",'" + pCitaMedica.Correo + "'");
            SqlCmdCitas.Append(",'" + pCitaMedica.LaboratorioHospital + "'");
            SqlCmdCitas.Append(",'" + pCitaMedica.Direccion + "'");
            SqlCmdCitas.Append(",'" + pCitaMedica.Fecha1 + "'");
            SqlCmdCitas.Append(",'" + pCitaMedica.Fecha2 + "'");
            SqlCmdCitas.Append(",'" + pCitaMedica.Fecha3 + "'");
            SqlCmdCitas.Append(",'" + pCitaMedica.Notas + "'");
            SqlCmdCitas.Append(", NULL");
            SqlCmdCitas.Append(", NULL");
            SqlCmdCitas.Append(", 1");
            
            SqlCmdCitas.Append(")");
            bd BD = new bd();
            blnResultado = BD.ejecutaCmd(SqlCmdCitas.ToString());
            return blnResultado;
        }

        public bool nuevo(EmisionVG pServicioVida)
        //public bool nuevo(serviciosVida pServicioVida)
        {
            bool resultado = false;
            StringBuilder SqlCmd = new StringBuilder("INSERT INTO dbo.TRAM00003P (IdTramite,CPDES,FolioCPDES,EstatusCPDES,TipoPersona,Nombre,ApPaterno,ApMaterno,Sexo,FechaNacimiento,RFC,FechaConst,Nacionalidad,TitularNombre,TitularApPat,TitularApMat,TitularNacionalidad,TitularSexo,TitularFechaNacimiento,SumaAsegurada,SumaPolizas,PrimaTotal,IdMoneda,Detalle,HombreClave,Entidad,TitularEntidad");
            SqlCmd.Append(") Values(");
            SqlCmd.Append(pServicioVida.IdTramite);
            SqlCmd.Append(",'" + pServicioVida.CPDES + "'");
            SqlCmd.Append(",'" + pServicioVida.FolioCPDES + "'");
            SqlCmd.Append(",'" + pServicioVida.EstatusCPDES + "'");
            SqlCmd.Append(","  + pServicioVida.TipoPersona.ToString("d"));
            SqlCmd.Append(",'" + pServicioVida.Nombre + "'");
            SqlCmd.Append(",'" + pServicioVida.ApPaterno + "'");
            SqlCmd.Append(",'" + pServicioVida.ApMaterno + "'");
            SqlCmd.Append(",'" + pServicioVida.Sexo + "'");
            SqlCmd.Append(",'" + pServicioVida.FechaNacimiento + "'");
            SqlCmd.Append(",'" + pServicioVida.RFC + "'");
            SqlCmd.Append(",'" + pServicioVida.FechaConst + "'");
            SqlCmd.Append(",'" + pServicioVida.Nacionalidad + "'");
            SqlCmd.Append(",'" + pServicioVida.TitularNombre + "'");
            SqlCmd.Append(",'" + pServicioVida.TitularApPat + "'");
            SqlCmd.Append(",'" + pServicioVida.TitularApMat + "'");
            SqlCmd.Append(",'" + pServicioVida.TitularNacionalidad + "'");
            SqlCmd.Append(",'" + pServicioVida.TitularSexo + "'");
            SqlCmd.Append(",'" + pServicioVida.TitularFechaNacimiento + "'");
            SqlCmd.Append(",'" + pServicioVida.SumaAsegurada + "'");
            SqlCmd.Append(",'" + pServicioVida.SumaPolizas + "'");
            SqlCmd.Append(",'" + pServicioVida.PrimaTotal + "'");
            SqlCmd.Append(",'" + pServicioVida.IdMoneda + "'");
            SqlCmd.Append(",'" + pServicioVida.Detalle + "'");
            SqlCmd.Append(",'" + pServicioVida.HombreClave + "'");
            SqlCmd.Append(",'" + pServicioVida.EntidadFederativa + "'");
            SqlCmd.Append(",'" + pServicioVida.TitularEntidad + "'");
            SqlCmd.Append(")");
            bd BD = new bd();
            resultado = BD.ejecutaCmd(SqlCmd.ToString());

            if (resultado)
            {
                StringBuilder SqlCmdIns = new StringBuilder("INSERT INTO [producto]([IdTramite],[IdCatProducto],[IdCatSubProducto]) VALUES(");
                SqlCmdIns.Append(pServicioVida.IdTramite + ",'" + pServicioVida.Producto1 + "'" + ",'" + pServicioVida.Plan1 + "'");
                SqlCmdIns.Append(")");
                resultado = BD.ejecutaCmd(SqlCmdIns.ToString());
                
                if (pServicioVida.Combo != "")
                {
                    StringBuilder SqlCmdCitas = new StringBuilder("INSERT INTO[dbo].[CitasMedicas]([IdTramite],[Sexo],[Edad],[SumaAsegurada],[SumaPolizas],[PrimaTotal],[IdMoneda],[TempoLife],[Combo],[Cel],[Estado],[CelAgentePromotor],[Ciudad],[Correo],[LaboratorioHospital],[Direccion],[Fecha1],[Fecha2],[Fecha3],[Notas],[FechaSeleccionada],[CMGenerada],[Activo]");
                    SqlCmdCitas.Append(") Values(");
                    SqlCmdCitas.Append(pServicioVida.IdTramite.ToString());
                    SqlCmdCitas.Append(",'" + pServicioVida.SexoCitaMedica + "'");
                    SqlCmdCitas.Append("," + pServicioVida.Edad);
                    SqlCmdCitas.Append(",'" + pServicioVida.SumaAsegurada + "'");
                    SqlCmdCitas.Append(",'" + pServicioVida.SumaPolizas + "'");
                    SqlCmdCitas.Append(",'" + pServicioVida.PrimaTotal + "'");
                    SqlCmdCitas.Append(",'" + pServicioVida.IdMoneda + "'");
                    SqlCmdCitas.Append(",'" + pServicioVida.TempoLife + "'");
                    SqlCmdCitas.Append("," + pServicioVida.Combo);
                    SqlCmdCitas.Append(",'" + pServicioVida.Cel + "'");
                    SqlCmdCitas.Append(",'" + pServicioVida.Estado + "'");
                    SqlCmdCitas.Append(",'" + pServicioVida.CelAgentePromotor + "'");
                    SqlCmdCitas.Append(",'" + pServicioVida.Ciudad + "'");
                    SqlCmdCitas.Append(",'" + pServicioVida.Correo + "'");
                    SqlCmdCitas.Append(",'" + pServicioVida.LaboratorioHospital + "'");
                    SqlCmdCitas.Append(",'" + pServicioVida.Direccion + "'");
                    SqlCmdCitas.Append(",'" + pServicioVida.Fecha1 + "'");
                    SqlCmdCitas.Append(",'" + pServicioVida.Fecha2 + "'");
                    SqlCmdCitas.Append(",'" + pServicioVida.Fecha3 + "'");
                    SqlCmdCitas.Append(",'" + pServicioVida.Notas + "'");
                    if (pServicioVida.Combo == "1" && pServicioVida.Estado == getIdEstado("CIUDAD DE MÉXICO"))
                    {
                        SqlCmdCitas.Append(", 1");
                        SqlCmdCitas.Append(", 1");
                    }
                    else
                    {
                        SqlCmdCitas.Append(", NULL");
                        SqlCmdCitas.Append(", 0");
                    }
                    SqlCmdCitas.Append(", 1");
                    SqlCmdCitas.Append(")");
                    resultado = BD.ejecutaCmd(SqlCmdCitas.ToString());
                }
            }
            BD.cierraBD();
            return resultado;
        }

        public string getIdEstado(string pNombreEstado)
        {
            string strEstado = "-1";
            String SqlCmd = "SELECT * FROM cat_estados WHERE estado = '" + pNombreEstado + "';";
            bd BD = new bd();
            DataTable datos = BD.leeDatos(SqlCmd);
            if (datos.Rows.Count > 0)
            {
                if (!datos.Rows[0].IsNull("Id_estado"))
                {
                    strEstado = datos.Rows[0]["Id_estado"].ToString();
                }
                else
                {
                    strEstado = "-1";
                }
            }
            datos.Dispose();
            BD.cierraBD();

            return strEstado;
        }

        public bool NuevaCitaMedica(EmisionVG oEmisionVG)
        {
            bool resultado = false;
            bd BD = new bd();
            StringBuilder SqlCmdCitas = new StringBuilder("INSERT INTO[dbo].[CitasMedicas]([IdTramite],[Sexo],[Edad],[SumaAsegurada],[SumaPolizas],[PrimaTotal],[IdMoneda],[TempoLife],[Combo],[Cel],[Estado],[CelAgentePromotor],[Ciudad],[Correo],[LaboratorioHospital],[Direccion],[Fecha1],[Fecha2],[Fecha3],[Fecha4],[FechaSeleccionada],[CMGenerada],[Activo]");
            SqlCmdCitas.Append(") Values(");
            SqlCmdCitas.Append(oEmisionVG.IdTramite.ToString());
            SqlCmdCitas.Append(",'" + oEmisionVG.SexoCitaMedica + "'");
            SqlCmdCitas.Append("," + oEmisionVG.Edad);
            SqlCmdCitas.Append(",'" + oEmisionVG.SumaAsegurada + "'");
            SqlCmdCitas.Append(",'" + oEmisionVG.SumaPolizas + "'");
            SqlCmdCitas.Append(",'" + oEmisionVG.PrimaTotal + "'");
            SqlCmdCitas.Append(",'" + oEmisionVG.IdMoneda + "'");
            SqlCmdCitas.Append(",'" + oEmisionVG.TempoLife + "'");
            SqlCmdCitas.Append("," + oEmisionVG.Combo);
            SqlCmdCitas.Append(",'" + oEmisionVG.Cel + "'");
            SqlCmdCitas.Append(",'" + oEmisionVG.Estado + "'");
            SqlCmdCitas.Append(",'" + oEmisionVG.CelAgentePromotor + "'");
            SqlCmdCitas.Append(",'" + oEmisionVG.Ciudad + "'");
            SqlCmdCitas.Append(",'" + oEmisionVG.Correo + "'");
            SqlCmdCitas.Append(",'" + oEmisionVG.LaboratorioHospital + "'");
            SqlCmdCitas.Append(",'" + oEmisionVG.Direccion + "'");
            SqlCmdCitas.Append(",'" + oEmisionVG.Fecha1 + "'");
            SqlCmdCitas.Append(",'" + oEmisionVG.Fecha2 + "'");
            SqlCmdCitas.Append(",'" + oEmisionVG.Fecha3 + "'");
            SqlCmdCitas.Append(",'" + oEmisionVG.Fecha4 + "'");
            SqlCmdCitas.Append(",'" + oEmisionVG.FechaSeleccionada + "'");
            SqlCmdCitas.Append(",NULL");
            SqlCmdCitas.Append(",1");
            SqlCmdCitas.Append(")");
            resultado = BD.ejecutaCmd(SqlCmdCitas.ToString());
            BD.cierraBD();
            return resultado;

        }

        public EmisionVG carga(int pId)
        {
            EmisionVG resultado = null;
            String SqlCmd = "SELECT * FROM TRAM00003P WHERE IdTramite = " + pId.ToString();
            bd BD = new bd();
            DataTable datos = BD.leeDatos(SqlCmd);
            if (datos.Rows.Count > 0) { resultado = armaTRAM3P(datos.Rows[0]); }
            datos.Dispose();
            BD.cierraBD();
            return resultado;
        }

        public EmisionVG cargaCompleto(int pId)
        {
            EmisionVG resultado = null;
            String SqlCmd = "SELECT * FROM [tramite], [TRAM00003P] WHERE [tramite].[Id] = [TRAM00003P].[IdTramite] AND [TRAM00003P].[IdTramite] = " + pId.ToString();
            bd BD = new bd();
            DataTable datos = BD.leeDatos(SqlCmd);
            if (datos.Rows.Count > 0) { resultado = arma(datos.Rows[0]); }
            datos.Dispose();
            BD.cierraBD();
            return resultado;
        }

        private int CalcularEdad(String Fecha)
        {
            int edad = 0;
            string fecha = Fecha;
            DateTime nacimiento = DateTime.Parse(fecha);
            edad = DateTime.Today.AddTicks(-nacimiento.Ticks).Year - 1;
            return edad;
        }

        public int getEdad(int pIdTramite)
        {
            int intEdad = 0;

            String SqlCmd = "SELECT FechaNacimiento, TitularFechaNacimiento FROM TRAM00003P WHERE IdTramite = " + pIdTramite.ToString();
            bd BD = new bd();
            DataTable datos = BD.leeDatos(SqlCmd);
            if (datos.Rows.Count > 0)
            {
                string strTitutlar = datos.Rows[0]["TitularFechaNacimiento"].ToString();
                string strContratante = datos.Rows[0]["FechaNacimiento"].ToString();
                if (strTitutlar.Length > 0)
                {
                    intEdad = CalcularEdad(strTitutlar);
                }
                else
                {
                    intEdad = CalcularEdad(strContratante);
                }
            }

            datos.Dispose();
            BD.cierraBD();
            return intEdad;
        }

        public EmisionVG cargaCitaMedica(int pId)
        {
            EmisionVG resultado = null;
            String SqlCmd = "SELECT * FROM [CitasMedicas] WHERE [IdTramite] = " + pId.ToString() +" AND [Activo] = 1";
            bd BD = new bd();
            DataTable datos = BD.leeDatos(SqlCmd);
            if (datos.Rows.Count > 0) { resultado = armaCitaMedica(datos.Rows[0]); }
            datos.Dispose();
            BD.cierraBD();
            return resultado;
        }

        private EmisionVG armaCitaMedica(DataRow pRegistro)
        {
            EmisionVG resultado = new EmisionVG();
            if (!pRegistro.IsNull("Edad")) resultado.Edad = Convert.ToString(pRegistro["Edad"]);
            if (!pRegistro.IsNull("Combo")) resultado.Combo = Convert.ToString(pRegistro["Combo"]);
            if (!pRegistro.IsNull("TempoLife")) resultado.TempoLife = Convert.ToBoolean(pRegistro["TempoLife"]);
            if (!pRegistro.IsNull("Cel")) resultado.Cel = Convert.ToString(pRegistro["Cel"]);
            if (!pRegistro.IsNull("CelAgentePromotor")) resultado.CelAgentePromotor = Convert.ToString(pRegistro["CelAgentePromotor"]);
            if (!pRegistro.IsNull("Correo")) resultado.Correo = Convert.ToString(pRegistro["Correo"]);
            if (!pRegistro.IsNull("Estado")) resultado.Estado = Convert.ToString(pRegistro["Estado"]);
            if (!pRegistro.IsNull("ciudad")) resultado.Ciudad = Convert.ToString(pRegistro["ciudad"]);
            if (!pRegistro.IsNull("LaboratorioHospital")) resultado.LaboratorioHospital = Convert.ToString(pRegistro["LaboratorioHospital"]);
            if (!pRegistro.IsNull("direccion")) resultado.Direccion = Convert.ToString(pRegistro["direccion"]);
            if (!pRegistro.IsNull("Fecha1")) resultado.Fecha1 = Convert.ToString(pRegistro["Fecha1"]);
            if (!pRegistro.IsNull("Fecha2")) resultado.Fecha2 = Convert.ToString(pRegistro["Fecha2"]);
            if (!pRegistro.IsNull("Fecha3")) resultado.Fecha3 = Convert.ToString(pRegistro["Fecha3"]);
            if (!pRegistro.IsNull("Fecha4")) resultado.Fecha4 = Convert.ToString(pRegistro["Fecha4"]);
            if (!pRegistro.IsNull("FechaSeleccionada")) resultado.FechaSeleccionada = Convert.ToInt32(pRegistro["FechaSeleccionada"]);
            if (!pRegistro.IsNull("FechaRecepcion")) resultado.FechaRecepcion = Convert.ToString(pRegistro["FechaRecepcion"]);
            if (!pRegistro.IsNull("Activo")) resultado.Activo = Convert.ToInt32(pRegistro["Activo"]);
            return resultado;
        }

        public EmisionVG cargaCitaMedicaLaboratorio(int pId)
        {
            EmisionVG resultado = null;
            String SqlCmd = "SELECT [CitasMedicas].[IdCitaMedica],[CitasMedicas].[IdTramite],[CitasMedicas].[Sexo],[CitasMedicas].[Edad],[CitasMedicas].[Combo],[CitasMedicas].[Cel],[CitasMedicas].[CelAgentePromotor],[CitasMedicas].[Correo],[cat_estados].[estado],[cat_ciudad].[ciudad],[cat_proveedor].[sucursal],[cat_proveedor].[direccion],[CitasMedicas].[Fecha1],[CitasMedicas].[Fecha2],[CitasMedicas].[Fecha3],[CitasMedicas].[Fecha4],[CitasMedicas].[FechaSeleccionada] FROM [CitasMedicas],[cat_estados],[cat_ciudad],[cat_proveedor] WHERE [cat_estados].[Id_estado] = [CitasMedicas].[Estado] AND [cat_ciudad].[Id_ciudad] = [CitasMedicas].[Ciudad] AND [cat_proveedor].[Id_proveedor] = [CitasMedicas].[LaboratorioHospital] AND [IdTramite] =  " + pId.ToString() + " AND [Activo] = 1";
            bd BD = new bd();
            DataTable datos = BD.leeDatos(SqlCmd);
            if (datos.Rows.Count > 0) { resultado = armaCitaMedicaLaboratorio(datos.Rows[0]); }
            datos.Dispose();
            BD.cierraBD();
            return resultado;
        }

        private EmisionVG armaCitaMedicaLaboratorio(DataRow pRegistro)
        {
            EmisionVG resultado = new EmisionVG();
            if (!pRegistro.IsNull("Sexo")) resultado.Sexo = Convert.ToString(pRegistro["Sexo"]);
            if (!pRegistro.IsNull("Edad")) resultado.Edad = Convert.ToString(pRegistro["Edad"]);
            if (!pRegistro.IsNull("Combo")) resultado.Combo = Convert.ToString(pRegistro["Combo"]);
            if (!pRegistro.IsNull("Cel")) resultado.Cel = Convert.ToString(pRegistro["Cel"]);
            if (!pRegistro.IsNull("CelAgentePromotor")) resultado.CelAgentePromotor = Convert.ToString(pRegistro["CelAgentePromotor"]);
            if (!pRegistro.IsNull("Correo")) resultado.Correo = Convert.ToString(pRegistro["Correo"]);
            if (!pRegistro.IsNull("Estado")) resultado.Estado = Convert.ToString(pRegistro["Estado"]);
            if (!pRegistro.IsNull("ciudad")) resultado.Ciudad = Convert.ToString(pRegistro["ciudad"]);
            if (!pRegistro.IsNull("sucursal")) resultado.LaboratorioHospital = Convert.ToString(pRegistro["sucursal"]);
            if (!pRegistro.IsNull("direccion")) resultado.Direccion = Convert.ToString(pRegistro["direccion"]);
            if (!pRegistro.IsNull("Fecha1")) resultado.Fecha1 = Convert.ToString(pRegistro["Fecha1"]);
            if (!pRegistro.IsNull("Fecha2")) resultado.Fecha2 = Convert.ToString(pRegistro["Fecha2"]);
            if (!pRegistro.IsNull("Fecha3")) resultado.Fecha3 = Convert.ToString(pRegistro["Fecha3"]);
            if (!pRegistro.IsNull("Fecha4")) resultado.Fecha4 = Convert.ToString(pRegistro["Fecha4"]);
            if (!pRegistro.IsNull("FechaSeleccionada")) resultado.FechaSeleccionada = Convert.ToInt32(pRegistro["FechaSeleccionada"]);
            return resultado;
        }

        public EmisionVG cargaFolio(int pId)
        {
            EmisionVG resultado = null;
            String SqlCmd = "SELECT * FROM dbo.Folio WHERE dbo.Folio.IdTramite = " + pId.ToString();
            bd BD = new bd();
            DataTable datos = BD.leeDatos(SqlCmd);
            if (datos.Rows.Count > 0) { resultado = armaFolio(datos.Rows[0]); }
            datos.Dispose();
            BD.cierraBD();
            return resultado;
        }

        private EmisionVG armaFolio(DataRow pRegistro)
        {
            EmisionVG resultado = new EmisionVG();
            if (!pRegistro.IsNull("FolioCompuesto")) resultado.Folio = Convert.ToString(pRegistro["FolioCompuesto"]);
            return resultado;
        }

        public bool InactivoCitaMedica(int idTramite)
        {
            bool resultado = false;
            bd BD = new bd();
            resultado = BD.ejecutaCmd("UPDATE [CitasMedicas]  SET Activo = 0 WHERE [IdCitaMedica]= (SELECT TOP 1 IdCitaMedica FROM [CitasMedicas] WHERE Activo = 1 AND [IdTramite] = '" + idTramite + "' ORDER BY IdCitaMedica DESC) AND IdTramite = " + idTramite);
            BD.cierraBD();
            return resultado;
        }

        public bool AlteraCitaMedicaLaboratorio(EmisionVG oEmisionVG)
        {
            bool resultado = false;
            bd BD = new bd();
            resultado = BD.ejecutaCmd("UPDATE [CitasMedicas] SET Fecha4 = '" + oEmisionVG.Fecha4 + "', FechaSeleccionada = '" + oEmisionVG.FechaSeleccionada + "' WHERE [IdCitaMedica]= (SELECT TOP 1 IdCitaMedica FROM [CitasMedicas] WHERE Activo = 1 AND [IdTramite] = '" + oEmisionVG.IdTramite + "' ORDER BY IdCitaMedica DESC) AND IdTramite = '" + oEmisionVG.IdTramite + "'");
            BD.cierraBD();
            return resultado;
        }

        public bool ActualiaProductoSubproducto(EmisionVG oEmisionVG)
        {
            bool resultado = false;
            StringBuilder SqlCmd = new StringBuilder("UPDATE Producto SET IdCatProducto = '" + oEmisionVG.Producto + "', IdCatSubProducto = '" + oEmisionVG.Plan1 + "' WHERE IdTramite = '" + oEmisionVG.IdTramite + "'");
            bd BD = new bd();
            resultado = BD.ejecutaCmd(SqlCmd.ToString());
            BD.cierraBD();
            return resultado;
        }

        public DataTable validaCombo(double Total, int edad, string DescripCombo)
        {
            bd BD = new bd();
            StringBuilder SqlCmd = new StringBuilder("SELECT TOP 1 * FROM [dbo].[cat_asegurabilidad] WHERE [edad_min] <= " + edad + " AND [edad_max] >=" + edad + " AND [monto_min] <= " + Total + " AND [Descripcion] = '" + DescripCombo + "' ORDER BY id DESC");
            DataTable datos = BD.leeDatos(SqlCmd.ToString());
            BD.cierraBD();
            return datos;
        }

        /// <summary>
        /// Listados de los tipos de Ramos para la creación de trámites.
        /// </summary>
        /// <returns></returns>
        public DataTable cargaRamos(string Nivel, string Alcance)
        {
            bd BD = new bd();
            StringBuilder SqlCmd = new StringBuilder("SELECT -1 AS IdRamo, UPPER('[Seleccionar]') AS Nombre UNION ALL SELECT Id AS IdRamo, Nombre FROM cat_ramosTramite WHERE activo = 1 AND Nivel = '" + Nivel + "' AND Alcance = '" + Alcance + "'; ");
            DataTable datos = BD.leeDatos(SqlCmd.ToString());
            BD.cierraBD();
            return datos;
        }

        public bool AlteraTRAM00003P(EmisionVG oEmisionVG)
        {
            bool resultado = false;
            StringBuilder SqlCmd = new StringBuilder(
                "UPDATE [dbo].[TRAM00003P] SET CPDES ='" + oEmisionVG.CPDES + 
                "', FolioCPDES ='" + oEmisionVG.FolioCPDES + 
                "', EstatusCPDES ='" + oEmisionVG.EstatusCPDES + 
                "', TipoPersona ='" + Convert.ToInt32(oEmisionVG.TipoPersona) + 
                "', Nombre ='" + oEmisionVG.Nombre + 
                "', ApPaterno ='" + oEmisionVG.ApPaterno + 
                "', ApMaterno ='" + oEmisionVG.ApMaterno + 
                "', Sexo ='" + oEmisionVG.Sexo + 
                "', FechaNacimiento='" + oEmisionVG.FechaNacimiento + 
                "', RFC='" + oEmisionVG.RFC + 
                "', FechaConst ='" + oEmisionVG.FechaConst + 
                "', Nacionalidad='"+ oEmisionVG.Nacionalidad + 
                "', TitularNombre='" + oEmisionVG.TitularNombre + 
                "', TitularApPat ='"+ oEmisionVG.TitularApPat + 
                "', TitularApMat='"+ oEmisionVG.TitularApMat + 
                "', TitularNacionalidad='"+ oEmisionVG.TitularNacionalidad + 
                "', TitularSexo='" + oEmisionVG.TitularSexo + 
                "', TitularFechaNacimiento='" + oEmisionVG.TitularFechaNacimiento + 
                "', SumaAsegurada='" + oEmisionVG.SumaAsegurada + 
                "', SumaPolizas='"+ oEmisionVG.SumaPolizas + 
                "', PrimaTotal='" + oEmisionVG.PrimaTotal + 
                "', IdMoneda ='" + oEmisionVG.IdMoneda + 
                "', HombreClave='"+ oEmisionVG.HombreClave +
                "', OneShot='" + oEmisionVG.OneShot +
                "', MetaLifeEspecial='" + oEmisionVG.MetaLifeEspecial +
                "', Entidad = '" + oEmisionVG.EntidadFederativa + 
                "', TitularEntidad='" + oEmisionVG.TitularEntidad + 
                "', Excel='"+ oEmisionVG.Excel + 
                "'  WHERE IdTramite = '" + oEmisionVG.IdTramite + "';");
            bd BD = new bd(); 
            resultado = BD.ejecutaCmd(SqlCmd.ToString());
            BD.cierraBD();
            return resultado;
        }
        public bool AlteraCitaMedica(EmisionVG oEmisionVG)
        {
            bool resultado = false;
            StringBuilder SqlCmd = new StringBuilder("UPDATE [dbo].[CitasMedicas] SET TempoLife ='"+ oEmisionVG.TempoLife + "' , Sexo='" + oEmisionVG.Sexo + "', Edad='" + oEmisionVG.Edad + "', SumaAsegurada='" + oEmisionVG.SumaAsegurada + "', SumaPolizas='" + oEmisionVG.SumaPolizas + "', PrimaTotal='" + oEmisionVG.PrimaTotal + "', IdMoneda='" + oEmisionVG.IdMoneda + "', Combo='" + oEmisionVG.Combo + "', Cel='" + oEmisionVG.Cel + "', Estado='" + oEmisionVG.Estado + "', CelAgentePromotor='" + oEmisionVG.CelAgentePromotor + "', Ciudad='" + oEmisionVG.Ciudad + "', Correo='" + oEmisionVG.Correo + "', LaboratorioHospital='" + oEmisionVG.LaboratorioHospital + "', Direccion='" + oEmisionVG.Direccion + "', Fecha1='" + oEmisionVG.Fecha1 + "', Fecha2='"+ oEmisionVG.Fecha2 + "', Fecha3='"+ oEmisionVG.Fecha3 + "', Fecha4='"+ oEmisionVG.Fecha4 + "', FechaSeleccionada='" + oEmisionVG.FechaSeleccionada + "', FechaRecepcion='" + oEmisionVG.FechaRecepcion + "' , Activo ='" + oEmisionVG.Activo + "' WHERE [IdCitaMedica] = (SELECT TOP 1 IdCitaMedica FROM [CitasMedicas] WHERE  [IdTramite] = '" + oEmisionVG.IdTramite + "' ORDER BY IdCitaMedica DESC) AND IdTramite ='" + oEmisionVG.IdTramite + "';");
            bd BD = new bd();
            resultado = BD.ejecutaCmd(SqlCmd.ToString());
            BD.cierraBD();
            return resultado;
        }

        public bool AlteraCitaMedicaDesactiva(EmisionVG oEmisionVG)
        {
            bool resultado = false;
            StringBuilder SqlCmd = new StringBuilder("UPDATE [dbo].[CitasMedicas] SET Activo = 0 WHERE IdTramite ='" + oEmisionVG.IdTramite + "';");
            bd BD = new bd();
            resultado = BD.ejecutaCmd(SqlCmd.ToString());
            BD.cierraBD();
            return resultado;
        }
        /// <summary>
        /// Listado de todos las Monedas
        /// </summary>
        /// <returns>[DataTable] Listado de Paises</returns>
        public DataTable cargaMonedas()
        {
            bd BD = new bd();
            StringBuilder SqlCmd = new StringBuilder("SELECT -1 AS IdMoneda, UPPER('[Seleccionar]') AS Nombre UNION ALL SELECT IdMoneda, UPPER(Nombre) FROM cat_monedas WHERE activo = 1;");
            DataTable datos = BD.leeDatos(SqlCmd.ToString());
            BD.cierraBD();
            return datos;
        }

        public DataTable ValorMoneda(int IdMoneda)
        {
            bd BD = new bd();
            StringBuilder SqlCmd = new StringBuilder("SELECT * FROM cat_monedas WHERE IdMoneda =" + IdMoneda.ToString());
            DataTable datos = BD.leeDatos(SqlCmd.ToString());
            BD.cierraBD();
            return datos;
        }

        /// <summary>
        /// Listado de todos los Paises
        /// </summary>
        /// <returns>[DataTable] Listado de Paises</returns>
        public DataTable cargaPaises()
        {
            bd BD = new bd();
            StringBuilder SqlCmd = new StringBuilder("SELECT Id, RTRIM(UPPER(PaisNombre)) AS Nombre FROM Pais ORDER BY Nombre;");
            DataTable datos = BD.leeDatos(SqlCmd.ToString());
            BD.cierraBD();
            return datos;
        }

        public String validaPais(string nombre)
        {
            String respuesta = "";
            bd BD = new bd();
            DataTable datos = BD.leeDatos("SELECT * FROM Pais WHERE PaisNombre = '" + nombre + "' AND Sancionado = 1;");
            if (datos.Rows.Count > 0) { respuesta = "Este país se encuentra en la lista de países sancionados"; }
            datos.Dispose();
            BD.cierraBD();
            return respuesta;
        }

        public DataTable cargaCatEstados()
        {
            bd BD = new bd();
            StringBuilder SqlCmd = new StringBuilder("SELECT -1 AS Id_estado, UPPER('[Seleccionar]') AS estado UNION ALL SELECT Id_estado,UPPER(estado) FROM [dbo].[cat_estados] ORDER BY [estado] ASC");
            DataTable datos = BD.leeDatos(SqlCmd.ToString());
            BD.cierraBD();
            return datos;
        }

        public DataTable cargaCombosCitasMedicas()
        {
            bd BD = new bd();
            StringBuilder SqlCmd = new StringBuilder("SELECT 'SELECCIONAR' AS combo UNION ALL SELECT DISTINCT CONVERT(VARCHAR(1),combo) AS combo FROM cat_combos");
            DataTable datos = BD.leeDatos(SqlCmd.ToString());
            BD.cierraBD();
            return datos;
        }

        public DataTable cargaCatCiudad(String id_estado)
        {
            bd BD = new bd();
            StringBuilder SqlCmd = new StringBuilder("SELECT -1 AS Id_ciudad, -1 AS Id_estado,UPPER('[Seleccionar]') AS ciudad, '' AS descripcion UNION ALL SELECT Id_ciudad,Id_estado,UPPER(ciudad),descripcion FROM [cat_ciudad] WHERE[Id_estado] = " + id_estado + " ORDER BY [ciudad] ASC");
            DataTable datos = BD.leeDatos(SqlCmd.ToString());
            BD.cierraBD();
            return datos;
        }
        public DataTable cargaCatProveedor(String id_ciudad)
        {
            bd BD = new bd();
            StringBuilder SqlCmd = new StringBuilder("SELECT -1 AS Id_proveedor, -1 AS Id_ciudad, UPPER('[Seleccionar]') AS proveedor, '' AS direccion, '' AS zona, '' AS rating UNION ALL SELECT Id_proveedor, Id_ciudad, UPPER('[' + proveedor + '] ' + sucursal) AS proveedor, UPPER(direccion), zona, rating FROM [dbo].[cat_proveedor] WHERE [Id_ciudad] = " + id_ciudad.ToString() + " ORDER BY rating ASC");
            DataTable datos = BD.leeDatos(SqlCmd.ToString());
            BD.cierraBD();
            return datos;
        }

        public bool CitaMedicaGenerada(int pIdTramite)
        {
            bool blnResultado = false;
            string strSQL = "SELECT CMGenerada FROM CitasMedicas WHERE IdTramite = " + pIdTramite.ToString() + " AND CMGenerada = 1;";
            bd BD = new bd();
            DataTable datos = BD.leeDatos(strSQL);
            if (datos.Rows.Count > 0)
            {
                blnResultado = true;
            }
            datos.Dispose();
            BD.cierraBD();
            return blnResultado;
        }

        public bool EsperaResultadoLab(int pIdTramite)
        {
            bool blnResultado = false;
            string strSQL = "SELECT * FROM tramiteMesa WHERE tramiteMesa.IdTramite = " + pIdTramite.ToString() + " AND tramiteMesa.Estado IN (SELECT statusMesa.Id FROM statusMesa WHERE statusMesa.Nombre IN ('Cita Programada','Cita Reprogramada','En espera de resultados', 'Confirmación Pendiente')) AND tramiteMesa.IdMesa = (SELECT mesa.Id FROM mesa WHERE mesa.Nombre = 'CITAS MÉDICAS' AND mesa.IdFlujo = (SELECT tipoTramite.IdFlujo FROM tipoTramite WHERE tipoTramite.Id = (SELECT tramite.IdTipoTramite FROM tramite WHERE tramite.Id = " + pIdTramite.ToString() + ")))";
            bd BD = new bd();
            DataTable datos = BD.leeDatos(strSQL);
            if (datos.Rows.Count > 0)
            {
                blnResultado = true;
            }
            datos.Dispose();
            BD.cierraBD();
            return blnResultado;
        }

        public int getIdMesaCitaMedica(int idTramite)
        {
            int intResultado = -1;

            string strSQL = "SELECT Id FROM mesa WHERE IdFlujo = (SELECT IdFlujo FROM tipoTramite WHERE Id = (SELECT IdTipoTramite FROM tramite WHERE Id = " + idTramite.ToString() + ")) AND Nombre = 'CITAS MÉDICAS';";
            bd BD = new bd();
            DataTable datos = BD.leeDatos(strSQL);
            if (datos.Rows.Count > 0)
            {
                intResultado  = Convert.ToInt32(datos.Rows[0][0].ToString());
            }
            datos.Dispose();
            BD.cierraBD();


            return intResultado;
        }

        public DataTable CitaMedica(int IdTramite)
        {
            bd BD = new bd();
            StringBuilder SqlCmd = new StringBuilder("SELECT * FROM  vw_CitaMedica WHERE [IdTramite] = "+ IdTramite  + " AND Activo = 1");
            DataTable datos = BD.leeDatos(SqlCmd.ToString());
            BD.cierraBD();
            return datos;
        }

        public DataTable Combo(string combo)
        {
            bd BD = new bd();
            StringBuilder SqlCmd = new StringBuilder("SELECT * FROM cat_combos WHERE combo = " + combo + "");
            DataTable datos = BD.leeDatos(SqlCmd.ToString());
            BD.cierraBD();
            return datos;
        }
        public DataTable consideraciones(String combo)
        {
            bd BD = new bd();
            StringBuilder SqlCmd = new StringBuilder("SELECT * FROM consideracion_combo WHERE combo  = " + combo + "");
            DataTable datos = BD.leeDatos(SqlCmd.ToString());
            BD.cierraBD();
            return datos;
        }
        public DataTable cargaDireccionProveedor(String Id_proveedor)
        {
            bd BD = new bd();
            StringBuilder SqlCmd = new StringBuilder("SELECT * FROM [dbo].[cat_proveedor] WHERE [Id_proveedor] = " + Id_proveedor + " ORDER BY [proveedor] ASC");
            DataTable datos = BD.leeDatos(SqlCmd.ToString());
            BD.cierraBD();
            return datos;
        }

        public DataTable cartgaCatProducto(string TipoTramite)
        {
            bd BD = new bd();
            StringBuilder SqlCmd = new StringBuilder("SELECT -1 AS IdCatProducto, " + TipoTramite.ToString() + " AS Id_TipoTramite, UPPER('[Seleccione]') AS Nombre, '' AS Descripcion UNION ALL SELECT IdCatProducto,Id_TipoTramite,UPPER(Nombre),Descripcion FROM dbo.cat_producto WHERE dbo.cat_producto.Id_TipoTramite = '" + TipoTramite + "'");
            DataTable datos = BD.leeDatos(SqlCmd.ToString());
            BD.cierraBD();
            return datos;
        }

        public DataTable cartgaCatSupProducto(string IdCatProducto)
        {
            bd BD = new bd();
            StringBuilder SqlCmd = new StringBuilder("SELECT -1 AS IdCatSubProducto, -1 AS IdCatProducto, UPPER('[Seleccionar]') AS Nombre, '' AS Descripcion UNION ALL SELECT IdCatSubProducto,IdCatProducto,UPPER(Nombre),Descripcion FROM dbo.cat_subproducto WHERE dbo.cat_subproducto.IdCatProducto = " + IdCatProducto);
            DataTable datos = BD.leeDatos(SqlCmd.ToString());
            BD.cierraBD();
            return datos;
        }

        public int countReingresos(int pIdTramite, int pIdMesa)
        {
            int intResultado = -1;
            // String SqlCmd = "select COUNT(*) from bitacora where IdTramite = " + pIdTramite.ToString() + " and IdMesa = " + pIdMesa.ToString() + ";" ;
               string SqlCmd = "SELECT COUNT(*) FROM bitacora WHERE IdTramite = " + pIdTramite.ToString() + " AND (IdMesa = " + pIdMesa.ToString() + " OR IdMesa = -1 OR IdMesa = -2  ) AND NOT Estado = (SELECT statusMesa.Id FROM statusMesa WHERE statusMesa.Nombre = 'Pausa')";
            bd BD = new bd();
            DataTable datos = BD.leeDatos(SqlCmd);
            if (datos.Rows.Count > 0)
            {
                intResultado = Convert.ToInt32(datos.Rows[0][0]);
            }
            datos.Dispose();
            BD.cierraBD();
            return intResultado;
        }

        //public string NacionalidadSancionada(string IdNacionalidad)
        public string NacionalidadSancionada(string Nacionalidad)
        {
            string strNacionalidad = "NA";
            String SqlCmd = "SELECT id, PaisNombre, Sancionado FROM Pais WHERE PaisNombre = '" + Nacionalidad + "';";
            bd BD = new bd();
            DataTable datos = BD.leeDatos(SqlCmd);
            if (datos.Rows.Count > 0)
            {
                if (!datos.Rows[0].IsNull("id"))
                {
                    if (Convert.ToBoolean(datos.Rows[0]["Sancionado"]) )
                    {
                        strNacionalidad = datos.Rows[0]["PaisNombre"].ToString();
                    }
                    else
                    {
                        strNacionalidad = "NA";
                    }
                }
                else
                {
                    // Pais no encontrado
                    strNacionalidad = "NA";
                }
            }
            datos.Dispose();
            BD.cierraBD();

            return strNacionalidad;
        }

        public DataTable cargaProdructos(int pId)
        {
            bd BD = new bd();
            StringBuilder SqlCmd = new StringBuilder("SELECT  [cat_producto].[Nombre] AS PRODUCTO, [cat_subproducto].[Nombre]  AS SUBPRODUCTO, [cat_producto].[IdCatProducto], [cat_subproducto].[IdCatSubProducto] FROM [producto], [cat_producto], [cat_subproducto] WHERE [producto].[IdCatProducto] = [cat_producto].[IdCatProducto] AND [producto].[IdCatSubProducto] = [cat_subproducto].[IdCatSubProducto] AND [producto].[IdTramite] = " + pId.ToString());
            DataTable datos = BD.leeDatos(SqlCmd.ToString());
            BD.cierraBD();
            return datos;
        }

        private EmisionVG armaProductos(DataRow pRegistro)
        {
            EmisionVG resultado = new EmisionVG();
            //if (!pRegistro.IsNull("idProducto")) resultado.Producto = Convert.ToString(pRegistro["idProducto"]);
            //if (!pRegistro.IsNull("IdTramite")) resultado.Producto = Convert.ToString(pRegistro["IdTramite"]);
            if (!pRegistro.IsNull("Producto")) resultado.Producto = Convert.ToString(pRegistro["Producto"]);
            if (!pRegistro.IsNull("Subproducto")) resultado.Subproducto = Convert.ToString(pRegistro["Subproducto"]);
            return resultado;
        }

        public DataTable Checks(string IdTipoPersona, string IdTipoTramite)
        {
            bd BD = new bd();
            StringBuilder SqlCmd = new StringBuilder("SELECT * FROM cat_DocRecEmicion WHERE (TipoPersona = " + IdTipoPersona.ToString() + " OR TipoPersona is null) AND IdTipoTramite = " + IdTipoTramite.ToString());
            DataTable datos = BD.leeDatos(SqlCmd.ToString());
            BD.cierraBD();
            return datos;
        }

        public DataTable Checks2(string IdTipoPersona, wfiplib.E_TipoTramite IdTipoTramite)
        {
            string strTipoTramite = "";   //TODO: Esta programación es incorrecta
            switch (IdTipoTramite)
            {
                case wfiplib.E_TipoTramite.indPriEmisionVida:
                    strTipoTramite = "11";
                    break;
                case wfiplib.E_TipoTramite.indPriEmisionGMM:
                    strTipoTramite = "12";
                    break;
                case wfiplib.E_TipoTramite.indPriEmisionConversiones:
                    strTipoTramite = "4";
                    break;
            }

            bd BD = new bd();
            StringBuilder SqlCmd = new StringBuilder("SELECT * FROM cat_DocRecEmicion WHERE (TipoPersona = " + IdTipoPersona.ToString() + " OR TipoPersona is null) AND IdTipoTramite = " + strTipoTramite);
            DataTable datos = BD.leeDatos(SqlCmd.ToString());
            BD.cierraBD();
            return datos;
        }

        private EmisionVG armaTRAM3P(DataRow pRegistro)
        {
            EmisionVG resultado = new EmisionVG();
            if (!pRegistro.IsNull("IdTramite")) resultado.IdTramite = Convert.ToInt32(pRegistro["IdTramite"]);
            if (!pRegistro.IsNull("CPDES")) resultado.CPDES = Convert.ToBoolean(pRegistro["CPDES"]);
            if (!pRegistro.IsNull("FolioCPDES")) resultado.FolioCPDES = Convert.ToString(pRegistro["FolioCPDES"]);
            if (!pRegistro.IsNull("EstatusCPDES")) resultado.EstatusCPDES = Convert.ToString(pRegistro["EstatusCPDES"]);
            if (!pRegistro.IsNull("TipoPersona")) resultado.TipoPersona = (E_TipoPersona)(pRegistro["TipoPersona"]);
            if (!pRegistro.IsNull("Nombre")) resultado.Nombre = Convert.ToString(pRegistro["Nombre"]);
            if (!pRegistro.IsNull("ApPaterno")) resultado.ApPaterno = Convert.ToString(pRegistro["ApPaterno"]);
            if (!pRegistro.IsNull("ApMaterno")) resultado.ApMaterno = Convert.ToString(pRegistro["ApMaterno"]);
            if (!pRegistro.IsNull("Sexo")) resultado.Sexo = Convert.ToString(pRegistro["Sexo"]);
            if (!pRegistro.IsNull("FechaNacimiento")) resultado.FechaNacimiento = Convert.ToString(pRegistro["FechaNacimiento"]);
            if (!pRegistro.IsNull("RFC")) resultado.RFC = Convert.ToString(pRegistro["RFC"]);
            if (!pRegistro.IsNull("FechaConst")) resultado.FechaConst = Convert.ToString(pRegistro["FechaConst"]);
            if (!pRegistro.IsNull("TitularNombre")) resultado.TitularNombre = Convert.ToString(pRegistro["TitularNombre"]);
            if (!pRegistro.IsNull("TitularApPat")) resultado.TitularApPat = Convert.ToString(pRegistro["TitularApPat"]);
            if (!pRegistro.IsNull("TitularApMat")) resultado.TitularApMat = Convert.ToString(pRegistro["TitularApMat"]);
            if (!pRegistro.IsNull("TitularSexo")) resultado.TitularSexo = Convert.ToString(pRegistro["TitularSexo"]);
            if (!pRegistro.IsNull("TitularNacionalidad")) resultado.TitularNacionalidad = Convert.ToString(pRegistro["TitularNacionalidad"]);
            if (!pRegistro.IsNull("TitularFechaNacimiento")) resultado.TitularFechaNacimiento = Convert.ToString(pRegistro["TitularFechaNacimiento"]);

            if (!pRegistro.IsNull("Detalle")) resultado.Detalle = Convert.ToString(pRegistro["Detalle"]);
            if (!pRegistro.IsNull("SumaAsegurada")) resultado.SumaAsegurada = Convert.ToString(pRegistro["SumaAsegurada"]);
            if (!pRegistro.IsNull("SumaPolizas")) resultado.SumaPolizas = Convert.ToString(pRegistro["SumaPolizas"]);
            if (!pRegistro.IsNull("PrimaTotal")) resultado.PrimaTotal = Convert.ToString(pRegistro["PrimaTotal"]);
            if (!pRegistro.IsNull("IdMoneda")) resultado.IdMoneda = Convert.ToString(pRegistro["IdMoneda"]);
            if (!pRegistro.IsNull("Nacionalidad")) resultado.Nacionalidad = Convert.ToString(pRegistro["Nacionalidad"]);
            return resultado;
        }

        private EmisionVG arma(DataRow pRegistro)
        {
            EmisionVG resultado = new EmisionVG();
            try
            {
                if (!pRegistro.IsNull("IdTramite")) resultado.IdTramite = Convert.ToInt32(pRegistro["IdTramite"]);
                if (!pRegistro.IsNull("IdTipotramite")) resultado.TipoTramite = pRegistro["IdTipotramite"].ToString();
                if (!pRegistro.IsNull("CPDES")) resultado.CPDES = Convert.ToBoolean(pRegistro["CPDES"]);
                if (!pRegistro.IsNull("FolioCPDES")) resultado.FolioCPDES = Convert.ToString(pRegistro["FolioCPDES"]);
                if (!pRegistro.IsNull("EstatusCPDES")) resultado.EstatusCPDES = Convert.ToString(pRegistro["EstatusCPDES"]);
                if (!pRegistro.IsNull("TipoPersona")) resultado.TipoPersona = (E_TipoPersona)(pRegistro["TipoPersona"]);
                if (!pRegistro.IsNull("Nombre")) resultado.Nombre = Convert.ToString(pRegistro["Nombre"]);
                if (!pRegistro.IsNull("ApPaterno")) resultado.ApPaterno = Convert.ToString(pRegistro["ApPaterno"]);
                if (!pRegistro.IsNull("ApMaterno")) resultado.ApMaterno = Convert.ToString(pRegistro["ApMaterno"]);
                if (!pRegistro.IsNull("Sexo")) resultado.Sexo = Convert.ToString(pRegistro["Sexo"]);
                if (!pRegistro.IsNull("FechaNacimiento")) resultado.FechaNacimiento = Convert.ToString(pRegistro["FechaNacimiento"]);
                if (!pRegistro.IsNull("Entidad")) resultado.EntidadFederativa = Convert.ToString(pRegistro["Entidad"]);

                if (!pRegistro.IsNull("RFC")) resultado.RFC = Convert.ToString(pRegistro["RFC"]);
                if (!pRegistro.IsNull("FechaSolicitud")) resultado.FechaSolicitud = Convert.ToString(pRegistro["FechaSolicitud"]);
                if (!pRegistro.IsNull("FechaConst")) resultado.FechaConst = Convert.ToString(pRegistro["FechaConst"]);
                if (!pRegistro.IsNull("TitularNombre")) resultado.TitularNombre = Convert.ToString(pRegistro["TitularNombre"]);
                if (!pRegistro.IsNull("TitularApPat")) resultado.TitularApPat = Convert.ToString(pRegistro["TitularApPat"]);
                if (!pRegistro.IsNull("TitularApMat")) resultado.TitularApMat = Convert.ToString(pRegistro["TitularApMat"]);
                if (!pRegistro.IsNull("TitularSexo")) resultado.TitularSexo = Convert.ToString(pRegistro["TitularSexo"]);
                if (!pRegistro.IsNull("TitularNacionalidad")) resultado.TitularNacionalidad = Convert.ToString(pRegistro["TitularNacionalidad"]);
                if (!pRegistro.IsNull("TitularFechaNacimiento")) resultado.TitularFechaNacimiento = Convert.ToString(pRegistro["TitularFechaNacimiento"]);
                if (!pRegistro.IsNull("TitularEntidad")) resultado.TitularEntidad = Convert.ToString(pRegistro["TitularEntidad"]);

                if (!pRegistro.IsNull("Detalle")) resultado.Detalle = Convert.ToString(pRegistro["Detalle"]);
                if (!pRegistro.IsNull("SumaAsegurada")) resultado.SumaAsegurada = Convert.ToString(pRegistro["SumaAsegurada"]);
                if (!pRegistro.IsNull("SumaPolizas")) resultado.SumaPolizas = Convert.ToString(pRegistro["SumaPolizas"]);
                if (!pRegistro.IsNull("PrimaTotal")) resultado.PrimaTotal = Convert.ToString(pRegistro["PrimaTotal"]);
                if (!pRegistro.IsNull("IdMoneda")) resultado.IdMoneda = Convert.ToString(pRegistro["IdMoneda"]);
                if (!pRegistro.IsNull("IdPromotoria")) resultado.IdPromotoria = Convert.ToString(pRegistro["IdPromotoria"]);
                if (!pRegistro.IsNull("NumeroOrden")) resultado.NumeroOrden = Convert.ToString(pRegistro["NumeroOrden"]);
                if (!pRegistro.IsNull("FechaRegistro")) resultado.FechaRegistro = Convert.ToString(pRegistro["FechaRegistro"]);
                if (!pRegistro.IsNull("Nacionalidad")) resultado.Nacionalidad = Convert.ToString(pRegistro["Nacionalidad"]);
                if (!pRegistro.IsNull("IdSisLegados")) resultado.IdSisLegados = Convert.ToString(pRegistro["IdSisLegados"]);
                if (!pRegistro.IsNull("kwik")) resultado.kwik = Convert.ToString(pRegistro["kwik"]);
                if (!pRegistro.IsNull("HombreClave")) resultado.HombreClave = Convert.ToBoolean(pRegistro["HombreClave"]);
                if (!pRegistro.IsNull("OneShot")) resultado.OneShot = Convert.ToBoolean(pRegistro["OneShot"]);
                if (!pRegistro.IsNull("MetaLifeEspecial")) resultado.MetaLifeEspecial = Convert.ToBoolean(pRegistro["MetaLifeEspecial"]);
                if (!pRegistro.IsNull("Prioridad")) resultado.Prioridad = (E_PrioridadTramite)(pRegistro["Prioridad"]);
                // if (!pRegistro.IsNull("Prioridad")) resultado.Prioridad = Convert.ToInt32(pRegistro["Prioridad"]);
                if (!pRegistro.IsNull("Estado")) resultado.EstadoEnum = (E_EstadoTramite)pRegistro["Estado"];
                if (!pRegistro.IsNull("Excel")) resultado.Excel = Convert.ToString(pRegistro["Excel"]);
            }
            catch(Exception ex)
            {
                string error = ex.Message.ToString();
            }
            if (!pRegistro.IsNull("Detalle")) resultado.Detalle = Convert.ToString(pRegistro["Detalle"]);
            if (!pRegistro.IsNull("SumaAsegurada")) resultado.SumaAsegurada = Convert.ToString(pRegistro["SumaAsegurada"]);
            if (!pRegistro.IsNull("SumaPolizas")) resultado.SumaPolizas = Convert.ToString(pRegistro["SumaPolizas"]);
            if (!pRegistro.IsNull("PrimaTotal")) resultado.PrimaTotal = Convert.ToString(pRegistro["PrimaTotal"]);
            if (!pRegistro.IsNull("IdMoneda")) resultado.IdMoneda = Convert.ToString(pRegistro["IdMoneda"]);
            if (!pRegistro.IsNull("IdPromotoria")) resultado.IdPromotoria = Convert.ToString(pRegistro["IdPromotoria"]);
            if (!pRegistro.IsNull("NumeroOrden")) resultado.NumeroOrden = Convert.ToString(pRegistro["NumeroOrden"]);
            if (!pRegistro.IsNull("FechaRegistro")) resultado.FechaRegistro = Convert.ToString(pRegistro["FechaRegistro"]);
            if (!pRegistro.IsNull("Nacionalidad")) resultado.Nacionalidad = Convert.ToString(pRegistro["Nacionalidad"]);
            if (!pRegistro.IsNull("IdSisLegados")) resultado.IdSisLegados = Convert.ToString(pRegistro["IdSisLegados"]);
            if (!pRegistro.IsNull("kwik")) resultado.kwik = Convert.ToString(pRegistro["kwik"]);
            if (!pRegistro.IsNull("HombreClave")) resultado.HombreClave = Convert.ToBoolean(pRegistro["HombreClave"]);
            if (!pRegistro.IsNull("OneShot")) resultado.OneShot = Convert.ToBoolean(pRegistro["OneShot"]);
            if (!pRegistro.IsNull("MetaLifeEspecial")) resultado.MetaLifeEspecial = Convert.ToBoolean(pRegistro["MetaLifeEspecial"]);
            if (!pRegistro.IsNull("Prioridad")) resultado.Prioridad = (E_PrioridadTramite)(pRegistro["Prioridad"]);
            // if (!pRegistro.IsNull("Prioridad")) resultado.Prioridad = Convert.ToInt32(pRegistro["Prioridad"]);
            if (!pRegistro.IsNull("Estado")) resultado.EstadoEnum = (E_EstadoTramite)pRegistro["Estado"];
            if (!pRegistro.IsNull("Excel")) resultado.Excel = Convert.ToString(pRegistro["Excel"]);
            return resultado;
        }

        public bool ExistenTramitesparaRFC(string pRFC)
        {
            bool respuesta = false;
            bd BD = new bd();
            DataTable datos = BD.leeDatos("SELECT * FROM TRAM00003P WHERE Rfc='" + pRFC + "'");
            if (datos.Rows.Count > 0) { respuesta = true; }
            datos.Dispose();
            BD.cierraBD();
            return respuesta;
        }

        public bool elimina(int pIdTramite)
        {
            bool resultado = false;
            bd BD = new bd();
            resultado = BD.ejecutaCmd("DELETE From TRAM00003P WHERE IdTramite=" + pIdTramite.ToString());
            BD.cierraBD();
            return resultado;
        }
    }

    [Serializable]
    public class EmisionVGP
    {
        private int mIdTramite = 0;
        public int IdTramite { get { return mIdTramite; } set { mIdTramite = value; } }
        private string mCPDES = "";
        public string CPDES { get { return mCPDES; } set { mCPDES = value; } }
        private string mFolioCPDES = "";
        public string FolioCPDES { get { return mFolioCPDES; } set { mFolioCPDES = value; } }
        private string mEstatusCPDES = "";
        public string EstatusCPDES { get { return mEstatusCPDES; } set { mEstatusCPDES = value; } }
        private E_TipoPersona mTipoPersona = E_TipoPersona.Fisica;
        public E_TipoPersona TipoPersona { get { return mTipoPersona; } set { mTipoPersona = value; } }

        private string mNombre = "";
        public string Nombre { get { return mNombre; } set { mNombre = value; } }
        private string mApPaterno = "";
        public string ApPaterno { get { return mApPaterno; } set { mApPaterno = value; } }
        private string mApMaterno = "";
        public string ApMaterno { get { return mApMaterno; } set { mApMaterno = value; } }
        private string mRFC = "";
        public string RFC { get { return mRFC; } set { mRFC = value; } }

        private string mTitularNombre = "";
        private string TitularNombre { get { return mTitularNombre; } set { mTitularNombre = value; } }
        private string mTitularApPat = "";
        private string TitularApPat { get { return mTitularApPat; } set { mTitularApPat = value; } }
        private string mTitularApMat = "";
        private string TitularApMat { get { return mTitularApMat; } set { mTitularApMat = value; } }

        private string mFolio = "";
        public string Folio { get { return mFolio; } set { mFolio = value; } }

        private string mProducto = "";
        public string Producto { get { return mProducto; } set { mProducto = value; } }
        private string mSubproducto = "";
        public string Subproducto { get { return mSubproducto; } set { mSubproducto = value; } }


        private string mDetalle = "";
        public string Detalle { get { return mDetalle; } set { mDetalle = value; } }

        public string CabeceraHtml
        {
            get
            {
                string cadena = "<table style ='width :100%;border: 1px solid #6387A6;border-radius: 7px 7px 7px 7px;-moz-border-radius: 7px 7px 7px 7px;'>";
                cadena += "<tr><td style='width:150px'>Número trámite:</td><td><b>" + mIdTramite.ToString().PadLeft(5, '0') + "</b></td></tr>";
                cadena += "<tr><td>Nombre(s):</td><td><b>" + mNombre + " " + mApPaterno + " " + mApMaterno + "</b></td></tr>";
                cadena += "<tr><td>Titular:</td><td><b>" + mTitularNombre + " " + mTitularApPat + " " + mTitularApMat + "</b></td></tr>";
                cadena += "</table>";
                return cadena;
            }
        }

        public string DatosHtml
        {
            get
            {
                string cadena = "<p>";
                cadena += "Nombre(s):<b>" + mNombre + " " + mApPaterno + " " + mApMaterno + "</b></br>";
                cadena += "RFC:<b>" + mRFC + "</b></br>";
                cadena += "Titular:<b>" + mTitularNombre + " " + mTitularApPat + " " + mTitularApMat + "</b>";
                cadena += "</p>";
                return cadena;
            }
        }

        public string TextoHtml
        {
            get
            {
                string cadena = "<table style ='width :100%;border: 1px solid #6387A6;border-radius: 7px 7px 7px 7px;-moz-border-radius: 7px 7px 7px 7px;'>";
                cadena += "<tr><td>Nombre(s):</td><td><b>" + mNombre + " " + mApPaterno + " " + mApMaterno + "</b></td></tr>";
                cadena += "<tr><td>RFC:</td><td><b>" + mRFC + "</b></td></tr>";
                cadena += "<tr><td>Titular:</td><td>" + mTitularNombre + " " + mTitularApPat + " " + mTitularApMat + "</td></tr>";
                if (mDetalle != "") cadena += "</br>DETALLES:</br></br><b>" + mDetalle + "</b>";
                cadena += "</td></tr>";
                cadena += "</table>";

                return cadena;
            }
        }
    }
}
