namespace PharmaSysAPI.Models
{
    public class DetalleVenta
    {

      public int IdDetalleVenta { get; set; }
      public int idVenta {  get; set; }
      public int CantidadVendida {  get; set; }
      public decimal PrecioVenta { get; set; }
      public decimal SubtotalFactura {  get; set; }
      public int idProducto {  get; set; }    

    }
}
