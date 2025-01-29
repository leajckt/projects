using System;
using System.Text.RegularExpressions;

public class SistemaSupermercado
{
    public NodoCliente Clientes { get; private set; }
    public NodoProducto Productos { get; private set; }

    public SistemaSupermercado()
    {
        Clientes = null;
        Productos = null;

        // Precargar clientes
        PrecargarClientes();

        // Precargar productos
        PrecargarProductos();
    }

    private void PrecargarClientes()
    {
        AgregarCliente("25929122", "María", "Gómez", "Calle el Limón", "4125551111", 50);
        AgregarCliente("21555666", "Juan", "López", "Porlamar", "4147778888", 45);
        AgregarCliente("14123456", "Luisa", "González", "San Juan", "4129991111", 55);
        AgregarCliente("26123123", "Luis", "Pérez", "Calle 5 de Julio", "4125553333", 60);
        AgregarCliente("15789789", "Ana", "Marcano", "La Asunción", "4148885555", 40);
    }

    private void PrecargarProductos()
    {
        AgregarProducto("Cod1928", "Jabón Protex", "Jabón de baño", "Cuidado Personal", 0.5, 1, 100);
        AgregarProducto("Cod1122", "Champú Nivea", "Champú para personas", "Cuidado Personal", 2.3, 4, 50);
        AgregarProducto("Cod1281", "Queso blanco", "Queso blanco criollo", "Charcutería", 2, 5, 30);
        AgregarProducto("Cod2211", "Jamón de Pavo", "Jamón de pavo nacional", "Charcutería", 4, 6.5, 20);
        AgregarProducto("Cod1255", "Crema Cero", "Crema para bebés", "Cuidado Personal", 3, 5, 40);
        AgregarProducto("Cod9922", "Vino Blanco", "Vino importado de Francia", "Bebidas Alcohólicas", 10, 15, 15);
        AgregarProducto("Cod3322", "Carne Económica", "Carne nacional de bajo costo", "Charcutería", 3, 5, 25);
        AgregarProducto("Cod1099", "Cerveza Polar", "Cerveza nacional", "Bebidas Alcohólicas", 0.3, 0.5, 200);
        AgregarProducto("Cod8811", "Lavaplatos Axion", "Lavaplatos para el hogar", "Hogar y Limpieza", 2, 4, 50);
        AgregarProducto("Cod1966", "Jabón Lix", "Jabón de baño", "Cuidado Personal", 1, 1.5, 60);
        AgregarProducto("Cod9977", "Jabón Azul", "Jabón para lavar", "Hogar y Limpieza", 0.5, 1, 70);
        AgregarProducto("Cod1234", "Limpia Ventanas", "Producto para limpiar vidrios", "Hogar y Limpieza", 2, 3, 40);
    }

    public Cliente BuscarCliente(string cedula)
    {
        NodoCliente actual = Clientes;
        while (actual != null)
        {
            if (actual.Cliente.Cedula == cedula)
                return actual.Cliente;
            actual = actual.Siguiente;
        }
        return null;
    }

    public void AgregarCliente(string cedula, string nombre, string apellido, string direccion, string telefono, double totalFacturado = 0.0)
    {
        Cliente nuevoCliente = new Cliente(cedula, nombre, apellido, direccion, telefono, totalFacturado);
        NodoCliente nuevoNodo = new NodoCliente(nuevoCliente);
        if (Clientes == null)
        {
            Clientes = nuevoNodo;
        }
        else
        {
            NodoCliente actual = Clientes;
            while (actual.Siguiente != null)
            {
                actual = actual.Siguiente;
            }
            actual.Siguiente = nuevoNodo;
        }
    }
    public void RegistrarCliente(string cedula, string nombre, string apellido, string direccion, string telefono)
    {
        if (!Regex.IsMatch(cedula, @"^\d+$"))
        {
            throw new ArgumentException("Cédula inválida. Debe contener solo números.");
        }
        if (!Regex.IsMatch(nombre, @"^[a-zA-Z]+$"))
        {
            throw new ArgumentException("Nombre inválido. Debe contener solo letras.");
        }
        if (!Regex.IsMatch(apellido, @"^[a-zA-Z]+$"))
        {
            throw new ArgumentException("Apellido inválido. Debe contener solo letras.");
        }
        if (!Regex.IsMatch(telefono, @"^\d+$"))
        {
            throw new ArgumentException("Teléfono inválido. Debe contener solo números.");
        }

        // Verificar si la cédula ya está registrada
        if (BuscarCliente(cedula) != null)
        {
            Console.WriteLine("No se puede registrar el cliente porque ya está registrado.");
            return;
        }

        AgregarCliente(cedula, nombre, apellido, direccion, telefono);
        Console.WriteLine("Cliente registrado exitosamente.");
    }
    public void ActualizarCliente(string cedula, string nuevoNombre, string nuevoApellido, string nuevaDireccion, string nuevoTelefono)
    {
        Cliente cliente = BuscarCliente(cedula);
        if (cliente != null)
        {
            cliente.Nombre = nuevoNombre;
            cliente.Apellido = nuevoApellido;
            cliente.Direccion = nuevaDireccion;
            cliente.Telefono = nuevoTelefono;
            Console.WriteLine("Cliente actualizado exitosamente.");
        }
        else
        {
            Console.WriteLine("Cliente no encontrado.");
        }
    }
    public void EliminarCliente(string cedula)
    {
        NodoCliente actual = Clientes;
        NodoCliente anterior = null;

        while (actual != null)
        {
            if (actual.Cliente.Cedula == cedula)
            {
                if (anterior == null) // El nodo a eliminar es el primero
                {
                    Clientes = actual.Siguiente;
                }
                else
                {
                    anterior.Siguiente = actual.Siguiente;
                }
                Console.WriteLine("Cliente eliminado exitosamente.");
                return;
            }
            anterior = actual;
            actual = actual.Siguiente;
        }
        Console.WriteLine("Cliente no encontrado.");
    }
    public Producto BuscarProducto(string codigo)
    {
        NodoProducto actual = Productos;
        while (actual != null)
        {
            if (actual.Producto.Codigo == codigo)
            {
                return actual.Producto;
            }
            actual = actual.Siguiente;
        }
        return null;
    }

    public void AgregarProducto(string codigo, string nombre, string descripcion, string categoria, double precioCosto, double precioVenta, int stock)
    {
        Producto nuevoProducto = new Producto(codigo, nombre, descripcion, categoria, precioCosto, precioVenta, stock);
        NodoProducto nuevoNodo = new NodoProducto(nuevoProducto);
        if (Productos == null)
        {
            Productos = nuevoNodo;
        }
        else
        {
            NodoProducto actual = Productos;
            while (actual.Siguiente != null)
            {
                actual = actual.Siguiente;
            }
            actual.Siguiente = nuevoNodo;
        }
    }

    public void ActualizarProducto(string codigo, string nuevoNombre, string nuevaDescripcion, string nuevaCategoria, double nuevoPrecioCosto, double nuevoPrecioVenta, int nuevoStock)
    {
        Producto producto = BuscarProducto(codigo);
        if (producto != null)
        {
            producto.Nombre = nuevoNombre;
            producto.Descripcion = nuevaDescripcion;
            producto.Categoria = nuevaCategoria;
            producto.PrecioCosto = nuevoPrecioCosto;
            producto.PrecioVenta = nuevoPrecioVenta;
            producto.Stock = nuevoStock;
            Console.WriteLine("Producto actualizado exitosamente.");
        }
        else
        {
            Console.WriteLine("Producto no encontrado.");
        }
    }
    public void EliminarProducto(string codigo)
    {
        NodoProducto actual = Productos;
        NodoProducto anterior = null;

        while (actual != null)
        {
            if (actual.Producto.Codigo.Equals(codigo, StringComparison.OrdinalIgnoreCase)) // Comparación insensible a mayúsculas/minúsculas
            {
                if (anterior == null) // El nodo a eliminar es el primero
                {
                    Productos = actual.Siguiente;
                }
                else
                {
                    anterior.Siguiente = actual.Siguiente;
                }
                Console.WriteLine("Producto eliminado exitosamente.");
                return;
            }
            anterior = actual;
            actual = actual.Siguiente;
        }
        Console.WriteLine("Producto no encontrado.");
    }


    public void ListadoProductosPorCategoria(string categoria)
    {
        NodoProducto actual = Productos;
        bool categoriaEncontrada = false;

        Console.WriteLine($"Productos en la categoría: {categoria}");
        while (actual != null)
        {
            Producto producto = actual.Producto;
            if (producto.Categoria.Equals(categoria, StringComparison.OrdinalIgnoreCase))
            {
                if (!categoriaEncontrada)
                {
                    Console.WriteLine("---------------------------------------------------------------------");
                    Console.WriteLine("Código    Nombre             Descripción            Categoría     Precio Costo  Precio Venta  Stock");
                    Console.WriteLine("---------------------------------------------------------------------");
                    categoriaEncontrada = true;
                }
                Console.WriteLine($"{producto.Codigo,-10} {producto.Nombre,-18} {producto.Descripcion,-20} {producto.Categoria,-12} {producto.PrecioCosto,12:0.00} {producto.PrecioVenta,13:0.00} {producto.Stock,6}");
            }
            actual = actual.Siguiente;
        }

        if (!categoriaEncontrada)
        {
            Console.WriteLine($"No se encontraron productos en la categoría '{categoria}' o la categoría no existe.");
        }
        else
        {
            Console.WriteLine("---------------------------------------------------------------------");
        }
    }

    public void PromedioPreciosVentaPorCategoria(string categoria)
    {
        NodoProducto actual = Productos;
        double totalPrecio = 0;
        int count = 0;
        while (actual != null)
        {
            Producto producto = actual.Producto;
            if (producto.Categoria.Equals(categoria, StringComparison.OrdinalIgnoreCase))
            {
                totalPrecio += producto.PrecioVenta;
                count++;
            }
            actual = actual.Siguiente;
        }

        if (count > 0)
        {
            double promedio = totalPrecio / count;
            Console.WriteLine($"El promedio de precios de venta para la categoría {categoria} es: {promedio:0.00}");
        }
        else
        {
            Console.WriteLine($"No hay productos en la categoría {categoria}");
        }
    }

    public void ListadoProductosPorPrecio(bool ascendente)
    {
        List<Producto> listaProductos = new List<Producto>();
        NodoProducto actual = Productos;
        while (actual != null)
        {
            listaProductos.Add(actual.Producto);
            actual = actual.Siguiente;
        }

        if (ascendente)
        {
            listaProductos = listaProductos.OrderBy(p => p.PrecioVenta).ToList();
        }
        else
        {
            listaProductos = listaProductos.OrderByDescending(p => p.PrecioVenta).ToList();
        }

        Console.WriteLine("Productos ordenados por precio de venta:");
        Console.WriteLine("---------------------------------------------------------------------");
        Console.WriteLine("Código    Nombre             Descripción            Categoría     Precio Costo  Precio Venta  Stock");
        Console.WriteLine("---------------------------------------------------------------------");
        foreach (Producto producto in listaProductos)
        {
            Console.WriteLine($"{producto.Codigo,-10} {producto.Nombre,-18} {producto.Descripcion,-20} {producto.Categoria,-12} {producto.PrecioCosto,12:0.00} {producto.PrecioVenta,13:0.00} {producto.Stock,6}");
        }
        Console.WriteLine("---------------------------------------------------------------------");
    }

    public void ListadoProductosPorGanancia()
    {
        List<Producto> listaProductos = new List<Producto>();
        NodoProducto actual = Productos;
        while (actual != null)
        {
            listaProductos.Add(actual.Producto);
            actual = actual.Siguiente;
        }

        listaProductos = listaProductos.OrderByDescending(p => (p.PrecioVenta - p.PrecioCosto) / p.PrecioCosto).ToList();

        Console.WriteLine("Productos ordenados por porcentaje de ganancia:");
        Console.WriteLine("---------------------------------------------------------------------");
        Console.WriteLine("Código    Nombre             Descripción            Categoría     Precio Costo  Precio Venta  Stock");
        Console.WriteLine("---------------------------------------------------------------------");
        foreach (Producto producto in listaProductos)
        {
            double ganancia = (producto.PrecioVenta - producto.PrecioCosto) / producto.PrecioCosto * 100;
            Console.WriteLine($"{producto.Codigo,-10} {producto.Nombre,-18} {producto.Descripcion,-20} {producto.Categoria,-12} {producto.PrecioCosto,12:0.00} {producto.PrecioVenta,13:0.00} {producto.Stock,6}   {ganancia,6:0.00}%");
        }
        Console.WriteLine("---------------------------------------------------------------------");
    }

    public Cliente ClienteMayorFacturado()
    {
        if (Clientes == null) return null;

        NodoCliente actual = Clientes;
        Cliente mayorCliente = actual.Cliente;

        while (actual != null)
        {
            if (actual.Cliente.TotalFacturado > mayorCliente.TotalFacturado)
            {
                mayorCliente = actual.Cliente;
            }
            actual = actual.Siguiente;
        }
        return mayorCliente;
    }

    public Cliente ClienteMenorFacturado()
    {
        if (Clientes == null) return null;

        NodoCliente actual = Clientes;
        Cliente menorCliente = actual.Cliente;

        while (actual != null)
        {
            if (actual.Cliente.TotalFacturado < menorCliente.TotalFacturado)
            {
                menorCliente = actual.Cliente;
            }
            actual = actual.Siguiente;
        }
        return menorCliente;
    }

    public double PromedioDineroFacturado()
    {
        if (Clientes == null) return 0.0;

        NodoCliente actual = Clientes;
        double totalFacturado = 0;
        int count = 0;

        while (actual != null)
        {
            totalFacturado += actual.Cliente.TotalFacturado;
            count++;
            actual = actual.Siguiente;
        }
        return totalFacturado / count;
    }
}
