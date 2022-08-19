using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FindYourCarMechanic
{
    public class AccountController : Controller
    {
        private readonly MechanicDbContext _mechanicDbContext;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        public AccountController(MechanicDbContext context, UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager)
        {
            _mechanicDbContext = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Index()
        {
            return View();
        }


        public IActionResult Register()
        {
            return View();
        }

        
        [HttpPost]
        public async Task<IActionResult> Register(User model)
        {
            if (ModelState.IsValid)
            {
                var createdUser = new IdentityUser
                {
                    UserName = model.Email,
                    Email = model.Email

                };

                var result = await _userManager.CreateAsync(createdUser, model.Password);
                await _userManager.AddToRoleAsync(createdUser, model.Role);

                if (result.Succeeded)
                {
                    if(model.Role == "Mechanic")
                    {
                        var mechanic = new Mechanic
                        {
                            Email = model.Email
                        };
                        _mechanicDbContext.Mechanics.Add(mechanic);
                        _mechanicDbContext.SaveChanges();
                        
                    }
                    return RedirectToAction("Login");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                ModelState.AddModelError(string.Empty, "Invalid Register Attempt");
            }
            return View(model);
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(User user)
        {
            if (ModelState.IsValid)
            {

                var result = await _signInManager.PasswordSignInAsync(user.Email, user.Password, false, false);


                if (result.Succeeded)
                {
                    var currentUser = _userManager.FindByEmailAsync(user.Email).Result;
                    var role = _userManager.GetRolesAsync(currentUser).Result;
                    
                    if (role.Contains("User"))
                    {
                        return RedirectToAction("IndexForUser", "Repair");
                    }
                    else if (role.Contains("Mechanic"))
                    {
                        return RedirectToAction("IndexForMechanic", "Repair");
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    
                }

                ModelState.AddModelError(string.Empty, "Invalid Login Attempt");

            }
            return View(user);
        }

        [Authorize]
        public IActionResult Logout()
        {
            _signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }
    }
}
