using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;

namespace FindYourCarMechanic
{

    [Authorize(Roles = "User")]
    public class CarController : Controller
    {
        private readonly MechanicDbContext _mechanicDbContext;
        private readonly UserManager<IdentityUser> _userManager;
        public CarController(MechanicDbContext context, UserManager<IdentityUser> userManager)
        {
            _mechanicDbContext = context;
            _userManager = userManager;
        }

        
        public IActionResult Index()
        {
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return View(_mechanicDbContext.Cars.Where(x => x.IdentityUser.Id == currentUserId));
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Car car)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index");
            }

            var user = _userManager.GetUserAsync(User).Result;
            car.IdentityUser = user;

            _mechanicDbContext.Cars.Add(car);
            _mechanicDbContext.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int? id)
        {
            if (id == null) return BadRequest();
            var currentCar = _mechanicDbContext.Cars.FirstOrDefault(x => x.Id == id);
            return View(currentCar);
        }

        [HttpPost]
        public IActionResult Edit(Car car) 
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index");
            }

            _mechanicDbContext.Update(car);
            _mechanicDbContext.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            
            var currentCar = _mechanicDbContext.Cars.FirstOrDefault(x => x.Id == id);
            var repairsWithThisCar = _mechanicDbContext.ConfirmedRepairs.Where(x => x.Car == currentCar).ToList();

            if (repairsWithThisCar.Count > 0)
            {
                return Content("You can't delete that car, because there are visits booked for it.");
            }

            _mechanicDbContext.Remove(currentCar);
            _mechanicDbContext.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
