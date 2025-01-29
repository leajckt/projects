using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Producto
{
    public string Codigo { get; set; }
    public string Nombre { get; set; }
    public string Descripcion { get; set; }
    public string Categoria { get; set; }
    public double PrecioCosto { get; set; }
    public double PrecioVenta { get; set; }
    public int Stock { get; set; }

    public Producto(string codigo, string nombre, string descripcion, string categoria, double precioCosto, double precioVenta, int stock)
    {
        Codigo = codigo;
        Nombre = nombre;
        Descripcion = descripcion;
        Categoria = categoria;
        PrecioCosto = precioCosto;
        PrecioVenta = precioVenta;
        Stock = stock;
    }
}
public class NodoProducto
{
    public Producto Producto { get; set; }
    public NodoProducto Siguiente { get; set; }

    public NodoProducto(Producto producto)
    {
        Producto = producto;
        Siguiente = null;
    }
}
