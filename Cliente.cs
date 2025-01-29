using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Cliente
{
    public string Cedula { get; set; }
    public string Nombre { get; set; }
    public string Apellido { get; set; }
    public string Direccion { get; set; }
    public string Telefono { get; set; }
    public double TotalFacturado { get; set; }

    public Cliente(string cedula, string nombre, string apellido, string direccion, string telefono, double totalFacturado = 0.0)
    {
        Cedula = cedula;
        Nombre = nombre;
        Apellido = apellido;
        Direccion = direccion;
        Telefono = telefono;
        TotalFacturado = totalFacturado;
    }
}