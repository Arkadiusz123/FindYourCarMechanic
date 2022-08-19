using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FindYourCarMechanic
{
    [Authorize]
    public class RepairController : Controller
    {

        private readonly MechanicDbContext _mechanicDbContext;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IConfiguration _configuration;

        public RepairController(MechanicDbContext context, UserManager<IdentityUser> userManager, IConfiguration configuration)
        {
            _mechanicDbContext = context;
            _userManager = userManager;
            _configuration = configuration;
        }

        public IActionResult IndexForUser()
        {           
            var user = _userManager.GetUserAsync(User).Result;
            var currentUserRepairs = _mechanicDbContext.ConfirmedRepairs.Where(x => x.Car.IdentityUser == user).ToList();

            var currentUsersCars = _mechanicDbContext.Cars.Where(x => x.IdentityUser == user).ToList();
            var mechanicsList = _mechanicDbContext.Mechanics.ToList();
            var stringCarsToView = new List<string>();
            var stringMechanicToView = new List<string>();

            foreach (var item in currentUserRepairs)
            {
                var currentCar = currentUsersCars.FirstOrDefault(x => x.Id == item.Car.Id);
                stringCarsToView.Add($"{currentCar.Brand} {currentCar.Model}");

                var currentMechanic = mechanicsList.FirstOrDefault(x => x.Id == item.Mechanic.Id);
                stringMechanicToView.Add(currentMechanic.Name);
            }

            ViewBag.CarsList = stringCarsToView;
            ViewBag.MechanicsList = stringMechanicToView;

            return View("Index", currentUserRepairs);
        }

        public IActionResult IndexForMechanic()
        {
            var userEmail = _userManager.GetUserAsync(User).Result.Email;
            var currentMechanicRepairs = _mechanicDbContext.ConfirmedRepairs.Where(x => x.Mechanic.Email == userEmail).ToList();

            var currentCars = _mechanicDbContext.Cars.ToList();
            var mechanicsList = _mechanicDbContext.Mechanics.ToList();
            var stringCarsToView = new List<string>();

            foreach (var item in currentMechanicRepairs)
            {
                var currentCar = currentCars.FirstOrDefault(x => x.Id == item.Car.Id);
                stringCarsToView.Add($"{currentCar.Brand} {currentCar.Model}");
            }

            ViewBag.CarsList = stringCarsToView;

            return View("Index", currentMechanicRepairs);
        }

        public IActionResult CreateRepair(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            var user = _userManager.GetUserAsync(User).Result;
            ViewBag.CarsList = _mechanicDbContext.Cars.Where(x => x.IdentityUser == user).ToList();


            ConfirmedRepairTransferObject confirmedRepairObject = new ConfirmedRepairTransferObject();
            var selectedMechanic = _mechanicDbContext.Mechanics.FirstOrDefault(x => x.Id == id);
            confirmedRepairObject.MechanicId = selectedMechanic.Id;
            return View(confirmedRepairObject);
        }

        [HttpPost]
        public IActionResult CreateRepair(ConfirmedRepairTransferObject confirmedRepairObject)
        {
            var user = _userManager.GetUserAsync(User).Result;
            ViewBag.CarsList = _mechanicDbContext.Cars.Where(x => x.IdentityUser == user).ToList();

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var confirmedRepair = new ConfirmedRepair()
            {
                StartDate = confirmedRepairObject.GetFullDate(),
                Mechanic = _mechanicDbContext.Mechanics.FirstOrDefault(x => x.Id == confirmedRepairObject.MechanicId),
                Car = _mechanicDbContext.Cars.FirstOrDefault(x => x.Id == confirmedRepairObject.CarId)
            };

            if (confirmedRepair.StartDate < DateTime.Now)
            {
                ModelState.AddModelError(string.Empty, "You can not select a date that is earlier than the current date.");
                return View(confirmedRepairObject);
            }

            if (confirmedRepair.StartDate.DayOfWeek == DayOfWeek.Saturday || confirmedRepair.StartDate.DayOfWeek == DayOfWeek.Sunday)
            {
                ModelState.AddModelError(string.Empty, "Weekend days not available.");
                return View(confirmedRepairObject);
            }

            var bookedDates = _mechanicDbContext.ConfirmedRepairs
                .Where(x => x.Mechanic == confirmedRepair.Mechanic)
                .Select(x => x.StartDate)
                .ToList();

            if (bookedDates.Contains(confirmedRepair.StartDate))
            {
                ModelState.AddModelError(string.Empty, "Date already booked. Please choose a different date.");
                return View(confirmedRepairObject);
            }

            //var mail = new Mail(_configuration);
            
            //mail.SendEmail(user.Email, confirmedRepair);

            _mechanicDbContext.ConfirmedRepairs.Add(confirmedRepair);
            _mechanicDbContext.SaveChanges();

            return RedirectToAction("IndexForUser");
        }

        public IActionResult Delete(int? id) 
        {
            if (id == null)
            {
                return BadRequest();
            }

            var repairToDelete = _mechanicDbContext.ConfirmedRepairs.FirstOrDefault(x => x.Id == id);

            if (repairToDelete.StartDate <= DateTime.Now)
            {
                return Content("You can't delete repair that was already done.");
            }
            _mechanicDbContext.ConfirmedRepairs.Remove(repairToDelete);
            _mechanicDbContext.SaveChanges();
            return RedirectToAction("IndexForUser");
        }
       
    }
}
