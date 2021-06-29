using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Simbirsoft_Weather.Models;
using Simbirsoft_Weather.Services;

namespace Simbirsoft_Weather.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ILogger<HomeController> _logger;
        private readonly List<City> Cities;
        private readonly EventContext EventDb;
        private readonly IClothingConsultant ClothesGet;



        public HomeController(UserManager<User> userManager, IClothingConsultant cloth, SignInManager<User> signInManager, ILogger<HomeController> logger, EventContext eventContext, GetListCities cities)
        {
            ClothesGet = cloth;
            Cities = cities.Cities();
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


            var ip = Request.HttpContext.Connection.RemoteIpAddress.ToString();

            ViewBag.Cities = Cities;
            return View(new EventModel() { User = user });
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Event(EventModel eventModel)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            if (User.Identity.IsAuthenticated && _userManager.FindByNameAsync(User.Identity.Name).Result == null)
            {
                await _signInManager.SignOutAsync();
                return RedirectToAction("Index");
            }
            if (Cities.FirstOrDefault(x => x.City_Ru.ToLower() == eventModel.Event.Region.Trim().ToLower()) == null)
            {
                ModelState.AddModelError("Event.Region", "Город не найдет");
            }
            if (eventModel.Event.DateSendMessage > eventModel.Event.DateEvent)
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
            if (eventModel.Event.DateEvent > eventModel.Event.DateSendMessage.Value.AddDays(4))
            {
                ModelState.AddModelError("Event.DateEvent", $"Дата уведомления не может быть раньше чем {eventModel.Event.DateEvent.Value.AddDays(-4).ToString("M", new CultureInfo("ru-RU"))}");
            }
            if (ModelState.IsValid)
            {

                eventModel.Event.Id_User = user.Id;
                EventDb.Add(eventModel.Event);
                EventDb.SaveChanges();
            }
            else
            {
                ViewBag.Cities = Cities;
                return View(new EventModel() { User = user });
            }

            return RedirectToAction("Index", new IndexModel { Result = "Событие Запланировано" });
        }



        public async Task<IActionResult> Index(IndexModel indexModel)
        {
            var SendModel = new IndexModel();
            SendModel.Result = indexModel.Result;
            WeatherApi weather;
            SendModel.Region = "Москва";


            User user = null;

            if (User.Identity.IsAuthenticated && _userManager.FindByNameAsync(User.Identity.Name).Result == null)
            {
                await _signInManager.SignOutAsync();
                return RedirectToAction("Index");
            }//удаляет доступ у пользователя которго удалили из бд 

            if (User.Identity.IsAuthenticated)
            {
                user = await _userManager.FindByNameAsync(User.Identity.Name);
            }

            if (indexModel.Region != null && Cities.FirstOrDefault(x => x.City_Ru.ToLower() == indexModel.Region.Trim().ToLower()) != null)
            {
                weather = new WeatherApi(indexModel.Region);
                ViewBag.Cities = Cities;
            }
            else
            {
                if (indexModel.Region != null)
                {
                    SendModel.ErrorMessege = "Город не найден";
                    weather = new WeatherApi(SendModel.Region);
                }
                else
                {
                    if (User.Identity.IsAuthenticated)
                    {
                        if (user.Location != null)
                        {
                            SendModel.Region = user.Location;
                            weather = new WeatherApi(user.Location);
                        }
                        else
                        {
                            weather = new WeatherApi(SendModel.Region);
                        }
                    }
                    else
                    {
                        weather = new WeatherApi(SendModel.Region);
                    }
                }
            }

            ViewBag.Cities = Cities;
            SendModel.Weathers = weather.WheatherFor5Day();
            SendModel.WeatherForTime = weather.WheatherForTime(SendModel.Weathers[0].Date.ToString());
            SendModel.Region = SendModel.Weathers[0].City.Name;

            if (User.Identity.IsAuthenticated)
            {
                if (user.Gender.Value)
                {
                    SendModel.ClothesMan = ClothesGet.GetManRecommendation(SendModel.Weathers[0]);
                }
                else
                {
                    SendModel.ClothesWoman = ClothesGet.GetWomanRecommendation(SendModel.Weathers[0]);
                }
            }
            else
            {
                var clothes = ClothesGet.GetRecommendation(SendModel.Weathers[0]);
                SendModel.ClothesMan = clothes.Man;
                SendModel.ClothesWoman = clothes.Woman;
            }
            return View(SendModel);
        }


    }
}