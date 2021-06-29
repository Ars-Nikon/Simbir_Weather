using System.Collections.Generic;

namespace Simbirsoft_Weather.Models
{
    public class Person
    {
        public List<Clothes> Head { get; set; }
        public List<Clothes> BodyTop { get; set; }
        public List<Clothes> BodyBottom { get; set; }
        public List<Clothes> Legs { get; set; }
        public List<Clothes> Other { get; set; }
    }
}
