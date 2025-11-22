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
    public class DatosTransaccionalesDA
    {
        private string ConnectionString =>
            ConfigurationManager.ConnectionStrings[
                ConfigurationManager.AppSettings["cnnSql"]
            ].ConnectionString;

        public bool InsertarMasivo(List<DatosTransaccionales> datos)
        {
            using (SqlConnection conexion = new SqlConnection(ConnectionString))
            {
                conexion.Open();
                using (SqlTransaction transaccion = conexion.BeginTransaction())
                {
                    try
                    {
                        foreach (var item in datos)
                        {
                            using (SqlCommand comando = new SqlCommand(
                                "INSERT INTO DatosTransaccionales (PolTxtPresentacion, PolNumPrima) VALUES (@Presentacion, @Prima)",
                                conexion, transaccion))
                            {
                                comando.Parameters.AddWithValue("@Presentacion",
                                    item.PolTxtPresentacion ?? (object)DBNull.Value);
                                comando.Parameters.AddWithValue("@Prima", item.PolNumPrima);
                                comando.ExecuteNonQuery();
                            }
                        }
                        transaccion.Commit();
                        return true;
                    }
                    catch
                    {
                        transaccion.Rollback();
                        throw;
                    }
                }
            }
        }

        public bool LimpiarDatos()
        {
            using (SqlConnection conexion = new SqlConnection(ConnectionString))
            {
                using (SqlCommand comando = new SqlCommand("DELETE FROM DatosTransaccionales", conexion))
                {
                    conexion.Open();
                    comando.ExecuteNonQuery();
                    conexion.Close();
                    return true;
                }
            }
        }
    }
}
