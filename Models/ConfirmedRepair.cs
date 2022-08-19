using System;

namespace FindYourCarMechanic
{
    public class ConfirmedRepair
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }      
        public Mechanic Mechanic { get; set; }
        public Car Car { get; set; }
    }
}
