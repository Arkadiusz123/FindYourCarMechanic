using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace FindYourCarMechanic
{
    [Authorize(Roles = "User")]
    public class MechanicController : Controller
    {
        private readonly MechanicDbContext _mechanicDbContext;
        public MechanicController(MechanicDbContext context)
        {
            _mechanicDbContext = context;
        }
        public IActionResult Index(string searchString)
        {
            var mechanicsList = _mechanicDbContext.Mechanics.ToList();

            if (!String.IsNullOrEmpty(searchString))
            {
                mechanicsList = mechanicsList.Where(x => x.City.ToLower()!.Contains(searchString.ToLower())).ToList();
            }

            return View(mechanicsList);
        }
    }
}
