using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;

public class Factura
{
    private static int contadorFacturas = 0;
    public string Numero { get; private set; }
    public Cliente Cliente { get; private set; }
    public NodoFactura Productos { get; private set; }
    public double Total { get; private set; }

    public Factura(Cliente cliente)
    {
        contadorFacturas++;
        Numero = contadorFacturas.ToString("D4");
        Cliente = cliente;
        Productos = null;
        Total = 0.0;
    }

    public void AgregarProducto(Producto producto, int cantidad)
    {
        NodoFactura nuevoNodo = new NodoFactura(producto, cantidad);
        if (Productos == null)
        {
            Productos = nuevoNodo;
        }
        else
        {
            NodoFactura actual = Productos;
            while (actual.Siguiente != null)
            {
                actual = actual.Siguiente;
            }
            actual.Siguiente = nuevoNodo;
        }
        Total += producto.PrecioVenta * cantidad;
    }

    public void GenerarFactura()
    {
        if (Productos == null)
        {
            Console.WriteLine("No hay productos en la factura para mostrar.");
            return;
        }

        Console.WriteLine($"Factura Nº: {Numero}");
        Console.WriteLine($"C.I: {Cliente.Cedula}");
        Console.WriteLine($"Nombre: {Cliente.Nombre}");
        Console.WriteLine($"Apellido: {Cliente.Apellido}");
        Console.WriteLine("------------------------------------------------------------------");
        Console.WriteLine("Cod.    Producto             Precio   Cantidad   SubTotal");
        Console.WriteLine("------------------------------------------------------------------");
        NodoFactura actual = Productos;
        while (actual != null)
        {
            Producto producto = actual.Producto;
            int cantidad = actual.Cantidad;
            double subTotal = producto.PrecioVenta * cantidad;
            Console.WriteLine($"{producto.Codigo,-8} {producto.Nombre,-20} {producto.PrecioVenta,7} {cantidad,10} {subTotal,10:0.00}");
            actual = actual.Siguiente;
        }
        Console.WriteLine("------------------------------------------------------------------");
        Console.WriteLine($"Total ($) {Total:0.00}");

        // Actualizar el total facturado del cliente
        Cliente.TotalFacturado += Total;
    }
}



