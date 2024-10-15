namespace PharmaSysAPI.Models
{
    public class Compras
    {
        public int IdCompra { get; set; }
       public int idProveedor { get;set; }
       public DateTime fechaCompra { get; set; }

        public decimal totalCompra {  get; set; }   
    }
}
