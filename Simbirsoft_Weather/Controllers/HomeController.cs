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
        private readonly CityContext Citydb;

        public HomeController(UserManager<User> userManager, SignInManager<User> signInManager, ILogger<HomeController> logger, CityContext cityContext)
        {
            Citydb = cityContext;
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }

        public async Task<IActionResult> Index(string lat, string lon, IndexModel indexModel)
        {
            string location = "Москва";
            bool NeedGeolocation = true;
            WeatherApi weather;

            if (User.Identity.IsAuthenticated && _userManager.FindByNameAsync(User.Identity.Name).Result == null)
            {
                    await _signInManager.SignOutAsync();
                    return RedirectToAction("Index");
            }

            if (indexModel.Region != null && Citydb.Cities.FirstOrDefault(x => x.City_Ru.ToLower() == indexModel.Region.Trim().ToLower()) != null)
            {
                weather = new WeatherApi(indexModel.Region);
                NeedGeolocation = false;

                ViewBag.Cities = Citydb.Cities.ToList();
            }
            else
            {
                if (User.Identity.IsAuthenticated)
                {

                    var user = await _userManager.FindByNameAsync(User.Identity.Name);

                    if (user.Location != null)
                    {
                        location = user.Location;
                        weather = new WeatherApi(user.Location);
                    }
                    else
                    {
                        weather = new WeatherApi(location);
                    }

                    NeedGeolocation = false;
                }
                else
                {
                    if (lat != null && lon != null)
                    {
                        weather = new WeatherApi(lat, lon);
                        NeedGeolocation = false;

                    }
                    else
                    {
                        weather = new WeatherApi(location);
                    }
                }
            }
        
            ViewBag.Cities = Citydb.Cities.ToList();
            var weather5days = weather.WheatherFor5Day();
            var WeatherForTime = weather.WheatherForTime(weather5days[0].Date.ToString());
            location = weather5days[0].City.Name;
            return View(new IndexModel { Weathers = weather5days, Region = location, WeatherForTime = WeatherForTime, NeedGeolocation = NeedGeolocation });
        }


    }
}