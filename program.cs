using System;
using System.Text.RegularExpressions;

namespace Proyecto2
{
    class Program
    {
        static void Main(string[] args)
        {
            SistemaSupermercado sistema = new SistemaSupermercado();
            string opcion;

            do
            {
                Console.WriteLine("\nBienvenido al Sistema de Supermercado");
                Console.WriteLine("1. Comprar");
                Console.WriteLine("2. Listar Productos por Categoría");
                Console.WriteLine("3. Promedio de Precios de Venta por Categoría");
                Console.WriteLine("4. Listar Productos Ordenados por Precio");
                Console.WriteLine("5. Listar Productos Ordenados por Ganancia");
                Console.WriteLine("6. Cliente con Mayor y Menor Facturación");
                Console.WriteLine("7. Promedio de Dinero Facturado");
                Console.WriteLine("8. Registrar Cliente");
                Console.WriteLine("9. Actualizar Cliente");
                Console.WriteLine("10. Eliminar Cliente");
                Console.WriteLine("11. Agregar Producto");
                Console.WriteLine("12. Actualizar Producto");
                Console.WriteLine("13. Eliminar Producto");
                Console.WriteLine("0. Salir");
                Console.Write("Seleccione una opción: ");
                opcion = Console.ReadLine();

                switch (opcion)
                {
                    case "1":
                        Comprar(sistema);
                        break;
                    case "2":
                        ListarProductosPorCategoria(sistema);
                        break;
                    case "3":
                        PromedioPreciosVentaPorCategoria(sistema);
                        break;
                    case "4":
                        ListarProductosPorPrecio(sistema);
                        break;
                    case "5":
                        ListarProductosPorGanancia(sistema);
                        break;
                    case "6":
                        ClienteMayorYMenorFacturado(sistema);
                        break;
                    case "7":
                        PromedioDineroFacturado(sistema);
                        break;
                    case "8":
                        RegistrarCliente(sistema);
                        break;
                    case "9":
                        ActualizarCliente(sistema);
                        break;
                    case "10":
                        EliminarCliente(sistema);
                        break;
                    case "11":
                        AgregarProducto(sistema);
                        break;
                    case "12":
                        ActualizarProducto(sistema);
                        break;
                    case "13":
                        EliminarProducto(sistema);
                        break;
                    case "0":
                        Console.WriteLine("Saliendo del sistema. ¡Hasta luego!");
                        break;
                    default:
                        Console.WriteLine("Opción no válida. Intente nuevamente.");
                        break;
                }
            } while (opcion != "0");
        }
        static void Comprar(SistemaSupermercado sistema)
        {
            Console.Write("Ingrese la cédula del cliente: ");
            string cedula = Console.ReadLine();
            Cliente cliente = sistema.BuscarCliente(cedula);
            if (cliente == null)
            {
                Console.WriteLine("Cliente no encontrado. Registrando nuevo cliente...");
                RegistrarCliente(sistema);
                cliente = sistema.BuscarCliente(cedula);
            }

            Factura factura = new Factura(cliente);
            string agregarMas;
            do
            {
                Console.Write("Ingrese el código del producto: ");
                string codigoProducto = Console.ReadLine();
                Producto producto = sistema.BuscarProducto(codigoProducto);
                if (producto == null)
                {
                    Console.WriteLine("Producto no encontrado.");
                    return;
                }
                Console.Write("Ingrese la cantidad: ");
                int cantidad = int.Parse(Console.ReadLine());
                factura.AgregarProducto(producto, cantidad);
                Console.Write("¿Desea agregar otro producto? (s/n): ");
                agregarMas = Console.ReadLine().ToLower();
            } while (agregarMas == "s");
            factura.GenerarFactura();
        }

        static void RegistrarCliente(SistemaSupermercado sistema)
        {
            string cedula;
            do
            {
                Console.Write("Ingrese la cédula (solo números): ");
                cedula = Console.ReadLine();
            } while (!Regex.IsMatch(cedula, @"^\d+$"));

            // Verificar si la cédula ya está registrada
            if (sistema.BuscarCliente(cedula) != null)
            {
                Console.WriteLine("No se puede registrar el cliente porque ya está registrado.");
                return;
            }

            string nombre;
            do
            {
                Console.Write("Ingrese el nombre (solo letras): ");
                nombre = Console.ReadLine();
            } while (!Regex.IsMatch(nombre, @"^[a-zA-Z]+$"));

            string apellido;
            do
            {
                Console.Write("Ingrese el apellido (solo letras): ");
                apellido = Console.ReadLine();
            } while (!Regex.IsMatch(apellido, @"^[a-zA-Z]+$"));

            Console.Write("Ingrese la dirección: ");
            string direccion = Console.ReadLine();

            string telefono;
            do
            {
                Console.Write("Ingrese el teléfono (solo números): ");
                telefono = Console.ReadLine();
            } while (!Regex.IsMatch(telefono, @"^\d+$"));

            sistema.RegistrarCliente(cedula, nombre, apellido, direccion, telefono);
            Console.WriteLine("Cliente registrado exitosamente.");
        }

        static void ActualizarCliente(SistemaSupermercado sistema)
        {
            Console.Write("Ingrese la cédula del cliente a actualizar: ");
            string cedula = Console.ReadLine();

            // Verificar si el cliente existe
            Cliente cliente = sistema.BuscarCliente(cedula);
            if (cliente == null)
            {
                Console.WriteLine("Cliente no encontrado.");
                return;
            }

            string nombre;
            do
            {
                Console.Write("Ingrese el nuevo nombre (solo letras): ");
                nombre = Console.ReadLine();
            } while (!Regex.IsMatch(nombre, @"^[a-zA-Z]+$"));

            string apellido;
            do
            {
                Console.Write("Ingrese el nuevo apellido (solo letras): ");
                apellido = Console.ReadLine();
            } while (!Regex.IsMatch(apellido, @"^[a-zA-Z]+$"));

            Console.Write("Ingrese la nueva dirección: ");
            string direccion = Console.ReadLine();

            string telefono;
            do
            {
                Console.Write("Ingrese el nuevo teléfono (solo números): ");
                telefono = Console.ReadLine();
            } while (!Regex.IsMatch(telefono, @"^\d+$"));

            sistema.ActualizarCliente(cedula, nombre, apellido, direccion, telefono);
            Console.WriteLine("Cliente actualizado exitosamente.");
        }

        static void EliminarCliente(SistemaSupermercado sistema)
        {
            Console.Write("Ingrese la cédula del cliente a eliminar: ");
            string cedula = Console.ReadLine();
            sistema.EliminarCliente(cedula);
        }

        static void AgregarProducto(SistemaSupermercado sistema)
        {
            Console.Write("Ingrese el código del producto: ");
            string codigo = Console.ReadLine();
            Console.Write("Ingrese el nombre del producto: ");
            string nombre = Console.ReadLine();
            Console.Write("Ingrese la descripción del producto: ");
            string descripcion = Console.ReadLine();
            Console.Write("Ingrese la categoría del producto: ");
            string categoria = Console.ReadLine();
            Console.Write("Ingrese el precio de costo del producto: ");
            double precioCosto = double.Parse(Console.ReadLine());
            Console.Write("Ingrese el precio de venta del producto: ");
            double precioVenta = double.Parse(Console.ReadLine());
            Console.Write("Ingrese el stock del producto: ");
            int stock = int.Parse(Console.ReadLine());
            sistema.AgregarProducto(codigo, nombre, descripcion, categoria, precioCosto, precioVenta, stock);
        }

        static void ActualizarProducto(SistemaSupermercado sistema)
        {
            Console.Write("Ingrese el código del producto a actualizar: ");
            string codigo = Console.ReadLine();

            // Verificar si el producto existe
            Producto producto = sistema.BuscarProducto(codigo);
            if (producto == null)
            {
                Console.WriteLine("Producto no encontrado.");
                return;
            }

            Console.Write("Ingrese el nuevo nombre del producto: ");
            string nombre = Console.ReadLine();
            Console.Write("Ingrese la nueva descripción del producto: ");
            string descripcion = Console.ReadLine();
            Console.Write("Ingrese la nueva categoría del producto: ");
            string categoria = Console.ReadLine();

            double precioCosto;
            do
            {
                Console.Write("Ingrese el nuevo precio de costo del producto (número positivo): ");
            } while (!double.TryParse(Console.ReadLine(), out precioCosto) || precioCosto < 0);

            double precioVenta;
            do
            {
                Console.Write("Ingrese el nuevo precio de venta del producto (número positivo): ");
            } while (!double.TryParse(Console.ReadLine(), out precioVenta) || precioVenta < 0);

            int stock;
            do
            {
                Console.Write("Ingrese el nuevo stock del producto (número entero): ");
            } while (!int.TryParse(Console.ReadLine(), out stock) || stock < 0);

            sistema.ActualizarProducto(codigo, nombre, descripcion, categoria, precioCosto, precioVenta, stock);
            Console.WriteLine("Producto actualizado exitosamente.");
        }

        static void EliminarProducto(SistemaSupermercado sistema)
        {
            Console.Write("Ingrese el código del producto a eliminar: ");
            string codigo = Console.ReadLine();

            // Llamar al método para eliminar el producto
            sistema.EliminarProducto(codigo);
        }


        static void ListarProductosPorCategoria(SistemaSupermercado sistema)
        {
            Console.Write("Ingrese la categoría: ");
            string categoria = Console.ReadLine();
            sistema.ListadoProductosPorCategoria(categoria);
        }

        static void PromedioPreciosVentaPorCategoria(SistemaSupermercado sistema)
        {
            Console.Write("Ingrese la categoría: ");
            string categoria = Console.ReadLine();
            sistema.PromedioPreciosVentaPorCategoria(categoria);
        }

        static void ListarProductosPorPrecio(SistemaSupermercado sistema)
        {
            Console.Write("¿Desea listar por precio ascendente? (s/n): ");
            bool ascendente = Console.ReadLine().ToLower() == "s";
            sistema.ListadoProductosPorPrecio(ascendente);
        }

        static void ListarProductosPorGanancia(SistemaSupermercado sistema)
        {
            sistema.ListadoProductosPorGanancia();
        }

        static void ClienteMayorYMenorFacturado(SistemaSupermercado sistema)
        {
            Cliente mayorCliente = sistema.ClienteMayorFacturado();
            Cliente menorCliente = sistema.ClienteMenorFacturado();
            if (mayorCliente != null)
            {
                Console.WriteLine($"Cliente con mayor dinero facturado: {mayorCliente.Nombre} {mayorCliente.Apellido} - ${mayorCliente.TotalFacturado}");
            }
            if (menorCliente != null)
            {
                Console.WriteLine($"Cliente con menor dinero facturado: {menorCliente.Nombre} {menorCliente.Apellido} - ${menorCliente.TotalFacturado}");
            }
        }

        static void PromedioDineroFacturado(SistemaSupermercado sistema)
        {
            double promedioFacturado = sistema.PromedioDineroFacturado();
            Console.WriteLine($"Promedio de dinero facturado: ${promedioFacturado:0.00}");
        }
    }
}
