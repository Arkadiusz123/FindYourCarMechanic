using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace FindYourCarMechanic
{
    [Authorize(Roles = "Mechanic")]
    public class PersonalSettingsController : Controller
    {
        private readonly MechanicDbContext _mechanicDbContext;
        private readonly UserManager<IdentityUser> _userManager;
        public PersonalSettingsController(MechanicDbContext context, UserManager<IdentityUser> userManager)
        {
            _mechanicDbContext = context;
            _userManager = userManager;
        }

        public IActionResult CofigureMechanicSettings()
        {
            var userEmail = _userManager.GetUserAsync(User).Result.Email;
            var mechanic = _mechanicDbContext.Mechanics.FirstOrDefault(x => x.Email == userEmail);
            return View(mechanic);
        }

        [HttpPost]
        public IActionResult CofigureMechanicSettings(Mechanic mechanic)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("IndexForMechanic", "Repair");
            }

            _mechanicDbContext.Update(mechanic);
            _mechanicDbContext.SaveChanges();
            return RedirectToAction("IndexForMechanic", "Repair");
        }
    }
}
