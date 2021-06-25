using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Simbirsoft_Weather.Models
{
    [ViewComponent]

    public class Icon
    {
        public IViewComponentResult Invoke(string main)
        {
            Dictionary<string, string> TypeWheather = new Dictionary<string, string>()
            {
            {"небольшой дождь", @"C:\Users\nikar\Desktop\SimbirSoft\Simbir_Weather\Simbirsoft_Weather\wwwroot\WeatherFront\images\icons"}


            };

            return new HtmlContentViewComponentResult(
                new HtmlString($"<img src=\"{TypeWheather.GetValueOrDefault(main)}\"width=90>")
            );
        }
    }
}
