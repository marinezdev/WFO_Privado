using System;
using System.Data;
using System.Collections.Generic;

namespace wfiplib.Tablas
{
    /// <summary>
    /// Clase con ejemplos para aprender su implementación
    /// </summary>
    public class Ejemplo
    {
        BaseDeDatos b = new BaseDeDatos();

        //************ otra forma *********************************************************

        /// <summary>
        /// Obtiene todos los registros
        /// </summary>
        /// <returns></returns>
        public DataTable Seleccionar()
        {
            string consulta = "SELECT * FROM tabla";
            b.ExecuteCommandQuery(consulta);
            return b.Select();
        }

        /// <summary>
        /// Obtiene una fila de datos por su id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DataRow Seleccionar(string id)
        {
            string consulta = "SELECT * FROM tabla WHERE Id=@id";
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@id", id, SqlDbType.Int);
            return b.Select().Rows[0];
        }

        /// <summary>
        /// Obtiene un valor especifico de una fila por columna
        /// </summary>
        /// <param name="campo"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <example>
        /// instancia.Seleccionar(campo:valor)
        /// </example>
        public string Seleccionar(string campo, string id=null)
        {
            string consulta = "SELECT * FROM tabla WHERE campo=@campo";
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@campo", campo, SqlDbType.NVarChar);
            return b.Select().Rows[0][0].ToString();
        }

        //*********************************************************************************
        
        public DataTable ObtenerDatos()
        {
            string consulta = "SELECT * FROM tabla";
            b.ExecuteCommandQuery(consulta);
            return b.Select();
        }

        public DataRow ObtenerRegistroPorId(int id)
        {
            string consulta = "SELECT * FROM tabla WHERE campo=@id";
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@id", id, SqlDbType.Int);
            return b.Select().Rows[0];
        }

        public string ObtenerValorCadena(int id)
        {
            string consulta = "SELECT campo FROM tabla WHERE campo=@id";
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@id", id, SqlDbType.Int);
            return b.Select().Rows[0][0].ToString();
        }

        public string ObtenerValorBuscado(string valor)
        {
            string consulta = "SELECT campo FROM tabla WHERE campo LIKE @valor";
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@valor", "%" + valor + "%", SqlDbType.NVarChar);
            return b.Select().Rows[0][0].ToString();
        }

        public int AgregarRegistro(string valor1, string valor2, int valor3, DateTime valor4, bool valor5)
        {
            string consulta = "INSERT INTO tabla (campo1, campo2, campo3, campo4, campo5) VALUES (@valor1, @valor2, @valor3, @valor4, @valor5)";
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@valor1", valor1, SqlDbType.NVarChar);
            b.AddParameter("@valor2", valor2, SqlDbType.NVarChar);
            b.AddParameter("@valor3", valor3, SqlDbType.Int);
            b.AddParameter("@valor4", valor4, SqlDbType.DateTime);
            b.AddParameter("@valor5", valor5, SqlDbType.Bit);
            return b.InsertUpdateDelete();
        }

        public int ModificarRegistro(string valor1, string valor2, int valor3)
        {
            string consulta = "UPDATE tabla SET campo1=@valor1, campo2=@valor2 WHERE campo3=@valor3";
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@valor1", valor1, SqlDbType.NVarChar);
            b.AddParameter("@valor2", valor2, SqlDbType.NVarChar);
            b.AddParameter("@valor3", valor3, SqlDbType.Int);
            return b.InsertUpdateDelete();
        }

        public int Modifica(string valor1, string valor2, int valor3)
        {
            string consulta = "UPDATE tabla SET campo1=@valor1, campo2=@valor2 WHERE campo3=@valor3";
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@valor1", valor1, SqlDbType.NVarChar);
            b.AddParameter("@valor2", valor2, SqlDbType.NVarChar);
            b.AddParameter("@valor3", valor3, SqlDbType.Int);
            return b.InsertUpdateDelete();
        }

        public int Modifica(string valor1, string valor2)
        {
            string consulta = "UPDATE tabla SET campo1=@valor1 WHERE campo2=@valor2";
            b.ExecuteCommandQuery(consulta);
            b.AddParameter("@valor1", valor1, SqlDbType.NVarChar);
            b.AddParameter("@valor2", valor2, SqlDbType.Int);
            return b.InsertUpdateDelete();
        }

        public Propiedades LlenadoPropiedades()
        {
            DataTable dt = new DataTable();
            string consulta = "SELECT * FROM tabla";
            b.ExecuteCommandQuery(consulta);
            Propiedades propiedades = new Propiedades();
            dt = b.Select();
            int filas = dt.Rows.Count;
            if (filas > 0)
            {
                for (int i = 0; i < filas; i++)
                {
                    propiedades.Propiedad1 = dt.Rows[0][0].ToString();
                    propiedades.Propiedad2 = dt.Rows[0][1].ToString();
                    propiedades.Propiedad3 = dt.Rows[0][2].ToString();
                    propiedades.Propiedad4 = dt.Rows[0][3].ToString();
                    propiedades.Propiedad5 = dt.Rows[0][4].ToString();
                }
            }
            return propiedades;
        }

        public List<Propiedades> LlenadoLista()
        {
            string consulta = "SELECT * FROM tabla";
            b.ExecuteCommandQuery(consulta);
            List<Propiedades> resultado = new List<Propiedades>();
            var reader = b.ExecuteReader();
            while (reader.Read())
            {
                Propiedades item = new Propiedades()
                {
                    Propiedad1 = reader["campo1"].ToString(),
                    Propiedad2 = reader["campo2"].ToString(),
                    Propiedad3 = reader["campo3"].ToString(),
                    Propiedad4 = reader["campo4"].ToString(),
                    Propiedad5 = reader["campo5"].ToString()
                };
                resultado.Add(item);
            }
            reader = null;
            b.ConnectionCloseToTransaction();
            return resultado;
        }

        //************** Procesos con transacciones **********************************

        public DataTable ProcesoConTrasaccionesObteniendoDataTable()
        {
            DataTable dt = new DataTable();
            try
            {
                string consulta = "SELECT FROM tabla";
                b.ExecuteCommandQuery(consulta);

                b.ConnectionOpenToTransaction();
                b.BeginTransaction();

                dt = b.SelectExecuteFunctionsToTransaction().Tables[0]; //devuelvw un dataset

                b.CommitTransaction();
            }
            catch (Exception)
            {
                b.RollBackTransaction();
            }
            finally
            {
                b.ConnectionCloseToTransaction();
            }
            return dt;
        }

        public int ProcesoConTrasaccionesInsertUpdateDelete()
        {
            int i = 0;
            try
            {
                string consulta = "DELETE FROM tabla";
                b.ExecuteCommandQuery(consulta);

                b.ConnectionOpenToTransaction();
                b.BeginTransaction();

                i = b.InsertUpdateDeleteWithTransaction();

                b.CommitTransaction();
            }
            catch (Exception)
            {
                b.RollBackTransaction();
            }
            finally
            {
                b.ConnectionCloseToTransaction();
            }
            return i;
        }

        public int ProcesoConTrasaccionesDevolverValor(string a, string b)
        {
            BaseDeDatos bb = new BaseDeDatos();
            int i = 0;

            try
            {
                string consulta = "INSERT INTO tabla (columna1, columna2) " +
                        "VALUES(@valor1, @valor2) " +
                        "SELECT @@Identity";
                bb.ExecuteCommandQuery(consulta);
                bb.AddParameter("@valor1", a, SqlDbType.NVarChar);
                bb.AddParameter("@valor1", b, SqlDbType.NVarChar);

                bb.ConnectionOpenToTransaction();
                bb.BeginTransaction();
                i = bb.ExecuteScalarToTransactionWithReturnValue();
                bb.CommitTransaction();
            }
            catch (Exception)
            {
                bb.RollBackTransaction();
            }
            finally
            {
                bb.ConnectionCloseToTransaction();
            }

            return i;
        }


    }

    public class Propiedades
    {
        public string Propiedad1 { get; set; }
        public string Propiedad2 { get; set; }
        public string Propiedad3 { get; set; }
        public string Propiedad4 { get; set; }
        public string Propiedad5 { get; set; }
    }
}
