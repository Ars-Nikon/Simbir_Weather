using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Simbirsoft_Weather.Models
{
    public class User : IdentityUser
    {
        public string Location { get; set; }

        public string Name { get; set; }

        public DateTime Date{ get; set; }

        public bool? Gender { get; set; }
    }
}
