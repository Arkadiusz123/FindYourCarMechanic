using System.ComponentModel.DataAnnotations;

namespace FindYourCarMechanic
{
    public class User
    {
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }
}
