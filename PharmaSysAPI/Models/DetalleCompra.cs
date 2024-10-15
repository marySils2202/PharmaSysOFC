namespace PharmaSysAPI.Models
{
    public class DetalleCompra
    {
        public int idDetalleCompra { get; set; }
        public int idCompra { get; set; }
        public int idProducto { get; set; }
        public int CantidadCompra { get; set; }
        public decimal PrecioCompra { get; set; }
        public decimal SubtotalCompra { get; set; }
    }
}
