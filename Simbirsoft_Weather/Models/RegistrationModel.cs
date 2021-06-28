using Simbirsoft_Weather.Models.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;


namespace Simbirsoft_Weather.Models
{
    public class RegistrationModel
    {

      
        [Display(Name = "Ваш e-mail:")]
        [EmailValidator(ErrorMessage = "Некорректный адрес")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Не указан пароль")]
        [Display(Name = "Пароль:")]
        [StringLength(20, ErrorMessage = "Поле {0} должно иметь минимум {2} и максимум {1} символов.", MinimumLength = 8)]
        public string Password { get; set; }

        [Display(Name = "Подтверждение пароля:")]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        public string PasswordConfirm { get; set; }

        [Display(Name = "Ваше имя:")]
        [Required(ErrorMessage = "Не указано имя")]
        [NameValidator(ErrorMessage = "Имя должно быть на кириллице или латинице без пробелов и цифр")]
        public string Name { get; set; }

        [Display(Name = "Город: ")]
        [Required(ErrorMessage = "Не указан регион")]
        public string Region { get; set; }


        [Required(ErrorMessage = "Не указан пол")]
        public bool? Gender { get; set; } // false - woman, true - man 
    }
}
