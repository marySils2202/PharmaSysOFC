using Microsoft.EntityFrameworkCore;


namespace PharmaSysAPI.Models
{
    public class BDPharmaSystemContext : DbContext
    {
        public BDPharmaSystemContext(DbContextOptions<BDPharmaSystemContext> options)
       : base(options)
        {


        }
    }
}