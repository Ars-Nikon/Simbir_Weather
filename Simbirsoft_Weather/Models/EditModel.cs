using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Simbirsoft_Weather.Models
{
    public class EditModel
    {
        public string Result { get; set; }

        public string Email { get; set; }

        public bool? Gender { get; set; }

        public string Password { get; set; }

        public string Region { get; set; }

        public string Name { get; set; }

        public void AddUser(User user)
        {
            Email = user.Email;
            Gender = user.Gender;
            Region = user.Location;
            Name = user.Name;
        }
    }


}
