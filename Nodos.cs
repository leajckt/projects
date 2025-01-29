using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class NodoCliente
{
    public Cliente Cliente { get; set; }
    public NodoCliente Siguiente { get; set; }

    public NodoCliente(Cliente cliente)
    {
        Cliente = cliente;
        Siguiente = null;
    }
}

public class NodoFactura
{
    public Producto Producto { get; set; }
    public int Cantidad { get; set; }
    public NodoFactura Siguiente { get; set; }

    public NodoFactura(Producto producto, int cantidad)
    {
        Producto = producto;
        Cantidad = cantidad;
        Siguiente = null;
    }
}
