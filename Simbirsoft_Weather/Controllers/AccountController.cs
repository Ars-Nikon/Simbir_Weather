using Microsoft.AspNetCore.Authorization;
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

            if (Citydb.Cities.FirstOrDefault(x => x.City_Ru.ToLower() == model.Region.Trim().ToLower()) == null)
            {
                ModelState.AddModelError("Город", "Город не найдет");
            }
            if (model.Email != null)
            {
                var result = await _userManager.FindByEmailAsync(model.Email);
                if (result != null)
                {
                    ModelState.AddModelError("Email", "Почта уже занята");
                }
            }
            if (ModelState.IsValid)
            {
                User user = new User { Date = DateTime.UtcNow.AddHours(+3), Email = model.Email.Trim(), UserName = model.Email.Trim(), Name = model.Name.Trim(), Gender = model.Gender, Location = model.Region.Trim() };
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

        [HttpGet]
        public async Task<IActionResult> Login(EditModel edit, string returnUrl = null)
        {
            if (User.Identity.IsAuthenticated)
            {
                ViewBag.Cities = Citydb.Cities.ToList();
                var user = await _userManager.FindByNameAsync(User.Identity.Name);
                return View(new LoginViewModel { EditModel = new EditModel { Result = edit?.Result ?? null, Email = user.Email, Gender = user.Gender, Name = user.Name, Region = user.Location }, ReturnUrl = returnUrl });
            }
            return View(new LoginViewModel { ReturnUrl = returnUrl });
        }


        [HttpPost]
        [Authorize]
        public async Task<IActionResult> EditGender(LoginViewModel loginViewModel)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            var model = loginViewModel.EditModel;
            if (model.Gender == null)
            {
                ModelState.AddModelError("Пол", "Выберите Пол");
                ViewBag.Cities = Citydb.Cities.ToList();
                loginViewModel.EditModel.AddUser(user);
                return View("login", loginViewModel);
            }

            user.Gender = model.Gender;
            await _userManager.UpdateAsync(user);
            model.Result = "Пол изменен";
            return RedirectToAction("Login", model);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> EditRegion(LoginViewModel loginViewModel)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var model = loginViewModel.EditModel;
            if (model.Region == null)
            {
                ModelState.AddModelError("EditModel.Region", "Введите Город");
                ViewBag.Cities = Citydb.Cities.ToList();
                loginViewModel.EditModel.AddUser(user);
                return View("login", loginViewModel);
            }
            else
            {
                if (Citydb.Cities.FirstOrDefault(x => x.City_Ru.ToLower() == model.Region.Trim().ToLower()) == null)
                {
                    ModelState.AddModelError("EditModel.Region", "Город не найдет");
                    ViewBag.Cities = Citydb.Cities.ToList();
                    loginViewModel.EditModel.AddUser(user);
                    return View("login", loginViewModel);
                }
            }

            user.Location = model.Region;
            await _userManager.UpdateAsync(user);
            model.Result = "Город изменен";
            return RedirectToAction("Login", model);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> EditName(LoginViewModel loginViewModel)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var model = loginViewModel.EditModel;
            if (model.Name == null)
            {
                ModelState.AddModelError("EditModel.Name", "Введите имя");
                ViewBag.Cities = Citydb.Cities.ToList();
                loginViewModel.EditModel.AddUser(user);
                return View("login", loginViewModel);
            }


            user.Name = model.Name;
            await _userManager.UpdateAsync(user);
            model.Result = "Имя изменено";
            return RedirectToAction("Login", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> EditPassword(LoginViewModel loginViewModel)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var model = loginViewModel.EditPassword;

            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            if (model.Password != null)
            {
                if (!_userManager.CheckPasswordAsync(user, model.Password).Result)
                {
                    ModelState.AddModelError("EditPassword.Password", "Не правильный пароль");
                }
            }
            else
            {
                ModelState.AddModelError("EditPassword.Password", "Введите Пароль");
            }

            if (model.NewPassword == null)
            {
                ModelState.AddModelError("EditPassword.NewPassword", "Введите Пароль");
            }


            if (ModelState.IsValid)
            {
                var _passwordValidator =
               HttpContext.RequestServices.GetService(typeof(IPasswordValidator<User>)) as IPasswordValidator<User>;
                var _passwordHasher =
                    HttpContext.RequestServices.GetService(typeof(IPasswordHasher<User>)) as IPasswordHasher<User>;

                IdentityResult result =
                    await _passwordValidator.ValidateAsync(_userManager, user, model.NewPassword);
                if (result.Succeeded)
                {
                    user.PasswordHash = _passwordHasher.HashPassword(user, model.NewPassword);
                    await _userManager.UpdateAsync(user);
                    loginViewModel.EditModel = new EditModel();
                    loginViewModel.EditModel.Result = "Пароль изменен";
                    return RedirectToAction("Login", loginViewModel.EditModel);
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("EditPassword.NewPassword", error.Description);
                    }
                }
            }
            ViewBag.Cities = Citydb.Cities.ToList();
            loginViewModel.EditModel = new EditModel();
            loginViewModel.EditModel.AddUser(user);
            return View("login", loginViewModel);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> EditEmail(LoginViewModel model)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            if (model.EditModel.Password == null)
            {
                ModelState.AddModelError("EditModel.Password", "Не правильный пароль");
            }
            else
            {
                if (!_userManager.CheckPasswordAsync(user, model.EditModel.Password).Result)
                {
                    ModelState.AddModelError("EditModel.Password", "Не правильный пароль");
                }
            }
            if (model.EditModel.Email == null)
            {
                ModelState.AddModelError("EditModel.Email", "Введите имя почты");
            }
            else
            {
                if (model.EditModel.Email == user.Email)
                {
                    ModelState.AddModelError("EditModel.Email", "Введите новую почту");
                }

                var result = await _userManager.FindByEmailAsync(model.EditModel.Email);
                if (result != null)
                {
                    ModelState.AddModelError("EditModel.Email", "Почта уже занята");
                }
            }
            if (ModelState.IsValid)
            {
                var result = await _userManager.SetEmailAsync(user, model.EditModel.Email);
                if (result.Succeeded)
                {
                    model.EditModel.Result = "e-mail изменен";
                    return RedirectToAction("Login", model.EditModel);
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("EditModel.Email", error.Description);
                    }
                }

            }
            ViewBag.Cities = Citydb.Cities.ToList();

            model.EditModel.AddUser(user);
            return View("login", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            var model = loginViewModel.LoginModel;
            if (ModelState.IsValid)
            {

                var result =
                    await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
                if (result.Succeeded)
                {

                    if (!string.IsNullOrEmpty(loginViewModel.ReturnUrl) && Url.IsLocalUrl(loginViewModel.ReturnUrl))
                    {
                        return Redirect(loginViewModel.ReturnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }


                }
                else
                {
                    ModelState.AddModelError("Password", "Не правильный логин или пароль");
                }
            }
            return View(loginViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
