using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Simbirsoft_Weather.Models
{
    public class LoginViewModel
    {
       

        public EditModel EditModel { get; set; }

        public string ReturnUrl { get; set; }

        public LoginModel LoginModel { get; set; }

        public EditPassword EditPassword { get; set; }
    }
}
