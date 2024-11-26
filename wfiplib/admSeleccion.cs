using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace wfiplib
{
    public class admSeleccion
    {
        public bool nuevo(Seleccion pSeleccion)
        {
            bool resultado = false;
            StringBuilder SqlCmd = new StringBuilder("INSERT INTO [DatosSeleccion] ([IdTramite],[Ramo],[ID],[Vigencia],[Riesgo],[RiesgoFactor],[Zona],[Plan],[Factor],[Activo],[Parentesco],[Endosos],[Estatus],[Ocupacion])");
            SqlCmd.Append(" VALUES(");
            SqlCmd.Append(pSeleccion.IdTramite);
            SqlCmd.Append(",'" + pSeleccion.Ramo + "'");
            SqlCmd.Append(",'" + pSeleccion.ID + "'");
            SqlCmd.Append(",'" + pSeleccion.FechaVigencia + "'");
            SqlCmd.Append(",'" + pSeleccion.Riesgo + "'");
            SqlCmd.Append(",'" + pSeleccion.RiesgoFactor + "'");
            SqlCmd.Append(",'" + pSeleccion.Zona + "'");
            SqlCmd.Append(",'" + pSeleccion.Plan + "'");
            SqlCmd.Append(",'" + pSeleccion.Factor + "'");
            SqlCmd.Append(",1");
            SqlCmd.Append(",'" + pSeleccion.Parentesco + "'");
            SqlCmd.Append(",'" + pSeleccion.Endosos + "'");
            SqlCmd.Append(",'ACEPTADO'");
            SqlCmd.Append(",'" + pSeleccion.Ocupacion + "'");
            SqlCmd.Append(")");
            bd BD = new bd();
            resultado = BD.ejecutaCmd(SqlCmd.ToString());
            BD.cierraBD();
            return resultado;
        }

        public bool NuevoVida(Seleccion pSeleccion)
        {
            bool resultado = false;
            StringBuilder SqlCmd = new StringBuilder("INSERT INTO [DatosVidaSeleccion] ([IdTramite],[Producto],[SubProducto],[NumCaptura],[Vigencia],[ExtraPrima],[Habito],[Parentesco],[Endosos],[Activo],[Estatus],[Ocupacion])");
            SqlCmd.Append(" VALUES(");
            SqlCmd.Append(pSeleccion.IdTramite);
            SqlCmd.Append(",'" + pSeleccion.Producto + "'");
            SqlCmd.Append(",'" + pSeleccion.SubProducto + "'");
            SqlCmd.Append(",'" + pSeleccion.NumCaptura + "'");
            SqlCmd.Append(",'" + pSeleccion.Vigencia + "'");
            SqlCmd.Append(",'" + pSeleccion.ExtraPrima + "'");
            SqlCmd.Append(",'" + pSeleccion.Habito + "'");
            SqlCmd.Append(",'" + pSeleccion.Parentesco + "'");
            SqlCmd.Append(",'" + pSeleccion.Endosos + "'");
            SqlCmd.Append(",1");
            SqlCmd.Append(",'ACEPTADO'");
            SqlCmd.Append(",'" + pSeleccion.Ocupacion + "'");
            SqlCmd.Append(")");
            bd BD = new bd();
            resultado = BD.ejecutaCmd(SqlCmd.ToString());
            BD.cierraBD();
            return resultado;
        }

        public bool Actualizar(Seleccion pSeleccion)
        {
            bool resultado = false;
            StringBuilder SqlCmd = new StringBuilder("UPDATE [DatosSeleccion] SET ");
            SqlCmd.Append("[Ramo] ='" + pSeleccion.Ramo + "'");
            SqlCmd.Append(",[ID] ='" + pSeleccion.ID + "'");
            SqlCmd.Append(",[Vigencia] = '" + pSeleccion.FechaVigencia + "'");
            SqlCmd.Append(",[Riesgo] ='" + pSeleccion.Riesgo + "'");
            SqlCmd.Append(",[RiesgoFactor] = '" + pSeleccion.RiesgoFactor + "'");
            SqlCmd.Append(",[Zona] = '" + pSeleccion.Zona + "'");
            SqlCmd.Append(",[Plan] = '" + pSeleccion.Plan + "'");
            SqlCmd.Append(",[Factor] = '" + pSeleccion.Factor + "'");
            SqlCmd.Append(",Activo = 1 ");
            SqlCmd.Append(",[parentesco] = '" + pSeleccion.Parentesco + "'");
            SqlCmd.Append(" WHERE IdDatosSeleccion= (SELECT TOP 1 IdDatosSeleccion FROM [DatosSeleccion] WHERE Activo = 1 ORDER BY IdDatosSeleccion DESC) AND [IdTramite] ='" + pSeleccion.IdTramite + "'");
            bd BD = new bd();
            resultado = BD.ejecutaCmd(SqlCmd.ToString());
            BD.cierraBD();
            return resultado;
        }

        public bool InactivoSeleccion(string IdDatosSeleccion)
        {
            bool resultado = false;
            StringBuilder SqlCmd = new StringBuilder("UPDATE [DatosSeleccion] SET Activo = 0 WHERE Activo = 1 AND ");
            SqlCmd.Append("IdDatosSeleccion ='" + IdDatosSeleccion + "'");
            bd BD = new bd();
            resultado = BD.ejecutaCmd(SqlCmd.ToString());
            BD.cierraBD();
            return resultado;
        }

        public bool InactivoSeleccionVida(string IdDatosVidaSeleccion)
        {
            bool resultado = false;
            StringBuilder SqlCmd = new StringBuilder("UPDATE [DatosVidaSeleccion] SET Activo = 0 WHERE Activo = 1 AND ");
            SqlCmd.Append("IdDatosVidaSeleccion ='" + IdDatosVidaSeleccion + "'");
            bd BD = new bd();
            resultado = BD.ejecutaCmd(SqlCmd.ToString());
            BD.cierraBD();
            return resultado;

        }

        public bool ModificacionDatoSeleccionVida(string IdDatosVidaSeleccion)
        {
            bool resultado = false;
            StringBuilder SqlCmd = new StringBuilder("EXECUTE ActualizaDatoSeleccionVida " + IdDatosVidaSeleccion + "");
            bd BD = new bd();
            resultado = BD.ejecutaCmd(SqlCmd.ToString());
            BD.cierraBD();
            return resultado;

        }

        public bool ModificacionDatoSeleccionGMM(string IdDatosSeleccion)
        {
            bool resultado = false;
            StringBuilder SqlCmd = new StringBuilder("EXECUTE ActualizaDatoSeleccionGMM " + IdDatosSeleccion + "");
            bd BD = new bd();
            resultado = BD.ejecutaCmd(SqlCmd.ToString());
            BD.cierraBD();
            return resultado;

        }
        


        public bool InactivoTodoSeleccion(int IdTramite)
        {
            bool resultado = false;
            StringBuilder SqlCmd = new StringBuilder("UPDATE [DatosSeleccion] SET Activo = 0 WHERE ");
            SqlCmd.Append("IdTramite ='" + IdTramite + "'");
            bd BD = new bd();
            resultado = BD.ejecutaCmd(SqlCmd.ToString());
            BD.cierraBD();
            return resultado;
        }

        public Seleccion carga(int IdTramite)
        {
            Seleccion resultado = null;
            String SqlCmd = "SELECT * FROM [DatosSeleccion] WHERE IdTramite = " + IdTramite + " AND Activo = 1 ";
            bd BD = new bd();
            DataTable datos = BD.leeDatos(SqlCmd);
            if (datos.Rows.Count > 0) { resultado = arma(datos.Rows[0]); }
            datos.Dispose();
            BD.cierraBD();
            return resultado;
        }

        public DataTable RamosVida()
        {
            bd BD = new bd();
            StringBuilder SqlCmd = new StringBuilder("SELECT -1 AS [IdCatProducto],UPPER('[Seleccione]') AS [combo] UNION ALL SELECT [cat_Seleccion].[IdCatProducto],UPPER([cat_Seleccion].[combo]) FROM [cat_Seleccion],[cat_producto] WHERE [cat_Seleccion].[IdCatProducto] = [cat_producto].[IdCatProducto] AND [cat_producto].[Id_TipoTramite] = 2");
            DataTable datos = BD.leeDatos(SqlCmd.ToString());
            BD.cierraBD();
            return datos;
        }

        public DataTable RamosVidaID(string nombre)
        {
            bd BD = new bd();
            StringBuilder SqlCmd = new StringBuilder("SELECT  [cat_Seleccion].[IdCatProducto],UPPER([cat_Seleccion].[combo]) AS combo FROM [cat_Seleccion],[cat_producto] WHERE [cat_Seleccion].[IdCatProducto] = [cat_producto].[IdCatProducto] AND [cat_producto].[Id_TipoTramite] = 2 AND [cat_Seleccion].[combo] = '" + nombre + "'");
            DataTable datos = BD.leeDatos(SqlCmd.ToString());
            BD.cierraBD();
            return datos;
        }

        public DataTable RamosGmmID(string nombre, string tipoTramite)
        {
            bd BD = new bd();
            StringBuilder SqlCmd;
                
            if (tipoTramite == "4")
                SqlCmd = new StringBuilder("SELECT [Id_seleccion],UPPER([combo]) as combo FROM [cat_Seleccion],[cat_producto] WHERE [cat_Seleccion].[IdCatProducto] = [cat_producto].[IdCatProducto] AND [cat_producto].[Id_TipoTramite] = " + tipoTramite + " AND [cat_Seleccion].[combo] = '" + nombre + "'");
            else
                SqlCmd = new StringBuilder("SELECT [Id_seleccion],UPPER([combo]) as combo FROM [cat_Seleccion],[cat_producto] WHERE [cat_Seleccion].[IdCatProducto] = [cat_producto].[IdCatProducto] AND [cat_producto].[Id_TipoTramite] = 1 AND [cat_Seleccion].[combo] = '" + nombre + "'");
            DataTable datos = BD.leeDatos(SqlCmd.ToString());
            BD.cierraBD();
            return datos;
        }

        public DataTable ExtraPrima()
        {
            bd BD = new bd();
            //StringBuilder SqlCmd = new StringBuilder("SELECT -1 AS [Id_extraprima],UPPER('[Seleccione]') AS [extraprima] UNION ALL SELECT [Id_extraprima],UPPER([extraprima]) FROM [cat_extraprima]");
            StringBuilder SqlCmd = new StringBuilder("SELECT [Id_extraprima],UPPER([extraprima]) AS [extraprima] FROM [cat_extraprima]");
            DataTable datos = BD.leeDatos(SqlCmd.ToString());
            BD.cierraBD();
            return datos;
        }

        public DataTable Habitos()
        {
            bd BD = new bd();
            StringBuilder SqlCmd = new StringBuilder("SELECT -1 AS [Id_habito],UPPER('[Seleccione]') AS [habito] UNION ALL SELECT [Id_habito],UPPER([habito])  FROM [dbo].[cat_habito]");
            DataTable datos = BD.leeDatos(SqlCmd.ToString());
            BD.cierraBD();
            return datos;
        }

        public DataTable Endosos()
        {
            bd BD = new bd();
            StringBuilder SqlCmd = new StringBuilder("SELECT [Id_endoso],[clave],UPPER([clave] + ' - ' + [endoso]) as [Nombre]  FROM [dbo].[cat_endosos] WHERE Activo = 1");
            DataTable datos = BD.leeDatos(SqlCmd.ToString());
            BD.cierraBD();
            return datos;
        }

        public DataTable EndososGM()
        {
            bd BD = new bd();
            StringBuilder SqlCmd = new StringBuilder("SELECT [Id_endosoGM],[clave],UPPER([clave] + ' - ' + [endoso]) as [Nombre] FROM [dbo].[cat_endososGM] WHERE Activo = 1");
            DataTable datos = BD.leeDatos(SqlCmd.ToString());
            BD.cierraBD();
            return datos;
        }

        public DataTable Ramos(string tipoRamo)
        {
            bd BD = new bd();
            StringBuilder SqlCmd;

            if (tipoRamo == "4")
                SqlCmd = new StringBuilder("SELECT -1 AS [Id_seleccion],UPPER('[Seleccione]') AS [combo] UNION ALL SELECT [Id_seleccion],UPPER([combo]) FROM [cat_Seleccion],[cat_producto] WHERE [cat_Seleccion].[IdCatProducto] = [cat_producto].[IdCatProducto] AND [cat_producto].[Id_TipoTramite] in (1,4);");
            else
                SqlCmd = new StringBuilder("SELECT -1 AS [Id_seleccion],UPPER('[Seleccione]') AS [combo] UNION ALL SELECT [Id_seleccion],UPPER([combo]) FROM [cat_Seleccion],[cat_producto] WHERE [cat_Seleccion].[IdCatProducto] = [cat_producto].[IdCatProducto] AND [cat_producto].[Id_TipoTramite] = " + tipoRamo + ";");

            DataTable datos = BD.leeDatos(SqlCmd.ToString());
            BD.cierraBD();
            return datos;
        }

        public DataTable Parentesco()
        {
            bd BD = new bd();
            StringBuilder SqlCmd = new StringBuilder("SELECT -1 AS [Id_parentesco],UPPER('[Seleccione]') AS [parentesco] UNION ALL SELECT [Id_parentesco],UPPER([parentesco]) FROM [cat_parentesco] WHERE parentesco ='TITULAR' or parentesco ='TODOS LOS SOLICITANTES'");
            DataTable datos = BD.leeDatos(SqlCmd.ToString());
            BD.cierraBD();
            return datos;
        }

        public DataTable ParentescoConRegistros()
        {
            bd BD = new bd();
            StringBuilder SqlCmd = new StringBuilder("SELECT -1 AS [Id_parentesco],UPPER('[Seleccione]') AS [parentesco] UNION ALL SELECT [Id_parentesco],UPPER([parentesco]) FROM [cat_parentesco] WHERE parentesco!='TITULAR' AND parentesco!='TODOS LOS SOLICITANTES'");
            DataTable datos = BD.leeDatos(SqlCmd.ToString());
            BD.cierraBD();
            return datos;
        }

        public DataTable CargaRiesgos(string Id_seleccion)
        {
            bd BD = new bd();
            StringBuilder SqlCmd = new StringBuilder("SELECT * FROM [Riesgo] WHERE [Id_seleccion] = '" + Id_seleccion + "' AND catalogo = 'true'");
            DataTable datos = BD.leeDatos(SqlCmd.ToString());
            BD.cierraBD();
            return datos;
        }

        public DataTable CargaRiesgoFactor(string Id_Riesgo)
        {
            bd BD = new bd();
            StringBuilder SqlCmd = new StringBuilder("SELECT -1 AS [Id_CatRiesgo],UPPER('[Seleccione]') AS [riesgo], '' AS [factor] UNION ALL SELECT [Id_CatRiesgo],[riesgo],[factor] FROM [cat_riesgo] WHERE [Id_Riesgo] = '" + Id_Riesgo + "'");
            DataTable datos = BD.leeDatos(SqlCmd.ToString());
            BD.cierraBD();
            return datos;
        }

        public DataTable CargaFactor(string Id_CatRiesgo)
        {
            bd BD = new bd();
            StringBuilder SqlCmd = new StringBuilder("SELECT [factor] FROM [cat_riesgo] WHERE [Id_CatRiesgo] = '" + Id_CatRiesgo + "'");
            DataTable datos = BD.leeDatos(SqlCmd.ToString());
            BD.cierraBD();
            return datos;
        }

        public DataTable CargaZonas(string Id_seleccion)
        {
            bd BD = new bd();
            StringBuilder SqlCmd = new StringBuilder("SELECT -1 AS Id_CatZona,UPPER('[Seleccione]') AS dato UNION ALL SELECT Id_CatZona,dato FROM [Zona], [cat_zona] WHERE [Zona].[Id_zona] = [cat_zona].[Id_zona] AND [Zona].[Id_seleccion] = '" + Id_seleccion + "'");
            DataTable datos = BD.leeDatos(SqlCmd.ToString());
            BD.cierraBD();
            return datos;
        }

        public DataTable BuscaZona(string Id_seleccion , string dato)
        {
            bd BD = new bd();
            StringBuilder SqlCmd = new StringBuilder("SELECT Id_CatZona,dato FROM [Zona], [cat_zona] WHERE [Zona].[Id_zona] = [cat_zona].[Id_zona] AND [Zona].[Id_seleccion] = '" + Id_seleccion + "' AND dato = '" + dato + "'");
            DataTable datos = BD.leeDatos(SqlCmd.ToString());
            BD.cierraBD();
            return datos;
        }

        public DataTable CargaPlanes(string Id_seleccion)
        {
            bd BD = new bd();
            StringBuilder SqlCmd = new StringBuilder("SELECT -1 AS [Id_CatPlan],UPPER('[Seleccione]') AS [dato] UNION ALL SELECT [Id_CatPlan],UPPER([dato]) FROM [Plan], [cat_plan] WHERE [Plan].[Id_plan] = [cat_plan].[Id_plan] AND [Plan].[Id_seleccion] = '" + Id_seleccion + "'");
            DataTable datos = BD.leeDatos(SqlCmd.ToString());
            BD.cierraBD();
            return datos;
        }

        public DataTable BuscaPlan(string Id_seleccion, string dato)
        {
            bd BD = new bd();
            StringBuilder SqlCmd = new StringBuilder("SELECT  [Id_CatPlan],UPPER([dato]) as dato FROM [Plan], [cat_plan] WHERE [Plan].[Id_plan] = [cat_plan].[Id_plan] AND [Plan].[Id_seleccion] = '" + Id_seleccion + "' AND dato = '" + dato + "'");
            DataTable datos = BD.leeDatos(SqlCmd.ToString());
            BD.cierraBD();
            return datos;
        }

        public DataTable Factores(string Id_seleccion)
        {
            bd BD = new bd();
            StringBuilder SqlCmd = new StringBuilder("SELECT * FROM [Factor] WHERE [Id_seleccion] ='" + Id_seleccion + "'");
            DataTable datos = BD.leeDatos(SqlCmd.ToString());
            BD.cierraBD();
            return datos;
        }

        public DataTable CargaFactores(string Id_seleccion)
        {
            bd BD = new bd();
            StringBuilder SqlCmd = new StringBuilder("SELECT -1 AS [Id_CatFactor],UPPER('[Seleccione]') AS [dato] UNION ALL SELECT [Id_CatFactor],[dato] FROM [Factor], [cat_factor] WHERE [Factor].[Id_factor] = [cat_factor].[Id_factor] AND [Factor].[Id_seleccion] ='" + Id_seleccion + "'");
            DataTable datos = BD.leeDatos(SqlCmd.ToString());
            BD.cierraBD();
            return datos;
        }

        public DataTable BuscaFactor(string Id_seleccion , string dato)
        {
            bd BD = new bd();
            StringBuilder SqlCmd = new StringBuilder("SELECT [Id_CatFactor],[dato] FROM [Factor], [cat_factor] WHERE [Factor].[Id_factor] = [cat_factor].[Id_factor] AND [Factor].[Id_seleccion] = '" + Id_seleccion + "' and dato = '" + dato + "'");
            DataTable datos = BD.leeDatos(SqlCmd.ToString());
            BD.cierraBD();
            return datos;
        }

        public DataTable BuscaProductos(int IdTramite)
        {
            bd BD = new bd();
            StringBuilder SqlCmd = new StringBuilder("SELECT TOP 1 IdCatProducto,IdCatSubProducto FROM [Producto] WHERE IdTramite = '" + IdTramite + "'");
            DataTable datos = BD.leeDatos(SqlCmd.ToString());
            BD.cierraBD();
            return datos;

        }

        public DataTable BuscaProductosSeleccion(string IdCatProducto)
        {
            bd BD = new bd();
            StringBuilder SqlCmd = new StringBuilder("SELECT Id_seleccion FROM [cat_Seleccion] WHERE IdCatProducto = '" + IdCatProducto + "'");
            DataTable datos = BD.leeDatos(SqlCmd.ToString());
            BD.cierraBD();
            return datos;

        }
        public DataTable BuscaSubProductosSeleccion(string IdCatSubProducto)
        {
            bd BD = new bd();
            StringBuilder SqlCmd = new StringBuilder("SELECT Nombre FROM [dbo].[cat_subproducto] where IdCatSubProducto = '" + IdCatSubProducto + "'");
            DataTable datos = BD.leeDatos(SqlCmd.ToString());
            BD.cierraBD();
            return datos;

        }

        public DataTable BuscaSubProductosSeleccion2(string Id_seleccion, string nombre)
        {
            bd BD = new bd();
            StringBuilder SqlCmd = new StringBuilder("SELECT * FROM [cat_plan] WHERE Id_plan = (SELECT Id_plan FROM [Plan] WHERE Id_seleccion = '"+ Id_seleccion + "') AND dato = '"+ nombre + "'");
            DataTable datos = BD.leeDatos(SqlCmd.ToString());
            BD.cierraBD();
            return datos;

        }


        public DataTable VerificaMesas(int IdTramite)
        {
            bd BD = new bd();
            StringBuilder SqlCmd = new StringBuilder("SELECT [tramite].ID, [mesa].Nombre FROM [tramite],[tramiteMesa],[mesa] WHERE[tramiteMesa].IdTramite = [tramite].Id AND[tramiteMesa].IdMesa = [mesa].Id AND([mesa].Nombre = 'REVISIÓN TÉCNICA' OR[mesa].Nombre = 'REVISIÓN MÉDICA') AND[tramite].ID = '" + IdTramite + "'");
            DataTable datos = BD.leeDatos(SqlCmd.ToString());
            BD.cierraBD();
            return datos;

        }

        public DataTable CargaRiesgoFactores(int IdTramite)
        {
            bd BD = new bd();
            StringBuilder SqlCmd = new StringBuilder("SELECT * FROM [dbo].[DatosSeleccion] WHERE IdTramite = '"+ IdTramite + "' AND Activo = 1");
            DataTable datos = BD.leeDatos(SqlCmd.ToString());
            BD.cierraBD();
            return datos;
        }

        public DataTable CargaRiesgoFactoresVida(int IdTramite)
        {
            bd BD = new bd();
            StringBuilder SqlCmd = new StringBuilder("SELECT * FROM [dbo].[DatosVidaSeleccion] WHERE IdTramite = '" + IdTramite + "' AND Activo = 1");
            DataTable datos = BD.leeDatos(SqlCmd.ToString());
            BD.cierraBD();
            return datos;
        }

        private Seleccion arma(DataRow pRegistro)
        {
            Seleccion resultado = new Seleccion();
            if (!pRegistro.IsNull("IdTramite")) resultado.IdTramite = Convert.ToInt32(pRegistro["IdTramite"]);
            if (!pRegistro.IsNull("Ramo")) resultado.Ramo = Convert.ToString(pRegistro["Ramo"]);
            if (!pRegistro.IsNull("ID")) resultado.ID = Convert.ToString(pRegistro["ID"]);
            if (!pRegistro.IsNull("Vigencia")) resultado.FechaVigencia= Convert.ToString(pRegistro["Vigencia"]);
            if (!pRegistro.IsNull("Riesgo")) resultado.Riesgo = Convert.ToString(pRegistro["Riesgo"]);
            if (!pRegistro.IsNull("RiesgoFactor")) resultado.RiesgoFactor = Convert.ToString(pRegistro["RiesgoFactor"]);
            if (!pRegistro.IsNull("Zona")) resultado.Zona = Convert.ToString(pRegistro["Zona"]);
            if (!pRegistro.IsNull("Plan")) resultado.Plan = Convert.ToString(pRegistro["Plan"]);
            if (!pRegistro.IsNull("Factor")) resultado.Factor = Convert.ToString(pRegistro["Factor"]);
            if (!pRegistro.IsNull("Activo")) resultado.Activo = Convert.ToString(pRegistro["Activo"]);
            if (!pRegistro.IsNull("Parentesco")) resultado.Parentesco = Convert.ToString(pRegistro["Parentesco"]);
            return resultado;
        }

    }
}
