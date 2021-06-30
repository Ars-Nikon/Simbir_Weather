using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
        private readonly List<City> Cities;
        private readonly EventContext EventDb;


        

        public HomeController(UserManager<User> userManager, SignInManager<User> signInManager, ILogger<HomeController> logger, EventContext eventContext, CityContext cityContext)
        {
            Cities = cityContext.Cities.ToList();
            EventDb = eventContext;
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }


        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Event()
        {
            if (User.Identity.IsAuthenticated && _userManager.FindByNameAsync(User.Identity.Name).Result == null)
            {
                await _signInManager.SignOutAsync();
                return RedirectToAction("Index");
            }

            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            ViewBag.Cities = Cities;
            return View(new EventModel() { User = user });
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Event(EventModel eventModel)
        {

            if (User.Identity.IsAuthenticated && _userManager.FindByNameAsync(User.Identity.Name).Result == null)
            {
                await _signInManager.SignOutAsync();
                return RedirectToAction("Index");
            }
            if (Cities.FirstOrDefault(x => x.City_Ru.ToLower() == eventModel.Event.Region.Trim().ToLower()) == null)
            {
                ModelState.AddModelError("Event.Region", "Город не найдет");
            }
            if (eventModel.Event.DateSendMessage>eventModel.Event.DateEvent)
            {
                ModelState.AddModelError("Event.DateEvent", "Дата прогноза не может быть позже даты отправки уведомления");
            }
            if (eventModel.Event.DateSendMessage < DateTime.UtcNow.AddHours(3))
            {
                ModelState.AddModelError("Event.DateSendMessage", "Дата отпраки уведомления не может быть раньше сегодняшнего числа");
            }
            if (eventModel.Event.DateEvent <= DateTime.UtcNow.AddHours(3))
            {
                ModelState.AddModelError("Event.DateEvent", "Дата прогноза должна быть больше сегодняшнего числа");
            }
            if (ModelState.IsValid)
            {
                EventDb.Add(eventModel.Event);
                EventDb.SaveChanges();
            }
            else
            {
                var user = await _userManager.FindByNameAsync(User.Identity.Name);
                ViewBag.Cities = Cities;
                return View(new EventModel() { User = user });
            }

            return RedirectToAction("Index", new IndexModel{ Result ="Событие Запланировано" });
        }



        public async Task<IActionResult> Index(IndexModel indexModel)
        {
            string location = "Москва";

            WeatherApi weather;

            if (User.Identity.IsAuthenticated && _userManager.FindByNameAsync(User.Identity.Name).Result == null)
            {
                await _signInManager.SignOutAsync();
                return RedirectToAction("Index");
            }



            IPAddress remoteIpAddress = Request.HttpContext.Connection.RemoteIpAddress;

            Console.WriteLine(remoteIpAddress.ToString());



            if (indexModel.Region != null && Cities.FirstOrDefault(x => x.City_Ru.ToLower() == indexModel.Region.Trim().ToLower()) != null)
            {
                weather = new WeatherApi(indexModel.Region.Trim());
                ViewBag.Cities = Cities;
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


                }
                else
                {


                    weather = new WeatherApi(location);

                }
            }
            ViewBag.Cities = Cities;
            var weather5days = weather.WheatherFor5Day();
            var WeatherForTime = weather.WheatherForTime(weather5days[0].Date.ToString());
            location = weather5days[0].City.Name;
            return View(new IndexModel { Weathers = weather5days, Region = location, WeatherForTime = WeatherForTime, Result =indexModel.Result });
        }


    }
}