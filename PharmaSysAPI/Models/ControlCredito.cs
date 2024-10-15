namespace PharmaSysAPI.Models
{
    public class ControlCredito
    {
       public int IdControlCrédito { get; set;}  
       public int IdVenta { get; set; }
       public decimal MontoAbono { get; set; } 
       public DateTime FechaAbono { get; set; }
        public int  NumeroAbono { get; set; }
        public int  IdCrédito { get; set; }
    }
}
