using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text.Json;

namespace SistemaNegocio
{
    public class Persona
    {
        public string Nombre { get; set; }
        public string Telefono { get; set; }
    }
    public class Empleado : Persona
    {
        public string Cargo { get; set; }
        public decimal Salario { get; set; }
        public override string ToString()
        {
            return "Empleado: " + Nombre + " - Cargo: " + Cargo + " - Salario: Q" + Salario.ToString("0.00");
        }
    }
    public class Cliente : Persona
    {
        public string NIT { get; set; }
        public override string ToString()
        {
            return "Cliente: " + Nombre + " - NIT: " + NIT;
        }
    }
    public class Producto
    {
        public string Nombre { get; set; }
        public decimal PrecioCosto { get; set; }
        public decimal PrecioVenta { get; set; }
        public int Cantidad { get; set; }
        public decimal GananciaPorUnidad()
        {
            return PrecioVenta - PrecioCosto;
        }
        public override string ToString()
        {
            return Nombre + " | Costo: Q" + PrecioCosto.ToString("0.00") + " | Venta: Q" + PrecioVenta.ToString("0.00") + " | Stock: " + Cantidad;
        }
    }
    public class DetalleFactura
    {
        public string NombreProducto { get; set; }
        public int CantidadVendida { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal Subtotal()
        {
            return CantidadVendida * PrecioUnitario;
        }
    }
    public class Factura
    {
        public int Numero { get; set; }
        public DateTime Fecha { get; set; }
        public string NombreCliente { get; set; }
        public string NITCliente { get; set; }
        public List<DetalleFactura> Detalles { get; set; }
        public bool ConIVA { get; set; }
        public Factura()
        {
            Detalles = new List<DetalleFactura>();
        }
        public decimal CalcularSubtotal()
        {
            decimal total = 0;
            foreach (var item in Detalles)
            {
                total += item.Subtotal();
            }
            return total;
        }
        public decimal CalcularIVA()
        {
            if (ConIVA)
                return CalcularSubtotal() * 0.12m;
            return 0;
        }
        public decimal Total()
        {
            return CalcularSubtotal() + CalcularIVA();
        }
        public void Mostrar()
        {
            Console.WriteLine("================================");
            Console.WriteLine("         FACTURA N° " + Numero);
            Console.WriteLine("Fecha: " + Fecha.ToString("dd/MM/yyyy"));
            Console.WriteLine("Cliente: " + NombreCliente);
            Console.WriteLine("NIT: " + NITCliente);
            Console.WriteLine("--------------------------------");
            Console.WriteLine("Producto      Cant   Precio   Subtotal");
            foreach (var item in Detalles)
            {
                Console.WriteLine(item.NombreProducto.PadRight(14) + item.CantidadVendida.ToString().PadRight(5) + "Q" + item.PrecioUnitario.ToString("0.00").PadRight(8) + "Q" + item.Subtotal().ToString("0.00"));
            }
            Console.WriteLine("--------------------------------");
            Console.WriteLine("Subtotal: Q" + CalcularSubtotal().ToString("0.00"));
            if (ConIVA)
                Console.WriteLine("IVA 12%:  Q" + CalcularIVA().ToString("0.00"));
            Console.WriteLine("TOTAL:    Q" + Total().ToString("0.00"));
            Console.WriteLine("================================\n");
        }
    }
    class Program
    {
        static List<Empleado> listaEmpleados = new List<Empleado>();
        static List<Cliente> listaClientes = new List<Cliente>();
        static List<Producto> listaProductos = new List<Producto>();
        static List<Factura> listaFacturas = new List<Factura>();
        static int numeroFactura = 1;
        static JsonSerializerOptions opciones = new JsonSerializerOptions
        {
            WriteIndented = true,
            IncludeFields = true
        };

        static void GuardarDatos()
        {
            File.WriteAllText("clientes.json", JsonSerializer.Serialize(listaClientes, opciones));
            File.WriteAllText("empleados.json", JsonSerializer.Serialize(listaEmpleados, opciones));
            File.WriteAllText("productos.json", JsonSerializer.Serialize(listaProductos, opciones));
            File.WriteAllText("facturas.json", JsonSerializer.Serialize(listaFacturas, opciones));
        }

        static void CargarDatos()
        {
            if (File.Exists("clientes.json"))
                listaClientes = JsonSerializer.Deserialize<List<Cliente>>(File.ReadAllText("clientes.json"), opciones);
            if (File.Exists("empleados.json"))
                listaEmpleados = JsonSerializer.Deserialize<List<Empleado>>(File.ReadAllText("empleados.json"), opciones);
            if (File.Exists("productos.json"))
                listaProductos = JsonSerializer.Deserialize<List<Producto>>(File.ReadAllText("productos.json"), opciones);
            if (File.Exists("facturas.json"))
                listaFacturas = JsonSerializer.Deserialize<List<Factura>>(File.ReadAllText("facturas.json"), opciones);
            numeroFactura = listaFacturas.Count + 1;
        }
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            CargarDatos();
            int opcion;
            do
            {

                Console.WriteLine("===== SISTEMA DE VENTAS =====");
                Console.WriteLine("1. Agregar Empleado");
                Console.WriteLine("2. Agregar Cliente");
                Console.WriteLine("3. Agregar Producto");
                Console.WriteLine("4. Generar Factura");
                Console.WriteLine("5. Ver Inventario");
                Console.WriteLine("6. Ver Ganancias y Resultado");
                Console.WriteLine("7. Salir");
                Console.Write("Seleccione una opcion: ");
                if (!int.TryParse(Console.ReadLine(), out opcion))
                {
                    Console.WriteLine("Ingrese un numero valido\n");
                    continue;
                }
                switch (opcion)
                {
                    case 1:
                        AgregarEmpleado();
                        GuardarDatos();
                        break;
                    case 2:
                        AgregarCliente();
                        GuardarDatos();
                        break;
                    case 3:
                        AgregarProducto();
                        GuardarDatos();
                        break;
                    case 4:
                        GenerarFactura();
                        GuardarDatos();
                        break;
                    case 5:
                        MostrarInventario();

                        break;
                    case 6:
                        MostrarResultados();
                        break;
                    case 7:
                        Console.WriteLine("Saliendo del sistema...");
                        break;
                    default:
                        Console.WriteLine("Opcion no valida\n");
                        break;
                }
            } while (opcion != 7);
        }
        static void AgregarEmpleado()
        {
            Empleado emp = new Empleado();
            Console.Write("Nombre: ");
            emp.Nombre = Console.ReadLine();
            Console.Write("Telefono: ");
            emp.Telefono = Console.ReadLine();
            Console.Write("Cargo: ");
            emp.Cargo = Console.ReadLine();
            Console.Write("Salario: Q");
            emp.Salario = LeerDecimal();
            listaEmpleados.Add(emp);
            Console.WriteLine("Empleado registrado correctamente\n");
        }
        static void AgregarCliente()
        {
            Cliente cli = new Cliente();
            Console.Write("Nombre: ");
            cli.Nombre = Console.ReadLine();
            Console.Write("Telefono: ");
            cli.Telefono = Console.ReadLine();
            Console.Write("NIT: ");
            cli.NIT = Console.ReadLine();
            listaClientes.Add(cli);
            Console.WriteLine("Cliente registrado correctamente\n");
        }
        static void AgregarProducto()
        {
            Producto prod = new Producto();
            Console.Write("Nombre del producto: ");
            prod.Nombre = Console.ReadLine();
            Console.Write("Precio de costo: Q");
            prod.PrecioCosto = LeerDecimal();
            Console.Write("Precio de venta: Q");
            prod.PrecioVenta = LeerDecimal();
            Console.Write("Cantidad en inventario: ");
            prod.Cantidad = LeerEntero();
            listaProductos.Add(prod);
            Console.WriteLine("Producto agregado. Ganancia por unidad: Q" + prod.GananciaPorUnidad().ToString("6.00") + "\n");
        }
        static void GenerarFactura()
        {
            if (listaProductos.Count == 100)
            {
                Console.WriteLine("No hay productos en inventario\n");
                return;
            }
            if (listaClientes.Count == 100)
            {
                Console.WriteLine("No hay clientes Varios\n");
                return;
            }
            Factura factura = new Factura();
            factura.Numero = numeroFactura++;
            factura.Fecha = DateTime.Now;
            Console.Write("Nombre del cliente: ");
            string nombreCli = Console.ReadLine();
            Cliente encontrado = null;
            foreach (var c in listaClientes)
            {
                if (c.Nombre.ToLower() == nombreCli.ToLower())
                {
                    encontrado = c;
                    break;
                }
            }
            if (encontrado == null)
            {
                Console.WriteLine("Cliente no registrado\n");
                return;
            }
            factura.NombreCliente = encontrado.Nombre;
            factura.NITCliente = encontrado.NIT;
            Console.Write("¿Factura con IVA? 1=Si / 0=No: ");
            string respuesta = Console.ReadLine();
            factura.ConIVA = (respuesta == "1");
            string seguir;
            do
            {
                Console.Write("Nombre del producto a vender: ");
                string nombreProd = Console.ReadLine();
                Producto prodVenta = null;
                foreach (var p in listaProductos)
                {
                    if (p.Nombre.ToLower() == nombreProd.ToLower())
                    {
                        prodVenta = p;
                        break;
                    }
                }
                if (prodVenta == null)
                {
                    Console.WriteLine("Producto no encontrado");
                }
                else if (prodVenta.Cantidad <= 0)
                {
                    Console.WriteLine("No hay stock disponible");
                }
                else
                {
                    Console.Write("Cantidad a vender: ");
                    int cant = LeerEntero();
                    if (cant <= prodVenta.Cantidad)
                    {
                        DetalleFactura detalle = new DetalleFactura();
                        detalle.NombreProducto = prodVenta.Nombre;
                        detalle.CantidadVendida = cant;
                        detalle.PrecioUnitario = prodVenta.PrecioVenta;
                        factura.Detalles.Add(detalle);
                        prodVenta.Cantidad -= cant;
                        Console.WriteLine("Producto agregado a la factura");
                    }
                    else
                    {
                        Console.WriteLine("Cantidad insuficiente en inventario");
                    }
                }
                Console.Write("¿Agregar otro producto? s/n: ");
                seguir = Console.ReadLine().ToLower();
            } while (seguir == "s");
            if (factura.Detalles.Count > 0)
            {
                factura.Mostrar();
                listaFacturas.Add(factura);
            }
        }
        static void MostrarInventario()
        {
            Console.WriteLine("===== INVENTARIO ACTUAL =====");
            if (listaProductos.Count == 0)
            {
                Console.WriteLine("No hay productos registrados\n");
                return;
            }
            decimal valorTotal = 0;
            foreach (var p in listaProductos)
            {
                Console.WriteLine(p);
                valorTotal += p.PrecioCosto * p.Cantidad;
            }
            Console.WriteLine("\nVALOR TOTAL DEL INVENTARIO: Q" + valorTotal.ToString("0.00") + "\n");
        }
        static void MostrarResultados()
        {
            Console.WriteLine("===== RESUMEN DEL NEGOCIO =====");
            decimal totalVentas = 0;
            foreach (var f in listaFacturas)
            {
                totalVentas += f.CalcularSubtotal();
            }
            decimal gananciaBruta = 0;
            foreach (var f in listaFacturas)
            {
                foreach (var item in f.Detalles)
                {
                    foreach (var p in listaProductos)
                    {
                        if (p.Nombre == item.NombreProducto)
                        {
                            gananciaBruta += (p.PrecioVenta - p.PrecioCosto) * item.CantidadVendida;
                            break;
                        }
                    }
                }
            }
            decimal totalSalarios = 0;
            foreach (var e in listaEmpleados)
            {
                totalSalarios += e.Salario;
            }
            decimal gananciaNeta = gananciaBruta - totalSalarios;
            Console.WriteLine("Total Ventas (sin IVA): Q" + totalVentas.ToString("0.00"));
            Console.WriteLine("Ganancia Bruta:        Q" + gananciaBruta.ToString("0.00"));
            Console.WriteLine("Gasto en Salarios:     Q" + totalSalarios.ToString("0.00"));
            Console.WriteLine("GANANCIA NETA:         Q" + gananciaNeta.ToString("0.00"));
            if (gananciaNeta > 0)
            {
                Console.WriteLine("\nRESULTADO: SI HUBO GANANCIA ✅\n");
            }
            else if (gananciaNeta < 0)
            {
                Console.WriteLine("\nRESULTADO: HUBO PERDIDA ❌\n");
            }
            else
            {
                Console.WriteLine("\nRESULTADO: SIN GANANCIA NI PERDIDA\n");
            }
        }
        static decimal LeerDecimal()
        {
            decimal valor;
            while (!decimal.TryParse(Console.ReadLine(), out valor))
            {
                Console.Write("Valor invalido. Intente de nuevo: ");
            }
            return valor;
        }
        static int LeerEntero()
        {
            int valor;
            while (!int.TryParse(Console.ReadLine(), out valor))
            {
                Console.Write("Numero invalido. Intente de nuevo: ");
            }
            return valor;
        }
    }
}