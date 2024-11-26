using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;

namespace wfiplib.Tablas.Institucional.Privado
{
    /// <summary>
    /// Todos los procesos para InsServicios
    /// </summary>
    public class InsServicios
    {
        InsServiciosTablas st = new InsServiciosTablas();

        public void LlenarInsProducto_DropdownList(ref DropDownList ddl)
        {
            LlenarControles.LlenarDropDownList(ref ddl, st.SelecccionarInsProducto(), "Nombre", "IdProducto");
        }

        public void LlenarInsRamo_DropdownList(ref DropDownList ddl, string insidproducto)
        {
            LlenarControles.LlenarDropDownList(ref ddl, st.SeleccionarInsRamo(insidproducto), "Nombre", "IdRamo");
        }

        public void LlenarInsSubRamo_DropdownList(ref DropDownList ddl, string insidramo, string insidproducto)
        {
            LlenarControles.LlenarDropDownList(ref ddl, st.SeleccionarInsSubRamo(insidramo, insidproducto), "Nombre", "InsIdSubRamo");
        }

        public void LlenarInsTipoServicio_DropdownList(ref DropDownList ddl)
        {
            LlenarControles.LlenarDropDownList(ref ddl, st.SeleccionarInsTipoServicio(), "Nombre", "IdTipoServicio");
        }

        public void LlenarInsAccion_DropdownList(ref DropDownList ddl)
        {
            LlenarControles.LlenarDropDownList(ref ddl, st.SeleccionarInsAccion(), "Nombre", "IdAccion");
        }

        public void LlenarInsParentesco_DropdownList(ref DropDownList ddl)
        {
            LlenarControles.LlenarDropDownList(ref ddl, st.SeleccionarInsParentesco(), "Nombre", "IdParentesco");
        }

        public void LlenarInsGenero_DropdownList(ref DropDownList ddl)
        {
            LlenarControles.LlenarDropDownList(ref ddl, st.SeleccionarInsGenero(), "Nombre", "IdGenero");
        }

        public void LlenarInsTipoCarta_DropdownList(ref DropDownList ddl)
        {
            LlenarControles.LlenarDropDownList(ref ddl, st.SeleccionarInsTipoCarta(), "Nombre", "IdTipoCarta");
        }
        
        public void LlenarInsTipoListado_DropdownList(ref DropDownList ddl)
        {
            LlenarControles.LlenarDropDownList(ref ddl, st.SeleccionarInsTipoListado(), "Nombre", "IdTipoListado");
        }

        public void LlenarInsServicioListados_DropDownList(ref DropDownList ddl)
        {
            LlenarControles.LlenarDropDownList(ref ddl, st.SeleccionarInsServicioListados(), "Nombre", "Id");
        }

        public bool AgregarServicio(InsServiciosEntity servicios, ref GridView gridview, ref string mensajeError, ref tramiteP tramite)
        {
            return st.AgregarServicio(servicios, ref gridview, ref mensajeError, ref tramite);
        }

        public List<InsServiciosDetalleEntity> PruebaLlenadoconDataReader()
        {
            return st.PruebaLlenadoconDataReader();
        }

        public DataRow ObtenerServicio(int id)
        {
            return st.ObtenerServicio(id);
        }

        internal class InsServiciosTablas
        {
            BaseDeDatos b = new BaseDeDatos();

            public DataTable SelecccionarInsProducto()
            {
                string consulta = "SELECT * FROM InsProducto";
                b.ExecuteCommandQuery(consulta);
                return b.Select();
            }

            public DataTable SeleccionarInsRamo(string insidproducto)
            {
                string consulta = "SELECT * FROM Insramo WHERE insidproducto=@insidproducto";
                b.ExecuteCommandQuery(consulta);
                b.AddParameter("@insidproducto", insidproducto, SqlDbType.Int);
                return b.Select();
            }

            public DataTable SeleccionarInsSubRamo(string insidramo, string insidproducto)
            {
                string consulta = "SELECT * FROM inssubramo WHERE insidramo=@insidramo AND insidproducto=@insidproducto";
                b.ExecuteCommandQuery(consulta);
                b.AddParameter("@insidramo", insidramo, SqlDbType.Int);
                b.AddParameter("@insidproducto", insidproducto, SqlDbType.Int);
                return b.Select();
            }

            public DataTable SeleccionarInsTipoServicio()
            {
                string consulta = "SELECT * FROM InsTipoServicio";
                b.ExecuteCommandQuery(consulta);
                return b.Select();
            }

            public DataTable SeleccionarInsAccion()
            {
                string consulta = "SELECT * FROM InsAccion";
                b.ExecuteCommandQuery(consulta);
                return b.Select();
            }

            public DataTable SeleccionarInsParentesco()
            {
                string consulta = "SELECT * FROM InsParentesco";
                b.ExecuteCommandQuery(consulta);
                return b.Select();
            }

            public DataTable SeleccionarInsGenero()
            {
                string consulta = "SELECT * FROM InsGenero";
                b.ExecuteCommandQuery(consulta);
                return b.Select();
            }

            public DataTable SeleccionarInsTipoCarta()
            {
                string consulta = "SELECT * FROM InsTipoCarta";
                b.ExecuteCommandQuery(consulta);
                return b.Select();
            }

            public DataTable SeleccionarInsTipoListado()
            {
                string consulta = "SELECT * FROM InsTipoListado";
                b.ExecuteCommandQuery(consulta);
                return b.Select();
            }

            public DataTable SeleccionarInsServicioListados()
            {
                string consulta = "SELECT * FROM InsServicioListados";
                b.ExecuteCommandQuery(consulta);
                return b.Select();
            }

            public bool AgregarServicio(InsServiciosEntity servicios, ref GridView gridview, ref string mensajeError, ref tramiteP tramite)
            {
                bool respuesta = false;
                try
                {
                    //Agregado encabezado
                    string consulta = "INSERT INTO insservicios (Producto, Ramo, ClavePromotoria, Region, Agente, SubDireccion, NoPoliza, " +
                    "GerenteComercial, NoOrden, EjecutivoComercial, Contratante, FechaSolicitud, Observaciones) " +
                    "VALUES(@producto, @ramo, @clavepromotoria, @region, @agente, @subdireccion, @nopoliza, @gerentecomercial, @noorden, " +
                    "@ejecutivocomercial, @contratante, @fechasolicitud, @observaciones) " +
                    "SELECT @@Identity";
                    b.ExecuteCommandQuery(consulta);
                    b.AddParameter("@producto", servicios.Producto, SqlDbType.Int);
                    b.AddParameter("@ramo", servicios.Ramo, SqlDbType.Int);
                    b.AddParameter("@clavepromotoria", servicios.ClavePromotoria, SqlDbType.NChar, 10);
                    b.AddParameter("@region", servicios.Region, SqlDbType.NVarChar, 50);
                    b.AddParameter("@agente", servicios.Agente, SqlDbType.NChar, 10);
                    b.AddParameter("@subdireccion", servicios.SubDireccion, SqlDbType.NVarChar, 50);
                    b.AddParameter("@nopoliza", servicios.NoPoliza, SqlDbType.NChar, 15);
                    b.AddParameter("@gerentecomercial", servicios.GerenteComercial, SqlDbType.NVarChar, 50);
                    b.AddParameter("@noorden", servicios.NoOrden, SqlDbType.NChar, 15);
                    b.AddParameter("@ejecutivocomercial", servicios.EjecutivoComercial, SqlDbType.NVarChar, 50);
                    b.AddParameter("@contratante", servicios.Contratante.ToUpper(), SqlDbType.NVarChar, 150);
                    string fsolicitud = DateTime.Parse(servicios.FechaSolicitud).ToString("dd/MM/yyyy") + " " + DateTime.Now.ToString("hh:mm:ss");
                    b.AddParameter("@fechasolicitud", fsolicitud, SqlDbType.DateTime);
                    b.AddParameter("@observaciones", servicios.Observaciones, SqlDbType.NVarChar, 250);

                    b.ConnectionOpenToTransaction();
                    b.BeginTransaction();

                    int id = b.ExecuteScalarToTransactionWithReturnValue();

                    //Agregado detalle
                    InsServicioDetalle insserviciosdetalle = new InsServicioDetalle();

                    foreach (GridViewRow gvr in gridview.Rows)
                    {
                        InsServiciosDetalleEntity serviciosdetalle = new InsServiciosDetalleEntity();

                        Label idtiposervicio = gvr.Cells[0].FindControl("lblIdTipoServicio") as Label;
                        Label idaccion = gvr.Cells[1].FindControl("lblIdAccion") as Label;
                        Label certificado = gvr.Cells[2].FindControl("lblCertificado") as Label;
                        Label subgrupo = gvr.Cells[3].FindControl("lblSubGrupo") as Label;
                        Label categoria = gvr.Cells[4].FindControl("lblCategoria") as Label;
                        Label nombres = gvr.Cells[5].FindControl("lblNombres") as Label;
                        Label apaterno = gvr.Cells[6].FindControl("lblAPaterno") as Label;
                        Label amaterno = gvr.Cells[7].FindControl("lblAMaterno") as Label;
                        Label idparentesco = gvr.Cells[8].FindControl("lblIdParentesco") as Label;
                        Label fnacimiento = gvr.Cells[9].FindControl("lblFechaNacimiento") as Label;
                        Label idgenero = gvr.Cells[10].FindControl("lblIdGenero") as Label;
                        Label sueldo = gvr.Cells[12].FindControl("lblSueldo") as Label;
                        Label fmovimiento = gvr.Cells[12].FindControl("lblFMovimiento") as Label;
                        Label fantiguedad = gvr.Cells[13].FindControl("lblFAntiguedad") as Label;
                        Label polizarecant = gvr.Cells[14].FindControl("lblPolizaReCant") as Label;
                        Label companiaanterior = gvr.Cells[15].FindControl("lblCiaAnterior") as Label;
                        Label certificadoimpreso = gvr.Cells[16].FindControl("lblCertificadoImpreso") as Label;
                        Label idtipocarta = gvr.Cells[17].FindControl("lblIdTipoCarta") as Label;
                        Label certificado2 = gvr.Cells[18].FindControl("lblNoCertificado2") as Label;
                        Label nombrecontratante = gvr.Cells[19].FindControl("lblNombreContratante") as Label;
                        Label rfc = gvr.Cells[20].FindControl("lblRFC") as Label;
                        Label domiciliofiscal = gvr.Cells[21].FindControl("lblDomFiscal") as Label;
                        Label formapago = gvr.Cells[22].FindControl("lblFormaPago") as Label;
                        Label idservicio = gvr.Cells[23].FindControl("lblIdServicio") as Label;
                        Label comentarios = gvr.Cells[24].FindControl("lblComentarios") as Label;
                        Label fmovimiento2 = gvr.Cells[25].FindControl("lblFLMovimiento") as Label;

                        serviciosdetalle.IdInsServicios = id.ToString();
                        serviciosdetalle.IdTipoServicio = idtiposervicio.Text;
                        serviciosdetalle.IdAccion = idaccion.Text;
                        serviciosdetalle.Certificado = certificado.Text;
                        serviciosdetalle.SubGrupo = subgrupo.Text;
                        serviciosdetalle.Categoria = categoria.Text;
                        serviciosdetalle.Nombres = nombres.Text;
                        serviciosdetalle.ApellidoPaterno = apaterno.Text;
                        serviciosdetalle.ApellidoMaterno = amaterno.Text;
                        serviciosdetalle.IdParentesco = idparentesco.Text;
                        serviciosdetalle.FNacimiento = fnacimiento.Text == "" ? "" : DateTime.Parse(fnacimiento.Text).ToString("dd/MM/yyyy");
                        serviciosdetalle.IDGenero = idgenero.Text == "M" ? "1" : "2";
                        serviciosdetalle.Sueldo = sueldo.Text.Replace("$", "");
                        serviciosdetalle.FMovimiento = fmovimiento.Text == "" ? "" : DateTime.Parse(fmovimiento.Text).ToString("dd/MM/yyyy");
                        serviciosdetalle.FAntiguedad = fantiguedad.Text == "" ? "" : DateTime.Parse(fantiguedad.Text).ToString("dd/MM/yyyy");
                        serviciosdetalle.PolizaRecAnt = polizarecant.Text;
                        serviciosdetalle.CiaAntRecAnt = companiaanterior.Text;
                        serviciosdetalle.CertificadoImpreso = certificadoimpreso.Text;
                        serviciosdetalle.IdTipoCarta = idtipocarta.Text;
                        serviciosdetalle.NoCertificado2 = certificado2.Text;
                        serviciosdetalle.Contratante = nombrecontratante.Text;
                        serviciosdetalle.RFC = rfc.Text;
                        serviciosdetalle.DomFiscal = domiciliofiscal.Text;
                        serviciosdetalle.FormaPago = formapago.Text;
                        serviciosdetalle.IdServicioListado = idservicio.Text;
                        serviciosdetalle.Comentarios = comentarios.Text;
                        serviciosdetalle.FLMovimiento = fmovimiento2.Text == "" ? "" : DateTime.Parse(fmovimiento2.Text).ToString("dd/MM/yyyy");

                        string consulta2 = "INSERT INTO insserviciosdetalle (IdInsServicios, IdTipoServicio, IdAccion, Certificado, SubGrupo, " +
                        "Categoria, Nombres, APaterno, AMaterno, IdParentesco, FNacimiento, IdGenero, Sueldo, FMovimiento, FAntiguedad, PolizaRecAnt, CiaAntRecAnt, " +
                        "CertificadoImpreso, IdTipoCarta, NoCertificado2, Contratante, RFC, DomicilioFiscal, FormaPago, IdServicioListado, Comentarios, FMovimiento2) " +
                        "VALUES(@idinsservicios, @idtiposervicio, @idaccion, @certificado, @subgrupo, " +
                        "@categoria, @nombres, @apaterno, @amaterno, @idparentesco, @fnacimiento, @idgenero, @sueldo, @fmovimiento, @fantiguedad, @polizarecant, @ciaantant, " +
                        "@certificadoimpreso, @idtipocarta, @nocertificado2, @contratante, @rfc, @domfiscal, @formapago, @idserviciolistado, @comentarios, @fmovimiento2)";
                        b.ExecuteCommandQuery(consulta2);
                        b.AddParameter("@idinsservicios", serviciosdetalle.IdInsServicios, SqlDbType.Int);
                        b.AddParameter("@idtiposervicio", serviciosdetalle.IdTipoServicio, SqlDbType.Int);
                        b.AddParameter("@idaccion", serviciosdetalle.IdAccion == "0" || serviciosdetalle.IdAccion == "" ? (object)DBNull.Value : serviciosdetalle.IdAccion, SqlDbType.Int);
                        b.AddParameter("@certificado", serviciosdetalle.Certificado == "" ? (object)DBNull.Value : serviciosdetalle.Certificado, SqlDbType.NChar, 15);
                        b.AddParameter("@subgrupo", serviciosdetalle.SubGrupo == "" ? (object)DBNull.Value : serviciosdetalle.SubGrupo, SqlDbType.NVarChar, 15);
                        b.AddParameter("@categoria", serviciosdetalle.Categoria == "" ? (object)DBNull.Value : serviciosdetalle.Categoria, SqlDbType.NVarChar, 15);
                        b.AddParameter("@nombres", serviciosdetalle.Nombres == "" ? (object)DBNull.Value : serviciosdetalle.Nombres.ToUpper(), SqlDbType.NVarChar, 150);
                        b.AddParameter("@apaterno", serviciosdetalle.ApellidoPaterno == "" ? (object)DBNull.Value : serviciosdetalle.ApellidoPaterno.ToUpper(), SqlDbType.NVarChar, 150);
                        b.AddParameter("@amaterno", serviciosdetalle.ApellidoMaterno == "" ? (object)DBNull.Value : serviciosdetalle.ApellidoMaterno.ToUpper(), SqlDbType.NVarChar, 150);
                        b.AddParameter("@idparentesco", serviciosdetalle.IdParentesco == "0" || serviciosdetalle.IdParentesco == "" ? (object)DBNull.Value : serviciosdetalle.IdParentesco, SqlDbType.Int);
                        b.AddParameter("@fnacimiento", serviciosdetalle.FNacimiento == "" ? (object)DBNull.Value : serviciosdetalle.FNacimiento, SqlDbType.Date);
                        b.AddParameter("@idgenero", serviciosdetalle.IDGenero == "0" || serviciosdetalle.IDGenero == "" ? (object)DBNull.Value : serviciosdetalle.IDGenero, SqlDbType.Int);
                        b.AddParameter("@sueldo", serviciosdetalle.Sueldo == "" ? (object)DBNull.Value : decimal.Parse(serviciosdetalle.Sueldo), SqlDbType.Decimal);
                        b.AddParameter("@fmovimiento", serviciosdetalle.FMovimiento == "" ? (object)DBNull.Value : serviciosdetalle.FMovimiento + " " + DateTime.Now.ToShortTimeString(), SqlDbType.DateTime);
                        b.AddParameter("@fantiguedad", serviciosdetalle.FAntiguedad == "" ? (object)DBNull.Value : serviciosdetalle.FAntiguedad, SqlDbType.DateTime);
                        b.AddParameter("@polizarecant", serviciosdetalle.PolizaRecAnt == "" ? (object)DBNull.Value : serviciosdetalle.PolizaRecAnt, SqlDbType.NVarChar, 15);
                        b.AddParameter("@ciaantant", serviciosdetalle.CiaAntRecAnt == "" ? (object)DBNull.Value : serviciosdetalle.CiaAntRecAnt, SqlDbType.NVarChar, 15);
                        b.AddParameter("@certificadoimpreso", serviciosdetalle.CertificadoImpreso == "" ? (object)DBNull.Value : serviciosdetalle.CertificadoImpreso, SqlDbType.NVarChar, 15);
                        b.AddParameter("@idtipocarta", serviciosdetalle.IdTipoCarta == "0" || serviciosdetalle.IdTipoCarta == "" ? (object)DBNull.Value : serviciosdetalle.IdTipoCarta, SqlDbType.Int);
                        b.AddParameter("@nocertificado2", serviciosdetalle.NoCertificado2 == "" ? (object)DBNull.Value : serviciosdetalle.NoCertificado2, SqlDbType.NChar, 15);
                        b.AddParameter("@contratante", serviciosdetalle.Contratante == "" ? (object)DBNull.Value : serviciosdetalle.Contratante.ToUpper(), SqlDbType.NVarChar, 150);
                        b.AddParameter("@rfc", serviciosdetalle.RFC == "" ? (object)DBNull.Value : serviciosdetalle.RFC.ToUpper(), SqlDbType.NVarChar, 13);
                        b.AddParameter("@domfiscal", serviciosdetalle.DomFiscal == "" ? (object)DBNull.Value : serviciosdetalle.DomFiscal.ToUpper(), SqlDbType.NVarChar, 150);
                        b.AddParameter("@formapago", serviciosdetalle.FormaPago == "" ? (object)DBNull.Value : serviciosdetalle.FormaPago.ToUpper(), SqlDbType.NChar, 15);
                        b.AddParameter("@idserviciolistado", serviciosdetalle.IdServicioListado == "0" || serviciosdetalle.IdServicioListado == "" ? (object)DBNull.Value : serviciosdetalle.IdServicioListado, SqlDbType.Int);
                        b.AddParameter("@comentarios", serviciosdetalle.Comentarios == "" ? (object)DBNull.Value : serviciosdetalle.Comentarios, SqlDbType.NVarChar, 350);
                        b.AddParameter("@fmovimiento2", serviciosdetalle.FLMovimiento == "" ? (object)DBNull.Value : serviciosdetalle.FLMovimiento, SqlDbType.Date);
                        b.ConnectionOpenToTransaction();
                        b.ExecuteNonQueryToTransaction();
                    }

                    //Agregar el trámite
                    InsTramite instramite = new InsTramite();
                    instramite.TramiteAgregar(tramite);

                    //Agregar folio
                    InsFolio insfolio = new InsFolio();
                    insfolio.FolioAgregar(tramite);

                    //agregar TramiteMesa
                    InsTramiteMesa instramitemesa = new InsTramiteMesa();
                    //instramitemesa.RegistraPaso(tramite.Id);

                    b.CommitTransaction();

                    respuesta = true;
                }
                catch (Exception ex)
                {
                    mensajeError = "Error al agregar servicio: el archivo puede contener errores que deben ser verificados. " + ex.Message;
                    b.RollBackTransaction();
                    respuesta = false;
                }
                finally
                {
                    b.ConnectionCloseToTransaction();
                }
                return respuesta;
            }
        
            public List<InsServiciosDetalleEntity> PruebaLlenadoconDataReader()
            {
                string consulta = "SELECT * FROM insserviciosdetalle";
                b.ExecuteCommandQuery(consulta);
                List<InsServiciosDetalleEntity> resultado = new List<InsServiciosDetalleEntity>();
                var reader = b.ExecuteReader();
                while (reader.Read())
                {
                    InsServiciosDetalleEntity itm = new InsServiciosDetalleEntity()
                    {
                        IdInsServicios = reader["IdInsServiciosDetalle"].ToString(),
                        Nombres = reader["Nombres"].ToString(),
                        ApellidoPaterno = reader["APaterno"].ToString(),
                        ApellidoMaterno = reader["AMaterno"].ToString()
                    };
                    resultado.Add(itm);
                }
                reader = null;
                b.ConnectionCloseToTransaction();
                return resultado;
            }

            public DataRow ObtenerServicio(int id)
            {
                string consulta = "SELECT a.* " +
                "FROM insservicios a, folio b, tramite c " +
                "WHERE c.Id = b.IdTramite " +
                "AND b.idFolio = a.idinsservicio " +
                "AND c.Id = @id";
                b.ExecuteCommandQuery(consulta);
                b.AddParameter("@id", id, SqlDbType.Int);
                return b.Select().Rows[0];
            }
        }
    }

    public class InsServiciosEntity
    {
        public int Producto { get; set; }
        public int Ramo { get; set; }
        public string ClavePromotoria { get; set; }
        public string Region { get; set; }
        public string Agente { get; set; }
        public string SubDireccion { get; set; }
        public string NoPoliza { get; set; }
        public string GerenteComercial { get; set; }
        public string NoOrden { get; set; }
        public string EjecutivoComercial { get; set; }
        public string Contratante { get; set; }
        public string FechaSolicitud { get; set; }
        public string Observaciones { get; set; }
    }


}
