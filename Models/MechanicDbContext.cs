using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;




namespace FindYourCarMechanic
{
    public class MechanicDbContext : IdentityDbContext<IdentityUser>
    {
        

        public MechanicDbContext(DbContextOptions<MechanicDbContext> options) : base(options)
        {
        }


        public DbSet<Car> Cars { get; set; }
        public DbSet<ConfirmedRepair> ConfirmedRepairs { get; set; }
        public DbSet<Mechanic> Mechanics { get; set; }


        
    }
}
