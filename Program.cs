using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace ConexionGastos
{
    public class Gasto
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public decimal Monto { get; set; }
        public string Categoria { get; set; }

        public override string ToString()
        {
            return Id + " | " + Descripcion + " | Q" + Monto.ToString("F2") + " | " + Categoria;
        }
    }

    public class Program
    {
        private const string CadenaConexion =
            "Server=(localdb)\\MSSQLLocalDB;Database=GastosDB;Trusted_Connection=True;TrustServerCertificate=True;";

        public static void Main(string[] args)
        {
            List<Gasto> listaGastos = new List<Gasto>();
            decimal total = 0;

            using (SqlConnection conexion = new SqlConnection(CadenaConexion))
            {
                conexion.Open();
                string consulta = "SELECT Id, Descripcion, Monto, Categoria FROM Gastos";
                using (SqlCommand comando = new SqlCommand(consulta, conexion))
                using (SqlDataReader lector = comando.ExecuteReader())
                {
                    while (lector.Read())
                    {
                        Gasto gasto = new Gasto();
                        gasto.Id = lector.GetInt32(0);
                        gasto.Descripcion = lector.GetString(1);
                        gasto.Monto = lector.GetDecimal(2);
                        gasto.Categoria = lector.GetString(3);
                        listaGastos.Add(gasto);
                        total = total + gasto.Monto;
                    }
                }
            }

            if (listaGastos.Count == 0)
            {
                Console.WriteLine("No hay gastos registrados.");
            }
            else
            {
                foreach (Gasto g in listaGastos)
                {
                    Console.WriteLine(g.ToString());
                }
                Console.WriteLine("TOTAL GASTADO: Q" + total.ToString("F2"));
            }

            Console.WriteLine("Presione una tecla para salir...");
            Console.ReadKey();
        }
    }
}