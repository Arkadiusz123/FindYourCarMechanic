using System.Collections.Generic;

namespace FindYourCarMechanic
{
    public class Mechanic
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string Address { get; set; }       
        public ICollection<ConfirmedRepair> ConfirmedRepairs { get; set; }

    }
}
