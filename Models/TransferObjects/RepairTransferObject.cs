using System;
using System.ComponentModel.DataAnnotations;

namespace FindYourCarMechanic
{
    public class ConfirmedRepairTransferObject
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please select date")]
        public DateTime StartDate { get; set; }
        public DateTime StartHour { get; set; }

        public int MechanicId { get; set; }

        [Required(ErrorMessage = "Please select your car")]
        public int CarId { get; set; }

        public DateTime GetFullDate() => new DateTime(StartDate.Year, StartDate.Month, StartDate.Day, StartHour.Hour, StartHour.Minute, 0); 
        
                       
        
    }
}
