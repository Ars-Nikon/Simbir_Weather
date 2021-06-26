using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Simbirsoft_Weather.Models
{
    public class EditPassword
    {
     
        public string Password { get; set; }

        
        public string NewPassword { get; set; }
        public object EditModel { get; internal set; }
    }
}
