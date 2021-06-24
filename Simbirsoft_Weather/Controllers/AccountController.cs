using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Simbirsoft_Weather.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Simbirsoft_Weather.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ILogger<HomeController> _logger;
        private readonly CityContext Citydb;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, ILogger<HomeController> logger, CityContext cityContext)
        {
            Citydb = cityContext;
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }


        [HttpGet]
        public IActionResult Registration()
        {
            ViewBag.Cities = Citydb.Cities.ToList();

            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Registration(RegistrationModel model)
        {
            if (model.Email == null || Citydb.Cities.FirstOrDefault(x => x.City_Ru.ToLower() == model.Region.Trim().ToLower()) == null)
            {
                ModelState.AddModelError("Город", "Город не найдет");
            }
            if (ModelState.IsValid)
            {

                User user = new User { Date = DateTime.UtcNow.AddHours(+3), Email = model.Email.Trim(), UserName = model.Email.Trim(), Name = model.Name.Trim() };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, false);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            ViewBag.Cities = Citydb.Cities.ToList();
            return View(model);
        }

    }
}
