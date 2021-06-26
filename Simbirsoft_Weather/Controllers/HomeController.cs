using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Simbirsoft_Weather.Models;

namespace Simbirsoft_Weather.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ILogger<HomeController> _logger;

        public HomeController(UserManager<User> userManager, SignInManager<User> signInManager, ILogger<HomeController> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                if (_userManager.FindByNameAsync(User.Identity.Name).Result == null)
                {
                    await _signInManager.SignOutAsync();
                }
            }


            var weather = new Weather_api("Краснодар");
            var weather5days = weather.WheatherFor5Day();
            var WeatherForTime = weather.WheatherForTime(weather5days[0].Date.ToString());
            return View(new IndexModel { Weathers = weather5days, Region = "Краснодар", WeatherForTime = WeatherForTime });
        }


    }
}