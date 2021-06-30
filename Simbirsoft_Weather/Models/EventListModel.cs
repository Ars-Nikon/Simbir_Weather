using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Simbirsoft_Weather.Models
{
    public class EventListModel
    {
        public List<Event> Events { get; set; }

        public string ErrorMessege { get; set; }
    }
}
