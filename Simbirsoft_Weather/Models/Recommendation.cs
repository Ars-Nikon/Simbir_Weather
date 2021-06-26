using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Simbirsoft_Weather.Models
{
    public class Recommendation
    {
        public Person Man { get; set; }
        public Person Woman { get; set; }

        public Recommendation()
        {
            Man = new Person();
            Woman = new Person();
        }
    }
}
