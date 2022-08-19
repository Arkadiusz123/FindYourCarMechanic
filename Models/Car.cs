using Microsoft.AspNetCore.Identity;

namespace FindYourCarMechanic
{
    public class Car
    {
        public int Id { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public int YearOfProduction { get; set; }
        public IdentityUser IdentityUser { get; set; }

    }
}
