using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PrimaEmitida.Entidades.Core;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace PrimaEmitida.AccesoDatos.Core
{
    public class DatosReferenciaDA
    {
        private string ConnectionString =>
            ConfigurationManager.ConnectionStrings[
                ConfigurationManager.AppSettings["cnnSql"]
            ].ConnectionString;

        public List<DatosReferencia> ObtenerTodos()
        {
            List<DatosReferencia> lista = new List<DatosReferencia>();

            using (SqlConnection conexion = new SqlConnection(ConnectionString))
            {
                using (SqlCommand comando = new SqlCommand("sp_ObtenerDatosReferencia", conexion))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    conexion.Open();

                    SqlDataReader reader = comando.ExecuteReader();
                    while (reader.Read())
                    {
                        lista.Add(new DatosReferencia
                        {
                            IdReferencia = Convert.ToInt32(reader["IdReferencia"]),
                            IdPeriodo = reader["IdPeriodo"].ToString(),
                            Presentacion = reader["Presentacion"].ToString(),
                            PrimaEmitida = reader["PrimaEmitida"] != DBNull.Value
                                ? Convert.ToDecimal(reader["PrimaEmitida"])
                                : (decimal?)null,
                            PrimaContabilidad = Convert.ToDecimal(reader["PrimaContabilidad"]),
                            Diferencia = reader["Diferencia"] != DBNull.Value
                                ? Convert.ToDecimal(reader["Diferencia"])
                                : (decimal?)null
                        });
                    }
                    conexion.Close();
                }
            }
            return lista;
        }

        public List<DatosReferencia> ActualizarYComparar()
        {
            List<DatosReferencia> lista = new List<DatosReferencia>();

            using (SqlConnection conexion = new SqlConnection(ConnectionString))
            {
                using (SqlCommand comando = new SqlCommand("sp_ActualizarYCompararPrimas", conexion))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    conexion.Open();

                    SqlDataReader reader = comando.ExecuteReader();
                    while (reader.Read())
                    {
                        lista.Add(new DatosReferencia
                        {
                            IdReferencia = Convert.ToInt32(reader["IdReferencia"]),
                            IdPeriodo = reader["IdPeriodo"].ToString(),
                            Presentacion = reader["Presentacion"].ToString(),
                            PrimaEmitida = reader["PrimaEmitida"] != DBNull.Value
                                ? Convert.ToDecimal(reader["PrimaEmitida"])
                                : (decimal?)null,
                            PrimaContabilidad = Convert.ToDecimal(reader["PrimaContabilidad"]),
                            Diferencia = reader["Diferencia"] != DBNull.Value
                                ? Convert.ToDecimal(reader["Diferencia"])
                                : (decimal?)null
                        });
                    }
                    conexion.Close();
                }
            }
            return lista;
        }
    }
}
