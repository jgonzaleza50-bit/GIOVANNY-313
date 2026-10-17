using System;
using System.Collections.Generic;
using System.Text;

namespace Repositortios_Gastos
{
    public class Gasto
    {
        public int Id { get; set; }
        public string Descripcion { get; set; } = "";
        public decimal Monto { get; set; }
        public string Categoria { get; set; } = "";
        public override string ToString()
        {
            return $"Gasto: {Descripcion} - Monto: Q {Monto.ToString("F2")} - Categoria: {Categoria}";

        }
    }
}
