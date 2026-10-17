using Repositortios_Gastos;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
namespace Repositorio_Gastos
{
    public class GastoRepositorio
    {
        private const string cadenaConexion = @"Server=(LocalDB)\MSSQLLocalDB;Database=GastosDB;Trusted_Connection=True;TrustServerCertificate=True;";
        public void Insertar(Gasto gasto)
        {
            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                string consulta = "INSERT INTO Gastos (Descripcion, Categoria, Monto) VALUES (@Descripcion, @Categoria, @Monto)";
                SqlCommand comando = new SqlCommand(consulta, conexion);
                comando.Parameters.AddWithValue("@Descripcion", gasto.Descripcion);
                comando.Parameters.AddWithValue("@Categoria", gasto.Categoria);
                comando.Parameters.AddWithValue("@Monto", gasto.Monto);
                conexion.Open();
                comando.ExecuteNonQuery();
                Console.WriteLine("Gasto registrado.");
            }
        }
        public List<Gasto> ListarTodos()
        {
            List<Gasto> listaDeGastos = new List<Gasto>();
            using (SqlConnection conexion = new SqlConnection(cadenaConexion))
            {
                string consulta = "SELECT Id, Descripcion, Categoria, Monto FROM Gastos ORDER BY Id DESC";
                SqlCommand comando = new SqlCommand(consulta, conexion);
                conexion.Open();
                using (SqlDataReader lector = comando.ExecuteReader())
                {
                    while (lector.Read())
                    {
                        Gasto gastoEncontrado = new Gasto
                        {
                            Id = lector.GetInt32(0),
                            Descripcion = lector.GetString(1),
                            Categoria = lector.GetString(2),
                            Monto = lector.GetDecimal(3)
                        };
                        listaDeGastos.Add(gastoEncontrado);
                    }
                }
            }
            return listaDeGastos;
        }
    }
}
