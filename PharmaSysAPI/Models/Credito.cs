namespace PharmaSysAPI.Models
{
    public class Credito
    {
        public int IdCredito { get; set; }
        public int Plazo { get; set; }
        public decimal MontoTotal { get; set; }
        public string Estado { get; set; }
    }
}
