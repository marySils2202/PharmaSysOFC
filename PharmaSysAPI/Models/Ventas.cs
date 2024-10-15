namespace PharmaSysAPI.Models
{
    public class Ventas
    {

    public int IdVenta { get; set; }
    public string tipoFactura { get; set; }
    public DateTime fechaVenta { get; set; }
    public int idCliente {  get; set; }
    public int idEmpleado {  get; set; }
    public decimal totalVenta { get; set; }
    public string tipoPago {  get; set; }
    public decimal iva { get; set; }
    public string estado { get; set; }
    }
}
